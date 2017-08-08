using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using Microsoft.Win32;

namespace CLCommunicationLib.Serial
{
    /// <summary>
    /// 科陆洲电子串口控制对象
    /// </summary>
    public class COM32 : IDisposable
    {
        private System.Threading.ManualResetEvent mre = new System.Threading.ManualResetEvent(true);

        /// <summary>
        /// 串口对象
        /// </summary>
        private SerialPort spCom;

        /// <summary>
        /// 是否是半双工工作模式
        /// </summary>
        private bool isHalfDuplex = true;

        /// <summary>
        /// 返回字节间超时时间（半双工有效）
        /// </summary>
        private int byteTimeOut = 500;

        /// <summary>
        /// 串口接收线程互斥
        /// </summary>
        private object ThreadObject = new object();

        /// <summary>
        /// 串口发送线程互斥
        /// </summary>
        private object ThreadSendObject = new object();

        /// <summary>
        /// 数据帧解析接口实例
        /// </summary>
        private IFrameAnalysis frameAnalysis = null;

        /// <summary>
        /// 返回数据帧队列互斥 
        /// </summary>
        private object ThreadreadQueueObj = new object();
        /// <summary>
        /// 串口返回帧队列先进先出栈
        /// </summary>
        private Queue<byte[]> readQueueBuff = new Queue<byte[]>();
        /// <summary>
        /// 发送数据帧队列互斥
        /// </summary>
        private object ThreadSendQueueObj = new object();
        /// <summary>
        /// 串口异步发送数据队列
        /// </summary>
        private Queue<byte[]> sendQueueBuff = new Queue<byte[]>();
        /// <summary>
        /// 串口异步发送操作线程
        /// </summary>
        private System.Threading.Thread threadSendData = null;
        /// <summary>
        /// 全双工的时候对读取线程停止标志。
        /// </summary>
        /// 
        private bool stopReceive = false;


        #region -------------------委托-------------------
        /// <summary>
        /// 异步回调委托
        /// </summary>
        /// <param name="buff">返回的BUFF</param>
        public delegate void AsyncSerailCallback(byte[] buff);

        #endregion

        #region--------------------属性--------------------
        /// <summary>
        /// 返回缓冲区帧数量（异步监听下无效）
        /// </summary>
        public int frameCount
        {
            get { return readQueueBuff.Count; }
        }

        /// <summary>
        /// 设置数据帧解析接口实例
        /// </summary>
        public IFrameAnalysis FrameAnalysis
        {
            set { frameAnalysis = value; }
        }

        /// <summary>
        /// 字节间超时时间
        /// </summary>
        public int ByteTimeOut
        {
            get { return byteTimeOut; }
            set { byteTimeOut = value; }
        }
        /// <summary>
        /// 帧间超时时间(半双工有效)
        /// </summary>
        public int TimeOut
        {
            get { return spCom.ReadTimeout; }
            set { spCom.ReadTimeout = value; }
        }
        /// <summary>
        /// 是否是半双工工作模式(只在全双工模式下有效)
        /// </summary>
        public bool IsHalfDuplex
        {
            get { return isHalfDuplex; }
            set { isHalfDuplex = value; }
        }

        /// <summary>
        /// 设置通信参数(1200,e,8,1)
        /// </summary>
        public string Settings
        {
            set
            {
                string[] tmp = value.Split(',');
                if (tmp.Length != 4)
                {
                    throw new Exception("通信参数设置格式错误，参数格式为“波特率,校验方式,数据位,停止位”...");
                }
                try
                {
                    if (int.Parse(tmp[0]) != spCom.BaudRate)
                    {
                        if (spCom.IsOpen)
                        {
                            throw new Exception("端口打开时不允许修改波特率，请先关闭端口，再设置波特率...");
                        }

                        spCom.BaudRate = int.Parse(tmp[0]);
                    }
                }
                catch
                {
                    throw new Exception("通信参数中，波特率应该是一个整数”...");
                }

                Parity parity = tmp[1].ToLower() == "e" ? Parity.Even : tmp[1].ToLower() == "o" ? Parity.Odd : Parity.None;

                if (parity != spCom.Parity)
                {
                    if (spCom.IsOpen)
                    {
                        throw new Exception("端口打开时不允许修改校验位，请先关闭端口，再设置校验位...");
                    }

                    spCom.Parity = parity;
                }
                try
                {
                    if (int.Parse(tmp[2]) != spCom.DataBits)
                    {
                        if (spCom.IsOpen)
                        {
                            throw new Exception("端口打开时不允许修改数据位，请先关闭端口，再设置数据位...");
                        }

                        spCom.DataBits = int.Parse(tmp[2]);
                    }
                }
                catch
                {
                    throw new Exception("通信参数中，数据位应该是一个0-8之间的整数...");
                }

                StopBits sbit = tmp[3] == "2" ? StopBits.Two : tmp[3] == "1.5" ? StopBits.OnePointFive : StopBits.One;

                if (sbit != spCom.StopBits)
                {
                    if (spCom.IsOpen)
                    {
                        throw new Exception("端口打开时不允许修改停止位，请先关闭端口，再设置停止位...");
                    }
                    spCom.StopBits = sbit;
                }

            }
        }

        /// <summary>
        /// 获取波特率
        /// </summary>
        public int BaudRate
        {
            get { return spCom.BaudRate; }
        }
        /// <summary>
        /// 获取数据位
        /// </summary>
        public int DataBits
        {
            get { return spCom.DataBits; }
        }
        /// <summary>
        /// 获取停止位
        /// </summary>
        public StopBits StopBit
        {
            get { return spCom.StopBits; }
        }
        /// <summary>
        /// 获取校验位
        /// </summary>
        public Parity CheckBits
        {
            get { return spCom.Parity; }
        }

        /// <summary>
        /// 设置和获取端口号(COM1,COM2...)
        /// </summary>
        public string PortNum
        {
            get { return spCom.PortName; }
            set
            {
                try
                {
                    if (spCom.PortName.ToLower() == value.ToLower()) return;
                    if (spCom.IsOpen)
                    {
                        throw new Exception("端口打开时不允许修改端口号，请先关闭端口，再设置端口号...");
                    }
                    spCom.PortName = value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion

        #region ----------------------构造函数-----------------
        /// <summary>
        /// 构造函数
        /// </summary>
        public COM32()
        {
            spCom = new SerialPort();
            spCom.WriteTimeout = 1000;
        }

        /// <summary>
        /// 串口构造函数
        /// </summary>
        /// <param name="settings">通信参数（1200,n,8,1）</param>
        /// <param name="ComNum">端口号(COM1,COM2...)</param>
        public COM32(string settings, string ComNum)
        {
            try
            {
                spCom = new SerialPort(ComNum);
                Settings = settings;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region ------------------------外部公开函数------------------------
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public bool OpenPort()
        {
            try
            {
                if (spCom.IsOpen) return true;
                spCom.DtrEnable = true;
                spCom.Open();
                DiscardReadBuff();
                DiscardSendQueue();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="settings">通信参数（1200,n,8,1）</param>
        /// <param name="comnum">端口号(COM1,COM2...)</param>
        /// <returns></returns>
        public bool OpenPort(string settings, string comnum)
        {
            Settings = settings;
            PortNum = comnum;

            return OpenPort();
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClosePort()
        {
            try
            {
                if (threadSendData != null)
                {
                    threadSendData.Abort();     //先停止发送线程
                    threadSendData = null;
                }

                StopListening();   //停止监听（在异步是有效，同步时该方法没有实质作用）

                DiscardSendQueue();   //丢弃发送缓冲区数据

                lock (ThreadSendObject)
                {
                    spCom.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 清除读取的帧队列
        /// </summary>
        public void DiscardReadBuff()
        {
            lock (ThreadreadQueueObj)
            {
                readQueueBuff.Clear();
            }
        }
        /// <summary>
        /// 读取缓冲队列中的帧数据（一般情况在全双工模式下使用，异步监听下无效）
        /// </summary>
        /// <returns></returns>
        public byte[] Read()
        {
            if (readQueueBuff.Count > 0)
            {
                lock (ThreadreadQueueObj)
                {
                    return readQueueBuff.Dequeue();
                }
            }
            else
            {
                return new byte[0];
            }
        }
        /// <summary>
        /// 串口监听开启（全双工模式）
        /// </summary>
        /// <returns></returns>
        public bool Listening()
        {
            if (!spCom.IsOpen) return false;
            stopReceive = false;

            spCom.DiscardInBuffer();
            spCom.DiscardOutBuffer();
            readQueueBuff.Clear();
            Action<object> method = delegate(object obj)
            {
                fullDuplexReadReceive();
            };


            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(method), new object());

            return true;
        }
        /// <summary>
        /// 串口异步回调监听（全双工模式）
        /// </summary>
        /// <param name="AscallBack"></param>
        /// <returns></returns>
        public bool BeginListening(AsyncSerailCallback AscallBack)
        {
            if (!spCom.IsOpen) return false;
            stopReceive = false;
            spCom.DiscardInBuffer();
            spCom.DiscardOutBuffer();

            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(fullDuplexReadReceive), (object)AscallBack);

            return true;
        }

        /// <summary>
        /// 停止监听（全双工模式）
        /// </summary>
        public void StopListening()
        {
            stopReceive = true;

            mre.WaitOne();
        }

        /// <summary>
        /// 异步发送数据（全双工模式）
        /// </summary>
        /// <param name="data"></param>
        public void BeginSendData(byte[] data)
        {
            if (!spCom.IsOpen) throw new Exception("串口打未打开...");
            lock (ThreadSendQueueObj)
            {
                sendQueueBuff.Enqueue(data);
            }

            if (threadSendData != null) return;

            threadSendData = new System.Threading.Thread((System.Threading.ThreadStart)snycsendData);

            threadSendData.Start();


        }
        /// <summary>
        /// 终止异步数据发送（全双工模式）
        /// </summary>
        public void AbortSendData()
        {
            if (threadSendData == null) return;
#if WINCE
            try
            {
                threadSendData.Abort();
            }
            catch{}
#else

            if (threadSendData.ThreadState != System.Threading.ThreadState.Aborted)
            {
                threadSendData.Abort();
            }
#endif
            threadSendData = null;

            DiscardSendQueue();
        }

        /// <summary>
        /// 清除发送队列
        /// </summary>
        public void DiscardSendQueue()
        {
            lock (ThreadSendQueueObj)
            {
                sendQueueBuff.Clear();
            }
        }
        /// <summary>
        /// 发送数据\接收数据（半双工）
        /// 调用该函数将会自动终止全双工的监听和发送，并清理缓冲数据
        /// </summary>
        /// <param name="vData">需要发送的数据帧（在有返回的时候，将会返回数据帧）</param>
        /// <param name="isReturn">是否需要返回</param>
        /// <returns></returns>
        public bool SendData(ref byte[] vData, bool isReturn)
        {
            if (!spCom.IsOpen) return false;

            StopListening();

            AbortSendData();

            lock (ThreadreadQueueObj)
            {
                readQueueBuff.Clear();
            }

            spCom.DiscardInBuffer(); spCom.DiscardOutBuffer();

            try
            {
                spCom.Write(vData, 0, vData.Length);            //发送数据

            }
            catch (TimeoutException ex)
            {
                int sfds = 2432;
            }
            if (!isReturn) return true;     //如果不需要返回，就直接返回真

            return halfDuplexReadReceive(out vData);  //返回接收结果

        }
        /// <summary>
        /// 发送数据\接收数据
        /// 调用该函数将会自动终止全双工的监听和发送，并清理缓冲数据
        /// </summary>
        /// <param name="vData">需要发送的数据帧（在有返回的时候，将会返回数据帧）</param>
        /// <returns></returns>
        public bool SendData(ref byte[] vData)
        {
            return SendData(ref vData, true);
        }

        /// <summary>
        /// 发送数据\接收数据
        /// 调用该函数将会自动终止全双工的监听和发送，并清理缓冲数据
        /// </summary>
        /// <param name="vData">需要发送的数据帧（在有返回的时候，将会返回数据帧）</param>
        /// <param name="Analysis">自定义拆帧方式类</param>
        /// <returns></returns>
        public bool SendData(ref byte[] vData, IFrameAnalysis Analysis)
        {
            if (Analysis != null) frameAnalysis = Analysis;
            return SendData(ref vData, true);
        }

        #endregion

        #region ---------------------私有函数-----------------
        /// <summary>
        /// 异步串口数据发送（线程使用）
        /// </summary>
        private void snycsendData()
        {
            if (!spCom.IsOpen) return;
            while (true)
            {
                if (sendQueueBuff.Count > 0)
                {
                    byte[] buff;

                    lock (ThreadSendQueueObj)
                    {
                        buff = sendQueueBuff.Dequeue();
                    }

                    if (isHalfDuplex)        //如果是半双工模式工作
                    {
                        while (spCom.BytesToRead > 0)
                        {
                            System.Threading.Thread.Sleep(2);
                        }
                    }

                    lock (ThreadSendObject)
                    {
                        spCom.Write(buff, 0, buff.Length);
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }


        /// <summary>
        /// 全双工读取缓冲区数据
        /// </summary>
        private void fullDuplexReadReceive()
        {
            fullDuplexReadReceive(null);
        }
        /// <summary>
        /// 全双工读取缓冲区数据
        /// <param name="asc">异步回调函数指针</param>
        /// </summary>
        private void fullDuplexReadReceive(object asc)
        {
            AsyncSerailCallback callback = null;
            if (asc != null)
            {
                callback = (AsyncSerailCallback)asc;
            }
            if (!spCom.IsOpen) return;



            List<byte> tmpList = new List<byte>();

            while (!stopReceive)
            {
                mre.Reset();            //线程标志

                byte[] tmpbuff;

                DateTime dt1 = DateTime.Now;

                bool isRead = false;

                //lock (ThreadObject)         //串口互斥（防止多线程操作的时候，该接收线程还在运行，其他线程关闭串口的情况发生）
                //{
                while (TimeSub(DateTime.Now, dt1) < (double)byteTimeOut)            //字节间最大等待超时时间
                {
                    if (stopReceive) break;             //如果停止了就直接退出

                    if (isHalfDuplex)//如果是开启了半双工，一问一答
                    {
                        while (spCom.BytesToWrite > 0)
                        {
                            System.Threading.Thread.Sleep(2);
                        }
                    }

                    if (spCom.BytesToRead > 0)
                    {
                        isRead = false;

                        tmpbuff = new byte[spCom.BytesToRead];          //创建一个长度大小为缓冲区等待接收数据长度的数组

                        spCom.Read(tmpbuff, 0, tmpbuff.Length);         //读取Buff

                        tmpList.AddRange(tmpbuff);      //将读出来的数据放到一个字节列表中，方便操作和转换追加

                        if (frameAnalysis != null)
                        {
                            int vstart, vlen;

                            if (frameAnalysis.FrameAnalysis(tmpList.ToArray(), out vstart, out vlen))
                            {
                                mre.Set();   //线程标志可被终止

                                try
                                {
                                    if (callback != null)
                                    {
                                        if (!isHalfDuplex) //如果是全双工方式，则使用新线程去调用回调函数
                                        {
                                            Action<object> threadmethod = delegate(object obj)
                                            {
                                                byte[] bytarr = (byte[])obj;
                                                callback(bytarr);
                                            };
                                            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(threadmethod), tmpList.GetRange(vstart, vlen).ToArray());
                                        }
                                        else
                                        {
                                            callback(tmpList.GetRange(vstart, vlen).ToArray());
                                        }
                                    }
                                    else
                                    {
                                        lock (ThreadreadQueueObj)
                                        {

                                            readQueueBuff.Enqueue(tmpList.GetRange(vstart, vlen).ToArray());            //在返回队列中插入一帧
                                        }
                                    }
                                }
                                catch
                                {
                                    throw new Exception("拆帧接口长度参数返回下标越界...");
                                }
                                tmpList.RemoveRange(9, vstart + vlen);
                                isRead = true;
                            }
                        }
                        dt1 = DateTime.Now;         //重置计时器
                    }
                }

                mre.Set(); //线程标志可被终止 

                if (tmpList.Count > 0 && !isRead)
                {
                    if (callback != null)
                    {
                        if (!isHalfDuplex)          //如果是全双工方式，则使用新线程去调用回调函数
                        {
                            Action<object> threadmethod = delegate(object obj)
                            {
                                byte[] bytarr = (byte[])obj;
                                callback(bytarr);
                            };
                            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(threadmethod), tmpList.ToArray());
                        }
                        else
                        {
                            callback(tmpList.ToArray());
                        }
                    }
                    else
                    {
                        lock (ThreadreadQueueObj)
                        {
                            readQueueBuff.Enqueue(tmpList.ToArray());
                        }
                    }
                    tmpList.Clear();
                }
                //}
            }



        }



        /// <summary>
        /// 读取串口缓冲区数据(半双工模式)
        /// </summary>
        /// <param name="buff">返回的字节数组</param>
        /// <returns></returns>
        private bool halfDuplexReadReceive(out byte[] buff)
        {
            buff = new byte[0];

            if (!spCom.IsOpen) return false;

            DateTime dt1 = DateTime.Now;

            bool isInBuff = false;          //等待帧超时标志变量

            while (TimeSub(DateTime.Now, dt1) < (double)spCom.ReadTimeout)      //帧间最大等待超时时间
            {

                if (spCom.BytesToRead > 0)          //如果缓冲区有等待接收数据，则退出超时器，进入下一个流程
                {
                    isInBuff = true;            //将标志变量变为真
                    break;
                }
            }

            if (!isInBuff) return false;            //如果标志变量为假，则表示帧等待超时，退出

            List<byte> tmpList = new List<byte>();

            byte[] tmpbuff;

            dt1 = DateTime.Now;

            while (TimeSub(DateTime.Now, dt1) < (double)byteTimeOut)            //字节间最大等待超时时间
            {
                if (spCom.BytesToRead > 0)
                {
                    tmpbuff = new byte[spCom.BytesToRead];          //创建一个长度大小为缓冲区等待接收数据长度的数组

                    spCom.Read(tmpbuff, 0, tmpbuff.Length);         //读取Buff

                    tmpList.AddRange(tmpbuff);      //将读出来的数据放到一个字节列表中，方便操作和转换追加

                    if (frameAnalysis != null)
                    {
                        int vstart, vlen;

                        if (frameAnalysis.FrameAnalysis(tmpList.ToArray(), out vstart, out vlen))
                        {
                            try
                            {
                                buff = tmpList.GetRange(vstart, vlen).ToArray();            //返回一帧数据
                            }
                            catch
                            {
                                throw new Exception("拆帧接口长度参数返回下标越界...");
                            }
                            return true;
                        }
                    }
                    dt1 = DateTime.Now;         //重置计时器
                }
            }
            if (tmpList.Count > 0)
            {
                buff = tmpList.ToArray();
                return true;
            }
            return false;

        }


        /// <summary>
        /// 时间差计算（用于串口通信延时使用）
        /// </summary>
        /// <param name="Time1">参数计算的时间1（被减数）</param>
        /// <param name="Time2">参与尖酸的时间2（减数）</param>
        /// <returns></returns>
        private double TimeSub(DateTime Time1, DateTime Time2)
        {
            TimeSpan tsSub = Time1.Subtract(Time2);

            return tsSub.TotalMilliseconds;
        }


        #endregion

        #region ---------------------静态公用函数--------------

        /// <summary>
        /// 获取可用串口列表
        /// </summary>
        /// <returns></returns>
        public static System.Collections.IEnumerable ComList()
        {
            RegistryKey rKey = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");

            if (rKey != null)
            {
                string[] sSubKeys = rKey.GetValueNames();
                foreach (string s in sSubKeys)
                {
                    yield return rKey.GetValue(s).ToString();
                }
            }

        }
        /// <summary>
        /// 根据驱动名称获取串口号
        /// </summary>
        /// <param name="DriverName">驱动名称</param>
        /// <returns></returns>
        public static string PortName(string DriverName)
        {
            RegistryKey rKey = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (rKey != null)
            {
                string[] sSubKeys = rKey.GetValueNames();
                foreach (string sName in sSubKeys)
                {
                    if (sName.ToLower().IndexOf(DriverName.ToLower()) >= 0)
                    {
                        return rKey.GetValue(sName).ToString();
                    }
                }
            }
            return "";
        }


        #endregion

        #region ---------------IDisposable 成员-------------

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            try
            {
                spCom.Close();
                spCom.Dispose();
                spCom = null;
            }
            catch { }
        }

        #endregion

        #region ------------------接口定义-----------------
        /// <summary>
        /// 帧解析接口
        /// </summary>
        public interface IFrameAnalysis
        {

            /// <summary>
            /// 解析帧长度
            /// </summary>
            /// <param name="vData">需要做解析的数据帧</param>
            /// <param name="start">帧开始位置</param>
            /// <param name="Len">帧长度</param>
            /// <returns>获取一帧完整帧返回帧，反之返回假</returns>
            bool FrameAnalysis(byte[] vData, out int start, out int Len);

        }


        #endregion
    }
}

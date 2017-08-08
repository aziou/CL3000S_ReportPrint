using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CLDC_DataCore.Const;
using System.Threading;

namespace CLDC_DataCore.SocketModule.Sock
{

    /// <summary>
    /// 与台体的通讯连接
    /// </summary>
    internal class Connection
    {
        private object objSendLock = new object();
        private object objPackLock = new object();

        /// <summary>
        /// 连接对象
        /// </summary>
        IConnection connection = null;

        /// <summary>
        /// 初始化为UDP连接，并打开连接
        /// </summary>
        /// <param name="remoteIp">远程服务器IP</param>
        /// <param name="remotePort">远程服务器端口</param>
        /// <param name="localPort">本地监听端口</param>
        /// <param name="intBasePort">本地监听端口</param>
        /// <param name="MaxWaitMSecond">指示最大等待时间</param>
        /// <param name="WaitSecondsPerByte">单字节最大等等时间</param>
        public Connection(IPAddress remoteIp, int remotePort, int localPort, int intBasePort, int MaxWaitMSecond, int WaitSecondsPerByte)
        {
            connection = new UDPClient(remoteIp.ToString().Split(':')[0], localPort, remotePort, intBasePort) as IConnection;
            connection.MaxWaitSeconds = MaxWaitMSecond;
            connection.WaitSecondsPerByte = WaitSecondsPerByte;
        }

        /// <summary>
        /// 初始化为COM连接，并打开连接
        /// </summary>
        /// <param name="commPort">COM端口</param>
        /// <param name="szBtl">波特率字符串，如：1200,e,8,1</param>
        /// <param name="WaitSecondsPerByte">单字节最大等等时间</param>
        /// <param name="MaxWaitMSecond">指示最大等待时间</param>
        public Connection(int commPort, string szBtl, int MaxWaitMSecond, int WaitSecondsPerByte)
        {
            connection = new COM32(szBtl, commPort);
            connection.MaxWaitSeconds = MaxWaitMSecond;
            connection.WaitSecondsPerByte = WaitSecondsPerByte;
        }

        /// <summary>
        /// 更新端口对应的COMM口波特率参数
        /// </summary>
        /// <param name="szBlt">要更新的波特率</param>
        /// <returns>更新是否成功</returns>
        public bool UpdatePortSetting(string szBlt)
        {
            if (connection != null && GlobalUnit.IsDemo != true) connection.UpdateBltSetting(szBlt);
            return true;
        }

        /// <summary>
        /// 发送并且接收返回数据
        /// </summary>
        /// <param name="sendPack">发送数据包</param>
        /// <param name="recvPack">接收数据包</param>
        /// <param name="PortName">仅用于报文日志，无实际端口作用</param>
        /// <returns></returns>
        public bool Send(Packet.SendPacket sendPack, Packet.RecvPacket recvPack, string PortName)
        {
            
            Monitor.Enter(objPackLock);
            //fjk
            CLDC_DataCore.Model.LogModel.LogFrameInfo Lf = new CLDC_DataCore.Model.LogModel.LogFrameInfo();
            Lf.strPortNo = PortName;
            Lf.strEquipName = sendPack.ToString();
            Lf.strItemName = "";
            Lf.strMessage = sendPack.GetPacketName();

            byte[] vData = sendPack.GetPacketData();

            Lf.sendFrm = new CLDC_DataCore.Struct.StSRFrame();
            Lf.sendFrm.Frame = vData;
            Lf.sendFrm.FrameMeaning = sendPack.GetPacketResolving();
            Lf.sendFrm.FrameTime = DateTime.Now;


            //

            //接到的数据存放在recvPack中，通过检测

            if (vData == null)
            {
                Lf.ResvFrm.Frame = null;
                Lf.ResvFrm.FrameMeaning = "发送数据包为null";
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return false;
            }

            {
                //TODO:控制,带协议2018
            }

            Console.WriteLine("SendData:({0}) {1}", sendPack.GetPacketName(), BitConverter.ToString(vData, 0));
            if (!Send(ref vData, sendPack.IsNeedReturn, sendPack.WaiteTime()))
            {

                Lf.ResvFrm.Frame = vData;
                Lf.ResvFrm.FrameMeaning = "发送失败";
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return false;
            }
            //if (!Send(ref vData, sendPack.IsNeedReturn, 1))
            //{

            //    Lf.ResvFrm.Frame = vData;
            //    Lf.ResvFrm.FrameMeaning = "发送失败";
            //    Lf.ResvFrm.FrameTime = DateTime.Now;

            //    CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
            //    Monitor.Exit(objPackLock);
            //    return false;
            //}
            if (sendPack.IsNeedReturn == false)//  && vData.Length < 1
            {
                Console.WriteLine("不需要回复数据");
                
                Lf.ResvFrm.Frame = vData;
                Lf.ResvFrm.FrameMeaning = "不需要回复数据";
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return true;
            }

            if (sendPack.IsNeedReturn == true && (vData == null || vData.Length < 1))
            {
                Console.WriteLine("无回复数据");

                Lf.ResvFrm.Frame = vData;
                Lf.ResvFrm.FrameMeaning = "无回复数据";
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return false;
            }
            if (sendPack.IsNeedReturn == true && recvPack == null)
            {
                Console.WriteLine("没有传入解析包");
                
                Lf.ResvFrm.Frame = vData;
                Lf.ResvFrm.FrameMeaning = "没有传入解析包";
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return false;
            }
            if (sendPack.IsNeedReturn == true && recvPack != null)
            {
                Console.WriteLine("收到数据{0}", BitConverter.ToString(vData, 0));
                if (false)
                {
                    //TODO:2018带协议 ，解包
                }
                bool ret = recvPack.ParsePacket(vData);
                
                Lf.ResvFrm.Frame = vData;
                Lf.ResvFrm.FrameMeaning = recvPack.GetPacketResolving();
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return ret;
            }
            else
            {
                Console.WriteLine("其他发送接收错误");
                
                Lf.ResvFrm.Frame = vData;
                Lf.ResvFrm.FrameMeaning = "其他发送接收错误";
                Lf.ResvFrm.FrameTime = DateTime.Now;

                CLDC_DataCore.Const.GlobalUnit.FrameLog.WriteFrameLog(Lf);
                Monitor.Exit(objPackLock);
                return false;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="vData"></param>
        /// <param name="isNeedReturn"></param>
        /// <param name="WaiteTime"></param>
        /// <returns></returns>
        private bool Send(ref byte[] vData, bool isNeedReturn, int WaiteTime)
        {
            if (connection == null) return false;
            lock (objSendLock)
            {
                if (connection != null)
                {
                    if (!connection.SendData(ref vData, isNeedReturn, WaiteTime))
                        return false;
                }
                if (isNeedReturn && vData.Length < 1)
                {
                    return false;
                }
                return true;
            }
        }
        public bool Close()
        {
            if (connection == null) return true;
            return connection.Close();
        }
    }
}

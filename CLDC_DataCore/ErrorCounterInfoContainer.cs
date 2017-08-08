using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CLDC_DataCore.Command.ErrorCounters;

namespace CLDC_DataCore
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ErrorCounterInfoHandler(object sender, List<ErrorCounterInfo> e);

    /// <summary>
    /// 误差板信息处理容器
    /// 其内部使用后台线程进行处理，
    /// 处理时，使用事件回调，
    /// </summary>
    public class ErrorCounterInfoContainer
    {
        private AutoResetEvent wakeUpEvent = new AutoResetEvent(false);
        private Thread workThread;
        Queue<List<ErrorCounterInfo>> errorCounterInfos = new Queue<List<ErrorCounterInfo>>();
        bool isStop = false;

        /// <summary>
        /// 
        /// </summary>
        public event ErrorCounterInfoHandler ErrorCounterInfoArriving;

        /// <summary>
        /// 
        /// </summary>
        public ErrorCounterInfoContainer()
        {
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this.workThread = new Thread(new ThreadStart(DoWork));
            this.workThread.IsBackground = true;
            this.workThread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            isStop = true;
            this.wakeUpEvent.Set();
        }

        private void DoWork()
        {
            while (isStop == false)
            {
                this.wakeUpEvent.WaitOne();
                //如果停止，则退出
                if (isStop == true)
                    break;
                //处理所有的数据
                while (this.errorCounterInfos.Count > 0)
                {
                    List<ErrorCounterInfo> item = null;

                    lock (this.errorCounterInfos)
                    {
                        item = this.errorCounterInfos.Dequeue();
                    }
                    if (this.ErrorCounterInfoArriving != null)
                    {
                        try
                        {
                            this.ErrorCounterInfoArriving(this, item);
                        }
                        catch (Exception ex)
                        {
                            CLDC_DataCore.Const.GlobalUnit.Logger.Error("ErrorCounterInfoContainer:ErrorCounterInfoArriving", ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 向容器中加消息
        /// </summary>
        /// <param name="infos"></param>
        public void Add(List<ErrorCounterInfo> infos)
        {
            if (isStop == true)
                return;

            lock (this.errorCounterInfos)
            {
                this.errorCounterInfos.Enqueue(infos);
            }
            //wake up work thread to process data
            this.wakeUpEvent.Set();
        }
    }
}

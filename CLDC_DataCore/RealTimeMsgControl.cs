// ***************************************************************
//  VerifyMsgControl   date: 09/14/2009
//  -------------------------------------------------------------
//  Description:
//  消息队列
//  -------------------------------------------------------------
//  Copyright (C) 2009 -CdClou All Rights Reserved
// ***************************************************************
// Modify Log:
// 09/14/2009 10-56-01    Created
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CLDC_Comm.MessageArgs;
using CLDC_Comm.Enum;

namespace CLDC_DataCore
{
    /// <summary>
    /// 实时数据消息管理器。
    /// 说明：
    ///      负责消息的添加，清空管理。提供线程处理入口及消息事件委托。 系统在运行过程中采用消息驱动机制更新数据。刷新UI。
    /// </summary>
    /// <example>以下示例展示如何使用消息队列
    /// <code>
    /// //实例化一个消息队列
    /// VerifyMsgControl myMsg= new VerifyMsgControl();
    /// //消息队列类型为:消息队列
    /// myMsg.IsMsg=true;
    /// //消息线程轮循时间为20ms
    /// myMsg.SleepTime=20;
    /// //指定消息到达时处理方法
    /// myMsg.ShowMsg+=new OnShowMsg(myShowMsg);
    /// //实例化一个消息线程
    /// Thread myMsgThread= new Thread(myMsg.DoWork);
    /// myMsgThread.Start();
    /// 
    /// ///消息处理函数
    /// private void myShowMsg(object sender, object E)
    /// {
    ///     Comm.MessageArgs.EventMessageArgs _Message 
    ///         = E as Comm.MessageArgs.EventMessageArgs;
    ///     /*
    ///         消息处理过程
    ///     */
    /// }
    /// </code>
    /// </example>
    public class RealTimeMsgControl
    {
        #region ----------公共成员----------
        /// <summary>
        /// 消息事件委托，
        /// </summary>
        /// <param name="sender">发生者</param>
        /// <param name="E">消息参数</param>
        public delegate void OnUpdateRealTimeMsg(object sender, object E);
        /// <summary>
        /// 消息委托，当有消息到达时触发
        /// </summary>
        public OnUpdateRealTimeMsg UpdateRealTimeMsg;

        /// <summary>
        /// 消息轮询间隔，此越小处理消息速度越快
        /// </summary>
        public int SleepTime = 10;

        
        /// <summary>
        /// 队列最大成员数量。多余部分刚删除掉
        /// </summary>
        public int MaxItem = 50;
        #endregion

        #region ----------私有成员----------
        /// <summary>
        /// 队列对象
        /// </summary>
        private Queue<CLDC_DataCore.Struct.StRealTimeMsg> lstMsg = new Queue<CLDC_DataCore.Struct.StRealTimeMsg>();
        /// <summary>
        /// 线程读取锁
        /// </summary>
        private object objLock = new object();
        /// <summary>
        /// 线程写锁
        /// </summary>
        private object objAddLock = new object();
        #endregion


        /// <summary>
        /// 清除当前所有没处理的消息
        /// </summary>
        public void ClearCache()
        {
            lstMsg.Clear();
        }

        /// <summary>
        /// 取当前消息队列中的消息数量
        /// </summary>
        public int Count
        {
            get
            {
                return lstMsg.Count;
            }
        }

        #region ---------消息队列添加---------

        /// <summary>
        /// 添加消息/数据到队列中
        /// </summary>
        /// <param name="sender">消息发出者</param>
        /// <param name="e">消息参数</param>
        public void AddMsg(object sender, object e)
        {
            //if (GlobalUnit.g_CUS.DnbData.CheckState == Cus_CheckStaute.停止检定)
            //    return;
            try
            {
                CLDC_DataCore.Struct.StRealTimeMsg _Msg = new CLDC_DataCore.Struct.StRealTimeMsg();
                _Msg.objSender = sender;
                //移除已经过期的消息
                while (lstMsg.Count > MaxItem)
                {
                    CLDC_DataCore.Struct.StRealTimeMsg m = lstMsg.Dequeue();                    
                }
                
                _Msg.cmdData = (CLDC_CTNProtocol.CTNPCommand)e;
                
                lstMsg.Enqueue(_Msg);

            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    string logPath = "/Log/Thread/MsgThread-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                    CLDC_DataCore.Const.GlobalUnit.Log.WriteLog(logPath, this, "AddMsg", ex.Message + "\r\n" + ex.StackTrace);
                }
#if DEBUG
                throw ex;
#endif
            }

        }

        #endregion

        #region----------消息/数据队列处理-DoWork()---------
        /// <summary>
        /// 消息处理线程，确保只有一个线程调用
        /// </summary>
        
        public void DoWork()
        {
            if (CLDC_DataCore.Const.GlobalUnit.IsDemo) return;
            while (true)
            {
                if (CLDC_DataCore.Const.GlobalUnit.ApplicationIsOver)
                {
                    break;
                }
                if (lstMsg.Count > 0)
                {
                    try
                    {
                        CLDC_DataCore.Struct.StRealTimeMsg _Msg = lstMsg.Dequeue();

                        if (UpdateRealTimeMsg != null)
                        {                            
                            //数据队列处理
                            UpdateRealTimeMsg(_Msg.objSender, _Msg.cmdData);                            
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        e.ToString();
                        //消息队列为空时的意外处理.
                    }
                    catch (Exception ex)
                    {
                        string logPath = "/Log/Thread/MsgThread-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                        CLDC_DataCore.Const.GlobalUnit.Log.WriteLog(logPath, this, "DoWork", ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
                Thread.Sleep(SleepTime);
            }
           
            string logThreadPath = "/Log/Thread/MsgThreadInfo.log";
            CLDC_DataCore.Const.GlobalUnit.Log.WriteLog(logThreadPath, this, "DoWork", Thread.CurrentThread.Name + "退出");

        }
        #endregion

        #region----------消息泵----------
        
        

        /// <summary>
        /// 上报实时检定数据
        /// </summary>        
        /// <param name="StrKey">当前项目的键值</param>        
        /// <param name="dataType">数据类型</param>        
        public void OutUpdateRealTimeData(string StrKey, Cus_MeterDataType dataType, bool bStartFlag)
        {
            //TODO:先本地临时库数据存储
            /*上报数据提取消息，不带检定数据，带了也不用处理，直接回答收到消息*/
            CLDC_DataCore.Command.Update.UpdateRealTimeData_Ask
                Cmd_UpdateRealTimeData = new CLDC_DataCore.Command.Update.UpdateRealTimeData_Ask();

            Cmd_UpdateRealTimeData.bStartFlag = bStartFlag;
            Cmd_UpdateRealTimeData.strItemKey = StrKey;            
            Cmd_UpdateRealTimeData.DataType = dataType;
            //添加到消息队列
            AddMsg(this, Cmd_UpdateRealTimeData);            
            /*上报数据完毕*/
        }

        /// <summary>
        /// 上报实时检定数据
        /// </summary>        
        /// <param name="StrKey">当前项目的键值</param>        
        /// <param name="dataType">数据类型</param>        
        public void OutUpdateRealTimeData(string StrKey, Cus_MeterDataType dataType)
        {
            OutUpdateRealTimeData(StrKey, dataType, true);
        }
        
        #endregion
    }
}

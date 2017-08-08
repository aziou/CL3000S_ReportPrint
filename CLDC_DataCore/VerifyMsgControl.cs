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
    /// 消息管理器。
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
    public class VerifyMsgControl
    {
        #region ----------公共成员----------
        /// <summary>
        /// 消息事件委托，
        /// </summary>
        /// <param name="sender">发生者</param>
        /// <param name="E">消息参数</param>
        public delegate void OnShowMsg(object sender, object E);
        /// <summary>
        /// 消息委托，当有消息到达时触发
        /// </summary>
        public OnShowMsg ShowMsg;

        /// <summary>
        /// 消息轮询间隔，此越小处理消息速度越快
        /// </summary>
        public int SleepTime = 5;

        /// <summary>
        /// 是否是消息队列，为TRUE时为消息队列，为FALSE时为数据队列
        /// </summary>
        public bool IsMsg = true;

        /// <summary>
        /// 队列最大成员数量。多余部分刚删除掉
        /// </summary>
        public int MaxItem = 50;
        #endregion

        #region ----------私有成员----------
        /// <summary>
        /// 队列对象
        /// </summary>
        private Queue<CLDC_DataCore.Struct.StVerifyMsg> lstMsg = new Queue<CLDC_DataCore.Struct.StVerifyMsg>();
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
                CLDC_DataCore.Struct.StVerifyMsg _Msg = new CLDC_DataCore.Struct.StVerifyMsg();
                _Msg.objSender = sender;
                //移除已经过期的消息
                while (lstMsg.Count > MaxItem)
                {
                    CLDC_DataCore.Struct.StVerifyMsg m = lstMsg.Dequeue();
                    //Console.WriteLine("move one message");
                }
                if (IsMsg)
                {
                    _Msg.objEventArgs = (EventArgs)e;
                    //进度消息不重复添加
                    if (e is CLDC_Comm.MessageArgs.EventProcessArgs)
                    {
                        ClearCache();
                    }
                    else if (e is CLDC_Comm.MessageArgs.EventMessageArgs)
                    {
                        //清空队列
                        if (((CLDC_Comm.MessageArgs.EventMessageArgs)e).MessageType == Cus_MessageType.清空消息队列)
                        {
                            ClearCache();
                            return;
                        }
                        else if (((CLDC_Comm.MessageArgs.EventMessageArgs)e).MessageType == Cus_MessageType.提示消息)
                        {
                            //线程消息过虑
                            if (((CLDC_Comm.MessageArgs.EventMessageArgs)e).Message.IndexOf("线程") != -1 ||
                                ((CLDC_Comm.MessageArgs.EventMessageArgs)e).Message.IndexOf("Thread was") != -1)
                            {
                                return;
                            }
                        }
                    }

                }
                else
                {
                    _Msg.cmdData = (CLDC_CTNProtocol.CTNPCommand)e;
                }
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
                        CLDC_DataCore.Struct.StVerifyMsg _Msg = lstMsg.Dequeue();

                        if (ShowMsg != null)
                        {
                            if (IsMsg)
                            {
                                //消息队列处理
                                ShowMsg(_Msg.objSender, _Msg.objEventArgs);
                            }
                            else
                            {
                                //数据队列处理
                                ShowMsg(_Msg.objSender, _Msg.cmdData);
                            }
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
#if DEBUG
                        //throw ex;
#endif
                    }
                }
                Thread.Sleep(SleepTime);
            }
           // Console.WriteLine("消息线程已经退出");
            string logThreadPath = "/Log/Thread/MsgThreadInfo.log";
            CLDC_DataCore.Const.GlobalUnit.Log.WriteLog(logThreadPath, this, "DoWork", Thread.CurrentThread.Name + "退出");

        }
        #endregion

        #region----------消息泵----------
        /// <summary>
        /// 外发消息:只刷新数据
        /// </summary>
        public void OutMessage()
        {
            OutMessage("null");
        }

        /// <summary>
        /// 外发检定消息[默认为运行时消息，需要刷新数据]
        /// </summary>
        /// <param name="strMessage"></param>
        public void OutMessage(string strMessage)
        {
            CLDC_Comm.MessageArgs.EventMessageArgs _Message = new EventMessageArgs();
            _Message.MessageType = CLDC_Comm.Enum.Cus_MessageType.运行时消息;
            _Message.Message = strMessage;
            
            OutMessage(_Message);
        }

        /// <summary>
        /// 外发检定消息[默认为运行时消息，可设置是否需要刷新数据]
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="RefreshData"></param>
        public void OutMessage(string strMessage, bool RefreshData)
        {
            CLDC_Comm.MessageArgs.EventMessageArgs _Message = new EventMessageArgs();
            _Message.MessageType = Cus_MessageType.运行时消息;
            _Message.Message = strMessage;
            _Message.RefreshData = RefreshData;
            OutMessage(_Message);

        }

        /// <summary>
        /// 外发检定消息[可设置是否刷新数据及消息类型]
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="RefreshData"></param>
        /// <param name="eType"></param>
        public void OutMessage(string strMessage, bool RefreshData, CLDC_Comm.Enum.Cus_MessageType eType)
        {
            CLDC_Comm.MessageArgs.EventMessageArgs _Message = new EventMessageArgs();
            _Message.MessageType = eType;
            _Message.Message = strMessage;
            _Message.RefreshData = RefreshData;
            OutMessage(_Message);
        }

        /// <summary>
        /// 外发检定消息
        /// </summary>
        /// <param name="MessageType"></param>
        public void OutMessage(CLDC_Comm.Enum.Cus_MessageType MessageType)
        {
            CLDC_Comm.MessageArgs.EventMessageArgs _E = new EventMessageArgs();
            _E.MessageType = MessageType;
            _E.RefreshData = false;
            OutMessage(_E);
        }

        /// <summary>
        /// 外发检定消息
        /// </summary>
        /// <param name="e"></param>
        public void OutMessage(CLDC_Comm.MessageArgs.EventMessageArgs e)
        {
            if (IsMsg)
            {
                AddMsg(this, e);
            }
        }

        /// <summary>
        /// 上报局部检定数据
        /// </summary>
        /// <param name="BW">表位号，如果为999则为所有表</param>
        /// <param name="arrStrKey">更新的键值</param>
        /// <param name="objValue">对应的数据</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="isDelete">为True时删除掉键值为strKey的数据</param>
        public void OutUpdateData(int BW, string[] arrStrKey, object[] objValue, Cus_MeterDataType dataType, bool isDelete)
        {
            //TODO:先本地临时库数据存储
            /*上报数据提取消息，不带检定数据，带了也不用处理，直接回答收到消息*/
            CLDC_DataCore.Command.Update.UpdateData_Ask
                Cmd_UpdateData = new CLDC_DataCore.Command.Update.UpdateData_Ask();
            Cmd_UpdateData.BW = BW;
            Cmd_UpdateData.isDelete = isDelete;
            Cmd_UpdateData.strKey = arrStrKey;
            Cmd_UpdateData.objData = objValue;
            Cmd_UpdateData.DataType = dataType;
            //添加到消息队列
            AddMsg(this, Cmd_UpdateData);
            //RaiseVerifyData(Cmd_UpdateData);
            //RaiseUpdateData(this, Cmd_UpdateData);
            /*上报数据完毕*/
        }

        /// <summary>
        /// 上报局部检定数据
        /// </summary>
        /// <param name="BW">表位号，如果为999则为所有表</param>
        /// <param name="arrStrKey">更新的键值</param>
        /// <param name="objValue">对应的数据</param>
        /// <param name="tableName">数据库路径</param>
        /// <param name="isDelete">为True时删除掉键值为strKey的数据</param>
        public void OutUpdateData(int BW, string[] arrStrKey, object[] objValue, Cus_DBTableName tableName, bool isDelete)
        {
            //TODO:先本地临时库数据存储
            /*上报数据提取消息，不带检定数据，带了也不用处理，直接回答收到消息*/
            CLDC_DataCore.Command.Update.UpdateData_Ask
                Cmd_UpdateData = new CLDC_DataCore.Command.Update.UpdateData_Ask();
            Cmd_UpdateData.taiID = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_DESKNO, -1);
            //添加到消息队列
            AddMsg(this, Cmd_UpdateData);
            //RaiseVerifyData(Cmd_UpdateData);
            //RaiseUpdateData(this, Cmd_UpdateData);
            /*上报数据完毕*/
        }
        /// <summary>
        /// 更新局部数据[如果存在则删除后添加，不存在则直接添加]
        /// </summary>
        /// <param name="BW"></param>
        /// <param name="arrStrKey"></param>
        /// <param name="objValue"></param>
        /// <param name="dataType"></param>
        public void OutUpdateData(int BW, string[] arrStrKey, object[] objValue, Cus_MeterDataType dataType)
        {
            OutUpdateData(BW, arrStrKey, objValue, dataType, false);
        }
        #endregion
    }
}

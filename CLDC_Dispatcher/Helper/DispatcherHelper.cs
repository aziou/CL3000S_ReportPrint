using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Const;

namespace CLDC_Dispatcher.Helper
{
    

    //数据库设计文档6.3
    public class DispatcherHelper : IDispatcherHelper
    {
        
        #region 
        public static string CurTaskNo = "";

        private static string _EquipNo = (GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_DESKNO));
        private static string _DateFormateStr = "yyyyMMdd HH:mm:ss";
        const int CST_LoginOn = 1,
        CST_LoginOut = 1,
        CST_ProgressBar = 2,
        CST_MeterStandard = 3,
        CST_ErrorBoard = 4,
        CST_CheckMessage = 5,
        CST_PressStatus = 6,
        CST_ReverseStatus = 7,
        CST_QuarantineStatus = 8,
        CST_CurCheckID = 9,
        CST_SchemeChanged = 10,
        CST_MeterChanged = 11,
        CST_ClientVersion = 12,
        CST_CurCheckState = 13,
        CST_HighVoltage = 14;

        #endregion

        public DispatcherModel _MParm
        {
            get;
            private set;
        }

        public DispatcherHelper()
        {
            _MParm = new DispatcherModel();
        }

        #region Message
        /// 1
        /// <summary>
        /// 
        /// </summary>
        public bool LoginOn()
        {
            
            Model.DSPTCH_CUR_MESSAGE MCurMessageLogin = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessageLogin.FK_DEVICE_MADE_NO = _EquipNo;
            MCurMessageLogin.AVR_MSG_TYPE = CST_LoginOn.ToString();
            MCurMessageLogin.AVR_DATA = "1";
            MCurMessageLogin.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessageLogin.AVR_HANDLE_FLAG = "0";
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessageLogin))
            {
                BCurMessage.Add(MCurMessageLogin);
            }
            WriteRunningLogs("登录装置编号：" + _EquipNo, 0);
            return true;
        }
        /// 1
        /// <summary>
        /// 
        /// </summary>
        public bool LoginOut()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessageLogin = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessageLogin.FK_DEVICE_MADE_NO = _EquipNo;
            MCurMessageLogin.AVR_MSG_TYPE = CST_LoginOut.ToString();
            MCurMessageLogin.AVR_DATA = "0";
            MCurMessageLogin.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessageLogin.AVR_HANDLE_FLAG = "0";
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessageLogin))
            {
                BCurMessage.Add(MCurMessageLogin);
            }

            WriteRunningLogs("退出登录装置编号：" + _EquipNo, 0);
            return true;
        }
        //2
        public bool SetProgressBar()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessageLogin = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessageLogin.FK_DEVICE_MADE_NO = _EquipNo;
            MCurMessageLogin.AVR_MSG_TYPE = CST_ProgressBar.ToString();
            MCurMessageLogin.AVR_DATA = _MParm.In_ProgressBarCurSeconds + "|" + _MParm.In_ProgressBarTotalSeconds;
            MCurMessageLogin.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessageLogin.AVR_HANDLE_FLAG = "0";
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessageLogin))
            {
                BCurMessage.Add(MCurMessageLogin);
            }
            return true;
        }
        //3
        public bool SetStdMeterData()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessageLogin = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessageLogin.FK_DEVICE_MADE_NO = _EquipNo;
            MCurMessageLogin.AVR_MSG_TYPE = CST_MeterStandard.ToString();
            MCurMessageLogin.AVR_DATA = _MParm.In_StdMeterDataJoinString;
            MCurMessageLogin.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessageLogin.AVR_HANDLE_FLAG = "0";
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessageLogin))
            {
                BCurMessage.Add(MCurMessageLogin);
            }
            return true;
        }
        //4
        public bool SetErrorBoard()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessageLogin = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessageLogin.FK_DEVICE_MADE_NO = _EquipNo;
            MCurMessageLogin.AVR_MSG_TYPE = CST_ErrorBoard.ToString();
            MCurMessageLogin.AVR_DATA = _MParm.In_ErrorBoardJoinString;
            MCurMessageLogin.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessageLogin.AVR_HANDLE_FLAG = "0";
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessageLogin))
            {
                BCurMessage.Add(MCurMessageLogin);
            }
            return true;
        }
        //5
        public bool SetCheckMessage()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessageLogin = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessageLogin.FK_DEVICE_MADE_NO = _EquipNo;
            MCurMessageLogin.AVR_MSG_TYPE = CST_CheckMessage.ToString();
            MCurMessageLogin.AVR_DATA = _MParm.In_CheckMessage;
            MCurMessageLogin.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessageLogin.AVR_HANDLE_FLAG = "0";
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessageLogin))
            {
                BCurMessage.Add(MCurMessageLogin);
            }
            return true;
        }
        //6
        public bool SetPressStatus()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = string.Join("|", _MParm.In_PressStatus);
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_PressStatus.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }
            return true;
        }
        //7
        public bool SetReverseStatus()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = string.Join("|", _MParm.In_ReverseStatus);
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_ReverseStatus.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }
            return true;
        }
        //8
        public bool SetQuarantineStatus()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = string.Join("|", _MParm.In_QuarantineStatus);
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_QuarantineStatus.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }
            return true;
        }
        //9
        public bool SetCurCheckID()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = _MParm.In_CurCheckID;
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_CurCheckID.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }
            return true;
        }
        //10
        public bool SetSchemeChanged()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = "";
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_SchemeChanged.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }
            return true;
        }
        //11
        public bool SetMeterChanged()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = "";
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_MeterChanged.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }
            return true;
        }
        //12
        public bool SetClientVersion()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = _MParm.In_ClientVersion;
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_ClientVersion.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }

            WriteRunningLogs("当前客户端版本：" + _MParm.In_ClientVersion, 0);
            return true;
        }
        //13
        public bool SetCurCheckState()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = _MParm.In_CurCheckState.ToString();
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_CurCheckState.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }

            WriteRunningLogs("当前客户端检定状态：" + _MParm.In_CurCheckState, 0);
            return true;
        }
        public bool SetCurCheckStateStop()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = "2";
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_CurCheckState.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }

            WriteRunningLogs("当前客户端检定状态：2", 0);
            return true;
        }
        //14
        public bool SetHighVoltage()
        {
            Model.DSPTCH_CUR_MESSAGE MCurMessagePress = new Model.DSPTCH_CUR_MESSAGE();
            MCurMessagePress.AVR_DATA = _MParm.In_HighVoltage;
            MCurMessagePress.AVR_HANDLE_FLAG = "0";
            MCurMessagePress.AVR_MSG_TYPE = CST_HighVoltage.ToString();
            MCurMessagePress.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MCurMessagePress.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_MESSAGE BCurMessage = new BLL.DSPTCH_CUR_MESSAGE();
            if (!BCurMessage.Update(MCurMessagePress))
            {
                BCurMessage.Add(MCurMessagePress);
            }

            WriteRunningLogs("当前客户端耐压仪数据：" + _MParm.In_HighVoltage, 0);
            return true;
        }
        #endregion Message

        #region Scheme
        public bool ZoomSetDicCheckID()
        {
            if (null != _MParm.In_MDicCheck)
            {
                BLL.DSPTCH_DIC_CHECK BDicCheck = new BLL.DSPTCH_DIC_CHECK();
                foreach (string[] item in _MParm.In_MDicCheck.Values)
                {
                    Model.DSPTCH_DIC_CHECK MDicCheck = new CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK();
                    MDicCheck.AVR_CHECK_NAME = item[0];
                    MDicCheck.AVR_CHECK_NO = item[1];
                    MDicCheck.AVR_CHECK_TYPE = item[2];
                    MDicCheck.AVR_NEED_TIME = item[3];
                    MDicCheck.FK_VIEW_NO = item[4];

                    if (BDicCheck.Exists(MDicCheck.AVR_CHECK_NO) == false)
                    {
                        BDicCheck.Add(MDicCheck);
                    }
                    else
                    {
                        BDicCheck.Update(MDicCheck);
                    }
                }
                return true;
            }
            return false;
        }
        public bool SetCurScheme()
        {
            Model.DSPTCH_CUR_SCHEME McurScheme = new CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME();
            McurScheme.AVR_SCHEME_ID = _MParm.In_CurSchemeID;
            McurScheme.FK_DEVICE_MADE_NO = _EquipNo;
            BLL.DSPTCH_CUR_SCHEME BcurScheme = new CLDC_Dispatcher.BLL.DSPTCH_CUR_SCHEME();
            if (BcurScheme.Exists(_EquipNo))
            {
                BcurScheme.Update(McurScheme);
            }
            else
            { 
                BcurScheme.Add(McurScheme); 
            }
            return true;
        }
        public bool AddSchemes()
        {

            Model.DSPTCH_SCHEME MScheme = new CLDC_Dispatcher.Model.DSPTCH_SCHEME();
            MScheme.AVR_SCHEME_ID = _MParm.In_AddSchemesSchemeID;
            MScheme.AVR_CHECK_NO = _MParm.In_AddSchemesCheckIDs;
            MScheme.AVR_SCHEME_NAME = _MParm.In_AddSchemesStrName;
            BLL.DSPTCH_SCHEME BcurScheme = new CLDC_Dispatcher.BLL.DSPTCH_SCHEME();
            if (BcurScheme.Exists(_MParm.In_AddSchemesStrName,_MParm.In_AddSchemesCheckIDs))
            {
                List<Model.DSPTCH_SCHEME> lstM=BcurScheme.GetModelList("AVR_SCHEME_NAME='" + _MParm.In_AddSchemesStrName + "' and AVR_CHECK_NO='" + _MParm.In_AddSchemesCheckIDs + "'");
                if (lstM!=null && lstM.Count>0)
                {

                    _MParm.Out_SchemeID = lstM[0].AVR_SCHEME_ID;
                }
            }
            else
            {
                try
                {
                    _MParm.Out_SchemeID = BcurScheme.Add(MScheme).ToString();
                }
                catch(Exception ex) 
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                    return false;
                }
            }
            return true;
        }
        #endregion Scheme

        #region Task
        public bool GetTaskNo()
        {

            BLL.DSPTCH_TASKS Btask = new CLDC_Dispatcher.BLL.DSPTCH_TASKS();
            List<Model.DSPTCH_TASKS> lst_M = Btask.GetModelList("AVR_DEVICE_ID='" + _EquipNo + "' and AVR_END_FLAG='0' and AVR_HANDLE_FLAG='0'");

            _MParm.Out_TaskNo = "";
            if (lst_M != null && lst_M.Count > 0)
            {
                _MParm.Out_TaskNo = lst_M[0].TASK_NO;
            }
            _MParm.Out_TaskNo = _MParm.Out_TaskNo.Trim();
            CurTaskNo = _MParm.Out_TaskNo;
            return !string.IsNullOrEmpty(CurTaskNo);

        }
        public bool SetTaskFlagIdle()
        {
            Model.DSPTCH_TASKS Mtask = new CLDC_Dispatcher.Model.DSPTCH_TASKS();
            Mtask.AVR_DEVICE_ID = _EquipNo;
            Mtask.AVR_HANDLE_FLAG = "1";
            Mtask.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            
            BLL.DSPTCH_TASKS Btask = new CLDC_Dispatcher.BLL.DSPTCH_TASKS();
            if (Btask.Exists(_EquipNo))
            {
                Btask.Update(Mtask);
            }
            return true;
        }
        public bool SetTaskFlagReady()
        {
            Model.DSPTCH_TASKS Mtask = new CLDC_Dispatcher.Model.DSPTCH_TASKS();
            Mtask.AVR_DEVICE_ID = _EquipNo;
            Mtask.AVR_HANDLE_FLAG = "2";
            Mtask.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);

            BLL.DSPTCH_TASKS Btask = new CLDC_Dispatcher.BLL.DSPTCH_TASKS();
            if (Btask.Exists(_EquipNo))
            {
                Btask.Update(Mtask);
            }
            return true;
        }
        public bool SetTaskFlagChecking()
        {
            Model.DSPTCH_TASKS Mtask = new CLDC_Dispatcher.Model.DSPTCH_TASKS();
            Mtask.AVR_DEVICE_ID = _EquipNo;
            Mtask.AVR_HANDLE_FLAG = "3";
            Mtask.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);

            BLL.DSPTCH_TASKS Btask = new CLDC_Dispatcher.BLL.DSPTCH_TASKS();
            if (Btask.Exists(_EquipNo))
            {
                Btask.Update(Mtask);
            }
            return true;
        }
        public bool SetTaskFlagFinished()
        {
            Model.DSPTCH_TASKS Mtask = new CLDC_Dispatcher.Model.DSPTCH_TASKS();
            Mtask.AVR_DEVICE_ID = _EquipNo;
            Mtask.AVR_HANDLE_FLAG = "4";
            Mtask.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);

            BLL.DSPTCH_TASKS Btask = new CLDC_Dispatcher.BLL.DSPTCH_TASKS();
            if (Btask.Exists(_EquipNo))
            {
                Btask.Update(Mtask);
            }
            return true;
        }
        public bool SetTaskFlagError()
        {
            Model.DSPTCH_TASKS Mtask = new CLDC_Dispatcher.Model.DSPTCH_TASKS();
            Mtask.AVR_DEVICE_ID = _EquipNo;
            Mtask.AVR_HANDLE_FLAG = "9";
            Mtask.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);

            BLL.DSPTCH_TASKS Btask = new CLDC_Dispatcher.BLL.DSPTCH_TASKS();
            if (Btask.Exists(_EquipNo))
            {
                Btask.Update(Mtask);
            }
            return true;
        }
        #endregion Task

        #region Log
        
        public bool WriteRunningLog()
        {
            return WriteRunningLogs(_MParm.In_RunningLogStrMsg, _MParm.In_RunningLogType);
        }
        public bool WriteRunningLogA()
        {
            return WriteRunningLogs(_MParm.In_RunningLogStrMsg, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="LogType">0：信息；1：告警；2错误</param>
        private bool WriteRunningLogs(string strMsg,int LogType)
        {
            Model.DSPTCH_LOG MLog = new CLDC_Dispatcher.Model.DSPTCH_LOG();
            MLog.AVR_DEVICE_ID = _EquipNo;
            MLog.AVR_LOG = strMsg;
            MLog.AVR_TYPE = LogType.ToString();
            MLog.AVR_WRITE_TIME = DateTime.Now.ToString(_DateFormateStr);
            MLog.TASK_NO = CurTaskNo;
            MLog.AVR_SOURCE = "1";
            BLL.DSPTCH_LOG BLog = new CLDC_Dispatcher.BLL.DSPTCH_LOG();
            BLog.Add(MLog);
            return true;
        }
        #endregion
    }
}

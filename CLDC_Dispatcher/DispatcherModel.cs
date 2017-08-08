using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Dispatcher.Model;

namespace CLDC_Dispatcher
{
    public class DispatcherModel
    {
        public int In_ProgressBarCurSeconds
        {
            internal get;
            set;
        }
        public int In_ProgressBarTotalSeconds
        {
            internal get;
            set;
        }
        public string In_StdMeterDataJoinString
        {
            internal get;
            set;
        }
        public string In_ErrorBoardJoinString
        {
            internal get;
            set;
        }
        public string In_CheckMessage
        {
            internal get;
            set;
        }
        public string[] In_PressStatus
        {
            internal get;
            set;
        }
        public string[] In_ReverseStatus
        {
            internal get;
            set;
        }
        public string[] In_QuarantineStatus
        {
            internal get;
            set;
        }
        public string In_CurCheckID
        {
            internal get;
            set;
        }
        public Dictionary<string,string[]> In_MDicCheck
        {
            internal get;
            set;
        }
        public string In_CurSchemeID
        {
            internal get;
            set;
        }
        public string In_AddSchemesSchemeID
        {
            internal get;
            set;
        }
        public string In_AddSchemesCheckIDs
        {
            internal get;
            set;
        }
        public string In_AddSchemesStrName
        {
            internal get;
            set;
        }
        public string In_RunningLogStrMsg
        {
            internal get;
            set;
        }
        /// <summary>
        /// 0：信息；1：告警；2错误
        /// </summary>
        public int In_RunningLogType
        {
            internal get;
            set;
        }
        public string In_ClientVersion
        {
            internal get;
            set;
        }
        public uint In_CurCheckState
        {
            internal get;
            set;
        }
        public string In_HighVoltage
        {
            internal get;
            set;
        }

        public string Out_TaskNo
        {
            get;
            internal set;
        }
        public string Out_SchemeID
        {
            get;
            internal set;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
//using CLDC_DataCore.Model.DnbModel;
using CLDC_Comm.Enum;
using CLDC_Comm.Command;
using CLDC_CTNProtocol;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 电能表检定数据发送类,已经初始CUS_MAIN_SERVERTOCLIENT
    /// </summary>
    [Serializable()]
    public class SendMeterData:CTNPCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public SendMeterData()
        {
        }
    }

}

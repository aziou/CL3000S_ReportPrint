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
    /// ���ܱ�춨���ݷ�����,�Ѿ���ʼCUS_MAIN_SERVERTOCLIENT
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

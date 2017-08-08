using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// 主动发送一只表的数据
    /// </summary>
    [Serializable ]
    public class SendMeterData_Ask:Command_Ask 
    {
        /// <summary>
        /// 一只电表的数据
        /// </summary>
        //public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo  MeterData = null;

        public SendMeterData_Ask()
        {
            AskMessage = "一只电表的数据";
        }
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// 发送总模型
    /// </summary>
    [Serializable]
    public class SendMeterGroup_Ask : Command_Ask 
    {
        /// <summary>
        /// 总模型数据
        /// </summary>
        //public CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup = null;

        public SendMeterGroup_Ask()
        {
            AskMessage = "所有表的数据";
        }
    }
}

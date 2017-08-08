using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// 请求传输 MeterGroup数据模型
    /// </summary>
    [Serializable]
    public class GetMeterGroup_Ask : Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMeterGroup_Ask()
        {
            AskMessage = "所有表的数据";
        }
    }
}

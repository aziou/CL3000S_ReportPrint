using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// 获取一只表的数据
    /// </summary>
    [Serializable]
    public class GetMeterData_Ask : Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMeterData_Ask()
        {
            AskMessage = "获取一只表的数据";
        }
    }


}

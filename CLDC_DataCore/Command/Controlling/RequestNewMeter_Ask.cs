using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// 挂新表命令
    /// </summary>
    [Serializable]
    public class RequestNewMeter_Ask:Command_Ask
    {

        /// <summary>
        /// 
        /// </summary>
        public RequestNewMeter_Ask()
        {
            AskMessage = "挂新表";
        }

    }
}

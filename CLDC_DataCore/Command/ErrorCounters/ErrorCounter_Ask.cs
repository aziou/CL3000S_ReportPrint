using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.ErrorCounters
{
    /// <summary>
    /// 误差板信息
    /// </summary>
    [Serializable]
    public class ErrorCounter_Ask : Command_Ask
    {
        private List<ErrorCounterInfo> errorCounterInfos=new List<ErrorCounterInfo>();

        /// <summary>
        /// 误差板信息
        /// </summary>
        public List<ErrorCounterInfo> ErrorCounterInfos
        {
            get { return this.errorCounterInfos; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ErrorCounter_Ask()
        {
            AskMessage = "误差板信息";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// 报告检定状态
    /// </summary>
    [Serializable()]
    public class UpdateCheckState_Ask:Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public CLDC_Comm.Enum.Cus_CheckStaute CheckState;
        /// <summary>
        /// 
        /// </summary>
        public UpdateCheckState_Ask()
        {
            AskMessage = "检定状态变更";
        }
    }


}

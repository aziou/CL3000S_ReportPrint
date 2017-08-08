using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{
    /// <summary>
    /// 报告配置方案完毕
    /// </summary>
    [Serializable]
    public class CreatePlan_Complated_Ask:Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePlan_Complated_Ask()
        {
            AskMessage = "配置方案完毕";
        }
    }


}

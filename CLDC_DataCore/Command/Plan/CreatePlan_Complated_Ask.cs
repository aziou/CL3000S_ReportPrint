using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{
    /// <summary>
    /// �������÷������
    /// </summary>
    [Serializable]
    public class CreatePlan_Complated_Ask:Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePlan_Complated_Ask()
        {
            AskMessage = "���÷������";
        }
    }


}

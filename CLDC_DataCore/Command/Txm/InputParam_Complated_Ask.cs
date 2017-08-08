using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// 参数录入完毕
    /// </summary>
    [Serializable]
    public class InputParam_Complated_Ask:Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public InputParam_Complated_Ask()
        {
            AskMessage = "参数录入完毕";
        }
    }


}

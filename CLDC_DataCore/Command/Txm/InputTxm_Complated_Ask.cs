using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// 录入条码完毕
    /// </summary>
    [Serializable]
    public class InputTxm_Complated_Ask : Command_Ask
    {
        /// <summary>
        /// 
        /// </summary>
        public InputTxm_Complated_Ask()
        {
            AskMessage = "录入条码完毕";
        }
    }
}

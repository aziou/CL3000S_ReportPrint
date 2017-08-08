using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// 请求录入条行码
    /// </summary>
    [Serializable]
    public class InputTxm_Ask : Command_Ask
    {
        /// <summary>
        /// 
        /// </summary>
        public InputTxm_Ask()
        {
            AskMessage = "请求录入条行码";
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// ����¼��������
    /// </summary>
    [Serializable]
    public class InputTxm_Ask : Command_Ask
    {
        /// <summary>
        /// 
        /// </summary>
        public InputTxm_Ask()
        {
            AskMessage = "����¼��������";
        }


    }
}

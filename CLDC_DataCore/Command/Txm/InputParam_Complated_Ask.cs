using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// ����¼�����
    /// </summary>
    [Serializable]
    public class InputParam_Complated_Ask:Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public InputParam_Complated_Ask()
        {
            AskMessage = "����¼�����";
        }
    }


}

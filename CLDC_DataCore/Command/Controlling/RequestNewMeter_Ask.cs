using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// ���±�����
    /// </summary>
    [Serializable]
    public class RequestNewMeter_Ask:Command_Ask
    {

        /// <summary>
        /// 
        /// </summary>
        public RequestNewMeter_Ask()
        {
            AskMessage = "���±�";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// ����ֹͣ�춨
    /// </summary>
    [Serializable]
    public class CheckStop_Ask : Command_Ask
    {
        /// <summary>
        /// 
        /// </summary>
        public CheckStop_Ask()
        {
            AskMessage = "ֹͣ�춨";
        }
    }
}

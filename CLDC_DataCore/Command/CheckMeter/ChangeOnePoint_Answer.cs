using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// �����춨�ظ���
    /// </summary>
    [Serializable]    
    public class ChangeOnePoint_Answer : Command_Answer
    {
        /// <summary>
        /// ���
        /// </summary>
        public bool bAgree;

    }
}

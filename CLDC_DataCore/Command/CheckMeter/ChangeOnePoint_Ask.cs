using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// �����춨
    /// </summary>
    [Serializable]
    public class ChangeOnePoint_Ask : Command_Ask
    {
         /// <summary>
        /// Ҫ��ת���ĵ�
        /// </summary>
        public int ActiveID;
        /// <summary>
        /// ��ʼ�����춨
        /// </summary>
        public ChangeOnePoint_Ask()
        {
            AskMessage="��ʼ�����춨";
        }
    }
}

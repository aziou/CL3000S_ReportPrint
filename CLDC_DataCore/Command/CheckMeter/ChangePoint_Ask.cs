using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// ��������
    /// </summary>
    [Serializable]
    public class ChangePoint_Ask:Command_Ask 
    {
        /// <summary>
        /// Ҫ��ת���ĵ�
        /// </summary>
        public int ActiveID;

        /// <summary>
        /// 
        /// </summary>
        public ChangePoint_Ask()
        {
            AskMessage = "�춨���л�";
        }
    }


}

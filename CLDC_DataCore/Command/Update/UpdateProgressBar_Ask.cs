using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// ���½�������Ϣ
    /// <summary>
    /// ���½�������Ϣ
    /// </summary>
    [Serializable ]
    public class UpdateProgressBar_Ask:Command_Ask 
    {
        /// <summary>
        /// ��ǰ��ֵ
        /// </summary>
        public int CurrentValue;

        /// <summary>
        /// �ܽ���ʱ��
        /// </summary>
        public int MaxValue;

        public UpdateProgressBar_Ask()
        {
            AskMessage = "���½�������Ϣ";
        }
    }
}

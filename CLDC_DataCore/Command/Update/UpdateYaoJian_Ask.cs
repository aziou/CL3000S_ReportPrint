using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// ����Ҫ�����Ϣ
    /// </summary>
    [Serializable]
    public class UpdateYaoJian_Ask:Command_Ask
    {
        /// <summary>
        /// ��λ��
        /// </summary>
        public int Bwh;

        /// <summary>
        /// �Ƿ�Ҫ��
        /// </summary>
        public bool IsYaoJian;

        /// <summary>
        /// 
        /// </summary>
        public UpdateYaoJian_Ask()
        {
            AskMessage = "���Ƿ�Ҫ��״̬����";
        }
    }
}

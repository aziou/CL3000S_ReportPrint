using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// �ͻ���ÿ¼һ�����붼��Ҫͨ�����ṹ���͵��ͻ���
    /// </summary>
    [Serializable]
    public class InputTxm_Update_Ask : Command_Ask
    {
        /// <summary>
        /// ���������������
        /// </summary>
        public string Txm = "";		

        /// <summary>
        /// ��λ��.�±��0��ʼ
        /// </summary>
        public int Bwh = 0;			

        /// <summary>
        /// ���캯��
        /// </summary>
        public InputTxm_Update_Ask()
        {
            AskMessage = "���������";
        }

    }
}

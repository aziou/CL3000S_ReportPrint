using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// ����ɨ����Ļظ���
    /// </summary>
    [Serializable]
    public class InputTxm_Answer : Command_Answer
    {

        /// <summary>
        /// �Ƿ�ͬ��
        /// </summary>
        public bool bAgree = false;

        /// <summary>
        /// 
        /// </summary>
        public InputTxm_Answer()
        {
        }

    }


}

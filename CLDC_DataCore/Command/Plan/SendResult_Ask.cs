using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{

    /// <summary>
    /// ������շ����ļ��Ľ��
    /// </summary>
    [Serializable]
    public class SendResult_Ask : Command_Ask 
    {
        /// <summary>
        /// �����Ƿ���ճɹ�
        /// </summary>
        public bool IsRecvSccuess = false;

        public SendResult_Ask()
        {
            AskMessage = "���շ����ļ����";
        }

    }



}

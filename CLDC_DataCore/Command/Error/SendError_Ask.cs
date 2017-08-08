using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Error
{
    /// <summary>
    /// ���ʹ�����ʾ��Ϣ��ͨ����һ������һ��ͨ���������ݷ���
    /// </summary>
    [Serializable]
    public class SendError_Ask : Command_Ask 
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string ErrorDescription = string.Empty;

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public object ErrorOther = null;
        /// ������ʾ��Ϣ
        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        public SendError_Ask()
        {
            AskMessage = "������ʾ��Ϣ";
        }
    }
}

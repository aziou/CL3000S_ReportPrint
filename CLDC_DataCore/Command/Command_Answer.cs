using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command
{ 
    /// <summary>
    /// ��������(�ش�)����
    /// </summary>
    [Serializable()]
    public class Command_Answer:CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// ���ص���Ϣ��ʾ
        /// </summary>
        public string AnswerMessage = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public Command_Answer()
        {
            AnswerMessage = string.Empty;
        }
    }


}

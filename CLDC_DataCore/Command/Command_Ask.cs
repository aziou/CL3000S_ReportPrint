using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command
{
    /// <summary>
    /// �������� (��) ����
    /// </summary>   
    [Serializable()]
    public class Command_Ask:CLDC_CTNProtocol.CTNPCommand 
    {
        /// <summary>
        /// �����ʾ������Ϣ
        /// </summary>
        public string AskMessage = string.Empty;

        /// ̨����
        /// <summary>
        /// ̨����
        /// </summary>
        public int taiID = -1;

        /// <summary>
        /// 
        /// </summary>
        public Command_Ask()
        {
            AskMessage = string.Empty;
        }
    }


}

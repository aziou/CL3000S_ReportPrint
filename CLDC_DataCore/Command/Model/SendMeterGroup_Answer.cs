using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// ������ģ��
    /// </summary>
    [Serializable]
    public class SendMeterGroup_Answer : Command_Answer 
    {
        /// <summary>
        /// ���ս��
        /// </summary>
        public bool bAgree;


    }
}

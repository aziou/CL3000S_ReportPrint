using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// �������������ķ���
    /// </summary>
    [Serializable]
    public class RequestControlling_Answer:Command_Answer 
    {
        /// <summary>
        /// �Ƿ�ͬ�����
        /// </summary>
        public bool bAgree = false;

        /// <summary>
        /// 
        /// </summary>
        public RequestControlling_Answer()
        {
        }
    }
}

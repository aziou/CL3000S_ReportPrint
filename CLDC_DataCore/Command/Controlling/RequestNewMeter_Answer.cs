using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// ���±�������
    /// </summary>
    [Serializable]
    public class RequestNewMeter_Answer : Command_Answer 
    {

        /// <summary>
        /// 
        /// </summary>
        public bool IsOk;

        /// <summary>
        /// 
        /// </summary>
        public RequestNewMeter_Answer()
        { }
    }
}

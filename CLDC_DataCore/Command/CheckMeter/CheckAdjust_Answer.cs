using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// ����������߽�������
    /// </summary>
    [Serializable]
    public class CheckAdjust_Answer:Command_Answer 
    {
        /// <summary>
        /// true����false��������
        /// </summary>
        public bool  bAgree;



    }
}

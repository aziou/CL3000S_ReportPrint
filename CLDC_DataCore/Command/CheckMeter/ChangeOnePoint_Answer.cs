using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 单步检定回复包
    /// </summary>
    [Serializable]    
    public class ChangeOnePoint_Answer : Command_Answer
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool bAgree;

    }
}

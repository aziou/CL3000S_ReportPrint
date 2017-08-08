using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 请求跳点
    /// </summary>
    [Serializable]
    public class ChangePoint_Answer:Command_Answer 
    {

        /// <summary>
        /// 结果
        /// </summary>
        public bool bAgree;


    }


}

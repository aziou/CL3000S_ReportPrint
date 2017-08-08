using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// 更新进度条信息
    /// <summary>
    /// 更新进度条信息
    /// </summary>
    [Serializable ]
    public class UpdateProgressBar_Ask:Command_Ask 
    {
        /// <summary>
        /// 当前的值
        /// </summary>
        public int CurrentValue;

        /// <summary>
        /// 总进度时间
        /// </summary>
        public int MaxValue;

        public UpdateProgressBar_Ask()
        {
            AskMessage = "更新进度条信息";
        }
    }
}

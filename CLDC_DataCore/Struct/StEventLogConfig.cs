using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 事件记录项目配置信息
    /// </summary>
    public struct StEventLogConfig
    {
        /// <summary>
        /// 事件记录项目ID
        /// </summary>
        public string EventLogPrjID;
        /// <summary>
        /// 事件记录项目名称  
        /// </summary>
        public string EventLogPrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public StPowerPramerter OutPramerter;
    }
}

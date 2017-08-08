using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 事件记录方案项目
    /// </summary>
    [Serializable()]
    public struct StPlan_EventLog
    {
        /// <summary>
        /// 事件记录项目ID
        /// </summary>
        public string EventLogPrjID;
        /// <summary>
        /// 多功能项目名称  
        /// </summary>
        public string EventLogPrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public CLDC_DataCore.Struct.StPowerPramerter OutPramerter;
        /// <summary>
        /// 项目检定参数
        /// </summary>
        public string PrjParm;

        public override string ToString()
        {
            return EventLogPrjName;
        }

    }
}

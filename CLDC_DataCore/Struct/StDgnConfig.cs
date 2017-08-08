using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 多功能项目配置信息
    /// </summary>
    public struct StDgnConfig
    {
        /// <summary>
        /// 多功能项目ID
        /// </summary>
        public string DgnPrjID;
        /// <summary>
        /// 多功能项目名称  
        /// </summary>
        public string DgnPrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public StPowerPramerter OutPramerter;
    }
}

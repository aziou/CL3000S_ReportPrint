using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 冻结项目配置信息
    /// </summary>
    [Serializable()]
    public class StFreezeConfig
    {
        /// <summary>
        /// 冻结项目ID
        /// </summary>
        public string FreezePrjID;
        /// <summary>
        /// 冻结项目名称  
        /// </summary>
        public string FreezePrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public StPowerPramerter OutPramerter;
    }
}

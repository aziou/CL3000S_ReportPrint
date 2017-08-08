using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 误差限
    /// </summary>
    public struct  StWuChaDeal
    {
        /// <summary>
        /// 表等级
        /// </summary>
        public Single MeterLevel;

        /// <summary>
        /// 误差上限
        /// </summary>
        public Single MaxError;

        /// <summary>
        /// 误差下限
        /// </summary>
        public Single MinError;

        /// <summary>
        /// 是否是标准表
        /// </summary>
        public bool IsBiaoZunBiao;
    }
}

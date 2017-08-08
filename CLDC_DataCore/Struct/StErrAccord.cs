using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 误差一致性项目结构
    /// </summary>
    [Serializable()]
    public struct StErrAccord
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PrjName;

        /// <summary>
        /// 误差一致性项目类型
        /// </summary>
        public int ErrAccordType;

        /// <summary>
        /// 误差点信息集
        /// </summary>
        public List<StErrAccordbase> lstErrPoint;

        /// <summary>
        /// 时间1(变差:表示两次测试时间间隔5min  过载试验:过载所用时间15min)
        /// </summary>
        public float Time1;

        /// <summary>
        /// 时间2(只有过载试验使用:恢复等待时间15min)
        /// </summary>
        public float Time2;

        public override string ToString()
        {
            return PrjName;
        }
    }
}

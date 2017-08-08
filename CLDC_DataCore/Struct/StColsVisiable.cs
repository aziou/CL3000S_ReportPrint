using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 列显示
    /// </summary>
    public struct StColsVisiable
    {
        /// <summary>
        /// 原始列名
        /// </summary>
        public string ColName;
        /// <summary>
        /// 要显示列名，规则默认等于原始
        /// </summary>
        public string ColShowName;
        /// <summary>
        /// 显示类型；0：不显示，1：显示
        /// </summary>
        public int ColShowType;
    }
}

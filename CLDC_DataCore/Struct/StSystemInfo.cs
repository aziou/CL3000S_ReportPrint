using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{

    /// <summary>
    /// 
    /// </summary>
    public struct StSystemInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 项目值
        /// </summary>
        public string Value;
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassName;
        /// <summary>
        /// 数据源，存在多个数据用管道符号分隔(是|否)（三相台|单相台）
        /// </summary>
        public string DataSource;
    }
}

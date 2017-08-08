using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
    {
    /// <summary>
    /// 结论结构体
    /// </summary>
    public struct  StWuChaResult
        {
        /// <summary>
        /// 结论
        /// </summary>
        public string  Result;
        
        /// <summary>
        /// 详细数据。数据结构参照《Cl3000DV90数据库结构设计文档V1.6相关定义》
        /// </summary>
        public string Data;
        }
    }

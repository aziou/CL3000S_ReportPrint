using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
    {
    /// <summary>
    /// 走字计算结果
    /// </summary>
    public struct StZZResult
        {
        /// <summary>
        /// 电表起码
        /// </summary>
        public float QiMa;

        /// <summary>
        /// 电表止码
        /// </summary>
        public float ZiMa;

        /// <summary>
        /// 实际走字电量
        /// </summary>
        public float DL;

        /// <summary>
        /// 相对误差
        /// </summary>
        public float Error;

        /// <summary>
        /// 检定结论
        /// </summary>
        public bool Result;

        }
    }

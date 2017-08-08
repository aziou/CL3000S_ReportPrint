using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
    {
    /// <summary>
    /// 时间结构
    /// </summary>
    public struct StTime
        {
        /// <summary>
        /// 时
        /// </summary>
        public int Hour;

        /// <summary>
        /// 分
        /// </summary>
        public  int Minute;

        /// <summary>
        /// 秒
        /// </summary>
        public int Seconds;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}",Hour.ToString("D2"),Minute.ToString("D2"),Seconds.ToString("D2"));
        }

        }


    }

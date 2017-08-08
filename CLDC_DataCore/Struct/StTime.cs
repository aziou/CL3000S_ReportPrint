using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
    {
    /// <summary>
    /// ʱ��ṹ
    /// </summary>
    public struct StTime
        {
        /// <summary>
        /// ʱ
        /// </summary>
        public int Hour;

        /// <summary>
        /// ��
        /// </summary>
        public  int Minute;

        /// <summary>
        /// ��
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

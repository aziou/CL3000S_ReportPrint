using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{

    /// <summary>
    /// 详细误差数据、第一次误差、第二次误差...
    /// </summary>
    [Serializable()]
    public struct  StError
    {
        private List<float> LstError;
        /// <summary>
        /// 添加一个误差
        /// </summary>
        /// <param name="fValue"></param>
        public void Add(float fValue)
        {
            if (LstError == null) LstError = new List<float>();
            LstError.Add(fValue);  
        }
        /// <summary>
        /// 误差查询
        /// </summary>
        /// <param name="index">误差次数</param>
        /// <returns>误差值</returns>
        public float this[int index]
        {
            get
            {
                if (LstError == null) LstError = new List<float>();
                return LstError[index];
            }
            set
            {
                if (LstError == null) LstError = new List<float>();
                LstError[index] = value;
            }
        }
        /// <summary>
        /// 误差总数量
        /// </summary>
        public int Count
        {
            get
            {
                if (LstError == null) LstError = new List<float>();
                return LstError.Count;
            }
        }
        /// <summary>
        /// 清空误差
        /// </summary>
        public void Clear()
        {
            if (LstError == null) LstError = new List<float>();
            LstError.Clear();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{

    /// <summary>
    /// ��ϸ������ݡ���һ�����ڶ������...
    /// </summary>
    [Serializable()]
    public struct  StError
    {
        private List<float> LstError;
        /// <summary>
        /// ���һ�����
        /// </summary>
        /// <param name="fValue"></param>
        public void Add(float fValue)
        {
            if (LstError == null) LstError = new List<float>();
            LstError.Add(fValue);  
        }
        /// <summary>
        /// ����ѯ
        /// </summary>
        /// <param name="index">������</param>
        /// <returns>���ֵ</returns>
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
        /// ���������
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
        /// ������
        /// </summary>
        public void Clear()
        {
            if (LstError == null) LstError = new List<float>();
            LstError.Clear();
        }

    }
}

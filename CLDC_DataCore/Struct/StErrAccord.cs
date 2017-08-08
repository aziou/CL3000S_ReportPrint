using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// ���һ������Ŀ�ṹ
    /// </summary>
    [Serializable()]
    public struct StErrAccord
    {
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string PrjName;

        /// <summary>
        /// ���һ������Ŀ����
        /// </summary>
        public int ErrAccordType;

        /// <summary>
        /// ������Ϣ��
        /// </summary>
        public List<StErrAccordbase> lstErrPoint;

        /// <summary>
        /// ʱ��1(���:��ʾ���β���ʱ����5min  ��������:��������ʱ��15min)
        /// </summary>
        public float Time1;

        /// <summary>
        /// ʱ��2(ֻ�й�������ʹ��:�ָ��ȴ�ʱ��15min)
        /// </summary>
        public float Time2;

        public override string ToString()
        {
            return PrjName;
        }
    }
}

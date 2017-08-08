using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// �����������ز���Ŀ�ṹ
    /// ��    �ߣ�vs
    /// ��д���ڣ�2010-09-06
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable()]
    public struct StPlan_Carrier
    {
        /// <summary>
        /// ��ĿID
        /// </summary>
        public string str_PrjID
        {
            get
            {
                return String.Format("{0}{1}{2}"                                          //Key:�μ����ݽṹ��Ƹ�2
                          , (int)Cus_MeterResultPrjID.�ز�����
                          , str_Code
                          , str_Type);
            }
            set { }
        }
        /// <summary>
        /// �ز���������
        /// </summary>
        public string str_Type;
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string str_Name;
        /// <summary>
        /// ��ʶ��
        /// </summary>
        public string str_Code;
        /// <summary>
        /// ���ʹ���
        /// </summary>
        public int int_Times;
        /// <summary>
        /// �ɹ���
        /// </summary>
        public float flt_Success;
        /// <summary>
        /// ���ʱ��(��)
        /// </summary>
        public int int_Interval;
        /// <summary>
        /// �ز�ģ�黥��
        /// </summary>
        public bool b_ModuleSwaps;
        /// <summary>
        /// Դ�������
        /// </summary>
        public StPowerPramerter OutPramerter;

        /// <summary>
        /// ��дToString��������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("�ز�|{0}|{1}",  str_Name, str_Code);
        }
    }
}

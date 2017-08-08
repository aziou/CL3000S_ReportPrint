using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// �����������������ݱȶ���Ŀ�ṹ
    /// ��    �ߣ�zzg
    /// ��д���ڣ�2014-05-07
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable()]
    public struct StPlan_Infrared
    {
        /// <summary>
        /// ��ĿID
        /// </summary>
        public string str_PrjID
        {
            get
            {
                return String.Format("{0}{1}"                                          //Key:�μ����ݽṹ��Ƹ�2
                        , (int)Cus_MeterResultPrjID.�������ݱȶ�����
                        , str_Code);
            }
            set { }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string str_Name;
        /// <summary>
        /// ��ʶ��
        /// </summary>
        public string str_Code;        
        
        /// <summary>
        /// ��дToString��������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("����|{0}|{1}",  str_Name, str_Code);
        }
    }
}

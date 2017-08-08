using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// �������������ݱ�ʶ��Ϣ
    /// ��    �ߣ�zzg
    /// ��д���ڣ�2013-04-26
    /// �޸ļ�¼��
    /// �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable()]
    public struct StDataFlagInfo
    {
        /// <summary>
        /// ���ݱ�ʶ����
        /// </summary>
        public string DataFlagName;
        /// <summary>
        /// ���ݱ�ʶ
        /// </summary>
        public string DataFlag;
        /// <summary>
        /// ���ݳ���
        /// </summary>
        public string DataLength;
        /// <summary>
        /// С��λ
        /// </summary>
        public string DataSmallNumber;        
        /// <summary>
        /// ���ݸ�ʽ
        /// </summary>
        public string DataFormat;        
    }
}

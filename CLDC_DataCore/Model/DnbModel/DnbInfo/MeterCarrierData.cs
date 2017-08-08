using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// �����������ز�ͨѶ�춨
    /// ��    �ߣ�vs
    /// ��д���ڣ�2010-09-13
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable()]
    public class MeterCarrierData : MeterErrorBase 
    {
        
        /// <summary>
        /// ��ĿID	
        /// </summary>
        public string Mce_PrjID = "";
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string Mce_PrjName = "";
        /// <summary>
        /// ��Ŀֵ
        /// </summary>
        public string Mce_PrjValue = "";
        /// <summary>
        /// ��ĿID
        /// </summary>
        public string Mce_PrjNumber = "";
        /// <summary>
        /// �춨�����
        /// </summary>
        public string Mce_ItemResult = "";

        /// <summary>
        /// 7��ʼ�ٲ�ʱ��
        /// </summary>
        public string DTM_START_TIME { get; set; }

        /// <summary>
        /// 8����ʱ��
        /// </summary>
        public string DTM_END_TIME { get; set; }

        /// <summary>
        /// 9�ܴ���
        /// </summary>
        public string AVR_NUMBER_TOTAL { get; set; }

        /// <summary>
        /// 10�ɹ�����
        /// </summary>
        public string AVR_NUMBER_SUCCEED { get; set; }

        /// <summary>
        /// 11ʧ�ܴ���
        /// </summary>
        public string AVR_NUMBER_FAIL { get; set; }

        /// <summary>
        /// 12�ɹ��ʣ���%ǰ������֣�
        /// </summary>
        public string AVR_RATIO_SUCCEED { get; set; }
        /// <summary>
        /// 13�ɹ�������ޣ�����0 ��һ��ֵ��
        /// </summary>
        public string AVR_LIMIT { get; set; }

        /// <summary>
        /// 14��ע���ز��豸��Ϣ
        /// </summary>
        public string AVR_RESERVE { get; set; }
    }
}

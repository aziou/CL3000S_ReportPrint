using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// ��������������ͨѶ�춨
    /// ��    �ߣ�zzg
    /// ��д���ڣ�2014-04-07
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    [Serializable()]
    public class MeterInfraredData : MeterErrorBase 
    {
        
        /// <summary>
        /// ��ĿID	
        /// </summary>
        public string Mif_PrjID = "";
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string Mif_PrjName = "";
        /// <summary>
        /// ���Ⳮ������
        /// </summary>
        public string Mif_PrjInfrareValue = "";
        /// <summary>
        /// 485��������
        /// </summary>
        public string Mif_Prj485Value = "";
        /// <summary>
        /// ��ĿID
        /// </summary>
        public string Mif_PrjNumber = "";
        /// <summary>
        /// �춨�����
        /// </summary>
        public string Mif_ItemResult = "";

        /// <summary>
        /// 7��ʼ�ٲ�ʱ��
        /// </summary>
        public string DTM_START_TIME { get; set; }

        /// <summary>
        /// 8����ʱ��
        /// </summary>
        public string DTM_END_TIME { get; set; }

        /// <summary>
        /// 14��ע�������豸��Ϣ
        /// </summary>
        public string AVR_RESERVE { get; set; }
    }
}

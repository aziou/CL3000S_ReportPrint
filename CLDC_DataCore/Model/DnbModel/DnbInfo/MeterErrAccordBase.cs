using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterErrAccordBase : MeterErrorBase
    {
        public MeterErrAccordBase()
        {

        }
        /// <summary>
        /// �����ĿID
        /// </summary>
        public string Mea_PrjID = "";
        /// <summary>
        /// ��Ŀ��������
        /// </summary>
        public string Mea_PrjName = "";
        /// <summary>
        /// �춨�����		�ϸ�/���ϸ�
        /// </summary>
        public string Mea_ItemResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
        /// <summary>
        /// ��������		1.0��0.5L��
        /// </summary>
        public string Mea_Glys = "";
        /// <summary>
        /// ��������		Imax,3.0Ib��
        /// </summary>
        public string Mea_xib = "";
        /// <summary>
        /// ��ѹ����		1,0.5,0.8,1.2�� 
        /// </summary>
        public string Mea_xU = "";
        /// <summary>
        /// �����		   ����|����
        /// </summary>
        public string Mea_WcLimit = "";
        /// <summary>
        /// Ȧ��
        /// </summary>
        public int Mea_Qs = 0;
        /// <summary>
        /// Ƶ��
        /// </summary>
        public string Mea_PL = "";
        /// <summary>
        /// ���ֵ1(��һ�β���)		���һ|����|...|���ƽ��|����
        /// </summary>	
        public string Mea_Wc1 = "";
        /// <summary>
        /// ���ֵ2(�ڶ��β���)		���һ|����|...|���ƽ��|����
        /// </summary>	
        public string Mea_Wc2 = "";
        /// <summary>
        /// ��ֵ
        /// </summary>
        public string Mea_Wc = "";
        /// <summary>
        /// ��Ʒ��ֵ                ֻ�����һ������Ч    
        /// </summary>
        public string Mea_WcAver = "";
        /// ���һ���Ե�����Ŀ��ţ�ֵΪ��{0}{1}�����һ�������ͣ�����Ŀ���+1
        /// <summary>
        /// ���һ���Ե�����Ŀ��ţ�ֵΪ��{0}{1}�����һ�������ͣ�����Ŀ���+1
        /// </summary>
        public string Sub_Item_ID = "";
    }
}

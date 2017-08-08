using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterErrAccord : MeterErrorBase
    {
        public MeterErrAccord()
        {

        }

        /// <summary>
        /// ���һ�������� 1�����һ���� 2������� 3�����ص������� 4����������
        /// </summary>
        public int Mea_Type;
        
        /// <summary>
        /// ��Ŀ����		�ϸ�/���ϸ�
        /// </summary>
        public string Mea_Result = CLDC_DataCore.Const.Variable.CTG_HeGe;

        /// <summary>
        /// ����ֵ
        /// </summary>
        public string ProgressValue;

        /// <summary>
        /// ����Ŀ�°����ļ춨��
        /// </summary>
        public Dictionary<string, MeterErrAccordBase> lstTestPoint = new Dictionary<string, MeterErrAccordBase>();
        /// <summary>
        /// ���ϸ�ԭ��
        /// </summary>
        public string Description;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using CLDC_Comm;
using CLDC_DataCore.Const;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.WuChaDeal
{
    abstract class WuChaBase
    {
        /// <summary>
        /// ���������ò���
        /// </summary>
        private CLDC_DataCore.Struct.StWuChaDeal m_WuChaPara;
        /// <summary>
        /// ��������
        /// </summary>
        private static Dictionary<string, float[]> DicJianJu = null;
        /// <summary>
        /// ������Ҫ���ص�����
        /// </summary>
        public string OtherData;

        /// <summary>
        /// ���캯��
        /// </summary>
        public WuChaBase(CLDC_DataCore.Struct.StWuChaDeal WuChaDeal)
        {
            m_WuChaPara = WuChaDeal;
        }
        /// <summary>
        /// ����������
        /// Ҳ�����ڹ���ʱ���뱾����
        /// </summary>
        public CLDC_DataCore.Struct.StWuChaDeal WuChaPara
        {
            get
            {
                return m_WuChaPara;
            }
            set
            {
                m_WuChaPara = value;
            }
        }

        //<summary>
        //�������
        //</summary>
        //<param name="arrNumber"></param>
        //<returns></returns>
        public virtual CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase SetWuCha(params  float[] arrNumber)
        {
            return new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase();
        }

        #region ��ӷ���---------AddFlag----------

        /// <summary>
        /// ��+-����
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        protected string AddFlag(string Number)
        {
            string strFlag = "";
            if (float.Parse(Number) >= 0)
            {
                strFlag = "+";
            }
            return String.Format("{0}{1}", strFlag, Number);
        }

        /// <summary>
        /// �������ּ�+-��
        /// </summary>
        /// <param name="Number">Ҫ����������</param>
        /// <param name="Priecision">��������</param>
        /// <returns>����ָ�����ȵĴ�+-�ŵ��ַ���</returns>
        protected string AddFlag(float Number, int Priecision)
        {
            string strValue = Number.ToString(String.Format("F{0}", Priecision));
            strValue = AddFlag(strValue);
            return strValue;
        }
        #endregion

        #region �������ܺ���
        /// <summary>
        /// �����������
        /// </summary>
        /// <IsWindage>�Ƿ���ƫ��</IsWindage> 
        /// <returns></returns>
        protected float getWuChaHzzJianJu(bool IsWindage)
        {
            float[] JianJu = new float[] { 2, 2 };
            string Key = String.Empty;
            //���ݱ��ȼ���������������
            if (!m_WuChaPara.IsBiaoZunBiao)
                Key = String.Format("Level{0}", m_WuChaPara.MeterLevel);
            else
                Key = String.Format("Level{0}B", m_WuChaPara.MeterLevel);

            if (DicJianJu == null)
            {
                DicJianJu = new Dictionary<string, float[]>();
                DicJianJu.Add("Level0.02B", new float[] { 0.002F, 0.0002F });     //0.02����
                DicJianJu.Add("Level0.05B", new float[] { 0.005F, 0.0005F });     //0.05����
                DicJianJu.Add("Level0.1B", new float[] { 0.01F, 0.001F });     //0.1����
                DicJianJu.Add("Level0.2B", new float[] { 0.02F, 0.002F });     //0.2����׼��
                DicJianJu.Add("Level0.2", new float[] { 0.02F, 0.004F });     //0.2����ͨ��
                DicJianJu.Add("Level0.5", new float[] { 0.05F, 0.01F });     //0.5����
                DicJianJu.Add("Level1", new float[] { 0.1F, 0.02F });     //1����
                DicJianJu.Add("Level2", new float[] { 0.2F, 0.04F });     //2����
            }

            if (DicJianJu.ContainsKey(Key))
            {
                JianJu = DicJianJu[Key];
            }
            else
            {
                JianJu = new float[] { 2, 2 };    //û�����ֵ����ҵ�����ֱ�Ӱ�2��
            }

            if (IsWindage)
            {
                //��ƫ��
                return (float)JianJu[1];
            }
            else
            {
                //��ͨ���
                return (float)JianJu[0];
            }
        }
        /// <summary>
        /// ȡ���ܱ�ƽ��ֵ��Լ����
        /// </summary>
        /// <returns>����</returns>
        protected int getAvgPrecision()
        {
            return GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_WC_AVGPRECISION, 4);
            //if (m_WuChaPara.MeterLevel <= 0.05F)
            //    return 4;
            //else if (m_WuChaPara.MeterLevel >= 0.1F && m_WuChaPara.MeterLevel <= 0.5F)
            //    return 3;
            //else
            //    return 2;
        }
        #endregion
    }
}

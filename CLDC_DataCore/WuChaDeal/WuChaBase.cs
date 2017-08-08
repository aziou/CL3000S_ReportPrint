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
        /// 误差计算配置参数
        /// </summary>
        private CLDC_DataCore.Struct.StWuChaDeal m_WuChaPara;
        /// <summary>
        /// 误差化整间距表
        /// </summary>
        private static Dictionary<string, float[]> DicJianJu = null;
        /// <summary>
        /// 其它需要返回的数据
        /// </summary>
        public string OtherData;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WuChaBase(CLDC_DataCore.Struct.StWuChaDeal WuChaDeal)
        {
            m_WuChaPara = WuChaDeal;
        }
        /// <summary>
        /// 设置误差参数
        /// 也可以在构造时引入本参数
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
        //计算误差
        //</summary>
        //<param name="arrNumber"></param>
        //<returns></returns>
        public virtual CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase SetWuCha(params  float[] arrNumber)
        {
            return new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase();
        }

        #region 添加符号---------AddFlag----------

        /// <summary>
        /// 加+-符号
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
        /// 修正数字加+-号
        /// </summary>
        /// <param name="Number">要修正的数字</param>
        /// <param name="Priecision">修正精度</param>
        /// <returns>返回指定精度的带+-号的字符串</returns>
        protected string AddFlag(float Number, int Priecision)
        {
            string strValue = Number.ToString(String.Format("F{0}", Priecision));
            strValue = AddFlag(strValue);
            return strValue;
        }
        #endregion

        #region 辅助功能函数
        /// <summary>
        /// 返回修正间距
        /// </summary>
        /// <IsWindage>是否是偏差</IsWindage> 
        /// <returns></returns>
        protected float getWuChaHzzJianJu(bool IsWindage)
        {
            float[] JianJu = new float[] { 2, 2 };
            string Key = String.Empty;
            //根据表精度及表类型生成主键
            if (!m_WuChaPara.IsBiaoZunBiao)
                Key = String.Format("Level{0}", m_WuChaPara.MeterLevel);
            else
                Key = String.Format("Level{0}B", m_WuChaPara.MeterLevel);

            if (DicJianJu == null)
            {
                DicJianJu = new Dictionary<string, float[]>();
                DicJianJu.Add("Level0.02B", new float[] { 0.002F, 0.0002F });     //0.02级表
                DicJianJu.Add("Level0.05B", new float[] { 0.005F, 0.0005F });     //0.05级表
                DicJianJu.Add("Level0.1B", new float[] { 0.01F, 0.001F });     //0.1级表
                DicJianJu.Add("Level0.2B", new float[] { 0.02F, 0.002F });     //0.2级标准表
                DicJianJu.Add("Level0.2", new float[] { 0.02F, 0.004F });     //0.2级普通表
                DicJianJu.Add("Level0.5", new float[] { 0.05F, 0.01F });     //0.5级表
                DicJianJu.Add("Level1", new float[] { 0.1F, 0.02F });     //1级表
                DicJianJu.Add("Level2", new float[] { 0.2F, 0.04F });     //2级表
            }

            if (DicJianJu.ContainsKey(Key))
            {
                JianJu = DicJianJu[Key];
            }
            else
            {
                JianJu = new float[] { 2, 2 };    //没有在字典中找到，则直接按2算
            }

            if (IsWindage)
            {
                //标偏差
                return (float)JianJu[1];
            }
            else
            {
                //普通误差
                return (float)JianJu[0];
            }
        }
        /// <summary>
        /// 取电能表平均值修约精度
        /// </summary>
        /// <returns>精度</returns>
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

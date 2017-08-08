using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Function;
using CLDC_DataCore.Model.DnbModel.DnbInfo;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 最大需量误差计算
    /// </summary>
    class MaxXL : WuChaBase
    {
        public MaxXL(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        { }
        /// <summary>
        /// 计算电能表最大需量误差
        /// </summary>
        /// <param name="arrNumber">电表实际需量</param>
        /// <returns>多功能数据结构体 MeterDgn </returns>
        public override CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            string strError;
            string strNumber;
            //计算标准功率
            float starndP = 0;
            try
            {
                starndP =float.Parse(OtherData);
            }
            catch
            { }

            OtherData = "";
            
            float xlError = Number.GetRelativeWuCha(arrNumber[0], starndP);
            MeterDgn returnValue = new MeterDgn();
            strError = xlError.ToString("F2");
            strNumber = arrNumber[0].ToString("F4");
            returnValue.Md_chrValue = string.Format("{0}|{1}|{2}", starndP, arrNumber[0], strError);
            if (Math.Abs(xlError) <= WuChaPara.MaxError + 0.05F * starndP / arrNumber[0])
            {
                OtherData = CLDC_DataCore.Const.Variable.CTG_HeGe;
            }
            else
            {
                OtherData = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
            }

            return returnValue;
        }
    }
}

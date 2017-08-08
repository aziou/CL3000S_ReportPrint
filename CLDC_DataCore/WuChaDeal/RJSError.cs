using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Model.DnbModel.DnbInfo;

namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 日计时误差计算器
    /// </summary>
    class RJSError : WuChaBase
    {
        public RJSError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        { }

        /// <summary>
        /// 计算日计时误差
        /// </summary>
        /// <param name="arrNumber">输入误差数量</param>
        /// <returns></returns>
        public override MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            MeterDgn stResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn();
            int Precision = getAvgPrecision();
            float avgWC = CLDC_DataCore.Function.Number.GetAvgA(arrNumber);
            //取平均值
            avgWC = (float)Math.Round(avgWC, Precision);
            /*
             日计时误差化整间距为0.01
             * 参照JJG-596-1999 5.1.1
             */
            avgWC = CLDC_DataCore.Function.Number.GetHzz(avgWC, 0.01F);
            //结论：当
            bool Result = false;
            if (avgWC >= WuChaPara.MinError && avgWC <= WuChaPara.MaxError)
                Result = true;
            stResult.Md_chrValue = CLDC_DataCore.Function.Common.ConverResult(Result);
            return stResult;
        }
    }
}

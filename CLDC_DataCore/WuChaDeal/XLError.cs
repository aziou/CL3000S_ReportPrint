using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Struct;
namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 需量示值误差及需量周期误差计算
    /// </summary>
    class XLError : WuChaBase
    {

        public XLError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        { }

        /// <summary>
        ///  计算需量示值误差[标准电能表法]
        /// </summary>
        /// <param name="p">被检表需量示值</param>
        /// <param name="T0">实测的需量周期</param>
        /// <param name="W">标准表实测累计电能值</param>
        /// <param name="K1">标准电流互感器额定变比</param>
        /// <param name="K0">标准电压互感器额定变比</param>
        /// <param name="r">装置[标准表]已定系统误差</param>
        /// <returns>返回具有检定误差及结论的一个结构体</returns>
        public StWuChaResult GetWuCha(
            float p, float T0
            , float W, float K1
            , float K0, float r)
        {
            float other = (10 - r) / 100;
            float p0 = 60 * W * K1 * K0 / T0 * other;
            return GetWuCha(p, p0);
        }

        /// <summary>
        ///  计算需量示值误差[标准电能表法]
        /// </summary>
        /// <param name="p">被检表需量示值</param>
        /// <param name="T0">实测的需量周期</param>
        /// <param name="W">标准表实测累计电能值</param>
        /// <returns>返回具有检定误差及结论的一个结构体</returns>
        public StWuChaResult GetWuCha(float p, float T0, float W)
        {
            float P0 = 60 * W / T0;
            return GetWuCha(p, P0);
        }


        /// <summary>
        /// 计算需量示值误差[标准功率法]
        /// </summary>
        /// <param name="p">被检表需量示值</param>
        /// <param name="p0">标准功率表示值</param>
        /// <returns>返回具有检定误差及结论的一个结构体</returns>
        public StWuChaResult GetWuCha(float p, float p0)
        {
            bool Result = false;
            StWuChaResult stResult = new StWuChaResult();
            float wc = CLDC_DataCore.Function.Number.GetRelativeWuCha(p, p0);
            float intSpace = getWuChaHzzJianJu(false);
            wc = CLDC_DataCore.Function.Number.GetHzz(wc, intSpace);
            /*
             误差限参照青海省电能计量检定中心作业指导书Q/QDJL ZY0003-2007
             * 5.10.3 需量示值误差{%}不应该大于被检电能表准确度等级
             */
            if ((float)Math.Abs(wc) < WuChaPara.MeterLevel)
            {
                Result = true;
            }
            stResult.Result = CLDC_DataCore.Function.Common.ConverResult(Result);
            stResult.Data = wc.ToString();
            return stResult;
        }


        /// <summary>
        /// 计算需量周期误差
        /// </summary>
        /// <param name="t1">预定需量周期[秒]</param>
        /// <param name="t2">实测需量周期[秒]</param>
        /// <returns>返回包含结论及实际周期|误差|结论的结构体</returns>
        public StWuChaResult GetZQWuCha(int t1, int t2)
        {
            StWuChaResult stResult = new StWuChaResult();
            float wc = CLDC_DataCore.Function.Number.GetRelativeWuCha(t1, t2);
            string strResult = String.Empty;
            stResult.Result = CLDC_DataCore.Function.Common.ConverResult(Math.Abs(wc) < 1 ? true : false);

            stResult.Data = String.Format("{0}|{1}|{2}", t2, wc.ToString(), strResult);
            return stResult;
        }
    }





}

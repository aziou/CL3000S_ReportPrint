using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Struct;
using CLDC_DataCore.Model.DnbModel.DnbInfo;

namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 走字误差计算器--标准表法
    /// </summary>
    class ZZError : WuChaBase
    {
        public ZZError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        {

        }

        public override CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            //第一个参数：是否是总费率 1:是0：不是
            //第二个参数：起码,止码,标准表[头表]电量,标准表[头表]相对误差
            if (arrNumber[0] > 0)
            {
                return SetWuCha(arrNumber[1], arrNumber[2], arrNumber[3], arrNumber[4]);
            }
            else
                return SetWuCha(arrNumber[1], arrNumber[2], arrNumber[3], (int)arrNumber[4]);
            //return base.SetWuCha(arrNumber);
        }

        /// <summary>
        /// 标准表法/走字试验法误差计算
        /// </summary>
        /// <param name="QiMa">被检表起码</param>
        /// <param name="ZiMa">被检表止码</param>
        /// <param name="starandDl">标准表[头表]电量</param>
        /// <param name="starandWCOr">标准表[头表]相对误差</param>
        /// <returns></returns>
        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase SetWuCha(
            float QiMa,
            float ZiMa,
            float starandDl,
            float starandWC)
        {
            float fDiff = 0.0f;
            string strDot = string.Empty;
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError
                curResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError();
            int dotLeng = CLDC_DataCore.Function.Common.GetPrecision(QiMa);
            int dotLeng2 = CLDC_DataCore.Function.Common.GetPrecision(ZiMa);
            if (dotLeng < dotLeng2) dotLeng = dotLeng2;
            dotLeng += 1;
            if (dotLeng == 0)dotLeng = 3;
            dotLeng = 5;
            //修正标准表电量
            
            int intPower=(int)Math.Pow(10,dotLeng);
            starandDl = (int)(Math.Round(starandDl * intPower)) ;
            starandDl /= intPower;

            curResult.Mz_chrQiMa = QiMa;
            curResult. Mz_chrZiMa = ZiMa;
            fDiff = ZiMa - QiMa;
            strDot = string.Format("F{0}", dotLeng);
            curResult.Mz_chrQiZiMaC = Math.Round(fDiff, 3).ToString(strDot);
            //
            // string strData = string.Empty;
            float err = CLDC_DataCore.Function.Number.GetRelativeWuCha(float.Parse(curResult.Mz_chrQiZiMaC), starandDl, starandWC);
            curResult.Mz_chrWc = Math.Round(err, 2).ToString("F2");
            //计算方法参见JJG56-1999 4.4.2
            if (err <= WuChaPara.MaxError && err > WuChaPara.MinError)
            {
                curResult.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_HeGe;
            }
            else
            {
                curResult.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
            }

            return curResult;
        }

        /// <summary>
        /// 计算分费率走字误差
        /// </summary>
        /// <param name="QiMa">被检表起码</param>
        /// <param name="ZiMa">被检表止码</param>
        /// <param name="ZhongDL">总码差</param>
        /// <param name="MeterPrecision">电能表计度器小数位数</param>
        /// <returns></returns>
        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError SetWuCha(
            float QiMa,
            float ZiMa,
            float ZhongDL,
            int MeterPrecision)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError
                curResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError();

            //暂时将MeterPrecision改为2，因为这个一直都为0，实际上645规定电量为2位小数

            MeterPrecision = 2;

            curResult.Mz_chrQiMa = QiMa;
            curResult.Mz_chrZiMa = ZiMa;
            curResult.Mz_chrQiZiMaC = Math.Round(ZiMa - QiMa, 5).ToString("F5");

            // |费率码差-总码差| * 10 ^ MeterPrecision <=2 参见JJG596-1999 4.4.4
            float err = (float)((float.Parse(curResult.Mz_chrQiZiMaC) - ZhongDL) * Math.Pow(10, MeterPrecision));

            curResult.Mz_chrWc = Math.Round(err, 3).ToString("F3");
            if (Math.Abs(err) < 2)
            {
                curResult.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_HeGe;
            }
            else
            {
                curResult.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
            }

            return curResult;
        }
    }

    /// <summary>
    /// 走字误差计算器---组合误差
    /// </summary>
    class ZZUnionError : WuChaBase
    {
        public ZZUnionError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        {

        }
        /// <summary>
        /// 计算组合误差
        /// </summary>
        /// <param name="arrNumber">计算参数[总，尖，峰，平，谷]</param>
        /// <returns></returns>
        public override MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            float allFenFv = arrNumber[1] + arrNumber[2] + arrNumber[3] + arrNumber[4];
            float wc = CLDC_DataCore.Function.Number.GetRelativeWuCha(arrNumber[0], allFenFv);
            MeterZZError zzError = new MeterZZError();
            zzError.Mz_chrWc = wc.ToString();
            //结论
            if (wc >= WuChaPara.MinError && wc <= WuChaPara.MaxError)
                zzError.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_HeGe;
            else
                zzError.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
            return zzError;
            //return base.SetWuCha(arrNumber);
        }
    }
}

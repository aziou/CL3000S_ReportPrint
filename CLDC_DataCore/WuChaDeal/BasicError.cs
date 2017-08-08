using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Function;
using CLDC_DataCore.Const;                 //常量声明  
namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 基本误差计算器
    /// </summary>
     class BasicError : WuChaBase
    {

         public BasicError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
             : base(wuChaDeal)
         { }

        #region----------基本误差计算----------

        /// <summary>
        /// 计算基本误差
        /// </summary>
        /// <param name="arrNumber">要参与计算的误差数组</param>
        /// <returns></returns>
        public override CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase  SetWuCha(float[] arrNumber)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError
                curResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError();
            int AvgPriecision = getAvgPrecision();                                  //取平均值修约精度 
            float intSpace = getWuChaHzzJianJu(false);                              //化整间距 
            float AvgWuCha = CLDC_DataCore.Function.Number.GetAvgA(arrNumber);
            float HzzWuCha = CLDC_DataCore.Function.Number.GetHzz(AvgWuCha, intSpace);
            string AvgNumber;
            string HZNumber;
            //添加符号
            int hzPrecision = CLDC_DataCore.Function.Common.GetPrecision(intSpace);

            AvgNumber = AddFlag(AvgWuCha, AvgPriecision).ToString();
            HZNumber = AddFlag(HzzWuCha, hzPrecision);
            //-0.001化整成+0.0问题
            if (AvgWuCha < 0)
            {
                HZNumber = HZNumber.Replace('+', '-');
            }
            // 检测是否超过误差限
            if (AvgWuCha >= WuChaPara.MinError &&
                AvgWuCha <= WuChaPara.MaxError)
            {
                curResult.Me_chrWcJl = Variable.CTG_HeGe;
            }
            else
            {
                curResult.Me_chrWcJl = Variable.CTG_BuHeGe;
            }

            //记录误差
            string strWuCha = String.Empty;
            int wcCount = 0;
            for (int i = 0; i < arrNumber.Length; i++)
            {
                if (arrNumber[i] != CLDC_DataCore.Const.Variable.WUCHA_INVIADE)
                {
                    wcCount++;
                    strWuCha += String.Format("{0}|", AddFlag(arrNumber[i], AvgPriecision));
                }
                else
                {
                    strWuCha += " |";
                }
            }
            if (wcCount != arrNumber.Length)
            {
                curResult.Me_chrWcJl = Variable.CTG_BuHeGe;
            }

            strWuCha += String.Format("{0}|", AvgNumber);
            strWuCha += String.Format("{0}", HZNumber);
            curResult.Me_chrWc = strWuCha;

            return curResult;
        }
        #endregion

    }
}

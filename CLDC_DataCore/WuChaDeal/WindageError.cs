using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Const;
namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// 标准偏差计算器
    /// </summary>
     class WindageError : WuChaBase
    {

         public WindageError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
             : base(wuChaDeal)
         { }

        public override CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase  SetWuCha(params float[] arrNumber)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError
                stResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError();
            bool Result = false;
            int NumberCount = arrNumber.Length;
            int intHZPrecision = getAvgPrecision();                     //偏差值间距
            float intSpace = getWuChaHzzJianJu(true);                   //化整间距 
            float Windage = CLDC_DataCore.Function.Number.GetWindage(arrNumber); //计算标准偏差
            float HZWindage = 0F;
            Windage = (float)Math.Round(Windage, intHZPrecision);
            HZWindage = CLDC_DataCore.Function.Number.GetHzz(Windage , intSpace);
            //HZWindage = HZWindage / (float)NumberCount;
            string AvgNumber;
            string HZNumber;

            //添加符号
            int hzPrecision = CLDC_DataCore.Function.Common.GetPrecision(intSpace);

            AvgNumber = AddFlag(Windage, 4).Replace("+","");
            HZNumber = AddFlag(HZWindage, hzPrecision).Replace("+", "");
          
            //记录误差
            string strWuCha = String.Empty;
            int wcCount = 0;
            for (int i = 0; i < arrNumber.Length; i++)
            {
                if (arrNumber[i] != CLDC_DataCore.Const.Variable.WUCHA_INVIADE)
                {
                    wcCount++;
                    strWuCha += String.Format("{0}|", AddFlag(arrNumber[i], 4));
                }
                else
                {
                    strWuCha += " |";
                }
            }
           
            strWuCha += String.Format("{0}|", AvgNumber);
            strWuCha += String.Format("{0}", HZNumber);
            // 检测是否超过误差限
            if (HZWindage <= WuChaPara.MaxError && HZWindage >= WuChaPara.MinError)
            {
                Result = true;
            }
            if (wcCount != arrNumber.Length)
            {
                Result = false;
            }

            stResult.Me_chrWcJl = CLDC_DataCore.Function.Common.ConverResult(Result);
            stResult.Me_chrWc = strWuCha;
            return stResult;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Const;

namespace CLDC_DataCore.WuChaDeal
{
    class AccordError:WuChaBase
    {
        public AccordError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
             : base(wuChaDeal)
         { }

        #region----------����������----------

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="arrNumber">Ҫ���������������</param>
        /// <returns></returns>
        public override CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase  SetWuCha(float[] arrNumber)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccordBase
                curResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccordBase();
            int AvgPriecision = getAvgPrecision();                                  //ȡƽ��ֵ��Լ���� 
            float intSpace = getWuChaHzzJianJu(false);                              //������� 
            float AvgWuCha = CLDC_DataCore.Function.Number.GetAvgA(arrNumber);               //ƽ��ֵ
            float HzzWuCha = CLDC_DataCore.Function.Number.GetHzz(AvgWuCha, intSpace);       //����ֵ
            string AvgNumber;
            string HZNumber;
            //��ӷ���
            int hzPrecision = CLDC_DataCore.Function.Common.GetPrecision(intSpace);

            AvgNumber = AddFlag(AvgWuCha, AvgPriecision).ToString();
            HZNumber = AddFlag(HzzWuCha, hzPrecision);

            // ����Ƿ񳬹������
            if (AvgWuCha >= WuChaPara.MinError &&
                AvgWuCha <= WuChaPara.MaxError)
            {
                curResult.Mea_ItemResult = Variable.CTG_HeGe;
            }
            else
            {
                curResult.Mea_ItemResult = Variable.CTG_BuHeGe;
            }

            //��¼���
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
                curResult.Mea_ItemResult = Variable.CTG_BuHeGe;
            }

            strWuCha += String.Format("{0}|", AvgNumber);
            strWuCha += String.Format("{0}", HZNumber);
            curResult.Mea_Wc1 = strWuCha;

            return curResult;
        }
        #endregion
    }
}

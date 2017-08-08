using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Model.DnbModel.DnbInfo;

namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// �ռ�ʱ��������
    /// </summary>
    class RJSError : WuChaBase
    {
        public RJSError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        { }

        /// <summary>
        /// �����ռ�ʱ���
        /// </summary>
        /// <param name="arrNumber">�����������</param>
        /// <returns></returns>
        public override MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            MeterDgn stResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn();
            int Precision = getAvgPrecision();
            float avgWC = CLDC_DataCore.Function.Number.GetAvgA(arrNumber);
            //ȡƽ��ֵ
            avgWC = (float)Math.Round(avgWC, Precision);
            /*
             �ռ�ʱ�������Ϊ0.01
             * ����JJG-596-1999 5.1.1
             */
            avgWC = CLDC_DataCore.Function.Number.GetHzz(avgWC, 0.01F);
            //���ۣ���
            bool Result = false;
            if (avgWC >= WuChaPara.MinError && avgWC <= WuChaPara.MaxError)
                Result = true;
            stResult.Md_chrValue = CLDC_DataCore.Function.Common.ConverResult(Result);
            return stResult;
        }
    }
}

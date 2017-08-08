using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Struct;
using CLDC_DataCore.Model.DnbModel.DnbInfo;

namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// ������������--��׼��
    /// </summary>
    class ZZError : WuChaBase
    {
        public ZZError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        {

        }

        public override CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            //��һ���������Ƿ����ܷ��� 1:��0������
            //�ڶ�������������,ֹ��,��׼��[ͷ��]����,��׼��[ͷ��]������
            if (arrNumber[0] > 0)
            {
                return SetWuCha(arrNumber[1], arrNumber[2], arrNumber[3], arrNumber[4]);
            }
            else
                return SetWuCha(arrNumber[1], arrNumber[2], arrNumber[3], (int)arrNumber[4]);
            //return base.SetWuCha(arrNumber);
        }

        /// <summary>
        /// ��׼��/�������鷨������
        /// </summary>
        /// <param name="QiMa">���������</param>
        /// <param name="ZiMa">�����ֹ��</param>
        /// <param name="starandDl">��׼��[ͷ��]����</param>
        /// <param name="starandWCOr">��׼��[ͷ��]������</param>
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
            //������׼�����
            
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
            //���㷽���μ�JJG56-1999 4.4.2
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
        /// ����ַ����������
        /// </summary>
        /// <param name="QiMa">���������</param>
        /// <param name="ZiMa">�����ֹ��</param>
        /// <param name="ZhongDL">�����</param>
        /// <param name="MeterPrecision">���ܱ�ƶ���С��λ��</param>
        /// <returns></returns>
        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError SetWuCha(
            float QiMa,
            float ZiMa,
            float ZhongDL,
            int MeterPrecision)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError
                curResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError();

            //��ʱ��MeterPrecision��Ϊ2����Ϊ���һֱ��Ϊ0��ʵ����645�涨����Ϊ2λС��

            MeterPrecision = 2;

            curResult.Mz_chrQiMa = QiMa;
            curResult.Mz_chrZiMa = ZiMa;
            curResult.Mz_chrQiZiMaC = Math.Round(ZiMa - QiMa, 5).ToString("F5");

            // |�������-�����| * 10 ^ MeterPrecision <=2 �μ�JJG596-1999 4.4.4
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
    /// ������������---������
    /// </summary>
    class ZZUnionError : WuChaBase
    {
        public ZZUnionError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        {

        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="arrNumber">�������[�ܣ��⣬�壬ƽ����]</param>
        /// <returns></returns>
        public override MeterErrorBase SetWuCha(params float[] arrNumber)
        {
            float allFenFv = arrNumber[1] + arrNumber[2] + arrNumber[3] + arrNumber[4];
            float wc = CLDC_DataCore.Function.Number.GetRelativeWuCha(arrNumber[0], allFenFv);
            MeterZZError zzError = new MeterZZError();
            zzError.Mz_chrWc = wc.ToString();
            //����
            if (wc >= WuChaPara.MinError && wc <= WuChaPara.MaxError)
                zzError.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_HeGe;
            else
                zzError.Mz_chrJL = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
            return zzError;
            //return base.SetWuCha(arrNumber);
        }
    }
}

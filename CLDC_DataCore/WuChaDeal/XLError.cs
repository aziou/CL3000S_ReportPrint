using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Struct;
namespace CLDC_DataCore.WuChaDeal
{
    /// <summary>
    /// ����ʾֵ����������������
    /// </summary>
    class XLError : WuChaBase
    {

        public XLError(CLDC_DataCore.Struct.StWuChaDeal wuChaDeal)
            : base(wuChaDeal)
        { }

        /// <summary>
        ///  ��������ʾֵ���[��׼���ܱ�]
        /// </summary>
        /// <param name="p">���������ʾֵ</param>
        /// <param name="T0">ʵ�����������</param>
        /// <param name="W">��׼��ʵ���ۼƵ���ֵ</param>
        /// <param name="K1">��׼��������������</param>
        /// <param name="K0">��׼��ѹ����������</param>
        /// <param name="r">װ��[��׼��]�Ѷ�ϵͳ���</param>
        /// <returns>���ؾ��м춨�����۵�һ���ṹ��</returns>
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
        ///  ��������ʾֵ���[��׼���ܱ�]
        /// </summary>
        /// <param name="p">���������ʾֵ</param>
        /// <param name="T0">ʵ�����������</param>
        /// <param name="W">��׼��ʵ���ۼƵ���ֵ</param>
        /// <returns>���ؾ��м춨�����۵�һ���ṹ��</returns>
        public StWuChaResult GetWuCha(float p, float T0, float W)
        {
            float P0 = 60 * W / T0;
            return GetWuCha(p, P0);
        }


        /// <summary>
        /// ��������ʾֵ���[��׼���ʷ�]
        /// </summary>
        /// <param name="p">���������ʾֵ</param>
        /// <param name="p0">��׼���ʱ�ʾֵ</param>
        /// <returns>���ؾ��м춨�����۵�һ���ṹ��</returns>
        public StWuChaResult GetWuCha(float p, float p0)
        {
            bool Result = false;
            StWuChaResult stResult = new StWuChaResult();
            float wc = CLDC_DataCore.Function.Number.GetRelativeWuCha(p, p0);
            float intSpace = getWuChaHzzJianJu(false);
            wc = CLDC_DataCore.Function.Number.GetHzz(wc, intSpace);
            /*
             ����޲����ຣʡ���ܼ����춨������ҵָ����Q/QDJL ZY0003-2007
             * 5.10.3 ����ʾֵ���{%}��Ӧ�ô��ڱ�����ܱ�׼ȷ�ȵȼ�
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
        /// ���������������
        /// </summary>
        /// <param name="t1">Ԥ����������[��]</param>
        /// <param name="t2">ʵ����������[��]</param>
        /// <returns>���ذ������ۼ�ʵ������|���|���۵Ľṹ��</returns>
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace CLDC_DataCore.Function
{
    /// <summary>
    /// �й����ּ���Ĺ�������
    /// </summary>
    public class Number
    {
        /// <summary>
        /// ����һ�������ƽ��ֵ
        /// </summary>
        /// <param name="arrNumber">������������</param>
        /// <param name="inviade">������������Ч����</param>
        /// <returns></returns>
        public static float GetAvgA(float[] arrNumber,float inviade)
        {
            int intCount = arrNumber.Length;
            float Sum = 0;
            if (0 == intCount) return 99.99F;
            for (int i = 0; i < intCount; i++)
            {
                if (arrNumber[i] != inviade)
                {
                    Sum += arrNumber[i];
                }
            }
            return Sum / (float)intCount;
        }

        /// <summary>
        /// ����һ�������ƽ��ֵ
        /// </summary>
        /// <param name="arrNumber">�����������֣�Ĭ��-999F���������</param>
        /// <returns></returns>
        public static float GetAvgA(float[] arrNumber)
        {
            return GetAvgA(arrNumber, -999F);
        }


        /// <summary>
        /// ����һ�����ݵ�ƽ��ֵ
        /// </summary>
        /// <param name="arrNumbers">Ҫ������������(�ɱ����)</param>
        /// <returns>���ز�������ƽ��ֵ,��������������ݹ�����ʹ��GetAvgA����</returns>
        public static float GetAvg(params  float[] arrNumbers)
        {
            return GetAvgA(arrNumbers);
        }



        /// <summary>
        /// ����ֵ����
        /// </summary>
        /// <param name="Number">Ҫ����������</param>
        /// <param name="Space">�������</param>
        /// <returns>������ĸ�����</returns>
        public static float GetHzz(float Number, float Space)
        {
            float opNumber = Math.Abs(Number);           //���ڲ���
            int PartZhengShu;                           //��������
            float PartXiaoShu;                          //С������
            int intFlag = Number > 0 ? 1 : -1;          //��¼����
            if (Space != 1)
            {
                //���������಻Ϊ1,��ֱ�ӽ�Number/Space��1�ķ�������
                opNumber = (float)(opNumber / Space);
            }
            PartZhengShu = (int)opNumber;                       // ȡ����������
            PartXiaoShu = opNumber - (float)PartZhengShu;       // �õ�С������
            if (PartXiaoShu > 0.5F)                             //�ұ߲��ִ���0.5������������++
            {
                PartZhengShu++;
            }
            else if (PartXiaoShu == 0.5F)                   //==0.5,���⻯��λ
            {
                if (PartZhengShu % 2 == 1)
                {
                    PartZhengShu++;
                }
            }
            //��ԭ
            opNumber = intFlag * PartZhengShu * Space;
            return opNumber;
        }



        /// <summary>
        /// ����һ�����ݵı�׼ƫ��
        /// </summary>
        /// <param name="arrNumber">������������</param>
        /// <param name="inviade">���в���������ֵ</param>
        /// <returns>����һ�����ݵ�ƫ��ֵ((δ����))</returns>
        public static float GetWindage(float[] arrNumber,float inviade)
        {
            int intCount = 0;    //Ҫ����ƫ��ĳ�Ա����
            float Sum = 0F;                     //�ͣ����ڼ���ƽ��ֵ
            float Avg = 0F;                     //ƽ��ֵ
            float Windage = 0F;                 //�����������
           // if (intCount < 1) return 0F;
            

            //����ƽ��ֵ
            for (int i = 0; i < arrNumber.Length; i++)
            {
                if (arrNumber[i] != inviade)
                {
                    Sum += arrNumber[i];
                    intCount++;
                }
            }
            if (intCount == 1)
            {
                return 0F;
            }
            Avg = Sum / (float)intCount;
            //����ƫ��
            for (int i = 0; i < intCount; i++)
            {
                if (arrNumber[i] != inviade)
                {
                    Windage += (float)Math.Pow((arrNumber[i] - Avg), 2);
                }
            }
            Windage = Windage / (float)(intCount - 1);
            return (float)Math.Sqrt((double)Windage);
        }

        /// <summary>
        /// ����һ�����ݵı�׼ƫ��
        /// </summary>
        /// <param name="arrNumber">������������</param>
        /// <returns>����һ�����ݵ�ƫ��ֵ((δ����))</returns>
        public static float GetWindage(float[] arrNumber)
        {
            return GetWindage(arrNumber, -999F);
        }

        /// <summary>
        /// ����һ�����ݵı�׼ƫ��
        /// </summary>
        /// <param name="arrNumbers">��������(�ɱ����)</param>
        /// <returns>���ؼ������ֵı�׼ƫ��(δ����)��������ݸ���������ʹ��GetWindage����</returns>
        public static float GetWindageA(params float[] arrNumbers)
        {
            return GetWindage(arrNumbers);
        }

        /// <summary>
        /// ����������ֵ[(a-b)/a]
        /// </summary>
        /// <param name="a">�Ƚϲ���</param>
        /// <param name="b">���Ƚϲ���</param>
        /// <returns>���ض����������ٷֱ�[����:(a-b)/b *100],�������������������ȼ���</returns>
        public static float GetRelativeWuCha(float a, float b)
        {
            if (b == 0) return 99F;
            return (float)Math.Round(((a - b) / b ) * 100F, 2);
        }

        /// <summary>
        /// ����������ֵ(a-b)/b+r
        /// </summary>
        /// <param name="a">�Ƚϲ���</param>
        /// <param name="b">���Ƚϲ���</param>
        /// <param name="other">��׼��������</param>
        /// <returns>���ض����������ٷֱ�[����:(a-b)/b *100],�������������������ȼ���</returns>
        public static float GetRelativeWuCha(float a, float b, float other)
        {
            return GetRelativeWuCha(a, b) + other;
        }

        /// <summary>
        /// ��ȡָ��IB�����ĵ���ֵ
        /// </summary>
        /// <param name="xIb">��������Imax,1.0Ib</param>
        /// <param name="Current">��������1.5(6)</param>
        /// <returns></returns>
        public static float GetCurrentByIb(string xIb, string Current)
        {
            //if (Current.IndexOf("(") >= 0 && Current.IndexOf(")") >= 0)
            float _Ib = 0F;
            float _Imax = 0F;
            Regex _Reg = new Regex("(?<ib>[\\d\\.]+)\\((?<imax>[\\d\\.]+)\\)");
            Match _Match = _Reg.Match(Current);
            if (_Match.Groups["ib"].Value.Length < 1)
                return 0F;

            _Ib = float.Parse(_Match.Groups["ib"].Value);
            _Imax = float.Parse(_Match.Groups["imax"].Value);

            if (xIb.ToLower() == "imax")
                return _Imax;
            else if (xIb.ToLower().IndexOf("imax") >= 0 && xIb.ToLower().IndexOf("ib") == -1)
                return _Imax * float.Parse(xIb.ToLower().Replace("imax", ""));
            else if (xIb.ToLower() == "ib")
                return _Ib;
            else if (xIb.ToLower().IndexOf("ib") >= 0 && xIb.ToLower().IndexOf("imax") == -1)
                return _Ib * float.Parse(xIb.ToLower().Replace("ib", ""));
            else if (xIb.ToLower().IndexOf("(imax-ib)") >= 0)
                if (xIb.ToLower().IndexOf("/") >= 0)
                    return (_Imax - _Ib) / float.Parse(xIb.ToLower().Replace("(imax-ib)/", ""));
                else
                    return (_Imax - _Ib) * float.Parse(xIb.ToLower().Replace("(imax-ib)", ""));
            else
                return 0F;
        }

        /// <summary>
        /// ��ȡ�й����޹�����ֵ 
        /// </summary>
        /// <param name="ConstString">���� �й����޹���</param>
        /// <param name="YouGong">�Ƿ����й�</param>
        /// <returns>[�й����޹�]</returns>
        public static int GetBcs(string ConstString,bool YouGong)
        {
            ConstString = ConstString.Replace("��", "(").Replace("��", ")");

            if (ConstString.Trim().Length < 1)
            {
                //System.Windows.Forms.MessageBox.Show("û��¼�볣��");
                return 1;
            }

            string[] arTmp = ConstString.Trim().Replace(")", "").Split('(');

            if (arTmp.Length == 1)
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(arTmp[0]))
                    return int.Parse(arTmp[0]);
                else
                    return 1;
            }
            else
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(arTmp[0]) && CLDC_DataCore.Function.Number.IsNumeric(arTmp[1]))
                {
                    if (YouGong)
                        return int.Parse(arTmp[0]);
                    else
                        return int.Parse(arTmp[1]);
                }
                else
                    return 1;
            }
        }
        /// <summary>
        /// ��ȡ������������ֵ
        /// </summary>
        /// <param name="xIb">���������ַ���1.5Ib</param>
        /// <param name="Current">��������1.5(6)</param>
        /// <returns></returns>
        public static float getxIb(string xIb, string Current)
        {
            float _Ib = 0F;
            float _Imax = 0F;
            Regex _Reg = new Regex("(?<ib>[\\d\\.]+)\\((?<imax>[\\d\\.]+)\\)");
            Match _Match = _Reg.Match(Current);
            if (_Match.Groups["ib"].Value.Length < 1)
                return 0F;

            _Ib = float.Parse(_Match.Groups["ib"].Value);
            _Imax = float.Parse(_Match.Groups["imax"].Value);
            float _BeiShu = _Imax / _Ib;
            if (xIb.ToLower() == "imax")
                return _BeiShu;
            else if (xIb.ToLower().IndexOf("imax") >= 0 && xIb.ToLower().IndexOf("ib") == -1)
                return _BeiShu * float.Parse(xIb.ToLower().Replace("imax", ""));
            else if (xIb.ToLower() == "ib")
                return 1F;
            else if (xIb.ToLower().IndexOf("ib") >= 0 && xIb.ToLower().IndexOf("imax") == -1)
                return 1F * float.Parse(xIb.ToLower().Replace("ib", ""));
            else if (xIb.ToLower().IndexOf("(imax-ib)") >= 0)
                if (xIb.ToLower().IndexOf("/") >= 0)
                    return ((_Imax - _Ib) / float.Parse(xIb.ToLower().Replace("(imax-ib)/", ""))) / _Ib;
                else
                    return ((_Imax - _Ib) * float.Parse(xIb.ToLower().Replace("(imax-ib)", ""))) / _Ib;
            else
                return 1F;
        }
        /// <summary>
        /// ��ȡ����������ֵ
        /// </summary>
        /// <param name="Glys">��������1.0,0.5L</param>
        /// <returns></returns>
        public static float getGlysValue(string Glys)
        {
            try
            {
                if (!IsNumeric(Glys.Substring(Glys.Length - 1, 1)))
                    Glys = Glys.Substring(0, Glys.Length - 1);
                return float.Parse(Glys);
            }
            catch
            {
                return float.Parse(Glys.Substring(0, Glys.Length - 1));
            }
        }
        private static Regex IsNumeric_Reg = null;
        /// <summary>
        /// ����Ƿ�������
        /// </summary>
        /// <param name="sNumeric">Ҫ��֤���ַ���</param>
        /// <returns>��Y��N</returns>
        public static bool IsNumeric(string sNumeric)
        {
            if (sNumeric == null || sNumeric.Length == 0) return false;
            if (IsNumeric_Reg == null)
                IsNumeric_Reg = new Regex("^[\\+\\-]?[0-9]*\\.?[0-9]+$");
            return IsNumeric_Reg.Replace(sNumeric, "").Length == 0;
        }

        private static Regex IsIntNumeric_Reg = null;
        /// <summary>
        /// ����Ƿ�Ϊ�������֡���������
        /// </summary>
        /// <param name="sNumeric"></param>
        /// <returns></returns>
        public static bool IsIntNumber(string sNumeric)
        {
            if (sNumeric == null || sNumeric.Length == 0) return false;
            if (IsIntNumeric_Reg == null)
                IsIntNumeric_Reg = new Regex("-?[0-9]+");
            return IsIntNumeric_Reg.Replace(sNumeric, "").Length == 0;
        }

        /// <summary>
        /// ���صȼ��������±�0=�й���1=�޹�
        /// </summary>
        /// <param name="DjString">�ȼ��ַ���1.0S(2.0)</param>
        /// <returns></returns>
        public static string[] getDj(string DjString)
        {
            DjString = DjString.ToUpper().Replace("S", "");
            DjString = DjString.ToUpper().Replace("��", "(").Replace("��", ")").Replace(")","");
            string[] _Dj = DjString.Split('(');
            if (_Dj.Length == 1)
                return new string[] { float.Parse(_Dj[0]).ToString("F1"), float.Parse(_Dj[0]).ToString("F1") };
            else
                return new string[] { float.Parse(_Dj[0]).ToString("F1"), float.Parse(_Dj[1]).ToString("F1") };
        }
        /// <summary>
        /// ��������ʱ�䷵��ʱ��Ϊ(����)
        /// </summary>
        /// <param name="Clfs">������ʽ</param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="I">����</param>
        /// <param name="U">��ѹ</param>
        /// <param name="Glys">��������</param>
        /// <param name="DianLiang">��Ҫת���ĵ���</param>
        /// <returns></returns>
        public static float DLtoTime(CLDC_Comm.Enum.Cus_Clfs Clfs, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, float I, float U, string Glys, float DianLiang)
        {
            float _GlysValue = Function.Number.getGlysValue(Glys);
            float _CS1 = 1F;
            switch ((int)Clfs)
            {
                case 0:     //��������
                    {
                        _CS1 = 3F;
                        break;
                    }
                case 5:     //����
                    {
                        _CS1 = 1F;
                        break;
                    }
                default:
                    {
                        _CS1 = 1.732F;
                        break;
                    }
            }
            _CS1 = (int)Yj > 1 ? 1F : _CS1;     //����Ǻ�Ԫ����ԭֵ�����Ǻ�Ԫ����1��=1
            return DianLiang * 60000 / (_CS1 * U * I * _GlysValue);
        }


    


        /// <summary>
        /// ���1.5(6)�����Ĳ���
        /// </summary>
        /// <param name="str">Ҫ��ֵĶ���</param>
        /// <param name="bFirst">�Ƿ���ȡ��һ�����������ΪFalse��ȡ�ڶ�������</param>
        /// <returns>ָ��������</returns>

        public static long SplitKF(string str, bool bFirst)
        {
            string[] _Array = getDj(str);


            return bFirst ? long.Parse((float.Parse(_Array[0])).ToString("F0")) : long.Parse((float.Parse(_Array[1])).ToString("F0"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrList"></param>
        /// <param name="IsUp"></param>
        public static void PopDesc(ref long[] arrList, bool IsUp)
        {
            float[] fltArray = ConvertArray.ConvertLong2Float(arrList);
            PopDesc(ref fltArray, IsUp);
           //���������
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrList"></param>
        /// <param name="IsUP"></param>
        public static void PopDesc(ref int[] arrList,bool IsUP)
        {
            float[] fltArray = ConvertArray.ConvertInt2Float(arrList);
            PopDesc(ref fltArray, IsUP);
            int pos=0;
            foreach (float v in fltArray)
            {
                arrList[pos] = (int)v;
                pos++;
            }
            //Array.Copy(fltArray, arrList, fltArray.Length);
            // ���������
            
        }
        /// <summary>
        /// ð������
        /// </summary>
        /// <param name="arrList">Ҫ���������</param>
        /// <param name="IsUp">��/����</param>
        public static void PopDesc(ref float[] arrList, bool IsUp)
        {

            for (int i = 0; i < arrList.Length; i++)
            {
                for (int j = i; j < arrList.Length; j++)
                {
                    if (IsUp) {
                        if (arrList[i] > arrList[j])
                        {
                            float temp = arrList[i];
                            arrList[i] = arrList[j];
                            arrList[j] = temp;
                        } 
                    }
                    else
                    {
                        if (arrList[i] < arrList[j])
                        {
                            float temp = arrList[i];
                            arrList[i] = arrList[j];
                            arrList[j] = temp;
                        }
                    }
                }
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class Common
    {
        /// <summary>
        /// ��Bool�ν��ת��Ϊ�ϸ�/���ϸ�
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public static string ConverResult(bool Result)
        {
            return Result == true
                ? "�ϸ�"
                : "���ϸ�";
        }
        /// <summary>
        /// ���ϸ�/���ϸ�ת��Ϊboolֵ
        /// </summary>
        /// <param name="Result">Ҫת����ֵ</param>
        /// <param name="CTG_BuHeGe">�Ƚϵı�׼ֵ</param>
        /// <returns></returns>
        public static bool ConverResult(string Result, string CTG_BuHeGe)
        {
            return Result == CTG_BuHeGe
                ? false
                : true;
        }
        /// <summary>
        /// ���ϸ�/���ϸ�ת��Ϊboolֵ
        /// </summary>
        /// <param name="Result">�����ֵĬ����"���ϸ�"�ı��Ƚ�</param>
        /// <returns></returns>
        public static bool ConverResult(string Result)
        {
            return ConverResult(Result, "���ϸ�");
        }

        /// <summary>
        /// ��ȡ����ʵ����ָ������ֵ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyOrFieldName"></param>
        /// <returns></returns>
        public static object GetObjectValue(object obj, string PropertyOrFieldName)
        {
            Type _Type = obj.GetType();
            System.Reflection.PropertyInfo _Info = _Type.GetProperty(PropertyOrFieldName);
            System.Reflection.FieldInfo _Info2 = _Type.GetField(PropertyOrFieldName);
            if (_Info == null)
            {
                if (_Info2 == null) return null;
                return  _Info2.GetValue(obj);
            }
            else
            {
                return _Info.GetValue(obj, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyOrFieldName"></param>
        /// <returns></returns>
        public static object GetMethodValue(object obj, string PropertyOrFieldName)
        {
            Type _Type = obj.GetType();
            System.Reflection.MethodInfo _Info = _Type.GetMethod(PropertyOrFieldName);
            if (_Info == null)
            {
                return null;
            }
            else
            {
                return _Info.Name;
            }
         
        }
       

        /// <summary>
        /// ���ö���ָ������/�ֶε�ֵ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="PropertyOrFieldName"></param>
        /// <param name="objValue"></param>
        public static void SetObjValue(object obj, string PropertyOrFieldName, object objValue)
        {
            Type _Type = obj.GetType();
            System.Reflection.PropertyInfo _Info = _Type.GetProperty(PropertyOrFieldName);
            System.Reflection.FieldInfo _Info2 = _Type.GetField(PropertyOrFieldName);
            if (_Info == null)
            {
                if (_Info2 == null) return;
                _Info2.SetValue(obj, objValue);
            }
            else
            {
                _Info.SetValue(obj, objValue, null);
            }
        }
        /// <summary>
        /// ���һ�����������¼����ҹ���delegate
        /// </summary>
        /// <param name="objectHasEvents">���¼��Ķ���</param>
        public static void ClearAllEvents(object objectHasEvents)
        {
            if (objectHasEvents == null)
            {
                return;
            }

            EventInfo[] events = objectHasEvents.GetType().GetEvents(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (events == null || events.Length < 1)
            {
                return;
            }

            for (int i = 0; i < events.Length; i++)
            {
                try
                {
                    EventInfo ei = events[i];

                    /********************************************************
                     * class��ÿ��event����Ӧ��һ��ͬ����private��delegate��
                     * �ͳ�Ա��������������Reflector֤ʵ������Ϊprivate��
                     * Ա�����޷��ڻ����н����޸ģ�����Ϊ���ܹ��õ�base 
                     * class���������¼���Ҫ��EventInfo��DeclaringType����ȡ
                     * event��Ӧ�ĳ�Ա������FieldInfo�������޸�
                     ********************************************************/
                    FieldInfo fi =
                        ei.DeclaringType.GetField(ei.Name,
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.Instance);
                    if (fi != null)
                    {
                        // ��event��Ӧ���ֶ����ó�null����������йҹ��ڸ�event�ϵ�delegate
                        fi.SetValue(objectHasEvents, null);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// ��ǰ�Ƿ��������������
        /// </summary>
        /// <returns></returns>
        public static bool IsVSDevenv()
        {
            string strExName = Application.ExecutablePath;
            strExName = strExName.Substring(strExName.LastIndexOf('\\') + 1).ToLower();
            if (strExName == @"devenv.exe")
                return true;
            return false;
        }

        /// <summary>
        /// ��ʾ�ڸǲ�
        /// </summary>
        /// <param name="parent">���ڸ��������������,������Form����UserControl������</param>
        /// <param name="Notice">�ڸǲ���ʾ����</param>
        /// <param name="IsCover">��ʾ�ڸ�?</param>
        public static void DoCover(object parent, string Notice, bool IsCover)
        {
            CLDC_DataCore.Function.Waiting.DoCover(parent, Notice, IsCover);
        }

        /// <summary>
        /// ��ʾ�ڸǲ�
        /// </summary>
        /// <param name="parent">���ڸ�������������������Form����UserControl������</param>
        /// <param name="IsCover">��ʾ�ڸ�?</param>
        public static void DoCover(object parent, bool IsCover)
        {
            CLDC_DataCore.Function.Waiting.DoCover(parent, IsCover);
        }

        /// <summary>
        /// ���һ���ַ����Ƿ�Ϊ��
        /// </summary>
        /// <param name="str">Ҫ�����ַ���</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(string str)
        {
            str = str.Trim();
            if (str.Length == 0 || str == "" || str == null || str == string.Empty)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ��ȡָ�����ݵ�С��λ��
        /// </summary>
        /// <param name="strNumber">�����ַ���</param>
        /// <returns></returns>
        public static int GetPrecision(string strNumber)
        {
            if (!Function.Number.IsNumeric(strNumber))
            {
                return 0;
            }
            int hzPrecision = strNumber.ToString().LastIndexOf('.');
            if (hzPrecision == -1)
            {
                //û��С���㣬����0
                hzPrecision = 0;
            }
            else
            {
                //��С����
                hzPrecision = strNumber.ToString().Length - hzPrecision - 1;
            }
            return hzPrecision;

        }
        /// <summary>
        /// ��ȡָ�����ݵ�С��λ��
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GetPrecision(float number)
        {
            return GetPrecision(number.ToString());
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref string[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref bool[] array, bool value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref long[] array, long value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
           
        }
        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref float[] array, float value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
          
        }


        /// <summary>
        /// ͳ�Ƴ�������ΪTrue�ĸ���,���С�ڴ������ĸ�������Ϊtrue
        /// </summary>
        /// <param name="bResult"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public static bool GetResultCount(bool[] bResult, int iCount)
        {

            int Count = 0;
            for (int i = 0; i < bResult.Length; i++)
            {
                if (bResult[i])
                {
                    Count++;
                }
            }
            if (Count <= iCount)
                return true;
            else
                return false;
        }

        public static Single[] GetPhiGlys(Cus_Clfs Clfs, Cus_PowerFangXiang Glfx, Cus_PowerYuanJian Glyj, string strGlys, Cus_PowerPhase bln_Nxx)
        {
            Single sngAngle;
            Single[] sng_Phi = new Single[7];

            int intFX;
            int intClfs;
            int m_intClfs;
            Single sngPhiTmp;


            intClfs = (int)Clfs;
            m_intClfs = intClfs;
            intFX = (int)Glfx;



            if (intClfs == 0)
            {
                if (intFX == 1 || intFX == 2)
                    intClfs = 0;
                else
                    intClfs = 1;
            }
            else if (intClfs == 1)
            {
                if (intFX == 1 || intFX == 2)
                    intClfs = 2;
                else
                    intClfs = 3;
            }
            else if (intClfs == 5)
            {
                if (intFX == 1 || intFX == 2)
                    intClfs = 0;
                else
                    intClfs = 1;
            }


            //��ѹ������λ
            sngAngle = GetGlysAngle(intClfs, strGlys);
            sngAngle = (int)sngAngle;
            sng_Phi[6] = sngAngle;
            if (m_intClfs == 0)
            {
                //----------------�������߽Ƕ�------------------------
                sng_Phi[0] = 0;           //Ua
                sng_Phi[1] = 240;         //Ub
                sng_Phi[2] = 120;         //Uc
                sng_Phi[3] = sng_Phi[0] - sngAngle;
                sng_Phi[4] = sng_Phi[1] - sngAngle;
                sng_Phi[5] = sng_Phi[2] - sngAngle;

                if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                if (sng_Phi[3] < 0) sng_Phi[3] = sng_Phi[3] + 360;
                if (sng_Phi[4] > 360) sng_Phi[4] = sng_Phi[4] - 360;
                if (sng_Phi[4] < 0) sng_Phi[4] = sng_Phi[4] + 360;
                if (sng_Phi[5] > 360) sng_Phi[5] = sng_Phi[5] - 360;
                if (sng_Phi[5] < 0) sng_Phi[5] = sng_Phi[5] + 360;



                //����Ƿ���Ҫ�������Ƕȷ�����
                if (strGlys.IndexOf('-') == -1)
                {
                    if (intFX == 2 || intFX == 4)
                    {
                        sng_Phi[3] = sng_Phi[3] + 180;
                        sng_Phi[4] = sng_Phi[4] + 180;
                        sng_Phi[5] = sng_Phi[5] + 180;

                        if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                        if (sng_Phi[4] > 360) sng_Phi[4] = sng_Phi[4] - 360;
                        if (sng_Phi[5] > 360) sng_Phi[5] = sng_Phi[5] - 360;
                    }
                }

                if (bln_Nxx == Cus_PowerPhase.����������)
                {
                    sngPhiTmp = sng_Phi[0];
                    sng_Phi[0] = sng_Phi[1];
                    sng_Phi[1] = sngPhiTmp;                    
                }
                else if (bln_Nxx == Cus_PowerPhase.��ѹ������)
                {
                    sngPhiTmp = sng_Phi[3];
                    sng_Phi[3] = sng_Phi[4];
                    sng_Phi[4] = sngPhiTmp;
                }

            }
            else if (m_intClfs == 1)
            {
                //---------------�������߽Ƕ�--------------------

                sng_Phi[0] = 0;           //Ua
                sng_Phi[2] = 120;         //Uc
                sng_Phi[3] = sng_Phi[0] - sngAngle;
                sng_Phi[5] = sng_Phi[2] - sngAngle;
                sng_Phi[0] = 30;
                sng_Phi[2] = 90;

                if (Glyj != Cus_PowerYuanJian.H)
                {
                    sng_Phi[3] = sng_Phi[0] - sngAngle;
                    sng_Phi[5] = sng_Phi[2] - sngAngle;
                }

                if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                if (sng_Phi[3] < 0) sng_Phi[3] = sng_Phi[3] + 360;
                if (sng_Phi[5] > 360) sng_Phi[5] = sng_Phi[5] - 360;
                if (sng_Phi[5] < 0) sng_Phi[5] = sng_Phi[5] + 360;



                //����Ƿ���Ҫ�������Ƕȷ�����
                if (strGlys.IndexOf('-') == -1)
                {
                    if (intFX == 2 || intFX == 4)
                    {
                        sng_Phi[3] = sng_Phi[3] + 180;
                        sng_Phi[5] = sng_Phi[5] + 180;

                        if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                        if (sng_Phi[5] > 360) sng_Phi[5] = sng_Phi[5] - 360;
                    }
                }
            }
            else if (m_intClfs == 2)
            {
                //-----------��Ԫ������90���޹���Ƕ�------------------
                sng_Phi[0] = 0;           //Ua
                sng_Phi[1] = 240;         //Ub
                sng_Phi[2] = 120;         //Uc
                sng_Phi[3] = sng_Phi[0] - sngAngle;
                sng_Phi[4] = sng_Phi[1] - sngAngle;
                sng_Phi[5] = sng_Phi[2] - sngAngle;

                //sngPhiUab = 30;          //Uab
                //sngPhiUbc = 270;         //Ubc
                //sngPhiUca = 150;         //Uca

                if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                if (sng_Phi[3] < 0) sng_Phi[3] = sng_Phi[3] + 360;
                if (sng_Phi[4] > 360) sng_Phi[4] = sng_Phi[4] - 360;
                if (sng_Phi[4] < 0) sng_Phi[4] = sng_Phi[4] + 360;
                if (sng_Phi[5] > 360) sng_Phi[5] = sng_Phi[5] - 360;
                if (sng_Phi[5] < 0) sng_Phi[5] = sng_Phi[5] + 360;
            }
            else
            {
                //-----------�����------------------
                sng_Phi[0] = 0;         //Ua
                sng_Phi[3] = sng_Phi[0] - sngAngle;

                if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                if (sng_Phi[3] < 0) sng_Phi[3] = sng_Phi[3] + 360;

                //����Ƿ���Ҫ�������Ƕȷ�����
                if (strGlys.IndexOf('-') == -1)
                {
                    if (intFX == 2 || intFX == 4)
                    {
                        sng_Phi[3] = sng_Phi[3] + 180;
                        if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                    }
                }
            }
            return sng_Phi;
        }

        private static Single GetGlysAngle(int intClfs, string strGlys)
        {
            Double dbl_Pha = 0;
            String sLC;
            Double dbl_Xs;
            Single PI = 3.14159f;
            strGlys = strGlys.Trim();
            sLC = strGlys.Substring(strGlys.Length - 1, 1);
            if (sLC.ToUpper() == "C" || sLC.ToUpper() == "L")
                dbl_Xs = Double.Parse(strGlys.Substring(0, strGlys.Length - 1));
            else
                dbl_Xs = double.Parse(strGlys);


            if (intClfs == 0 || intClfs == 2)      //�й�
            {
                if (dbl_Xs > 0 && dbl_Xs <= 1)
                    dbl_Pha = Math.Atan(Math.Sqrt(1 - dbl_Xs * dbl_Xs) / dbl_Xs);
                else if (dbl_Xs < 0 && dbl_Xs >= -1)
                    dbl_Pha = Math.Atan(Math.Sqrt(1 - dbl_Xs * dbl_Xs) / dbl_Xs) + PI;
                else if (dbl_Xs == 0)
                    dbl_Pha = PI / 2;
            }
            else
            {
                if (dbl_Xs > -1 && dbl_Xs < 1)
                    dbl_Pha = Math.Atan(dbl_Xs / Math.Sqrt(1 - dbl_Xs * dbl_Xs));
                else if (dbl_Xs == -1)
                    dbl_Pha = -PI * 0.5f;
                else if (dbl_Xs == 1)
                    dbl_Pha = PI * 0.5f;
            }
            dbl_Pha = dbl_Pha * 180 / PI;


            if (intClfs == 2 && sLC.ToUpper() == "C")
                dbl_Pha = 360 - dbl_Pha;
            else if ((intClfs == 1 || intClfs == 3) && sLC.ToUpper() == "C")
                dbl_Pha = 360 - dbl_Pha - 180;
            else if (sLC.ToUpper() == "C")
                dbl_Pha = 360 - dbl_Pha;
            

            if (dbl_Pha < 0) dbl_Pha = 360 + dbl_Pha;
            if (dbl_Pha >= 360) dbl_Pha = dbl_Pha - (dbl_Pha / 360) * 360;
            dbl_Pha = Math.Round(dbl_Pha, 4);
            return (Single)dbl_Pha;
        }
        /// <summary>
        /// ����Ƕ� �������
        /// </summary>
        /// <param name="int_Clfs">������ʽ</param>
        /// <param name="str_Glys">��������</param>
        /// <param name="int_Element">������</param>
        /// <param name="bln_NXX">������</param>
        /// <returns>�������飬����Ԫ��Ϊ����ABC���ѹ�����Ƕ�</returns>
        public static Single[] GetPhiGlys2(int int_Clfs, string str_Glys, int int_Element, bool bln_NXX)
        {
            string str_CL = str_Glys.ToUpper().Substring(str_Glys.Length - 1, 1);
            Double dbl_XS = 0;
            if (str_CL == "C" || str_CL == "L")
                dbl_XS = Convert.ToDouble(str_Glys.Substring(0, str_Glys.Length - 1));
            else
                dbl_XS = Convert.ToDouble(str_Glys);
            Double dbl_Phase;

            if (int_Clfs == 1 || int_Clfs == 3 || int_Clfs == 6)
                dbl_Phase = Math.Asin(Math.Abs(dbl_XS));                              //�޹�����
            else
                dbl_Phase = Math.Acos(Math.Abs(dbl_XS));                              //�й�����

            dbl_Phase = dbl_Phase * 180 / Math.PI;      //�ǶȻ���
            if (dbl_XS < 0) dbl_Phase = 180 + dbl_Phase;         //����
            if (str_CL == "C") dbl_Phase = 360 - dbl_Phase;
            if (dbl_Phase < 0) dbl_Phase = 360 + dbl_Phase;

            Single sng_UIPhi = Convert.ToSingle(dbl_Phase);
            Single[] sng_Phi = new Single[6];

            if (bln_NXX)
            {
                sng_Phi[0] = 0;         //Ua
                sng_Phi[1] = 240;       //Ub
                sng_Phi[2] = 120;       //Uc
            }
            else
            {
                sng_Phi[0] = 0;         //Ua
                sng_Phi[1] = 120;       //Ub
                sng_Phi[2] = 240;       //Uc
            }


            sng_Phi[3] = sng_Phi[0] + sng_UIPhi;       //Ia
            sng_Phi[4] = sng_Phi[1] + sng_UIPhi;       //Ib
            sng_Phi[5] = sng_Phi[2] + sng_UIPhi;       //Ic

            if (int_Clfs == 2 || int_Clfs == 3)
            {
                sng_Phi[2] += 60;       //Uc
                sng_Phi[3] += 30;       //Ia
                sng_Phi[4] += 30;       //Ib
                sng_Phi[5] += 30;       //Ic
            }

            sng_Phi[3] %= 360;       //Ia
            sng_Phi[4] %= 360;       //Ib
            sng_Phi[5] %= 360;



            //0, 240, 120, 0, 240, 120
            //0, 240, 120, 180, 60, 300
            //0, 240, 120, 30, 270, 150
            //0, 240, 120, 210, 90, 330,

            return sng_Phi;
        }

        //public static Single[] GetPhiGlys(int int_Clfs,

        /// <summary>
        /// ����Ƕ� �������
        /// 
        /// </summary>
        /// <param name="int_Clfs">������ʽ</param>
        /// <param name="str_Glys">��������</param>
        /// <param name="int_Element">������</param>
        /// <param name="bln_NXX">������</param>
        /// <param name="isYouGong">�С��޹�</param>
        /// <returns>�������飬����Ԫ��Ϊ����ABC���ѹ�����Ƕ�</returns>
        public static Single[] GetPhiGlys(int int_Clfs, string str_Glys, int int_Element, Cus_PowerPhase bln_NXX,bool isYouGong)
        {
            /*
            /*   ���������й� = 0,
         ���������޹� = 1,
         ���������й� = 2,
         ���������޹� = 3,
         ��Ԫ������90 = 4,
         ��Ԫ������60 = 5,
         ��Ԫ������90 = 6,
             
        ��������=0,
        ��������=1,
        ��Ԫ������90=2,
        ��Ԫ������60=3,
        ��Ԫ������90=4,
        ����=5
             
             */
            string str_CL = str_Glys.ToUpper().Substring(str_Glys.Length - 1, 1);
            Double dbl_XS = 0;
            if (str_CL == "C" || str_CL == "L")
                dbl_XS = Convert.ToDouble(str_Glys.Substring(0, str_Glys.Length - 1));
            else
                dbl_XS = Convert.ToDouble(str_Glys);
            Double dbl_Phase;

            if (!isYouGong)
                dbl_Phase = Math.Asin(Math.Abs(dbl_XS));                              //�޹�����
            else
                dbl_Phase = Math.Acos(Math.Abs(dbl_XS));                              //�й�����

            dbl_Phase = dbl_Phase * 180 / Math.PI;      //�ǶȻ���
            if (dbl_XS < 0) dbl_Phase = 180 + dbl_Phase;         //����
            if (str_CL == "C") dbl_Phase = 360 - dbl_Phase;
            if (dbl_Phase < 0) dbl_Phase = 360 + dbl_Phase;

            Single sng_UIPhi = Convert.ToSingle(dbl_Phase);
            Single[] sng_Phi = new Single[6];

            if (isYouGong)
            {
                switch (str_Glys.Trim().ToUpper())
                {
                    case "0.5L":
                        sng_UIPhi = 60F;
                        break;
                    case "0.8C":
                        sng_UIPhi = 330F;
                        break;
                    case "0.5C":
                        sng_UIPhi = 300F;
                        break;
                    case "0.25L":
                        sng_UIPhi = 75.5F;
                        break;

                    default: break;

                }
                sng_Phi[0] = 0;         //Ua
                sng_Phi[1] = 0;       //Ub
                sng_Phi[2] = 0;       //Uc


            }
            else
            {
                switch (str_Glys.Trim().ToUpper())
                {
                    case "1.0":
                        sng_UIPhi = 90F;
                        break;
                    case "0.5L":
                        sng_UIPhi = 30F;
                        break;
                    case "0.8C":
                        sng_UIPhi = 120F;
                        break;
                    case "0.5C":
                        sng_UIPhi = 150F;
                        break;
                    case "0.25L":
                        sng_UIPhi = 14.5F;
                        break;

                    default: break;

                }
                sng_Phi[0] = 90;         //Ua
                sng_Phi[1] = 90;       //Ub
                sng_Phi[2] = 90;       //Uc

            }



            //if (bln_NXX)
            //{
                //sng_Phi[0] = 0;         //Ua
                //sng_Phi[1] = 240;       //Ub
                //sng_Phi[2] = 120;       //Uc

                //sng_Phi[0] = 0;         //Ua
                //sng_Phi[1] = 0;       //Ub
                //sng_Phi[2] = 0;       //Uc

            //}
            //else
            //{
            //}


            sng_Phi[3] = sng_Phi[0] + sng_UIPhi;       //Ia
            sng_Phi[4] = sng_Phi[1] + sng_UIPhi;       //Ib
            sng_Phi[5] = sng_Phi[2] + sng_UIPhi;       //Ic

            //if (int_Clfs == 2 || int_Clfs == 3)
            //{
            //    sng_Phi[2] += 60;       //Uc
            //    sng_Phi[3] += 30;       //Ia
            //    sng_Phi[4] += 30;       //Ib
            //    sng_Phi[5] += 30;       //Ic
            //}

            sng_Phi[3] %= 360;       //Ia
            sng_Phi[4] %= 360;       //Ib
            sng_Phi[5] %= 360;



            //0, 240, 120, 0, 240, 120
            //0, 240, 120, 180, 60, 300
            //0, 240, 120, 30, 270, 150
            //0, 240, 120, 210, 90, 330,

            return sng_Phi;
        }

        /// <summary>
        /// ��ȡ8�ֽ�ΨһID��4�ֽ�ʱ���+3�ֽ�����MAC+1�ֽ���������
        /// </summary>
        /// <param name="id">�������У�ֻȡ1�ֽ�</param>
        /// <returns>8�ֽ�ΨһID</returns>
        public static long GetUniquenessID8(int id)
        {
            string strMac = "";
            long lngMac = Net.GetMac(out strMac);

            string s = string.Format("{0:X8}{1:X6}{2:X2}", DateTimes.GetTimeStamp(), ((int)(lngMac)) & 0x00FFFFFF, ((byte)id));
            long n = Convert.ToInt64(s, 16);
            
            return n;
        }
        /// <summary>
        /// ��ȡ12�ֽ�ΨһID��4�ֽ�ʱ���+4�ֽ�����MAC+2�ֽڽ���PID+2�ֽ���������
        /// </summary>
        /// <param name="id">�������У�ֻȡ2�ֽ�</param>
        /// <returns>12�ֽ�ΨһID</returns>
        public static long GetUniquenessID12(int id)
        {
            string strMac = "";
            long lngMac = Net.GetMac(out strMac);
            Process curPro = Process.GetCurrentProcess();
            string s = string.Format("{0:X8}{1:X8}{2:X4}{3:X4}", DateTimes.GetTimeStamp(), (int)(lngMac), (short)curPro.Id, ((short)id));
            long n = Convert.ToInt64(s, 16);

            return n;
        }
        /// <summary>
        /// ��ʮ������ת���ɶ�����
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string ConvertTo2From16(string strData)
        {
            string strReturnData = "";
            for (int intInc = strData.Length - 1; intInc >= 0; intInc--)
            {
                byte byt_Tmp = Convert.ToByte(strData.Substring(intInc, 1), 16);
                strReturnData += Convert.ToString(byt_Tmp, 2).PadLeft(4, '0');
            }
            return strReturnData;
        }


        /// <summary>
        /// �ַ���ת10����  ������ͨ��ȡ�Ľ��ת��   ���磺00050000ת����500
        /// </summary>
        /// <param name="strHex"></param>
        /// <returns></returns>
        public static string[] StringConverToDecima(string[] strData)
        {
            string[] strDecimalism = new string[strData.Length];
            for (int i = 0; i < strData.Length; i++)
            {
                if (!string.IsNullOrEmpty(strData[i]))
                {
                    strDecimalism[i] = (Convert.ToInt32(strData[i]) / 100).ToString();
                }
                else
                {
                    strDecimalism[i] = "";
                }
            }
            return strDecimalism;
        }


        

    }

}

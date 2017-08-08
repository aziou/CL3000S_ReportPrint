using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 提供转换数组的函数
    /// </summary>
    public class ConvertArray
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static long[] ConvertInt2Long(int[] arrInput)
        {
            Converter<int, long> myCon = new Converter<int, long>(CInt2Long);
            long[] _fl = Array.ConvertAll<int, long>(arrInput, myCon);
            return _fl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intIn"></param>
        /// <returns></returns>
        private static long CInt2Long(int intIn)
        {
            try
            {
                return long.Parse(intIn.ToString());
            }
            catch 
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static float[] ConvertInt2Float(int[] arrInput)
        {
            Converter<int, float> myCon = new Converter<int, float>(Cint2Float);
            float[] _fl = Array.ConvertAll<int, float>(arrInput, myCon);
            return _fl;
        }
        private static float Cint2Float(int lngIn)
        {
            try
            {
                return float.Parse(lngIn.ToString());
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static float[] ConvertLong2Float(long[] arrInput)
        {
            Converter<long, float> myCon = new Converter<long, float>(CLong2Float);
            float[] _fl = Array.ConvertAll<long, float>(arrInput, myCon);
            return _fl;
        }
        private static float CLong2Float(long lngIn)
        {
            try
            {
                return float.Parse(lngIn.ToString());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Int数组---->字符串数组
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static string[] ConvertInt2String(int[] arrInput)
        {
            Converter<int, string> myCon = new Converter<int, string>(CInt2string);
            string[] _fl = Array.ConvertAll<int, string>(arrInput, myCon);
            return _fl;

        }

        private static string CInt2string(int str)
        {
            try
            {
                return Convert.ToString(str);
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Int数组---->字符串数组
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static int[] ConvertString2Int(string[] arrInput)
        {
            Converter<string, int> myCon = new Converter<string, int>(Cstring2Int);
            int[] _fl = Array.ConvertAll<string, int>(arrInput, myCon);
            return _fl;

        }

        private static int Cstring2Int(string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return -999;
            }
        }
        /// <summary>
        /// Object类型数组----->字符串
        /// </summary>
        /// <param name="arrInput">要转换的数组</param>
        /// <returns></returns>
        public static string[] ConvertObject2String(object[] arrInput)
        {
            Converter<object, string> myCon = new Converter<object, string>(Cobj2string);
            string[] _fl = Array.ConvertAll<object, string>(arrInput, myCon);
            return _fl;

        }

        private static string Cobj2string(object str)
        {
            try
            {
                return Convert.ToString(str);
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 将变体类型转换为Float
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static float[] ConvertObj2Float(object[] arrInput)
        {
            Converter<object, float> myCon = new Converter<object, float>(Cobj2float);
            float[] _fl = Array.ConvertAll<object, float>(arrInput, myCon);
            return _fl;
        }

        private static float Cobj2float(object str)
        {
            try
            {
                string strValue = str.ToString();
                if (strValue != "")
                    return Convert.ToSingle(strValue);
                else
                    return -999F;
            }
            catch
            {
                return -999F;
            }
        }




        /// <summary>
        /// 字符串数组---->float;
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        /// 


        public static float[] ConvertStr2Float(string[] arrInput)
        {
            Converter<string, float> myCon = new Converter<string, float>(Cstr2float);
            float[] _fl = Array.ConvertAll<string, float>(arrInput, myCon);
            return _fl;
        }

        private static float Cstr2float(string str)
        {
            try
            {
                return Convert.ToSingle(str);
            }
            catch
            {
                return -999F;
            }
        }
        public static string[] ConvertFloat2Str(float[] arrInput)
        {
            Converter<float, string> myCon = new Converter<float, string>(Cfloat2str);
            string[] _fl = Array.ConvertAll<float, string>(arrInput, myCon);
            return _fl;
        }
        private static string Cfloat2str(float str)
        {
            try
            {
                return str.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 将byte[]装换位字符串，fjk
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static string ConvertByte2String(byte[] arrInput)
        {
            if (arrInput == null)
            {
                return "";
            }
            return BitConverter.ToString(arrInput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="int_Len">返回数组长度</param>
        /// <param name="str_Value"></param>
        /// <param name="bln_Reverse">true:反转 false:不反转</param>
        /// <returns></returns>
        public static byte[] GetBytesArry(int int_Len, string str_Value, bool bln_Reverse)
        {
            byte[] byt_Data = new byte[int_Len];
            string str_Tmp = str_Value;
            if (str_Value.Length > int_Len * 2)
                str_Tmp = str_Value.Substring(str_Value.Length - int_Len * 2);
            else if (str_Value.Length < int_Len * 2)
                str_Tmp = str_Value.PadLeft(int_Len * 2 - str_Value.Length, '0');
            if (bln_Reverse)
            {
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                    byt_Data[int_Len - 1 - int_Inc] = Convert.ToByte(str_Tmp.Substring(int_Inc * 2, 2), 16);
            }
            else
            {
                for (int int_Inc = 0; int_Inc < int_Len; int_Inc++)
                    byt_Data[int_Inc] = Convert.ToByte(str_Tmp.Substring(int_Inc * 2, 2), 16);
            }
            return byt_Data;
        }
    }
}

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
        /// 把Bool形结果转换为合格/不合格
        /// </summary>
        /// <param name="Result"></param>
        /// <returns></returns>
        public static string ConverResult(bool Result)
        {
            return Result == true
                ? "合格"
                : "不合格";
        }
        /// <summary>
        /// 将合格/不合格转换为bool值
        /// </summary>
        /// <param name="Result">要转换的值</param>
        /// <param name="CTG_BuHeGe">比较的标准值</param>
        /// <returns></returns>
        public static bool ConverResult(string Result, string CTG_BuHeGe)
        {
            return Result == CTG_BuHeGe
                ? false
                : true;
        }
        /// <summary>
        /// 将合格/不合格转换为bool值
        /// </summary>
        /// <param name="Result">传入的值默认与"不合格"文本比较</param>
        /// <returns></returns>
        public static bool ConverResult(string Result)
        {
            return ConverResult(Result, "不合格");
        }

        /// <summary>
        /// 获取对象实例的指定属性值
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
        /// 设置对象指定属性/字段的值
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
        /// 清除一个对象所有事件所挂钩的delegate
        /// </summary>
        /// <param name="objectHasEvents">有事件的对象</param>
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
                     * class的每个event都对应了一个同名的private的delegate类
                     * 型成员变量（这点可以用Reflector证实）。因为private成
                     * 员变量无法在基类中进行修改，所以为了能够拿到base 
                     * class中声明的事件，要从EventInfo的DeclaringType来获取
                     * event对应的成员变量的FieldInfo并进行修改
                     ********************************************************/
                    FieldInfo fi =
                        ei.DeclaringType.GetField(ei.Name,
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.Instance);
                    if (fi != null)
                    {
                        // 将event对应的字段设置成null即可清除所有挂钩在该event上的delegate
                        fi.SetValue(objectHasEvents, null);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 当前是否在设计器中运行
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
        /// 显示遮盖层
        /// </summary>
        /// <param name="parent">被遮盖区域的容器对象,可以是Form或者UserControl的子类</param>
        /// <param name="Notice">遮盖层提示文字</param>
        /// <param name="IsCover">显示遮盖?</param>
        public static void DoCover(object parent, string Notice, bool IsCover)
        {
            CLDC_DataCore.Function.Waiting.DoCover(parent, Notice, IsCover);
        }

        /// <summary>
        /// 显示遮盖层
        /// </summary>
        /// <param name="parent">被遮盖区域的容器对象可以是Form或者UserControl的子类</param>
        /// <param name="IsCover">显示遮盖?</param>
        public static void DoCover(object parent, bool IsCover)
        {
            CLDC_DataCore.Function.Waiting.DoCover(parent, IsCover);
        }

        /// <summary>
        /// 检测一个字符串是否为空
        /// </summary>
        /// <param name="str">要检测的字符串</param>
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
        /// 获取指定数据的小数位数
        /// </summary>
        /// <param name="strNumber">数字字符串</param>
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
                //没有小数点，返回0
                hzPrecision = 0;
            }
            else
            {
                //有小数点
                hzPrecision = strNumber.ToString().Length - hzPrecision - 1;
            }
            return hzPrecision;

        }
        /// <summary>
        /// 获取指定数据的小数位数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GetPrecision(float number)
        {
            return GetPrecision(number.ToString());
        }

        /// <summary>
        /// 初始化布尔数组
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref string[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        /// <summary>
        /// 初始化布尔数组
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref bool[] array, bool value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        /// <summary>
        /// 初始化整型数组
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        /// <summary>
        /// 初始化长整型数组
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref long[] array, long value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
           
        }
        /// <summary>
        /// 初始化浮点型数据
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Memset(ref float[] array, float value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
          
        }


        /// <summary>
        /// 统计出数据中为True的个数,如果小于传进来的个数，则为true
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


            //电压电流相位
            sngAngle = GetGlysAngle(intClfs, strGlys);
            sngAngle = (int)sngAngle;
            sng_Phi[6] = sngAngle;
            if (m_intClfs == 0)
            {
                //----------------三相四线角度------------------------
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



                //如果是反向要将电流角度反过来
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

                if (bln_Nxx == Cus_PowerPhase.电流逆相序)
                {
                    sngPhiTmp = sng_Phi[0];
                    sng_Phi[0] = sng_Phi[1];
                    sng_Phi[1] = sngPhiTmp;                    
                }
                else if (bln_Nxx == Cus_PowerPhase.电压逆相序)
                {
                    sngPhiTmp = sng_Phi[3];
                    sng_Phi[3] = sng_Phi[4];
                    sng_Phi[4] = sngPhiTmp;
                }

            }
            else if (m_intClfs == 1)
            {
                //---------------三相三线角度--------------------

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



                //如果是反向要将电流角度反过来
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
                //-----------三元件跨相90°无功表角度------------------
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
                //-----------单相表------------------
                sng_Phi[0] = 0;         //Ua
                sng_Phi[3] = sng_Phi[0] - sngAngle;

                if (sng_Phi[3] > 360) sng_Phi[3] = sng_Phi[3] - 360;
                if (sng_Phi[3] < 0) sng_Phi[3] = sng_Phi[3] + 360;

                //如果是反向要将电流角度反过来
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


            if (intClfs == 0 || intClfs == 2)      //有功
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
        /// 计算角度 分相计算
        /// </summary>
        /// <param name="int_Clfs">测量方式</param>
        /// <param name="str_Glys">功率因数</param>
        /// <param name="int_Element">逆相序</param>
        /// <param name="bln_NXX">逆相序</param>
        /// <returns>返回数组，数组元素为各相ABC相电压电流角度</returns>
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
                dbl_Phase = Math.Asin(Math.Abs(dbl_XS));                              //无功计算
            else
                dbl_Phase = Math.Acos(Math.Abs(dbl_XS));                              //有功计算

            dbl_Phase = dbl_Phase * 180 / Math.PI;      //角度换算
            if (dbl_XS < 0) dbl_Phase = 180 + dbl_Phase;         //反向
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
        /// 计算角度 分相计算
        /// 
        /// </summary>
        /// <param name="int_Clfs">测量方式</param>
        /// <param name="str_Glys">功率因数</param>
        /// <param name="int_Element">逆相序</param>
        /// <param name="bln_NXX">逆相序</param>
        /// <param name="isYouGong">有、无功</param>
        /// <returns>返回数组，数组元素为各相ABC相电压电流角度</returns>
        public static Single[] GetPhiGlys(int int_Clfs, string str_Glys, int int_Element, Cus_PowerPhase bln_NXX,bool isYouGong)
        {
            /*
            /*   三相四线有功 = 0,
         三相四线无功 = 1,
         三相三线有功 = 2,
         三相三线无功 = 3,
         二元件跨相90 = 4,
         二元件跨相60 = 5,
         三元件跨相90 = 6,
             
        三相四线=0,
        三相三线=1,
        二元件跨相90=2,
        二元件跨相60=3,
        三元件跨相90=4,
        单相=5
             
             */
            string str_CL = str_Glys.ToUpper().Substring(str_Glys.Length - 1, 1);
            Double dbl_XS = 0;
            if (str_CL == "C" || str_CL == "L")
                dbl_XS = Convert.ToDouble(str_Glys.Substring(0, str_Glys.Length - 1));
            else
                dbl_XS = Convert.ToDouble(str_Glys);
            Double dbl_Phase;

            if (!isYouGong)
                dbl_Phase = Math.Asin(Math.Abs(dbl_XS));                              //无功计算
            else
                dbl_Phase = Math.Acos(Math.Abs(dbl_XS));                              //有功计算

            dbl_Phase = dbl_Phase * 180 / Math.PI;      //角度换算
            if (dbl_XS < 0) dbl_Phase = 180 + dbl_Phase;         //反向
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
        /// 获取8字节唯一ID：4字节时间戳+3字节主机MAC+1字节自增序列
        /// </summary>
        /// <param name="id">自增序列，只取1字节</param>
        /// <returns>8字节唯一ID</returns>
        public static long GetUniquenessID8(int id)
        {
            string strMac = "";
            long lngMac = Net.GetMac(out strMac);

            string s = string.Format("{0:X8}{1:X6}{2:X2}", DateTimes.GetTimeStamp(), ((int)(lngMac)) & 0x00FFFFFF, ((byte)id));
            long n = Convert.ToInt64(s, 16);
            
            return n;
        }
        /// <summary>
        /// 获取12字节唯一ID：4字节时间戳+4字节主机MAC+2字节进程PID+2字节自增序列
        /// </summary>
        /// <param name="id">自增序列，只取2字节</param>
        /// <returns>12字节唯一ID</returns>
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
        /// 从十六进制转换成二进制
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
        /// 字符串转10进制  用于普通读取的金额转换   例如：00050000转换后500
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

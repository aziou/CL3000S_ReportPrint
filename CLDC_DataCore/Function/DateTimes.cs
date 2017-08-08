using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Const;
namespace CLDC_DataCore.Function
{
    /// <summary>
    /// ʱ����ز���
    /// </summary>
    public class DateTimes
    {
        /// <summary>
        /// �߳�����ʱ
        /// </summary>
        /// <param name="MSec">��λ��MS</param>
        public static void Delay(int MSec)
        {
            DateTime startTime = DateTime.Now;
            long pastTime = 0;
            MSec *= 10000;
            while (pastTime < MSec)
            {
                pastTime = DateTime.Now.Ticks - startTime.Ticks;
                //if (GlobalUnit.ForceVerifyStop)
                //    break;
                System.Threading.Thread.Sleep(GlobalUnit.g_ThreadWaitTime);

            }

        }

        /// <summary>
        /// ��ʽ���ַ���Ϊ����
        /// </summary>
        /// <param name="FormatDate">Ҫ��ʽ�����ַ���,��:20090909000000</param>
        /// <returns></returns>
        public static DateTime FormatStringToDateTime(string FormatDate)
        {
            string _year;
            string _month;
            string _day;
            string _hour;
            string _minute;
            string _second;

            if (FormatDate.Length == 12)
            {
                _year = DateTime.Now.Year.ToString().Substring(0, 2) + FormatDate.Substring(0, 2);
                _month = FormatDate.Substring(2, 2);
                _day = FormatDate.Substring(4, 2);
                _hour = FormatDate.Substring(6, 2);
                _minute = FormatDate.Substring(8, 2);
                _second = FormatDate.Substring(10, 2);
                return DateTime.Parse(string.Format("{0}-{1}-{2} {3}:{4}:{5}", _year, _month, _day, _hour, _minute, _second));
            }
            else if (FormatDate.Length == 14)
            {
                _year = FormatDate.Substring(0, 4);
                _month = FormatDate.Substring(4, 2);
                _day = FormatDate.Substring(6, 2);
                _hour = FormatDate.Substring(8, 2);
                _minute = FormatDate.Substring(10, 2);
                _second = FormatDate.Substring(12, 2);
                _month = (int.Parse(_month) % 12).ToString();
                if (_month == "0") _month = "12";
                return DateTime.Parse(string.Format("{0}-{1}-{2} {3}:{4}:{5}", _year, _month, _day, _hour, _minute, _second));

            }
            else
            {
                return DateTime.Now; 
            }
        }


        /// <summary>
        /// ����ַ����Ƿ�Ϊ��������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(string str)
        {
            bool isDate = false;
            try
            {
                DateTime d = DateTime.Parse(str);
                isDate = true;
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }

        /// <summary>
        /// ����������ڵ�ʱ���(��λ����)
        /// </summary>
        /// <param name="oldDate">һ���Ͼɵ�ʱ��</param>
        /// <param name="newDate">��ʱ��</param>
        /// <returns></returns>
        public static int DateDiff(DateTime oldDate, DateTime newDate)
        {
            TimeSpan ts1 = new TimeSpan(oldDate.Ticks);
            TimeSpan ts2 = new TimeSpan(newDate.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();
            return TimeSerial(ts);
        }
        /// <summary>
        /// ����һ��ʱ���뵱ǰʱ��Ĳ�(��λ����)
        /// </summary>
        /// <param name="CompareDate">Ҫ�Ƚϵ�����</param>
        /// <returns>ʱ��(��λ����)</returns>
        public static int DateDiff(DateTime CompareDate)
        {
            DateTime dtNow = DateTime.Now;
            return DateDiff(CompareDate, dtNow);
        }
        /// <summary>
        /// ��ָ��ʱ���֡������л�����
        /// </summary>
        /// <param name="Hour">Ҫ���л���Сʱ</param>
        /// <param name="Minute">Ҫ���л��ķ�</param>
        /// <param name="Seconds">Ҫ���л�����</param>
        /// <returns>����������</returns>
        public static int TimeSerial(int Hour, int Minute, int Seconds)
        {
            return (Hour * 3600 + Minute * 60 + Seconds);
        }
        /// <summary>
        /// ��ָ��ʱ��(TimeSpan)���л�Ϊ��
        /// </summary>
        /// <param name="ts">Ҫ���л���ʱ��</param>
        /// <returns>����������</returns>
        public static int TimeSerial(TimeSpan ts)
        {
            return TimeSerial(ts.Hours, ts.Minutes, ts.Seconds);
        }
        /// <summary>
        /// ��ָ��ʱ��(��)���г�ʱ-��-���ʽ
        /// </summary>
        /// <param name="Seconds">Ҫ���л���ʱ��(��)</param>
        /// <returns>����Struct.stTime�ṹ�壬�ڰ���ʱ���֣���</returns>
        public static Struct.StTime TimeSerial(int Seconds)
        {
            Struct.StTime ST = new Struct.StTime();
            ST.Hour = (int)(Seconds / 3600);
            ST.Minute = (int)((Seconds - ST.Hour * 3600) / 60);
            ST.Seconds = Seconds % 60;
            return ST;
        }
        /// <summary>
        /// ��õ�ǰʱ���4�ֽ�ʱ���
        /// </summary>
        /// <returns></returns>
        public static int GetTimeStamp()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1); //�õ�1970���ʱ��� 
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000; //ע��������ʱ�����⣬��now��Ҫ����8��Сʱ
            int b = (int)a;
            return b;
        }
        /// <summary>
        /// ��4�ֽ�ʱ���ת��Ϊʱ��
        /// </summary>
        /// <param name="Stamp"></param>
        /// <returns></returns>
        public static DateTime getTimeByStamp(int Stamp)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);
            long d = Stamp * 10000000 + timeStamp.Ticks;
            DateTime e = new DateTime(d);
            return e;
        }
    }
}

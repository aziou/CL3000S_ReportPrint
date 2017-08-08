using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Utils
{
    /// <summary>
    /// 数组集合相关操作
    /// </summary>
    public class ArrayHelper
    {

        /// <summary>
        /// 判断数组中的所有元素是否相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsAllValueMatch<T>(T[] values)
        {
            if (values.Length == 0)
            {
                return false;
            }
            return IsAllValueMatch(values, values[0]);
        }

        /// <summary>
        /// 判断数组中的所有元素是否与给定的 对象相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="va"></param>
        /// <returns></returns>
        public static bool IsAllValueMatch<T>(T[] values, T va)
        {
            foreach (T t in values)
            {
                if (va.Equals(t) == false)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 求数据中的最大数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T Max<T>(params T[] values) where T : IComparable
        {
            T maxValue = default(T);
            foreach (T t in values)
            {
                if (t.CompareTo(maxValue) > 0)
                {
                    maxValue = t;
                }
            }

            return maxValue;
        }


        /// <summary>
        /// 使用给定的数据，生成一个数组
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">用来实初始化数组内数据的值</param>
        /// <param name="len">数组的长度</param>
        /// <returns></returns>
        public static T[] MakeArray<T>(T value, int len)
        {
            T[] values = new T[len];
            for (int i = 0; i < values.Length; i++)
                values[i] = value;

            return values;
        }

        /// <summary>
        /// 生成指定类型的 List
        /// 
        /// </summary>
        /// <typeparam name="T">要生成的元素类型</typeparam>
        /// <param name="len">元素的个数</param>
        /// <returns></returns>
        public static List<T> GenArray<T>(int len) where T : new()
        {
            List<T> list = new List<T>(len);

            for (int i = 0; i < len; i++)
            {
                list.Add(new T());
            }

            return list;
        }
    }
}

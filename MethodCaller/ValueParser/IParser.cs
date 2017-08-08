using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    interface IParser
    {

        /// <summary>
        /// 是否可以处理，该种类型的转换
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool AcceptType(Type t);

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object Parse(Type t, MarshalAsAttribute attri, string value);

        string GetDefalutValue(Type t, MarshalAsAttribute attri);

    }
}

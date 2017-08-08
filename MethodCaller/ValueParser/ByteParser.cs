using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class ByteParser : IParser
    {
        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t == typeof(byte) || t.Name.Equals(typeof(byte).Name + "&", StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            return byte.Parse(value);
        }

        #endregion

        #region IParser 成员


        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            return "1";
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class UIntParser : IParser
    {
        #region IParser 成员
        public bool AcceptType(Type t)
        {
            if (t == typeof(UInt16) || t == typeof(UInt32) || t == typeof(UInt64)
                 || t.Name.Equals(typeof(UInt16) + "&", StringComparison.OrdinalIgnoreCase) || t.Name.Equals(typeof(UInt32) + "&", StringComparison.OrdinalIgnoreCase)
                 || t.Name.Equals(typeof(UInt64) + "&", StringComparison.OrdinalIgnoreCase)
                )
                return true;
            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            if (t == typeof(UInt16) || t.Name.Equals(typeof(UInt16) + "&", StringComparison.OrdinalIgnoreCase))
            {
                return UInt16.Parse(value);
            }
            if (t == typeof(UInt32) || t.Name.Equals(typeof(UInt32) + "&", StringComparison.OrdinalIgnoreCase))
            {
                return UInt32.Parse(value);
            }

            if (t == typeof(UInt64) || t.Name.Equals(typeof(UInt64) + "&", StringComparison.OrdinalIgnoreCase))
            {
                return UInt64.Parse(value);
            }

            return null;
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

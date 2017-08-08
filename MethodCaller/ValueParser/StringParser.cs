using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class StringParser : IParser
    {
        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t == typeof(string) || t.Name.Equals("String&", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            return value;
        }

        #endregion

        #region IParser 成员


        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            return "STR";
        }

        #endregion
    }
}

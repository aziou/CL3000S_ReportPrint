using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class BooleanParser : IParser
    {
        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t.Name == typeof(Boolean).Name || t.Name.Equals(typeof(Boolean).Name + "&", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            return bool.Parse(value);
        }

        #endregion

        #region IParser 成员


        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            return false.ToString();
        }

        #endregion
    }
}

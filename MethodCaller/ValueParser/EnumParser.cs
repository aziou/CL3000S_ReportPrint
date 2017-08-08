using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class EnumParser : IParser
    {

        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t.IsEnum || (t.BaseType != null && t.BaseType.IsEnum))
                return true;

            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            return Enum.Parse(t, value);
        }

        #endregion

        #region IParser 成员


        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            Array value = Enum.GetValues(t);
            return value.GetValue(0).ToString();
        }

        #endregion
    }
}

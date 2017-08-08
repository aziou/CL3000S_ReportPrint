using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class SingleParser : IParser
    {
        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t == typeof(float) || t == typeof(double) || t.Name.Equals(typeof(float).Name + "&", StringComparison.OrdinalIgnoreCase) || t.Name.Equals(typeof(double).Name + "&", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            if (t == typeof(float) || t.Name.Equals(typeof(float).Name + "&", StringComparison.OrdinalIgnoreCase))
            {
                return float.Parse(value);
            }
            if (t == typeof(double) || t.Name.Equals(typeof(double).Name + "&", StringComparison.OrdinalIgnoreCase))
            {
                return double.Parse(value);
            }

            return null;
        }

        #endregion

        #region IParser 成员


        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            return "0.0";
        }

        #endregion
    }
}

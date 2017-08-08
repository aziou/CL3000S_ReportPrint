
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{

    class IntParser : IParser
    {
        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t == typeof(Int16) || t == typeof(Int32) || t == typeof(Int64)
                 || t.Name.Equals(typeof(Int16).Name + "&", StringComparison.OrdinalIgnoreCase) || t.Name.Equals(typeof(Int32).Name + "&", StringComparison.OrdinalIgnoreCase) ||
                 t.Name.Equals(typeof(Int64).Name + "&", StringComparison.OrdinalIgnoreCase)
                )
                return true;
            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            if (t == typeof(Int16) || t.Name.Equals(typeof(Int16).Name + "&", StringComparison.OrdinalIgnoreCase))
            {
                return Int16.Parse(value);
            }
            if (t == typeof(Int32) || t.Name.Equals(typeof(Int32).Name + "&", StringComparison.OrdinalIgnoreCase))
            {
                return Int32.Parse(value);
            }

            if (t == typeof(Int64) || t.Name.Equals(typeof(Int64).Name + "&", StringComparison.OrdinalIgnoreCase))
            {
                return Int64.Parse(value);
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
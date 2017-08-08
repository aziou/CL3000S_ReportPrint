using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace MethodCaller.ValueParser
{
    class ObjectParser : IParser
    {
        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t == typeof(object) || t.Name.Equals(typeof(object).Name + "&", StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {

            return new CtrComm.YCIVCtrClassClass();
            //if (t.IsByRef)
            //    t = t.GetElementType();
    
            //ConstructorInfo[] cis = t.GetConstructors();
            //foreach (ConstructorInfo ci in cis)
            //{
            //    if (ci.GetParameters().Length == 0)
            //    {
            //        return Activator.CreateInstance(t);
            //    }
            //}
            //return new object();
        }

        #endregion

        #region IParser 成员


        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            return "Object";
        }

        #endregion
    }
}

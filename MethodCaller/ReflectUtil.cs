using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MethodCaller
{
    class ReflectUtil
    {

        public static MarshalAsAttribute GetMarshalAsAttribute(ParameterInfo pi)
        {
            object[] attrs = pi.GetCustomAttributes(true);
            MarshalAsAttribute attri = null;
            foreach (object o in attrs)
            {
                if (o is System.Runtime.InteropServices.MarshalAsAttribute)
                {
                    attri = o as System.Runtime.InteropServices.MarshalAsAttribute;
                }
            }

            return attri;
        }

        public static MethodInfo[] GetMethod(Type t)
        {
            List<MethodInfo> mes = new List<MethodInfo>();
            MethodInfo[] mis = t.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach (MethodInfo mi in mis)
            {
                if (mi.DeclaringType == null || mi.DeclaringType == t)
                {
                    mes.Add(mi);
                }
            }

            return mes.ToArray();
        }
    }
}

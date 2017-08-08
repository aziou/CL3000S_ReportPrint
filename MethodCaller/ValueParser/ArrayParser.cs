using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class ArrayParser : IParser
    {

        public static Dictionary<VarEnum, Type> typeDic = new Dictionary<VarEnum, Type>();

        static ArrayParser()
        {
            typeDic.Add(VarEnum.VT_I2, typeof(short));
            typeDic.Add(VarEnum.VT_R4, typeof(float));
            typeDic.Add(VarEnum.VT_R8, typeof(double));
            typeDic.Add(VarEnum.VT_I4, typeof(long));
            typeDic.Add(VarEnum.VT_I8, typeof(Int64));
        }


        #region IParser 成员

        public bool AcceptType(Type t)
        {
            if (t.IsArray || t.Name == "Array&")
                return true;
            return false;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            object o = null;
            Type baseType = Type.GetType(t.FullName.Replace("&", "").Replace("[]", ""));

            if (baseType == null)
            {
                baseType = t.Assembly.GetType(t.FullName.Substring(0, t.FullName.IndexOf('[')));

            }
            if (t.Name.EndsWith("[][]") || t.Name.EndsWith("[,]"))
            {
                Type newType = Type.GetType(baseType.FullName + "[]");
                string[] strs = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                Array array = Array.CreateInstance(baseType, strs.Length, strs[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length);

                for (int i = 0; i < strs.Length; i++)
                {
                    Array subArray = (Array)Parse(newType, attri, strs[i]);
                    for (int j = 0; j < subArray.Length; j++)
                    {
                        array.SetValue(subArray.GetValue(j), i, j);
                    }
                }
                o = array;
            }
            else if (t.Name.EndsWith("[]") || t.Name.EndsWith("&"))
            {
                string[] strs = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Array array = null;
                if (t.Name.Equals("Array&"))
                {
                    array = Array.CreateInstance(typeDic[attri.SafeArraySubType], strs.Length);
                    for (int i = 0; i < strs.Length; i++)
                    {
                        array.SetValue(Parser.Instance.Parse(typeDic[attri.SafeArraySubType], attri, strs[i]), i);
                    }
                }
                else
                {
                    array = Array.CreateInstance(baseType, strs.Length);
                    for (int i = 0; i < strs.Length; i++)
                    {
                        array.SetValue(Parser.Instance.Parse(baseType, attri, strs[i]), i);
                    }
                }
                o = array;
            }
            else
            {
            }
            return o;
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

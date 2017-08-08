using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MethodCaller.ValueParser
{
    class Parser : IParser
    {
        static Parser parser = new Parser();

        public static IParser Instance
        {
            get
            {
                return parser;
            }
        }

        private List<IParser> parsers = new List<IParser>();

        public Parser()
        {
            parsers.Add(new ArrayParser());
            parsers.Add(new EnumParser());
            parsers.Add(new IntParser());
            parsers.Add(new SingleParser());
            parsers.Add(new BooleanParser());
            parsers.Add(new StringParser());
            parsers.Add(new ByteParser());
            parsers.Add(new UIntParser());
            parsers.Add(new ObjectParser());
        }


        public bool AcceptType(Type t)
        {
            foreach (IParser ip in this.parsers)
            {
                if (ip.AcceptType(t))
                {
                    return true;
                }
            }
            return false;
        }

        public IParser GetParser(Type t)
        {
            foreach (IParser ip in this.parsers)
            {
                if (ip.AcceptType(t))
                {
                    return ip;
                }
            }
            return null;
        }

        public object Parse(Type t, MarshalAsAttribute attri, string value)
        {
            return this.GetParser(t).Parse(t, attri, value);
        }

        public string GetDefalutValue(Type t, MarshalAsAttribute attri)
        {
            return GetParser(t).GetDefalutValue(t, attri);
        }
    }
}

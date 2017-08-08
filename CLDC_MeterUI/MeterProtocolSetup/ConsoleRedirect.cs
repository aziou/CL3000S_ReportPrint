using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_MeterUI
{
    class ConsoleRedirect:System.IO.TextWriter
    {
        public static ConsoleRedirect Instance = null;
        static ConsoleRedirect()
        {
            Instance = new ConsoleRedirect();
        }
        public ConsoleRedirect()
        {
            if (Instance != null) throw new Exception("单例模式，不能多次实例化");
        }


        public Action<string> OnMessage;

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        public override void Write(string msg)
        {
            if (OnMessage != null) OnMessage(msg);
        }
        public override void WriteLine(string format, object arg0)
        {
            Write(string.Format(format,arg0));
        }

        public override void WriteLine(string value)
        {
            Write(value);
        }
    }
}

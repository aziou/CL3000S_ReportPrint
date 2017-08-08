using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CtrComm_StdMeter;

namespace MethodCaller
{
    public partial class StdCommMethodCaller : MethodCaller.MethodCallerMain
    {
        public StdCommMethodCaller()
        {
            InitializeComponent();
        }

        protected override object GetDriver()
        {
            return new ClsStdCommClass();
        }

        protected override System.Reflection.MethodInfo[] GetMethods()
        {
            return ReflectUtil.GetMethod(typeof(ClsStdCommClass));
        }
    }
}

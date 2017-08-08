using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MethodCaller
{
    public partial class GenyDriverMethodCaller : MethodCaller.MethodCallerMain
    {
        public GenyDriverMethodCaller()
        {
            InitializeComponent();
            this.Load += new EventHandler(GenyDriverMethodCaller_Load);
        }

        void GenyDriverMethodCaller_Load(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.cbbSerialPorts.Visible = false;
            this.tbSerialPortConfig.Visible = false;
        }

        protected override object GetDriver()
        {
            return new DeviceDriver.Driver(24);
        }


        protected override System.Reflection.MethodInfo[] GetMethods()
        {
            return ReflectUtil.GetMethod(typeof(DeviceDriver.Driver));
        }
    }
}

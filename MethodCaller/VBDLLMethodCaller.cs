using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MethodCaller
{
    public partial class VBDLLMethodCaller : MethodCaller.MethodCallerMain
    {
        public VBDLLMethodCaller()
        {
            InitializeComponent();
        }

        protected override System.Reflection.MethodInfo[] GetMethods()
        {
            return ReflectUtil.GetMethod(typeof(CtrComm.YCIVCtrClassClass));
        }

        protected override object GetDriver()
        {
            return new CtrComm.YCIVCtrClassClass();
        }

        protected override void OnFuctionSlected(MethodInfo mi)
        {
            this.dataGridView1.Rows[1].Cells[2].Value = this.cbbSerialPorts.SelectedItem.ToString().ToString().Substring(3);
            if (mi.Name.Equals("OpenBox") == false)
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals("SettingStr", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        row.Cells[2].Value = this.tbSerialPortConfig.Text;
                    }
                }
            }
        }
    }
}

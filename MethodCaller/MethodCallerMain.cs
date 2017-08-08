using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO.Ports;
using Microsoft.Win32;
using MethodCaller.ValueParser;
using System.Runtime.InteropServices;
using System.Threading;

namespace MethodCaller
{
    public partial class MethodCallerMain : Form
    {
        /// <summary>
        /// 调用 方法列表
        /// </summary>
        protected MethodInfo[] methods;

        /// <summary>
        /// 调用 对象
        /// </summary>
        protected object driver;

        public MethodCallerMain()
        {
            InitializeComponent();
            methods = GetMethods();
            driver = GetDriver();
        }

        protected virtual object GetDriver()
        {
            return null;
        }

        protected virtual MethodInfo[] GetMethods()
        {
            return null;
        }

        protected virtual void OnFuctionSlected(MethodInfo mi)
        {
        }

        void MethodCallerMain_Load(object sender, EventArgs e)
        {
            if (methods == null)
                return;
            foreach (MethodInfo mi in methods)
            {
                this.cbbMethods.Items.Add((this.cbbMethods.Items.Count + 1) + " " + mi.ToString());
            }

            string[] virtualPorts = COM32Helper.GetVirtualPorts();

            foreach (string s in virtualPorts)
            {
                //if (int.Parse(s.Substring(3)) % 2 == 0)
                //    continue;
                this.cbbSerialPorts.Items.Add(s);
            }
            if (this.cbbSerialPorts.Items.Count > 0)
            {
                this.cbbSerialPorts.SelectedIndex = 0;
            }
        }

        private void cbbMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();

            MethodInfo mi = this.methods[this.cbbMethods.SelectedIndex];
            ParameterInfo ret = mi.ReturnParameter;

            this.dataGridView1.Rows.Add("返回值类型", ret.ParameterType.Name, "");
            ParameterInfo[] para = mi.GetParameters();
            foreach (ParameterInfo pi in para)
            {
                string defaultVa = Parser.Instance.GetDefalutValue(pi.ParameterType, ReflectUtil.GetMarshalAsAttribute(pi));
                if (pi.ParameterType.Name.Equals("Array&"))
                {
                    this.dataGridView1.Rows.Add(pi.Name, pi.ParameterType.Name + "(" + ArrayParser.typeDic[ReflectUtil.GetMarshalAsAttribute(pi).SafeArraySubType].Name + ")", defaultVa);
                }
                else
                {
                    this.dataGridView1.Rows.Add(pi.Name, pi.ParameterType.Name, defaultVa);
                }
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Tag = pi;
            }

            this.OnFuctionSlected(mi);
        }

        private bool CheckInputValue()
        {
            for (int i = 1; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[this.colValue.Index].Value == null)
                {
                    MessageBox.Show("请输入第" + (i + 1) + "行的值");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 创建要调用的参数
        /// </summary>
        /// <returns></returns>
        protected object[] CreateParameters()
        {
            object[] values = new object[this.dataGridView1.Rows.Count - 1];
            for (int i = 1; i < this.dataGridView1.Rows.Count; i++)
            {
                ParameterInfo pi = this.dataGridView1.Rows[i].Tag as ParameterInfo;
                Type paraType = pi.ParameterType;

                values[i - 1] = Parser.Instance.Parse(paraType, ReflectUtil.GetMarshalAsAttribute(pi), this.dataGridView1.Rows[i].Cells[this.colValue.Index].Value.ToString());
            }
            return values;
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Alt)
            {
                this.CallAllMethod();
                return ;
            }

            if (CheckInputValue() == false)
            {
                return;
            }
            try
            {
                object[] obs = CreateParameters();
                object o = methods[this.cbbMethods.SelectedIndex].Invoke(driver, obs);
                if (o != null)
                {
                    this.richTextBox1.AppendText("执行结果:" + o.ToString());
                    this.richTextBox1.AppendText(Environment.NewLine);
                }
                else
                {
                    this.richTextBox1.AppendText("该方法无返回值" + Environment.NewLine);
                }
            }
            catch (TargetInvocationException tie)
            {
                MessageBox.Show(tie.InnerException.Message, tie.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool isCallAllMethod = false;
        private void CallAllMethod()
        {
            if (isCallAllMethod == true)
            {
                return;
            }
            isCallAllMethod = true;
            for (int i = 0; i < this.cbbMethods.Items.Count; i++)
            {
                this.cbbMethods.SelectedIndex = i;
                Application.DoEvents();
                this.btnCall_Click(this.btnCall, new EventArgs());
                Application.DoEvents();
            }
            isCallAllMethod = false;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.textBox1.TextLength < 1)
            {
                return;
            }

            for (int i = 0; i < this.cbbMethods.Items.Count; i++)
            {
                if (this.cbbMethods.Items[i].ToString().IndexOf(this.textBox1.Text, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    this.cbbMethods.SelectedIndex = i;
                    break;
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnQuery_Click(this.btnQuery, new EventArgs());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using MethodCaller.ValueParser;
using System.Runtime.InteropServices;

namespace MethodCaller
{
    public partial class RequestPacketTest : Form
    {
        List<Type> types = DeviceDriver.Drivers.Geny.Packets.PacketSeracher.GetAllOutPacketType();
        Type currentType = null;
        ConstructorInfo currentCI = null;
        object currentObject = null;

        public RequestPacketTest()
        {
            InitializeComponent();
            this.Load += new EventHandler(RequestPacketTest_Load);
        }

        void RequestPacketTest_Load(object sender, EventArgs e)
        {
            //加载所有类型
            foreach (Type t in types)
            {
                this.cbbClass.Items.Add((this.cbbClass.Items.Count + 1) + "  " + t.Name);
            }
            if (types.Count > 1)
                this.cbbClass.SelectedIndex = 0;
        }

        private bool CheckInputValue()
        {
            for (int i = 1; i < this.dgvTypeInfo.Rows.Count; i++)
            {
                if (this.dgvTypeInfo.Rows[i].Cells[this.colValue.Index].Value == null)
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
            object[] values = new object[this.dgvTypeInfo.Rows.Count];

            for (int i = 0; i < this.dgvTypeInfo.Rows.Count; i++)
            {
                ParameterInfo pi = this.dgvTypeInfo.Rows[i].Tag as ParameterInfo;
                Type paraType = pi.ParameterType;

                values[i] = Parser.Instance.Parse(paraType, ReflectUtil.GetMarshalAsAttribute(pi), this.dgvTypeInfo.Rows[i].Cells[this.colValue.Index].Value.ToString());
            }
            return values;
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            if (this.tbSerach.TextLength < 1)
            {
                return;
            }

            for (int i = 0; i < this.cbbClass.Items.Count; i++)
            {
                if (this.cbbClass.Items[i].ToString().IndexOf(this.tbSerach.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.cbbClass.SelectedIndex = i;
                    return;
                }
            }

            MessageBox.Show(this.tbSerach.Text, "未找到");
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            string methodName = "GetPacketData";
            MethodInfo mi = this.currentType.GetMethod(methodName);
            try
            {
                object[] parameters = CreateParameters();
                if (parameters.Length == 0)
                    parameters = null;
                this.currentObject = Activator.CreateInstance(this.currentType, parameters);
                object retValue = mi.Invoke(this.currentObject, null);
                byte[] buf = retValue as byte[];
                this.rtbData.AppendText(ByteFormarter.Formart(buf));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "调用方法:" + methodName);
            }
        }

        private void cbbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentType = null;
            this.currentObject = null;
            this.currentCI = null;
            this.dgvTypeInfo.Rows.Clear();

            this.currentType = this.types[this.cbbClass.SelectedIndex];
            ConstructorInfo[] cis = this.currentType.GetConstructors();
            foreach (ConstructorInfo c in cis)
            {
                if (c.GetParameters().Length != 0)
                {
                    this.currentCI = c;
                    break;
                }
            }
            if (this.currentCI == null)
            {
                MessageBox.Show("该方法不具有带参的构造函数");
                this.currentCI = cis[0];
            }

            try
            {
                ParameterInfo[] pis = this.currentCI.GetParameters();
                foreach (ParameterInfo pi in pis)
                {
                    if (pi.ParameterType.Name.Equals("Array&"))
                    {
                        this.dgvTypeInfo.Rows.Add(pi.Name, pi.ParameterType.Name + "(" + ArrayParser.typeDic[ReflectUtil.GetMarshalAsAttribute(pi).SafeArraySubType].Name + ")", "");
                    }
                    else
                    {
                        this.dgvTypeInfo.Rows.Add(pi.Name, pi.ParameterType.Name, "");
                    }
                    this.dgvTypeInfo.Rows[this.dgvTypeInfo.Rows.Count - 1].Tag = pi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "创建对象失败");
            }
        }

        private void tbSerach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSerach_Click(this.btnSerach, new EventArgs());
            }
        }
    }
}

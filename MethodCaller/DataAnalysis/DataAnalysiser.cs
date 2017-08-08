using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CLCommunicationLib.Serial;

namespace MethodCaller.DataAnalysis
{
    public partial class DataAnalysiser : Form
    {

        CLCommunicationLib.Serial.COM32 com321;
        CLCommunicationLib.Serial.COM32 com322;

        public DataAnalysiser()
        {
            InitializeComponent();
        }

        private void DataAnalysiser_Load(object sender, EventArgs e)
        {
            string[] virtualPorts = COM32Helper.GetVirtualPorts();

            foreach (string s in virtualPorts)
            {
                if (int.Parse(s.Substring(3)) % 2 != 0)
                    continue;

                this.cbbSerialPort1.Items.Add(s);
                this.cbbSerialPort2.Items.Add(s);
            }

            if (this.cbbSerialPort1.Items.Count > 0)
            {
                this.cbbSerialPort1.SelectedIndex = 0;
                this.cbbSerialPort2.SelectedIndex = 0;
            }
        }

        private void btnOpen1_Click(object sender, EventArgs e)
        {
            if (this.com321 == null)
            {
                this.com321 = this.OpenCom32(this.cbbSerialPort1.SelectedItem.ToString());
                this.com321.ByteTimeOut = 5;
                if (this.com321 == null)
                    return;
                this.com321.BeginListening(new COM32.AsyncSerailCallback(this.Com321Listener));
                this.btnOpen1.Text = "关闭";
            }
            else
            {
                this.com321.ClosePort();
                this.com321 = null;
                this.btnOpen1.Text = "打开";
            }
        }

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            if (this.com322 == null)
            {
                this.com322 = this.OpenCom32(this.cbbSerialPort2.SelectedItem.ToString());
                if (this.com322 == null)
                {
                    return;
                }
                this.com322.BeginListening(new COM32.AsyncSerailCallback(this.Com322Listener));
                this.btnOpen2.Text = "关闭";
            }
            else
            {
                this.com322.ClosePort();
                this.com322 = null;
                this.btnOpen2.Text = "打开";
            }
        }

        private void Com321Listener(byte[] buf)
        {
            this.AppenData(this.richTextBox1, buf, this.com321);
        }

        private void Com322Listener(byte[] buf)
        {
            this.AppenData(this.richTextBox2, buf, this.com322);
        }

        private void AppenData(RichTextBox rtb, byte[] buf, COM32 com32)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(DateTime.Now + ": ");
            sb.Append(ByteFormarter.Formart(buf));
            if (buf[0] == 0xE6 && buf[2] == 0x02 && buf[3] == 0xAA && buf[4] == 0x37)
            {
                buf[3] = 79;
                buf[4] = 75;
                com32.BeginSendData(buf);
                byte[] btmp = DeviceDriver.Drivers.Geny.CRC.GetCrcValueLittleEndian(buf, 0, 5);
                buf[5] = btmp[0];
                buf[6] = btmp[1];
                sb.AppendLine(DateTime.Now + " Out:" + ByteFormarter.Formart(buf));
            }

            Action<int> method = delegate(int i)
            {
                rtb.AppendText(sb.ToString());
                this.BringToFront();
                this.Activate();
            };
            rtb.Invoke(method, 0);
        }

        private CLCommunicationLib.Serial.COM32 OpenCom32(string portName)
        {
            COM32 com32 = null;

            try
            {
                com32 = new COM32();
                if (com32.OpenPort("1200,n,8,1", portName) == false)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                com32 = null;
            }

            return com32;
        }

        private void DataAnalysiser_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.com321.ClosePort();
            }
            catch
            { }

            try
            {
                this.com322.ClosePort();
            }
            catch { }
        }
    }
}

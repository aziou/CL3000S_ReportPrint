using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    public partial class Frm_ControlMessage : Office2007Form
    {
        public Frm_ControlMessage()
        {
            InitializeComponent();
            
        }

        private void Lab_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(); 
        }

        public void SetData(string MsgString)
        {
            Txt_Message.BeginInvoke(new Dgt_WriteText(WriteText),MsgString);
        }

        private delegate void Dgt_WriteText(string MessageText);

        private void WriteText(string MsgString)
        {
            Txt_Message.AppendText(MsgString + " " + DateTime.Now.ToString() + '\r' + '\n');
        }
        /// <summary>
        /// 数据清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Clear_Click(object sender, EventArgs e)
        {
            Txt_Message.Text = "";
        }
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Save_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\ControlTxt.Txt"))
            { 
                System.IO.FileStream File= System.IO.File.Create(Application.StartupPath + @"\ControlTxt.Txt");
                File.Close();
            }

            System.IO.TextWriter TxtWrite=new System.IO.StreamWriter(Application.StartupPath + @"\ControlTxt.Txt",false);

            TxtWrite.Write(Txt_Message.Text);

            TxtWrite.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

    }


}
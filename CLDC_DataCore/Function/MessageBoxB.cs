using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 倒计时提示框
    /// </summary>
    public partial class MessageBoxB : Form
    {
        private string MsgText = string.Empty;
        private int LastSecond = 30;    //30秒到计时
        /// <summary>
        /// 要倒计时
        /// </summary>
        private bool _isDelay = true;//

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Caption"></param>
        public MessageBoxB(string Text,string Caption)
        {
            InitializeComponent();
            this.Text = Caption;
            this.MsgText = Text;
            this.Lab_Msg.Text = Text;
            this.DialogResult = DialogResult.No;

            this.Load += new EventHandler(MessageBoxB_Load);
        }
        public MessageBoxB(string Text, string Caption, bool isDelay)
        {
            InitializeComponent();
            this.Text = Caption;
            this.MsgText = Text;
            this.Lab_Msg.Text = Text;
            this.DialogResult = DialogResult.No;
            _isDelay = isDelay;
            this.Load += new EventHandler(MessageBoxB_Load);
        }

        void MessageBoxB_Load(object sender, EventArgs e)
        {
            this.Btn_Yes.Focus();
            if (_isDelay)
            {
                timer1.Interval = 1000;
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Start();
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            this.Lab_Msg.Text = string.Format("{0}({1})", MsgText, LastSecond--);
            if (LastSecond < 0)
            {
                this.Close();
            }
        }


        /// <summary>
        /// 倒计时30S提示
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Caption"></param>
        /// <returns></returns>
        public static DialogResult Show(string Text, string Caption)
        {
            MessageBoxB Dlg = new MessageBoxB(Text, Caption);
            DialogResult Result = Dlg.ShowDialog();
            Dlg.Dispose();
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Caption"></param>
        /// <param name="isDelay">是否倒计时</param>
        /// <returns></returns>
        public static DialogResult Show(string Text, string Caption, bool isDelay)
        {
            MessageBoxB Dlg = new MessageBoxB(Text, Caption, isDelay);
            DialogResult Result = Dlg.ShowDialog();
            Dlg.Dispose();
            return Result;
        }




    }
}
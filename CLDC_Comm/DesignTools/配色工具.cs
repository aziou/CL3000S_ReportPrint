using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CLDC_Comm.DesignTools
{

    /// <summary>
    /// 动态调色工具，使用方法:Comm.DesignTools.配色工具.Bind(需要调色的控件)
    /// </summary>
    public partial class 配色工具 : Form
    {

        /// <summary>
        /// 绑定[配色工具]、使他里面的所有控件，在右键时都会弹出一个调色控制面版
        /// </summary>
        /// <param name="parentControl"></param>
        public static void Bind(Control parentControl)
        {
            parentControl.MouseDown += delegate(object sender, MouseEventArgs e)
            {
                if (!(sender is Control) || e.Button != MouseButtons.Right) return;

                //ModeMenu menu = new ModeMenu();

                Control hSender = (Control)sender;
              

                //do {
                //    menu.Items.Add(hSender.Name);
                //    if (hSender.Parent != null )
                //        hSender = hSender.Parent;
                //}while(hSender.Parent != null);

                //menu.ShowDialog();
                //string strControlName = menu.menuStrip1

                配色工具 dlg = new 配色工具((Control)sender);
                dlg.Show();
            };


            foreach (Control ChildControl in parentControl.Controls)
            {
                Bind(ChildControl);
            }
        }


        //=============================================================
        /// <summary>
        /// 被绑定对象
        /// </summary>
        private Control HandleControl = null;


        #region 构造函数:私有
        private 配色工具(Control pControl)
        {
            InitializeComponent();
            HandleControl = pControl;
            Lab_Top.MouseMove += new MouseEventHandler(FormMove);

            txtControlName.Text = pControl.Name;
            radio_qianjing.CheckedChanged += delegate(object sender, EventArgs e)
            {
                if (radio_qianjing.Checked)
                {
                    Color color = HandleControl.ForeColor;
                    txtColorValue.Text = string.Format("{0},{1},{2}",color.R,color.G ,color.B);
                    trackBar_R.Value = color.R;
                    trackBar_G.Value = color.G;
                    trackBar_B.Value = color.B;
                }
            };
            radio_beijing.CheckedChanged += delegate(object sender, EventArgs e)
            {
                if (radio_beijing.Checked)
                {
                    Color color = HandleControl.BackColor;
                    txtColorValue.Text = string.Format("{0},{1},{2}", color.R, color.G, color.B);
                    trackBar_R.Value = color.R;
                    trackBar_G.Value = color.G;
                    trackBar_B.Value = color.B;
                }
            };

            trackBar_R.ValueChanged += new EventHandler(ChangeColor);
            trackBar_G.ValueChanged += new EventHandler(ChangeColor);
            trackBar_B.ValueChanged += new EventHandler(ChangeColor);

        }



        private void ChangeColor(object sender, EventArgs e)
        {
            Color color = Color.FromArgb(trackBar_R.Value,trackBar_G.Value,trackBar_B.Value);
            txtColorValue.Text = string.Format("{0},{1},{2}", color.R, color.G, color.B);
            if (radio_qianjing.Checked)
            {
                HandleControl.ForeColor = color;
            }
            else
            {
                HandleControl.BackColor = color;
            }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        #region 窗体拖曳
        public const int WM_SYSCOMMAND = 0x0112;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public extern static bool ReleaseCapture();
        private void FormMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            CLDC_Comm.Win32Api.SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, 0xF017, 0);
        }
        #endregion

        #region 关闭按钮
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }


    }



}
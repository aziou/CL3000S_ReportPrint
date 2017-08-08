using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CLDC_MeterUI
{
    /// <summary>
    /// 业务描述窗体
    /// </summary>
    public partial class ShowCheckStep: Form
    {
        /// <summary>
        /// 显示延时定时器
        /// </summary>
        private Timer timerDelay1500 = new Timer();
        /// <summary>
        /// 图片路径
        /// </summary>
        private static string bllName = "业务描述.jpg";
        /// <summary>
        /// 当前实例
        /// </summary>
        private static ShowCheckStep instance;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object syncRoot = new Object();
        /// <summary>
        /// 获取当前实例
        /// </summary>
        /// <param name="bllname"></param>
        /// <returns></returns>
        public static ShowCheckStep Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                        instance = new ShowCheckStep();
                }
                return instance;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShowCheckStep()
        {
            InitializeComponent();
            timerDelay1500.Interval = 1500;
            timerDelay1500.Tick += new EventHandler(timerDelay1500_Tick);
            RefreshPictureBox();
            timerDelay1500.Enabled = false;
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerDelay1500_Tick(object sender, EventArgs e)
        {
            //延时显示图片
            RefreshPictureBox();

            //根据鼠标位置显示窗体
            int positionX = MousePosition.X + 10;
            int positionY = MousePosition.Y;
            if(positionY+this.Height> Screen.GetWorkingArea(this).Height)
                positionY=Screen.GetWorkingArea(this).Height-this.Height;

            ShowCheckStep.Instance.Location = new Point(positionX, positionY);

            this.Visible = true;
            this.Show();
            //关闭定时器
            timerDelay1500.Enabled = false;
        }

        /// <summary>
        /// 开始显示图片
        /// </summary>
        /// <param name="imageName">图片名称</param>
        public void Display(string imageName)
        {
            bllName = imageName;
            timerDelay1500.Start();
        }

        /// <summary>
        /// 隐藏窗体显示
        /// </summary>
        public void HideForm()
        {
            timerDelay1500.Stop();
            this.Visible = false;
        }
        /// <summary>
        /// 刷新业务流程的图片
        /// </summary>
        /// <param name="stringName"></param>
        private void RefreshPictureBox()
        {
            labelBllName.Text =string.Format("{0}", bllName);
            string picturePath = string.Format(@"{0}\Pic\BllDescription\{1}.jpg", Application.StartupPath, bllName);
            Image imageBll = new Bitmap(32, 32);

            if (File.Exists(picturePath))
                imageBll = Image.FromFile(picturePath);
            else
                imageBll = null;

            //窗体大小和图片相适应。图片大小不能太大
            if (imageBll != null)
            {
                this.Size = new Size(imageBll.Width + 5, 24 + 5 + imageBll.Height);
                pictureBoxBll.BackgroundImage = imageBll;
            }
            else
                this.Size = MinimumSize;
        }
        ~ShowCheckStep()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    public delegate void dlgButtnClick(object type);
    public partial class StepUserControl : UserControl
    {
        public dlgButtnClick dlgButtnClickCall;
        public StepUserControl()
        {
            InitializeComponent();
        }
        private string _strBisicInfo = "电能表概要信息";
        public string strBisicInfo
        {
            set
            {
                _strBisicInfo = value;
                //this.labelX1.Text = _strBisicInfo;
            }
        }
        private string _strNowItem = "当前项目";
        public string strNowItem
        {
            set
            {
                _strNowItem = value;
                Action<object> method = delegate(object o)
                {
                    this.labelX2.Text = "当前项目：" + _strNowItem;
                };
                this.BeginInvoke(method, "");
            }
        }
        private string _strNowRun = "当前状态";
        public string strNowRun
        {
            set
            {
                _strNowRun = value;
                Action<object> method = delegate(object o)
                {
                    this.labelX3.Text = "当前状态：" + _strNowRun;
                };
                this.BeginInvoke(method, "");
            }
        }
        private string _strTotalTime = "";
        /// <summary>
        /// 预计：1小时17分17秒
        /// </summary>
        public string strTotalTime
        {
            set
            {
                _strTotalTime = value;
                Action<object> method = delegate(object o)
                {
                    this.stateBarCircle1.TotalTime = "预计：" + _strTotalTime;
                };
                this.BeginInvoke(method, "");
            }
        }
        private string _strLastTime = "";
        /// <summary>
        /// 剩余：1小时17分17秒
        /// </summary>
        public string strLastTime
        {
            set
            {
                this._strLastTime = value;
                Action<object> method = delegate(object o)
                {
                    this.stateBarCircle1.LastTime = "";//TODO: "剩余：" + _strLastTime;
                };
                this.BeginInvoke(method, "");
                
                
            }
        }


        public bool isStepEnabled
        {
            set
            {
                Action<object> method = delegate(object o)
                {
                    this.ToolBtn_StepStart.Enabled = value;
                };
                this.BeginInvoke(method, "");
                //this.ToolBtn_StepStart.Enabled = value;
            }
        }
        public bool isStartEnabled
        {
            set
            {
                Action<object> method = delegate(object o)
                {
                    this.ToolBtn_Start.Enabled = value;
                };
                this.BeginInvoke(method, "");
                //this.ToolBtn_Start.Enabled = value;
            }
        }
        public bool isStopEnabled
        {
            set
            {
                invoke_btn_stop_State(value);

            }
        }

        private void invoke_btn_stop_State(bool value)
        {
            Action<object> method = delegate(object o)
            {
                this.ToolBtn_Stop.Enabled = value;
            };
            this.BeginInvoke(method, "");
        }

        private void ribbonBar2_ItemClick(object sender, EventArgs e)
        {//连续检定 单步检定 停止 三个按钮

            calldelegate(sender);
        }
        private void calldelegate(object type)
        {
            if (dlgButtnClickCall != null)
            {
                
                dlgButtnClickCall(type);
            }
        }
        public bool isEnabled
        {
            set
            {
                Action<object> method = delegate(object o)
                {
                    this.stateBarCircle1.isEnabled = value;
                };
                this.BeginInvoke(method, "");
                //this.stateBarCircle1.isEnabled = value;
            }
        }


        private static void ToNearCircleP(object sender, PaintEventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            System.Drawing.Drawing2D.GraphicsPath btnPath = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Rectangle newRectangle = btn.ClientRectangle;
            newRectangle.Inflate(-1, -1);
            e.Graphics.DrawEllipse(System.Drawing.Pens.BlanchedAlmond, newRectangle);
            newRectangle.Inflate(-1, -1);
            btnPath.AddEllipse(newRectangle);
            btn.Region = new System.Drawing.Region(btnPath);
        }

        //private void pictureBox2_Paint(object sender, PaintEventArgs e)
        //{
        //    ToNearCircleP(sender, e);
        //}
    }
}

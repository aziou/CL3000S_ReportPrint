using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.Runtime.InteropServices;
using System.Threading;

namespace CLDC_MeterUI
{

    public partial class Monitor : Office2007Form
    {
        public Monitor()
        {
            InitializeComponent();
            this.TopLevel = false;

            label1.MouseDown += new MouseEventHandler(FormMove);
            label2.MouseDown += new MouseEventHandler(FormMove);
            label4.MouseDown += new MouseEventHandler(FormMove);
            label5.MouseDown += new MouseEventHandler(FormMove);
            label3.MouseDown += new MouseEventHandler(FormMove);
            label5.MouseDown += new MouseEventHandler(FormMove);
            label7.MouseDown += new MouseEventHandler(FormMove);
            label9.MouseDown += new MouseEventHandler(FormMove);
            label11.MouseDown += new MouseEventHandler(FormMove);
            label13.MouseDown += new MouseEventHandler(FormMove);
            label15.MouseDown += new MouseEventHandler(FormMove);
            label17.MouseDown += new MouseEventHandler(FormMove);
            Lab_Ua.MouseDown += new MouseEventHandler(FormMove);
            Lab_Ia.MouseDown += new MouseEventHandler(FormMove);
            Lab_Pa.MouseDown += new MouseEventHandler(FormMove);
            Lab_Ub.MouseDown += new MouseEventHandler(FormMove);
            Lab_Ib.MouseDown += new MouseEventHandler(FormMove);
            Lab_Pb.MouseDown += new MouseEventHandler(FormMove);
            Lab_Uc.MouseDown += new MouseEventHandler(FormMove);
            Lab_Ic.MouseDown += new MouseEventHandler(FormMove);
            Lab_Pc.MouseDown += new MouseEventHandler(FormMove);
            lab_WP.MouseDown += new MouseEventHandler(FormMove);
            lab_WQ.MouseDown += new MouseEventHandler(FormMove);
            lab_WS.MouseDown += new MouseEventHandler(FormMove);

            this.Load += new EventHandler(Monitor_Load);
        }


        void Monitor_Load(object sender, EventArgs e)
        {
            setColor(Color.Yellow, Color.Green, Color.Red);
            Btn_Expend_Click(sender, e);
        }

        private void setColor(Color color, Color color_2, Color color_3)
        {
            //A
            this.label1.ForeColor = color;
            this.Lab_Ua.ForeColor = color;
            this.label3.ForeColor = color;
            this.Lab_Ia.ForeColor = color;
            this.label5.ForeColor = color;
            this.Lab_Pa.ForeColor = color;
            //B
            this.label7.ForeColor = color_2;
            this.label9.ForeColor = color_2;
            this.label11.ForeColor = color_2;
            this.Lab_Ub.ForeColor = color_2;
            this.Lab_Ib.ForeColor = color_2;
            this.Lab_Pb.ForeColor = color_2;
            //B
            this.label13.ForeColor = color_3;
            this.label15.ForeColor = color_3;
            this.label17.ForeColor = color_3;
            this.Lab_Uc.ForeColor = color_3;
            this.Lab_Ic.ForeColor = color_3;
            this.Lab_Pc.ForeColor = color_3;
        }


        #region 窗体拖曳
        public const int WM_SYSCOMMAND = 0x0112;
        [DllImport("user32.dll")]
        public extern static bool ReleaseCapture();
        private void FormMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            CLDC_Comm.Win32Api.SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, 0xF017, 0);
        }
        #endregion

        #region 属性赋值
        /// <summary>
        /// A相电压，如果没有则传入一个空字符串
        /// </summary>
        public string Ua
        {
            get
            {
                if (Lab_Ua.Text == "")
                    return "";
                else
                    return Lab_Ua.Text;
            }
            set
            {
                if (value == null || value == "")
                    Lab_Ua.Text = "";
                else
                    Lab_Ua.Text = value;
            }
        }
        /// <summary>
        /// A相电压，如果没有则传入一个空字符串
        /// </summary>
        public string Ub
        {
            get
            {
                if (Lab_Ub.Text == "")
                    return "";
                else
                    return Lab_Ub.Text;
            }
            set
            {
                if (value == null || value == "")
                    Lab_Ub.Text = "";
                else
                    Lab_Ub.Text = value;
            }
        }
        /// <summary>
        /// B相电压，如果没有则传入一个空字符串
        /// </summary>
        public string Uc
        {
            get
            {
                if (Lab_Uc.Text == "")
                    return "";
                else
                    return Lab_Uc.Text;
            }
            /// <summary>
            /// C相电压，如果没有则传入一个空字符串
            /// </summary>
            set
            {
                if (value == null || value == "")
                    Lab_Uc.Text = "";
                else
                    Lab_Uc.Text = value;
            }
        }

        /// <summary>
        /// A相电流，如果没有则传入一个空字符串
        /// </summary>
        public string Ia
        {
            get
            {
                if (Lab_Ia.Text == "")
                    return "";
                else
                    return Lab_Ia.Text;
            }
            set
            {
                if (value == null || value == "")
                    Lab_Ia.Text = "";
                else
                    Lab_Ia.Text = value;
            }
        }
        /// <summary>
        /// B相电流，如果没有则传入一个空字符串
        /// </summary>
        public string Ib
        {
            get
            {
                if (Lab_Ib.Text == "")
                    return "";
                else
                    return Lab_Ib.Text;
            }
            set
            {
                if (value == null || value == "")
                    Lab_Ib.Text = "";
                else
                    Lab_Ib.Text = value;
            }
        }
        /// <summary>
        /// C相电流，如果没有则传入一个空字符串
        /// </summary>
        public string Ic
        {
            get
            {
                if (Lab_Ic.Text == "")
                    return "";
                else
                    return Lab_Ic.Text;
            }
            /// <summary>
            /// C相电压，如果没有则传入一个空字符串
            /// </summary>
            set
            {
                if (value == null || value == "")
                    Lab_Ic.Text = "";
                else
                    Lab_Ic.Text = value;
            }
        }
        /// <summary>
        /// A相角度，如果没有则传入一个空字符串
        /// </summary>
        public string Phia
        {
            get
            {
                if (Lab_Pa.Text == "")
                    return "";
                else
                    return Lab_Pa.Text;
            }
            set
            {
                if (value == null || value == "")
                    Lab_Pa.Text = "";
                else
                    Lab_Pa.Text = value;
            }
        }
        /// <summary>
        /// B相角度，如果没有则传入一个空字符串
        /// </summary>
        public string Phib
        {

            get
            {
                if (Lab_Pb.Text == "")
                    return "";
                else
                    return Lab_Pb.Text;
            }
            set
            {
                if (value == null || value == "")
                    Lab_Pb.Text = "";
                else
                    Lab_Pb.Text = value;
            }
        }
        /// <summary>
        /// C相角度，如果没有则传入一个空字符串
        /// </summary>
        public string Phic
        {
            get
            {
                if (Lab_Pc.Text == "")
                    return "";
                else
                    return Lab_Pc.Text;
            }
            /// <summary>
            /// C相电压，如果没有则传入一个空字符串
            /// </summary>
            set
            {
                if (value == null || value == "")
                    Lab_Pc.Text = "";
                else
                    Lab_Pc.Text = value;
            }
        }
        /// <summary>
        /// 有功功率，如果没有则传入一个空字符串
        /// </summary>
        public string P
        {

            get
            {
                if (lab_WP.Text == "")
                    return "";
                else
                    return lab_WP.Text;
            }
            set
            {
                if (value == null || value == "")
                    lab_WP.Text = "";
                else
                    lab_WP.Text = value;
            }
        }
        /// <summary>
        /// 无功功率，如果没有则传入一个空字符串
        /// </summary>
        public string Q
        {

            get
            {
                if (lab_WQ.Text == "")
                    return "";
                else
                    return lab_WQ.Text;
            }
            set
            {
                if (value == null || value == "")
                    lab_WQ.Text = "";
                else
                    lab_WQ.Text = value;
            }
        }
            /// <summary>
        /// 视在功率，如果没有则传入一个空字符串
        /// </summary>
        public string S
        {

            get
            {
                if (lab_WS.Text == "")
                    return "";
                else
                    return lab_WS.Text;
            }
            set
            {
                if (value == null || value == "")
                    lab_WS.Text = "";
                else
                    lab_WS.Text = value;
            }
        }
        #endregion
        /// <summary>
        /// 设置显示数据
        /// </summary>
        /// <param name="tagPower"></param>
        public void SetMonitorData(CLDC_DataCore.Struct.StPower tagPower)
        {
            MonitorConfig = tagPower;
        }


        /// <summary>
        /// 根据Power结构体获取和设置监视器
        /// </summary>
        /// 

        private CLDC_DataCore.Struct.StPower MonitorConfig
        {

            get
            {
                CLDC_DataCore.Struct.StPower _Power = new CLDC_DataCore.Struct.StPower();
                _Power.Ua = (Ua == "" ? 0F : float.Parse(Ua));
                _Power.Ub = (Ub == "" ? 0F : float.Parse(Ub));
                _Power.Uc = (Uc == "" ? 0F : float.Parse(Uc));
                _Power.Ia = (Ia == "" ? 0F : float.Parse(Ia));
                _Power.Ib = (Ib == "" ? 0F : float.Parse(Ib));
                _Power.Ic = (Ic == "" ? 0F : float.Parse(Ic));
                //_Power.PhiA = (Phia == "" ? 0F : float.Parse(Phia));
                //_Power.PhiB = (Phib == "" ? 0F : float.Parse(Phib));
                //_Power.PhiC = (Phic == "" ? 0F : float.Parse(Phic));
                return _Power;
            }
            set
            {
                Ua = value.Ua.ToString("F5");
                Ub = value.Ub.ToString("F5");
                Uc = value.Uc.ToString("F5");
                Ia = value.Ia.ToString("F5");
                Ib = value.Ib.ToString("F5");
                Ic = value.Ic.ToString("F5");
                Phia = value.Phi_Ia.ToString("F4");
                Phib = value.Phi_Ib.ToString("F4");
                Phic = value.Phi_Ic.ToString("F4");
                P = value.P.ToString("F4");
                Q = value.Q.ToString("F4");
                S = value.S.ToString("F4");
            }
        }

        /// <summary>
        /// 设置单相、三相模式
        ///  DanXiangTai = true 为单相模式，否则为三相模式
        /// </summary>
        public bool DanXiangTai
        {
            set
            {
                if (value)
                {
                    //设置到单相模式
                    label1.Text = "U(V)";
                    label3.Text = "I(A)";
                    label5.Text = "Phi";
                    this.Height = 20;
                }
                else
                {
                    label1.Text = "Ua(V)";
                    label7.Text = "Ub(V)";
                    label13.Text = "Uc(V)";
                    label3.Text = "Ia(A)";
                    label9.Text = "Ib(A)";
                    label15.Text = "Ic(A)";
                    label5.Text = "Phia";
                    label11.Text = "Phib";
                    label17.Text = "Phic";
                    this.Height = 60;
                }

            }
        }

        public void Btn_Expend_Click(object sender, EventArgs e)
        {
            if (this.Parent == null) return;
            Control CtrlParent = this.Parent;

            /* 只要不是在 收缩 状态、就收缩*/
            int top = 2;
            if (CtrlParent is CLDC_MeterUI.UI_Client)
            {
                top = 29;
            }

            //控件默认加载时展开显示
            if (sender is CLDC_MeterUI.UI_Detection_New.Main)
            {
                this.Location = new Point(CtrlParent.Width - this.Width - 12, top);
                return;
            }

            if (this.Location.X == CtrlParent.Width - 30
                && this.Location.Y == top )
            {
                //当前在收缩 状态 -> 展开
                this.Location = new Point(CtrlParent.Width - this.Width - 12, top);
            }
            else
            {
                //非 收缩 状态  -> 收缩
                this.Location = new Point(CtrlParent.Width - 30, top);
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class DgnReadDl : DgnBase
    {
        public DgnReadDl()
        {
            InitializeComponent();
        }

        public DgnReadDl(CLDC_DataCore.Struct.StDgnConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }
        
        public override Color PanelBackColor
        {
            set
            {
                Panel_Back.BackColor = value;
            }
        }

        public override Color CaptionColor
        {
            set
            {
                Panel_Back.CaptionColorOne = value;
            }
        }

        public override Color CaptionColorTwo
        {
            set
            {
                Panel_Back.CaptionColorTwo = value;
            }
        }

        public override CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                base.Parm = string.Format("{0}{1}{2}{3}{4}|{5}{6}{7}{8}{9}{10}{11}{12}"
                                       , Chk_Zong.Checked == true ? "1" : "0"
                                       , Chk_Feng.Checked == true ? "1" : "0"
                                       , Chk_Ping.Checked == true ? "1" : "0"
                                       , Chk_Gu.Checked == true ? "1" : "0"
                                       , Chk_Jian.Checked == true ? "1" : "0"
                                       , Chk_Pz.Checked == true ? "1" : "0"
                                       , Chk_Pf.Checked == true ? "1" : "0"
                                       , Chk_Qz.Checked == true ? "1" : "0"
                                       , Chk_Qf.Checked == true ? "1" : "0"
                                       , Chk_Q1.Checked == true ? "1" : "0"
                                       , Chk_Q2.Checked == true ? "1" : "0"
                                       , Chk_Q3.Checked == true ? "1" : "0"
                                       , Chk_Q4.Checked == true ? "1" : "0");

                if (base.Parm.Split('|')[0] == "00000" || base.Parm.Split('|')[1] == "00000000")
                {
                    base._IsCheck = false;
                }
                return base.DgnPlanPrj;
            }
        }

        public override string Parm
        {
            get
            {
                return base.Parm;
            }
            set
            {
                string[] _Arr = value.Split('|');
                if (_Arr != null)
                {
                    Chk_Zong.Checked = _Arr[0].Substring(0, 1) == "1" ? true : false;        //×Ü
                    Chk_Feng.Checked = _Arr[0].Substring(1, 1) == "1" ? true : false;        //·å
                    Chk_Ping.Checked = _Arr[0].Substring(2, 1) == "1" ? true : false;        //Æ½
                    Chk_Gu.Checked = _Arr[0].Substring(3, 1) == "1" ? true : false;          //¹È
                    Chk_Jian.Checked = _Arr[0].Substring(4, 1) == "1" ? true : false;        //¼â
                    Chk_Pz.Checked = _Arr[1].Substring(0, 1) == "1" ? true : false;          //P+
                    Chk_Pf.Checked = _Arr[1].Substring(1, 1) == "1" ? true : false;          //P-
                    Chk_Qz.Checked = _Arr[1].Substring(2, 1) == "1" ? true : false;          //Q+    
                    Chk_Qf.Checked = _Arr[1].Substring(3, 1) == "1" ? true : false;          //Q-
                    Chk_Q1.Checked = _Arr[1].Substring(4, 1) == "1" ? true : false;          //Q1
                    Chk_Q2.Checked = _Arr[1].Substring(5, 1) == "1" ? true : false;          //Q2
                    Chk_Q3.Checked = _Arr[1].Substring(6, 1) == "1" ? true : false;          //Q3
                    Chk_Q4.Checked = _Arr[1].Substring(7, 1) == "1" ? true : false;          //Q4
                }
            }
        }


        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_Dgn FaItem)
        {
            base.LoadFA(FaItem);
            if (base.Parm == string.Empty)
            {
                return;
            }
            this.Parm = base.Parm;
        }
    }
}

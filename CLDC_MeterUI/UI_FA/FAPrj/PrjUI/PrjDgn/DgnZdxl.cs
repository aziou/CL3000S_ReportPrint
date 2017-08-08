using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class DgnZdxl: DgnBase 
    {
        public DgnZdxl()
        {
            InitializeComponent();
        }

        public DgnZdxl(CLDC_DataCore.Struct.StDgnConfig Item)
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
                this.ParmOK();
                base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}", Txt_Xlzq.Text   //需量周期
                                       , Txt_Hcsj.Text      //华差时间
                                       , Txt_Hccs.Text      //划差次数
                                       , Chk_Zqwc.CheckState == CheckState.Checked ? "1" : "0"       //周期误差
                                       , string.Format("{0}{1}{2}{3}"
                                       , Chk_XlPz.CheckState == CheckState.Checked ? "1" : "0"
                                       , Chk_XlPf.CheckState == CheckState.Checked ? "1" : "0"
                                       , Chk_XlQz.CheckState == CheckState.Checked ? "1" : "0"
                                       , Chk_XlQf.CheckState == CheckState.Checked ? "1" : "0"));        //检定方向

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
                    Txt_Xlzq.Text = _Arr[0];           //需量周期
                    Txt_Hcsj.Text = _Arr[1];           //划差时间
                    Txt_Hccs.Text = _Arr[2];           //划差次数
                    Chk_Zqwc.CheckState = _Arr[3] == "1" ? CheckState.Checked : CheckState.Unchecked;
                    try
                    {
                        Chk_XlPz.Checked = _Arr[4].Substring(0, 1) == "1" ? true : false;
                        Chk_XlPf.Checked = _Arr[4].Substring(1, 1) == "1" ? true : false;
                        Chk_XlQz.Checked = _Arr[4].Substring(2, 1) == "1" ? true : false;
                        Chk_XlQf.Checked = _Arr[4].Substring(3, 1) == "1" ? true : false;
                    }
                    catch
                    {
                        Chk_XlPz.Checked = true;
                        Chk_XlPf.Checked = false;
                        Chk_XlQz.Checked = false;
                        Chk_XlQf.Checked = false;
                    }
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

        private void ParmOK()
        {
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Xlzq.Text))
            {
                Txt_Xlzq.Text = "15";
            }

            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Hcsj.Text))
            {
                Txt_Hcsj.Text = "1";
            }

            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Hccs.Text))
            {
                Txt_Hccs.Text = "1";
            }

            if (Chk_XlPz.CheckState == CheckState.Unchecked && Chk_XlPf.CheckState == CheckState.Unchecked && Chk_XlQz.CheckState == CheckState.Unchecked && Chk_XlQf.CheckState == CheckState.Unchecked)
            {
                Chk_XlPz.CheckState = CheckState.Checked;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze
{
    public partial class FreezeTiming : FreezeBase
    {
        public FreezeTiming()
        {
            InitializeComponent();
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

        public FreezeTiming(CLDC_DataCore.Struct.StFreezeConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = this.Panel_Back;
        }

        public override CLDC_DataCore.Struct.StPlan_Freeze FreezePlanPrj
        {
            get
            {
                this.ParmOK();
                 
                base.Parm = string.Format("{0}|{1}|{2}"
                                       , chk_Month.CheckState == CheckState.Checked ? "1" : "0"
                                       , chk_Day.CheckState == CheckState.Checked ? "1" : "0"
                                       , chkHour.CheckState == CheckState.Checked ? "1" : "0");
                return base.FreezePlanPrj;
            }
        }

        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_Freeze FaItem)
        {
            base.LoadFA(FaItem);
            if (base.Parm == string.Empty)
            {
                return;
            }
            this.Parm = base.Parm;
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
                    try
                    {
                        chk_Month.Checked = _Arr[0] == "1" ? true : false;
                        chk_Day.Checked = _Arr[1] == "1" ? true : false;
                        chkHour.Checked = _Arr[2] == "1" ? true : false;
                    }
                    catch
                    {
                        chk_Month.Checked = true;
                        chk_Day.Checked = false;
                        chkHour.Checked = false;
                    }
                }
            }
        }

        private void ParmOK()
        {
            if (chk_Month.CheckState == CheckState.Unchecked && chk_Day.CheckState == CheckState.Unchecked && chkHour.CheckState == CheckState.Unchecked)
            {
                chk_Month.CheckState = CheckState.Checked;
            }
        }
    }
}

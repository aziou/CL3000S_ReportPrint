using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public partial class DgnChangePassword : DgnBase
    {
        public DgnChangePassword()
            : base()
        {
            InitializeComponent();
        }
        public DgnChangePassword(CLDC_DataCore.Struct.StDgnConfig Item)
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
        public override CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                base.Parm = txtWarnMoney1.Text + "|" + txtWarnMoney2.Text;
                
                base._IsCheck = Panel_Back.Checked;
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
                if (_Arr != null && _Arr.Length >= 2)
                {
                    txtWarnMoney1.Text = _Arr[0];
                    txtWarnMoney2.Text = _Arr[1];                    
                }
                base.Parm = value;
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

        private void txtWarnMoney1_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text != "")
            {
                char[] chr = t.Text.ToCharArray();
                char c = chr[chr.Length - 1];
                if (((c <'0' || c >'9')&&c!='.') || (c == '.' && t.Text.IndexOf(c) != t.Text.LastIndexOf(c)))
                {
                    t.Text = t.Text.Remove(t.Text.Length - 1);
                    t.SelectionStart = t.Text.Length;
                }
            }
        }
    }
}

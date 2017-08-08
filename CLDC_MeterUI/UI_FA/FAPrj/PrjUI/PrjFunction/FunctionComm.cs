using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction
{
    public partial class FunctionComm : FunctionBase 
    {
        public FunctionComm()
        {
            InitializeComponent();
        }

        public FunctionComm(CLDC_DataCore.Struct.StFunctionConfig Item)
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
        
        public override CLDC_DataCore.Struct.StPlan_Function FunctionPlanPrj
        {
            get
            {
                this.ParmOK();
                base.Parm = string.Format("{0}|{1}|{2}", Txt_Keep.Text   //走字时间                                       
                                       , Txt_TestNum.Text      //试验次数                                       
                                       , string.Format("{0}{1}"                        
                                       , Chk_P.CheckState == CheckState.Checked ? "1" : "0"
                                       , Chk_Q.CheckState == CheckState.Checked ? "1" : "0"));        //功率方向

                return base.FunctionPlanPrj;
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
                    Txt_Keep.Text = _Arr[0];           //保持时间                    
                    Txt_TestNum.Text = _Arr[1];           //试验次数                    
                    try
                    {                        
                        Chk_P.Checked = _Arr[2].Substring(0, 1) == "1" ? true : false;
                        Chk_Q.Checked = _Arr[2].Substring(1, 1) == "1" ? true : false;                        
                    }
                    catch
                    {
                        Chk_P.Checked = true;
                        Chk_Q.Checked = false;                        
                    }
                }
            }
        }


        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_Function FaItem)
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
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Keep.Text))
            {
                Txt_Keep.Text = "1";
            }


            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_TestNum.Text))
            {
                Txt_TestNum.Text = "1";
            }

            if (Chk_P.CheckState == CheckState.Unchecked && Chk_Q.CheckState == CheckState.Unchecked)
            {
                Chk_P.CheckState = CheckState.Checked;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel_Back_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

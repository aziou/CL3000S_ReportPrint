using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog
{
    public partial class ELLoseComm : EventLogBase 
    {
        public ELLoseComm()
        {
            InitializeComponent();
        }

        public ELLoseComm(CLDC_DataCore.Struct.StEventLogConfig Item)
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
        
        public override CLDC_DataCore.Struct.StPlan_EventLog EventLogPlanPrj
        {
            get
            {
                this.ParmOK();
                base.Parm = string.Format("{0}|{1}|{2}", Txt_Keep.Text   //保持时间
                                       , Txt_Recover.Text      //恢复等待时间
                                       , Txt_TestNum.Text      //试验次数                                       
                                       ); 

                return base.EventLogPlanPrj;
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
                    Txt_Recover.Text = _Arr[1];        //恢复等待时间
                    Txt_TestNum.Text = _Arr[2];           //试验次数                    
                    
                }
            }
        }


        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_EventLog FaItem)
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

            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Recover.Text))
            {
                Txt_Recover.Text = "1";
            }

            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_TestNum.Text))
            {
                Txt_TestNum.Text = "1";
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

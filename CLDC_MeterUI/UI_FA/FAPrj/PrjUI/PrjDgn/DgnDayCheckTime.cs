using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class DgnDayCheckTime : DgnBase//UserControl
    {
        public DgnDayCheckTime():base()
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
        public DgnDayCheckTime(CLDC_DataCore.Struct.StDgnConfig Item):base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }

        public override CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                base.Parm=CLDC_DataCore.Function.Number.IsNumeric(Txt_Wcx.Text)?Txt_Wcx.Text:"1";
                base.Parm += ("|" + (CLDC_DataCore.Function.Number.IsNumeric(TxtWcCount.Text) ? TxtWcCount.Text : "5"));
                base.Parm += ("|" + (CLDC_DataCore.Function.Number.IsNumeric(TxtTestCount.Text) ? TxtTestCount.Text : "60"));
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
                try 
                {
                    string[] str = value.Split('|');
                    Txt_Wcx.Text = str[0];
                    TxtWcCount.Text = str[1];
                    TxtTestCount.Text = str[2];
                }
                catch (Exception ex)
                {
                    Txt_Wcx.Text = "1";
                    TxtWcCount.Text = "5";
                    TxtTestCount.Text = "60";
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
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

        private void TxtWcCount_Leave(object sender, EventArgs e)
        {
            int WcCount;
            if (int.TryParse(TxtWcCount.Text, out WcCount))
            {
 
            }
            if (WcCount > 10)
                TxtWcCount.Text = "10";

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class NotParmPrj : DgnBase  // UserControl
    {
        public NotParmPrj()
            : base()
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
        public NotParmPrj(CLDC_DataCore.Struct.StDgnConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }

        public override CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                return base.DgnPlanPrj;
            }
        }

    }
}

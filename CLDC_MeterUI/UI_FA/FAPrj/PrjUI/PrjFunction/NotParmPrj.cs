using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction
{
    public partial class NotParmPrj :FunctionBase  // UserControl
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
        public NotParmPrj(CLDC_DataCore.Struct.StFunctionConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }

        public override CLDC_DataCore.Struct.StPlan_Function FunctionPlanPrj
        {
            get
            {
                return base.FunctionPlanPrj;
            }
        }

    }
}

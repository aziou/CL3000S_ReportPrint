using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjPrePare
{
    public partial class NotParmPrj : PreBase
    {
        public NotParmPrj()
            : base()
        {
            InitializeComponent();

        }

        public NotParmPrj(CLDC_DataCore.Struct.StDgnConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }
        public NotParmPrj(string PlanName)
            : base(PlanName)
        {

            InitializeComponent();
            base.SetPanel = Panel_Back;
            switch (PlanName)
            {
                case "正向有功":
                    {
                        _ErrPoint.PrjID = "111010700";
                        _ErrPoint.PrjName =PlanName+ "接线调试";
                        _ErrPoint.PowerDianLiu = "1.0Ib";
                        _ErrPoint.PowerYinSu = "1.0";                       
                        break;
                    }
                case "正向无功":
                    {
                        _ErrPoint.PrjID = "131010700";
                        _ErrPoint.PrjName = PlanName + "接线调试";
                        _ErrPoint.PowerDianLiu = "1.0Ib";
                        _ErrPoint.PowerYinSu = "1.0";
                        break;
                    }
                case "反向无功":
                    {
                        _ErrPoint.PrjID = "141010700";
                        _ErrPoint.PrjName = PlanName + "接线调试";
                        _ErrPoint.PowerDianLiu = "1.0Ib";
                        _ErrPoint.PowerYinSu = "1.0";
                        break;
                    }
                case "反向有功":
                    {
                        _ErrPoint.PrjID = "121010700";
                        _ErrPoint.PrjName = PlanName + "接线调试";
                        _ErrPoint.PowerDianLiu = "1.0Ib";
                        _ErrPoint.PowerYinSu = "1.0";
                        break;
                    }
            }
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
        

        public override CLDC_DataCore.Struct.StPlan_PrePareTest DgnPlanPrj
        {
            get
            {
                return base.DgnPlanPrj;
            }
        }

    }
}

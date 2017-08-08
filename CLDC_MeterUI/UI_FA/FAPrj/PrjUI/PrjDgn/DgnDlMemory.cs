using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class DgnDlMemory : DgnBase 
    {
        public DgnDlMemory():base()
        {
            InitializeComponent();
        }

        public DgnDlMemory(CLDC_DataCore.Struct.StDgnConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = this.Panel_Back;
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
        /// <summary>
        /// 获取多功能检定项目
        /// </summary>
        public override CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                this.ParmOK();
                base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}", Txt_formula.Text
                                           , Txt_Q1.Text
                                           , Txt_Q2.Text
                                           , Txt_Q3.Text
                                           , Txt_Q4.Text);
                return base.DgnPlanPrj;
            }
        }
        /// <summary>
        /// 录入的数据进行验证
        /// </summary>
        private void ParmOK()
        {
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Q1.Text))
            {
                Txt_Q1.Text = "1";
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Q2.Text))
            {
                Txt_Q2.Text = "2";
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Q3.Text))
            {
                Txt_Q3.Text = "3";
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Q4.Text))
            {
                Txt_Q4.Text="4";
            }
            if (Txt_formula.Text == "")
            {
                Txt_formula.Text="1+4";
            }
        
        }
        /// <summary>
        /// 设置或获取多功能试验参数
        /// </summary>
        public override string Parm
        {
            get
            {
                return base.Parm;
            }
            set
            {
                string[] Tmp_Arr = value.Split('|');
                try
                {
                    Txt_formula.Text = Tmp_Arr[0];
                    Txt_Q1.Text = Tmp_Arr[1];
                    Txt_Q2.Text = Tmp_Arr[2];
                    Txt_Q3.Text = Tmp_Arr[3];
                    Txt_Q4.Text = Tmp_Arr[4];
                }
                catch
                { }
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

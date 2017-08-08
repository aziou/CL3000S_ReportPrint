using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Model.Plan;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_Insulation : UI_TableBase
    {
        private Plan_Insulation planInsulation;

        //public UI_Insulation()
        //{
        //    InitializeComponent();
        //    this.InitUI();
        //}

        public UI_Insulation(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            this.InitUI();

        }

        public UI_Insulation(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_Insulation(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_Insulation FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);
        }

        ///// <summary>
        ///// 加载测试方案的构造函数
        ///// </summary>
        //public UI_Insulation(Plan_Insulation plan)
        //{
        //    InitializeComponent();
        //    InitUI();
        //    planInsulation = plan;
        //    RefreshPlan();
        //}

        /// <summary>
        /// 加载三个试验项
        /// </summary>
        private void InitUI()
        {
            container.Controls.Clear();
            //for (int i = 0; i <3; i ++)
            for (int i = 0; i < 2; i++)
            {
                FAPrj.PrjUI.UI_InsulationItem insulationItem=new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_InsulationItem();
                insulationItem.InsulationParam.InsulationType=(CLDC_DataCore.Struct.StInsulationParam.EnumInsulationType)i;
                insulationItem.LoadVariable();
                container.Controls.Add(insulationItem,0,1);
                insulationItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void RefreshPlan()
        {
            foreach (PrjUI.UI_InsulationItem uiItem in container.Controls)
            {
                uiItem.IsChecked = false;
            }
            for (int i = 0; i < planInsulation.Count; i++)
            {
                //add by zxr 处理无数据异常
                if (planInsulation.GetPlan(i) == null)
                {
                    continue;
                }
                //
                if (container.Controls != null && container.Controls.Count > ((int)planInsulation.GetPlan(i).InsulationType))
                {
                    PrjUI.UI_InsulationItem uiItem = container.Controls[((int)planInsulation.GetPlan(i).InsulationType)] as PrjUI.UI_InsulationItem;
                    uiItem.InsulationParam = planInsulation.GetPlan(i);
                    uiItem.IsChecked = true;
                }
            }
        }

        /// <summary>
        /// 根据方案的内容更新界面
        /// </summary>
        /// <param name="plan">方案的内容</param>
        public void LoadFA(Plan_Insulation plan)
        {
            planInsulation = plan;
            RefreshPlan();
        }

        /// <summary>
        /// 从文件加载方案内容，再更新界面
        /// </summary>
        /// <param name="faName"></param>
        public void LoadFA(string faName)
        {
            LoadFA(new Plan_Insulation((int)base.TaiType, faName));
        }

        public Plan_Insulation GetPlan()
        {
            planInsulation.RemoveAll();
            foreach (PrjUI.UI_InsulationItem uiItem in container.Controls)
            {
                if (uiItem.IsChecked)
                {
                    planInsulation.Add(uiItem.InsulationParam);
                }
            }
            return planInsulation;
        }

    }
}

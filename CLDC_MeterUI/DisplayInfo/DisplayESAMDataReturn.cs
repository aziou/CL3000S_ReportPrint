using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    public partial class DisplayESAMDataReturn : Base
    {
        /// <summary>
        /// 读取ESAM数据项目ID
        /// </summary>
        private const string Key = "007";
                
        public DisplayESAMDataReturn()
        {
            InitializeComponent();
        }
        public DisplayESAMDataReturn(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">要求传入时计度器示值组合误差方案</param>
        public DisplayESAMDataReturn(CLDC_DataCore.Struct.StPlan_CostControl CostPlan)
        {
            InitializeComponent();
            if (CostPlan.CostControlPrjID != Key)        //如果项目ID不是计度器示值组合误差的ID则退出！！
                return;

            int _ColIndex = Data_View.Columns.Add("Data_Z", "ESAM数据内容");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            if (MeterInfo.MeterDgns.Count == 0) return;
            Data_View.Rows.Clear();

            string _Key = Key + "01";

            if (MeterInfo.MeterCostControls.ContainsKey(_Key))
            {
                if (MeterInfo.MeterCostControls[_Key].Mfk_chrJL != null && MeterInfo.MeterCostControls[_Key].Mcc_PrjName != string.Empty)
                {
                    int rowIndex = Data_View.Rows.Add();
                    Data_View["表位", rowIndex].Value = MeterInfo.ToString();
                    Data_View["ESAM数据内容", rowIndex].Value = MeterInfo.MeterCostControls[_Key].Mfk_chrData;
                }
            }

            base.SetData(MeterInfo, allowedit);
        }       
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            if (MeterGroup.MeterGroup.Count == 0) return;
            Data_View.Rows.Clear();

            string _Key = Key + "01";
            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;  
                if (MeterGroup.MeterGroup[i].MeterCostControls.ContainsKey(_Key))
                {
                    if (MeterGroup.MeterGroup[i].MeterCostControls[_Key].Mcc_PrjName != null && MeterGroup.MeterGroup[i].MeterCostControls[_Key].Mfk_chrData != string.Empty)
                    {                        
                        int rowIndex = Data_View.Rows.Add();
                        Data_View["表位", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();
                        Data_View["ESAM数据内容", rowIndex].Value = MeterGroup.MeterGroup[i].MeterCostControls[_Key].Mfk_chrData;                        
                    }
                }
            }
            base.SetData(MeterGroup, allowedit);
        }       

    }
}

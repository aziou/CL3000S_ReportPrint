using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CostControlDataView
{
    /// <summary>
    /// 切换本地模式
    /// </summary>
    public partial class ViewChangeLocalMode : UserControl
    {

        private const string Key = "024";

        public ViewChangeLocalMode()
        {
            InitializeComponent();
        }

        public ViewChangeLocalMode(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CostControlPlan">切换本地模式</param>
        public ViewChangeLocalMode(CLDC_DataCore.Struct.StPlan_CostControl CostControlPlan)
        {
            InitializeComponent();
            if (CostControlPlan.CostControlPrjID != Key)        //如果项目ID不是切换本地模式则退出！！
                return;

            int _ColIndex = Data_View.Columns.Add("Data_Z", "费控模式状态字");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count)           //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
            {
                Data_View.Rows.Clear();
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    int RowIndex = Data_View.Rows.Add();
                    if ((RowIndex + 1) % 2 == 0)
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup[i].ToString();
                }
                Data_View.Refresh();
            }


            for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {  
                string _Key = Key;
                if (!MeterGroup[i].YaoJianYn) continue;
                if (!MeterGroup[i].MeterCostControls.ContainsKey(_Key))
                {
                    Data_View.Rows[i].Cells[0].Value = "";    
                    continue;
                }
                if (MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == null || MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == string.Empty) continue;

                Data_View.Rows[i].Cells[0].Value = MeterGroup[i].MeterCostControls[_Key].Mfk_chrData; 
            }
        
        }
    }
}

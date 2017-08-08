using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CostControlDataView
{
    public partial class ViewWaringData : UserControl
    {
        /// <summary>
        /// 读取保电功能数据项目ID
        /// </summary>
        private const string Key = "003";
              
        public ViewWaringData()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CostControlPlan">要求传入保电功能方案</param>
        public ViewWaringData(CLDC_DataCore.Struct.StPlan_CostControl CostControlPlan)
        {
            InitializeComponent();
            if (CostControlPlan.CostControlPrjID != Key)        //如果项目ID不是保电功能的ID则退出！！
                return;

            int _ColIndex = Data_View.Columns.Add("Data_Z", "  报警  ");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "报警状态字");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "  解除报警  ");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "解除报警状态字");
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


            for (int i = 0; i < MeterGroup.Count; i++)         
            {
                for (int j = 1; j <= 4; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    if (!MeterGroup[i].YaoJianYn) continue;
                    if (!MeterGroup[i].MeterCostControls.ContainsKey(_Key))
                    {
                        Data_View.Rows[i].Cells[j - 1].Value = "";
                        continue;
                    }
                    if (MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == null || MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == string.Empty) continue;

                    Data_View.Rows[i].Cells[j - 1].Value = MeterGroup[i].MeterCostControls[_Key].Mfk_chrData;
                }
            }
        }
        

    }
}

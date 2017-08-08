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
    /// 透支功能
    /// </summary>
    public partial class ViewOverdrawData : UserControl
    {
        private string Key = "026";

        public ViewOverdrawData()
        {
            InitializeComponent();
        }


        public ViewOverdrawData(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CostControlPlan">透支功能</param>
        public ViewOverdrawData(CLDC_DataCore.Struct.StPlan_CostControl CostControlPlan)
        {
            InitializeComponent();
            if (CostControlPlan.CostControlPrjID != Key)        //如果项目ID不是切换本地模式则退出！！
                return;

            int _ColIndex = Data_View.Columns.Add("Data_Z", "透支电表跳闸"); //1
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "非保电状态透支拉闸");//2
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "设置合闸允许金额并充值电表不应合闸");//3
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "充值令剩余金额大于合闸允许金额电表应合闸");//4
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "设置透支金额限值走字令透支金额小于透支金额电表应跳闸");//5
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "提示按键合闸电表应合闸");//6
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "设置透支金额限值走字令透支金额近似等于透支金额限值电表应跳闸");//7
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "继续走字令透支金额大于透支金额限值电表不应合闸");//8
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "设置合闸允许金额充值令剩余金额小于合闸允许金额电表不应合闸");//9
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;


            //_ColIndex = Data_View.Columns.Add("Data_Z", "充值令剩余金额大于合闸允许金额限值电表应合闸");//10
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
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


            //for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            //{
            //    for (int j = 1; j <= 3; j++)
            //    {
            //        string _Key = Key + j.ToString("D2");
            //        if (!MeterGroup[i].YaoJianYn) continue;
            //        if (!MeterGroup[i].MeterCostControls.ContainsKey(_Key))
            //        {
            //            Data_View.Rows[i].Cells[j - 1].Value = "";
            //            continue;
            //        }
            //        if (MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == null || MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == string.Empty) continue;

            //        Data_View.Rows[i].Cells[j - 1].Value = MeterGroup[i].MeterCostControls[_Key].Mfk_chrData;
            //    }
            //}

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

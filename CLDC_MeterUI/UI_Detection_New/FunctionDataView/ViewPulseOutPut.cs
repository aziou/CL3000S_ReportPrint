using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FunctionDataView
{
    public partial class ViewPulseOutPut : UserControl
    {
        /// <summary>
        /// 脉冲输出数据项目ID
        /// </summary>
        private const string Key = "006";

        private bool bShowXl = false;
              
        public ViewPulseOutPut()
        {
            InitializeComponent();

            CLDC_Comm.Enum.Cus_Clfs _clfs = CLDC_Comm.Enum.Cus_Clfs.单相;
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterFirstInfo = null;
            MeterFirstInfo = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter];
            _clfs = (CLDC_Comm.Enum.Cus_Clfs)MeterFirstInfo.Mb_intClfs;
            if (_clfs == CLDC_Comm.Enum.Cus_Clfs.单相)
                bShowXl = false;
            else
                bShowXl = true;

            int _ColIndex = Data_View.Columns.Add("Data_Z", "表号");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;

            _ColIndex = Data_View.Columns.Add("Data_Z", "电能脉冲输出");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;

            _ColIndex = Data_View.Columns.Add("Data_Z", "秒脉冲输出");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;

            _ColIndex = Data_View.Columns.Add("Data_Z", "投切脉冲输出");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;

            if (bShowXl)
            {
                _ColIndex = Data_View.Columns.Add("Data_Z", "需量脉冲输出");
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }        
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            int iShowCol = 0;
            if (bShowXl)
                iShowCol = 4;
            else
                iShowCol = 3;
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
                Data_View.Rows[i].Cells[0].Value = string.Format("第{0}表位", i + 1);

                for (int j = 1; j <= iShowCol; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    if (!MeterGroup[i].YaoJianYn) continue;
                    if (!MeterGroup[i].MeterFunctions.ContainsKey(_Key))
                    {                        
                        Data_View.Rows[i].Cells[j].Value = "";
                        continue;
                    }
                    if (MeterGroup[i].MeterFunctions[_Key].Mf_chrValue == null || MeterGroup[i].MeterFunctions[_Key].Mf_chrValue == string.Empty) continue;
                                        
                    Data_View.Rows[i].Cells[j].Value = MeterGroup[i].MeterFunctions[_Key].Mf_chrValue;                    
                }
            }
        }
        

    }
}

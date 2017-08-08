using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewRatePeriod : UserControl
    {
        /// <summary>
        /// 费率时段示值误差项目ID
        /// </summary>
        private const string Key = "006";

        private int _FirstColIndex = 0;

        public ViewRatePeriod()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">要求传入费率时段电能示值误差方案</param>
        public ViewRatePeriod(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            InitializeComponent();
            //if (DgnPlan.DgnPrjID != Key)        //如果项目ID不是费率时段示值误差的ID则退出！！
            //    return;

            string[] _Values = DgnPlan.PrjParm.Split('|');
            if (_Values.Length == -1 || _Values.Length == 0)
            {
                return;
            }
            int _VLength=_Values.Length;
            if (_Values[_VLength - 1].Length == 4)
            {
                _VLength = _VLength - 2;
            }
            else if (_Values[_VLength - 1].Length == 1)
            {
                _VLength = _VLength - 1;
            }

            for (int i = 0; i < _VLength; i++)            //动态创建数据表单样式
            {
                string _SDName = _Values[i].Substring(_Values[i].IndexOf('(') + 1).Replace(")", "");     //从字符串中解析出费率名

                int ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("试验前({0})电量", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("试验后({0})电量", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("试验前(总)电量", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("试验后(总)电量", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("({0})误差", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

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
                    if ((RowIndex + 1) % 4 == 0)
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

            if ((Data_View.Columns.Count - _FirstColIndex) % 5 != 0) return;     //如果列数不是3的整数倍则退出，因为每个费率需要有3列数据填充

            for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {                

                for (int j = _FirstColIndex; j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 5; j++)
                {
                    string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 5D + 0);
                    if (!MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        Data_View.Rows[i].Cells[_ColIndex].Value = "";              //试验前分费率电量
                        _ColIndex++;
                        Data_View.Rows[i].Cells[_ColIndex].Value ="";              //试验后分费率电量
                        _ColIndex++;
                        Data_View.Rows[i].Cells[_ColIndex].Value ="";              //试验前总电量
                        _ColIndex++;
                        Data_View.Rows[i].Cells[_ColIndex].Value = "";              //试验后总电量
                        _ColIndex++;
                        Data_View.Rows[i].Cells[_ColIndex].Value = "";              //误差
                        continue;
                    }
                    if (MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values = MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');
                    if (_Values.Length != 6) continue;          //分割出来的数据数组应该是6个元素，试验前分费率电量|试验后分费率电量|试验前总电量|试验后总电量|误差|费率

                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[0];              //试验前分费率电量
                    _ColIndex++;
                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[1];              //试验后分费率电量
                    _ColIndex++;
                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[2];              //试验前总电量
                    _ColIndex++;
                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[3];              //试验后总电量
                    _ColIndex++;
                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[4];              //误差
                }
            }
        }

        private int getFeiDlID(string FeiLvName)
        {
            switch (FeiLvName)
            {
                case "峰":
                    return 1;
                case "平":
                    return 2;
                case "谷":
                    return 3;
                case "尖":
                    return 4;
                default:
                    return 0;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    /// <summary>
    /// 详细数据-费率时段示值误差
    /// </summary>
    public partial class DisplayRatePeriod : Base
    {
        /// <summary>
        /// 费率时段示值误差项目ID
        /// </summary>
        private const string Key = "006";

        private int _FirstColIndex = 0;

        public DisplayRatePeriod()
        {
            InitializeComponent();
        }
        public DisplayRatePeriod(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            Data_View.Rows.Clear();
            Data_View.Columns.Clear();
                        
            for (int j = _FirstColIndex; j < _FirstColIndex + (int)(30 - _FirstColIndex) / 5; j++)
            {
                string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 5D + 0);
                if (MeterInfo.MeterDgns.ContainsKey(_Key))
                {
                    if (MeterInfo.MeterDgns[_Key].Md_chrValue == null || MeterInfo.MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values = MeterInfo.MeterDgns[_Key].Md_chrValue.Split('|');
                    if (_Values.Length != 6) continue;          //分割出来的数据数组应该是6个元素，试验前分费率电量|试验后分费率电量|试验前总电量|试验后总电量|误差|费率

                    int ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验前({0})电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验后({0})电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验前(总)电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验后(总)电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("({0})误差", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    if (j == 0)
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
                        Data_View.Rows[RowIndex].HeaderCell.Value = MeterInfo.ToString();
                        Data_View.Refresh();
                    }

                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[0];              //试验前分费率电量
                    _ColIndex++;
                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[1];              //试验后分费率电量
                    _ColIndex++;
                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[2];              //试验前总电量
                    _ColIndex++;
                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[3];              //试验后总电量
                    _ColIndex++;
                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[4];              //误差
                }
            }
            
        }
        public DisplayRatePeriod(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            int intFirstMeter = MeterGroup.GetFirstYaoJianMeterBwh();
            Data_View.Rows.Clear();
            Data_View.Columns.Clear();

            if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns.Count == 0) return;

            for (int j = _FirstColIndex; j < _FirstColIndex + (int)(30 - _FirstColIndex) / 5; j++)
            {
                string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 5D + 0);
                if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns.ContainsKey(_Key))
                {
                    if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values = MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue.Split('|');
                    if (_Values.Length != 6) continue;          //分割出来的数据数组应该是6个元素，试验前分费率电量|试验后分费率电量|试验前总电量|试验后总电量|误差|费率

                    int ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验前({0})电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验后({0})电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验前(总)电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验后(总)电量", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("({0})误差", _Values[5]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;                    
                }
            }
            int IndexCount = 0;
            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                for (int j = _FirstColIndex; j < _FirstColIndex + (int)(30 - _FirstColIndex) / 5; j++)
                {
                    string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 5D + 0);
                    if (MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        if (MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                        string[] _Values = MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');
                        if (_Values.Length != 6) continue;          //分割出来的数据数组应该是6个元素，试验前分费率电量|试验后分费率电量|试验前总电量|试验后总电量|误差|费率

                        if (j == 0)
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
                            Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup.MeterGroup[i].ToString();
                            Data_View.Refresh();
                        }
                        if (Data_View.Rows.Count > 0)
                        {
                            Data_View.Rows[Data_View.Rows.Count - 1].Cells[_ColIndex].Value = _Values[0];              //试验前分费率电量
                            _ColIndex++;
                            Data_View.Rows[Data_View.Rows.Count - 1].Cells[_ColIndex].Value = _Values[1];              //试验后分费率电量
                            _ColIndex++;
                            Data_View.Rows[Data_View.Rows.Count - 1].Cells[_ColIndex].Value = _Values[2];              //试验前总电量
                            _ColIndex++;
                            Data_View.Rows[Data_View.Rows.Count - 1].Cells[_ColIndex].Value = _Values[3];              //试验后总电量
                            _ColIndex++;
                            Data_View.Rows[Data_View.Rows.Count - 1].Cells[_ColIndex].Value = _Values[4];              //误差
                        }
                        
                    }
                }
                IndexCount++;
            }
        }
    }
}

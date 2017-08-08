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
    /// 详细数据-计度器示值组合误差
    /// </summary>
    public partial class DisplayRegister : Base
    {
        /// <summary>
        /// 计度器示值组合误差项目ID
        /// </summary>
        private const string Key = "005";
                
        private int _FirstColIndex = 0;

        public DisplayRegister()
        {
            InitializeComponent();
        }
        public DisplayRegister(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
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
                        
            for (int j = _FirstColIndex; j <= _FirstColIndex + (int)(30 - _FirstColIndex) / 3; j++)
            {
                string _Key = Key + (j - _FirstColIndex).ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                if (MeterInfo.MeterDgns.ContainsKey(_Key))
                {
                    if (MeterInfo.MeterDgns[_Key].Md_chrValue == null || MeterInfo.MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values;
                    if (MeterInfo.MeterDgns[_Key].Md_chrValue.IndexOf('|') != -1)
                        _Values = MeterInfo.MeterDgns[_Key].Md_chrValue.Split('|');
                    else
                    {
                        _Values = new string[1];
                        _Values[0] = MeterInfo.MeterDgns[_Key].Md_chrValue;
                    }
                    if (_Values.Length != 4 && j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率

                    int ColIndex;
                    if (_Values.Length == 4)
                    {
                        ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验前({0})电量", _Values[3]));
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                        ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验后({0})电量", _Values[3]));
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                        ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("({0})电量差值", _Values[3]));
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    else
                    {
                        ColIndex = Data_View.Columns.Add("Data_Z", "组合误差");
                        Data_View.Columns[ColIndex].Tag = 0;
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    if (j == 0)
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
                        Data_View.Rows[RowIndex].HeaderCell.Value = MeterInfo.ToString();

                        Data_View.Refresh();
                    }
                    if (Data_View.Rows[0].Cells.Count > _ColIndex)
                    {
                        Data_View.Rows[0].Cells[_ColIndex].Value = _Values[0];              //起始电量
                        _ColIndex++;
                        if (j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3)
                        {
                            Data_View.Rows[0].Cells[_ColIndex].Value = _Values[1];              //终止电量
                            _ColIndex++;
                            Data_View.Rows[0].Cells[_ColIndex].Value = _Values[2];              //电量差值
                        }
                    }
                }

            }
            
        }
        public DisplayRegister(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
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

            for (int j = _FirstColIndex; j <= _FirstColIndex + (int)(30 - _FirstColIndex) / 3; j++)
            {
                string _Key = Key + (j - _FirstColIndex).ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                
                if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns.ContainsKey(_Key))
                {
                    if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values;
                    if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue.IndexOf('|') != -1)
                        _Values = MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue.Split('|');
                    else
                    {
                        _Values = new string[1];
                        _Values[0] = MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue;
                    }
                    if (_Values.Length != 4 && j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率
                
                    int ColIndex;
                    if (_Values.Length == 4)
                    {
                        ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验前({0})电量", _Values[3]));
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                        ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("试验后({0})电量", _Values[3]));
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                        ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("({0})电量差值", _Values[3]));
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    else
                    {
                        ColIndex = Data_View.Columns.Add("Data_Z", "组合误差");
                        Data_View.Columns[ColIndex].Tag = 0;
                        Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            if (Data_View.Rows.Count != MeterGroup.MeterGroup.Count)           //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
            {
                if (Data_View.ColumnCount == 0) return;
                Data_View.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
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
                    Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup.MeterGroup[i].ToString();
                }
                Data_View.Refresh();
            }
            if (Data_View.Columns.Count == 0) return;
            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                //int RowIndex = Data_View.Rows.Add();
                int j;
                for (j = _FirstColIndex; j <= _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3; j++)
                {
                    string _Key = Key + (j - _FirstColIndex).ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                    if (MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        if (MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                        string[] _Values;
                        if (MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue.IndexOf('|') != -1)
                            _Values = MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');
                        else
                        {
                            _Values = new string[1];
                            _Values[0] = MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue;
                        }
                        if (_Values.Length != 4 && j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率

                        
                        //if (j == 0)
                        //{                            
                        //    if ((RowIndex + 1) % 2 == 0)
                        //    {
                        //        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                        //    }
                        //    else
                        //    {
                        //        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                        //    }
                        //    Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup.MeterGroup[i].ToString();

                        //    Data_View.Refresh();
                        //}
                        if (_Values.Length != 4 && j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3) continue;
                        if (_ColIndex >= Data_View.Columns.Count)
                        {
                            continue;
                        }
                        Data_View.Rows[i].Cells[_ColIndex].Value = _Values[0];              //起始电量
                        _ColIndex++;
                        if (j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3)
                        {
                            Data_View.Rows[i].Cells[_ColIndex].Value = _Values[1];              //终止电量
                            _ColIndex++;
                            Data_View.Rows[i].Cells[_ColIndex].Value = _Values[2];              //电量差值
                        }
                    }

                }
                {
                    string _Key = Key + (j + 1).ToString("D2");
                    if (!MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        Data_View.Rows[i].Cells[Data_View.Columns.Count - 1].Value = "";
                    }
                    else
                    {
                        if (MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue != null)
                        {
                            Data_View.Rows[i].Cells[Data_View.Columns.Count - 1].Value = MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue;
                        }
                    }
                }
            }
        }
    }
}

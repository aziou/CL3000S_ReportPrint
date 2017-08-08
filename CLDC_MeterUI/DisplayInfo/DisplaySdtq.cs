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
    /// 详细数据-时段投切
    /// </summary>
    public partial class DisplaySdtq : Base
    {
        /// <summary>
        /// 时段投切项目ID
        /// </summary>
        private const string Key = "004";

        private int _FirstColIndex = 0;

        public DisplaySdtq()
        {
            InitializeComponent();
        }
        public DisplaySdtq(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
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
            if (MeterInfo.MeterDgns.Count == 0) return;

            
            for (int j = _FirstColIndex; j < _FirstColIndex + (int)(30 - _FirstColIndex) / 3; j++)
            {
                string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                if (MeterInfo.MeterDgns.ContainsKey(_Key))
                {
                    if (MeterInfo.MeterDgns[_Key].Md_chrValue == null || MeterInfo.MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values = MeterInfo.MeterDgns[_Key].Md_chrValue.Split('|');
                    if (_Values.Length != 4) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率

                    int ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("标准投切时间({0})", _Values[3]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("实际投切时间({0})", _Values[3]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("投切误差({0})", _Values[3]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

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
                    }


                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[0];              //标准时间
                    _ColIndex++;
                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[1];              //实际时间
                    _ColIndex++;
                    Data_View.Rows[0].Cells[_ColIndex].Value = _Values[2];              //投切误差，在投切过程中应该显示对应费率电量
                }
            }
            
        }
        public DisplaySdtq(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
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

            for (int j = _FirstColIndex; j < _FirstColIndex + (int)(30 - _FirstColIndex) / 3; j++)
            {
                string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns.ContainsKey(_Key))
                {
                    if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values = MeterGroup.MeterGroup[intFirstMeter].MeterDgns[_Key].Md_chrValue.Split('|');
                    if (_Values.Length != 4) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率
                                        
                    int ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("标准投切时间({0})", _Values[3]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("实际投切时间({0})", _Values[3]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                    ColIndex = Data_View.Columns.Add("Data_" + j, string.Format("投切误差({0})", _Values[3]));
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;                    
                }
            }
            if (Data_View.Columns.Count == 0) return;
            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                int RowIndex = Data_View.Rows.Add();
                for (int j = _FirstColIndex; j < _FirstColIndex + (int)(30 - _FirstColIndex) / 3; j++)
                {
                    string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                    if (MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        if (MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                        string[] _Values = MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');
                        if (_Values.Length != 4) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率
                                                
                        if ((RowIndex + 1) % 2 == 0)
                        {
                            Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                        }
                        else
                        {
                            Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                        }
                        Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup.MeterGroup[i].ToString();                        


                        Data_View.Rows[RowIndex].Cells[_ColIndex].Value = _Values[0];              //标准时间
                        _ColIndex++;
                        Data_View.Rows[RowIndex].Cells[_ColIndex].Value = _Values[1];              //实际时间
                        _ColIndex++;
                        Data_View.Rows[RowIndex].Cells[_ColIndex].Value = _Values[2];              //投切误差，在投切过程中应该显示对应费率电量
                    }
                }                
            }
        }
    }
}

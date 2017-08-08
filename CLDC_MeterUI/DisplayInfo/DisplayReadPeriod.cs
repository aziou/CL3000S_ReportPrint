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
    /// 详细数据-费率时段检查
    /// </summary>
    public partial class DisplayReadPeriod : Base
    {
        /// <summary>
        /// 读取费率信息项目ID
        /// </summary>
        private const string Key = "003";

        private int _FirstColIndex = 0;

        public DisplayReadPeriod()
        {
            InitializeComponent();
        }
        public DisplayReadPeriod(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
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

            string strShowValue = "运行时区|第一套时区表数据|第二套时区表数据|第一套第1日时段表数据|第二套第1日时段表数据|标准时段表数据";
            string[] _ShowValues = strShowValue.Split('|');


            for (int i = 0; i < _ShowValues.Length; i++)            //动态创建数据表单样式
            {
                int ColIndex = Data_View.Columns.Add("Data_" + i, _ShowValues[i]);
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (i == _ShowValues.Length - 1)
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
            }
            
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

            for (int j = 1; j <= 6; j++)
            {
                string _Key = Key + j.ToString("D2");
                int _ColIndex = (int)(_FirstColIndex + j - 1);
                if (!MeterInfo.MeterDgns.ContainsKey(_Key))
                {
                    Data_View.Rows[0].Cells[_ColIndex].Value = "";
                    continue;
                }
                if (MeterInfo.MeterDgns[_Key].Md_chrValue == null || MeterInfo.MeterDgns[_Key].Md_chrValue == string.Empty) continue;

                Data_View.Rows[0].Cells[_ColIndex].Value = MeterInfo.MeterDgns[_Key].Md_chrValue;
            }
            
        }
        public DisplayReadPeriod(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
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

            string strShowValue = "运行时区|第一套时区表数据|第二套时区表数据|第一套第1日时段表数据|第二套第1日时段表数据|标准时段表数据";
            string[] _ShowValues = strShowValue.Split('|');


            for (int i = 0; i < _ShowValues.Length; i++)            //动态创建数据表单样式
            {
                int ColIndex = Data_View.Columns.Add("Data_" + i, _ShowValues[i]);
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == _ShowValues.Length - 1)
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
            }

            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                //if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
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

                for (int j = 1; j <= 6; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + j - 1);
                    if (!MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        if (i < Data_View.Rows.Count )
                        Data_View.Rows[i].Cells[_ColIndex].Value = "";
                        continue;
                    }
                    if (MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;

                    Data_View.Rows[RowIndex].Cells[_ColIndex].Value = MeterGroup.MeterGroup[i].MeterDgns[_Key].Md_chrValue;
                }
            }
        } 
    }
}

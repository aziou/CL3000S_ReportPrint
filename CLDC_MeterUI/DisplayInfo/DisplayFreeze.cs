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
    /// 冻结结论
    /// </summary>
    public partial class DisplayFreeze : Base 
    {
        public DisplayFreeze()
        {
            InitializeComponent();
        }
        public DisplayFreeze(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {

            if (MeterInfo.MeterFreezes .Count == 0) return;
            Dgw_Data.Rows.Clear();

            foreach (string _Key in MeterInfo.MeterFreezes.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze  _Fre = MeterInfo.MeterFreezes[_Key];
                if (_Fre.Md_PrjID.Trim().Length == 3)         //大ID
                {
                    int rowIndex = Dgw_Data.Rows.Add();
                    Dgw_Data["表位", rowIndex].Value = MeterInfo.ToString();
                    if (_Fre.Md_PrjName==null||_Fre.Md_PrjName.Length == 0)
                        Dgw_Data["项目名称", rowIndex].Value = (CLDC_Comm.Enum.Cus_FreezeItem )int.Parse(_Fre.Md_PrjID);
                    else
                        Dgw_Data["项目名称", rowIndex].Value = _Fre.Md_PrjName;
                    Dgw_Data["项目结论", rowIndex].Value = "    " + _Fre.Md_chrValue;
                }
            }

            base.SetData(MeterInfo, allowedit);
        }

        public DisplayFreeze(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            int intFirstMeter = MeterGroup.GetFirstYaoJianMeterBwh();
            if (MeterGroup.MeterGroup[intFirstMeter].MeterDgns.Count == 0) return;
            Dgw_Data.Rows.Clear();

            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                foreach (string _Key in MeterGroup.MeterGroup[i].MeterFreezes.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze _Fre = MeterGroup.MeterGroup[i].MeterFreezes[_Key];
                    if (_Fre.Md_PrjID.Length == 3)         //大ID
                    {
                        int rowIndex = Dgw_Data.Rows.Add();
                        Dgw_Data["表位", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();
                        Dgw_Data["项目名称", rowIndex].Value = _Fre.Md_PrjName;
                        Dgw_Data["项目结论", rowIndex].Value = "    " + _Fre.Md_chrValue;
                    }
                }
            }
            SpanRow(0, Dgw_Data.Rows.Count, 0);
            base.SetData(MeterGroup, allowedit);
        }

        private void SpanRow(int startRowIndex, int rowCount, int col)
        {
            if (rowCount == 0) return;

            int rowSpan = 1;

            int intstart = startRowIndex;

            for (int i = startRowIndex + 1; i < rowCount + intstart; i++)
            {
                if (Dgw_Data[col, i].Value.ToString() == Dgw_Data[col, startRowIndex].Value.ToString())
                {
                    rowSpan++;
                }
                else
                {
                    ((CLZDataGridView.DataGridViewTextBoxCellEx)Dgw_Data[col, startRowIndex]).RowSpan = rowSpan;
                    if (col < 2)
                    {
                        SpanRow(startRowIndex, rowSpan, col + 1);
                    }
                    startRowIndex = i;
                    rowSpan = 1;
                }

            }

            ((CLZDataGridView.DataGridViewTextBoxCellEx)Dgw_Data[col, startRowIndex]).RowSpan = rowSpan;

            if (col < 2)
            {
                SpanRow(startRowIndex, rowSpan, col + 1);
            }
        }
    }
}

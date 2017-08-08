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
    /// 费控检定数据
    /// </summary>
    public partial class DisplayCost : Base 
    {
        public DisplayCost()
        {
            InitializeComponent();
        }
        public DisplayCost(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {

            if (MeterInfo.MeterDgns.Count == 0) return;
            Dgw_Data.Rows.Clear();

            foreach (string _Key in MeterInfo.MeterCostControls.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK _Cost = MeterInfo.MeterCostControls[_Key];
                if (_Cost.Mcc_PrjName == null) continue;
                if (_Cost.Mfk_chrItemType.Length == 3)         //大ID
                {
                    int rowIndex = Dgw_Data.Rows.Add();
                    Dgw_Data["表位", rowIndex].Value = MeterInfo.ToString();
                    Dgw_Data["项目名称", rowIndex].Value = _Cost.Mcc_PrjName;
                    Dgw_Data["项目结论", rowIndex].Value = "    " + _Cost.Mfk_chrJL;
                }
            }

            base.SetData(MeterInfo, allowedit);
        }
        public DisplayCost(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            int intFirstMeter = MeterGroup.GetFirstYaoJianMeterBwh();
            if (MeterGroup.MeterGroup[intFirstMeter].MeterCostControls.Count == 0) return;
            Dgw_Data.Rows.Clear();

            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;  
                foreach (string _Key in MeterGroup.MeterGroup[i].MeterCostControls.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK _Cost = MeterGroup.MeterGroup[i].MeterCostControls[_Key];
                    if (_Cost.Mfk_chrItemType.Length == 3)         //大ID
                    {
                        int rowIndex = Dgw_Data.Rows.Add();
                        Dgw_Data["表位", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();
                        Dgw_Data["项目名称", rowIndex].Value = _Cost.Mcc_PrjName;
                        Dgw_Data["项目结论", rowIndex].Value = "    " + _Cost.Mfk_chrJL;
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

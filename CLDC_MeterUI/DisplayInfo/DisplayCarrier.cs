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
    /// 载波检定数据
    /// </summary>
    public partial class DisplayCarrier : Base 
    {
        public DisplayCarrier()
        {
            InitializeComponent();
        }
        public DisplayCarrier(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            if (MeterInfo.MeterCarrierDatas.Count == 0) return;
            Dgw_Data.Rows.Clear();

            foreach (string _Key in MeterInfo.MeterCarrierDatas.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterCarrierData _Carrier = MeterInfo.MeterCarrierDatas[_Key];
                if (_Carrier.Mce_PrjID.Length > 0)         //大ID
                {
                    Dgw_Data.Rows.Add(MeterInfo.ToString (),_Carrier.Mce_PrjName, _Carrier.Mce_PrjValue, _Carrier.Mce_ItemResult);
                }
            }

            base.SetData(MeterInfo, allowedit);
        }
        public DisplayCarrier(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            if (MeterGroup.MeterGroup.Count == 0) return;
            Dgw_Data.Rows.Clear();
            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                foreach (string _Key in MeterGroup.MeterGroup[i].MeterCarrierDatas.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterCarrierData _Carrier = MeterGroup.MeterGroup[i].MeterCarrierDatas[_Key];
                    if (_Carrier.Mce_PrjID.Length > 0)         //大ID
                    {                        
                        int rowIndex = Dgw_Data.Rows.Add();
                        Dgw_Data["表位", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();
                        Dgw_Data["项目名称", rowIndex].Value = _Carrier.Mce_PrjName;
                        Dgw_Data["数据", rowIndex].Value = _Carrier.Mce_PrjValue;
                        Dgw_Data["结论", rowIndex].Value = "    " + _Carrier.Mce_ItemResult;
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

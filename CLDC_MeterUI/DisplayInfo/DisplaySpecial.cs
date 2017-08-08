using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    public partial class DisplaySpecial : Base
    {
        public DisplaySpecial()
        {
            InitializeComponent();
        }

        public DisplaySpecial(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            if (MeterInfo.MeterSpecialErrs.Count == 0) return;
            Dgw_Data.Rows.Clear();
            foreach (string _Key in MeterInfo.MeterSpecialErrs.Keys)
            {
                int rowIndex = Dgw_Data.Rows.Add();
                Dgw_Data["表位", rowIndex].Value = MeterInfo.ToString();
                Dgw_Data["项目名称", rowIndex].Value = MeterInfo.MeterSpecialErrs[_Key].Mse_PrjName;
                Dgw_Data["项目结论", rowIndex].Value = "    " + MeterInfo.MeterSpecialErrs[_Key].Mse_Result;
                string[] Arr_Err = MeterInfo.MeterSpecialErrs[_Key].Mse_Wc.Split('|');
                if (Arr_Err.Length != 4) continue;
                Dgw_Data[3, rowIndex].Value = Arr_Err[0];
                Dgw_Data[4, rowIndex].Value = Arr_Err[1];
                Dgw_Data[5, rowIndex].Value = Arr_Err[2];
                Dgw_Data[6, rowIndex].Value = Arr_Err[3];
            }

            base.SetData(MeterInfo, allowedit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            Dgw_Data.Rows.Clear();
            for (int i = 0; i < MeterGroup._Bws; i++)
            {

                if (MeterGroup.MeterGroup[i].YaoJianYn == false) continue;
                MeterInfo = MeterGroup.MeterGroup[i];
                foreach (string _Key in MeterInfo.MeterSpecialErrs.Keys)
                {
                    
                    int rowIndex = Dgw_Data.Rows.Add();
                    Dgw_Data["表位", rowIndex].Value = MeterInfo.ToString();
                    Dgw_Data["项目名称", rowIndex].Value = MeterInfo.MeterSpecialErrs[_Key].Mse_PrjName;
                    Dgw_Data["项目结论", rowIndex].Value = "    " + MeterInfo.MeterSpecialErrs[_Key].Mse_Result;
                    string[] Arr_Err = MeterInfo.MeterSpecialErrs[_Key].Mse_Wc.Split('|');
                    if (Arr_Err.Length != 4) continue;
                    Dgw_Data[3, rowIndex].Value = Arr_Err[0];
                    Dgw_Data[4, rowIndex].Value = Arr_Err[1];
                    Dgw_Data[5, rowIndex].Value = Arr_Err[2];
                    Dgw_Data[6, rowIndex].Value = Arr_Err[3];
                }

                base.SetData(MeterInfo, allowedit);
            }
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

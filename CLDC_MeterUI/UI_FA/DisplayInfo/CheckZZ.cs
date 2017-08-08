using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UI.DisplayInfo
{
    /// <summary>
    /// ���ּ춨����
    /// </summary>
    public partial class CheckZZ : Base
    {
        public CheckZZ()
        {
            InitializeComponent();
        }
        public CheckZZ(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
            : base(meterInfo, allowedit)
        {
            InitializeComponent();
            if (Comm.Function.Common.IsVSDevenv()) return;

            SetData(MeterInfo, AllowEdit);
        }

        #region SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        public override void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            base.SetData(meterInfo, allowedit);

            Grid_Main.Rows.Clear();

            //��MeterInfo.MeterZZErrors��Item��Ŀö�ٳ���������Key�ַ����ŵ�DataTable�У����Ұ��������������
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));
            foreach (string Key in MeterInfo.MeterZZErrors.Keys)
            {
                Comm.Model.DnbModel.DnbInfo.MeterZZError MeterError = MeterInfo.MeterZZErrors[Key];
                dtKeys.Rows.Add(Key, MeterError.Mz_PrjID);
            }
            DataRow[] Rows = dtKeys.Select("Keys <>'' and PrjId <> ''", "PrjId asc");

            //Grid_Main.SuspendLayout();

            for (int i = 0; i < Rows.Length; i++)
            {
                Comm.Model.DnbModel.DnbInfo.MeterZZError meterError = MeterInfo.MeterZZErrors[Rows[i][0].ToString()];
                string prjId = meterError.Mz_PrjID;

                int rowIndex = Grid_Main.Rows.Add();

                Grid_Main["���ʷ���", rowIndex].Value = ((Comm.Enum.Cus_PowerFangXiang)int.Parse(prjId[0].ToString())).ToString();

                switch (prjId[1])
                {
                    case '1':
                        Grid_Main["����Ԫ��", rowIndex].Value = "��Ԫ"; break;
                    case '2':
                        Grid_Main["����Ԫ��", rowIndex].Value = "AԪ"; break;
                    case '3':
                        Grid_Main["����Ԫ��", rowIndex].Value = "BԪ"; break;
                    case '4':
                        Grid_Main["����Ԫ��", rowIndex].Value = "CԪ"; break;
                    default:
                        break;
                }

                Grid_Main["��������", rowIndex].Value = meterError.Mz_Glys;

                Grid_Main["���ص���", rowIndex].Value = meterError.Mz_xIb;

                switch (prjId[6])
                {
                    case '0':
                        Grid_Main["����", rowIndex].Value = "��"; break;
                    case '1':
                        Grid_Main["����", rowIndex].Value = "��"; break;
                    case '2':
                        Grid_Main["����", rowIndex].Value = "��"; break;
                    case '3':
                        Grid_Main["����", rowIndex].Value = "ƽ"; break;
                    case '4':
                        Grid_Main["����", rowIndex].Value = "��"; break;
                    default:
                        break;
                }

                Grid_Main["����", rowIndex].Value = meterError.Mz_Start.ToString();
                Grid_Main["ֹ��", rowIndex].Value = meterError.Mz_End.ToString();
                Grid_Main["�����", rowIndex].Value = meterError.Mz_Diff ?? "";
                Grid_Main["���", rowIndex].Value = meterError.Mz_Wc;
                Grid_Main["����", rowIndex].Value = meterError.Mz_Result;

            }
            SpanRow(0, Grid_Main.Rows.Count, 0);

        }

        private void SpanRow(int startRowIndex, int rowCount, int col)
        {
            if (rowCount == 0) return;

            int rowSpan = 1;

            int intstart = startRowIndex;

            for (int i = startRowIndex + 1; i < rowCount + intstart; i++)
            {
                if (Grid_Main[col, i].Value.ToString() == Grid_Main[col, startRowIndex].Value.ToString())
                {
                    rowSpan++;
                }
                else
                {
                    ((CLZDataGridView.DataGridViewTextBoxCellEx)Grid_Main[col, startRowIndex]).RowSpan = rowSpan;
                    if (col < 2)
                    {
                        SpanRow(startRowIndex, rowSpan, col + 1);
                    }
                    startRowIndex = i;
                    rowSpan = 1;
                }

            }

            ((CLZDataGridView.DataGridViewTextBoxCellEx)Grid_Main[col, startRowIndex]).RowSpan = rowSpan;

            if (col < 2)
            {
                SpanRow(startRowIndex, rowSpan, col + 1);
            }
        }

        #endregion



    }
}

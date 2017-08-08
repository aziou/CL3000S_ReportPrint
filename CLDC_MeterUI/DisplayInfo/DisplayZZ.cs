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
    /// ���ּ춨����
    /// </summary>
    public partial class DisplayZZ : Base
    {
        public DisplayZZ()
        {
            InitializeComponent();
        }
        public DisplayZZ(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            SetData(MeterInfo, AllowEdit);
        }


        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            base.SetData(MeterInfo, allowedit);

            Grid_Main.Rows.Clear();

            //��MeterInfo.MeterZZErrors��Item��Ŀö�ٳ���������Key�ַ����ŵ�DataTable�У����Ұ��������������
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));

            foreach (string Key in MeterInfo.MeterZZErrors.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError MeterError = MeterInfo.MeterZZErrors[Key];
                dtKeys.Rows.Add(Key, MeterError.Me_chrProjectNo);
            }
            DataRow[] Rows = dtKeys.Select("Keys <>'' and PrjId <> ''", "PrjId asc");

            //Grid_Main.SuspendLayout();

            for (int j = 0; j < Rows.Length; j++)
            {
                if (MeterInfo.MeterZZErrors.Count == 0) continue;
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError meterError = MeterInfo.MeterZZErrors[Rows[j][0].ToString()];
                string prjId = meterError.Me_chrProjectNo;

                int rowIndex = Grid_Main.Rows.Add();

                Grid_Main["��λ", rowIndex].Value = MeterInfo.ToString();

                Grid_Main["���ʷ���", rowIndex].Value = ((CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(meterError.Mz_chrJdfx)).ToString();

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

                Grid_Main["��������", rowIndex].Value = meterError.Mz_chrGlys;

                Grid_Main["���ص���", rowIndex].Value = meterError.Mz_chrxIbString;

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

                Grid_Main["����", rowIndex].Value = meterError.Mz_chrQiMa.ToString();
                Grid_Main["ֹ��", rowIndex].Value = meterError.Mz_chrZiMa.ToString();
                Grid_Main["�����", rowIndex].Value = meterError.Mz_chrQiZiMaC ?? "";
                Grid_Main["������", rowIndex].Value = meterError.Mz_chrPules;
                Grid_Main["��׼������", rowIndex].Value = meterError.AVR_STANDARD_METER_ENERGY;
                Grid_Main["���", rowIndex].Value = meterError.Mz_chrWc;
                Grid_Main["����", rowIndex].Value = meterError.Mz_chrJL;

            }
            SpanRow(0, Grid_Main.Rows.Count, 0);
            
        }
        public DisplayZZ(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            SetData(MeterGroup, AllowEdit);
        }

        
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            base.SetData(MeterGroup, allowedit);

            Grid_Main.Rows.Clear();

            //��MeterInfo.MeterZZErrors��Item��Ŀö�ٳ���������Key�ַ����ŵ�DataTable�У����Ұ��������������
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));

            foreach (string Key in MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterZZErrors.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError MeterError = MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterZZErrors[Key];
                dtKeys.Rows.Add(Key, MeterError.Me_chrProjectNo);
            }
            DataRow[] Rows = dtKeys.Select("Keys <>'' and PrjId <> ''", "PrjId asc");

                //Grid_Main.SuspendLayout();
            for (int i = 0; i < _MeterGroup._Bws; i++)
            {
                for (int j = 0; j < Rows.Length; j++)
                {
                    if (MeterGroup.MeterGroup[i].MeterZZErrors.Count == 0) continue;
                    if (!MeterGroup.MeterGroup[i].MeterZZErrors.ContainsKey(Rows[j][0].ToString())) continue;
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError meterError = MeterGroup.MeterGroup[i].MeterZZErrors[Rows[j][0].ToString()];
                    string prjId = meterError.Me_chrProjectNo;

                    int rowIndex = Grid_Main.Rows.Add();

                    Grid_Main["��λ", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();
                    if (meterError.Mz_chrJdfx.Length > 0)
                    {
                        if (CLDC_DataCore.Function.Number.IsNumeric(meterError.Mz_chrJdfx))
                        {
                            Grid_Main["���ʷ���", rowIndex].Value = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(meterError.Mz_chrJdfx);
                        }
                        else
                        {
                            Grid_Main["���ʷ���", rowIndex].Value = Enum.Parse(typeof(CLDC_Comm.Enum.Cus_PowerFangXiang), meterError.Mz_chrJdfx);
                        }
                    }
                    else
                        Grid_Main["���ʷ���", rowIndex].Value = "";

                    switch (prjId.Substring(1,1))
                    {
                        case "1":
                            Grid_Main["����Ԫ��", rowIndex].Value = "��Ԫ"; break;
                        case "2":
                            Grid_Main["����Ԫ��", rowIndex].Value = "AԪ"; break;
                        case "3":
                            Grid_Main["����Ԫ��", rowIndex].Value = "BԪ"; break;
                        case "4":
                            Grid_Main["����Ԫ��", rowIndex].Value = "CԪ"; break;
                        default:
                            break;
                    }

                    Grid_Main["��������", rowIndex].Value = meterError.Mz_chrGlys;

                    Grid_Main["���ص���", rowIndex].Value = meterError.Mz_chrxIbString;

                    Grid_Main["����", rowIndex].Value = meterError.Mz_chrFl;                        

                    Grid_Main["����", rowIndex].Value = meterError.Mz_chrQiMa.ToString();
                    Grid_Main["ֹ��", rowIndex].Value = meterError.Mz_chrZiMa.ToString();
                    Grid_Main["�����", rowIndex].Value = meterError.Mz_chrQiZiMaC ?? "";
                    Grid_Main["������", rowIndex].Value = meterError.Mz_chrPules;
                    Grid_Main["��׼������", rowIndex].Value = meterError.AVR_STANDARD_METER_ENERGY;
                    Grid_Main["���", rowIndex].Value = meterError.Mz_chrWc;
                    Grid_Main["����", rowIndex].Value = meterError.Mz_chrJL;                    
                }                
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
                    if (col < 4)
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

    }
}

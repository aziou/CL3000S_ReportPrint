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
    /// �������춨����
    /// </summary>
    public partial class DisplayWC_Normal : Base 
    {
        /// <summary>
        /// =true��ʾ�������ݡ�=falseֻ��ʾ���ϸ�����
        /// </summary>
        public bool IsDisplayAll = true;

        public DisplayWC_Normal()
        {
            InitializeComponent();
        }
        public DisplayWC_Normal(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
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
            for (int i = Grid_Main.Columns.Count - 1; i > 6; i--)
            {
                Grid_Main.Columns.RemoveAt(i);
            }

            int ColCount = 7;
            //��MeterInfo.MeterErrors��Item��Ŀö�ٳ�
            //������Key�ַ����ŵ�DataTable�����У����Ұ��������������
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));
            bool status=true ;
       
            foreach (string Key in MeterInfo.MeterErrors.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError meterError = MeterInfo.MeterErrors[Key];
                //�Ƿ�ֻ��ʾ���ϸ�����
                if (!IsDisplayAll && meterError.Me_chrWcJl != CLDC_DataCore.Const.Variable.CTG_BuHeGe) continue;
                dtKeys.Rows.Add(Key, meterError.Me_chrProjectNo);
                //ֻ���������������
                if (meterError.Me_chrProjectNo == null) continue;
                if (meterError.Me_chrProjectNo[0] == '1')
                {
                    string[] arWc = meterError.Me_chrWcMore.Split('|');
                    if (5 + arWc.Length > ColCount)
                        ColCount = 5 + arWc.Length;
                }
            }
            //ColCount += 1;

            for (int i = 6; i < ColCount; i++)
            {
                int colIndex = 0;
                if (i != ColCount - 1)
                    colIndex = Grid_Main.Columns.Add(string.Format("���{0}", i - 5), string.Format("���{0}", i - 5));
                else
                    colIndex = Grid_Main.Columns.Add("����", "����");
                Grid_Main.Columns[colIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Grid_Main.Columns[colIndex].FillWeight = 5;
                Grid_Main.Columns[colIndex].ReadOnly = true;
                Grid_Main.Columns[colIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grid_Main.Columns[colIndex].DefaultHeaderCellType = Grid_Main.Columns[0].DefaultHeaderCellType;
            }

            //ֻ���������������
            DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '1%' ", "PrjId asc");
            
            for (int i = 0; i < Rows.Length; i++)
            {
                string Key = Rows[i][0].ToString();
                //if (MeterInfo.YaoJianYn == false) continue;
                if (!MeterInfo.MeterErrors.ContainsKey(Key)) continue;
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError MeterError = MeterInfo.MeterErrors[Key];
                string PrjId = MeterError.Me_chrProjectNo;

                int rowIndex = Grid_Main.Rows.Add();

                Grid_Main["��λ", rowIndex].Value = MeterInfo.ToString();

                #region ���ʷ���

                switch (PrjId[1])
                {
                    case '1':
                        Grid_Main["���ʷ���", rowIndex].Value = "�����й�"; break;
                    case '2':
                        Grid_Main["���ʷ���", rowIndex].Value = "�����й�"; break;
                    case '3':
                        Grid_Main["���ʷ���", rowIndex].Value = "�����޹�"; break;
                    case '4':
                        Grid_Main["���ʷ���", rowIndex].Value = "�����޹�"; break;
                    case '5':
                        Grid_Main["���ʷ���", rowIndex].Value = "��һ�����޹�"; break;
                    case '6':
                        Grid_Main["���ʷ���", rowIndex].Value = "�ڶ������޹�"; break;
                    case '7':
                        Grid_Main["���ʷ���", rowIndex].Value = "���������޹�"; break;
                    case '8':
                        Grid_Main["���ʷ���", rowIndex].Value = "���������޹�"; break;
                    default:
                        break;
                }
                #endregion

                #region ����Ԫ��

                switch (PrjId[2])
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
                #endregion

                //��������
                Grid_Main["��������", rowIndex].Value = MeterError.Me_chrGlys;

                //��������
                Grid_Main["���ص���", rowIndex].Value = MeterError.Me_dblxIb;

                string[] arWC = MeterError.Me_chrWcMore.Split('|');

                //����ֵ
                if (arWC.Length > 0)
                {
                    Grid_Main["����ֵ", rowIndex].Value = arWC[arWC.Length - 1]; //GetNewCell(Key,MeterError.MeWc,arWC.Length - 1);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 1]);
                }

                //ƽ��ֵ
                if (arWC.Length > 1)
                {
                    Grid_Main["ƽ��ֵ", rowIndex].Value = arWC[arWC.Length - 2]; //GetNewCell(Key, MeterError.MeWc, arWC.Length - 2);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 2]);
                }

                //1-10�����ֵ
                for (int j = 0; j < arWC.Length - 2; j++)
                {
                    Grid_Main[j + 7, rowIndex].Value = arWC[j];// GetNewCell(Key,MeterError.MeWc, j);//new SourceGrid.Cells.Cell(arWC[j]);
                }
                //����
                Grid_Main["����", rowIndex].Value = MeterError.Me_chrWcJl; //GetNewCell(Key, meterError.Me_chrWcJl, 0);
            }

            SpanRow(0, Grid_Main.Rows.Count, 0);

        }

        public DisplayWC_Normal(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
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
            for (int i = Grid_Main.Columns.Count - 1; i > 6; i--)
            {
                Grid_Main.Columns.RemoveAt(i);
            }

            int ColCount = 7;
            //��MeterInfo.MeterErrors��Item��Ŀö�ٳ�
            //������Key�ַ����ŵ�DataTable�����У����Ұ��������������
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));
            bool status = true;
            for (int i = 0; i < MeterGroup.MeterGroup.Count && status; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                foreach (string Key in MeterGroup.MeterGroup[i].MeterErrors.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError meterError = MeterGroup.MeterGroup[i].MeterErrors[Key];
                    //�Ƿ�ֻ��ʾ���ϸ�����
                    if (!IsDisplayAll && meterError.Me_chrWcJl != CLDC_DataCore.Const.Variable.CTG_BuHeGe) continue;
                    dtKeys.Rows.Add(Key, meterError.Me_chrProjectNo);
                    //ֻ���������������
                    if (meterError.Me_chrProjectNo == null) continue;
                    if (meterError.Me_chrProjectNo[0] == '1')
                    {
                        string[] arWc = meterError.Me_chrWcMore.Split('|');
                        if (5 + arWc.Length > ColCount)
                            ColCount = 5 + arWc.Length;
                    }
                    status = false;
                }
            }
            //ColCount += 1;

            for (int i = 6; i < ColCount; i++)
            {
                int colIndex=0;
                if (i != ColCount - 1)
                    colIndex = Grid_Main.Columns.Add(string.Format("���{0}", i - 5), string.Format("���{0}", i - 5));
                else
                    colIndex = Grid_Main.Columns.Add("����","����");
                Grid_Main.Columns[colIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Grid_Main.Columns[colIndex].FillWeight = 5;
                Grid_Main.Columns[colIndex].ReadOnly = true;
                Grid_Main.Columns[colIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grid_Main.Columns[colIndex].DefaultHeaderCellType = Grid_Main.Columns[0].DefaultHeaderCellType;
            }

            //ֻ���������������
            DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '1%' ", "PrjId asc");


            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                for (int j = 0; j < Rows.Length; j++)
                {
                    string Key = Rows[j][0].ToString();
                    if (!MeterGroup.MeterGroup[i].MeterErrors.ContainsKey(Key)) continue;
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError MeterError = MeterGroup.MeterGroup[i].MeterErrors[Key];
                    string PrjId = MeterError.Me_chrProjectNo;

                    int rowIndex = Grid_Main.Rows.Add();

                    Grid_Main["��λ", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();

                    #region ���ʷ���

                    switch (PrjId[1])
                    {
                        case '1':
                            Grid_Main["���ʷ���", rowIndex].Value = "�����й�"; break;
                        case '2':
                            Grid_Main["���ʷ���", rowIndex].Value = "�����й�"; break;
                        case '3':
                            Grid_Main["���ʷ���", rowIndex].Value = "�����޹�"; break;
                        case '4':
                            Grid_Main["���ʷ���", rowIndex].Value = "�����޹�"; break;
                        case '5':
                            Grid_Main["���ʷ���", rowIndex].Value = "��һ�����޹�"; break;
                        case '6':
                            Grid_Main["���ʷ���", rowIndex].Value = "�ڶ������޹�"; break;
                        case '7':
                            Grid_Main["���ʷ���", rowIndex].Value = "���������޹�"; break;
                        case '8':
                            Grid_Main["���ʷ���", rowIndex].Value = "���������޹�"; break;
                        default:
                            break;
                    }
                    #endregion

                    #region ����Ԫ��

                    switch (PrjId[2])
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
                    #endregion

                    //��������
                    Grid_Main["��������", rowIndex].Value = MeterError.Me_chrGlys;

                    //��������
                    Grid_Main["���ص���", rowIndex].Value = MeterError.Me_dblxIb;

                    string[] arWC = MeterError.Me_chrWcMore.Split('|');

                    //����ֵ
                    if (arWC.Length > 0)
                    {
                        Grid_Main["����ֵ", rowIndex].Value = arWC[arWC.Length - 1]; //GetNewCell(Key,MeterError.MeWc,arWC.Length - 1);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 1]);
                    }

                    //ƽ��ֵ
                    if (arWC.Length > 1)
                    {
                        Grid_Main["ƽ��ֵ", rowIndex].Value = arWC[arWC.Length - 2]; //GetNewCell(Key, MeterError.MeWc, arWC.Length - 2);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 2]);
                    }
                    try
                    {
                        //1-10�����ֵ
                        for (int k = 0; k < arWC.Length - 2; k++)
                        {
                            if ((k + 7) < Grid_Main.ColumnCount)
                                Grid_Main[k + 7, rowIndex].Value = arWC[k];// GetNewCell(Key,MeterError.MeWc, j);//new SourceGrid.Cells.Cell(arWC[j]);
                        }
                    }
                    catch (Exception ex)
                    {
 
                    }
                    //����
                    Grid_Main["����", rowIndex].Value = MeterError.Me_chrWcJl; //GetNewCell(Key, meterError.Me_chrWcJl, 0);
                }
            }
            SpanRow(0,Grid_Main.Rows.Count,0);
            
        }

        private void SpanRow(int startRowIndex,int rowCount,int col)
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
    }
}

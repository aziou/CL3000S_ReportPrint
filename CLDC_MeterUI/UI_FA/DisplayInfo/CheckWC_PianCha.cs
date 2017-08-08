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
    /// ��׼ƫ������
    /// </summary>
    public partial class CheckWC_PianCha : Base
    {

        /// <summary>
        /// =true��ʾ�������ݡ�=falseֻ��ʾ���ϸ�����
        /// </summary>
        public bool IsDisplayAll = true;

        public CheckWC_PianCha()
        {
            InitializeComponent();
        }
        public CheckWC_PianCha(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
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

            for (int i = Grid_Main.Columns.Count - 1; i > 5; i--)
            {
                Grid_Main.Columns.RemoveAt(i);
            }

            int ColCount = 4;
            //��MeterInfo.MeterErrors��Item��Ŀö�ٳ���������Key�ַ����ŵ�DataTable�У����Ұ��������������
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));
            foreach (string Key in MeterInfo.MeterErrors.Keys)
            {

                Comm.Model.DnbModel.DnbInfo.MeterError MeterError = MeterInfo.MeterErrors[Key];
                //�Ƿ�ֻ��ʾ���ϸ�����
                if (!IsDisplayAll && MeterError.Me_Result != Comm.Const.Variable.CTG_BuHeGe) continue;
                dtKeys.Rows.Add(Key, MeterInfo.MeterErrors[Key].Me_PrjID);
                //ֻ�����׼ƫ�������
                if (MeterError.Me_PrjID[0] == '2')
                {
                    string[] arWc = MeterError.Me_Wc.Split('|');
                    if (4 + arWc.Length > ColCount)
                        ColCount = 4 + arWc.Length;
                }
            }
            ColCount += 1;

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

            //ֻ�����׼ƫ�������
            DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '2%' ", "PrjId asc");

            //Grid_Main.SuspendLayout();

            for (int i = 0; i < Rows.Length; i++)
            {
                string Key = Rows[i][0].ToString();
                Comm.Model.DnbModel.DnbInfo.MeterError MeterError = MeterInfo.MeterErrors[Key];
                string PrjId = MeterError.Me_PrjID;

                int rowIndex = Grid_Main.Rows.Add();

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
                Grid_Main["��������", rowIndex].Value = MeterError.Me_Glys;

                //��������
                Grid_Main["���ص���", rowIndex].Value = MeterError.Me_xib;

                string[] arWC = MeterError.Me_Wc.Split('|');

                //����ֵ
                if (arWC.Length > 0)
                {
                    Grid_Main["����ֵ", rowIndex].Value = arWC[arWC.Length - 1]; //GetNewCell(Key,MeterError.Me_Wc,arWC.Length - 1);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 1]);
                }

                //ƽ��ֵ
                if (arWC.Length > 1)
                {
                    Grid_Main["ԭʼֵ", rowIndex].Value = arWC[arWC.Length - 2]; //GetNewCell(Key, MeterError.Me_Wc, arWC.Length - 2);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 2]);
                }

                //1-10�����ֵ
                for (int j = 0; j < arWC.Length - 2; j++)
                {
                    Grid_Main[j + 6, rowIndex].Value = arWC[j];// GetNewCell(Key,MeterError.Me_Wc, j);//new SourceGrid.Cells.Cell(arWC[j]);
                }
                //����
                Grid_Main["����", rowIndex].Value = MeterError.Me_Result; //GetNewCell(Key, MeterError.Me_Result, 0);


                //������ϸ�����ʾ��ɫ
                if (Grid_Main["����", rowIndex].Value.ToString() == Comm.Const.Variable.CTG_BuHeGe)
                {
                    Grid_Main["����", rowIndex].Style.ForeColor = Color.Red;// UI.UI_Detection.Main.Color_Grid_BuHeGe;
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

        //private SourceGrid.Cells.Cell GetNewCell(string Key, string Me_Wc, int index)
        //{
        //    string[] arWc = Me_Wc.Split('|');
        //    string CellValue = arWc[index];
        //    if (AllowEdit)
        //    {
        //        SourceGrid.Cells.Cell Cell = new SourceGrid.Cells.Cell(CellValue, new SourceGrid.Cells.Editors.TextBox(typeof(string)));
        //        Cell.Tag = new object[] { Key, Me_Wc, index };
        //        Cell.Editor.OnEditerValueChanged += new SourceGrid.Cells.Editors.EventOnEditerValueChanged(EditorValueChanged);
        //        return Cell;
        //    }
        //    else
        //    {
        //        return new SourceGrid.Cells.Cell(CellValue);
        //    }
        //}

        //bool EditorValueChanged(SourceGrid.Cells.Editors.EditorBase editor, object newvalue)
        //{
        //    if (newvalue == null)
        //        return false;
        //    //�������Ϊ[����]
        //    if (editor.Cell.DisplayText == "����")
        //        if (newvalue.ToString() != "�ϸ�" && newvalue.ToString() != "���ϸ�")
        //            return false;
        //    if (!Comm.Function.Number.IsNumeric(newvalue.ToString()))
        //        return false ;
        //    //�������Cell.Tag
        //    string Key = ((object[])editor.Cell.Tag)[0].ToString();
        //    string Me_Wc = ((object[])editor.Cell.Tag)[1].ToString();
        //    int index = Convert.ToInt32(((object[])editor.Cell.Tag)[2]);
        //    string[] arWc = Me_Wc.Split('|');
        //    arWc[index] = newvalue.ToString();
        //    Me_Wc = arWc[0];
        //    for (int i = 1; i < arWc.Length; i++)
        //        Me_Wc += string.Format("|{0}", arWc[i]);
        //    editor.Cell.Tag = new object[] { Key, Me_Wc, index };
        //    //�޸�MeterInfo
        //    MeterInfo.MeterErrors[Key].Me_Wc = Me_Wc;
        //    return true;
        //}


    }
}

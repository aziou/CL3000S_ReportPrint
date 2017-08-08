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
    /// 标准偏差数据
    /// </summary>
    public partial class DisplayWC_PianCha : Base
    {

        /// <summary>
        /// =true显示所有数据、=false只显示不合格数据
        /// </summary>
        public bool IsDisplayAll = true;

        public DisplayWC_PianCha()
        {
            InitializeComponent();
        }
        public DisplayWC_PianCha(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
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
            //将MeterInfo.MeterErrors的Item项目枚举出，把所有Key字符串放到DataTable中，并且按照升序检索出来
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));
            foreach (string Key in MeterInfo.MeterErrors.Keys)
            {

                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError MeterError = MeterInfo.MeterErrors[Key];
                //是否只显示不合格数据
                if (!IsDisplayAll && MeterError.Me_chrWcJl != CLDC_DataCore.Const.Variable.CTG_BuHeGe) continue;
                dtKeys.Rows.Add(Key, MeterInfo.MeterErrors[Key].Me_chrProjectNo);
                //只计算标准偏差的数据
                if (MeterError.Me_chrProjectNo == null) continue;
                if (MeterError.Me_chrProjectNo[0] == '2')
                {
                    string[] arWc = MeterError.Me_chrWcMore.Split('|');
                    if (5 + arWc.Length > ColCount)
                        ColCount = 5 + arWc.Length;
                }
            }
            //ColCount += 1;

            for (int j = 6; j < ColCount; j++)
            {
                int colIndex = 0;
                if (j != ColCount - 1)
                    colIndex = Grid_Main.Columns.Add(string.Format("误差{0}", j - 5), string.Format("误差{0}", j - 5));
                else
                    colIndex = Grid_Main.Columns.Add("结论", "结论");
                Grid_Main.Columns[colIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Grid_Main.Columns[colIndex].FillWeight = 5;
                Grid_Main.Columns[colIndex].ReadOnly = true;
                Grid_Main.Columns[colIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grid_Main.Columns[colIndex].DefaultHeaderCellType = Grid_Main.Columns[0].DefaultHeaderCellType;
            }

            //只计算标准偏差的数据
            DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '2%' ", "PrjId asc");

            //Grid_Main.SuspendLayout();

            for (int j = 0; j < Rows.Length; j++)
            {
                string Key = Rows[j][0].ToString();
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError MeterError = MeterInfo.MeterErrors[Key];
                string PrjId = MeterError.Me_chrProjectNo;

                int rowIndex = Grid_Main.Rows.Add();

                Grid_Main["表位", rowIndex].Value = MeterInfo.ToString();

                #region 功率方向
                switch (PrjId[1])
                {
                    case '1':
                        Grid_Main["功率方向", rowIndex].Value = "正向有功"; break;
                    case '2':
                        Grid_Main["功率方向", rowIndex].Value = "反向有功"; break;
                    case '3':
                        Grid_Main["功率方向", rowIndex].Value = "正向无功"; break;
                    case '4':
                        Grid_Main["功率方向", rowIndex].Value = "反向无功"; break;
                    case '5':
                        Grid_Main["功率方向", rowIndex].Value = "第一象限无功"; break;
                    case '6':
                        Grid_Main["功率方向", rowIndex].Value = "第二象限无功"; break;
                    case '7':
                        Grid_Main["功率方向", rowIndex].Value = "第三象限无功"; break;
                    case '8':
                        Grid_Main["功率方向", rowIndex].Value = "第四象限无功"; break;
                    default:
                        break;
                }
                #endregion

                #region 功率元件
                switch (PrjId[2])
                {
                    case '1':
                        Grid_Main["功率元件", rowIndex].Value = "合元"; break;
                    case '2':
                        Grid_Main["功率元件", rowIndex].Value = "A元"; break;
                    case '3':
                        Grid_Main["功率元件", rowIndex].Value = "B元"; break;
                    case '4':
                        Grid_Main["功率元件", rowIndex].Value = "C元"; break;
                    default:
                        break;
                }
                #endregion

                //功率因数
                Grid_Main["功率因数", rowIndex].Value = MeterError.Me_chrGlys;

                //电流倍数
                Grid_Main["负载电流", rowIndex].Value = MeterError.Me_dblxIb;

                string[] arWC = MeterError.Me_chrWcMore.Split('|');

                //化整值
                if (arWC.Length > 0)
                {
                    Grid_Main["化整值", rowIndex].Value = arWC[arWC.Length - 1]; //GetNewCell(Key,MeterError.MeWc,arWC.Length - 1);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 1]);
                }

                //平均值
                if (arWC.Length > 1)
                {
                    Grid_Main["原始值", rowIndex].Value = arWC[arWC.Length - 2]; //GetNewCell(Key, MeterError.MeWc, arWC.Length - 2);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 2]);
                }

                //1-10次误差值
                for (int k = 0; k < arWC.Length - 2; k++)
                {
                    Grid_Main[k + 7, rowIndex].Value = arWC[k];// GetNewCell(Key,MeterError.MeWc, j);//new SourceGrid.Cells.Cell(arWC[j]);
                }
                //结论
                Grid_Main["结论", rowIndex].Value = MeterError.Me_chrWcJl; //GetNewCell(Key, meterError.Me_chrWcJl, 0);


                //如果不合格则显示红色
                if (Grid_Main["结论", rowIndex].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    Grid_Main["结论", rowIndex].Style.ForeColor = Color.Red;// CLDC_MeterUI.UI_Detection.Main.Color_Grid_BuHeGe;
                }

            }

            SpanRow(0, Grid_Main.Rows.Count, 0);
            

        }
        public DisplayWC_PianCha(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
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
            //将MeterInfo.MeterErrors的Item项目枚举出，把所有Key字符串放到DataTable中，并且按照升序检索出来
            DataTable dtKeys = new DataTable();
            dtKeys.Columns.Add("Keys", typeof(string));
            dtKeys.Columns.Add("PrjId", typeof(string));

            foreach (string Key in MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterErrors.Keys)
            {

                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError MeterError = MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterErrors[Key];
                //是否只显示不合格数据
                if (!IsDisplayAll && MeterError.Me_chrWcJl != CLDC_DataCore.Const.Variable.CTG_BuHeGe) continue;
                dtKeys.Rows.Add(Key, MeterGroup.MeterGroup[MeterGroup.GetFirstYaoJianMeterBwh()].MeterErrors[Key].Me_chrProjectNo);
                //只计算标准偏差的数据
                if (MeterError.Me_chrProjectNo == null) continue;
                if (MeterError.Me_chrProjectNo[0] == '2')
                {
                    string[] arWc = MeterError.Me_chrWcMore.Split('|');
                    if (5 + arWc.Length > ColCount)
                        ColCount = 5 + arWc.Length;
                }
            }
            //ColCount += 1;

            for (int j = 6; j < ColCount; j++)
            {
                int colIndex = 0;
                if (j != ColCount - 1)
                    colIndex = Grid_Main.Columns.Add(string.Format("误差{0}", j - 5), string.Format("误差{0}", j - 5));
                else
                    colIndex = Grid_Main.Columns.Add("结论", "结论");
                Grid_Main.Columns[colIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Grid_Main.Columns[colIndex].FillWeight = 5;
                Grid_Main.Columns[colIndex].ReadOnly = true;
                Grid_Main.Columns[colIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grid_Main.Columns[colIndex].DefaultHeaderCellType = Grid_Main.Columns[0].DefaultHeaderCellType;
            }

            //只计算标准偏差的数据
            DataRow[] Rows = dtKeys.Select("Keys <> '' and  PrjId Like '2%' ", "PrjId asc");

            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                for (int j = 0; j < Rows.Length; j++)
                {
                    string Key = Rows[j][0].ToString();
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError MeterError = MeterGroup.MeterGroup[i].MeterErrors[Key];
                    string PrjId = MeterError.Me_chrProjectNo;

                    int rowIndex = Grid_Main.Rows.Add();

                    Grid_Main["表位", rowIndex].Value = MeterGroup.MeterGroup[i].ToString();

                    #region 功率方向
                    switch (PrjId[1])
                    {
                        case '1':
                            Grid_Main["功率方向", rowIndex].Value = "正向有功"; break;
                        case '2':
                            Grid_Main["功率方向", rowIndex].Value = "反向有功"; break;
                        case '3':
                            Grid_Main["功率方向", rowIndex].Value = "正向无功"; break;
                        case '4':
                            Grid_Main["功率方向", rowIndex].Value = "反向无功"; break;
                        case '5':
                            Grid_Main["功率方向", rowIndex].Value = "第一象限无功"; break;
                        case '6':
                            Grid_Main["功率方向", rowIndex].Value = "第二象限无功"; break;
                        case '7':
                            Grid_Main["功率方向", rowIndex].Value = "第三象限无功"; break;
                        case '8':
                            Grid_Main["功率方向", rowIndex].Value = "第四象限无功"; break;
                        default:
                            break;
                    }
                    #endregion

                    #region 功率元件
                    switch (PrjId[2])
                    {
                        case '1':
                            Grid_Main["功率元件", rowIndex].Value = "合元"; break;
                        case '2':
                            Grid_Main["功率元件", rowIndex].Value = "A元"; break;
                        case '3':
                            Grid_Main["功率元件", rowIndex].Value = "B元"; break;
                        case '4':
                            Grid_Main["功率元件", rowIndex].Value = "C元"; break;
                        default:
                            break;
                    }
                    #endregion

                    //功率因数
                    Grid_Main["功率因数", rowIndex].Value = MeterError.Me_chrGlys;

                    //电流倍数
                    Grid_Main["负载电流", rowIndex].Value = MeterError.Me_dblxIb;

                    string[] arWC = MeterError.Me_chrWcMore.Split('|');

                    //化整值
                    if (arWC.Length > 0)
                    {
                        Grid_Main["化整值", rowIndex].Value = arWC[arWC.Length - 1]; //GetNewCell(Key,MeterError.MeWc,arWC.Length - 1);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 1]);
                    }

                    //平均值
                    if (arWC.Length > 1)
                    {
                        Grid_Main["原始值", rowIndex].Value = arWC[arWC.Length - 2]; //GetNewCell(Key, MeterError.MeWc, arWC.Length - 2);//new SourceGrid.Cells.Cell(arWC[arWC.Length - 2]);
                    }

                    //1-10次误差值
                    for (int k = 0; k < arWC.Length - 2; k++)
                    {
                        Grid_Main[k + 7, rowIndex].Value = arWC[k];// GetNewCell(Key,MeterError.MeWc, j);//new SourceGrid.Cells.Cell(arWC[j]);
                    }
                    //结论
                    Grid_Main["结论", rowIndex].Value = MeterError.Me_chrWcJl; //GetNewCell(Key, meterError.Me_chrWcJl, 0);


                    //如果不合格则显示红色
                    if (Grid_Main["结论", rowIndex].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    {
                        Grid_Main["结论", rowIndex].Style.ForeColor = Color.Red;// CLDC_MeterUI.UI_Detection.Main.Color_Grid_BuHeGe;
                    }

                }

                SpanRow(0, Grid_Main.Rows.Count, 0);
            }

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
        //    //如果该列为[结论]
        //    if (editor.Cell.DisplayText == "结论")
        //        if (newvalue.ToString() != "合格" && newvalue.ToString() != "不合格")
        //            return false;
        //    if (!CLDC_DataCore.Function.Number.IsNumeric(newvalue.ToString()))
        //        return false ;
        //    //重新组合Cell.Tag
        //    string Key = ((object[])editor.Cell.Tag)[0].ToString();
        //    string Me_Wc = ((object[])editor.Cell.Tag)[1].ToString();
        //    int index = Convert.ToInt32(((object[])editor.Cell.Tag)[2]);
        //    string[] arWc = Me_Wc.Split('|');
        //    arWc[index] = newvalue.ToString();
        //    Me_Wc = arWc[0];
        //    for (int i = 1; i < arWc.Length; i++)
        //        Me_Wc += string.Format("|{0}", arWc[i]);
        //    editor.Cell.Tag = new object[] { Key, Me_Wc, index };
        //    //修改MeterInfo
        //    MeterInfo.MeterErrors[Key].Me_Wc = Me_Wc;
        //    return true;
        //}


    }
}

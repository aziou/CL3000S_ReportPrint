using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FunctionDataView
{
    public partial class ViewRatePeriod : UserControl
    {
        /// <summary>
        /// 费率时段功能数据项目ID
        /// </summary>
        private const string Key = "004";
              
        public ViewRatePeriod()
        {
            InitializeComponent();

            RefreshGrid();

            //int _ColIndex = 0;
            //int iCols = 15;

            //_ColIndex = Data_View.Columns.Add("Data_Z", " 表号 ");
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "   两套时区表切换时间   ");
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "   两套日时段切换时间   ");
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "   约定冻结数据模式字   ");
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_ColIndex = Data_View.Columns.Add("Data_Z", "   当前正向有功电量   ");
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //for (int i = 0; i < iCols; i++)
            //{
            //    _ColIndex = Data_View.Columns.Add("Data_Z", "      费率时段功能      ");
            //    Data_View.Columns[_ColIndex].Tag = 0;
            //    Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //    Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
        }

        private void RefreshGrid()
        {
            int intColNo = 1;
            int _ColIndex = 0;
            int iCols = 18;
            string strColName = "";
           

            Data_View.Columns.Clear();

            string[] arrTitle = new string[] { "表号", "   两套时区表切换时间   ", "   两套日时段切换时间   ", "   约定冻结数据模式字   ", "   当前正向有功电量   "};

            foreach (string strTitle in arrTitle)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, strTitle);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            string[] str_Values = new string[] { "运行时区", "运行时段", "时区表切换时间", "时段表切换时间", "时区切换正向有功", "时段切换正向有功" };
            for (int i = 0; i < iCols; i++)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, str_Values[i % 6]);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            Data_View.MergeColumnNames.Add("Column1");
                        
            Data_View.AddSpanHeader(5, 6, "【切换前】信息");
            
            Data_View.AddSpanHeader(11, 6, "【切换后】信息");

            Data_View.AddSpanHeader(17, 6, "【恢复后】信息");
        }
        
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count)           //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
            {
                Data_View.Rows.Clear();
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    int RowIndex = Data_View.Rows.Add();
                    if ((RowIndex + 1) % 2 == 0)
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup[i].ToString();
                }
                Data_View.Refresh();
            }


            for (int i = 0; i < MeterGroup.Count; i++)         
            {
                Data_View.Rows[i].Cells[0].Value = string.Format("第{0}表位", i + 1);

                for (int j = 1; j <= 7; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    if (!MeterGroup[i].YaoJianYn) continue;
                    if (!MeterGroup[i].MeterFunctions.ContainsKey(_Key))
                    {
                        if (j < 5)
                            Data_View.Rows[i].Cells[j].Value = "";
                        else
                        {
                            for (int k = 0; k < 6; k++)
                                Data_View.Rows[i].Cells[(j-5) * 6 + k + 5].Value = "";
                        }
                        continue;
                    }
                    if (MeterGroup[i].MeterFunctions[_Key].Mf_chrValue == null || MeterGroup[i].MeterFunctions[_Key].Mf_chrValue == string.Empty) continue;

                    if (j < 5)
                    {
                        Data_View.Rows[i].Cells[j].Value = MeterGroup[i].MeterFunctions[_Key].Mf_chrValue;
                    }
                    else
                    {
                        string[] arrValue = null;
                        if (MeterGroup[i].MeterFunctions[_Key].Mf_chrValue.IndexOf('|') == -1)
                            continue;
                        arrValue = MeterGroup[i].MeterFunctions[_Key].Mf_chrValue.Split('|');
                        for (int k = 0; k < arrValue.Length; k++)
                            Data_View.Rows[i].Cells[(j - 5) * 6 + k + 5].Value = arrValue[k];
                    }
                }
            }
        }
        /// <summary>
        /// 刷新单个电能表信息
        /// </summary>
        /// <param name="MeterInfo">电能表信息</param>
        /// <param name="allowedit"></param>
        public void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {

            if (MeterInfo.MeterFunctions.Count == 0) return;
            Data_View.Rows.Clear();

            int RowIndex = Data_View.Rows.Add();
            if ((RowIndex + 1) % 2 == 0)
            {
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
            }
            else
            {
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
            }
            Data_View.Rows[RowIndex].Cells[0].Value = MeterInfo.ToString();


            for (int j = 1; j <= 7; j++)
            {
                string _Key = Key + j.ToString("D2");
                if (!MeterInfo.YaoJianYn) continue;
                if (!MeterInfo.MeterFunctions.ContainsKey(_Key))
                {
                    if (j < 5)
                        Data_View.Rows[RowIndex].Cells[j].Value = "";
                    else
                    {
                        for (int k = 0; k < 5; k++)
                            Data_View.Rows[RowIndex].Cells[(j - 5) * 6 + k + 5].Value = "";
                    }
                    continue;
                }
                if (MeterInfo.MeterFunctions[_Key].Mf_chrValue == null || MeterInfo.MeterFunctions[_Key].Mf_chrValue == string.Empty) return ;

                if (j < 5)
                {
                    Data_View.Rows[RowIndex].Cells[j].Value = MeterInfo.MeterFunctions[_Key].Mf_chrValue;
                }
                else
                {
                    string[] arrValue = null;
                    if (MeterInfo.MeterFunctions[_Key].Mf_chrValue.IndexOf('|') == -1)
                        return ;
                    arrValue = MeterInfo.MeterFunctions[_Key].Mf_chrValue.Split('|');
                    for (int k = 0; k < arrValue.Length; k++)
                        Data_View.Rows[RowIndex].Cells[(j - 5) * 6 + k + 5].Value = arrValue[k];
                }
            }
        }

        #region 重绘datagridview表头

        int top = 0;
        int left = 0;
        int height = 0;
        int width1 = 0;
        private void CellPainting(object sender, DataGridViewCellPaintingEventArgs e, int start, string topValue, string[] bottomValues)
        {

            DataGridView dgv = (DataGridView)(sender);
            if (e.RowIndex == -1 && (e.ColumnIndex == start || e.ColumnIndex == start + 1 || e.ColumnIndex == start + 2 || e.ColumnIndex == start + 3 || e.ColumnIndex == start + 4 || e.ColumnIndex == start + 5)) //列上加了个表头
            {
                if (e.ColumnIndex == start)
                {
                    top = e.CellBounds.Top;
                    left = e.CellBounds.Left;
                    height = e.CellBounds.Height;
                    width1 = e.CellBounds.Width;
                }

                int width3 = this.Data_View.Columns[5].Width;
                int width4 = this.Data_View.Columns[6].Width;
                int width5 = this.Data_View.Columns[7].Width;
                int width6 = this.Data_View.Columns[8].Width;
                int sumWidth = width1 + width3 + width4 + width5 + width6;

                Rectangle rect = new Rectangle(left, top, sumWidth, e.CellBounds.Height);
                using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    //抹去原来的cell背景
                    e.Graphics.FillRectangle(backColorBrush, rect);
                }

                using (Pen gridLinePen = new Pen(dgv.GridColor))
                {
                    e.Graphics.DrawLine(gridLinePen, left, 0, left + sumWidth, 0);
                    e.Graphics.DrawLine(gridLinePen, left, height / 2, left + sumWidth, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left, height, left + sumWidth, height);

                    e.Graphics.DrawLine(gridLinePen, left, 0, left, height / 2);

                    e.Graphics.DrawLine(gridLinePen, left + width1, height / 2, left + width1, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width3, height / 2, left + width1 + width3, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width3 + width4, height / 2, left + width1 + width3 + width4, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width3 + width4 + width5, height / 2, left + width1 + width3 + width4 + width5, height);
                    e.Graphics.DrawLine(gridLinePen, left + sumWidth - 1, height / 2, left + sumWidth - 1, height);

                    //计算绘制字符串的位置
                    string columnValue = topValue;
                    SizeF sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    float lstr = (sumWidth - sf.Width) / 2;
                    float rstr = (height / 2 - sf.Height) / 2;
                    //画出文本框

                    if (columnValue != "")
                    {
                        e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                   new SolidBrush(e.CellStyle.ForeColor),
                                                     left + lstr,
                                                     top + rstr,
                                                     StringFormat.GenericDefault);
                    }
                    int iTmp = left;
                    for (int i = 0; i < bottomValues.Length; i++)
                    {
                        //计算绘制字符串的位置
                        columnValue = bottomValues[i];
                        sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                        lstr = (this.Data_View.Columns[i + 1].Width - sf.Width) / 2;
                        rstr = (height / 2 - sf.Height) / 2;
                        //画出文本框                        
                        iTmp += this.Data_View.Columns[i + 4].Width;
                        if (columnValue != "")
                        {
                            e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                       new SolidBrush(e.CellStyle.ForeColor),
                                                         iTmp - 100,
                                                         top + height / 2 + rstr,
                                                         StringFormat.GenericDefault);
                        }
                    }
                }
                e.Handled = true;
            }
        }
        #endregion

        private void Data_View_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //string[] str_Top = new string[] { "【切换前】信息", "【切换后】信息", "【恢复后】信息" };
            //string[] str_Values = new string[] { "运行时区", "时区表切换时间", "时段表切换时间",  "时区切换正向有功", "时段切换正向有功" };
            //for (int i = 0; i < str_Top.Length; i++)
            //{
            //    CellPainting(sender, e, 5 + i * 5, str_Top[i], str_Values);
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FunctionDataView
{
    public partial class ViewTimingData : UserControl
    {
        /// <summary>
        /// 读取计时功能数据项目ID
        /// </summary>
        private const string Key = "002";
              
        public ViewTimingData()
        {
            InitializeComponent();

            int _ColIndex = 0;
            //int iCols = 32;
            int iCols = 36;
            _ColIndex = Data_View.Columns.Add("Data_Z", "   表号   ");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;


            for (int i = 0; i < iCols; i++)
            {
                _ColIndex = Data_View.Columns.Add("Data_Z", "    计时功能    ");
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
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


            for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {
                Data_View.Rows[i].Cells[0].Value = string.Format("第{0}表位", i + 1);
                for (int j = 1; j <= 8; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    if (!MeterGroup[i].YaoJianYn) continue;
                    if (!MeterGroup[i].MeterFunctions.ContainsKey(_Key))
                    {
                        for (int k = 0; k < 4; k++)
                            Data_View.Rows[i].Cells[(j - 1) * 4 + k + 1].Value = "";                       
                        continue;
                    }
                    if (MeterGroup[i].MeterFunctions[_Key].Mf_chrValue == null || MeterGroup[i].MeterFunctions[_Key].Mf_chrValue == string.Empty) continue;

                    
                    string[] arrValue = null;
                    if (MeterGroup[i].MeterFunctions[_Key].Mf_chrValue.IndexOf('|') == -1)
                        continue;
                    arrValue = MeterGroup[i].MeterFunctions[_Key].Mf_chrValue.Split('|');
                    for (int k = 0; k < arrValue.Length; k++)
                        Data_View.Rows[i].Cells[(j - 1) * 4 + k + 1].Value = arrValue[k];
                    
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

            for (int j = 1; j <= 8; j++)
            {
                string _Key = Key + j.ToString("D2");

                if (!MeterInfo.MeterFunctions.ContainsKey(_Key))
                {
                    for (int k = 0; k < 4; k++)
                        Data_View.Rows[RowIndex].Cells[(j - 1) * 4 + k + 1].Value = "";
                    continue;
                }
                if (MeterInfo.MeterFunctions[_Key].Mf_chrValue == null || MeterInfo.MeterFunctions[_Key].Mf_chrValue == string.Empty) continue;


                string[] arrValue = null;
                if (MeterInfo.MeterFunctions[_Key].Mf_chrValue.IndexOf('|') == -1)
                    return ;
                arrValue = MeterInfo.MeterFunctions[_Key].Mf_chrValue.Split('|');
                for (int k = 0; k < arrValue.Length; k++)
                    Data_View.Rows[RowIndex].Cells[(j - 1) * 4 + k + 1].Value = arrValue[k];

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
            if (e.RowIndex == -1 && (e.ColumnIndex == start || e.ColumnIndex == start + 1 || e.ColumnIndex == start + 2 || e.ColumnIndex == start + 3 ||
                                        e.ColumnIndex == start + 4 || e.ColumnIndex == start + 5 || e.ColumnIndex == start + 6||
                                        e.ColumnIndex == start + 7 || e.ColumnIndex == start + 8 )) //列上加了个表头
            {
                if (e.ColumnIndex == start)
                {
                    top = e.CellBounds.Top;
                    left = e.CellBounds.Left;
                    height = e.CellBounds.Height;
                    width1 = e.CellBounds.Width;
                }

                int width2 = this.Data_View.Columns[2].Width;
                int width3 = this.Data_View.Columns[3].Width;
                int width4 = this.Data_View.Columns[4].Width;
                int width5 = this.Data_View.Columns[5].Width;
                int sumWidth = width1 + width2 + width3 + width4 + width5;

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
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2, height / 2, left + width1 + width2, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3, height / 2, left + width1 + width2 + width3, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3 + width4 - 1, height / 2, left + width1 + width2 + width3 + width4 - 1, height);

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
                                                     left + lstr - 10,
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
                        iTmp += this.Data_View.Columns[i].Width;
                        if (columnValue != "")
                        {
                            e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                       new SolidBrush(e.CellStyle.ForeColor),
                                                         iTmp - 70,
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
            string[] str_Top = new string[] { "【23点55分前】广播校时",  "【23点57分】广播校时", "【零点1分】广播校时", 
                "【零点5分后】广播校时", "【小于5分后】广播校时", "【大于5分】广播校时",
                "【一天一次】广播校时", "【一天重复】广播校时", "【带表地址】广播校时" };
            string[] str_Values = new string[] { "广播校时前时间", "广播校时时间", "广播校时后时间", "结论" };
            for (int i = 0; i < str_Top.Length; i++)
            {
                CellPainting(sender, e, 1 + i * 4, str_Top[i], str_Values);
            }
        }
    }
}

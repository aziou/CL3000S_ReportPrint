using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FreezeDataView
{
    public partial class ViewDataTiming : UserControl
    {
        public ViewDataTiming()
        {
            InitializeComponent();
            int _ColIndex = 0;
            int iCols =12;

            _ColIndex = Data_View.Columns.Add("Data_Z", "   表号   ");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;


            for (int i = 0; i < iCols; i++)
            {
                _ColIndex = Data_View.Columns.Add("Data_Z", "   冻结   ");
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private const string Key_Month = "00101";                     //月冻结电量在集合中存储的关键字
        private const string Key_Day = "00102";                     //日冻结电量在集合中存储的关键字
        private const string Key_Hour = "00103";                     //小时冻结电量在集合中存储的关键字

        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count)          //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
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
                //月冻结
                if ((MeterGroup[i].MeterFreezes.ContainsKey(Key_Month) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key_Month].Md_chrValue)))
                {
                    string[] _MonthValues = MeterGroup[i].MeterFreezes[Key_Month].Md_chrValue.Split('|');
                    if (_MonthValues.Length != 4) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _MonthValues.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 1].Value = _MonthValues[j];
                    }
                }
                //日冻结
                if (MeterGroup[i].MeterFreezes.ContainsKey(Key_Day) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key_Day].Md_chrValue))
                {
                    string[] _DayValues = MeterGroup[i].MeterFreezes[Key_Day].Md_chrValue.Split('|');
                    if (_DayValues.Length != 4) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _DayValues.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 5].Value = _DayValues[j];
                    }

                }
                //小时冻结
                if (MeterGroup[i].MeterFreezes.ContainsKey(Key_Hour) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key_Hour].Md_chrValue))
                {
                    string[] _HourValues = MeterGroup[i].MeterFreezes[Key_Hour].Md_chrValue.Split('|');
                    if (_HourValues.Length != 4) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _HourValues.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 9].Value = _HourValues[j];
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
          
            Data_View.Rows.Clear();
            if (MeterInfo == null) return;
            if (MeterInfo.MeterFreezes.Count > 0)
            {
                int RowIndex = Data_View.Rows.Add();
                Data_View.Rows[RowIndex].Cells[0].Value = string.Format("{0}表位", MeterInfo._intBno +1);
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                Data_View.Refresh();


                //月冻结
                if ((MeterInfo.MeterFreezes.ContainsKey(Key_Month) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key_Month].Md_chrValue)))
                {
                    string[] _MonthValues = MeterInfo.MeterFreezes[Key_Month].Md_chrValue.Split('|');
                    if (_MonthValues.Length != 4) return ;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _MonthValues.Length - 1; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 1].Value = _MonthValues[j];
                    }
                }
                //日冻结
                if (MeterInfo.MeterFreezes.ContainsKey(Key_Day) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key_Day].Md_chrValue))
                {
                    string[] _DayValues = MeterInfo.MeterFreezes[Key_Day].Md_chrValue.Split('|');
                    if (_DayValues.Length != 4) return;         //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _DayValues.Length - 1; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 4].Value = _DayValues[j];
                    }

                }
                //小时冻结
                if (MeterInfo.MeterFreezes.ContainsKey(Key_Hour) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key_Hour].Md_chrValue))
                {
                    string[] _HourValues = MeterInfo.MeterFreezes[Key_Hour].Md_chrValue.Split('|');
                    if (_HourValues.Length != 4) return;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _HourValues.Length - 1; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 7].Value = _HourValues[j];
                    }
                }
            }
        }

        private void Data_View_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            string[] str_Top = new string[] { "月（周期）", "日（周期）", "小时（周期）" };
            string[] str_Values = new string[] { "冻结前电量", "冻结时电量", "冻结后电量", "     结论   " };
            for (int i = 0; i < str_Top.Length; i++)
            {
                CellPainting(sender, e, 1 + i * 4, str_Top[i], str_Values);
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
            if (e.RowIndex == -1 && (e.ColumnIndex == start || e.ColumnIndex == start + 1 || e.ColumnIndex == start + 2 || e.ColumnIndex == start + 3)) //列上加了个表头
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


                Rectangle rect = new Rectangle(left, top, width1 + width2 + width3 + width4, e.CellBounds.Height);
                using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    //抹去原来的cell背景
                    e.Graphics.FillRectangle(backColorBrush, rect);
                }

                using (Pen gridLinePen = new Pen(dgv.GridColor))
                {
                    e.Graphics.DrawLine(gridLinePen, left, 0, left + width1 + width2 + width3 + width4, 0);
                    e.Graphics.DrawLine(gridLinePen, left, height / 2, left + width1 + width2 + width3 + width4, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left, height, left + width1 + width2 + width3 + width4, height);

                    e.Graphics.DrawLine(gridLinePen, left, 0, left, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3 + width4, 0, left + width1 + width2 + width3 + width4, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left, height / 2, left, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1, height / 2, left + width1, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2, height / 2, left + width1 + width2, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3, height / 2, left + width1 + width2 + width3, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3 + width4, height / 2, left + width1 + width2 + width3 + width4, height);

                    //计算绘制字符串的位置
                    string columnValue = topValue;
                    SizeF sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    float lstr = (width1 + width2 + width3 + width4 - sf.Width) / 2;
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


                    //计算绘制字符串的位置
                    columnValue = bottomValues[0];
                    sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    lstr = (width1 - sf.Width) / 2;
                    rstr = (height / 2 - sf.Height) / 2;
                    //画出文本框

                    if (columnValue != "")
                    {
                        e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                   new SolidBrush(e.CellStyle.ForeColor),
                                                     left + lstr,
                                                     top + height / 2 + rstr,
                                                     StringFormat.GenericDefault);
                    }


                    //计算绘制字符串的位置
                    columnValue = bottomValues[1];
                    sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    lstr = (width2 - sf.Width) / 2;
                    rstr = (height / 2 - sf.Height) / 2;
                    //画出文本框

                    if (columnValue != "")
                    {
                        e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                   new SolidBrush(e.CellStyle.ForeColor),
                                                     left + width1 + lstr,
                                                     top + height / 2 + rstr,
                                                     StringFormat.GenericDefault);
                    }


                    //计算绘制字符串的位置
                    columnValue = bottomValues[2];
                    sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    lstr = (width3 - sf.Width) / 2;
                    rstr = (height / 2 - sf.Height) / 2;
                    //画出文本框
                    if (columnValue != "")
                    {
                        e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                   new SolidBrush(e.CellStyle.ForeColor),
                                                     left + width1 +width2 +lstr,
                                                     top + height / 2 + rstr,
                                                     StringFormat.GenericDefault);
                    }

                    columnValue = bottomValues[3];
                    sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    lstr = (width4 - sf.Width) / 2;
                    rstr = (height / 2 - sf.Height) / 2;

                    if (columnValue != "")
                    {
                        e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                   new SolidBrush(e.CellStyle.ForeColor),
                                                     left + width1 + width2 + width3+ lstr,
                                                     top + height / 2 + rstr,
                                                     StringFormat.GenericDefault);
                    }
                }
                e.Handled = true;
            }
        }
        #endregion
    }
}

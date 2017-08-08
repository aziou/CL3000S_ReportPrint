using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FreezeDataView
{
    public partial class ViewDataAppoint : UserControl
    {
        private const string Key1 = "00401";                     //两套时区表切换冻结电量在集合中存储的关键字
        private const string Key2 = "00402";                     //两套日时段表切换冻结电量在集合中存储的关键字
        private const string Key3 = "00403";                     //两套费率电价切换冻结电量在集合中存储的关键字
        private const string Key4 = "00404";                     //两套阶梯切换冻结电量在集合中存储的关键字

        public ViewDataAppoint()
        {
            InitializeComponent();
        }

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

                //两套时区表切换冻结
                if ((MeterGroup[i].MeterFreezes.ContainsKey(Key1) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key1].Md_chrValue)))
                {
                    string[] _Values = MeterGroup[i].MeterFreezes[Key1].Md_chrValue.Split('|');
                    if (_Values.Length != 3) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 1].Value = _Values[j];
                    }
                }

                //两套日时段表切换冻结
                if ((MeterGroup[i].MeterFreezes.ContainsKey(Key2) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key2].Md_chrValue)))
                {
                    string[] _Values = MeterGroup[i].MeterFreezes[Key2].Md_chrValue.Split('|');
                    if (_Values.Length != 3) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 4].Value = _Values[j];
                    }
                }

                //两套费率电价切换冻结
                if ((MeterGroup[i].MeterFreezes.ContainsKey(Key3) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key3].Md_chrValue)))
                {
                    string[] _Values = MeterGroup[i].MeterFreezes[Key3].Md_chrValue.Split('|');
                    if (_Values.Length != 3) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 7].Value = _Values[j];
                    }
                }

                //两套阶梯切换冻结
                if ((MeterGroup[i].MeterFreezes.ContainsKey(Key4) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key4].Md_chrValue)))
                {
                    string[] _Values = MeterGroup[i].MeterFreezes[Key4].Md_chrValue.Split('|');
                    if (_Values.Length != 3) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 10].Value = _Values[j];
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
                Data_View.Rows[RowIndex].Cells[0].Value = string.Format("{0}表位", MeterInfo._intBno + 1);
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;

                //两套时区表切换冻结
                if ((MeterInfo.MeterFreezes.ContainsKey(Key1) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key1].Md_chrValue)))
                {
                    string[] _Values = MeterInfo.MeterFreezes[Key1].Md_chrValue.Split('|');
                    if (_Values.Length != 3) return ;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 1].Value = _Values[j];
                    }
                }

                //两套日时段表切换冻结
                if ((MeterInfo.MeterFreezes.ContainsKey(Key2) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key2].Md_chrValue)))
                {
                    string[] _Values = MeterInfo.MeterFreezes[Key2].Md_chrValue.Split('|');
                    if (_Values.Length != 3) return;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 4].Value = _Values[j];
                    }
                }

                //两套费率电价切换冻结
                if ((MeterInfo.MeterFreezes.ContainsKey(Key3) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key3].Md_chrValue)))
                {
                    string[] _Values = MeterInfo.MeterFreezes[Key3].Md_chrValue.Split('|');
                    if (_Values.Length != 3) return ;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 7].Value = _Values[j];
                    }
                }

                //两套阶梯切换冻结
                if ((MeterInfo.MeterFreezes.ContainsKey(Key4) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key4].Md_chrValue)))
                {
                    string[] _Values = MeterInfo.MeterFreezes[Key4].Md_chrValue.Split('|');
                    if (_Values.Length != 3) return;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 10].Value = _Values[j];
                    }
                }

            }
        }


        private void Data_View_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            string[] str_Top = new string[] { "两套时区表切换冻结", "两套日时段表切换冻结", "两套费率电价切换冻结", "两套阶梯切换冻结" };
            string[] str_Values = new string[] { "冻结前电量", "冻结时电量", "冻结后电量" };
            for (int i = 0; i < str_Top.Length; i++)
            {
                CellPainting(sender, e, 1 + i * 3, str_Top[i], str_Values);
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
            if (e.RowIndex == -1 && (e.ColumnIndex == start || e.ColumnIndex == start + 1 || e.ColumnIndex == start + 2)) //列上加了个表头
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

                Rectangle rect = new Rectangle(left, top, width1 + width2 + width3, e.CellBounds.Height);
                using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    //抹去原来的cell背景
                    e.Graphics.FillRectangle(backColorBrush, rect);
                }

                using (Pen gridLinePen = new Pen(dgv.GridColor))
                {
                    e.Graphics.DrawLine(gridLinePen, left, 0, left + width1 + width2 + width3, 0);
                    e.Graphics.DrawLine(gridLinePen, left, height / 2, left + width1 + width2 + width3, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left, height, left + width1 + width2 + width3, height);

                    e.Graphics.DrawLine(gridLinePen, left, 0, left, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3, 0, left + width1 + width2 + width3, height / 2);
                    e.Graphics.DrawLine(gridLinePen, left, height / 2, left, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1, height / 2, left + width1, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2, height / 2, left + width1 + width2, height);
                    e.Graphics.DrawLine(gridLinePen, left + width1 + width2 + width3, height / 2, left + width1 + width2 + width3, height);

                    //计算绘制字符串的位置
                    string columnValue = topValue;
                    SizeF sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                    float lstr = (width1 + width2 + width3 - sf.Width) / 2;
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
                                                     left + width1 + width2 + lstr,
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.EventLogDataView
{
    public partial class ViewEventOpenMeterCover : UserControl
    {
        /// <summary>
        /// 读取开盖事件记录数据项目ID
        /// </summary>
        private string Key = "";
              
        public ViewEventOpenMeterCover()
        {
            Key = ((int)CLDC_Comm.Enum.Cus_EventLogItem.开表盖记录).ToString().PadLeft(3, '0');

            InitializeComponent();

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            int intColNo = 1;
            int _ColIndex = 0;
            int iCols = 6;
            string strColName = "";
            string strTitleName = "";

            Data_View.Columns.Clear();

            string[] arrTitle = new string[] { " 表号 " };

            foreach (string strTitle in arrTitle)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, strTitle);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            string[] str_Values = new string[] { "开表盖总次数", "事件记录发生时刻", "事件记录结束时刻" };
            for (int i = 0; i < iCols; i++)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, str_Values[i % 3]);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            Data_View.MergeColumnNames.Add("Column1");


            strTitleName = "开表盖";

            strColName = string.Format("【{0}】事件产生（前）信息", strTitleName);
            Data_View.AddSpanHeader(1, 3, strColName);
            strColName = string.Format("【{0}】事件产生（后）信息", strTitleName);
            Data_View.AddSpanHeader(4, 3, strColName);
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
                for (int j = 1; j <= 2; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    if (!MeterGroup[i].YaoJianYn) continue;
                    if (!MeterGroup[i].MeterSjJLgns.ContainsKey(_Key))
                    {
                        for (int k = 0; k < 3; k++)
                            Data_View.Rows[i].Cells[(j - 1) * 3 + k + 1].Value = "";
                        continue;
                    }
                    if (MeterGroup[i].MeterSjJLgns[_Key].RecordOther == null || MeterGroup[i].MeterSjJLgns[_Key].RecordOther == string.Empty) continue;
                    else
                    {
                        string[] arrValue = null;
                        if (MeterGroup[i].MeterSjJLgns[_Key].RecordOther.IndexOf('|') == -1)
                            continue;
                        arrValue = MeterGroup[i].MeterSjJLgns[_Key].RecordOther.Split('|');
                        for (int k = 0; k < arrValue.Length; k++)
                        {
                            Data_View.Rows[i].Cells[(j - 1) * 3 + k + 1].Value = arrValue[k];

                        }
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

            if (MeterInfo.MeterSjJLgns.Count == 0) return;
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

            int CountIndex = 1;
            for (int j = 1; j <= 2; j++)
            {
                string _Key = Key + j.ToString("D2");
                if (MeterInfo.MeterSjJLgns.ContainsKey(_Key))
                {
                    string[] arrValue = null;
                    if (MeterInfo.MeterSjJLgns[_Key].RecordOther.IndexOf('|') == -1)
                        continue;
                    arrValue = MeterInfo.MeterSjJLgns[_Key].RecordOther.Split('|');
                    for (int k = 0; k < arrValue.Length; k++)
                        Data_View.Rows[RowIndex].Cells[CountIndex++].Value = arrValue[k];

                }
                else
                {
                    CountIndex += 2;
                }

            }
        }

        

        private void Data_View_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}

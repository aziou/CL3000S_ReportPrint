using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
using CLDC_DataCore.Function;

namespace CLDC_MeterUI.UI_Detection_New.LoadRecordDataView
{
    public partial class ViewLoadRecord : UserControl
    {
        /// <summary>
        /// 读取负荷记录数据项目ID
        /// </summary>
        private string Key = "001";

        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo m_MeterInfo;

        public ViewLoadRecord()
        {

            InitializeComponent();

            cmb_RecordType.SelectedIndex = 0;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            int intColNo = 1;
            int _ColIndex = 0;
            int iCols = 2;
            string strColName = "";            
            
            Data_View.Columns.Clear();

            string[] arrTitle = new string[] { "表号", "次数" };

            foreach (string strTitle in arrTitle)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, strTitle);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            string[] str_Values = new string[] { "负荷记录信息",  "记录时间"};
            for (int i = 0; i < iCols; i++)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, str_Values[i]);
                Data_View.Columns[_ColIndex].Tag = 0;
                //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i % 2 == 0) Data_View.Columns[_ColIndex].Width = 500;
            }
            Data_View.MergeColumnNames.Add("Column1");                        
            
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            int intRecordType = 1;
            int intTestNum = 0;
            string[] arrParam = null;
            int intSchemeCount = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan.Count;
            int intKey = int.Parse(Key);
            
            CLDC_DataCore.Struct.StPlan_LoadRecord _Item = new CLDC_DataCore.Struct.StPlan_LoadRecord();

            for (int intInc = 0; intInc < intSchemeCount; intInc++)
            {
                if (GlobalUnit.g_CUS.DnbData.CheckPlan[intInc] is CLDC_DataCore.Struct.StPlan_LoadRecord)
                {
                    if (((CLDC_DataCore.Struct.StPlan_LoadRecord)GlobalUnit.g_CUS.DnbData.CheckPlan[intInc]).PrjID == Key)
                    {
                        _Item = (CLDC_DataCore.Struct.StPlan_LoadRecord)GlobalUnit.g_CUS.DnbData.CheckPlan[intInc];
                        intTestNum = _Item.OverTime / 1;// _Item.MarginTime;
                    }
                }
            }
            intRecordType = cmb_RecordType.SelectedIndex + 1;

            if (intTestNum < 1) return;

            if (intTestNum > 10) intTestNum = 10;
            
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count * intTestNum)           //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
            {
                Data_View.Rows.Clear();
                for (int intIna = 0; intIna < MeterGroup.Count; intIna++)
                {
                    for (int intInb = 0; intInb < intTestNum; intInb++)
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
                        Data_View.Rows[intIna * intTestNum + intInb].Cells[0].Value = string.Format("第{0}表位", intIna + 1);
                        Data_View.Rows[intIna * intTestNum + intInb].Cells[1].Value = string.Format("第{0}次", intInb + 1);
                    }
                }
                Data_View.Refresh();
            }


            for (int intIna = 0; intIna < MeterGroup.Count; intIna++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {
                for (int intCurrentNum = 1; intCurrentNum <= intTestNum; intCurrentNum++)
                {

                    if (!MeterGroup[intIna].YaoJianYn) continue;

                    string _Key = Key + intRecordType.ToString("D2") + intCurrentNum.ToString("D2");

                    if (MeterGroup[intIna].MeterLoadRecords == null) continue;
                    if (!MeterGroup[intIna].MeterLoadRecords.ContainsKey(_Key))
                    {
                        for (int k = 0; k < 2; k++)
                            Data_View.Rows[intIna * intTestNum + intCurrentNum - 1].Cells[k + 2].Value = "";
                        continue;
                    }
                    if (MeterGroup[intIna].MeterLoadRecords[_Key].Ml_chrValue == null || MeterGroup[intIna].MeterLoadRecords[_Key].Ml_chrValue == string.Empty) continue;
                    else
                    {
                        string[] arrValue = null;
                        if (MeterGroup[intIna].MeterLoadRecords[_Key].Ml_chrValue.IndexOf(',') == -1)
                            continue;
                        arrValue = MeterGroup[intIna].MeterLoadRecords[_Key].Ml_chrValue.Split(',');
                        for (int k = 0; k < arrValue.Length; k++)
                        {
                            Data_View.Rows[intIna * intTestNum + intCurrentNum - 1].Cells[k + 2].Value = arrValue[k];

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
            int intRecordType = cmb_RecordType.SelectedIndex + 1;
            m_MeterInfo = MeterInfo;
            if (MeterInfo == null) return;
            if (MeterInfo.MeterLoadRecords.Count == 0) return;
            Data_View.Rows.Clear();
           
            for (int intCurrentNum = 1; intCurrentNum <= 10; intCurrentNum++)
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
                Data_View.Rows[intCurrentNum - 1].Cells[0].Value = MeterInfo.ToString();
                Data_View.Rows[intCurrentNum - 1].Cells[1].Value = string.Format("第{0}次", intCurrentNum);


                string _Key = Key + intRecordType.ToString("D2") + intCurrentNum.ToString("D2");

                if (MeterInfo.MeterLoadRecords == null) continue;
                if (!MeterInfo.MeterLoadRecords.ContainsKey(_Key))
                {
                    for (int k = 0; k < 2; k++)
                        Data_View.Rows[intCurrentNum - 1].Cells[k + 2].Value = "";
                    continue;
                }
                if (MeterInfo.MeterLoadRecords[_Key].Ml_chrValue == null || MeterInfo.MeterLoadRecords[_Key].Ml_chrValue == string.Empty) continue;
                else
                {
                    string[] arrValue = null;
                    if (MeterInfo.MeterLoadRecords[_Key].Ml_chrValue.IndexOf(',') == -1)
                        continue;
                    arrValue = MeterInfo.MeterLoadRecords[_Key].Ml_chrValue.Split(',');
                    for (int k = 0; k < arrValue.Length; k++)
                    {
                        Data_View.Rows[intCurrentNum - 1].Cells[k + 2].Value = arrValue[k];

                    }
                }

            }
        }
        


        private void Data_View_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void cmb_RecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GlobalUnit.g_CUS == null)
                SetData(m_MeterInfo, true);
            else
                SetData(GlobalUnit.g_CUS.DnbData.MeterGroup);
        }
    }
}

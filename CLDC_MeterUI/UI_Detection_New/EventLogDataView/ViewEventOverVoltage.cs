using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Const;
using CLDC_DataCore.Function;

namespace CLDC_MeterUI.UI_Detection_New.EventLogDataView
{
    public partial class ViewEventOverVoltage : UserControl
    {
        /// <summary>
        /// 过压事件记录数据项目ID
        /// </summary>
        private string Key;

        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo m_MeterInfo;

        public ViewEventOverVoltage()
        {
            Key = ((int)CLDC_Comm.Enum.Cus_EventLogItem.过压记录).ToString().PadLeft(3, '0');

            InitializeComponent();

            cmb_PhaseType.SelectedIndex = 0;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            int intColNo = 1;
            int _ColIndex = 0;
            int iCols = 8;
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
            string[] str_Values = new string[] { "事件状态", "过压总次数", "事件记录发生时刻", "事件记录结束时刻" };
            for (int i = 0; i < iCols; i++)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, str_Values[i % 4]);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            Data_View.MergeColumnNames.Add("Column1");

            Data_View.AddSpanHeader(2, 4, "【过压】事件产生（前）信息");
            Data_View.AddSpanHeader(6, 4, "【过压】事件产生（后）信息"); 
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            int intPhase = 1;
            int intTestNum = 0;
            string[] arrParam = null;
            int intSchemeCount = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan.Count;
            int intKey = int.Parse(Key);

            CLDC_DataCore.Struct.StPlan_EventLog _Item = new CLDC_DataCore.Struct.StPlan_EventLog();

            for (int intInc = 0; intInc < intSchemeCount; intInc++)
            {
                if (GlobalUnit.g_CUS.DnbData.CheckPlan[intInc] is CLDC_DataCore.Struct.StPlan_EventLog)
                {
                    if (((CLDC_DataCore.Struct.StPlan_EventLog)GlobalUnit.g_CUS.DnbData.CheckPlan[intInc]).EventLogPrjID == Key)
                        _Item = (CLDC_DataCore.Struct.StPlan_EventLog)GlobalUnit.g_CUS.DnbData.CheckPlan[intInc];
                }
            }
            if (_Item.PrjParm == null) return;
            if (_Item.PrjParm.Length < 1) return;
            arrParam = _Item.PrjParm.Split('|');
            if (arrParam.Length < 4) return;

            intPhase = cmb_PhaseType.SelectedIndex + 1;

            intTestNum = int.Parse(arrParam[2]);

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
                        Data_View.Rows[intIna * intTestNum + intInb].Cells[1].Value = string.Format("上{0}次", intInb + 1);
                    }
                }
                Data_View.Refresh();
            }


            for (int intIna = 0; intIna < MeterGroup.Count; intIna++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {
                for (int intCurrentNum = 1; intCurrentNum <= intTestNum; intCurrentNum++)
                {
                    for (int intHappen = 1; intHappen <= 2; intHappen++)
                    {
                        if (!MeterGroup[intIna].YaoJianYn) continue;

                        string _Key = Key + intPhase.ToString("D2") + intCurrentNum.ToString("D2") + intHappen.ToString("D2");


                        if (!MeterGroup[intIna].MeterSjJLgns.ContainsKey(_Key))
                        {
                            for (int k = 0; k < 4; k++)
                                Data_View.Rows[intIna * intTestNum + intCurrentNum - 1].Cells[(intHappen - 1) * 4 + k + 2].Value = "";
                            continue;
                        }
                        if (MeterGroup[intIna].MeterSjJLgns[_Key].RecordOther == null || MeterGroup[intIna].MeterSjJLgns[_Key].RecordOther == string.Empty) continue;
                        else
                        {
                            string[] arrValue = null;
                            if (MeterGroup[intIna].MeterSjJLgns[_Key].RecordOther.IndexOf('|') == -1)
                                continue;
                            arrValue = MeterGroup[intIna].MeterSjJLgns[_Key].RecordOther.Split('|');
                            for (int k = 0; k < arrValue.Length; k++)
                            {
                                Data_View.Rows[intIna * intTestNum + intCurrentNum - 1].Cells[(intHappen - 1) * 4 + k + 2].Value = arrValue[k];
                            }
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
            int intPhase = 1;
            intPhase = cmb_PhaseType.SelectedIndex + 1;
            m_MeterInfo = MeterInfo;
            if (MeterInfo == null) return;
            if (MeterInfo.MeterSjJLgns.Count == 0) return;
            Data_View.Rows.Clear();
            int RowIndex = 0;

            for (int intCurrentNum = 1; intCurrentNum <= 12; intCurrentNum++)
            {
                RowIndex = Data_View.Rows.Add();
                if ((RowIndex + 1) % 2 == 0)
                {
                    Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                }
                else
                {
                    Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                }
                Data_View.Rows[intCurrentNum - 1].Cells[0].Value = MeterInfo.ToString();
                Data_View.Rows[intCurrentNum - 1].Cells[1].Value = string.Format("上{0}次", intCurrentNum);

                for (int intHappen = 1; intHappen <= 2; intHappen++)
                {
                    string _Key = Key + intPhase.ToString("D2") + intCurrentNum.ToString("D2") + intHappen.ToString("D2");


                    if (!MeterInfo.MeterSjJLgns.ContainsKey(_Key))
                    {
                        for (int k = 0; k < 4; k++)
                            Data_View.Rows[intCurrentNum - 1].Cells[(intHappen - 1) * 4 + k + 2].Value = "";
                        continue;
                    }
                    if (MeterInfo.MeterSjJLgns[_Key].RecordOther == null || MeterInfo.MeterSjJLgns[_Key].RecordOther == string.Empty) continue;
                    else
                    {
                        string[] arrValue = null;
                        if (MeterInfo.MeterSjJLgns[_Key].RecordOther.IndexOf('|') == -1)
                            continue;
                        arrValue = MeterInfo.MeterSjJLgns[_Key].RecordOther.Split('|');
                        for (int k = 0; k < arrValue.Length; k++)
                        {
                            Data_View.Rows[intCurrentNum - 1].Cells[(intHappen - 1) * 4 + k + 2].Value = arrValue[k];

                        }
                    }
                }
            } 
        }

       

        private void cmb_PhaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobalUnit.g_CUS == null)
                SetData(m_MeterInfo, true);
            else
                SetData(GlobalUnit.g_CUS.DnbData.MeterGroup);
        }

        
    }
}

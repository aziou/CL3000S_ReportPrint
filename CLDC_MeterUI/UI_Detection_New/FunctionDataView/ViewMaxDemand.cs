using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using CLDC_DataCore.Const;
using CLDC_DataCore.Function;

namespace CLDC_MeterUI.UI_Detection_New.FunctionDataView
{
    public partial class ViewMaxDemand : UserControl
    {
        /// <summary>
        /// 读取最大需量功能项目ID
        /// </summary>
        private string Key = "001";

        private int m_intTestNum;

        private int m_intGlfx = 0;

        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo m_MeterInfo;

        public ViewMaxDemand()
        {
            InitializeComponent();

            Key = ((int)CLDC_Comm.Enum.Cus_FunctionItem.最大需量功能).ToString().PadLeft(3, '0');

            cmb_Type.SelectedIndex = 0;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            int intColNo = 1;
            int _ColIndex = 0;
            int iCols = 0;
            string strColName = "";

            Data_View.Columns.Clear();

            string[] arrTitle = new string[] { " 表号 ", "结算次数", "第1结算日", "组合有功特征字", "组合无功1特征字", "组合无功2特征字" };

            foreach (string strTitle in arrTitle)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, strTitle);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            string[] str_Values = new string[] { "总", "尖", "峰", "平", "谷" };

            iCols = str_Values.Length;

            
            for (int i = 0; i < iCols; i++)
            {
                strColName = string.Format("Column{0}", intColNo++);
                _ColIndex = Data_View.Columns.Add(strColName, str_Values[i % iCols]);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            Data_View.MergeColumnNames.Add("Column1");

            Data_View.AddSpanHeader(6, 5, "需量信息");            
       
        }

        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            string strValue = "";

            string[] arrParam = null;

            m_intGlfx = cmb_Type.SelectedIndex + 1;

            if (CLDC_DataCore.Const.GlobalUnit.g_CUS == null) return;
            int intSchemeCount = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan.Count;

            CLDC_DataCore.Struct.StPlan_Function _Item = new CLDC_DataCore.Struct.StPlan_Function();

            for (int intInc = 0; intInc < intSchemeCount; intInc++)
            {
                if (GlobalUnit.g_CUS.DnbData.CheckPlan[intInc] is CLDC_DataCore.Struct.StPlan_Function)
                {
                    if (((CLDC_DataCore.Struct.StPlan_Function)GlobalUnit.g_CUS.DnbData.CheckPlan[intInc]).FunctionPrjID == Key)
                        _Item = (CLDC_DataCore.Struct.StPlan_Function)GlobalUnit.g_CUS.DnbData.CheckPlan[intInc];
                }
            }
            if (_Item.PrjParm == null) return;
            if (_Item.PrjParm.Length < 1) return;
            arrParam = _Item.PrjParm.Split('|');
            if (arrParam.Length < 3) return;

            m_intTestNum = int.Parse(arrParam[1]);

            if (Data_View == null) return;

            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count * (m_intTestNum+1))           //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
            {
                Data_View.Rows.Clear();
                for (int intIna = 0; intIna < MeterGroup.Count; intIna++)
                {
                    for (int intInb = 0; intInb <= m_intTestNum; intInb++)
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
                        Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup[intIna].ToString();

                        Data_View.Rows[RowIndex].Cells[0].Value = MeterGroup[intIna].ToString();

                        if (intInb == 0)
                            strValue = "当前";
                        else
                            strValue = string.Format("上{0}次", intInb);

                        Data_View.Rows[RowIndex].Cells[1].Value = strValue;
                    }
                }
                Data_View.Refresh();
            }

            for (int intIna = 0; intIna < MeterGroup.Count; intIna++)
            {
                for (int intCurrentNum = 0; intCurrentNum <= m_intTestNum; intCurrentNum++)
                {
                    string _Key = Key + m_intGlfx.ToString("D2") + intCurrentNum.ToString("D2");
                    if (!MeterGroup[intIna].YaoJianYn) continue;
                    int intRowIndex = intIna * (m_intTestNum + 1) + intCurrentNum;
                    
                    if (!MeterGroup[intIna].MeterFunctions.ContainsKey(_Key))
                    {

                        for (int k = 0; k < 9; k++)
                            Data_View.Rows[intRowIndex].Cells[k+2].Value = "";

                        continue;
                    }

                    if (MeterGroup[intIna].MeterFunctions[_Key].Mf_chrValue == null || MeterGroup[intIna].MeterFunctions[_Key].Mf_chrValue == string.Empty) continue;

                    string[] arrValue = null;
                    string[] arrEnergy = null;
                    if (MeterGroup[intIna].MeterFunctions[_Key].Mf_chrValue.IndexOf(',') == -1)
                        continue;
                    arrValue = MeterGroup[intIna].MeterFunctions[_Key].Mf_chrValue.Split(',');

                    if (arrValue.Length < 5) continue;

                    Data_View.Rows[intRowIndex].Cells[2].Value = arrValue[0];
                    Data_View.Rows[intRowIndex].Cells[3].Value = arrValue[1];
                    Data_View.Rows[intRowIndex].Cells[4].Value = arrValue[2];
                    Data_View.Rows[intRowIndex].Cells[5].Value = arrValue[3];

                    arrEnergy = arrValue[4].Split('|');
                    if (arrEnergy.Length < 5) continue;
                    for (int k = 0; k < arrEnergy.Length; k++)
                        Data_View.Rows[intRowIndex].Cells[k + 6].Value = arrEnergy[k];

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
            string strValue;

            m_MeterInfo = MeterInfo;

            m_intGlfx = cmb_Type.SelectedIndex + 1;

            if (MeterInfo == null) return;
            if (MeterInfo.MeterFunctions.Count == 0) return;
            Data_View.Rows.Clear();

            for (int intCurrentNum = 0; intCurrentNum <= 12; intCurrentNum++)
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
                Data_View.Rows[RowIndex].HeaderCell.Value = MeterInfo.ToString();

                Data_View.Rows[RowIndex].Cells[0].Value = MeterInfo.ToString();

                if (intCurrentNum == 0)
                    strValue = "当前";
                else
                    strValue = string.Format("上{0}次", intCurrentNum);

                Data_View.Rows[RowIndex].Cells[1].Value = strValue;


                string _Key = Key + m_intGlfx.ToString("D2") + intCurrentNum.ToString("D2");

                int intRowIndex = intCurrentNum;

                if (!MeterInfo.MeterFunctions.ContainsKey(_Key))
                {

                    for (int k = 0; k < 9; k++)
                        Data_View.Rows[intRowIndex].Cells[k + 2].Value = "";

                    continue;
                }

                if (MeterInfo.MeterFunctions[_Key].Mf_chrValue == null || MeterInfo.MeterFunctions[_Key].Mf_chrValue == string.Empty) continue;

                string[] arrValue = null;
                string[] arrEnergy = null;
                if (MeterInfo.MeterFunctions[_Key].Mf_chrValue.IndexOf(',') == -1)
                    continue;
                arrValue = MeterInfo.MeterFunctions[_Key].Mf_chrValue.Split(',');

                if (arrValue.Length < 5) continue;

                Data_View.Rows[intRowIndex].Cells[2].Value = arrValue[0];
                Data_View.Rows[intRowIndex].Cells[3].Value = arrValue[1];
                Data_View.Rows[intRowIndex].Cells[4].Value = arrValue[2];
                Data_View.Rows[intRowIndex].Cells[5].Value = arrValue[3];

                arrEnergy = arrValue[4].Split('|');
                if (arrEnergy.Length < 5) continue;
                for (int k = 0; k < arrEnergy.Length; k++)
                    Data_View.Rows[intRowIndex].Cells[k + 6].Value = arrEnergy[k];

            }
        }

        

        private void Data_View_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //string[] str_Top = new string[] { "【当前】组合有功电量", "【上1结算日】组合有功电量", "【当前】正向有功电量", "【上1结算日】正向有功电量", "【当前】反向有功电量", "【上1结算日】反向有功电量" };
            //string[] str_Values = new string[] { "总", "尖", "峰", "平", "谷" };
            //for (int i = 0; i < str_Top.Length; i++)
            //{
            //    CellPainting(sender, e, 3 + i * 5, str_Top[i], str_Values);
            //}
        }

       
        private void cmb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_intGlfx = cmb_Type.SelectedIndex;

            if (GlobalUnit.g_CUS == null)
                SetData(m_MeterInfo, true);
            else
                SetData(GlobalUnit.g_CUS.DnbData.MeterGroup);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Model.DnbModel.DnbInfo;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    /// <summary>
    /// 费率电价
    /// </summary>
    public partial class ViewRatesTime : UserControl
    {

        /// <summary>
        /// 费率电价检查项目ID
        /// </summary>
        private const string Key = "017";//044

        private int _FirstColIndex = 0;

        public ViewRatesTime()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CostPlan">要求传入读取费率信息方案</param>
        public ViewRatesTime(CLDC_DataCore.Struct.StPlan_CostControl CostPlan)
        {
            InitializeComponent();
            CLDC_DataCore.Struct.StPlan_Dgn stD = new CLDC_DataCore.Struct.StPlan_Dgn();
            stD.DgnPrjID = CostPlan.CostControlPrjID;
            stD.DgnPrjName = CostPlan.CostControlPrjName;
            stD.OutPramerter = CostPlan.OutPramerter;
            stD.PrjParm = CostPlan.PrjParm;
            InitViewCost(stD);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">要求传入读取费率信息方案</param>
        public ViewRatesTime(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            InitializeComponent();
            InitViewCost(DgnPlan);

        }

        private void InitViewCost(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            if (DgnPlan.DgnPrjID != Key)        //如果项目ID不是读取费率信息的ID则退出！！
                return;

            LoadStepParam(DgnPlan);

            string strShowValue = string.Empty;
            if (needReadTest && needWriteTest)
            {
                strShowValue = "费率数(读)|费率电价(读)|结论(读)|费率数(写)|费率电价(写)|结论(写)";
            }
            else if (needReadTest)
            {
                strShowValue = "费率数(读)|费率电价(读)|结论(读)";
            }
            else if (needWriteTest)
            {
                strShowValue = "费率数(写)|费率电价(写)|结论(写)";
            }
            string[] _ShowValues = strShowValue.Split('|');
            string[] _Values = DgnPlan.PrjParm.Split('|');
            if (_Values.Length == -1 || _Values.Length == 0)
                return;
            for (int i = 0; i < _ShowValues.Length; i++)            //动态创建数据表单样式
            {
                int ColIndex = Data_View.Columns.Add("Data_" + i, _ShowValues[i]);
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            #region 初始化行
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
            #endregion 初始化行

            #region 加载数据
            foreach (DataGridViewColumn column in Data_View.Columns)
            {
                string keyString = string.Empty;
                switch (column.HeaderText)
                {
                    case "费率数(读)":
                        keyString = (int.Parse(Key) + 100).ToString() ;
                        break;
                    case "费率电价(读)":
                        keyString = (int.Parse(Key) + 200).ToString();
                        break;
                    case "结论(读)":
                        keyString = (int.Parse(Key) + 300).ToString();
                        break;
                    case "费率数(写)":
                        keyString = (int.Parse(Key) + 400).ToString();
                        break;
                    case "费率电价(写)":
                        keyString = (int.Parse(Key) + 500).ToString();
                        break;
                    case "结论(写)":
                        keyString = (int.Parse(Key) + 600).ToString();
                        break;
                }

                if (keyString == string.Empty)
                    continue;

                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    for (int j = 1; j < Data_View.ColumnCount; j++)
                    {
                        int _ColIndex = (int)(_FirstColIndex + j - 1);
                        if (!MeterGroup[i].MeterCostControls.ContainsKey(keyString))
                        {
                            Data_View.Rows[i].Cells[column.Index].Value = "";
                            continue;
                        }
                        else
                        {
                            MeterFK meterDgn = MeterGroup[i].MeterCostControls[keyString];
                            Data_View.Rows[i].Cells[column.Index].Value = meterDgn.Mfk_chrData;
                        }
                    }
                }
            }
            #endregion 加载数据
        }

        #region 加载方案参数
        //需要读参数测试
        private bool needReadTest = false;
        //需要写参数测试
        private bool needWriteTest = false;
        /// <summary>
        /// 加载方案参数
        /// </summary>
        /// <param name="DgnPlan"></param>
        private void LoadStepParam(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            string[] paramArray = DgnPlan.PrjParm.Split('|');
            needReadTest = bool.Parse(paramArray[1]);
            needWriteTest = bool.Parse(paramArray[2]);
        }
        #endregion 加载方案参数
    }
}

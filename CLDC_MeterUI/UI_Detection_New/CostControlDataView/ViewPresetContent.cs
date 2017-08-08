using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CostControlDataView
{
    /// <summary>
    /// @C_B
    /// </summary>
    public partial class ViewPresetContent : UserControl
    {
        /// <summary>
        /// 预置内容检查
        /// </summary>
        private const string key1 = "021";
        /// <summary>
        /// 预置内容设置
        /// </summary>
        private const string key2 = "022";
        public ViewPresetContent()
        {
            InitializeComponent();
        }
        public ViewPresetContent(CLDC_DataCore.Struct.StPlan_CostControl costPlan)
            : this()
        {
            InitViewCost(costPlan);
        }
        private void InitViewCost(CLDC_DataCore.Struct.StPlan_CostControl costPlan)
        {
            if (costPlan.CostControlPrjID != key2)        
                return;

            //LoadStepParam(costPlan);

            string strShowValue = "预置金额结论|实际金额结论|报警金额1结论(设置)|报警金额1结论(检查)|报警金额1(设置)|报警金额1(检查)|报警金额2结论(设置)|报警金额2结论(检查)|报警金额2(设置)|报警金额2(检查)|设置电价结论(设置)|费率数(设置)|费率电价(设置)|设置电价结论(检查)|费率数(检查)|费率电价(检查)";
            string[] _ShowValues = strShowValue.Split('|');
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
            for (int i = 0; i < MeterGroup.Count; i++)
            {
                if (MeterGroup[i].YaoJianYn)
                {
                    int num = 0;
                    int tmp = 3;
                    for (int j = 0; j < 8; j++)
                    {
                        string keyTemp1 = key2 + (j+1).ToString().PadLeft(2, '0');
                        string keyTemp2 = key1 + (j+1).ToString().PadLeft(2, '0');
                        if (j < 5)
                        {
                            if (MeterGroup[i].MeterCostControls.ContainsKey(keyTemp1))
                            {
                                Data_View.Rows[i].Cells[j+num].Value = MeterGroup[i].MeterCostControls[keyTemp1].Mfk_chrData;
                            }
                            else
                            {
                                Data_View.Rows[i].Cells[j+num].Value = "";
                            }
                            num++;
                            if (MeterGroup[i].MeterCostControls.ContainsKey(keyTemp2))
                            {
                                Data_View.Rows[i].Cells[j+num].Value = MeterGroup[i].MeterCostControls[keyTemp2].Mfk_chrData;
                            }
                            else
                            {
                                Data_View.Rows[i].Cells[j+num].Value = "";
                            }
                        }
                        else
                        {
                            if (MeterGroup[i].MeterCostControls.ContainsKey(keyTemp1))
                            {
                                Data_View.Rows[i].Cells[j + num].Value = MeterGroup[i].MeterCostControls[keyTemp1].Mfk_chrData;
                            }
                            else
                            {
                                Data_View.Rows[i].Cells[j + num].Value = "";
                            }
                            if (MeterGroup[i].MeterCostControls.ContainsKey(keyTemp2))
                            {
                                Data_View.Rows[i].Cells[j + num+tmp].Value = MeterGroup[i].MeterCostControls[keyTemp2].Mfk_chrData;
                            }
                            else
                            {
                                Data_View.Rows[i].Cells[j + num+tmp].Value = "";
                            }
                        }
                        
                    }
                }
            }
            #endregion 加载数据
        }
        #region 加载方案参数
        //public CLDC_Comm.Enum.Cus_VerifyMode verifyMode = CLDC_Comm.Enum.Cus_VerifyMode.仅核对;
        ///// <summary>
        ///// 加载方案参数
        ///// </summary>
        ///// <param name="DgnPlan"></param>
        //private void LoadStepParam(CLDC_DataCore.Struct.StPlan_CostControl costPlan)
        //{
        //    string[] paramArray = costPlan.PrjParm.Split('|');
        //    int intMode = 0;
        //    int.TryParse(paramArray[1], out intMode);
        //    verifyMode = (CLDC_Comm.Enum.Cus_VerifyMode)intMode;
        //}
        #endregion 加载方案参数
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Const;

namespace CLDC_MeterUI.UI_Detection_New.CostControlDataView
{
    /// <summary>
    ///控制功能UI
    /// </summary>
    public partial class ViewControlFunction : UserControl
    {
        /// <summary>
        /// 读取控制功能项目ID
        /// </summary>
        private const string Key = "015" ;
        public ViewControlFunction()
        {
            InitializeComponent();
        }
        public ViewControlFunction(CLDC_DataCore.Struct.StPlan_CostControl CostControlPlan)
        {
            InitializeComponent();
            if (CostControlPlan.CostControlPrjID != Key)        //如果项目ID不是计度器示值组合误差的ID则退出！！
            {
                return;
            }
            Data_View.RowHeadersVisible = false;
            int intTemp = Data_View.Columns.Add("表位号", "表位号");
            Data_View.Columns[intTemp].Tag = 0;
            Data_View.Columns[intTemp].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[intTemp].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[intTemp].SortMode = DataGridViewColumnSortMode.NotSortable;

            //当前电价|开始剩余金额|开始总电量|结束剩余金额|结束总电量|误差
            //结论
            //不合格原因
            string[] arrayTemp = new string[]{"报警测试写剩余金额","写报警金额","报警测试读电价","报警状态字","跳闸测试写入剩余金额","写跳闸金额","跳闸测试读电价","跳闸状态字",
"结论"};
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                int _ColIndex = Data_View.Columns.Add(arrayTemp[i], arrayTemp[i]);
                Data_View.Columns[_ColIndex].Tag = 0;
                Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                Data_View.Rows.Add();
                Data_View.Rows[i].Cells["表位号"].Value = i + 1;
            }
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            for (int i = 0; i < MeterGroup.Count; i++)
            {
                if (MeterGroup[i].YaoJianYn)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        string keyTemp = Key + (j+1).ToString().PadLeft(2, '0');
                        if(MeterGroup[i].MeterCostControls.ContainsKey(keyTemp))
                        {
                            Data_View.Rows[i].Cells[j+1].Value=MeterGroup[i].MeterCostControls[keyTemp].Mfk_chrData;
                        }
                    }
                    if (MeterGroup[i].MeterCostControls.ContainsKey(Key))
                    {
                        Data_View.Rows[i].Cells[9].Value = MeterGroup[i].MeterCostControls[Key].Mfk_chrJL;
                    }
                }
            }
        }
    }
}

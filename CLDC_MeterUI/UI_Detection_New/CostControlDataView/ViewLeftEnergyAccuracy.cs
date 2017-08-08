using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CostControlDataView
{
    public partial class ViewLeftEnergyAccuracy : UserControl
    {
        /// <summary>
        /// 读取保电功能数据项目ID
        /// </summary>
        private const string Key = "009";
              
        public ViewLeftEnergyAccuracy()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CostControlPlan">要求传入保电功能方案</param>
        public ViewLeftEnergyAccuracy(CLDC_DataCore.Struct.StPlan_CostControl CostControlPlan)
        {
            InitializeComponent();
            if (CostControlPlan.CostControlPrjID != Key)        //如果项目ID不是保电功能的ID则退出！！
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
            string[] arrayTemp=new string[]{"当前电价","开始剩余金额","开始总电量","结束剩余金额","结束总电量","误差","结论",
"不合格原因"};
            string[] arrayTemp1 = new string[]{"当前电价(元)","开始剩余金额(元)","开始总电量(度)","结束剩余金额(元)","结束总电量(度)","误差","结论",
"不合格原因"};
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                int _ColIndex = Data_View.Columns.Add(arrayTemp[i], arrayTemp1[i]);
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
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterTemp = MeterGroup[i];
                if (meterTemp.YaoJianYn)
                {
                    if(meterTemp.MeterCostControls.ContainsKey(Key))
                    {
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK meterFk = meterTemp.MeterCostControls[Key];
                       string[] arrayResult = meterFk.Mfk_chrData.Split('|');
                       if (arrayResult.Length == 6)
                       {
                           //当前电价|开始剩余金额|开始总电量|结束剩余金额|结束总电量|误差
                           Data_View.Rows[i].Cells["当前电价"].Value = arrayResult[0];
                           Data_View.Rows[i].Cells["开始剩余金额"].Value = arrayResult[1];
                           Data_View.Rows[i].Cells["开始总电量"].Value = arrayResult[2];
                           Data_View.Rows[i].Cells["结束剩余金额"].Value = arrayResult[3];
                           Data_View.Rows[i].Cells["结束总电量"].Value = arrayResult[4];
                           Data_View.Rows[i].Cells["误差"].Value = arrayResult[5];
                       }
                       Data_View.Rows[i].Cells["结论"].Value = meterFk.Mfk_chrJL;
                       Data_View.Rows[i].Cells["不合格原因"].Value = meterFk.AVR_DIS_REASON;
                    }
                }
            }
        }


    }
}

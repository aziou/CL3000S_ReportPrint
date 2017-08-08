using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewReadPeriod : UserControl
    {
        /// <summary>
        /// 读取费率信息项目ID
        /// </summary>
        private const string Key = "003";

        private int _FirstColIndex = 0;

        public ViewReadPeriod()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">要求传入读取费率信息方案</param>
        public ViewReadPeriod(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            InitializeComponent();
            if (DgnPlan.DgnPrjID != Key)        //如果项目ID不是读取费率信息的ID则退出！！
                return;
            string strShowValue = "运行时区|运行时段|第一套时区表数据|第二套时区表数据|第一套第1日时段表数据|第二套第1日时段表数据|标准时段表数据";
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

                if (i == _ShowValues.Length - 1)
                    Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);                
            }

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
                    if ((RowIndex + 1) % 4 == 0)
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

                for (int j = 1; j <= 7; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + j - 1);
                    if (!MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {
                        Data_View.Rows[i].Cells[_ColIndex].Value = "";                                   
                        continue;
                    }
                    if (MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                                                  

                    Data_View.Rows[i].Cells[_ColIndex].Value = MeterGroup[i].MeterDgns[_Key].Md_chrValue;             
                    
                }
            }
        }

        private int getFeiDlID(string FeiLvName)
        {
            switch (FeiLvName)
            {
                case "峰":
                    return 1;
                case "平":
                    return 2;
                case "谷":
                    return 3;
                case "尖":
                    return 4;
                default:
                    return 0;
            }
        }

    }
}

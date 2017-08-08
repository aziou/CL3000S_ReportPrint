using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewDateTimeErr : UserControl
    {
        /// <summary>
        /// 日计时误差项目ID,化整值
        /// </summary>
        private const string Key = "00201";
        /// <summary>
        /// 日计时误差项目ID，1-5次误差
        /// </summary>
        private const string Key1to5 = "00202";

        /// <summary>
        /// 日计时误差项目ID,6-10次误差
        /// </summary>
        private const string Key6to10 = "00203";

        public ViewDateTimeErr()
        {
            InitializeComponent();
        }
        public ViewDateTimeErr(int Count)
        {
            InitializeComponent();
            if (!(Count > 0))
                Count = 10;
            SetGridViewCount(Count);
        }
        /// <summary>
        /// 设置日计时数据表的列数
        /// </summary>
        /// <param name="Count"></param>
        private void SetGridViewCount(int Count)
        {
            Data_View.Columns.Clear();
            for (int i = 1; i <= Count; i++)
            {
                DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                Column.HeaderText = "误差 " + i.ToString();

                Data_View.Columns.Add(Column);
            }
            DataGridViewTextBoxColumn myColumn = new DataGridViewTextBoxColumn();
            myColumn.HeaderText = "平均值|化整值";

            Data_View.Columns.Add(myColumn);
        }
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Rows.Count != MeterGroup.Count)
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


            for (int i = 0; i < MeterGroup.Count; i++)          //循环将电能表数据集合中的误差值插入到表格中
            {
                if (!MeterGroup[i].MeterDgns.ContainsKey(Key1to5))
                    continue;
                if (MeterGroup[i].MeterDgns[Key1to5].Md_chrValue == null)
                    continue;
                string[] _Values = MeterGroup[i].MeterDgns[Key1to5].Md_chrValue.Split('|');

                if (_Values.Length != 5) continue;

                for (int j = 0; j < _Values.Length; j++)
                {
                    if (j < Data_View.Columns.Count)
                    {
                        Data_View.Rows[i].Cells[j].Value = _Values[j];
                    }
                }

                if (!MeterGroup[i].MeterDgns.ContainsKey(Key6to10))
                    continue;
                if (MeterGroup[i].MeterDgns[Key6to10].Md_chrValue == null)
                    continue;
                _Values = MeterGroup[i].MeterDgns[Key6to10].Md_chrValue.Split('|');

                if (_Values.Length != 5) continue;

                for (int j = 0; j < _Values.Length; j++)
                {
                    if (j + 5 < Data_View.Columns.Count)
                    {
                        Data_View.Rows[i].Cells[j + 5].Value = _Values[j];
                    }
                }

                if (!MeterGroup[i].MeterDgns.ContainsKey(Key))
                    continue;

                Data_View.Rows[i].Cells[Data_View.Columns.Count - 1].Value = MeterGroup[i].MeterDgns[Key].Md_chrValue;

            }



        }
    }
}

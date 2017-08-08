using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using CLDC_DataCore.Const;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewSdtq : UserControl
    {
        /// <summary>
        /// 时段投切项目ID
        /// </summary>
        private const string Key = "004";

        private const string _DlKey = "01901";           //临时电量存储ID
        private int _FirstColIndex = 0;

        public ViewSdtq()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">要求传入时段投切方案</param>
        public ViewSdtq(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            InitializeComponent();
            //if (DgnPlan.DgnPrjID != Key)        //如果项目ID不是投切误差的ID则退出！！
            //    return;

            string[] _Values = DgnPlan.PrjParm.Split('|');
            if (_Values.Length == -1 || _Values.Length == 0)
            {
                return;
            }
            int _VLength = _Values.Length;
            if (_Values[_VLength - 1].Length == 4)
            {
                _VLength = _VLength - 2;
            }
            else if (_Values[_VLength - 1].Length == 1)
            {
                _VLength = _VLength - 1;
            }
            //int _ColIndex = Data_View.Columns.Add("Data_Z", "总电量");
            //Data_View.Columns[_ColIndex].Tag = 0;
            //Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //_FirstColIndex++;

            //for (int i = 0; i < _Values.Length; i++)
            //{
            //    string _SDName = _Values[i].Substring(_Values[i].IndexOf('(') + 1).Replace(")", "");     //从字符串中解析出费率名

            //    int ColIndex = Data_View.Columns.Add("FeiLv_" + i, string.Format("{0}电量", _SDName));
            //    Data_View.Columns[ColIndex].Tag = this.getFeiDlID(_SDName);         //记录费率ID
            //    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            //    _FirstColIndex++;

            //}

            if (GlobalUnit.isAutoRead)
            {
                _Values = GlobalUnit.strSDTQ;
            }
            for (int i = 0; i < _VLength; i++)            //动态创建数据表单样式
            {
                string _SDName = _Values[i].Substring(_Values[i].IndexOf('(') + 1).Replace(")", "");     //从字符串中解析出费率名

                int ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("标准投切时间({0})", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("实际投切时间({0})", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                ColIndex = Data_View.Columns.Add("Data_" + i, string.Format("投切误差({0})", _SDName));
                Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Data_View.Columns[ColIndex].DefaultCellStyle.Font = new Font("Arial Black", 10);
                Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
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

            if ((Data_View.Columns.Count - _FirstColIndex) % 3 != 0) return;     //如果列数不是3的整数倍则退出，因为每个费率需要有3列数据填充

            for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {
                if (MeterGroup[i].YaoJianYn == false) continue;
                if (MeterGroup[i].MeterDgns.ContainsKey(_DlKey))
                {
                    string[] Arr_Dl = MeterGroup[i].MeterDgns[_DlKey].Md_chrValue.Split('|');

                    if (Arr_Dl.Length == 5)     //总|峰|平|谷|尖 

                        for (int j = 0; j < _FirstColIndex; j++)
                        {
                            Data_View.Rows[i].Cells[j].Value = Arr_Dl[(int)Data_View.Columns[j].Tag];
                        }
                }

                for (int j = _FirstColIndex; j < _FirstColIndex + (int)(Data_View.Columns.Count - _FirstColIndex) / 3; j++)
                {
                    string _Key = Key + (j - _FirstColIndex + 1).ToString("D2");
                    int _ColIndex = (int)(_FirstColIndex + (j - _FirstColIndex) * 3D + 0);
                    if (!MeterGroup[i].MeterDgns.ContainsKey(_Key))
                    {//不存在该数据
                        Data_View.Rows[i].Cells[_ColIndex].Value = "";              //标准时间
                        _ColIndex++;
                        Data_View.Rows[i].Cells[_ColIndex].Value ="";              //实际时间
                        _ColIndex++;
                        Data_View.Rows[i].Cells[_ColIndex].Value ="";              //投切误差，在投切过程中应该显示对应费率电量

                        continue;
                    }
                    if (MeterGroup[i].MeterDgns[_Key].Md_chrValue == null || MeterGroup[i].MeterDgns[_Key].Md_chrValue == string.Empty) continue;
                    string[] _Values = MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');
                    if (_Values.Length != 4) continue;          //分割出来的数据数组应该是4个元素，标准时间|实际时间|投切误差|费率

                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[0];              //标准时间
                    _ColIndex++;
                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[1];              //实际时间
                    _ColIndex++;
                    Data_View.Rows[i].Cells[_ColIndex].Value = _Values[2];              //投切误差，在投切过程中应该显示对应费率电量
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

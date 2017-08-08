using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewMaxXl : UserControl
    {


        private bool _Xlzq = false;

        /// <summary>
        /// 最大需量小编号
        /// </summary>
        private const string ChildMaxKey = "1";
        /// <summary>
        /// 需量周期误差小编号
        /// </summary>
        private const string ChildCycKey = "02";
        /// <summary>
        /// 需量周期
        /// </summary>
        /// 
        private string CycXl = "";

        private string[] Arr_Glfx = new string[] { "P+", "P-", "Q+", "Q-" };

        /// <summary>
        /// 需量的功率方向
        /// </summary>
        private string sTr_Glfx;

        /// <summary>
        /// 最大需量项目ID
        /// </summary>
        private string Key = "";

        public ViewMaxXl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">多功能方案（最大需量）</param>
        public ViewMaxXl(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            InitializeComponent();

            #region  -------------------------------处理需量表单样式-------------------------------------

            Key = DgnPlan.DgnPrjID;             //获取项目大ID

            string[] _Values = DgnPlan.PrjParm.Split('|');

            if (_Values.Length != 5)
                return;

            CycXl = _Values[0];                 //需量周期

            sTr_Glfx = _Values[4];              //功率方向

            for (int i = 0; i < sTr_Glfx.Length; i++)
            {
                if (sTr_Glfx.Substring(i, 1) == "1")
                {
                    int ColIndex = Data_View.Columns.Add("Data_" + i.ToString(), Arr_Glfx[i] + "标准需量");

                    Data_View.Columns[ColIndex].Tag = (i + 1).ToString();

                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    ColIndex = Data_View.Columns.Add("Data_" + i.ToString(), Arr_Glfx[i] + "实际需量");

                    Data_View.Columns[ColIndex].Tag = (i + 1).ToString();

                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    ColIndex = Data_View.Columns.Add("Data_" + i.ToString(), Arr_Glfx[i] + "需量误差");

                    Data_View.Columns[ColIndex].Tag = (i + 1).ToString();

                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            if (_Values[3] == "1")
            {
                _Xlzq = true;           //要做需量周期误差

                int ColIndex = Data_View.Columns.Add("Col_1", "周期误差");
                Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //float _FillWeight = 100f / Data_View.Columns.Count;

            for (int i = 0; i < Data_View.Columns.Count; i++)
            {
                Data_View.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //Data_View.Columns[i].FillWeight = _FillWeight;
                Data_View.Columns[i].ReadOnly = true;
                Data_View.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            #endregion
        }
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Columns.Count < 3 || MeterGroup.Count == 0) return;           //最大需量数据最小有3列标准需量，实际需量，误差
            try
            {
                if (Data_View.Rows.Count != MeterGroup.Count)          //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
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

                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    int _ColNum = 0;
                    if (MeterGroup[i].YaoJianYn == false) continue;
                    if (_Xlzq)
                    {
                        _ColNum = Data_View.Columns.Count - 1;
                    }
                    else
                    {
                        _ColNum = Data_View.Columns.Count;
                    }
                    for (int ColIndex = 0; ColIndex < (int)_ColNum / 3; ColIndex++)
                    {
                        string _Key = string.Format("{0}{1}{2}", Key, Data_View.Columns[ColIndex * 3].Tag, ChildMaxKey);        //最大需量值的项目ID

                        if (MeterGroup[i].MeterDgns.Count == 0) return;

                        if (MeterGroup[i].MeterDgns.ContainsKey(_Key))
                        {
                            string[] _Values = MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');
                            if (_Values.Length >= 3)
                            {
                                for (int j = 0; j < _Values.Length; j++)
                                {
                                    Data_View.Rows[i].Cells[j + ColIndex * 3 ].Value = _Values[j];
                                }
                            }
                        }
                    }

                    string _XlzqKey = string.Format("{0}{1}", Key, ChildCycKey);           //需量周期值的项目ID
                    //if (Data_View.Columns.Count != 5) continue;

                    if (!MeterGroup[i].MeterDgns.ContainsKey(_XlzqKey))
                    {
                        //Data_View.Rows[i].Cells[Data_View.Columns.Count - 1].Value = "";          //周期误差
                        continue;         //如果不存在该项目ID，则跳出
                    }
                    if (MeterGroup[i].MeterDgns[_XlzqKey].Md_chrValue == null) continue;
                    string[] _CycValues = MeterGroup[i].MeterDgns[_XlzqKey].Md_chrValue.Split('|');

                    if (_CycValues.Length != 3) continue;          //如果分割后长度不等于3，及表示数据有问题，直接退出

                    Data_View.Rows[i].Cells[Data_View.Columns.Count - 1].Value = _CycValues[1];          //周期误差

                }
            }
            catch (Exception ex)
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage(ex.Message, false);
                
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckTeShu : UserControl
    {
        /// <summary>
        /// 选中行变更的委托
        /// </summary>
        /// <param name="RowIndex">当前选中行号</param>
        public delegate void Evt_GridSelectRowIndexChanged(int RowIndex);
        /// <summary>
        /// 选中行变更后触发的事件
        /// </summary>
        public event Evt_GridSelectRowIndexChanged GridSelectRowIndexChanged;

        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;       

        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;


        public CheckTeShu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数，当前检定项目ID
        /// </summary>
        /// <param name="parent">Main窗体</param>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        /// <param name="TaiID">台体编号</param>
        /// <param name="taiType">台体类型</param>
        public CheckTeShu(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            
            this.InitializationGrid(meterGroup);

            this.RefreshGrid(meterGroup, CheckOrderID);

        }     


        /// <summary>
        /// 初始化数据菜单
        /// </summary>
        /// <param name="MeterGroup"></param>
        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_Error.Rows.Count != _Count)
            {
                Data_Error.Rows.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int _RowIndex = Data_Error.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                        Data_Error.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    else
                        Data_Error.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    Data_Error.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Error.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;

                }
            }
        }

        /// <summary>
        /// 设置创建表格列的样式
        /// </summary>
        /// <param name="Index">列下标</param>
        private void CreateColumnStyle(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, StPlan_SpecalCheck Item)
        {

            if (Data_Error.Columns.Count != Item.WcCheckNumic + 4 + 2)          //如果误差数据表格的列数大于默认列数，则需要删除大于部分 误差次数+固定列+平均值，化整值列
            {
                for (int i = Data_Error.Columns.Count - 1; i > 3; i--)
                {
                    Data_Error.Columns.RemoveAt(i);
                }


                for (int i = 0; i < Item.WcCheckNumic; i++)
                {
                    Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "误差" + (i + 1).ToString());
                }

                Data_Error.Columns.Add("Tmp_Pj", "平均值");

                Data_Error.Columns.Add("Tmp_Hz", "化整值");

            }

            float _FillWeight = 100F / (Data_Error.Columns.Count - 4);

            for (int i = 4; i < Data_Error.Columns.Count; i++)
            {
                DataGridViewColumn _Column = Data_Error.Columns[i];
                _Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _Column.FillWeight = _FillWeight;
            }
            Data_Error.Refresh();

        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            if (!(MeterGroup.CheckPlan[CheckOrderID] is StPlan_SpecalCheck)) return;

            StPlan_SpecalCheck _Item = (StPlan_SpecalCheck)MeterGroup.CheckPlan[CheckOrderID];

            #region ---------------------动态创建数据表格样式------------------------------

            this.CreateColumnStyle(MeterGroup, _Item);           //创建表格列样式

            #endregion

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Error.Rows[i];

                _Row.Cells[1].Value = _MeterInfo.ToString();            //表位号

                if (!_MeterInfo.YaoJianYn)   //如果不检
                {
                    _Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       //如果不检，并且常数或者等级都为空，则将勾选单元格设置为只读
                    {
                        _Row.Cells[0].ReadOnly = true;
                    }
                    for (int j = 2; j < _Row.Cells.Count; j++)
                    {
                        _Row.Cells[j].Value = string.Empty;
                    }
                    continue;
                }

                _Row.Cells[0].Value = true;

                _Row.Cells[2].Value = _Item.ToString();             //项目描述

                string _Key = "P_" + CheckOrderID.ToString();

                if (_MeterInfo.MeterSpecialErrs.ContainsKey(_Key))            //如果数据模型中已经存在改点的数据
                {
                    _Row.Cells[3].Value = _MeterInfo.MeterSpecialErrs[_Key].Mse_Result;
                    string[] Arr_Err = _MeterInfo.MeterSpecialErrs[_Key].Mse_Wc.Split('|');
                    if (Arr_Err.Length <= 1) continue;

                    for (int j = 0; j < Arr_Err.Length; j++)
                    {
                        if (Data_Error.Columns.Count <= j + 4) break;
                        _Row.Cells[j + 4].Value = Arr_Err[j];
                    }

                    if (_MeterInfo.MeterSpecialErrs[_Key].Mse_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //如果不合格需要修改当前行背景颜色为红色
                    {
                        _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                    //}
                        foreach (DataGridViewCell cell in _Row.Cells)
                        {
                            cell.ToolTipText = MeterGroup.MeterGroup[i].MeterSpecialErrs[_Key].AVR_DIS_REASON;
                        }
                    }
                    else
                    {
                        _Row.DefaultCellStyle.ForeColor = Color.Black;
                    //}
                        foreach (DataGridViewCell cell in _Row.Cells)
                        {
                            cell.ToolTipText = string.Empty;
                        }
                    }

                }
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    for (int j = 3; j < Data_Error.Columns.Count; j++)
                    {
                        _Row.Cells[j].Value = "";
                    }
                }
                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Red;

                }
                else
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Black;

                }
            }
        }

        /// <summary>
        /// 外部调用数据刷新，
        /// </summary>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定ID</param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_Error.Enabled = true;            
        }

        /// <summary>
        /// 表位行选中，选择是否要检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Error_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex == -1)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    try
                    {
                        this.GridSelectRowIndexChanged(e.RowIndex);
                    }
                    catch
                    { }
                }
                return;     //如果不是第一列，则退出
            }
            try
            {
                this.GridSelectRowIndexChanged(e.RowIndex);
            }
            catch { }

            if (Data_Error[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Error.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Error.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Error.EndEdit();
                }
                Data_Error.Enabled = false;
                //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBoxEx.Show(this,"没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 显示基本信息窗体相关

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Error_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(Data_Error.SelectedRows.Count > 0)
                    SetDnbInfoViewData(Data_Error.SelectedRows[0].Index);
                else
                    SetDnbInfoViewData(0);
            }
            catch
            {
                SetDnbInfoViewData(0);         //如果出现错误就自动选择第一个表位
            }
        }

        #endregion

        /// <summary>
        /// 获取或设置当前选中的行号
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                if (Data_Error.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Error.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Error.IsHandleCreated)
                {
                    if (value >= 0 && Data_Error.Rows.Count > value)
                    {
                        Data_Error.Rows[value].Selected = true;
                        Data_Error.CurrentCell = Data_Error.Rows[value].Cells[1];
                    }
                }
            }
        }
    }
}

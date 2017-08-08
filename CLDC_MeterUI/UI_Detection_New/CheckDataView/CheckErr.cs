using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using System.Threading;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    /// <summary>
    /// 基本误差测试
    /// </summary>
    public partial class CheckErr : UserControl
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


        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;


        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;

       

        public CheckErr()
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
        public CheckErr(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            this.DoubleBuffered = true; //开启双缓存
            this._TaiID = TaiID;
            this._TaiType = taiType;

            
            this.InitializationGrid(meterGroup);

            this.RefreshGrid(meterGroup, CheckOrderID);

        }        


        /// <summary>
        /// 初始化数据菜单
        /// </summary>
        /// <param name="MeterGroup"></param>
        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_Error.Rows.Count != _Count)           //初始化右部数据表单
            {
                Data_Error.Rows.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int index = Data_Error.Rows.Add();
                    if ((index + 1) % 2 == 0)
                        Data_Error.Rows[index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    else
                        Data_Error.Rows[index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    Data_Error.Rows[index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Error.Rows[index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }

            Data_Error.Refresh();
        }

        /// <summary>
        /// 刷新数据列表
        /// </summary>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            _DnbGroup = MeterGroup;

            int FirstYJMeter = Main.GetFirstYaoJianMeterIndex(MeterGroup);
            if (CheckOrderID >= MeterGroup.CheckPlan.Count
                || !(MeterGroup.CheckPlan[CheckOrderID] is StPlan_WcPoint))
            {
                return;
            }

            StPlan_WcPoint _Item = (StPlan_WcPoint)MeterGroup.CheckPlan[CheckOrderID];

            #region ---------------------动态创建数据表格样式-------------------------------
            if (_Item.ToString().IndexOf("FHC") != -1)
            {
                //带分合差列样式
                if (Data_Error.Columns.Count != MeterGroup.WcCheckNumic + 7)          //如果误差数据表格的列数大于默认列数，则需要删除大于部分
                {
                    for (int i = Data_Error.Columns.Count - 1; i >= 4; i--)
                    {
                        Data_Error.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < MeterGroup.WcCheckNumic; i++)
                    {
                        Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "误差" + (i + 1).ToString());
                    }
                    Data_Error.Columns.Add("Tmp_Pj", "平均值");

                    Data_Error.Columns.Add("Tmp_Hz", "化整值");

                    Data_Error.Columns.Add("Tmp_FhcRand", "差值上下限");

                    Data_Error.Columns.Add("Tmp_Fhc", "差值");

                    this.CreateColumnStyle();           //创建表格列样式
                }
            }
            else if (_Item.ToString().IndexOf("标准偏差") == -1)       //如果不是标准偏差
            {   //普通误差样式
                if (Data_Error.Columns.Count != MeterGroup.WcCheckNumic + 6)          //如果误差数据表格的列数大于默认列数，则需要删除大于部分
                {
                    for (int i = Data_Error.Columns.Count - 1; i >= 4; i--)
                    {
                        Data_Error.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < MeterGroup.WcCheckNumic; i++)
                    {
                        Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "误差" + (i + 1).ToString());
                    }
                    Data_Error.Columns.Add("Tmp_Pj", "平均值");

                    Data_Error.Columns.Add("Tmp_Hz", "化整值");

                    this.CreateColumnStyle();           //创建表格列样式
                }
            }
            else
            {   //偏差表格样式
                if (Data_Error.Columns.Count != MeterGroup.PcCheckNumic + 6)          //如果误差数据表格的列数大于默认列数，则需要删除大于部分
                {
                    for (int i = Data_Error.Columns.Count - 1; i >= 4; i--)
                    {
                        Data_Error.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < MeterGroup.PcCheckNumic; i++)
                    {
                        Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "误差" + (i + 1).ToString());
                    }

                    Data_Error.Columns.Add("Tmp_Pj", "偏差值");

                    Data_Error.Columns.Add("Tmp_Hz", "化整值");

                    this.CreateColumnStyle();           //创建表格列样式
                }

            }

            #endregion

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_Error.Rows[i];

                Row.Cells[1].Value = _MeterInfo.ToString();      //插入表位号

                if (!_MeterInfo.YaoJianYn)        //如果不检
                {

                    Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       //如果不检，并且常数或者等级都为空，则将勾选单元格设置为只读
                    {
                        Row.Cells[0].ReadOnly = true;
                    }
                    for (int j = 2; j < Row.Cells.Count; j++)
                    {
                        Row.Cells[j].Value = string.Empty;
                    }
                    continue;
                }

                //if (_MeterInfo.Mb_chrResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                //{
                //    Row.HeaderCell.Style.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                //}
                //else
                //{
                //    Row.HeaderCell.Style.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                //}

                Row.Cells[0].Value = true;

                string _Key = _Item.PrjID;

                if (_MeterInfo.MeterErrors.ContainsKey(_Key))          //如果数据模型中已经存在该点的数据
                {
                    #region
                    try
                    {
                        if (_MeterInfo.MeterErrors[_Key].Me_chrWcJl == CLDC_DataCore.Const.Variable.CTG_BuHeGe)            //如果不合格修改当前行背景颜色为红色
                        {
                            Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = _MeterInfo.MeterErrors[_Key].AVR_DIS_REASON;
                            }
                        }
                        else
                        {
                            Row.DefaultCellStyle.ForeColor = Color.Black;
                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = string.Empty;
                            }
                        }
                    }
                    catch { }
                    try
                    {
                        string[] Arr_WcLimit = _MeterInfo.MeterErrors[_Key].Me_WcLimit.Split('|');      //分解误差限
                        if (Arr_WcLimit.Length == 2)
                        {
                            Row.Cells[2].Value = string.Format("{0}  {1}", Arr_WcLimit[0], Arr_WcLimit[1]);       //项目误差限  
                        }
                        else
                        {
                            Row.Cells[2].Value = "";
                        }
                    }
                    catch { }
                    try
                    {
                        Row.Cells[3].Value = _MeterInfo.MeterErrors[_Key].Me_chrWcJl;
                    }
                    catch { }
                    try
                    {
                        string[] Arr_Err = _MeterInfo.MeterErrors[_Key].Me_chrWcMore.Split('|');           //分解误差

                        if (Arr_Err.Length == -1) continue;
                        for (int j = 0; j < Arr_Err.Length; j++)
                        {
                            if (Data_Error.Columns.Count <= j + 4) break;           //如果列数小于当前误差数，则自动退出
                            Row.Cells[j + 4].Value = Arr_Err[j];
                        }
                    }
                    catch { }
                    try
                    {
                        if (_Item.Dif_Err_Flag == 1)
                        {
                            Row.Cells[8].Value = _MeterInfo.MeterErrors[_Key].AVR_UPPER_LIMIT + " " + _MeterInfo.MeterErrors[_Key].AVR_LOWER_LIMIT;
                            Row.Cells[9].Value = _MeterInfo.MeterErrors[_Key].AVR_DIF_ERR_AVG;//AVR_DIF_ERR_AVG
                        }
                    }
                    catch { }
                    #endregion
                }
                else
                {
                    //Row.DefaultCellStyle.ForeColor = Color.Black;
                    for (int j = 2; j < Data_Error.Columns.Count; j++)
                        Row.Cells[j].Value = "";
                }

                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    Row.DefaultCellStyle.ForeColor = Color.Red;
                    
                }
                else
                {
                    Row.DefaultCellStyle.ForeColor = Color.Black;
                    
                }
            }
        }

        /// <summary>
        /// 设置创建表格列的样式
        /// </summary>
        /// <param name="Index">列下标</param>
        private void CreateColumnStyle()
        {

            if (Data_Error.Columns.Count <= 3) return;

            float _FillWeight = 100F / (Data_Error.Columns.Count - 3);

            for (int i = 3; i < Data_Error.Columns.Count; i++)
            {
                DataGridViewColumn _Column = Data_Error.Columns[i];
                _Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _Column.FillWeight = _FillWeight;
                _Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Data_Error.Refresh();

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
                Data_Error.Enabled = true;
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
                SetDnbInfoViewData(Data_Error.SelectedRows[0].Index);
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

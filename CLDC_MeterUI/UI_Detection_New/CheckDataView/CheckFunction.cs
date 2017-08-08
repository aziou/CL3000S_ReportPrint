using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckFunction : UserControl
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

        private  CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;


        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;

        public CheckFunction()
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
        public CheckFunction(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Dgn.Rows.Count != _Count)
            {
                Data_Dgn.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = Data_Dgn.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_Dgn.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Dgn.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Dgn.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Dgn.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_Dgn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_Dgn.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_Function _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Function)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_Function)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Dgn.Rows[i];
                //表位号
                _Row.Cells[1].Value = _MeterInfo.ToString();            //插入表位号

                if (!_MeterInfo.YaoJianYn)           //如果不检
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

                _Row.Cells[2].Value = _Item.ToString();

                if (Data_Dgn.Tag == null) return;

                if (_MeterInfo.MeterFunctions == null)
                    _MeterInfo.MeterFunctions = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFunction>();
                if (_MeterInfo.MeterShows == null)
                {
                    _MeterInfo.MeterShows = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterShow>();
                }
                if (_MeterInfo.MeterFunctions.ContainsKey(Data_Dgn.Tag.ToString()))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterFunctions[Data_Dgn.Tag.ToString()].Mf_chrValue;                    
                }
                else if (_MeterInfo.MeterShows.ContainsKey(Data_Dgn.Tag.ToString()))
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterShows[Data_Dgn.Tag.ToString()].Msh_chrJL;
                }
                else
                {
                    _Row.Cells[4].Value = "";
                }
                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Red;
                    if (_MeterInfo.MeterFunctions.ContainsKey(Data_Dgn.Tag.ToString()))
                    {
                        foreach (DataGridViewCell cell in _Row.Cells)
                        {
                            cell.ToolTipText = MeterGroup.MeterGroup[i].MeterFunctions[Data_Dgn.Tag.ToString()].AVR_DIS_REASON;
                        }
                    }
                    else if (_MeterInfo.MeterShows.ContainsKey(Data_Dgn.Tag.ToString()))
                    {
                        foreach (DataGridViewCell cell in _Row.Cells)
                        {
                            cell.ToolTipText = MeterGroup.MeterGroup[i].MeterShows[Data_Dgn.Tag.ToString()].AVR_DIS_REASON;
                        }
                    }
                }
                else
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Black;

                    foreach (DataGridViewCell cell in _Row.Cells)
                    {
                        cell.ToolTipText = string.Empty;
                    }
                }
            }

            #region -----------------------------------------数据页刷新-------------------------------------------
            if (Tab_Function.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_Function.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewComputation)         //如果是计量功能
            {
                ((CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewComputation)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewTimingData)            //如果是计时功能
            {
                ((CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewTimingData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewShowFunction)            //如果是显示功能
            {
                ((CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewShowFunction)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewRatePeriod)            //如果是费率时段功能
            {
                ((CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewRatePeriod)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewMaxDemand)            //如果是最大需量功能
            {
                ((CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewMaxDemand)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewPulseOutPut)            //如果该控件是脉冲输出功能
            {
                ((CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewPulseOutPut)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            
            #endregion
        }

        /// <summary>
        /// 刷新数据事件
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Function)) return;

            CLDC_DataCore.Struct.StPlan_Function _Item = (CLDC_DataCore.Struct.StPlan_Function)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_Function.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_Dgn.Tag != null && Data_Dgn.Tag.ToString() == _Item.FunctionPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_Function.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_Function.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Dgn.Tag = _Item.FunctionPrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.FunctionPrjID)
                {
                    case "001":
                        {
                            Tab_Function.TabPages.Add("计量功能数据");
                            CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewComputation _Computate = new CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewComputation();
                            Tab_Function.TabPages[1].Controls.Add(_Computate);
                            _Computate.Dock = DockStyle.Fill;
                            _Computate.Margin = new Padding(0);
                            break;
                        }
                    case "002":
                        {
                            Tab_Function.TabPages.Add("计时功能数据");
                            CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewTimingData _Timing = new CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewTimingData();
                            Tab_Function.TabPages[1].Controls.Add(_Timing);
                            _Timing.Dock = DockStyle.Fill;
                            _Timing.Margin = new Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_Function.TabPages.Add("显示功能数据");
                            CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewShowFunction _show = new CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewShowFunction();
                            Tab_Function.TabPages[1].Controls.Add(_show);
                            _show.Dock = DockStyle.Fill;
                            _show.Margin = new Padding(0);
                            break;
                        }
                    case "004":
                        {
                            Tab_Function.TabPages.Add("费率时段功能数据");
                            CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewRatePeriod _ratePeriod = new CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewRatePeriod();
                            Tab_Function.TabPages[1].Controls.Add(_ratePeriod);
                            _ratePeriod.Dock = DockStyle.Fill;
                            _ratePeriod.Margin = new Padding(0);
                            break;
                        }
                    case "005":
                        {
                            Tab_Function.TabPages.Add("脉冲输出功能数据");
                            CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewPulseOutPut _pulse = new CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewPulseOutPut();
                            Tab_Function.TabPages[1].Controls.Add(_pulse);
                            _pulse.Dock = DockStyle.Fill;
                            _pulse.Margin = new Padding(0);
                            break;
                        }   
                    case "006":
                        {
                            Tab_Function.TabPages.Add("最大需量功能数据");
                            CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewMaxDemand _MaxDemand = new CLDC_MeterUI.UI_Detection_New.FunctionDataView.ViewMaxDemand();
                            Tab_Function.TabPages[1].Controls.Add(_MaxDemand);
                            _MaxDemand.Dock = DockStyle.Fill;
                            _MaxDemand.Margin = new Padding(0);
                            break;
                        }
                                     
                }
            }
            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_Dgn.Enabled = true;
        }

        private void Data_Dgn_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_Dgn[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Dgn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Dgn.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Dgn.EndEdit();
                }
                Data_Dgn.Enabled = false;
                //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBox.Show("没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 显示基本信息窗体相关

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Dgn_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Dgn.SelectedRows[0].Index);
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
                if (Data_Dgn.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Dgn.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Dgn.IsHandleCreated)
                {
                    if (value >= 0 && Data_Dgn.Rows.Count > value)
                    {
                        Data_Dgn.Rows[value].Selected = true;
                        Data_Dgn.CurrentCell = Data_Dgn.Rows[value].Cells[1];
                    }
                }
            }
        }

        /// <summary>
        /// 单击了行以后切换 ui_Popup_MeterIndex 以切换误差详细数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Dgn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定 && e.ColumnIndex == 0)
            {
                if (ParentMain.Evt_OnYaoJianChanged != null)
                {
                    bool Yn;
                    if ((bool)Data_Dgn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        Yn = false;
                        Data_Dgn.EndEdit();
                    }
                    else
                    {
                        Yn = true;
                        Data_Dgn.EndEdit();
                    }
                    Data_Dgn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Yn;
                    _DnbGroup.MeterGroup[e.RowIndex].YaoJianYn = Yn;
                    Data_Dgn.Enabled = false;
                    //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                    ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    MessageBox.Show("没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Data_Dgn.SelectedRows.Count < 1) return;
            
        }

        private void Data_Dgn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Data_Dgn_CellClick(Data_Dgn, e);            
        }

    }
}

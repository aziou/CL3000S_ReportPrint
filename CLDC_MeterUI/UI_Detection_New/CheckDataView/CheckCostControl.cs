using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckCostControl : UserControl
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

        public CheckCostControl()
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
        public CheckCostControl(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Cost.Rows.Count != _Count)
            {
                Data_Cost.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = Data_Cost.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_Cost.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Cost.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Cost.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Cost.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_Cost.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_Cost.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_CostControl _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_CostControl)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_CostControl)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Cost.Rows[i];
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

                if (Data_Cost.Tag == null) return;

                if (_MeterInfo.MeterCostControls == null)
                    _MeterInfo.MeterCostControls = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK>();
                if (_MeterInfo.MeterCostControls.ContainsKey(Data_Cost.Tag.ToString()))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    _Row.Cells[3].Value = "100%";
                    _Row.Cells[4].Value = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].Mfk_chrJL;
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.检定 ||
                        MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.单步检定)
                    {
                        switch (_Item.CostControlPrjID)
                        {                            
                            case "001":
                                _Row.Cells[3].Value = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].Mfk_chrData;
                                break;
                            default:
                                _Row.Cells[3].Value = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].Mfk_chrData;
                                break;
                        }
                    }
                    if (_Row.Cells[4].Value != null)
                    {
                        if (_Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //不合格修改当前行背景颜色
                        {
                            _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            foreach (DataGridViewCell cell in _Row.Cells)
                            {
                                cell.ToolTipText = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].AVR_DIS_REASON;
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
                }
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    _Row.Cells[4].Value = "";
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

            #region -----------------------------------------数据页刷新-------------------------------------------
            if (Tab_CostControl.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_CostControl.TabPages[1].Controls[0];

            
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn)            //ESAM数据回抄数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData)            //远程控制数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData)            //符合开关数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip)            //远程控制直接合闸
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData)                 //报警功能
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData)                 //密钥更新
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData)                 //密钥恢复
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction )                 //密钥更新
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)            //如果是费率电价检查表
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)            //如果是费率电价检查表
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy)                 //剩余电量递减准确度
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent)                 //预置内容设置 @C_B
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode)                
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            //
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode)
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            //
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser)
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData)
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData)_Control).SetData(MeterGroup.MeterGroup);
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
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_CostControl)) return;

            CLDC_DataCore.Struct.StPlan_CostControl _Item = (CLDC_DataCore.Struct.StPlan_CostControl)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_CostControl.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_Cost.Tag != null && Data_Cost.Tag.ToString() == _Item.CostControlPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_CostControl.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_CostControl.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Cost.Tag = _Item.CostControlPrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.CostControlPrjID)
                {
                    case "002":
                        {
                            Tab_CostControl.TabPages.Add("远程控制数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_CostControl.TabPages.Add("报警功能数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "007":
                        {
                            Tab_CostControl.TabPages.Add("ESAM数据回抄数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "009":
                        {
                            Tab_CostControl.TabPages.Add("剩余电量递减准确度");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "011":
                        {
                            Tab_CostControl.TabPages.Add("负荷开关数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "013":
                        {
                            Tab_CostControl.TabPages.Add("密钥更新数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "014":
                        {
                            Tab_CostControl.TabPages.Add("密钥恢复数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "015":
                        {
                            Tab_CostControl.TabPages.Add("控制功能数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "016":
                        {
                            Tab_CostControl.TabPages.Add("阶梯电价检查");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff stepPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(stepPrice);
                            stepPrice.Dock = DockStyle.Fill;
                            stepPrice.Margin = new System.Windows.Forms.Padding(0);
                        }
                        break;
                    case "017":
                        {
                            Tab_CostControl.TabPages.Add("费率电价检查");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime ratesPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(ratesPrice);
                            ratesPrice.Dock = DockStyle.Fill;
                            ratesPrice.Margin = new System.Windows.Forms.Padding(0);
                        }
                        break;
                    case "018":
                        {
                            Tab_CostControl.TabPages.Add("远程控制直接合闸数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "022": //@C_B
                        {
                            Tab_CostControl.TabPages.Add("预置内容数据");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "023":
                        {
                            Tab_CostControl.TabPages.Add("本地模式切换远程模式");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                    case "024":
                        {
                            Tab_CostControl.TabPages.Add("远程模式切换本地模式");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                    case "025":
                        {
                            Tab_CostControl.TabPages.Add("用户卡开户");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                    case "026":
                        {
                            Tab_CostControl.TabPages.Add("透支功能");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                        

                }
            }

            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_Cost.Enabled = true;
        }

        private void Data_Cost_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_Cost[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Cost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Cost.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Cost.EndEdit();
                }
                Data_Cost.Enabled = false;
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

        private void Data_Cost_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Cost.SelectedRows[0].Index);
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
                if (Data_Cost.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Cost.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Cost.IsHandleCreated)
                {
                    if (value >= 0 && Data_Cost.Rows.Count > value)
                    {
                        Data_Cost.Rows[value].Selected = true;
                        Data_Cost.CurrentCell = Data_Cost.Rows[value].Cells[1];
                    }
                }
            }
        }

        /// <summary>
        /// 单击了行以后切换 ui_Popup_MeterIndex 以切换误差详细数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Cost_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定 && e.ColumnIndex == 0)
            {
                if (ParentMain.Evt_OnYaoJianChanged != null)
                {
                    bool Yn;
                    if ((bool)Data_Cost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        Yn = false;
                        Data_Cost.EndEdit();
                    }
                    else
                    {
                        Yn = true;
                        Data_Cost.EndEdit();
                    }
                    Data_Cost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Yn;
                    _DnbGroup.MeterGroup[e.RowIndex].YaoJianYn = Yn;
                    Data_Cost.Enabled = false;
                    //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                    ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    MessageBox.Show("没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Data_Cost.SelectedRows.Count < 1) return;
            
        }

        private void Data_Cost_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Data_Cost_CellClick(Data_Cost, e);
            
        }

    }
}

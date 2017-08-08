using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckEventLog : UserControl
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

        public CheckEventLog()
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
        public CheckEventLog(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_EventLog.Rows.Count != _Count)
            {
                Data_EventLog.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = Data_EventLog.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_EventLog.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_EventLog.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_EventLog.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_EventLog.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_EventLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_EventLog.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_EventLog _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_EventLog)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_EventLog)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_EventLog.Rows[i];
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

                if (Data_EventLog.Tag == null) return;

                if (_MeterInfo.MeterSjJLgns == null)
                    _MeterInfo.MeterSjJLgns = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn>();

                if (_MeterInfo.MeterSjJLgns.ContainsKey(Data_EventLog.Tag.ToString()))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    _Row.Cells[3].Value = "100%";
                    _Row.Cells[4].Value = _MeterInfo.MeterSjJLgns[Data_EventLog.Tag.ToString()].ItemConc;
                    
                    //if (_Row.Cells[4].Value != null)
                    //{
                    //    if (_Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //不合格修改当前行背景颜色
                    //    {
                    //        _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                    //    }
                    //    else
                    //    {
                    //        _Row.DefaultCellStyle.ForeColor = Color.Black;
                    //    }
                    //}
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
            if (Tab_EventLog.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_EventLog.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage)         //失压记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage)         //全失压记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage)         //过压记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage)         //欠压记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent)            //失流记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent)         //过流记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent)               //断流记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad)            //过载记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase)               //断相记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }            
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump)            //掉电记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme)            //编程记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover)          //开盖记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy)          //清零记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU)            //电压逆相序记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI)            //电流逆相序记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover)            //开端钮盖记录
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU)            //电压不平衡
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI)            //电流不平衡
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand)            //清需量
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent)            //事件清零
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime)            //校时
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend)          //潮流反向
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower)          //功率反向
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand)          //需量超限
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF)          //功率因数超下限
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485)            //过流485
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            //if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_ZB)            //过流载波
            //{
            //    ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_ZB)_Control).SetData(MeterGroup.MeterGroup);
            //    return;
            //}
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
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_EventLog)) return;

            CLDC_DataCore.Struct.StPlan_EventLog _Item = (CLDC_DataCore.Struct.StPlan_EventLog)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_EventLog.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_EventLog.Tag != null && Data_EventLog.Tag.ToString() == _Item.EventLogPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_EventLog.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_EventLog.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_EventLog.Tag = _Item.EventLogPrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.EventLogPrjID)
                {
                    case "001":
                        {
                            Tab_EventLog.TabPages.Add("失压事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "002":
                        {
                            Tab_EventLog.TabPages.Add("过压事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_EventLog.TabPages.Add("欠压事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "004":
                        {
                            Tab_EventLog.TabPages.Add("失流事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "005":
                        {
                            Tab_EventLog.TabPages.Add("断流事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "006":
                        {
                            Tab_EventLog.TabPages.Add("过流事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    //case "006":
                    //    {
                    //        Tab_EventLog.TabPages.Add("过流(485)事件记录数据");
                    //        CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485 _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485();
                    //        Tab_EventLog.TabPages[1].Controls.Add(_View);
                    //        _View.Dock = DockStyle.Fill;
                    //        _View.Margin = new Padding(0);
                    //        break;
                    //    }
                    case "007":
                        {
                            Tab_EventLog.TabPages.Add("过载事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "008":
                        {
                            Tab_EventLog.TabPages.Add("断相事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }                   
                                   
                    case "009":
                        {
                            Tab_EventLog.TabPages.Add("掉电事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "010":
                        {
                            Tab_EventLog.TabPages.Add("全失压事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "011":
                        {
                            Tab_EventLog.TabPages.Add("电压不平衡事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "012":
                        {
                            Tab_EventLog.TabPages.Add("电流不平衡事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "013":
                        {
                            Tab_EventLog.TabPages.Add("电压逆相序事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "014":
                        {
                            Tab_EventLog.TabPages.Add("电流逆相序事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "015":
                        {
                            Tab_EventLog.TabPages.Add("开表盖事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "016":
                        {
                            Tab_EventLog.TabPages.Add("开端钮盒事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "017":
                        {
                            Tab_EventLog.TabPages.Add("编程事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "018":
                        {
                            Tab_EventLog.TabPages.Add("校时事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "019":
                        {
                            Tab_EventLog.TabPages.Add("需量清零事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "020":
                        {
                            Tab_EventLog.TabPages.Add("事件清零事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "021":
                        {
                            Tab_EventLog.TabPages.Add("电表清零事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "022":
                        {
                            Tab_EventLog.TabPages.Add("潮流反向事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "023":
                        {
                            Tab_EventLog.TabPages.Add("功率反向事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "024":
                        {
                            Tab_EventLog.TabPages.Add("需量超限事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "025":
                        {
                            Tab_EventLog.TabPages.Add("功率因数超下限事件记录数据");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }

                }
            }


            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_EventLog.Enabled = true;
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

            if (Data_EventLog[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_EventLog.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_EventLog.EndEdit();
                }
                Data_EventLog.Enabled = false;
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
                SetDnbInfoViewData(Data_EventLog.SelectedRows[0].Index);
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
                if (Data_EventLog.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_EventLog.SelectedRows[0].Index;
            }
            set
            {
                if (Data_EventLog.IsHandleCreated)
                {
                    if (value >= 0 && Data_EventLog.Rows.Count > value)
                    {
                        Data_EventLog.Rows[value].Selected = true;
                        Data_EventLog.CurrentCell = Data_EventLog.Rows[value].Cells[1];
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
                    if ((bool)Data_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        Yn = false;
                        Data_EventLog.EndEdit();
                    }
                    else
                    {
                        Yn = true;
                        Data_EventLog.EndEdit();
                    }
                    Data_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Yn;
                    _DnbGroup.MeterGroup[e.RowIndex].YaoJianYn = Yn;
                    Data_EventLog.Enabled = false;
                    //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                    ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    MessageBox.Show("没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Data_EventLog.SelectedRows.Count < 1) return;
            
        }

        private void Data_Dgn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Data_Dgn_CellClick(Data_EventLog, e);
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckFreeze : UserControl
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

        private const string Key_Month = "00101";                     //月冻结电量在集合中存储的关键字
        private const string Key_Day = "00102";                       //日冻结电量在集合中存储的关键字
        private const string Key_Hour = "00103";                      //小时冻结电量在集合中存储的关键字

        /// <summary>
        /// 构造函数，当前检定项目ID
        /// </summary>
        /// <param name="parent">Main窗体</param>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        /// <param name="TaiID">台体编号</param>
        /// <param name="taiType">台体类型</param>
        public CheckFreeze(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_Freeze.Rows.Count != _Count)
            {
                Data_Freeze.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = Data_Freeze.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_Freeze.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Freeze.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Freeze.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Freeze.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_Freeze.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_Freeze.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            string str_FreezeType = string.Empty;
            CLDC_DataCore.Struct.StPlan_Freeze _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Freeze)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_Freeze)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Freeze.Rows[i];
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

                if (Data_Freeze.Tag == null) return;

                if (_MeterInfo.MeterFreezes == null)
                    _MeterInfo.MeterFreezes = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze>();

                if (_MeterInfo.MeterFreezes.ContainsKey(Data_Freeze.Tag.ToString()))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterFreezes[Data_Freeze.Tag.ToString()].Md_chrValue;
                    if (_MeterInfo.MeterFreezes.ContainsKey(Key_Month))
                    {
                        str_FreezeType = "正在进行月冻结......";
                    }
                    if (_MeterInfo.MeterFreezes.ContainsKey(Key_Day))
                    {
                        str_FreezeType = "正在进行日冻结......";
                    }
                    if (_MeterInfo.MeterFreezes.ContainsKey(Key_Hour))
                    {
                        str_FreezeType = "正在进行小时冻结......";
                    }

                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.检定 ||
                        MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.单步检定)
                    {
                        switch (_Item.FreezePrjID)
                        {
                            case "001":
                                _Row.Cells[3].Value = str_FreezeType;
                                break;
                            case "002":
                                _Row.Cells[3].Value = "正在进行瞬时冻结......";
                                break;
                            case "003":
                                _Row.Cells[3].Value = "正在进行日冻结......";
                                break;
                            case "004":
                                _Row.Cells[3].Value = "正在进行约定冻结......";
                                break;
                            case "005":
                                _Row.Cells[3].Value = "正在进行整点冻结......";
                                break;
                            default:
                                _Row.Cells[3].Value = "";
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
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterFreezes[Data_Freeze.Tag.ToString()].AVR_DIS_REASON;
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
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.检定 ||
                        MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.单步检定)
                    {
                        switch (_Item.FreezePrjID)
                        {
                            case "001":
                                _Row.Cells[3].Value = str_FreezeType;
                                break;
                            case "002":
                                _Row.Cells[3].Value = "正在进行瞬时冻结......";
                                break;
                            case "003":
                                _Row.Cells[3].Value = "正在进行日冻结......";
                                break;
                            case "004":
                                _Row.Cells[3].Value = "正在进行约定冻结......";
                                break;
                            case "005":
                                _Row.Cells[3].Value = "正在进行整点冻结......";
                                break;
                            default:
                                _Row.Cells[3].Value = "";
                                break;
                        }
                    }
                    else
                    {
                        _Row.Cells[3].Value = "";
                    }
                    _Row.DefaultCellStyle.ForeColor = Color.Black;
                    _Row.Cells[4].Value = "";
                }

                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    _Row.DefaultCellStyle.ForeColor = Color.Red;
                else
                    _Row.DefaultCellStyle.ForeColor = Color.Black;
            }

            #region -----------------------------------------数据页刷新-------------------------------------------
            if (Tab_Freeze.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_Freeze.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataTiming)         //如果是定时冻结
            {
                ((CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataTiming)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            if (_Control is CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataInstant)         //如果是瞬时冻结
            {
                ((CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataInstant)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            if (_Control is CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataDay)         //如果是日冻结
            {
                ((CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataDay)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            if (_Control is CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataAppoint)         //如果是约定冻结
            {
                ((CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataAppoint)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            if (_Control is CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataWholePoint)         //如果是整点冻结
            {
                ((CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataWholePoint)_Control).SetData(MeterGroup.MeterGroup);
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
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Freeze)) return;

            CLDC_DataCore.Struct.StPlan_Freeze _Item = (CLDC_DataCore.Struct.StPlan_Freeze)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_Freeze.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_Freeze.Tag != null && Data_Freeze.Tag.ToString() == _Item.FreezePrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_Freeze.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_Freeze.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Freeze.Tag = _Item.FreezePrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.FreezePrjID)
                {
                    case "001":
                        {
                            Tab_Freeze.TabPages.Add("定时冻结数据");
                            CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataTiming _FreezeDL = new CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataTiming();
                            Tab_Freeze.TabPages[1].Controls.Add(_FreezeDL);
                            _FreezeDL.Dock = DockStyle.Fill;
                            _FreezeDL.Margin = new Padding(0);
                            break;
                        }
                    case "002":
                        {
                            Tab_Freeze.TabPages.Add("瞬时冻结数据");
                            CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataInstant _FreezeDL = new CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataInstant();
                            Tab_Freeze.TabPages[1].Controls.Add(_FreezeDL);
                            _FreezeDL.Dock = DockStyle.Fill;
                            _FreezeDL.Margin = new Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_Freeze.TabPages.Add("日冻结数据");
                            CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataDay _FreezeDL = new CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataDay();
                            Tab_Freeze.TabPages[1].Controls.Add(_FreezeDL);
                            _FreezeDL.Dock = DockStyle.Fill;
                            _FreezeDL.Margin = new Padding(0);
                            break;
                        }
                    case "004":
                        {
                            Tab_Freeze.TabPages.Add("约定冻结数据");
                            CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataAppoint _FreezeDL = new CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataAppoint();
                            Tab_Freeze.TabPages[1].Controls.Add(_FreezeDL);
                            _FreezeDL.Dock = DockStyle.Fill;
                            _FreezeDL.Margin = new Padding(0);
                            break;
                        }
                    case "005":
                        {
                            Tab_Freeze.TabPages.Add("整点冻结数据");
                            CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataWholePoint _FreezeDL = new CLDC_MeterUI.UI_Detection_New.FreezeDataView.ViewDataWholePoint();
                            Tab_Freeze.TabPages[1].Controls.Add(_FreezeDL);
                            _FreezeDL.Dock = DockStyle.Fill;
                            _FreezeDL.Margin = new Padding(0);
                            break;
                        }
                }
            }

            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_Freeze.Enabled = true;
        }

        private void Data_Freeze_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_Freeze[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Freeze.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Freeze.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Freeze.EndEdit();
                }
                Data_Freeze.Enabled = false;
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

        private void Data_Freeze_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Freeze.SelectedRows[0].Index);
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
                if (Data_Freeze.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Freeze.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Freeze.IsHandleCreated)
                {
                    if (value >= 0 && Data_Freeze.Rows.Count > value)
                    {
                        Data_Freeze.Rows[value].Selected = true;
                        Data_Freeze.CurrentCell = Data_Freeze.Rows[value].Cells[1];
                    }
                }
            }
        }
    }
}

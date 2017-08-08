using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckPrePareTest : UserControl
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

        public CheckPrePareTest()
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
        public CheckPrePareTest(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Prepare.Rows.Count != _Count)
            {
                Data_Prepare.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = Data_Prepare.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_Prepare.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Prepare.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Prepare.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Prepare.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_Prepare.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_Prepare.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_Dgn _Item = new CLDC_DataCore.Struct.StPlan_Dgn();
            CLDC_DataCore.Struct.StPlan_PrePareTest item;
            string PlanName = string.Empty;
            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_PrePareTest)
            {
                item = (CLDC_DataCore.Struct.StPlan_PrePareTest)MeterGroup.CheckPlan[CheckOrderID];
                PlanName = item.PrePrjName;
                if (PlanName.Contains("接线调试"))
                {

                }
                else
                {
            
                    _Item.OutPramerter = item.OutPramerter;
                    _Item.PrjParm = item.PrjParm;
                    _Item.DgnPrjID = item.PrePrjID;
                    _Item.DgnPrjName = item.PrePrjName;
                }
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Prepare.Rows[i];
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

                _Row.Cells[2].Value = "预先调试：" + PlanName;
                if (Data_Prepare.Tag == null) return;

                if (PlanName== "通信测试")
                {
                    _Row.Cells[3].Value = _MeterInfo.Mb_chrAddr;
                }
                else if (PlanName == "GPS对时")
                {
                    string itemKey = string.Empty;
                    itemKey = ((int)CLDC_Comm.Enum.Cus_DgnItem.GPS对时).ToString().PadLeft(3, '0');
                    if (_MeterInfo.MeterDgns.ContainsKey(itemKey))
                        _Row.Cells[3].Value = _MeterInfo.MeterDgns[itemKey].AVR_CONCLUSION;
                }
                else if (PlanName.Contains("费率时段检查"))
                {
                    string itemKey = item.PrePrjID + "04";

                    if (_MeterInfo.MeterDgns.ContainsKey(itemKey))
                        _Row.Cells[3].Value = _MeterInfo.MeterDgns[itemKey].Md_chrValue;
                }
                else if (PlanName.Contains("接线调试"))
                {
                    string itemKey = item.PrePrjID;

                    if (_MeterInfo.MeterErrors.ContainsKey(itemKey))
                        _Row.Cells[3].Value = _MeterInfo.MeterErrors[itemKey].Me_chrWcMore;
                }
                else
                {
                    _Row.Cells[3].Value = "--";
                }
                if (_MeterInfo.MeterDgns.ContainsKey(Data_Prepare.Tag.ToString()))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterDgns[Data_Prepare.Tag.ToString()].Md_chrValue;
                    if ((MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.检定) == CLDC_Comm.Enum.Cus_CheckStaute.检定 ||
                        (MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.单步检定) == CLDC_Comm.Enum.Cus_CheckStaute.单步检定)
                    {
                        switch (_Item.DgnPrjID)
                        {
                            case "014":
                            case "015":
                            case "016":
                                string[] data = _Item.PrjParm.Split('|');
                                float time = float.Parse(data[0]);
                                if (data.Length > 3)
                                {
                                    float hcsj = data[1] != "" ? float.Parse(data[1]) : 0;
                                    float hccs = data[2] != "" ? float.Parse(data[2]) : 0;
                                    time += hcsj * hccs;
                                }
                                //_Row.Cells[3].Value = string.Format("{0}/{1} （当前运行/周期时间（分））", MeterGroup.NowMinute, _Item.PrjParm.Substring(0, _Item.PrjParm.IndexOf("|")));
                                _Row.Cells[3].Value = string.Format("{0}/{1} （当前运行/周期时间（分））", MeterGroup.NowMinute, time.ToString());
                                break;
                            case "029":     //校对需量
                                _Row.Cells[3].Value = string.Format("{0}/{1} （试验需要时间/当前已进行时间(分)）", "2", MeterGroup.NowMinute);
                                break;
                            default:
                                _Row.Cells[3].Value = "";
                                break;
                        }
                    }
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
                else if (_MeterInfo.MeterErrors.ContainsKey(Data_Prepare.Tag.ToString()))
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterErrors[Data_Prepare.Tag.ToString()].Me_chrWcJl;
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
            if (Tab_Prepare.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_Prepare.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr)         //如果是日计时误差
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod)            //如果是费率时段检查
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq)            //如果是时段投切
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod)            //如果是费率时段示值误差
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister)            //如果是计度器示值组合误差
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck)            //如果该控件是电量存储器检查的数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl)          //如果该控件是最大需量的数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang)          //如果该控件是读电量的数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime)            //如果是时间误差数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)            //如果是时间误差数据表单
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)            //如果是费率电价检查表
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)_Control).SetData(MeterGroup.MeterGroup);
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
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_PrePareTest)) return;

            CLDC_DataCore.Struct.StPlan_PrePareTest item = (CLDC_DataCore.Struct.StPlan_PrePareTest)meterGroup.CheckPlan[CheckOrderID];
            CLDC_DataCore.Struct.StPlan_Dgn _Item = new CLDC_DataCore.Struct.StPlan_Dgn();// (CLDC_DataCore.Struct.StPlan_Dgn)meterGroup.CheckPlan[CheckOrderID];
            _Item.DgnPrjName = item.PrePrjName;
            _Item.DgnPrjID = item.PrePrjID;
            _Item.PrjParm = item.PrjParm;
            _Item.OutPramerter = item.OutPramerter;
            bool bFind = false;

            if (Tab_Prepare.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_Prepare.Tag != null && Data_Prepare.Tag.ToString() == _Item.DgnPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_Prepare.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_Prepare.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Prepare.Tag = _Item.DgnPrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.DgnPrjID)
                {
                    #region
                    case "002":
                        {
                            int Count;
                            string[] str = _Item.PrjParm.Split('|');
                            if (str.Length >= 2)
                          int.TryParse(str[1], out Count);
                            else
                            {
                                Count = 10;
                            }
                            Tab_Prepare.TabPages.Add("日计时误差数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr _DateTimeErr = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr(Count);
                            Tab_Prepare.TabPages[1].Controls.Add(_DateTimeErr);
                            _DateTimeErr.Dock = DockStyle.Fill;
                            _DateTimeErr.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_Prepare.TabPages.Add("费率时段数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod _Period = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_Period);
                            _Period.Dock = DockStyle.Fill;
                            _Period.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "004":
                    case "028":
                    case "029":
                    case "030":
                        {
                            Tab_Prepare.TabPages.Add("时段投切数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq _Sdtq = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_Sdtq);
                            _Sdtq.Dock = DockStyle.Fill;
                            _Sdtq.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "005":
                    case "031":
                    case "032":
                    case "033":
                        {
                            Tab_Prepare.TabPages.Add("计度器示值组合误差数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister _Register = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_Register);
                            _Register.Dock = DockStyle.Fill;
                            _Register.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "006":
                    case "034":
                    case "035":
                    case "036":
                        {
                            Tab_Prepare.TabPages.Add("费率时段示值误差数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod _RatePeriod = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_RatePeriod);
                            _RatePeriod.Dock = DockStyle.Fill;
                            _RatePeriod.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "013":
                        {
                            Tab_Prepare.TabPages.Add("时间误差");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime _Gps = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime();
                            Tab_Prepare.TabPages[1].Controls.Add(_Gps);
                            _Gps.Dock = DockStyle.Fill;
                            _Gps.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "019":
                        {
                            Tab_Prepare.TabPages.Add("电量寄存器检查");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck _MemortCheck = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck();
                            Tab_Prepare.TabPages[1].Controls.Add(_MemortCheck);
                            _MemortCheck.Dock = DockStyle.Fill;
                            _MemortCheck.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "014":
                        {
                            Tab_Prepare.TabPages.Add("最大需量0.1Ib数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl _MaxXl = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_MaxXl);
                            _MaxXl.Dock = DockStyle.Fill;
                            _MaxXl.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "015":
                        {
                            Tab_Prepare.TabPages.Add("最大需量1.0Ib数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl _MaxXl = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_MaxXl);
                            _MaxXl.Dock = DockStyle.Fill;
                            _MaxXl.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "016":
                        {
                            Tab_Prepare.TabPages.Add("最大需量Imax数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl _MaxXl = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_MaxXl);
                            _MaxXl.Dock = DockStyle.Fill;
                            _MaxXl.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "017":
                        {
                            Tab_Prepare.TabPages.Add("读取电量数据");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang _DianLiang = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(_DianLiang);
                            _DianLiang.Dock = DockStyle.Fill;
                            _DianLiang.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "043":
                        {
                            Tab_Prepare.TabPages.Add("阶梯电价检查");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff stepPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(stepPrice);
                            stepPrice.Dock = DockStyle.Fill;
                            stepPrice.Margin = new System.Windows.Forms.Padding(0);
                        }
                        break;
                    case "044":
                        {
                            Tab_Prepare.TabPages.Add("费率电价检查");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime ratesPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime(_Item);
                            Tab_Prepare.TabPages[1].Controls.Add(ratesPrice);
                            ratesPrice.Dock = DockStyle.Fill;
                            ratesPrice.Margin = new System.Windows.Forms.Padding(0);
                        }
                        break;
                    #endregion
                }
            }
            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_Prepare.Enabled = true;
        }

        private void Data_Prepare_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_Prepare[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Prepare.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Prepare.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Prepare.EndEdit();
                }
                Data_Prepare.Enabled = false;
                //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBoxEx.Show(this, "没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 显示基本信息窗体相关

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Prepare_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Prepare.SelectedRows[0].Index);
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
                if (Data_Prepare.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Prepare.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Prepare.IsHandleCreated)
                {
                    if (value >= 0 && Data_Prepare.Rows.Count > value)
                    {
                        Data_Prepare.Rows[value].Selected = true;
                        Data_Prepare.CurrentCell = Data_Prepare.Rows[value].Cells[1];
                    }
                }
            }
        }
    }
}

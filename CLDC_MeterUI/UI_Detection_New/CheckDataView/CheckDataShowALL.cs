using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Struct;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckDataShowALL : UserControl
    {
        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;
        /// <summary>
        /// 在当前选择更改时发生
        /// </summary>
        public event EventHandler SelectionChanged;
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;

        List<int> lst_buHeGe = new List<int>();

        public CheckDataShowALL()
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
        public CheckDataShowALL(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            this.DoubleBuffered = true; //开启双缓存
            this._TaiID = TaiID;
            this._TaiType = taiType;

            this.InitializationGrid(meterGroup);
            dataGridViewX_ShowALL.SelectionChanged += new EventHandler(dataGridViewX_ShowALL_SelectionChanged);
            //this.RefreshGrid(meterGroup, CheckOrderID, false);

        }

        void dataGridViewX_ShowALL_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, e);
            }
        }

        /// <summary>
        /// 初始化数据菜单
        /// </summary>
        /// <param name="MeterGroup"></param>
        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            dataGridViewX_ShowALL.EnableHeadersVisualStyles = false;
            int _Count = MeterGroup.MeterGroup.Count;

            //列数=表位数
            if (dataGridViewX_ShowALL.Columns.Count != _Count)
            {
                if (_Count <= 16)
                {
                    dataGridViewX_ShowALL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dataGridViewX_ShowALL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
                dataGridViewX_ShowALL.Columns.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int colId = dataGridViewX_ShowALL.Columns.Add("BW" + i.ToString(), (i + 1).ToString().PadLeft(2, '0') + "表位");
                    dataGridViewX_ShowALL.Columns[colId].Resizable = DataGridViewTriState.True;
                    dataGridViewX_ShowALL.Columns[colId].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridViewX_ShowALL.Columns[colId].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            dataGridViewX_ShowALL.RowHeadersVisible = false;
            //行数=方案项目数
            int _CountR = RefreshRows(MeterGroup);

            dataGridViewX_ShowALL.Refresh();
        }
        /// <summary>
        /// 刷新行数
        /// </summary>
        /// <param name="MeterGroup"></param>
        /// <param name="_Count"></param>
        /// <returns></returns>
        private int RefreshRows(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            int _Count = MeterGroup.CheckPlan.Count;
            if (dataGridViewX_ShowALL.Rows.Count != _Count)           //初始化右部数据表单
            {
                dataGridViewX_ShowALL.Rows.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int index = dataGridViewX_ShowALL.Rows.Add();
                    if ((index + 1) % 2 == 0)
                    {
                        dataGridViewX_ShowALL.Rows[index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        dataGridViewX_ShowALL.Rows[index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    #region 行标识定义
                    object objPlan = null == MeterGroup ? CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan[i] : MeterGroup.CheckPlan[i];
                    //dataGridViewX_ShowALL.Rows[i].Cells[0].Value = objPlan.ToString();
                    try
                    {
                        //将测试项的名称加入到总览表显示
                        dataGridViewX_ShowALL.Rows[i].HeaderCell.Value = objPlan.ToString();
                        if (objPlan is StPlan_WGJC)
                        {
                            StPlan_WGJC oth = (StPlan_WGJC)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "外观|100";
                        }
                        else if (objPlan is StPlan_WcPoint)
                        {
                            StPlan_WcPoint oth = (StPlan_WcPoint)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "数值|" + oth.PrjID;
                        }
                        else if (objPlan is StPlan_QiDong)
                        {
                            StPlan_QiDong oth = (StPlan_QiDong)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "启动|" + (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验 + ((int)oth.PowerFangXiang).ToString();
                        }
                        else if (objPlan is StPlan_QianDong)
                        {
                            StPlan_QianDong oth = (StPlan_QianDong)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "潜动|" + (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验 + ((int)oth.PowerFangXiang).ToString() + (Convert.ToInt32(oth.FloatxU * 100)).ToString("D3");
                        }
                        else if (objPlan is StPlan_SpecalCheck)
                        {
                            StPlan_SpecalCheck oth = (StPlan_SpecalCheck)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "特殊|P_" + i;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_Dgn)
                        {
                            CLDC_DataCore.Struct.StPlan_Dgn oth = (CLDC_DataCore.Struct.StPlan_Dgn)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "功能|" + oth.DgnPrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_PrePareTest)
                        {

                            CLDC_DataCore.Struct.StPlan_PrePareTest oth = (CLDC_DataCore.Struct.StPlan_PrePareTest)objPlan;
                            if (oth.PrePrjName.Contains("接线调试"))
                            {
                                dataGridViewX_ShowALL.Rows[i].Tag = "数值|" + oth.PrePrjID;
                            }
                            else
                            {
                                dataGridViewX_ShowALL.Rows[i].Tag = "功能|" + oth.PrePrjID;
                            }
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPowerConsume)
                        {
                            CLDC_DataCore.Struct.StPowerConsume oth = (CLDC_DataCore.Struct.StPowerConsume)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "功耗|" + oth.PowerConsumePrjID;
                        }
                        else if (objPlan is StPlan_ConnProtocol)
                        {
                            StPlan_ConnProtocol oth = (StPlan_ConnProtocol)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "DLT|" + oth.PrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StInsulationParam)
                        {
                            CLDC_DataCore.Struct.StInsulationParam oth = (CLDC_DataCore.Struct.StInsulationParam)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "耐压|" + oth.InsulationPrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_CostControl)
                        {
                            CLDC_DataCore.Struct.StPlan_CostControl oth = (CLDC_DataCore.Struct.StPlan_CostControl)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "费控|" + oth.CostControlPrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_ZouZi)
                        {
                            CLDC_DataCore.Struct.StPlan_ZouZi oth = (CLDC_DataCore.Struct.StPlan_ZouZi)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "走字|" + oth.PrjID;
                        }
                        else if (objPlan is StErrAccord)
                        {
                            StErrAccord oth = (StErrAccord)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "一致|" + oth.ErrAccordType.ToString();
                        }
                        else if (objPlan is StPlan_Carrier)
                        {
                            StPlan_Carrier oth = (StPlan_Carrier)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "载波|" + String.Format("{0}{1}{2}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.载波试验, oth.str_Code, oth.str_Type);
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_Freeze)
                        {
                            CLDC_DataCore.Struct.StPlan_Freeze oth = (CLDC_DataCore.Struct.StPlan_Freeze)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "冻结|" + oth.FreezePrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_EventLog)
                        {
                            CLDC_DataCore.Struct.StPlan_EventLog oth = (CLDC_DataCore.Struct.StPlan_EventLog)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "事件|" + oth.EventLogPrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_Function)
                        {
                            CLDC_DataCore.Struct.StPlan_Function oth = (CLDC_DataCore.Struct.StPlan_Function)objPlan;
                            dataGridViewX_ShowALL.Rows[i].Tag = "计量|" + oth.FunctionPrjID;
                        }
                        else if (objPlan is CLDC_DataCore.Struct.StPlan_LoadRecord)
                        {                            
                            dataGridViewX_ShowALL.Rows[i].Tag = "负荷|001";
                        }
                        else
                        {
                            dataGridViewX_ShowALL.Rows[i].Tag = "结论|" + "--";
                        }
                    }
                    catch
                    {
                        dataGridViewX_ShowALL.Rows[i].Tag = "结论|" + "--";
                    }
                    #endregion
                }
            }
            return _Count;
        }
        /// <summary>
        /// 刷新数据列表
        /// </summary>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID, bool IsOne)
        {            
            _DnbGroup = MeterGroup;

            int FirstYJMeter = Main.GetFirstYaoJianMeterIndex(MeterGroup);
            if (CheckOrderID >= MeterGroup.CheckPlan.Count)
            {
                return;
            }
            int _Count = MeterGroup.MeterGroup.Count;
            int _PlanCount = MeterGroup.CheckPlan.Count;
            //切换方案，数目大于前方案，刷新行数
            if (_PlanCount != dataGridViewX_ShowALL.Rows.Count)
            {
                int _CountR = RefreshRows(MeterGroup);

                dataGridViewX_ShowALL.Refresh();
            }

            SelectRowIndex = CheckOrderID;

            if (IsOne == true)
            {
                RefreshGridOne(MeterGroup, _Count, CheckOrderID);
            }
            else
            {
                for (int ri = 0; ri < _PlanCount; ri++)
                {
                    RefreshGridOne(MeterGroup, _Count, ri);
                }
            }

            
        }

        private void RefreshGridOne(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int _Count, int ri)
        {
            try
            {
                #region
                string strRst = "";
                int failCount = 0;
                int testResult = 0;
                DataGridViewRow Row = dataGridViewX_ShowALL.Rows[ri];

                for (int ci = 0; ci < _Count; ci++)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[ci];
                    if (!_MeterInfo.YaoJianYn)
                    {
                        continue;
                    }
                    strRst = "";
                    string[] strTag = Row.Tag.ToString().Split('|');
                    #region 外观
                    if ("外观" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterResults.ContainsKey(strTag[1]))
                        {
                            if (null == _MeterInfo.MeterResults[strTag[1]])
                            {
                                strRst = "null";
                            }
                            else
                            {
                                strRst = _MeterInfo.MeterResults[strTag[1]].Mr_chrRstValue;
                            }
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterResults[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }
                    }
                    #endregion 外观
                    #region 数值
                    else if ("数值" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterErrors.ContainsKey(strTag[1]))
                        {
                            string[] strErr = _MeterInfo.MeterErrors[strTag[1]].Me_chrWcMore.Split('|');
                            if (strErr != null && strErr.Length > 1)
                            {

                                Row.Cells[ci].Value = strErr[strErr.Length - 2];
                            }
                            strRst = _MeterInfo.MeterErrors[strTag[1]].Me_chrWcJl;
                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;

                                failCount++;
                                testResult = 2;

                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterErrors[strTag[1]].AVR_DIS_REASON;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                                else
                                {
                                    if (testResult == 1)
                                        testResult = 0;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }
                    }
                    #endregion 数值
                    #region 特殊
                    else if ("特殊" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterSpecialErrs.ContainsKey(strTag[1]))
                        {
                            string[] strErr = _MeterInfo.MeterSpecialErrs[strTag[1]].Mse_Wc.Split('|');
                            if (strErr != null && strErr.Length > 1)
                            {
                                Row.Cells[ci].Value = strErr[strErr.Length - 2];
                            }
                            strRst = _MeterInfo.MeterSpecialErrs[strTag[1]].Mse_Result;
                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterSpecialErrs[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }
                    }
                    #endregion 特殊
                    #region 启动
                    else if ("启动" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterQdQids.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterQdQids[strTag[1]].Mqd_chrJL;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterQdQids[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }
                    }
                    #endregion 启动
                    #region 潜动
                    else if ("潜动" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterQdQids.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterQdQids[strTag[1]].Mqd_chrJL;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterQdQids[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }
                    }
                    #endregion 潜动
                    #region 功能
                    else if ("功能" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterDgns.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterDgns[strTag[1]].Md_chrValue;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterDgns[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 功能
                    #region 功耗
                    else if ("功耗" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterPowers.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterPowers[strTag[1]].Md_chrValue;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterPowers[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 功耗
                    #region 走字
                    else if ("走字" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterZZErrors.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterZZErrors[strTag[1]].Mz_chrJL;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterZZErrors[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 走字
                    #region 费控
                    else if ("费控" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterCostControls.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterCostControls[strTag[1]].Mfk_chrJL;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterCostControls[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 费控
                    #region DLT
                    else if ("DLT" == strTag[0])
                    {
                        strRst = "";

                        if (_MeterInfo.MeterDLTDatas.ContainsKey(strTag[1]))
                        {
                            if (null == _MeterInfo.MeterDLTDatas[strTag[1]])
                            {
                                strRst = "null";
                            }
                            else
                            {
                                strRst = _MeterInfo.MeterDLTDatas[strTag[1]].AVR_CONC;
                            }
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterDLTDatas[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion DLT
                    #region 耐压
                    else if ("耐压" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterInsulations.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterInsulations[strTag[1]].Result;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterInsulations[strTag[1]].Description;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 耐压
                    #region 一致性试验
                    else if ("一致" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterErrAccords.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterErrAccords[strTag[1]].Mea_Result;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterErrAccords[strTag[1]].Description;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 一致
                    #region 载波
                    else if ("载波" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterCarrierDatas.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterCarrierDatas[strTag[1]].Mce_ItemResult;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterCarrierDatas[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 载波
                    #region 计量/智能
                    else if ("计量" == strTag[0])
                    {
                        if (((int)CLDC_Comm.Enum.Cus_FunctionItem.显示功能).ToString().PadLeft(3, '0') == strTag[1])
                        {
                            strRst = "";
                            if (_MeterInfo.MeterShows.ContainsKey(strTag[1]))
                            {
                                strRst = _MeterInfo.MeterShows[strTag[1]].Msh_chrJL;
                                Row.Cells[ci].Value = strRst;


                                if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                                {
                                    Row.Cells[ci].Style.ForeColor = Color.Red;
                                    Row.Cells[ci].ToolTipText = _MeterInfo.MeterShows[strTag[1]].AVR_DIS_REASON;

                                    failCount++;
                                    testResult = 2;
                                }
                                else
                                {
                                    Row.Cells[ci].Style.ForeColor = Color.Black;
                                    Row.Cells[ci].ToolTipText = string.Empty;

                                    if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                    {
                                        if (testResult == 0)
                                            testResult = 1;
                                    }
                                }
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;
                                Row.Cells[ci].Value = "";
                            }
                        }
                        else
                        {
                            strRst = "";
                            if (_MeterInfo.MeterFunctions.ContainsKey(strTag[1]))
                            {
                                strRst = _MeterInfo.MeterFunctions[strTag[1]].Mf_chrValue;
                                Row.Cells[ci].Value = strRst;


                                if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                                {
                                    Row.Cells[ci].Style.ForeColor = Color.Red;
                                    Row.Cells[ci].ToolTipText = _MeterInfo.MeterFunctions[strTag[1]].AVR_DIS_REASON;

                                    failCount++;
                                    testResult = 2;
                                }
                                else
                                {
                                    Row.Cells[ci].Style.ForeColor = Color.Black;
                                    Row.Cells[ci].ToolTipText = string.Empty;

                                    if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                    {
                                        if (testResult == 0)
                                            testResult = 1;
                                    }
                                }
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;
                                Row.Cells[ci].Value = "";
                            }
                        }
                    }
                    #endregion
                    #region 事件
                    else if ("事件" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterSjJLgns.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterSjJLgns[strTag[1]].ItemConc;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterSjJLgns[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion
                    #region 负荷记录
                    else if ("负荷" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterLoadRecords.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterLoadRecords[strTag[1]].Ml_Result;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterLoadRecords[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion
                    #region 冻结
                    else if ("冻结" == strTag[0])
                    {
                        strRst = "";
                        if (_MeterInfo.MeterFreezes.ContainsKey(strTag[1]))
                        {
                            strRst = _MeterInfo.MeterFreezes[strTag[1]].Md_chrValue;
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterFreezes[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;
                            Row.Cells[ci].Value = "";
                        }

                    }
                    #endregion 
                    #region 其它
                    else
                    {
                        strRst = "";

                        if (_MeterInfo.MeterResults.ContainsKey(strTag[1]))
                        {
                            if (null == _MeterInfo.MeterResults[strTag[1]])
                            {
                                strRst = "null";
                            }
                            else
                            {
                                strRst = _MeterInfo.MeterResults[strTag[1]].Mr_chrRstValue;
                            }
                            Row.Cells[ci].Value = strRst;


                            if (CLDC_DataCore.Const.Variable.CTG_BuHeGe == strRst)
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Red;
                                Row.Cells[ci].ToolTipText = _MeterInfo.MeterResults[strTag[1]].AVR_DIS_REASON;

                                failCount++;
                                testResult = 2;
                            }
                            else
                            {
                                Row.Cells[ci].Style.ForeColor = Color.Black;
                                Row.Cells[ci].ToolTipText = string.Empty;

                                if (CLDC_DataCore.Const.Variable.CTG_HeGe == strRst)
                                {
                                    if (testResult == 0)
                                        testResult = 1;
                                }
                            }
                        }
                        else
                        {
                            Row.Cells[ci].Style.ForeColor = Color.Black;
                            Row.Cells[ci].ToolTipText = string.Empty;

                            Row.Cells[ci].Value = "";
                        }
                    }
                    #endregion 其它



                }

                if (EventRefreshSummary != null)
                {
                    EventRefreshSummary(ri, testResult, failCount);
                }

                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action<string>(A => RefreshHeaderCellResult(MeterGroup, _Count)), "");
                }
                #endregion


            }
            catch (Exception ex)
            {
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }

        //private void RefreshHeaderCellResult()
        //{
            //string _strBW;
            //lst_buHeGe.Clear();
            //foreach (DataGridViewRow row in dataGridViewX_ShowALL.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (lst_buHeGe.Contains(cell.ColumnIndex))
            //        {
            //            continue;
            //        }
            //        _strBW = (cell.ColumnIndex + 1).ToString().PadLeft(2, '0') + "表位";
            //        if (cell.Style.ForeColor == Color.Red)
            //        {
            //            lst_buHeGe.Add(cell.ColumnIndex);
            //            dataGridViewX_ShowALL.Columns[cell.ColumnIndex].HeaderCell.Value = _strBW + " ×";
            //            dataGridViewX_ShowALL.Columns[cell.ColumnIndex].HeaderCell.Style.ForeColor = Color.Red;
            //        }
            //    }
            //}
            //foreach (DataGridViewColumn col in dataGridViewX_ShowALL.Columns)
            //{
            //    if (lst_buHeGe.Contains(col.Index) == false)
            //    {
            //        _strBW = (col.Index + 1).ToString().PadLeft(2, '0') + "表位";
            //        col.HeaderCell.Value = _strBW + " √";
            //        col.HeaderCell.Style.ForeColor = Color.Green;
            //    }
            //}


            
        //}
        private void RefreshHeaderCellResult(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int _Count)
        {

            for (int ci = 0; ci < _Count; ci++)
            {
                string rstA = MeterGroup.MeterGroup[ci].Mb_Result;
                if (rstA == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    dataGridViewX_ShowALL.Columns[ci].HeaderCell.Style.ForeColor = Color.Red;
                }
                else if (rstA == CLDC_DataCore.Const.Variable.CTG_HeGe)
                {
                    dataGridViewX_ShowALL.Columns[ci].HeaderCell.Style.ForeColor = Color.Green;
                }
            }

        }

        /// <summary>
        /// 创建刷新测试结论事件
        /// </summary>
        /// <param name="index">测试项目序号</param>
        /// <param name="testResult">测试结论</param>
        /// <param name="failCount">测试失败的表的数量</param>
        public delegate void DelegateRefreshSummary(int index, int testResult, int failCount);
        public event DelegateRefreshSummary EventRefreshSummary;

        /// <summary>
        /// 外部调用数据刷新，
        /// </summary>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定ID</param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(v =>
                                {
                                    this.BeginInvoke(new Action<string>(A =>
                                    {

                                        this.RefreshGrid(meterGroup, CheckOrderID, true);

                                    }), "");
                                }));
        }
        /// <summary>
        /// 刷新所有数据
        /// </summary>
        /// <param name="meterGroup"></param>
        public void RefreshDataALL(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(A =>
            {
                while (this.IsHandleCreated == false)
                {

                }
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action<string>(v =>
                    {

                        this.RefreshGrid(meterGroup, 0, false);

                    }), "");
                }

            }));
        }
        /// <summary>
        /// 获取或设置当前选中的行号
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                //if (dataGridViewX_ShowALL.SelectedRows.Count == 0)
                //{
                    return 0;
                //}
                //else
                //{
                //    //return dataGridViewX_ShowALL.SelectedRows[0].Index;
                //    if (dataGridViewX_ShowALL.SelectedColumns.Count > 0)
                //    {
                //        return dataGridViewX_ShowALL.SelectedColumns[0].Index;
                //    }
                //    return -1;
                //}
            }
            set
            {
                if (dataGridViewX_ShowALL.IsHandleCreated)
                {
                    if (value >= 0 && dataGridViewX_ShowALL.Rows.Count > value)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.BeginInvoke(new Action<string>(A =>
                            {
                                dataGridViewX_ShowALL.Rows[value].Selected = true;
                                dataGridViewX_ShowALL.FirstDisplayedScrollingRowIndex = value > 10 ? value - 10 : 0;
                                //dataGridViewX_ShowALL.CurrentCell = dataGridViewX_ShowALL.Rows[value].Cells[0];
                            }), "");
                        }
                    }
                }
            }
        }
        public int GetRowIndex
        {
            get
            {
                if (dataGridViewX_ShowALL.SelectedCells.Count > 0)
                    return dataGridViewX_ShowALL.SelectedCells[0].RowIndex;
                else return -1;
            }

        }
        /// <summary>
        /// 设置行头是否显示
        /// </summary>
        /// <param name="state"></param>
        public void SetRowHeaderState(bool state)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<bool>(A => { dataGridViewX_ShowALL.RowHeadersVisible = state; }), state);
            }
            else
            {
                dataGridViewX_ShowALL.RowHeadersVisible = state;
            }
        }


    }
}

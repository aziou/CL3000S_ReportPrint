using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using System.Threading;

using CLDC_DataCore.Struct;

namespace CLDC_MeterUI.UI_Detection_New
{

    /// <summary>
    /// 项目选中触发事件委托函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void EventDat_Prj_SelectionChanged(object sender, EventArgs e);



    public partial class CheckBase : Base
    {        
        /// <summary>
        /// 是否是切点操作
        /// </summary>
        private bool IsChangedCheckPoint = false;

        /// <summary>
        /// 被检表信息窗体
        /// </summary>
        static CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_DnbInfoView _DnbInfoView;

        /// <summary>
        /// 控制消息窗体
        /// </summary>
        static CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_ControlMessage _ControlMsg;

        /// <summary>
        /// 检定参数设置窗体
        /// </summary>
        static CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_CheckParamView _CheckParm;

        /// <summary>
        /// 浏览检定数据窗体
        /// </summary>
        static CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_DnbDataView _DnbDataView;

        /// <summary>
        /// 检定项目ID（整个方案列表中的元素下标）
        /// </summary>
        protected int CheckOrderID = -2100000000;

        /// <summary>
        /// 当前选中的表位号
        /// </summary>
        private int SelectedBwh = 0;

        private string _strBasicInfo = "电能表信息";
        //设置电能表概要信息
        public string strBasicInfo
        {
            set
            {
                _strBasicInfo = value;
                this.labelX1.Text = _strBasicInfo;
            }
        }

        public CheckBase()
        {
            InitializeComponent();
            Btn_DoComplated.Visible = false;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent">父域名称</param>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="taiType">台体类型0-三相台，1-单相台</param>
        /// <param name="taiId">台体编号</param>
        public CheckBase(
            Main parent
            , CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup
            , int taiType
            , int taiId)
            : base(parent, meterGroup, taiType, taiId)
        {
            InitializeComponent();
            //splitContainer1.Visible = false;    //先隐藏、待初始化完成以后再显示
            Btn_DoComplated.Visible = false;
            Chk_TiaoBiao.CheckedChanged -= new EventHandler(Chk_TiaoBiao_Click);
            Chk_TiaoBiao.CheckedChanged += new EventHandler(Chk_TiaoBiao_Click);
            chk_BianChengTip.Checked = CLDC_DataCore.Const.GlobalUnit.Tip_Programming.isTip;
            chk_BianChengTip.Click += new EventHandler(chk_BianChengTip_Click);
            this.Disposed += delegate(object sender, EventArgs e)
                            {
                                this.CloseViewForm();
                            };
            CLDC_DataCore.Function.ThreadCallBack.Call(new CLDC_DataCore.Function.CallBack(DipscherAutoStartCheck), 10000);
        }


        ~CheckBase()
        {
            if (_DnbInfoView != null)
            {
                if (_DnbInfoView.Visible)
                {
                    _DnbInfoView.Close();
                }
                _DnbInfoView.Dispose();
            }

            if (_ControlMsg != null)
            {
                if (_ControlMsg.Visible)
                {
                    _ControlMsg.Close();
                }
                _ControlMsg.Dispose();

            }
            if (_CheckParm != null)
            {
                if (_CheckParm.Visible)
                {
                    _CheckParm.Close();
                }
                _CheckParm.Dispose();
            }
            if (_DnbDataView != null)
            {
                if (_DnbDataView.Visible)
                {
                    _DnbDataView.Close();
                }
                _DnbDataView.Dispose();
            }

        }

        void DipscherAutoStartCheck()
        {
            //TODO:调度启动时，加载后5秒自动开始检定
            //开始检定先决条件
            if (CLDC_DataCore.Const.GlobalUnit.DispatcherType != 1)
            {
                return;
            }
            if (CLDC_DataCore.Const.GlobalUnit.IsDemo)
            {
                return;
            }
            if (CLDC_DataCore.Const.GlobalUnit.EquipConnectedState != CLDC_Comm.Enum.Cus_NetState.Connected)
            {
                return;
            }
            
            if (CLDC_DataCore.Const.GlobalUnit.DispatcherCanAutoStart == false)
            {
                return;
            }
            if (MeterGroup.ActiveItemID != 0 && ParentMain.ActiveIdByClick != 0)
            {
                return;
            }
            
            //连续检定
            ButtonItem btnStartTmp = new ButtonItem("btnStartTmp", "连续检定");
            ParentMain.ActiveToolStrip_Item(btnStartTmp);
            CLDC_DataCore.Const.GlobalUnit.DispatcherCanAutoStart = false;
        }
        /// <summary>
        /// 调表操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Chk_TiaoBiao_Click(object sender, EventArgs e)
        {
            if (Chk_TiaoBiao.Checked)
            {
                //Chk_TiaoBiao.Checked = 
                ParentMain.CheckAdjust(true);
            }
            else
            {
                //Chk_TiaoBiao.Checked = !
                ParentMain.CheckAdjust(false);
            }
        }
        /// <summary>
        /// 编程提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chk_BianChengTip_Click(object sender, EventArgs e)
        {
            if (chk_BianChengTip.Checked)
            {
                chk_BianChengTip.Checked = ParentMain.ProgrammingTipAdjust(false);
            }
            else
            {
                chk_BianChengTip.Checked = !ParentMain.ProgrammingTipAdjust(true);
            }
        }
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CheckBase_Load(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            this.Tool_Control.Enabled = false;

            //this.Cmb_ClearDataType.SelectedIndex = 0;
            this.TButtn_ChangeFA.Click += new EventHandler(TButtn_ChangeFA_Click);
            this.TButton_Refresh.Click += new EventHandler(TButton_Refresh_Click);
            //this.TButton_Clear.Click += new EventHandler(TButton_Clear_Click);
            this.TButton_Show.Click += new EventHandler(TButton_Show_Click);
            ThreadPool.QueueUserWorkItem(new WaitCallback(InitDat_PrjRows));
            Init_advTreeStyle();
        }

        private void TButton_Show_Click(object sender, EventArgs e)
        {
            if (this.TButton_Show.Text.IndexOf("隐藏") >= 0)
            {
                this.splitContainer1.Panel1Collapsed = true;
                this.TButton_Show.Text = "显示方案列表";

                if (tabControlPanel_ShowALL.Controls.Count > 0)
                {
                    UserControl _BaseControl = (UserControl)tabControlPanel_ShowALL.Controls[0];
                    if (_BaseControl is CheckDataView.CheckDataShowALL)
                    {
                        ((CheckDataView.CheckDataShowALL)_BaseControl).SetRowHeaderState(true);
                    }
                }
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = false;
                this.TButton_Show.Text = "隐藏方案列表";

                if (tabControlPanel_ShowALL.Controls.Count > 0)
                {
                    UserControl _BaseControl = (UserControl)tabControlPanel_ShowALL.Controls[0];
                    if (_BaseControl is CheckDataView.CheckDataShowALL)
                    {
                        ((CheckDataView.CheckDataShowALL)_BaseControl).SetRowHeaderState(false);
                    }
                }
            }
        }
        #region --------------------------方案更换、刷新、项目检定数据清理相关--------------------
        #region 注释的内容
        /// <summary>
        /// 项目检定内容删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void TButton_Clear_Click(object sender, EventArgs e)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType ClearType = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType();

            switch (this.Cmb_ClearDataType.SelectedIndex)
            {
                case 1: //启动数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.启动数据;
                    break;
                case 2: //潜动数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.潜动数据;
                    break;
                case 3: //基本误差试验
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.误差数据;
                    break;
                case 4://走字数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.走字数据;
                    break;
                case 5://多功能数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.多功能数据;
                    break;
                case 6://特殊检定数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.特殊检定数据;
                    break;
                case 7://通讯协议检查试验
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.通讯协议检定数据;
                    break;
                case 8://所有数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.特殊检定数据;
                    break;
                case 9://载波试验数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.载波数据;
                    break;
                case 10://误差一致性数据
                    ClearType = CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo.ClearDataType.误差一致性数据;
                    break;
                default:
                    return;
            }
            if (MessageBoxEx.Show(string.Format("请问是否确认清理{0}", Cmb_ClearDataType.Text), "数据清理询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("数据清理中，请稍后...");
            if ((int)ClearType == 0)
            {
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
                {
                    if (MeterGroup.MeterGroup[i].YaoJianYn)
                    {
                        MeterGroup.MeterGroup[i].ClearData();
                    }
                }
            }
            else
            {
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
                {
                    if (MeterGroup.MeterGroup[i].YaoJianYn)
                    {
                        MeterGroup.MeterGroup[i].ClearData(ClearType);
                    }
                }
            }


            if (ParentMain.Evt_InputParam_OnOk != null)
            {
                ParentMain.Evt_InputParam_OnOk(MeterGroup, base.TaiType, base.TaiId);
                
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();

        }
        */
        #endregion

        /// <summary>
        /// 方案刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TButton_Refresh_Click(object sender, EventArgs e)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在刷新数据，请稍后...");
            if (MeterGroup.CreateFA(base.TaiType, MeterGroup.FaName))
            {
                //this.CheckFAChangeAndDataRefrash(MeterGroup);
                MeterGroup.CheckFAChangeAndDataRefrash();
                if (ParentMain.Evt_InputParam_OnOk != null)
                {
                    this.CreateNewRowCell(MeterGroup.CheckPlan);
                    ParentMain.Evt_InputParam_OnOk(MeterGroup, base.TaiType, base.TaiId);
                }
                try
                {
                    CLDC_Dispatcher.DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.SetSchemeChanged);
                }
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();

        }
        /// <summary>
        /// 方案改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TButtn_ChangeFA_Click(object sender, EventArgs e)
        {
            FrmNewFA FaSelect = new FrmNewFA(base.TaiType);
            FaSelect.Location = this.PointToScreen(new Point(0, 21));
            FaSelect.SetFaName(base.MeterGroup.FaName);

            FaSelect.ShowDialog();

            string FaName = FaSelect.FaName;

            if (FaName == "")
            {
                return;
            }
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在加载新方案并刷新数据，请稍后...");
            if (MeterGroup.CreateFA(base.TaiType, FaName))
            {
                //this.CheckFAChangeAndDataRefrash(MeterGroup);
                MeterGroup.CheckFAChangeAndDataRefrash();
                if (ParentMain.Evt_InputParam_OnOk != null)
                {
                    this.CreateNewRowCell(MeterGroup.CheckPlan);
                    ParentMain.Evt_InputParam_OnOk(MeterGroup, base.TaiType, base.TaiId);
                }
                try
                {
                    CLDC_Dispatcher.DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.SetSchemeChanged);
                }
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }

        #region 注释的内容
        ///// <summary>
        ///// 进行方案比较，同时检查数据
        ///// </summary>
        ///// <param name="DnbGroup"></param>
        //private void CheckFAChangeAndDataRefrash( CLDC_DataCore.Model.DnbModel.DnbGroupInfo DnbGroup)
        //{
        //    int int_Bwh = DnbGroup.GetFirstYaoJianMeterBwh();
        //    if (int_Bwh == -1) return;
        //    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = DnbGroup.GetMeterBasicInfoByBwh(int_Bwh + 1);

        //    string[] TmpKeys;

        //    #region -------------------启动、潜动项目数据和新方案检查更新------------------------
        //    if (MeterInfo.MeterResults.Count > 0)
        //    {
        //        TmpKeys = new string[MeterInfo.MeterResults.Count];

        //        MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);          //将结论关键字集拷贝到一个数组中，便于后续操作

        //        foreach (string Key in TmpKeys)
        //        {
        //            if (Key.Length == 4 && Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString())  //启动
        //            {
        //                bool IsFind = false;          //是否找到节点的标志
        //                for (int i = 0; i < DnbGroup.CheckPlan.Count; i++)          //循环当前方案
        //                {
        //                    if (DnbGroup.CheckPlan[i] is Comm.Struct.StQiDong)      //如果当前方案项目是启动    
        //                    {
        //                        if ((int)((Comm.Struct.StQiDong)DnbGroup.CheckPlan[i]).PowerFangXiang == int.Parse(Key.Substring(3)))      //如果当前方案项目和已存在的数据项目吻合，则表示找到对应节点
        //                        {
        //                            IsFind = true;
        //                            break;
        //                        }
        //                    }
        //                }
        //                if (!IsFind)            //如果没找到对应节点，就要删除当前检定过的数据节点
        //                {
        //                    for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                    {
        //                        if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].MeterResults.ContainsKey(Key))
        //                            {
        //                                DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (Key.Length == 4 && Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString())      //潜动
        //            {
        //                bool IsFind = false;          //是否找到节点的标志
        //                for (int i = 0; i < DnbGroup.CheckPlan.Count; i++)          //循环当前方案
        //                {
        //                    if (DnbGroup.CheckPlan[i] is Comm.Struct.StQianDong)      //如果当前方案项目是启动    
        //                    {
        //                        if ((int)((Comm.Struct.StQianDong)DnbGroup.CheckPlan[i]).PowerFangXiang == int.Parse(Key.Substring(3)))      //如果当前方案项目和已存在的数据项目吻合，则表示找到对应节点
        //                        {
        //                            IsFind = true;
        //                            break;
        //                        }
        //                    }
        //                }
        //                if (!IsFind)            //如果没找到对应节点，就要删除当前检定过的数据节点
        //                {
        //                    for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                    {
        //                        if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].MeterResults.ContainsKey(Key))
        //                            {
        //                                DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (MeterInfo.MeterResults.Count > 0)
        //    {
        //        TmpKeys = new string[MeterInfo.MeterResults.Count];
        //        MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);     //将结论关键字集拷贝到一个数组中，便于后续操作
        //        foreach (string Key in TmpKeys)         //再次遍历，看是否存在有总结点，没有子节点的情况，如果存在就应该删除总结点，比如：存在109，但是不存在109（1-4）
        //        {
        //            if (Key == ((int)Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString())  //如果存在启动总结论
        //            {
        //                bool IsFind = false;        //分结论是否存在标志
        //                for (int i = 1; i <= 4; i++)
        //                {
        //                    if (MeterInfo.MeterResults.ContainsKey(Key + i.ToString()))         //如果找到分结论则跳出
        //                    {
        //                        IsFind = true;
        //                        break;
        //                    }
        //                }
        //                if (!IsFind)            //如果没有分结论，则开始查找并删除所有要检表的总结论
        //                {
        //                    for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                    {
        //                        if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].MeterResults.ContainsKey(Key))
        //                            {
        //                                DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (Key == ((int)Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString())  //如果存在潜动总结论
        //            {
        //                bool IsFind = false;         //分结论是否存在标志
        //                for (int i = 1; i <= 4; i++)
        //                {
        //                    if (MeterInfo.MeterResults.ContainsKey(Key + i.ToString())) //如果找到分结论则跳出
        //                    {
        //                        IsFind = true;
        //                        break;
        //                    }
        //                }
        //                if (!IsFind)        //如果没有分结论，则开始查找并删除所有要检表的总结论
        //                {
        //                    for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                    {
        //                        if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].MeterResults.ContainsKey(Key))
        //                            {
        //                                DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    #endregion

        //    #region -------------------------基本误差试验数据和新方案检查更新---------------------

        //    #region ----------------------------清理误差数据表的多余数据------------------------
        //    if (MeterInfo.MeterErrors.Count > 0)
        //    {
        //        TmpKeys = new string[MeterInfo.MeterErrors.Count];

        //        MeterInfo.MeterErrors.Keys.CopyTo(TmpKeys, 0);

        //        foreach (string Key in TmpKeys)         //检查当前误差数据集关键字数组
        //        {
        //            bool IsFind = false;            //是否找到标志

        //            for (int i = 0; i < DnbGroup.CheckPlan.Count; i++)          //循环方案
        //            {
        //                if (DnbGroup.CheckPlan[i] is Comm.Struct.CheckPoint)        //如果当前方案项目是误差结构体
        //                {
        //                    if (Key == ((Comm.Struct.CheckPoint)DnbGroup.CheckPlan[i]).PrjID)       //如果当前方案项目在误差数据集里面存在
        //                    {
        //                        IsFind = true;
        //                        break;
        //                    }
        //                }
        //            }
        //            if (!IsFind)                    //如果循环了方案都还找不到，即表示当前方案中不检改点，需要删除该负载点数据
        //            {
        //                for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                {
        //                    if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                    {
        //                        if (DnbGroup.MeterGroup[i].MeterErrors.Count > 0)
        //                        {
        //                            try
        //                            {
        //                                DnbGroup.MeterGroup[i].MeterErrors.Remove(Key);      //从数据集合冲移除该负载点数据
        //                            }
        //                            catch { }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    #endregion

        //    #region ------------------------清理结论数据表中的多余数据----------------------
        //    if (MeterInfo.MeterResults.Count > 0)      //清理结论表
        //    {
        //        if (MeterInfo.MeterErrors.Count == 0)       //如果误差数据集合没有数据
        //        {
        //            #region ---------------没有误差数据，清理所有误差相关结论------------------------
        //            for (int Bwh = 0; Bwh < DnbGroup.MeterGroup.Count; Bwh++)
        //            {
        //                if (DnbGroup.MeterGroup[Bwh].YaoJianYn)
        //                {
        //                    for (int i = 1; i <= 4; i++)            //循环功率方向ID,,下面全部做容错，这样比用ContainsKey方法要快很多
        //                    {
        //                        try
        //                        {
        //                            DnbGroup.MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}{1}", (int)Comm.Enum.Cus_MeterResultPrjID.基本误差, i));
        //                        }
        //                        catch { }
        //                        try
        //                        {
        //                            DnbGroup.MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}{1}", (int)Comm.Enum.Cus_MeterResultPrjID.标准偏差, i));
        //                        }
        //                        catch { }
        //                    }
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}", (int)Comm.Enum.Cus_MeterResultPrjID.最大偏差));
        //                    }
        //                    catch { }
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}", (int)Comm.Enum.Cus_MeterResultPrjID.基本误差));
        //                    }
        //                    catch { }
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}", (int)Comm.Enum.Cus_MeterResultPrjID.标准偏差));
        //                    }
        //                    catch { }
        //                }
        //            }
        //            #endregion 
        //        }
        //        else
        //        {
        //            #region --------------------清理分项数据-------------------------------
        //            TmpKeys = new string[MeterInfo.MeterResults.Count];
        //            MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);
        //            foreach (string Key in TmpKeys)
        //            {
        //                if (Key.Length == 4 && Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.基本误差).ToString())
        //                {
        //                    bool BlnFind = false;
        //                    foreach (string ErrKey in MeterInfo.MeterErrors.Keys)
        //                    {
        //                        if (ErrKey.Substring(0, 1) == ((int)Comm.Enum.Cus_WcType.基本误差).ToString()
        //                            && ErrKey.Substring(1, 1) == Key.Substring(3, 1))          //基本误差功率方向
        //                        {
        //                            BlnFind = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!BlnFind)           //没找到，则删除该结论
        //                    {
        //                        for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                            {
        //                                try
        //                                {
        //                                    DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                                }
        //                                catch { }
        //                            }
        //                        }
        //                    }
        //                }
        //                if (Key.Length == 4 && Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString())
        //                {
        //                    bool BlnFind = false;
        //                    foreach (string ErrKey in MeterInfo.MeterErrors.Keys)
        //                    {
        //                        if (ErrKey.Substring(0, 1) == ((int)Comm.Enum.Cus_WcType.标准偏差).ToString()
        //                            && ErrKey.Substring(1, 1) == Key.Substring(3, 1))          //标准偏差功率方向
        //                        {
        //                            BlnFind = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!BlnFind)           //没找到，则删除该结论
        //                    {
        //                        for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                            {
        //                                try
        //                                {
        //                                    DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                                }
        //                                catch { }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()))
        //            {
        //                bool IsFind = false;
        //                for (int i = 1; i <= 4; i++)
        //                {
        //                    if (MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString() + i.ToString()))
        //                    {
        //                        IsFind = true;
        //                        break;
        //                    }
        //                }
        //                if (!IsFind)
        //                {
        //                    for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                    {
        //                        try
        //                        {
        //                            DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString());
        //                        }
        //                        catch { }
        //                        try
        //                        {
        //                            DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString());
        //                        }
        //                        catch { }
        //                    }
        //                }
        //            }
        //            if (MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPrjID.基本误差).ToString()))
        //            {
        //                bool IsFind = false;
        //                for (int i = 1; i <= 4; i++)
        //                {
        //                    if (MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPrjID.基本误差).ToString() + i.ToString()))
        //                    {
        //                        IsFind = true;
        //                        break;
        //                    }
        //                }
        //                if (!IsFind)
        //                {
        //                    for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                    {
        //                        try
        //                        {
        //                            DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.基本误差).ToString());
        //                        }
        //                        catch { }
        //                    }
        //                }
        //            }


        //            #endregion 
        //        }

        //    }
        //    #endregion
        //    #endregion

        //    #region ----------------多功能数据和信方案检查更新-----------------------
        //    if (MeterInfo.MeterDgns.Count > 0)
        //    {
        //        TmpKeys = new string[MeterInfo.MeterDgns.Count];
        //        MeterInfo.MeterDgns.Keys.CopyTo(TmpKeys, 0);

        //        foreach (string Key in TmpKeys)
        //        {
        //            bool BlnFind = false;
        //            for (int i = 0; i < DnbGroup.CheckPlan.Count; i++)
        //            {
        //                if (DnbGroup.CheckPlan[i] is Comm.Struct.StDgnPlan)
        //                {
        //                    if (Key == ((Comm.Struct.StDgnPlan)DnbGroup.CheckPlan[i]).DgnPrjID)
        //                    {
        //                        BlnFind = true;
        //                        break;
        //                    }
        //                }
        //            }
        //            if (!BlnFind)
        //            {
        //                for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                {
        //                    if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                    {
        //                        try
        //                        {
        //                            DnbGroup.MeterGroup[i].MeterDgns.Remove(Key);
        //                        }
        //                        catch { }
        //                    }
        //                }
        //            }
        //        }

        //        if (MeterInfo.MeterDgns.Count == 0)
        //        {
        //            for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //            {
        //                if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                {
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.多功能试验).ToString());
        //                    }
        //                    catch { }
        //                }
        //            }
        //        }

        //    }



        //    #endregion

        //    #region -------------------走字、特殊检定数据和新方案检查更新----------------

        //    this.CheckZZAndTsData(int_Bwh+1,DnbGroup);

        //    #endregion 
        //}
        ///// <summary>
        ///// 进行走字和特殊检定的方案比较，走字和特殊检定的Key并非项目编号，检查难度非常大，所以独立出来写了个方法处理，该方法不做其他用途
        ///// 只供CheckFAChangeAndDataRefrash函数调用，
        ///// </summary>
        ///// <param name="FirstBwh">第一只要检表</param>
        ///// <param name="DnbGroup">电能表数据集合</param>
        //private void CheckZZAndTsData(int FirstBwh, CLDC_DataCore.Model.DnbModel.DnbGroupInfo DnbGroup)
        //{
        //    string[] TmpKeys;

        //    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = DnbGroup.GetMeterBasicInfoByBwh(FirstBwh);

        //    #region --------------------走字数据和新方案检查更新-----------------------------

        //    if (MeterInfo.MeterZZErrors.Count > 0)
        //    {
        //        List<string> TmpOldKeys = new List<string>();
        //        List<string> TmpNewKeys = new List<string>();

        //        for (int i = 0; i < DnbGroup.CheckPlan.Count; i++)          //循环新方案
        //        {
        //            if (DnbGroup.CheckPlan[i] is Comm.Struct.StZouZiPlan)       //如果方案项目里面存在走字项目
        //            {
        //                Comm.Struct.StZouZiPlan FaZouItem = DnbGroup.CheckPlan[i] as Comm.Struct.StZouZiPlan;

        //                foreach (string Key in MeterInfo.MeterZZErrors.Keys)        //循环走字检定记录
        //                {
        //                    if (FaZouItem.PrjID == MeterInfo.MeterZZErrors[Key].Mz_PrjID)       //如果在记录里面找到该项目
        //                    {
        //                        if (!TmpOldKeys.Contains(Key))          //同时该项目关键字不能在旧关键字列表中存在
        //                        {
        //                            TmpOldKeys.Add(Key);                //在旧关键字列表中增加走字检定记录项目关键字
        //                            TmpNewKeys.Add("P_" + i.ToString());    //在新关键字列表中增加新方案项目下标。
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (TmpOldKeys.Count == 0)     //如果旧关键字列表为空，则表示新方案和当前检定的走字数据没有共同项目，所以要移除所有的走字检定数据和结论
        //        {
        //            for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //            {
        //                if (!DnbGroup.MeterGroup[i].YaoJianYn) continue;
        //                DnbGroup.MeterGroup[i].MeterZZErrors.Clear();       //清理走字检定数据
        //                try
        //                {
        //                    DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString());       //移除走字检定结论
        //                }
        //                catch { }
        //                try
        //                {
        //                    DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString());       //移除走字组合误差试验
        //                }
        //                catch { }
        //                try
        //                {
        //                    DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString());       //移除走字组合误差值
        //                }
        //                catch { }
        //                for (int j = 1; j <= 4; j++)
        //                {
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString()+j.ToString());
        //                    }
        //                    catch { }
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString() + j.ToString());       //移除走字检定结论
        //                    }
        //                    catch { }
        //                    try
        //                    {
        //                        DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString() + j.ToString());       //移除走字组合误差值
        //                    }
        //                    catch { }
        //                }


        //            }
        //        }
        //        else
        //        {
        //            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError> ZZItems;

        //            for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //            {
        //                if (!DnbGroup.MeterGroup[i].YaoJianYn) continue;
        //                if (DnbGroup.MeterGroup[i].MeterZZErrors.Count == 0) continue;

        //                ZZItems= new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError>();

        //                for (int j = 0; j < TmpOldKeys.Count; j++)
        //                {
        //                    try
        //                    {
        //                        ZZItems.Add(TmpNewKeys[j], DnbGroup.MeterGroup[i].MeterZZErrors[TmpOldKeys[j]]);
        //                    }
        //                    catch { }
        //                }
        //                DnbGroup.MeterGroup[i].MeterZZErrors = ZZItems;
        //                ZZItems = null;
        //            }
        //            #region --------------------清理分项数据-------------------------------
        //            TmpKeys = new string[MeterInfo.MeterResults.Count];
        //            MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);
        //            foreach (string Key in TmpKeys)
        //            {
        //                if (Key.Length == 4 && 
        //                                (Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString() || 
        //                                 Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString() || 
        //                                 Key.Substring(0, 3) == ((int)Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString()))
        //                {
        //                    bool BlnFind = false;
        //                    foreach( CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZItem in MeterInfo.MeterZZErrors.Values)
        //                    {
        //                        if (Key.Substring(3, 1)==ZZItem.Mz_PrjID.Substring(0, 1))
        //                        {
        //                            BlnFind = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!BlnFind)           //没找到，则删除该结论
        //                    {
        //                        for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //                        {
        //                            if (DnbGroup.MeterGroup[i].YaoJianYn)
        //                            {
        //                                try
        //                                {
        //                                    DnbGroup.MeterGroup[i].MeterResults.Remove(Key);
        //                                }
        //                                catch { }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion 
        //        }
        //    }
        //    #endregion 

        //    #region-------------------特殊检定数据和新方案检查更新-----------------------

        //    if (MeterInfo.MeterSpecialErrs.Count > 0)
        //    {
        //        List<string> TmpOldKeys = new List<string>();
        //        List<string> TmpNewKeys = new List<string>();
        //        for (int i = 0; i < DnbGroup.CheckPlan.Count; i++)          //循环新方案
        //        {
        //            if (DnbGroup.CheckPlan[i] is Comm.Struct.StSpecalCheckPlan)       //如果方案项目里面存在特殊项目
        //            {
        //                Comm.Struct.StSpecalCheckPlan FaSpItem = (Comm.Struct.StSpecalCheckPlan)DnbGroup.CheckPlan[i];

        //                foreach (string Key in MeterInfo.MeterSpecialErrs.Keys)        //循环特殊检定记录
        //                {
        //                    if (FaSpItem.PrjName == MeterInfo.MeterSpecialErrs[Key].Mse_PrjName)       //如果在记录里面找到该项目
        //                    {
        //                        if (!TmpOldKeys.Contains(Key))          //同时该项目关键字不能在旧关键字列表中存在
        //                        {
        //                            TmpOldKeys.Add(Key);                //在旧关键字列表中增加走字检定记录项目关键字
        //                            TmpNewKeys.Add("P_" + i.ToString());    //在新关键字列表中增加新方案项目下标。
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (TmpOldKeys.Count == 0)
        //        {
        //            for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //            {
        //                if (!DnbGroup.MeterGroup[i].YaoJianYn) continue;
        //                DnbGroup.MeterGroup[i].MeterSpecialErrs.Clear();       //清理特殊检定数据
        //                try
        //                {
        //                    DnbGroup.MeterGroup[i].MeterResults.Remove(((int)Comm.Enum.Cus_MeterResultPrjID.特殊检定).ToString());       //移除特殊检定结论
        //                }
        //                catch { }
        //            }
        //        }
        //        else
        //        {
        //            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr> SpItems = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr>();

        //            for (int i = 0; i < DnbGroup.MeterGroup.Count; i++)
        //            {
        //                if (!DnbGroup.MeterGroup[i].YaoJianYn) continue;
        //                if (DnbGroup.MeterGroup[i].MeterSpecialErrs.Count == 0) continue;

        //                SpItems = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr>();

        //                for (int j = 0; j < TmpOldKeys.Count; j++)
        //                {
        //                    try
        //                    {
        //                        SpItems.Add(TmpNewKeys[j], DnbGroup.MeterGroup[i].MeterSpecialErrs[TmpOldKeys[j]]);
        //                    }
        //                    catch { }
        //                }
        //                DnbGroup.MeterGroup[i].MeterSpecialErrs = SpItems;
        //                SpItems = null;
        //            }

        //        }

        //    }

        //    #endregion
        //}
        #endregion 注释的内容



        #endregion

        private void Init_advTreeStyle()
        {
            advTree_Prj.NodeSpacing = 2;
            advTree_Prj.Margin = new System.Windows.Forms.Padding(0);
            advTree_Prj.Padding = new System.Windows.Forms.Padding(0);
            advTree_Prj.GridRowLines = false;//行分割线
            //advTree_Prj.GridLinesColor = Color.White;//行分割线颜色
            advTree_Prj.Font = new Font("微软雅黑", 9);//"SimSun", 11
            advTree_Prj.ExpandBackColor = Color.LightGray;
            advTree_Prj.ColumnsVisible = false;//列头 
            SetTreeWidth();
            advTree_Prj.SelectionBoxStyle = eSelectionStyle.FullRowSelect;//选中节点模式

            advTree_Prj.NodesConnector.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;


        }
        private void InitDat_PrjRows(object obj)
        {
            if (MeterGroup.MeterGroup.Count == 0)           //如果没有电能表信息
                return;
            if (advTree_Prj.IsHandleCreated)
            {
                advTree_Prj.BeginInvoke(new Deg_CreateNewRowCell(CreateNewRowCell), MeterGroup.CheckPlan);
            }
            else
            {
                this.CreateNewRowCell(MeterGroup.CheckPlan);
            }
        }


        private delegate void Deg_CreateNewRowCell(List<object> CheckFaPrj);

        /// <summary>
        /// 创建一个新的表格行
        /// </summary>
        /// <param name="CheckFaPrj">检定方案项目集</param>
        /// <returns></returns>
        public void CreateNewRowCell(List<object> CheckFaPrj)
        {

            node_Prj.Tag = false;
            node_Prj.Nodes.Clear();
            node_Prj.Text = MeterGroup.FaName;
            node_Prj.CheckBoxVisible = true;
            for (int i = 0; i < CheckFaPrj.Count; i++)            //将检定项目插入到检定列表中
            {
                string _FaDescription = "";

                if (CheckFaPrj[i] is StPlan_PrePareTest)         //如果是预先调试项目
                    _FaDescription = "预先调试：" + ((StPlan_PrePareTest)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_YuRe)         //如果是预热试验项目
                    _FaDescription = "预热试验：" + ((StPlan_YuRe)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_WGJC)         //如果是外观检查试验项目
                    _FaDescription = "外观检查试验：" + ((StPlan_WGJC)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StInsulationParam)
                    _FaDescription = "工频耐压试验：" + ((StInsulationParam)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_QiDong)         //如果是起动试验项目
                    _FaDescription = "起动试验：" + ((StPlan_QiDong)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_QianDong)         //如果是潜动试验项目
                    _FaDescription = "潜动试验：" + ((StPlan_QianDong)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_WcPoint)         //如果是基本误差试验项目
                    _FaDescription = "基本误差试验：" + ((StPlan_WcPoint)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_ZouZi)         //如果是走字项目
                    _FaDescription = "走字试验：" + ((StPlan_ZouZi)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_ConnProtocol)         //如果是通讯协议检查试验
                    _FaDescription = ((StPlan_ConnProtocol)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_Dgn)         //如果是多功能项目
                    _FaDescription = "多功能试验：" + ((CLDC_DataCore.Struct.StPlan_Dgn)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_SpecalCheck)         //如果是特殊项目
                    _FaDescription = "影响量试验：" + ((StPlan_SpecalCheck)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is StPlan_Carrier)            //如果是载波项目
                    _FaDescription = "载波试验：" + ((StPlan_Carrier)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StErrAccord)        //如果是误差一致性项目
                    _FaDescription = "误差一致性：" + ((StErrAccord)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPowerConsume)          //如果是功耗项目
                    _FaDescription = "电气要求：" + ((StPowerConsume)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StDataSendForRelay)          //如果是数据转发
                    _FaDescription = "数据转发试验：" + ((StDataSendForRelay)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_Freeze)
                    _FaDescription = "冻结功能试验：" + ((StPlan_Freeze)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_Function)
                    _FaDescription = "智能表功能试验：" + ((StPlan_Function)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_CostControl)
                    _FaDescription = "费控功能试验：" + ((StPlan_CostControl)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_EventLog)
                    _FaDescription = "事件记录试验：" + ((StPlan_EventLog)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_Infrared)
                    _FaDescription = "红外数据比对试验：" + ((StPlan_Infrared)CheckFaPrj[i]).ToString();
                else if (CheckFaPrj[i] is CLDC_DataCore.Struct.StPlan_LoadRecord)
                    _FaDescription = "负荷记录试验：" + ((StPlan_LoadRecord)CheckFaPrj[i]).ToString();

                if (_FaDescription == "") continue;

                node_Prj.Tag = true;

                if (advTree_Prj.IsHandleCreated)
                {
                    advTree_Prj.BeginInvoke(new EventDat_PrjInsertRow(Dat_PrjInsertRow), _FaDescription, i);
                }
            }
            node_Prj.ExpandAll();
            node_Prj.Checked = true;

            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan != null)
            {
                for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan.Length; i++)
                {
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan[i] = true;
                }
            }

        }

        private delegate void EventDat_PrjInsertRow(string strName, int index);

        private void Dat_PrjInsertRow(string strName, int index)
        {
            string[] strGN = strName.Split('：');

            Node[] isFind = null;
            if (node_Prj.Nodes != null)
            {
                isFind = node_Prj.Nodes.Find("G" + strGN[0], false);
            }
            int L1 = 0;
            if (isFind == null || isFind.Length == 0)
            {
                Node ndh = new Node();
                ndh.Name = "G" + strGN[0];
                ndh.Text = strGN[0];
                ndh.CheckBoxVisible = true;
                ndh.Checked = true;
                L1 = node_Prj.Nodes.Add(ndh, eTreeAction.Expand);

                //给ndh添加右键目录
     
                    ndh.ContextMenu = MenuNode;
            }
            else
            {
                L1 = isFind[0].Index;
            }
            Node nd = new Node();
            nd.Name = "I" + strGN[1];
            nd.Text = strName.Replace(strGN[0], "");
            //lsx--子项检测点ndh添加右键目录
            nd.TagString = nd.Text;
            //nd.NodeMouseHover -= new EventHandler(nd_NodeMouseHover);
            //nd.NodeMouseHover += new EventHandler(nd_NodeMouseHover);//TODO:Tooltip
            nd.CheckBoxVisible = true;
            nd.Checked = true;
            nd.Tag = index;
            int RowIndex = node_Prj.Nodes[L1].Nodes.Add(nd, eTreeAction.Expand);               //插入项目描述，当前项目序号

                nd.ContextMenu = MenuNode;
            //node_Prj.Rows[RowIndex].Cells[0].Value = string.Format("{0}", RowIndex + 1).PadLeft(3, '0');
            //ElementStyle esa = new ElementStyle();

            //if ((RowIndex + 1) % 2 == 0)
            //{

            //    esa.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;

            //}
            //else
            //{

            //    esa.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
            //}
            //node_Prj.Nodes[L1].Nodes[RowIndex].Style = esa;//node行背景色
            //node_Prj.Rows[RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
            if (RowIndex == 0)
                ParentMain.ActiveIdByClick = node_Prj.Nodes[0].Index;

            //解决重新启动后无法继续上次检定的问题，，，2009-4-27 by：Winahriman
            if (MeterGroup.ActiveItemID < 0)
            {
                node_Prj.Nodes[0].Nodes[0].Checked = true;
                advTree_Prj.SelectedNode = node_Prj.Nodes[0].Nodes[0];
                //node_Prj.SelectNode(node_Prj.Nodes[0], eTreeAction.Mouse);        //如果项目数大于0的话，则自动选择第一个项目
            }
            else
            {

                if (MeterGroup.ActiveItemID == (int)node_Prj.Nodes[L1].Nodes[RowIndex].Tag)
                {
                    if (!node_Prj.Nodes[L1].Nodes[RowIndex].Checked)
                    {
                        node_Prj.Nodes[L1].Nodes[RowIndex].Checked = true;
                        advTree_Prj.SelectedNode = node_Prj.Nodes[L1].Nodes[RowIndex];
                        ParentMain.ActiveIdByClick = MeterGroup.ActiveItemID;

                    }
                }
            }
        }
        
        void nd_NodeMouseHover(object sender, EventArgs e)
        {
            //TreeNodeMouseEventArgs tm=e as TreeNodeMouseEventArgs;
            //superTooltip1.ShowTooltip(sender, new Point(tm.X, tm.Y));
        }

        private void advTree_Prj_AfterCheck(object sender, AdvTreeCellEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan == null)
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_CheckPlan.Count];
            if (node_Prj.Nodes.Count == 0) return;
            Node nd = (sender as AdvTree).SelectedNode;
            //if (nd.IsSelected == false) nd.Checked = true;
            //处理父节点全选，1级节点全选

            

            if (nd != null && nd.Level <= 2)
            {
                CheckState rdck = nd.CheckState;

                if (nd.Level == 2)
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan[(int)nd.Tag] = rdck == CheckState.Checked ? true : false;

                foreach (Node item in nd.Nodes)
                {
                    item.CheckState = rdck;

                    if (item.Level == 2)
                        CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan[(int)item.Tag] = rdck == CheckState.Checked ? true : false;

                    foreach (Node child in item.Nodes)
                    {
                        child.CheckState = rdck;

                        if (child.Level == 2)
                            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.m_bCheckPlan[(int)child.Tag] = rdck == CheckState.Checked ? true : false;
                    }
                }
            }
        }
        /// <summary>
        /// 项目选中触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void advTree_Prj_SelectionChanged(object sender, EventArgs e)
        {
            
            if (node_Prj.Nodes.Count == 0) return;
            Node nd = (sender as AdvTree).SelectedNode;

            if (nd == null) return;
            //if (nd.IsSelected == false) nd.Checked = true;

            //判断父节点和顶级节点
            if (nd.Tag == null || !CLDC_DataCore.Function.Number.IsIntNumber(nd.Tag.ToString())) return;
            //tag存方案序号
            CheckOrderID = (int)nd.Tag;
            if (Chk_AutoScroll.Checked)//自动滚动
            {
                node_Prj.SetChecked(true, eTreeAction.Expand);
            }

            #region ----------------加载数据表格部分仅限与停止检定状态--------------------

            if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
            {
                MeterGroup.ActiveItemID = CheckOrderID;
                ParentMain.ActiveIdByClick = MeterGroup.ActiveItemID;            
                this.BeginInvoke(new Action<string>(A =>
                {
                    this.RefreshRightGridView(CheckOrderID);
                }), "");
            }            
            #endregion
        }
        /// <summary>
        /// 真空丢失ID次数
        /// </summary>
        private int RunLostNum = 0;
        /// <summary>
        /// 刷新数据方法
        /// </summary>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="taiType">台体类型</param>
        /// <param name="taiId">台体编号</param>
        public override void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {

            Chk_TiaoBiao.Checked = (meterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.调表) == CLDC_Comm.Enum.Cus_CheckStaute.调表;//刷新调表检测状态
            if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)                                                       //停止检定的时候才允许操作
            {
                this.BeginInvoke(new Action<string>(A =>
                {
                    this.Tool_Control.Enabled = true;
                    this.TButtn_ChangeFA.Enabled = true;
                    this.TButton_Refresh.Enabled = true;
                    //this.TButton_Show.Enabled = true;
                }), "");
            }
            else
            {
                this.BeginInvoke(new Action<string>(A =>
                {
                    //this.Tool_Control.Enabled = false;
                    this.TButtn_ChangeFA.Enabled = false;
                    this.TButton_Refresh.Enabled = false;
                    //this.TButton_Show.Enabled = true;
                }), "");
            }
            base.RefreshData(meterGroup, taiType, taiId);

            if (advTree_Prj.IsHandleCreated)
            {
                advTree_Prj.BeginInvoke(new Deg_RefrashGrid(RefrashGrid));
            }

        }

        private delegate void Deg_RefrashGrid();

        private void RefrashGrid()
        {
            node_Prj.Enabled = true;

            if (IsChangedCheckPoint)         //如果是切点操作
            {
                if (this.RunLostNum < 3 && MeterGroup.ActiveItemID != advTree_Prj.SelectedIndex)     //如果在起点操作下，选中点！=当前检定点，则返回
                {
                    this.RunLostNum++;
                    return;
                }
                this.RunLostNum = 0;
                IsChangedCheckPoint = false;
            }
            //检定项目
            if (MeterGroup.ActiveItemID < 0) MeterGroup.ActiveItemID = 0;
            int GrpCount = node_Prj.Nodes.Count;
            bool FindSelect = true;
            for (int i = 0; i < GrpCount && FindSelect; i++)
            {
                int ChdCount = node_Prj.Nodes[i].Nodes.Count;
                for (int ci = 0; ci < ChdCount; ci++)
                {
                    if (null == node_Prj.Nodes[i].Nodes[ci].Tag || !CLDC_DataCore.Function.Number.IsIntNumber(node_Prj.Nodes[i].Nodes[ci].Tag.ToString()))
                    {
                        continue;
                    }
                    if (MeterGroup.ActiveItemID == (int)node_Prj.Nodes[i].Nodes[ci].Tag)
                    {
                        if (!node_Prj.Nodes[i].Nodes[ci].IsSelected)
                        {
                            //advTree_Prj.SelectedIndex = node_Prj.Nodes[i].Nodes[ci].Index;
                            advTree_Prj.SelectedNode = node_Prj.Nodes[i].Nodes[ci];
                            FindSelect = false;
                        }
                        break;
                    }
                }
            }
            node_Prj.Tag = true;             //允许刷新表格
            CheckOrderID = MeterGroup.ActiveItemID;

            this.RefreshRightGridView(CheckOrderID);

        }


        #region  -----------------------数据表单刷新-------------------------------

        private void RefreshRightGridView(int CheckOrderID)
        {
            //总览数据空间，1选项卡
            UserControl _BaseControl = null;
            //某项检定进度，检定数据控件，2、3选项卡
            UserControl _Control = null;
            /*
            	12/28/2009 15-08-18  By Niaoked
            	内容说明：
                对CheckorderID进行合法性检测。防止在参数录入时加载方案失败而影起的错误	
            */
            if (CheckOrderID < 0) CheckOrderID = 0;
            if (CheckOrderID >= MeterGroup.CheckPlan.Count)
            {
                MessageBoxEx.Show(this, "错误的检定项目号。请重新加载方案后再试！", "系统提示");
                MeterGroup.ActiveItemID = -1;
                return;
            }
            //if (this.splitContainer1.Panel2.Controls.Count > 0)
            {
                if (tabControlPanel_ShowALL.Controls.Count > 0)
                {
                    _BaseControl = (UserControl)tabControlPanel_ShowALL.Controls[0];
                }

                if (tabControlPanel_SomeOne.Controls.Count > 0)
                {
                    //_Control = (UserControl)this.splitContainer1.Panel2.Controls[0];
                    _Control = (UserControl)tabControlPanel_SomeOne.Controls[0];
                }
            }
            #region --------总览列表表格-----------
            //if (tabControlPanel_ShowALL.Visible == true)
            {
                if (_BaseControl != null)
                {
                    if (!(_BaseControl is CheckDataView.CheckDataShowALL))
                    {
                        this.tabControlPanel_ShowALL.Controls.Clear();
                        _BaseControl = null;
                    }
                }
                if (_BaseControl == null)
                {
                    _BaseControl = new CheckDataView.CheckDataShowALL(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);

                    ((CheckDataView.CheckDataShowALL)_BaseControl).SelectionChanged += new EventHandler(CheckBase_SelectionChanged);

                    #region 注册刷新树事件
                    
                    (_BaseControl as CheckDataView.CheckDataShowALL).EventRefreshSummary -= ShowResultInTree;
                    (_BaseControl as CheckDataView.CheckDataShowALL).EventRefreshSummary += ShowResultInTree;
                    #endregion 注册刷新树事件

                    ((CheckDataView.CheckDataShowALL)_BaseControl).RefreshDataALL(MeterGroup);
                    this.tabControlPanel_ShowALL.Controls.Add(_BaseControl);
                    _BaseControl.Dock = DockStyle.Fill;
                    _BaseControl.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckDataShowALL)_BaseControl).RefreshData(MeterGroup, CheckOrderID);
                //((CheckDataView.CheckDataShowALL)_BaseControl).SelectRowIndex = CheckOrderID;// this.SelectedBwh;
            }

            #endregion
            #region 刷新各种数据表格
            #region --------判断是否是预先调试表格，加载预先调试表格-------
            if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_PrePareTest)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckPrePareTest))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckPrePareTest(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckPrePareTest)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckPrePareTest.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckPrePareTest)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckPrePareTest.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckPrePareTest)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckPrePareTest)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion
            #region --------判断是否是预热试验表格，加载预热试验表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_YuRe)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckYuRe))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckYuRe(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckYuRe)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckYuRe.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckYuRe)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckYuRe.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckYuRe)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckYuRe)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = true;
            }
            #endregion

            #region --------判断是否是直观检查表格，加载直观检查表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_WGJC)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckWGJC))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckWGJC(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckWGJC)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckWGJC.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckWGJC)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckWGJC.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    ((CheckDataView.CheckWGJC)_Control).RefreshRightGridView += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckWGJC.Evt_RefreshRightGridView(RefreshRightGridView);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckWGJC)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckWGJC)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }

            #endregion

            #region --------判断是否是起动试验表格，加载起动试验表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_QiDong)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckQiDong))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckQiDong(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckQiDong)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckQiDong.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckQiDong)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckQiDong.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckQiDong)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckQiDong)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #region --------判断是否是潜动表格，加载潜动表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_QianDong)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckQianDong))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckQianDong(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckQianDong)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckQianDong.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckQianDong)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckQianDong.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckQianDong)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckQianDong)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }

            #endregion

            #region --------判断是否是误差表格，加载误差表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_WcPoint)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckErr))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckErr(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckErr)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckErr.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckErr)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckErr.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckErr)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckErr)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = true;
            }
            #endregion

            #region --------判断是否是走字表格，加载走字表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_ZouZi)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckZouZi))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckZouZi(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckZouZi)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckZouZi.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckZouZi)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckZouZi.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckZouZi)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckZouZi)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #region --------判断是否是多功能表格，加载多功能表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Dgn)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckDgn))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckDgn(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckDgn)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckDgn.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckDgn)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckDgn.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckDgn)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckDgn)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }

            #endregion

            #region --------判断是否是通讯协议检查试验，加载通讯协议检查试验表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_ConnProtocol)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckConnProtocol))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckConnProtocol(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckConnProtocol)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckConnProtocol.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckConnProtocol)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckConnProtocol.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckConnProtocol)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckConnProtocol)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }

            #endregion

            #region --------判断是否是特殊检定表格，加载特殊检定表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_SpecalCheck)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckTeShu))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckTeShu(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckTeShu)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckTeShu.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckTeShu)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckTeShu.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckTeShu)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckTeShu)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = true;
            }
            #endregion

            #region --------判断是否是载波表格，加载载波表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is StPlan_Carrier)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckCarrier))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckCarrier(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckCarrier)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckCarrier.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckCarrier)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckCarrier.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckCarrier)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckCarrier)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;

            }
            #endregion

            #region --------判断是否是误差一致性表格，加载误差一致性表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StErrAccord)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckErrAccord))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    //重新加载表格
                    _Control = new CheckDataView.CheckErrAccord(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckErrAccord)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckErrAccord.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckErrAccord)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckErrAccord.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckErrAccord)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckErrAccord)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = true;
            }
            #endregion

            #region --------判断是否是功耗表格，加载功耗表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPowerConsume)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckPowerConsume))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckPowerConsume(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckPowerConsume)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckPowerConsume.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckPowerConsume)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckPowerConsume.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckPowerConsume)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckPowerConsume)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;

            }
            #endregion

            #region --------判断是否是智能表功能表格，加载智能表功能表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Function)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckFunction))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckFunction(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckFunction)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckFunction.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckFunction)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckFunction.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckFunction)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckFunction)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #region --------判断是否是费控功能表格，加载费控功能表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_CostControl)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckCostControl))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckCostControl(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckCostControl)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckCostControl.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckCostControl)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckCostControl.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckCostControl)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckCostControl)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }

            #endregion

            #region --------判断是否是事件记录功能表格，加载事件记录功能表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_EventLog)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckEventLog))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckEventLog(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckEventLog)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckEventLog.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckEventLog)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckEventLog.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckEventLog)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckEventLog)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }

            #endregion

            #region --------判断是否是冻结表格，加载冻结测试表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Freeze)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckFreeze))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckFreeze(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckFreeze)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckFreeze.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckFreeze)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckFreeze.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckFreeze)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckFreeze)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #region --------判断是否是数据转发表格，加载数据转发测试表格-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StDataSendForRelay)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckDataSend))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckDataSend(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckDataSend)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckDataSend.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckDataSend)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckDataSend.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckDataSend)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckDataSend)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #region --------判断是否是耐压试验表格，加载耐压试验测试数据-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StInsulationParam)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckInsulation))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckInsulation(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckInsulation)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckInsulation.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckInsulation)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckInsulation.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckInsulation)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckInsulation)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #region --------判断是否是耐压试验表格，加载耐压试验测试数据-------
            else if (base.MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_LoadRecord)
            {
                if (_Control != null)
                {
                    if (!(_Control is CheckDataView.CheckLoadRecord))
                    {
                        this.tabControlPanel_SomeOne.Controls.Clear();
                        _Control = null;
                    }
                }
                if (_Control == null)
                {
                    _Control = new CheckDataView.CheckLoadRecord(base.ParentMain, base.MeterGroup, CheckOrderID, TaiType, TaiId);
                    ((CheckDataView.CheckLoadRecord)_Control).SetDnbInfoViewData += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckLoadRecord.Evt_SetDnbInfoViewData(SetDnbInfoViewData);
                    ((CheckDataView.CheckLoadRecord)_Control).GridSelectRowIndexChanged += new CLDC_MeterUI.UI_Detection_New.CheckDataView.CheckLoadRecord.Evt_GridSelectRowIndexChanged(CheckBase_GridSelectRowIndexChanged);
                    this.tabControlPanel_SomeOne.Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                ((CheckDataView.CheckLoadRecord)_Control).RefreshData(MeterGroup, CheckOrderID);
                ((CheckDataView.CheckLoadRecord)_Control).SelectRowIndex = this.SelectedBwh;
                Chk_TiaoBiao.Visible = false;
            }
            #endregion

            #endregion
        }

        void CheckBase_SelectionChanged(object sender, EventArgs e)
        {
            int Row;
            if (node_Prj.Nodes.Count == 0) return;
            if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
            {

                CheckDataView.CheckDataShowALL temp = (CheckDataView.CheckDataShowALL)sender;
                Row = temp.GetRowIndex;
                if (Row >= 0)
                {
                    try
                    {
                        int Count = 0;
                        int LastCount = 0;
                        for (int i = 0; i < node_Prj.Nodes.Count; i++)
                        {
                            Count += node_Prj.Nodes[i].Nodes.Count;
                            if (Row < Count)
                            {
                                Node n = node_Prj.Nodes[i].Nodes[Row - LastCount];
                                advTree_Prj.SelectedNode = n;
                                break;
                            }
                            LastCount = Count;
                        }
                    }
                    catch (Exception ex)
                    { }

                }
            }
        }


        /// <summary>
        /// 表位号选中变更事件
        /// </summary>
        /// <param name="RowIndex"></param>
        private void CheckBase_GridSelectRowIndexChanged(int RowIndex)
        {
            this.SelectedBwh = RowIndex;
        }

        #endregion





        /// <summary>
        /// 检定项目行单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree_Prj_NodeMouseUp(object sender, TreeNodeMouseEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
            {
                if (e.Button == MouseButtons.Left )
                {
                    return;
                }
            }
 
            advTree_Prj.ContextMenuStrip = null;
            if (e.Node == null) return;
            if (e.Node.Tag == null) return;

            if (e.Node.Tag.GetType().Equals(typeof(bool))) return;
            node_Prj.Tag = true;

            (sender as AdvTree).SelectNode(e.Node, eTreeAction.Expand);
            //if (CheckOrderID == MeterGroup.ActiveItemID)           //如果是相同点则不处理
            //{
            //    return;
            //}
            ParentMain.ActiveIdByClick = (int)e.Node.Tag;


            if (this.IsHandleCreated)
            {
                //advTree_Prj.ContextMenuStrip = MenuNode;
                MeterGroup.ActiveItemID = CheckOrderID;
                //this.BeginInvoke(new Deg_ChangeCheckPoint(ChangeCheckPoint), ParentMain.ActiveIdByClick, TaiType, TaiId);
            }


        }
        /// <summary>
        /// 切点委托
        /// </summary>
        /// <param name="ActiveID"></param>
        /// <param name="intTaiType"></param>
        /// <param name="intTaiID"></param>
        private delegate void Deg_ChangeCheckPoint(int ActiveID, int intTaiType, int intTaiID);

        /// <summary>
        /// 切点操作委托方法
        /// </summary>
        /// <param name="ActiveID"></param>
        /// <param name="intTaiType"></param>
        /// <param name="intTaiID"></param>
        private void ChangeCheckPoint(int ActiveID, int intTaiType, int intTaiID)
        {
            if (ParentMain.Evt_OnChangePoint != null)
            {
                if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)//fjk 
                {
                    CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在执行切点操作...");
                    if (!ParentMain.Evt_OnChangePoint(ActiveID, intTaiType, intTaiID))
                    {
                        CLDC_DataCore.Function.TopWaiting.HideWaiting();
                        MessageBoxEx.Show(this, "操作失败!");
                        return;
                    }
                    else
                    {
                        IsChangedCheckPoint = true;
                        node_Prj.Enabled = false;
                    }
                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                }
            }
            else
            {
                MessageBoxEx.Show(this, "没有接收切点事件!");
            }
        }



        /// <summary>
        /// 获取数据表格当前选中的行号
        /// </summary>
        /// <returns></returns>
        private int getSelectRowIndex()
        {
            if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;

            UserControl _Control = (UserControl)this.tabControlPanel_SomeOne.Controls[0];

            if (_Control is CheckDataView.CheckYuRe)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckYuRe)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckQiDong)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckQiDong)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckQianDong)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckQianDong)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckErr)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckErr)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckZouZi)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckZouZi)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckDgn)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckDgn)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckTeShu)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckTeShu)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckCarrier)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckCarrier)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckErrAccord)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckErrAccord)_Control).SelectRowIndex;
            }


            if (_Control is CheckDataView.CheckFunction)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckFunction)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckEventLog)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckEventLog)_Control).SelectRowIndex;
            }
            if (_Control is CheckDataView.CheckCostControl)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckCostControl)_Control).SelectRowIndex;
            }

            if (_Control is CheckDataView.CheckFreeze)
            {
                if (this.splitContainer1.Panel2.Controls.Count == 0) return 0;
                return ((CheckDataView.CheckFreeze)_Control).SelectRowIndex;
            }
            return 0;
        }



        #region ----------被检表参数信息设置窗体相关操作-----------

        /// <summary>
        /// 被检表信息图片按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDnbInfo_Click(object sender, EventArgs e)
        {
            if (_DnbInfoView != null)
            {
                _DnbInfoView.Close();
                return;
            }

            if (_ControlMsg != null)
            {
                _ControlMsg.Close();
                //_ControlMsg.Dispose();
            }
            if (_CheckParm != null)
            {
                _CheckParm.Close();
            }
            if (_DnbDataView != null)
            {
                _DnbDataView.Close();
            }
            _DnbInfoView = new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_DnbInfoView();


            //匿名委托方法，在窗体释放的时候将该静态对象设置为空
            _DnbInfoView.Disposed += delegate(object vsender, EventArgs ve)
                                    {
                                        _DnbInfoView = null;
                                    };
            _DnbInfoView.ValueChanged += new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_DnbInfoView.Event_ValueChanged(DnbInfoView_ValueChanged);

            Point FrmPoint = new Point();

            FrmPoint = ButtonDnbInfo.PointToScreen(new Point(ButtonDnbInfo.Left, ButtonDnbInfo.Top));

            FrmPoint.X -= _DnbInfoView.Width;

            FrmPoint.Y = advTree_Prj.PointToScreen(new Point(advTree_Prj.Left, advTree_Prj.Top)).Y;

            _DnbInfoView.Height = splitContainer1.Height;



            _DnbInfoView.SetData(MeterGroup.MeterGroup[this.getSelectRowIndex()], MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定 ? true : false);

            _DnbInfoView.Show();

            _DnbInfoView.Location = FrmPoint;

            _DnbInfoView.TopMost = true;

        }


        /// <summary>
        /// 电能表信息窗体可编辑值发生变化事件
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="Value"></param>
        /// <param name="Bwh"></param>
        private void DnbInfoView_ValueChanged(string PropertyName, object Value, int Bwh)
        {
            if (ParentMain.Evt_DataInfoChanged != null)
            {
                ParentMain.Evt_DataInfoChanged(PropertyName, Value, Bwh, base.TaiType, base.TaiId);
            }
        }
        /// <summary>
        /// 设置电能表信息窗体里面的信息值
        /// </summary>
        /// <param name="Bwh"></param>
        private void SetDnbInfoViewData(int Bwh)
        {
            if (_DnbInfoView != null)
            {
                _DnbInfoView.TopMost = true;
                _DnbInfoView.SetData(MeterGroup.MeterGroup[Bwh], MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定 ? true : false);
            }

        }

        #endregion

        #region ------------消息记录窗体相关操作-------------
        /// <summary>
        /// 检定过程的日志按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogButton_Click(object sender, EventArgs e)
        {
            if (_ControlMsg != null)
            {
                _ControlMsg.Close();
                return;
            }

            if (_DnbInfoView != null)
            {
                _DnbInfoView.Close();
            }
            if (_CheckParm != null)
            {
                _CheckParm.Close();
            }
            if (_DnbDataView != null)
            {
                _DnbDataView.Close();
            }

            _ControlMsg = new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_ControlMessage();

            //匿名委托方法，在窗体释放的时候将该静态对象设置为空
            _ControlMsg.Disposed += delegate(object vsender, EventArgs ve)
                                    {
                                        _ControlMsg = null;
                                    };
            Point FrmPoint = new Point();

            FrmPoint = ButtonDnbData.PointToScreen(new Point(ButtonDnbData.Left, ButtonDnbData.Top));

            FrmPoint.X -= _ControlMsg.Width;

            FrmPoint.Y = advTree_Prj.PointToScreen(new Point(advTree_Prj.Left, advTree_Prj.Top)).Y;

            _ControlMsg.Height = splitContainer1.Height;

            _ControlMsg.Show();

            _ControlMsg.Location = FrmPoint;

            _ControlMsg.TopMost = true;
        }

        /// <summary>
        /// 设置检定信息消息
        /// </summary>
        /// <param name="MsgString"></param>
        public override void SetCheckMessage(string MsgString)
        {
            if (_ControlMsg != null)
            {
                _ControlMsg.SetData(MsgString);
            }
        }
        #endregion

        #region --------方案参数设置窗体相关操作-----------

        /// <summary>
        /// 设置检定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlPanel_Click(object sender, EventArgs e)
        {


            if (_CheckParm != null)
            {
                _CheckParm.Close();
                return;
            }

            if (_DnbInfoView != null)
            {
                _DnbInfoView.Close();
            }
            if (_ControlMsg != null)
            {
                _ControlMsg.Close();
            }
            if (_DnbDataView != null)
            {
                _DnbDataView.Close();
            }
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在准备参数数据...");

            _CheckParm = new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_CheckParamView(MeterGroup);
            _CheckParm.SendData += new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_CheckParamView.Event_SendData(CheckParm_SendData);
            //匿名委托方法，在窗体释放的时候将该静态对象设置为空
            _CheckParm.Disposed += delegate(object vsender, EventArgs ve)
                                    {
                                        _CheckParm = null;
                                    };
            Point FrmPoint = new Point();

            FrmPoint = ButtonDnbData.PointToScreen(new Point(ButtonDnbData.Left, ButtonDnbData.Top));

            FrmPoint.X -= splitContainer1.Panel2.Width; //_CheckParm.Width;

            FrmPoint.Y = advTree_Prj.PointToScreen(new Point(advTree_Prj.Left, advTree_Prj.Top)).Y;

            _CheckParm.Height = splitContainer1.Height;
            _CheckParm.Width = splitContainer1.Panel2.Width;
            _CheckParm.Show();

            _CheckParm.Location = FrmPoint;

            CLDC_DataCore.Function.TopWaiting.HideWaiting();

            System.Threading.Thread.Sleep(10);

            _CheckParm.TopMost = true;
            _CheckParm.Select();
        }

        private void CheckParm_SendData()
        {
            if (ParentMain.FaParmChanged != null)
            {
                ParentMain.FaParmChanged(MeterGroup, TaiType, TaiId);
            }
        }

        #endregion


        /// <summary>
        /// 如果右侧窗体在显示状态时，需要根据主窗体状态变化而变化
        /// </summary>
        /// <param name="Hide"></param>
        public void SetViewFormState(bool Hide)
        {
            if (_DnbInfoView != null)
            {
                _DnbInfoView.Visible = Hide;
            }
            if (_ControlMsg != null)
            {
                _ControlMsg.Visible = Hide;
            }
            if (_CheckParm != null)
            {
                _CheckParm.Visible = Hide;
            }
            if (_DnbDataView != null)
            {
                _DnbDataView.Visible = Hide;
            }
        }




        public void CloseViewForm()
        {
            if (_DnbInfoView != null)
            {
                _DnbInfoView.Close();
                _DnbInfoView = null;
            }
            if (_ControlMsg != null)
            {
                _ControlMsg.Close();
                _ControlMsg = null;
            }
            if (_CheckParm != null)
            {
                _CheckParm.Close();
                _CheckParm = null;
            }
            if (_DnbDataView != null)
            {
                _DnbDataView.Close();
                _DnbDataView = null;
            }
        }

        /// <summary>
        /// 控件大小发生变化时，如果右侧辅助窗体在显示状态的话则同样需要改变尺寸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBase_SizeChanged(object sender, EventArgs e)
        {
            if (!advTree_Prj.IsHandleCreated) return;       //如果还没有创建句柄则自动退出

            Point FrmPoint = new Point();

            FrmPoint = ButtonDnbData.PointToScreen(new Point(ButtonDnbData.Left, ButtonDnbData.Top));

            FrmPoint.Y = advTree_Prj.PointToScreen(new Point(advTree_Prj.Left, advTree_Prj.Top)).Y;

            if (_ControlMsg != null)        //消息窗体
            {
                _ControlMsg.Height = splitContainer1.Height;
                FrmPoint.X -= _ControlMsg.Width;
                _ControlMsg.Location = FrmPoint;
                _ControlMsg.TopMost = true;
            }

            if (_DnbInfoView != null)           //电能表基本信息
            {
                _DnbInfoView.Height = splitContainer1.Height;
                FrmPoint.X -= _DnbInfoView.Width;
                _DnbInfoView.Location = FrmPoint;
                _DnbInfoView.TopMost = true;
            }
            if (_CheckParm != null)             //方案参数窗体
            {
                FrmPoint.X -= splitContainer1.Panel2.Width;
                _CheckParm.Height = splitContainer1.Height;
                _CheckParm.Width = splitContainer1.Panel2.Width;
                _CheckParm.Location = FrmPoint;
                _CheckParm.TopMost = true;


            }
            if (_DnbDataView != null)
            {
                FrmPoint.X -= splitContainer1.Panel2.Width;
                _DnbDataView.Height = splitContainer1.Height;
                _DnbDataView.Width = splitContainer1.Panel2.Width;
                _DnbDataView.Location = FrmPoint;
                _DnbDataView.TopMost = true;
            }
            SetTreeWidth();
        }

        #region 要求录入起止码方法

        public bool Fun_InputZZNumber(bool IsStartNumber)
        {
            if (this.splitContainer1.Panel2.Controls.Count == 0) return false;

            Control _Control = this.splitContainer1.Panel2.Controls[0];

            if (_Control is CheckDataView.CheckZouZi)
            {
                return ((CheckDataView.CheckZouZi)_Control).Fun_InputZZNumber(IsStartNumber);
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// 走字起止码录入确认事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void Btn_DoComplated_Click(object sender, EventArgs e)
        {
            //    if (!Btn_DoComplated.Visible) return;

            //    if (this.splitContainer1.Panel2.Controls.Count == 0) return;

            //    Control _Control = this.splitContainer1.Panel2.Controls[0];

            //    if (_Control is CheckDataView.CheckZouZi)
            //    {
            //        ((CheckDataView.CheckZouZi)_Control).Btn_DoComplated_Click(Btn_DoComplated);
            //    }

        }

        #endregion

        #region 显示业务描述窗体
        /// <summary>
        /// 根据节点的文本显示规程的描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree_Prj_NodeMouseEnter(object sender, TreeNodeMouseEventArgs e)
        {
            string stringDisplayHelp = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_DISPLAY_CHECKDESCRIPTION, "否");
            if (stringDisplayHelp == "是")
            {
                //获取第一层的节点的名称
                Node node = e.Node;
                if (node.Level == 0)
                    return;
                else if (node.Level == 2)
                {
                    if (node.Parent.Text != "多功能试验")
                        node = e.Node.Parent;
                }

                ShowCheckStep.Instance.Display(node.Text);
            }
        }

        //鼠标离开后显示隐藏窗体
        private void advTree_Prj_NodeMouseLeave(object sender, TreeNodeMouseEventArgs e)
        {
            string stringDisplayHelp = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_DISPLAY_CHECKDESCRIPTION, "否");
            if (stringDisplayHelp == "是")
            {
                ShowCheckStep.Instance.HideForm();
            }
        }
        #endregion 显示业务描述窗体


        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SetTreeWidth();
        }

        private void 执行当前检测项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (this.IsHandleCreated)
            {
                // MeterGroup.ActiveItemID = CheckOrderID;
                this.BeginInvoke(new Deg_ChangeCheckPoint(ChangeCheckPoint), ParentMain.ActiveIdByClick, TaiType, TaiId);

            }
        }

        #region 移动测试项目
        //上移
        private void menuItemUp_Click_1(object sender, EventArgs e)
        {
            Node node = advTree_Prj.SelectedNode;
            if (node == null)
                return;
            if (node.Level == 0)
                return;
            if (node.Level == 2)
                node = node.Parent;
            int index = node.Index;
            if (index <= 0)
                return;

            Node nodeParent = node.Parent;
            nodeParent.Nodes.Remove(node);
            nodeParent.Nodes.Insert(index - 1, node);

            advTree_Prj.SelectedNode = node;

            SaveCheckPlan();
        }

        //下移
        private void menuItemDown_Click_1(object sender, EventArgs e)
        {
            Node node = advTree_Prj.SelectedNode;
            if (node == null)
                return;
            if (node.Level == 0)
                return;
            if (node.Level == 2)
                node = node.Parent;
            int index = node.Index;
            if (index >= node.Parent.Nodes.Count - 1)
                return;

            Node nodeParent = node.Parent;
            nodeParent.Nodes.Remove(node);
            nodeParent.Nodes.Insert(index + 1, node);

            advTree_Prj.SelectedNode = node;

            SaveCheckPlan();

        }

        //默认排序标记
        private bool boolSortDefault = false;
        //恢复默认排序
        private void menuItemDefault_Click_1(object sender, EventArgs e)
        {
            boolSortDefault = true;
            SaveCheckPlan();
            boolSortDefault = false;
            TButton_Refresh_Click(sender, e);
        }

        // 保存测试方案
        private void SaveCheckPlan()
        {
            //方案列表
            List<int> listPlanID = new List<int>();

            // 获取方案索引表
            Dictionary<string, int> planDictionary = new Dictionary<string, int>();
            for (int i = 1; i < 18; i++)
            {
                planDictionary.Add(((CLDC_Comm.Enum.Cus_FAGroup)i).ToString(), i);
            }

            //根据索引表获取方案列表
            foreach (Node node in node_Prj.Nodes)
            {
                if (planDictionary.ContainsKey(node.Text))
                    listPlanID.Add(planDictionary[node.Text]);
                else if (node.Text.Contains("通讯协议检查试验"))
                    listPlanID.Add(planDictionary["通讯协议检查试验"]);
            }

            //判断是否采用默认排序
            if (boolSortDefault)
                listPlanID.Sort();

            //保存方案列表
            string planName = node_Prj.Text;
            string filePath = string.Format(@"{0}\SX\Group\{1}.xml", Application.StartupPath, planName);

            CLDC_DataCore.DataBase.clsXmlControl xmlNode = new CLDC_DataCore.DataBase.clsXmlControl();
            xmlNode.appendchild("", "FAGroup", "Name", planName);
            for (int i = 0; i < listPlanID.Count; i++)
            {
                xmlNode.appendchild("", "R", "Name", listPlanID[i].ToString(), "Index", i.ToString(), planName);
            }

            xmlNode.SaveXml(filePath);
        }
        #endregion 移动测试项目

        #region 在方案树上面显示测试结果
        //测试成功的图片
        private Image imagePass = Image.FromFile(string.Format(@"{0}/Pic/UI/pass.gif", Application.StartupPath));
        //测试失败时的图片
        private Image imageFail = Image.FromFile(string.Format(@"{0}/Pic/UI/fail.png", Application.StartupPath));
        private ElementStyle styleCell = new ElementStyle();
        /// <summary>
        /// 在方案树上面显示测试结果
        /// </summary>
        /// <param name="nodeindex">结点</param>
        /// <param name="testResult">测试结论，1：通过；2：失败；0或其它：无结论</param>
        /// <param name="failCount">测试失败的电表数量</param>
        private void ShowResultInTree(int nodeIndex, int testResult, int failCount)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(v =>
            {
                this.BeginInvoke(new Action<string>(A =>
                {
                    #region 根据序号获取Node
                    if (advTree_Prj.Nodes == null || advTree_Prj.Nodes.Count < 1)
                        return;

                    int index = 0;
                    Node node = null;
                    Node node1 = advTree_Prj.Nodes[0];

                    foreach (Node node2 in node1.Nodes)
                    {
                        if (node != null)
                            break;
                        foreach (Node node3 in node2.Nodes)
                        {
                            if (index == nodeIndex)
                            {
                                node = node3;
                                break;
                            }
                            index++;
                        }
                    }

                    if (node == null)
                        return;
                    #endregion 根据序号获取Node

                    #region 设置Node的测试结论
                    Cell cell = new Cell();
                    while (node.Cells.Count < 2)
                    {
                        node.Cells.Add(new Cell() { });
                    }

                    cell = node.Cells[1];
                    if (testResult == 1)
                    {
                        cell.Images.Image = imagePass;
                        cell.ImageAlignment = eCellPartAlignment.NearCenter;
                        cell.Text = string.Empty;
                    }
                    else if (testResult == 2)
                    {
                        cell.Images.Image = imageFail;
                        cell.ImageAlignment = eCellPartAlignment.NearCenter;
                        cell.Text = failCount.ToString();
                        styleCell.TextColor = Color.Red;
                        cell.StyleNormal = styleCell;
                    }
                    else
                    {
                        cell.Images.Image = null;
                        cell.Text = string.Empty;
                    }
                    #endregion 设置Node的测试结论

                    #region 父节点测试结论
                    Node nodeParent = node.Parent;
                    if (nodeParent != null)
                    {
                        if (testResult == 2)
                        {
                            SetParentNodeTestResult(nodeParent, false);
                            return;
                        }
                        int rc = 0;
                        int rcb = 0;
                        foreach (Node nodeChild in nodeParent.Nodes)
                        {
                            if (nodeChild.Cells.Count > 1)
                            {
                                if (nodeChild.Cells[1] != null)
                                {
                                    rcb++;
                                    if (nodeChild.Cells[1].Images.Image == imageFail)// || nodeChild.Cells[1].Images.Image==null 
                                    {
                                        SetParentNodeTestResult(nodeParent, false);
                                        return;
                                    }
                                    if (nodeChild.Cells[1].Images.Image == null)
                                    {
                                        rc++;
                                    }
                                }
                            }
                        }
                        if (rc != rcb)
                        {
                            SetParentNodeTestResult(nodeParent, true);
                        }
                    }
                    #endregion 父节点测试结论
                }), "");
            }));
        }

        /// <summary>
        /// 设置父节点测试
        /// </summary>
        private void SetParentNodeTestResult(Node nodeParent,bool result)
        {
            Cell cell = new Cell();
            while (nodeParent.Cells.Count < 2)
            {
                nodeParent.Cells.Add(new Cell() { });
            }

            cell = nodeParent.Cells[1];
            if (result)
            {
                cell.Images.Image = imagePass;
                cell.ImageAlignment = eCellPartAlignment.NearCenter;
                cell.Text = string.Empty;
            }
            else
            {
                cell.Images.Image = imageFail;
                cell.ImageAlignment = eCellPartAlignment.NearCenter;
                styleCell.TextColor = Color.Red;
                cell.StyleNormal = styleCell;
            }
        }

        /// <summary>
        /// 设置方案总览的外观
        /// </summary>
        private void SetTreeWidth()
        {
            advTree_Prj.Width = splitContainer1.Panel1.Width - 10;
            if (advTree_Prj.Columns.Count >= 2)
            {
                if (advTree_Prj.VScrollBar == null)
                {
                    advTree_Prj.Columns[0].Width.Absolute = splitContainer1.Panel1.Width - advTree_Prj.Columns[1].Width.Absolute - 40;
                }
                else
                {
                    advTree_Prj.Columns[0].Width.Absolute = splitContainer1.Panel1.Width - advTree_Prj.Columns[1].Width.Absolute - 40 - advTree_Prj.VScrollBar.Width;
                }
            }
        }
        /// <summary>
        /// 设置鼠标滚动条出现时的treeview宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree_Prj_Layout(object sender, LayoutEventArgs e)
        {
            if (advTree_Prj.VScrollBar != null)
            {
                SetTreeWidth();
            }
        }
        #endregion 在方案树上面显示测试结果

        private void ButtonDnbData_Click(object sender, EventArgs e)
        {
            if (_CheckParm != null)
            {
                _CheckParm.Close();
                
            }
            if (_DnbInfoView != null)
            {
                _DnbInfoView.Close();
            }
            if (_ControlMsg != null)
            {
                _ControlMsg.Close();
            }
            if (_DnbDataView != null)
            {
                _DnbDataView.Close();
                return;
            }

            _DnbDataView = new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_DnbDataView(MeterGroup);
            _DnbDataView.SendData += new CLDC_MeterUI.UI_Detection_New.ShowDataView.Frm_DnbDataView.Event_SendData(CheckParm_SendData);
            //匿名委托方法，在窗体释放的时候将该静态对象设置为空
            _DnbDataView.Disposed += delegate(object vsender, EventArgs ve)
            {
                _DnbDataView = null;
            };
            Point FrmPoint = new Point();

            FrmPoint = ButtonDnbData.PointToScreen(new Point(ButtonDnbData.Left, ButtonDnbData.Top));

            FrmPoint.X -= splitContainer1.Panel2.Width; //_CheckParm.Width;

            FrmPoint.Y = advTree_Prj.PointToScreen(new Point(advTree_Prj.Left, advTree_Prj.Top)).Y;

            _DnbDataView.Height = splitContainer1.Height;
            _DnbDataView.Width = splitContainer1.Panel2.Width;
            _DnbDataView.Show();

            _DnbDataView.Location = FrmPoint;

            System.Threading.Thread.Sleep(10);

            _DnbDataView.TopMost = true;
            _DnbDataView.Select();
        }
    }
}

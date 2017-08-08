using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using System.Threading;
//using CLDC_ResourceManager;

namespace CLDC_MeterUI.UI_Detection_New
{
    #region 声明委托
    /// <summary>
    /// 方案参数变化事件
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool EventFaParmChanged(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);



    /// <summary>
    /// 菜单条接换事件 (主程序接收到事件以后填充参数值)
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    /// <returns>操作是否成功</returns>
    public delegate bool EventOnTabChanged(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, ref int taiType, ref int taiId);

    /// <summary>
    /// 是否要检事件
    /// </summary>
    /// <param name="taiType">台体类型</param>
    /// <param name="taiId">台体ID</param>
    /// <param name="bwh">表位号</param>
    /// <param name="YaoJianYn">是否要检</param>
    public delegate bool EventOnYaoJianChanged(int taiType, int taiId, int bwh, bool YaoJianYn);

    /// <summary>
    /// 终止检定
    /// </summary>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    public delegate bool EventOnCheckStop(int taiType, int taiId);

    /// <summary>
    /// 暂停检定
    /// </summary>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    public delegate bool EventOnCheckPause(int taiType, int taiId);

    /// <summary>
    /// 调表
    /// </summary>
    /// <param name="IsTiaoBiao">true(调表)，false(停止调表)</param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    public delegate bool EventOnCheckAdjust(bool IsTiaoBiao, int taiType, int taiId);
    /// <summary>
    /// 编程提示
    /// </summary>
    /// <param name="IsTip">true提示，false不提示</param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool EventOnProgrammingTipAdjust(bool IsTip, int taiType, int taiId);
    /// <summary>
    /// 录入参数完毕
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    public delegate bool Event_InputParam_OnOk(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);


    /// <summary>
    /// 从MIS中下载信息
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_DownMeterInfoFromMis(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// 从MIS中下载方案
    /// </summary>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_DownSchemeInfoFromMis(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);
    /// <summary>
    /// 读取参数
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_ReadPara(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup,int taiType,int taiId);
    /// <summary>
    /// 方案配置完毕(新的UI不使用该事件)
    /// </summary>
    public delegate bool Event_LoadFA_OnOk(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// 跳点事件
    /// </summary>
    /// <param name="prjid"></param>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    /// <returns></returns>
    public delegate bool Event_OnChangePoint(int prjindex, int taiType, int taiId);

    /// <summary>
    /// 单步检定
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnStepStart(int CheckId, int taiType, int taiId);

    /// <summary>
    /// 存档事件
    /// </summary>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnAuditingSave(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// 准备存档事件
    /// </summary>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnAuditingSaveBefore(int taiType, int taiId);

    /// <summary>
    /// 录入起码止码完毕
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnInputNumberEnd(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// 挂新表
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnHangUpNewMeter(int taiType, int taiId);


    /// <summary>
    /// 数据项目值发生变化事件（该事件用于基本信息UI部分修改与底层同步）
    /// </summary>
    /// <param name="PropertyName">属性名称</param>
    /// <param name="ChangeValue">改变的属性值</param>
    /// <param name="Bwh">改变值的表位号，如果是所有表位都改变即为999</param>
    /// <param name="taiType">台体类型</param>
    /// <param name="taiId">台体编号</param>
    /// <returns></returns>
    public delegate bool Event_DataInfoChanged(string PropertyName, object ChangeValue, int Bwh, int taiType, int taiId);
    /// <summary>
    /// 菜单单击委托
    /// </summary>
    /// <param name="EventID">枚举项</param>
    public delegate void dlg_MenuClick(CLDC_Comm.Enum.Cus_MenuEventID EventID);
    #endregion

    public partial class Main : Office2007Form, CLDC_DataCore.Interfaces.IMeterInfoUpdateDownEnablecs
    {
        #region 定义变量、事件、枚举
        private int TaiType;

        public int TaiId;

        /// <summary>
        /// 是否刷新方案列表
        /// </summary>
        public bool ReSetSchemeList = false;

        /// <summary>
        /// 电能表数据
        /// </summary>
        public CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup;

        /// <summary>
        /// 记录上一次的ToolBarItem的Text值，用于在停止检定时刷新数据的定位
        /// </summary>
        private string LastToolBarItemText = string.Empty;

        #region 枚举 项目类型
        /// <summary>
        /// 项目类型
        /// </summary>
        private enum EnumCheckType
        {
            录入参数 = 1,
            方案检定 = 2,
            审核存盘 = 3
        }
        #endregion

        /// <summary>
        /// 方案参数被修改事件
        /// </summary>
        public EventFaParmChanged FaParmChanged;

        /// <summary>
        /// 菜单条接换事件 (主程序接收到事件以后填充参数值)
        /// </summary>
        public EventOnTabChanged OnTabChanged;

        /// <summary>
        /// 终止检定
        /// </summary>
        public EventOnCheckStop OnCheckStop;

        /// <summary>
        /// 暂停检定
        /// </summary>
        public EventOnCheckPause OnCheckPause;

        /// <summary>
        /// 单步检定
        /// </summary>
        public Event_OnStepStart Evt_OnStepStart;

        /// <summary>
        /// 调表
        /// </summary>
        public EventOnCheckAdjust OnCheckAdjust;

        /// <summary>
        /// 编程提示
        /// </summary>
        public EventOnProgrammingTipAdjust OnProgrammingTipAdjust;

        /// <summary>
        /// 跳点
        /// </summary>
        public Event_OnChangePoint Evt_OnChangePoint;

        /// <summary>
        /// 存档事件
        /// </summary>
        public Event_OnAuditingSave Evt_OnAuditingSave;

        /// <summary>
        /// 准备存档事件
        /// </summary>
        public Event_OnAuditingSaveBefore Evt_OnAuditingSaveBefore;

        /// <summary>
        /// 录入起码止码完毕
        /// </summary>
        public Event_OnInputNumberEnd Evt_OnInputNumberEnd;

        /// <summary>
        /// 更改是否检定标志事件
        /// </summary>
        public EventOnYaoJianChanged Evt_OnYaoJianChanged;

        /// <summary>
        /// 挂新表
        /// </summary>
        public Event_OnHangUpNewMeter Evt_OnHangUpNewMeter;

        /// <summary>
        /// 录入参数完毕
        /// </summary>
        public Event_InputParam_OnOk Evt_InputParam_OnOk;

        /// <summary>
        /// 读取参数
        /// </summary>
        public Event_ReadPara Evt_ReadPara;

        /// <summary>
        /// 从MIS中下载电表信息
        /// </summary>
        public Event_DownMeterInfoFromMis Evt_DownMeterInfoFromMis;

        /// <summary>
        /// 从MIS中下载方案
        /// </summary>
        public Event_DownSchemeInfoFromMis Evt_DwonSchemeInfoFromMis;

        /// <summary>
        /// 方案配置完毕
        /// </summary>
        public Event_LoadFA_OnOk Evt_LoadFA_OnOk;

        /// <summary>
        /// 数据项变化事件
        /// </summary>
        public Event_DataInfoChanged Evt_DataInfoChanged;
        /// <summary>
        /// 菜单事件
        /// </summary>
        public event dlg_MenuClick Evt_dlg_MenuClick;
        #endregion
        private void OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID EventID)
        {
            if (Evt_dlg_MenuClick != null)
            {
                Evt_dlg_MenuClick(EventID);
            }
        }
        /// <summary>
        /// 设置状态栏消息
        /// </summary>
        /// <param name="msg"></param>
        public void SetSystemMessage(string msg)
        {
            if (PopByServer)
            {
                if (this.CurrentUIControl != null)
                {
                    if (this.CurrentUIControl is CLDC_MeterUI.UI_Detection_New.CheckBase)
                    {
                        ((CheckBase)this.CurrentUIControl).SetCheckMessage(msg);//设置消息信息
                    }
                }
                ShowStepUserMsg(msg);
                ShowStepInputParaMsg(msg);
            }
        }

        

        
        #region 初始化项目子控件 Fun_SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        /// <summary>
        /// 初始化项目子控件
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        /// <param name="CheckType"></param>
        private void Fun_SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("系统正在加载...");
            try
            {
                lock (CurrentUIControl_Lock)
                {
                    this.StatusMain_Proc.Visible = true;//进度条
                    bool IsNew = false;

                    switch (CheckType)
                    {
                        #region
                        case EnumCheckType.录入参数:
                            {
                                this.StatusMain_Proc.Visible = false;//进度条隐藏
                                if (Plan_ChildContainer.Controls.Count == 1 && Plan_ChildContainer.Controls[0] is InputPara_V80Style)
                                {
                                    
                                }
                                else
                                {
                                    CurrentUIControl = new InputPara_V80Style(this, meterGroup, taiType, taiId);
                                    this.stepUserControl1.Visible = false;//隐藏总进度框
                                    this.tableLayoutPanel1.RowStyles[1].Height = 2;
                                    IsNew = true;
                                }
                                break;
                            }
                        case EnumCheckType.方案检定:
                            if (Plan_ChildContainer.Controls.Count == 1 && Plan_ChildContainer.Controls[0] is CheckBase)
                            {
                                
                            }
                            else
                            {
                                CurrentUIControl = new CheckBase(this, meterGroup, taiType, taiId);
                                ((CheckBase)CurrentUIControl).strBasicInfo = strBasicInfo;
                                this.stepUserControl1.Visible = true;
                                this.tableLayoutPanel1.RowStyles[1].Height = 57;
                                IsNew = true;
                            }
                            break;
                        case EnumCheckType.审核存盘:
                            if (Plan_ChildContainer.Controls.Count == 1 && Plan_ChildContainer.Controls[0] is AuditingSave)
                            { }
                            else
                            {
                                CurrentUIControl = new AuditingSave(this, meterGroup, taiType, taiId);
                                this.stepUserControl1.Visible = false;
                                this.tableLayoutPanel1.RowStyles[1].Height = 2;
                                IsNew = true;
                            }
                            break;
                        default:
                            break;
                        #endregion
                    }
                    if (IsNew)              //如果是新表单，就需要先移除所有表单，再追加
                    {
                        if (CurrentUIControl != null)
                        {
                            #region
                            Init_Stutas();//控制状态栏
                            while (Plan_ChildContainer.Controls.Count > 0)
                            {
                                Control tmpControlHand = Plan_ChildContainer.Controls[0];
                                Plan_ChildContainer.Controls.Remove(tmpControlHand);
                                tmpControlHand.Dispose();
                            }

                            Plan_ChildContainer.Controls.Add(CurrentUIControl);

                            CurrentUIControl.Margin = new System.Windows.Forms.Padding(1);
                            CurrentUIControl.Dock = DockStyle.Fill;
                            #endregion
                        }
                    }

                    this.Fun_RefreshData(meterGroup, TaiType, taiId, CheckType);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,408]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Const.GlobalUnit.Logger.Fatal("初始化类:" + typeof(Main).FullName + "失败:", ex);
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }
        #endregion

        #region 刷新子项目数据 Fun_RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        /// <summary>
        /// 刷新子项目数据 
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        /// <param name="CheckType"></param>
        private void Fun_RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            lock (CurrentUIControl_Lock)
            {
                //截获错误、提示并写入日志
                try
                {
                    switch (CheckType)
                    {
                        case EnumCheckType.录入参数:
                            ((InputPara_V80Style)CurrentUIControl).RefreshData(meterGroup, taiType, taiId);
                            break;
                        case EnumCheckType.方案检定:
                            ((CheckBase)CurrentUIControl).RefreshData(meterGroup, taiType, taiId);
                            if (ReSetSchemeList)
                            {
                                ((CheckBase)CurrentUIControl).CreateNewRowCell(meterGroup.CheckPlan);
                                ReSetSchemeList = false;
                            }
                            break;
                        case EnumCheckType.审核存盘:
                            ((AuditingSave)CurrentUIControl).RefreshData(meterGroup, taiType, taiId);
                            CLDC_DataCore.Const.GlobalUnit.g_RealTimeDataControl.OutUpdateRealTimeData("", CLDC_Comm.Enum.Cus_MeterDataType.装置状态信息数据, false);
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG          
                    MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,457]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }
        #endregion

        #region  要求录入起码、止码
        /// <summary>
        /// 要求录入起码、止码
        /// </summary>
        /// <param name="IsStartNumber">是否录入起码？否则录入止码</param>
        /// <returns>操作是否成功</returns>
        public bool Fun_InputZZNumber(bool IsStartNumber)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                if (CurrentUIControl is CheckBase)
                {
                    return ((CheckBase)CurrentUIControl).Fun_InputZZNumber(IsStartNumber);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,488]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }
        #endregion

        #region 调表操作
        /// <summary>
        /// 调表操作
        /// </summary>
        /// <param name="IsAdjust">true:开始调表、false:停止调表</param>
        public bool CheckAdjust(bool IsAdjust)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                bool bResult = false;
                if (IsAdjust)
                {
                    //Comm.Function.TopWaiting.ShowWaiting("正在开始调表...");

                    if (OnCheckAdjust != null)
                    {
                        bResult = OnCheckAdjust(true, TaiType, TaiId);
                    }
                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    //Comm.Function.TopWaiting.ShowWaiting("正在结束调表...");
                    if (OnCheckAdjust != null)
                    {
                        bResult = OnCheckAdjust(false, TaiType, TaiId);
                    }
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                return bResult;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,531]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }
        #endregion

        #region 编程提示
        /// <summary>
        /// 编程提示
        /// </summary>
        /// <param name="IsAdjust">true:提示、false:不提示</param>
        public bool ProgrammingTipAdjust(bool IsAdjust)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                bool bResult = false;
                if (IsAdjust)
                {
                    //Comm.Function.TopWaiting.ShowWaiting("...");

                    if (OnProgrammingTipAdjust != null)
                    {
                        bResult = OnProgrammingTipAdjust(true, TaiType, TaiId);
                    }
                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    //Comm.Function.TopWaiting.ShowWaiting("结束...");
                    if (OnProgrammingTipAdjust != null)
                    {
                        bResult = OnProgrammingTipAdjust(false, TaiType, TaiId);
                    }
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                return bResult;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,574]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }
        #endregion

        #region 定义
        /// <summary>
        /// 记录当前的子UI
        /// </summary>
        private Base CurrentUIControl = null;
        /// <summary>
        /// 线程锁
        /// </summary>
        private object CurrentUIControl_Lock = new object();

        private object objSetDataLock = new object();

        /// <summary>
        /// 当前检定项目下标
        /// </summary>
        public int ActiveIdByClick = -1;

        /// <summary>
        /// 网络指示灯
        /// </summary>
        private Image[] ImgNetState = new Image[2];

        /// <summary>
        /// 监视器
        /// </summary>
        public CLDC_MeterUI.Monitor UIMonitor;

        /// <summary>
        /// 是否服务器弹出
        /// </summary>
        public readonly bool PopByServer;
        /// <summary>
        /// 登录时间/人员
        /// </summary>
        public readonly string LoginerAndTime;
        #endregion

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = string.Format("主控端 - {0}号 【{1}】", TaiId, (TaiType == 0 ? "三相台" : "单相台"));
            }
        }

        /// <summary>
        /// 设置 网络连接状态
        /// </summary>
        public CLDC_Comm.Enum.Cus_NetState NetState
        {
            set
            {
                if (ImgNetState[(int)CLDC_Comm.Enum.Cus_NetState.DisConnected] == null || ImgNetState[(int)CLDC_Comm.Enum.Cus_NetState.Connected] == null)
                {
                    ImgNetState[(int)CLDC_Comm.Enum.Cus_NetState.Connected] = (Image)new Icon(CLDC_DataCore.Function.File.GetPhyPath(Application.StartupPath + @"\Pic\NetConnected.ico")).ToBitmap();
                    ImgNetState[(int)CLDC_Comm.Enum.Cus_NetState.DisConnected] = (Image)new Icon(CLDC_DataCore.Function.File.GetPhyPath(Application.StartupPath + @"\Pic\NetDisConnected.ico")).ToBitmap();
                }
                Status_Light.Image = ImgNetState[(int)value];
            }
        }

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public Main()
        {
            InitializeComponent();

            StatusMain_Text.Text = "";

            this.Resize += new EventHandler(Main_Resize);

            this.Load += new EventHandler(Main_Load);

            StatusMain_Mode.Text = "[" + CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("ISDEMO") + "]";

            LoginerAndTime = "   检定员：" + CLDC_DataCore.Const.GlobalUnit.User_Jyy.UserName + "   核验员：" + CLDC_DataCore.Const.GlobalUnit.User_Hyy.UserName + "   登录时间：" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            
            
            //this.buttonItem5.Text = this.buttonItem5.Text.GetString();
        }

        private void Init_Stutas()
        {
            
            StatusMain_LabLoginMeg.Text = LoginerAndTime;
            if (StatusMain_Proc.Visible == false)
            {
                StatusMain_Text.Width = this.Width - 85 - StatusMain_TxtStatus1.Width - StatusMain_TxtStatus2.Width - StatusMain_Mode.Width - StatusMain_LabLoginMeg.Width;
            }
            else
            {
                StatusMain_Text.Width = this.Width - StatusMain_Proc.Width - 85 - StatusMain_TxtStatus1.Width - StatusMain_TxtStatus2.Width - StatusMain_Mode.Width - StatusMain_LabLoginMeg.Width;
            }
        }
        
        /// <summary>
        /// 重写的构造函数
        /// </summary>
        /// <param name="byserver">是否来自服务器端</param>
        public Main(bool byserver)
            : this()
        {
            PopByServer = byserver;
            if (PopByServer == true)
            {
                this.MinimizeBox = false;
                //this.MaximizeBox  = false;
            }
        }

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Main_Load(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            Control tmpParent = this;
            while (tmpParent.Parent != null && !(tmpParent is CLDC_MeterUI.UI_ClientFrame))
            {
                tmpParent = tmpParent.Parent;
            }
            
            try
            {
                //客户端主控时
                if (tmpParent is CLDC_MeterUI.UI_ClientFrame)
                {
                    CLDC_MeterUI.UI_ClientFrame ClientFrameHandle = (CLDC_MeterUI.UI_ClientFrame)tmpParent;
                    this.UIMonitor = ClientFrameHandle.UIMonitor;

                    UIMonitor = new Monitor();
                    UIMonitor.DanXiangTai = (CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("DESKTYPE") == "单相台");
                    ClientFrameHandle.Controls.Add(UIMonitor);
                    ClientFrameHandle.Controls.SetChildIndex(UIMonitor, 0);
                    UIMonitor.Visible = true;
                }
                else //服务器端弹出时
                {

                    UIMonitor = new Monitor();
                    UIMonitor.DanXiangTai = TaiType != 0;
                    this.Controls.Add(UIMonitor);
                    this.Controls.SetChildIndex(UIMonitor, 0);
                    UIMonitor.Visible = true;
                }
                this.stepUserControl1.dlgButtnClickCall = ActiveToolStrip_Item;
          
                
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,743]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }

            //使当前窗体在最前端
            CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(SetWindowState), 500);
        }

        private void SetWindowState()
        {
            this.TopMost = true;
            this.TopMost = false;

            this.WindowState = FormWindowState.Maximized;
            Init_Stutas();
        }
        #endregion

        #region Main_Resize(object sender, EventArgs e)
        private void Main_Resize(object sender, EventArgs e)
        {
            StatusMain_LabLoginMeg.Text = LoginerAndTime;
            if (StatusMain_Proc.Visible == false)
            {
                StatusMain_Text.Width = this.Width - StatusMain_TxtStatus1.Width - 85 - StatusMain_TxtStatus2.Width - StatusMain_Mode.Width - StatusMain_LabLoginMeg.Width;
            }
            else
            {
                StatusMain_Text.Width = this.Width - StatusMain_Proc.Width - 85 - StatusMain_TxtStatus1.Width - StatusMain_TxtStatus2.Width - StatusMain_Mode.Width -  StatusMain_LabLoginMeg.Width;
            }
            if (UIMonitor != null)
            {
                UIMonitor.Btn_Expend_Click(sender, e);
            }
        }
        #endregion

        #region  重设、刷新数据
        private void AddToolStripItem(string text, string imgUrl)
        {
            //ToolStripButton ToolBtn = new ToolStripButton();
            //imgUrl = CLDC_DataCore.Function.File.GetPhyPath(imgUrl);
            //ToolBtn.BackColor = System.Drawing.Color.Transparent;
            //ToolBtn.Image = Image.FromFile(imgUrl);
            //ToolBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            //ToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            //ToolBtn.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            //ToolBtn.Size = new System.Drawing.Size(57, 48);
            //ToolBtn.Text = text;
            //ToolBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;

            //ToolStrip_Main.Items.Add((ToolStripItem)ToolBtn);
        }


        private bool hasSetTtype = false;
        private string strBasicInfo = "";
        private delegate void EventSetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

        /// <summary>
        /// ========== 重设、刷新数据  ==========
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        public void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                this.TaiType = taiType;
                this.TaiId = taiId;
                this.MeterGroup = meterGroup;
                strBasicInfo = "电能表：" + CLDC_DataCore.Const.GlobalUnit.Clfs.ToString() + "、" + CLDC_DataCore.Const.GlobalUnit.U + "V、" + CLDC_DataCore.Const.GlobalUnit.Ib + "(" + CLDC_DataCore.Const.GlobalUnit.Imax + ")A、" + meterGroup.MinConst[0] + "imp/Kwh、当前方案：" + meterGroup.FaName;
                this.stepUserControl1.strBisicInfo = strBasicInfo;
                int totalTime = meterGroup.CheckPlan.Count * 115;//miao
                if (totalTime == 0)
                {
                    totalTime = 120;
                }
                if (CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum.Year == 1)
                    CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum = DateTime.Now;
                CLDC_DataCore.Const.GlobalUnit.g_CheckTimeEndSum = CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum.AddSeconds(totalTime);
                string strTotalTime = string.Format("{0}小时{1}分{2}秒", totalTime / 3600, (totalTime % 3600) / 60, totalTime % 60);
                this.stepUserControl1.strTotalTime = strTotalTime;
                this.stepUserControl1.strLastTime = strTotalTime;
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventSetData(SetDataInvoke), meterGroup, TaiType, taiId);
                }
                else
                {
                    SetDataInvoke(meterGroup, TaiType, taiId);
                }
            }
            catch (Exception ex)
            {
                //截获错误、提示，并写入日志
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,840]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }

        public void SetDataInvoke(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            lock (objSetDataLock)
            {
                this.Text = ""; //刷新标题 由于属性自动处理
                 StatusMain_TxtStatus2.Text = meterGroup.CheckState.ToString();

                int ActiveId = meterGroup.ActiveItemID;

                SetWindowText(taiId, taiType, meterGroup._Bws, meterGroup.CheckState, ActiveId);
                if (!hasSetTtype && UIMonitor != null)
                {
                    UIMonitor.DanXiangTai = taiType == 1 ? true : false;
                    hasSetTtype = true;
                } //停止检定状态下，被刷新数据不切换工具菜单
                //if (meterGroup.CheckState == Comm.Enum.Cus_CheckStaute.停止检定
                //    && LastToolBarItemText != string.Empty && meterGroup.ActiveItemID >= 0
                //    && LastToolBarItemText != "录入参数")
                //{
                //    SetData2(meterGroup, GetCheckPrjIndexFromGroupInfo(MeterGroup, LastToolBarItemText), taiType, taiId);
                //}
                //else
                //{
                SetData2(meterGroup, ActiveId, taiType, taiId);
                //}
            }
        }

        /// <summary>
        /// ========== 重设、刷新数据  ==========
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        private void SetData2(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int ActiveId, int taiType, int taiId)
        {
            lock (objSetDataLock)
            {
                int FirstIndex = GetFirstYaoJianMeterIndex(meterGroup);
                object objActivePlan = null;
                if (ActiveId >= 0 && meterGroup.CheckPlan.Count > 0 && ActiveId < meterGroup.CheckPlan.Count)
                {
                    objActivePlan = meterGroup.CheckPlan[ActiveId];
                }

                #region 录入参数|审核存盘
                if (ActiveId <= 0)           //当前ID小于0（这个表示是不在检定过程中）并且检定方案是创建过的。
                {
                    //ToolBtn_StepStart.Enabled = false;
                    //ToolBtn_Start.Enabled = false;
                    //ToolBtn_Stop.Enabled = false;

                    //停止检定状态下可以切换到 [录入参数]
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
                    {
                        ToolBtn_InputParam.Enabled = true;
                    }
                    else
                    {
                        ToolBtn_InputParam.Enabled = false;
                    }

                    //保留前面4项，单步、连续、停止、录入参数,0,1,2,3
                    //for (int i = 4; i < ToolStrip_Main.Items.Count; i++)
                    //{
                    //    ToolStrip_Main.Items.RemoveAt(i--);
                    //}

                    #region 便历所有检定项目加载所有需要检定的项目工具条
                    //if (meterGroup.ActiveItemID >= 0 || meterGroup.ActiveItemID == -3)            //当前激活的ID大于0（表示是在检定过程中），或者是在审核存盘的状态

                    AddToolStripItem("方案检定", Application.StartupPath + @"\Pic\Detection\V90Style\wcjd.png");

                    AddToolStripItem("审核存盘", Application.StartupPath + @"\Pic\Detection\V90Style\shcp.png");

                    SetToolBtnEnableByText("方案检定", false);
                    SetToolBtnEnableByText("审核存盘", false);

                    if (meterGroup.CheckPlan.Count > 0)
                    {

                        SetToolBtnEnableByText("方案检定", true);
                        SetToolBtnEnableByText("审核存盘", true);
                    }
                    #endregion

                }

                #region 录入参数
                if (ActiveId == -1)
                {
                    if (CurrentUIControl is InputPara_V80Style)
                    {
                        Fun_RefreshData(meterGroup, taiType, taiId, EnumCheckType.录入参数);
                    }
                    else
                    {
                        Fun_SetData(meterGroup, taiType, taiId, EnumCheckType.录入参数);
                    }
                    SetCurrentToolBtnStyle("参数录入");
                    //SetToolBtnEnableByText("方案检定", false);
                    //SetToolBtnEnableByText("审核存盘", false);
                }
                #endregion

                #region 审核存盘
                else if (ActiveId == -3)
                {
                    if (CurrentUIControl is AuditingSave)
                    {
                        Fun_RefreshData(meterGroup, taiType, taiId, EnumCheckType.审核存盘);
                    }
                    else
                    {
                        Fun_SetData(meterGroup, taiType, taiId, EnumCheckType.审核存盘);

                    }
                    //SetCurrentToolBtnStyle("审核存盘");

                }
                #endregion

                #endregion

                #region 根据检定状态设置、连续检定、停止检定、调表按钮可用状态
                if (ActiveId >= 0)
                {
                    
                    //保留前面4项，单步、连续、停止、录入参数,0,1,2,3
                    //if (ToolStrip_Main.Items.Count == 4)
                    //{
                    //    AddToolStripItem("方案检定", Application.StartupPath + @"\Pic\Detection\V90Style\wcjd.png");

                    //    AddToolStripItem("审核存盘", Application.StartupPath + @"\Pic\Detection\V90Style\shcp.png");
                    //}

                    if (meterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
                    {
                        SetToolBtnEnableByText("参数录入", true);
                        SetToolBtnEnableByText("预先调试", true);
                        SetToolBtnEnableByText("重新联机", true);
                        SetToolBtnEnableByText("升源输出", true);
                        SetToolBtnEnableByText("关源停止", true);
                        SetToolBtnEnableByText("审核存盘", true);
                    }
                    else
                    {
                        SetToolBtnEnableByText("参数录入", false);
                        SetToolBtnEnableByText("预先调试", false);
                        SetToolBtnEnableByText("重新联机", false);
                        SetToolBtnEnableByText("升源输出", false);
                        SetToolBtnEnableByText("关源停止", false);
                        SetToolBtnEnableByText("审核存盘", false);
                    }


                    switch (meterGroup.CheckState)
                    {
                        case CLDC_Comm.Enum.Cus_CheckStaute.单步检定:
                            this.stepUserControl1.isStepEnabled = false;
                            this.stepUserControl1.isStartEnabled = false;
                            this.stepUserControl1.isStopEnabled = true;
                            break;

                        case CLDC_Comm.Enum.Cus_CheckStaute.调表:
                            this.stepUserControl1.isStepEnabled = false;
                            this.stepUserControl1.isStartEnabled = false;
                            this.stepUserControl1.isStopEnabled = true;
                            break;

                        case CLDC_Comm.Enum.Cus_CheckStaute.检定:
                            this.stepUserControl1.isStepEnabled = false;
                            this.stepUserControl1.isStartEnabled = false;
                            this.stepUserControl1.isStopEnabled = true;
                            break;

                        case CLDC_Comm.Enum.Cus_CheckStaute.停止检定:
                            //this.stepUserControl1.isStepEnabled = true;
                            //this.stepUserControl1.isStartEnabled = true;
                            //this.stepUserControl1.isStopEnabled = false;
                            //this.stepUserControl1.isEnabled = false;
                            System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(SetStopBtnState));
                            

                            break;

                        default:
                            break;
                    }
                }
                #endregion

                #region 以下状态为连续检定以后

                if (ActiveId >= 0)
                {
                    if (CurrentUIControl is CheckBase)
                    {
                        Fun_RefreshData(meterGroup, taiType, taiId, EnumCheckType.方案检定);
                    }
                    else
                    {
                        Fun_SetData(meterGroup, taiType, taiId, EnumCheckType.方案检定);
                        //SetCurrentToolBtnStyle("方案检定");
                    }
                    //用.Tag保存当前检定项目
                    if (CurrentUIControl != null)
                        CurrentUIControl.Tag = objActivePlan;

                }
                #endregion

                #region//停止检定状态下可以切换到 [录入参数]
                if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定/* && ActiveId >= 0*/)
                {
                    ToolBtn_InputParam.Enabled = true;
                    //if (ActiveId == -1)
                    {
                        //if (this.ToolStrip_Main.Items.Count > 4)
                        //{
                        if (meterGroup.CheckPlan.Count > 0 && meterGroup.ActiveItemID >= 0)
                        {
                            SetToolBtnEnableByText("预先调试", true);
                            SetToolBtnEnableByText("重新联机", true);
                            SetToolBtnEnableByText("升源输出", true);
                            SetToolBtnEnableByText("关源停止", true);
                            SetToolBtnEnableByText("方案检定", true);
                            SetToolBtnEnableByText("审核存盘", true);
                        }
                        else
                        {
                            SetToolBtnEnableByText("预先调试", false);
                            SetToolBtnEnableByText("重新联机", false);
                            SetToolBtnEnableByText("升源输出", false);
                            SetToolBtnEnableByText("关源停止", false);
                            SetToolBtnEnableByText("方案检定", false);
                            SetToolBtnEnableByText("审核存盘", false);
                        }
                        //}
                    }
                }
                else
                {
                    //ToolBtn_InputParam.Enabled = false;
                    SetToolBtnEnableByText("预先调试", false);
                    SetToolBtnEnableByText("重新联机", false);
                    SetToolBtnEnableByText("升源输出", false);
                    SetToolBtnEnableByText("关源停止", false);
                    SetToolBtnEnableByText("参数录入", false);
                    SetToolBtnEnableByText("预先调试", false);
                    SetToolBtnEnableByText("审核存盘", false);
                }
                #endregion
            }
        }

        private void SetStopBtnState(object o)
        {
            while (CLDC_DataCore.Const.GlobalUnit.ApplicationIsOver == false)
            {
                this.stepUserControl1.isStepEnabled = !CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning;
                this.stepUserControl1.isStartEnabled = !CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning;
                this.stepUserControl1.isStopEnabled = CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning;
                this.stepUserControl1.isEnabled = CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning;
                if (CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning == false)
                {
                    CLDC_Dispatcher.DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.SetCurCheckStateStop);
                    break;
                }
                Thread.Sleep(100);
            }
        }
        #endregion

        #region 设置状态栏（进度和提示文字）信息
        /// <summary>
        /// 设置状态栏（进度和提示文字）信息
        /// </summary>
        /// <param name="NoticeText"></param>
        public void SetStatus(string NoticeText)
        {
            if (this.CurrentUIControl != null)
            {
                if (this.CurrentUIControl is CLDC_MeterUI.UI_Detection_New.CheckBase)
                {
                    ((CheckBase)this.CurrentUIControl).SetCheckMessage(NoticeText);//设置消息信息
                }
            }
            
            //根据当前检定项目和进度设置进度条
            try
            {
                float maxProcess = 0;
                int curPorcess = 0;
                if (this.MeterGroup != null)
                {
                    curPorcess = (int)(MeterGroup.NowMinute * 100);
                    if (MeterGroup.ActiveItemID >= 0)
                    {
                        if (/*this.MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定 ||*/
                            this.MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.未赋值的
                        )
                        {
                            int _FirstIndex = GetFirstYaoJianMeterIndex(MeterGroup);

                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _firstMeter = MeterGroup.MeterGroup[_FirstIndex];

                            if (MeterGroup.CheckPlan.Count > MeterGroup.ActiveItemID)
                            {

                                object objActivePlan = null;
                                objActivePlan = MeterGroup.CheckPlan[MeterGroup.ActiveItemID];
                                this.stepUserControl1.strNowItem = objActivePlan.ToString();
                                this.stepUserControl1.strNowRun = this.MeterGroup.CheckState.ToString();
                                
                                if (objActivePlan is StPlan_YuRe)
                                {
                                    maxProcess = ((StPlan_YuRe)objActivePlan).Times;
                                    
                                }
                                else if (objActivePlan is StPlan_QiDong)
                                {
                                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString(), ((int)((StPlan_QiDong)objActivePlan).PowerFangXiang).ToString());
                                    if (_firstMeter.MeterResults.ContainsKey(_Key))
                                    {
                                        //TODO:启动实验项目的进度显示处理
                                        //if (CLDC_DataCore.Function.Number.IsNumeric(_firstMeter.MeterResults[_Key].Mr_Time))
                                        //    maxProcess = float.Parse(_firstMeter.MeterResults[_Key].Mr_Time);
                                        //else
                                        //    maxProcess = 0;
                                    }
                                    else
                                    {
                                        maxProcess = ((StPlan_QiDong)objActivePlan).CheckTime;
                                    }
                                }
                                else if (objActivePlan is StPlan_QianDong)
                                {
                                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString(), ((int)((StPlan_QianDong)objActivePlan).PowerFangXiang).ToString());
                                    if (_firstMeter.MeterResults.ContainsKey(_Key))
                                    {
                                        //TODO:潜动的试验进度条处理
                                        //if (CLDC_DataCore.Function.Number.IsNumeric(_firstMeter.MeterResults[_Key].Mr_Time))
                                        //    maxProcess = float.Parse(_firstMeter.MeterResults[_Key].Mr_Time);
                                        //else
                                        //    maxProcess = 0;
                                    }
                                    else
                                    {
                                        maxProcess = ((StPlan_QianDong)objActivePlan).CheckTime;
                                    }

                                }
                                #region----------基本误差和特殊检定进度----------
                                else if (objActivePlan is StPlan_WcPoint ||
                                        objActivePlan is StPlan_SpecalCheck)
                                {
                                    if (objActivePlan is StPlan_WcPoint)
                                    {
                                        if (((StPlan_WcPoint)objActivePlan).Pc == 1)
                                            maxProcess = MeterGroup.PcCheckNumic;
                                        else
                                            maxProcess = MeterGroup.WcCheckNumic;
                                    }
                                    else
                                    {
                                        maxProcess = MeterGroup.WcCheckNumic;
                                    }
                                    if (MeterGroup.MeterGroup[_FirstIndex].MeterErrors.ContainsKey("P_" + MeterGroup.ActiveItemID))
                                    {
                                        string[] curwc =
                                            MeterGroup.MeterGroup[_FirstIndex].MeterErrors["P_" + MeterGroup.ActiveItemID].Me_chrWcMore.Split('|');
                                        if (curwc.Length > 0)
                                        {
                                            int k = 0;
                                            for (k = 0; k < curwc.Length; k++)
                                            {
                                                if (curwc[k].IndexOf("999") != -1)
                                                {
                                                    break;
                                                }
                                            }
                                            if (k > maxProcess)
                                            {
                                                k = (int)maxProcess;
                                            }
                                            curPorcess = k * 100;
                                        }

                                    }
                                    else
                                    {
                                        curPorcess = 0;
                                    }
                                }
                                #endregion

                                else if (objActivePlan is StPlan_ZouZi)
                                {
                                    maxProcess = ((StPlan_ZouZi)objActivePlan).UseMinutes;
                                }
                                else
                                {
                                    curPorcess = 100;
                                    maxProcess = 1;
                                }
                            }
                        }
                    }
                }

                if (maxProcess == 0)
                {
                    
                    ShowStepUserMsg(NoticeText);

                    ShowStepInputParaMsg(NoticeText);
                }
                else
                {
                    int maxP = (int)(maxProcess * 100);
                    if (curPorcess > maxP)
                    {
                        curPorcess = maxP;
                    }
                    SetStatus(NoticeText, curPorcess, maxP);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                //MessageBoxEx.Show(ex.Message, "[本框只有DEBUG时出现]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Const.GlobalUnit.Logger.Debug(typeof(Main).FullName, ex);
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }

        private void ShowStepUserMsg(string NoticeText)
        {
            //StatusMain_Text.Text = NoticeText;
            stepUserControl1.strNowRun = NoticeText;
        }

        private void ShowStepInputParaMsg(string NoticeText)
        {           
            if (Plan_ChildContainer.Controls.Count == 1 && Plan_ChildContainer.Controls[0] is InputPara_V80Style)
            {
                ((InputPara_V80Style)Plan_ChildContainer.Controls[0]).strNowRun = NoticeText;
            }
        }

        /// <summary>
        /// 设置状态栏（进度和提示文字）信息
        /// </summary>
        /// <param name="NoticeText">状态栏提示文字</param>
        /// <param name="IncProgressValue">进度值自动垒加1</param>
        public void SetStatus(string NoticeText, bool IncProgressValue)
        {
            ShowStepUserMsg(NoticeText);
            ShowStepInputParaMsg(NoticeText);
            if (IncProgressValue)
            {
                if (StatusMain_Proc.Value < StatusMain_Proc.Maximum)
                    StatusMain_Proc.Value = StatusMain_Proc.Value + 1;
            }
        }
        /// <summary>
        /// 设置状态栏（进度和提示文字）信息
        /// </summary>
        /// <param name="NoticeText">状态栏提示文字</param>
        /// <param name="ProgressValue">状态栏进度，默认 0-100</param>
        public void SetStatus(string NoticeText, int ProgressValue)
        {
            ShowStepUserMsg(NoticeText);
            ShowStepInputParaMsg(NoticeText);
            if (ProgressValue > StatusMain_Proc.Maximum)
                StatusMain_Proc.Value = StatusMain_Proc.Maximum;
            else
                StatusMain_Proc.Value = ProgressValue;
        }

        /// <summary>
        /// 设置状态栏（进度和提示文字）信息
        /// </summary>
        /// <param name="NoticeText">状态栏提示文字</param>
        /// <param name="ProgressValue">状态栏进度，默认 0-100</param>
        /// <param name="ProgressMaxValue">进度条最大值</param>
        public void SetStatus(string NoticeText, int ProgressValue, int ProgressMaxValue)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                if (ProgressValue < 0 || ProgressMaxValue < 1) return;
                if (ProgressValue > 2100000000 || ProgressMaxValue > 2100000000) return;

                ShowStepUserMsg(NoticeText);
                ShowStepInputParaMsg(NoticeText);
                //CLDC_DataCore.Function.SetControl.SetText(this, StatusMain_Text, NoticeText);

                if (!this.IsHandleCreated) return;
                this.BeginInvoke
                    (new CLDC_DataCore.Function.SetControl.EventSetProcessbar
                    (CLDC_DataCore.Function.SetControl.InvokeSetProcessbar),
                    new object[] { StatusMain_Proc, ProgressValue, ProgressMaxValue });
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,1340]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }
        #endregion

        #region 工具条单击事件

        /// <summary>
        /// 激活指定的菜单选项
        /// 1 停止
        /// </summary>
        /// <param name="type"></param>
        public void ActiveToolStrip_Item(object type)
        {
            Action<object> method = delegate(object o)
            {
                EventArgs args = new EventArgs();
                this.ToolStrip_Main_ItemClicked(type, args);
            };
            this.Invoke(method, "");
        }

        /// <summary>
        /// 工具条单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStrip_Main_ItemClicked(object sender, EventArgs e)
        {//lsx
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                if (!ToolStrip_Main.Enabled) return;

                ToolStrip_Main.Enabled = false;
                //如果底层或是网络在规定时间内没有返回。则自动将按钮设置为可用。且去掉提示文本

                CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(thResetButton), 30000);
                this.stepUserControl1.isEnabled = false;
                ButtonItem btie = sender as ButtonItem;
                switch (btie.Text.Trim())
                {

                    case "连续检定":
                        {
                            if (CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.IsTipAuxiliaryPress == true)
                            {
                                if(DialogResult.Cancel ==MessageBoxEx.Show(this, "请先将辅助端子压针压好。如果压好请点“确定”，否则点“取消”取消操作。", "提示",MessageBoxButtons.OKCancel))
                                {
                                    break;
                                }
                            }
                            #region
                            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在开始检定...");
                            this.stepUserControl1.isEnabled = true;
                            if (Evt_OnChangePoint != null)
                            {
                                if (MeterGroup.ActiveItemID >= 0)
                                {
                                    if (ActiveIdByClick != -1)
                                    {
                                        //从用户选定的项目连续检定
                                        Evt_OnChangePoint(ActiveIdByClick, TaiType, TaiId);
                                    }
                                    else
                                    {
                                        //接着上次停止时的进度连续检定
                                        Evt_OnChangePoint(MeterGroup.ActiveItemID, TaiType, TaiId);
                                    }
                                }
                                else
                                {
                                    //从第一个项目连续检定
                                    Evt_OnChangePoint(GetFirstCheckPrjIndexFromGroupInfo(MeterGroup), TaiType, TaiId);
                                }
                            }
                            else
                            {
                                MessageBoxEx.Show(this,"没有处理Evt_OnChangePoint事件!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            CLDC_DataCore.Function.TopWaiting.HideWaiting();
                            CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning = true;
                            ActiveIdByClick = -1;
                            #endregion
                            break;
                        }

                    case "停止检定":
                        {
                            #region
                            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在停止检定...");
                            
                            if (OnCheckStop != null)
                            {
                                OnCheckStop(TaiType, TaiId);
                            };
                            
                            ActiveIdByClick = -1;

                            CLDC_DataCore.Function.TopWaiting.HideWaiting();
                            #endregion
                            break;
                        }

                    case "单步检定":
                        {
                            if (CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.IsTipAuxiliaryPress == true)
                            {
                                if (DialogResult.Cancel == MessageBoxEx.Show(this, "请先将辅助端子压针压好。如果压好请点“确定”，否则点“取消”取消操作。", "提示", MessageBoxButtons.OKCancel))
                                {
                                    break;
                                }
                            }
                            #region
                            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在开始单步检定...");
                            this.stepUserControl1.isEnabled = true;
                            if (Evt_OnStepStart != null)
                            {
                                //说明自上次[停止检定]以后、用户没有通过鼠标点击切换检定点
                                if (ActiveIdByClick == -1)
                                {
                                    ActiveIdByClick = MeterGroup.ActiveItemID;
                                }

                                if (ActiveIdByClick >= 0)
                                {
                                    Evt_OnStepStart(ActiveIdByClick, TaiType, TaiId);
                                    //从用户选定的项目连续检定
                                }
                                else
                                {
                                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                                    MessageBoxEx.Show(this,"请选选一个需要单步检定的项目!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    ToolStrip_Main.Enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                                MessageBoxEx.Show(this,"没有处理事件Evt_OnStepStart", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ToolStrip_Main.Enabled = true;
                                return;
                            }
                            CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning = true;
                            CLDC_DataCore.Function.TopWaiting.HideWaiting();
                            #endregion
                            break;
                        }
                    case "录入参数":
                    case "参数录入":
                        {
                            SetData2(MeterGroup, -1, TaiType, TaiId);
                            break;
                        }
                    //case "预先调试":
                    //    {
                    //        break;
                    //    }
                    case "审核存盘":
                        {
                            #region 如果是详细窗体时，在审核存盘前再向客户端请求一次数据总模型
                            //if (this.Evt_OnAuditingSaveBefore != null)
                            //{
                            //    if (Evt_OnAuditingSaveBefore(TaiType, TaiId))
                            //    {
                            //        Thread.Sleep(1000);
                            //    }
                            //}
                            #endregion
                            SetData2(MeterGroup, -3, TaiType, TaiId);
                            //SetCurrentToolBtnStyle("审核存盘");
                            break;
                        }
                    case "重新联机":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.联机);
                            break;
                        }
                    case "输出电压":
                    case "只升电压":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.升源);
                            break;
                        }
                    case "输出电流":
                    case "电压电流":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.升标定电压电流);
                            break;
                        }
                    case "停止输出":
                    case "关源停止":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.关源);
                            break;
                        }
                    case "自由控源":
                    case "自由输出":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.自由控源);
                            break;
                        }
                    case "高级配置"://大按钮默认进入系统配置
                    case "系统配置":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.系统配置);
                            break;
                        }
                    case "方案管理":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.方案管理);
                            break;
                        }
                    case "设备通道":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.配置设备端口);
                            break;
                        }
                    case "485通道":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.RS485通道配置);
                            break;
                        }
                    case "显示配置":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.显示配置);
                            break;
                        }
                    case "协议配置":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.协议配置);
                            break;
                        }
                    case "工具箱"://大按钮默认进入一个
                    case "电机操作":
                    case "高级控制":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.电机操作);
                            break;
                        }
                    case "报文工具":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.报文工具);
                        }
                        break;
                    case "协议字典":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.协议字典);
                        }
                        break;
                    case "模块测试":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.模块测试);
                        }
                        break;
                    case "延时设置":
                            {
                                OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.延时设置);
                            }
                        break;
                    case "数据管理":
                        CLDC_DataCore.Function.File.RunOtherExe(CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEEXEPATH, "/CLDC_DataManager.exe"), CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEPROCESSNAME, "ClDataManager"));
                        break;
                    //试验
                    default:
                        {//fjk 
                            SetStatus(string.Format("{0},{1},{2}", btie.Text, MeterGroup.CheckState, DateTime.Now.ToString()));
                            if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
                            {
                                //直接跳转
                                if (MeterGroup.ActiveItemID >= 0)
                                    SetData2(MeterGroup, MeterGroup.ActiveItemID, TaiType, TaiId);
                                else
                                    SetData2(MeterGroup, 0, TaiType, TaiId);
                            }
                            else
                            {

                            }
                            break;
                        }
                }
                ToolStrip_Main.Enabled = true;

            }
            catch (Exception ex)
            {
#if DEBUG             
                MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,1614]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }
        #endregion

        #region 恢复工具条状态为可用--用于操作长时间没有返回时
        private void thResetButton()
        {
            if (ToolStrip_Main.Enabled == false)
            {
                ToolStrip_Main.Enabled = true;
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                SetStatus("操作长时间没有返回，请重新操作");
            }
        }

        #endregion

        #region 根据标题文字获取工具条项
        /// <summary>
        /// 获取工具条项
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private ButtonItem GetToolBtnByText(string Text)
        {
            foreach (RibbonBar ctr in ToolStrip_Main.Controls)
            {
                foreach (ButtonItem bti in ctr.Items)
                {
                    if (bti.Text == Text)
                    {
                        return bti;
                    }
                }
                
            }
            return null;
        }
        #endregion

        #region 设置工具条项的可用状态
        /// <summary>
        /// 设置工具条项的可用状态
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="enable"></param>
        private void SetToolBtnEnableByText(string Text, bool enable)
        {
            ButtonItem ToolBtn = GetToolBtnByText(Text);
            if (ToolBtn != null)
                ToolBtn.Enabled = enable;
        }
        #endregion

        #region 设置工具条项为当前项（样式）
        /// <summary>
        /// 设置工具条项为当前项（样式）
        /// </summary>
        /// <param name="Text"></param>
        private void SetCurrentToolBtnStyle(string Text)
        {
            foreach (RibbonBar ctr in ToolStrip_Main.Controls)
            {
                foreach (ButtonItem bti in ctr.Items)
                {
                    if (bti.Text == Text)
                    {
                        bti.Enabled = true;
                        LastToolBarItemText = Text;
                    }
                    else
                    {
                        if (bti.Text == "数据管理" || bti.Text == "方案管理" || bti.Text == "高级配置" || bti.Text == "工具箱")
                        { }
                        else
                        {
                            bti.Enabled = false;
                        }
                    }
                }

            }
            
        }
        #endregion

        #region 在 CLDC_DataCore.Model.DnbModel.DnbGroupInfo中找到第一个检定项目下标
        /// <summary>
        ///  在 CLDC_DataCore.Model.DnbModel.DnbGroupInfo中找到第一个检定项目下标
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <returns></returns>
        private int GetFirstCheckPrjIndexFromGroupInfo(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup)
        {
            int index = -1;
            for (index = 0; index < meterGroup.CheckPlan.Count; index++)
            {
                object objItem = meterGroup.CheckPlan[index];
                if (objItem is StPlan_QiDong
                    || objItem is StPlan_YuRe
                    || objItem is StPlan_QianDong
                    || objItem is StPlan_WcPoint
                    || objItem is CLDC_DataCore.Struct.StPlan_Dgn
                    || objItem is StPlan_Carrier
                    || objItem is StPlan_ZouZi
                    || objItem is StPlan_SpecalCheck
                    )
                {
                    break;
                }
            }
            return index;
        }
        #endregion

        #region MeterGroup中[指定项目]的第一个检定项目下标(静态)
        /// <summary>
        /// MeterGroup中[指定项目]的第一个检定项目下标(静态)
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckTypeName"></param>
        /// <returns></returns>
        public static int GetCheckPrjIndexFromGroupInfo(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, CLDC_Comm.Enum.Cus_FAGroup CheckTypeName)
        {
            int index = -1;
            for (index = 0; index < meterGroup.CheckPlan.Count; index++)
            {
                object objItem = meterGroup.CheckPlan[index];
                if (objItem is StPlan_PrePareTest && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.预先调试)
                {
                    break;
                }
                if (objItem is StPlan_YuRe && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.预热试验)
                {
                    break;
                }
                else if (objItem is StPlan_WGJC && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.外观检查试验)
                {
                    break;
                }
                else if (objItem is StPlan_QiDong && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.起动试验)
                {
                    break;
                }
                else if (objItem is StPlan_QianDong && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.潜动试验)
                {
                    break;
                }
                else if (objItem is StPlan_WcPoint && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.基本误差试验)
                {
                    break;
                }
                else if (objItem is StPlan_SpecalCheck && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.影响量试验)
                {
                    break;
                }

                else if (objItem is CLDC_DataCore.Struct.StPlan_Dgn && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.多功能试验)
                {
                    break;
                }
                else if (objItem is StPlan_Carrier && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.载波试验)
                {
                    break;
                }
                else if (objItem is StPlan_ZouZi && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.走字试验)
                {
                    break;
                }
            }
            return index;
        }
        #endregion

        #region 获取第一只要检表的下标、没有返回-1
        /// <summary>
        /// 获取第一只要检表的下标、没有返回-1
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <returns></returns>
        public static int GetFirstYaoJianMeterIndex(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup)
        {
            for (int i = 0; i < meterGroup.MeterGroup.Count; i++)
            {
                if (meterGroup.MeterGroup[i].YaoJianYn) return i;
            }
            return -1;
        }
        #endregion

        #region 设置窗口标题文字
        private void SetWindowText(int taiId, int TaiType, int BW, CLDC_Comm.Enum.Cus_CheckStaute State, int ActiveId)
        {
            //string _TaiType = TaiType == 0 ? "三相" : "单相";
            //string _WindowText = String.Format("{0}号台,{1}{2}表位,状态:", taiId, _TaiType, BW);
            //if (State == Comm.Enum.Cus_CheckStaute.检定)
            //    _WindowText += String.Format("正在检定第{0}项", ActiveId);
            //else
            //    _WindowText += State.ToString();
            this.Text = "";
        }
        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            if (PopByServer == true)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            SendMessage(m);
            base.WndProc(ref m);
        }

        /// <summary>
        /// 发送截获的系统消息，主要为了处理窗体最小化，最大化
        /// </summary>
        /// <param name="m"></param>
        public void SendMessage(Message m)
        {
            if (!(CurrentUIControl is CheckBase)) return;

            const int SC_MINIMIZE = 0xF020;
            const int SC_MAXIMIZE = 0xF030;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_NOMUAL = 0XF120;
            const int SC_DOUBLENOMUAL = 0xf122;
            const int SC_DOUBLEMAXIMIZE = 0x012;
            const int SC_CLOSE = 0xF060;


            if (m.Msg == WM_SYSCOMMAND)
            {
                switch ((int)m.WParam)
                {
                    case SC_MINIMIZE:
                        ((CheckBase)CurrentUIControl).SetViewFormState(false);
                        break;
                    case SC_NOMUAL:
                        ((CheckBase)CurrentUIControl).SetViewFormState(true);
                        break;
                    case SC_MAXIMIZE:
                        ((CheckBase)CurrentUIControl).SetViewFormState(true);
                        break;
                    case SC_DOUBLENOMUAL:
                        ((CheckBase)CurrentUIControl).SetViewFormState(true);
                        break;
                    case SC_DOUBLEMAXIMIZE:
                        ((CheckBase)CurrentUIControl).SetViewFormState(true);
                        break;
                    case SC_CLOSE:
                        ((CheckBase)CurrentUIControl).CloseViewForm();
                        break;
                }

            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {

        }

        public void SetFormSize(int Width, int Height)
        {
            this.Size = new Size(Width, Height);
        }

        #region IMeterInfoUpdateDownEnablecs 成员

        public event CLDC_DataCore.Interfaces.GetMeterInfo OnGetMeterInfo;
        /// <summary>
        /// 对外触发通过MIS接口获取电能表基本信息事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo GetMeterInfo(string key)
        {
            if (OnGetMeterInfo == null) return null;
            return OnGetMeterInfo(key);
        }
        #endregion

        #region 设置设备连接状态
        //用于异步线程调用设置连接状态
        public void SetConnectStatusInvoke(bool connectStatus)
        {
            if (buttonItemConnectStatus.InvokeRequired)
            {
                buttonItemConnectStatus.Invoke(new Action<bool>(delegate(bool status)
                                                                                                {
                                                                                                    SetConnectStatus(status);
                                                                                                }), new object[]{connectStatus});
            }
            else
            { 
                SetConnectStatus(connectStatus);
            }
        }
        //设置设备与PC的连接状态
        private void SetConnectStatus(bool connectStatus)
        {
            if (connectStatus)
            {
                buttonItemConnectStatus.Image = Image.FromFile(CLDC_DataCore.Function.File.GetPhyPath(@"\Pic\UI\连接正常.png"));
                buttonItemConnectStatus.Tooltip = "连接正常";
            }
            else
            {
                buttonItemConnectStatus.Image = Image.FromFile(CLDC_DataCore.Function.File.GetPhyPath(@"\Pic\UI\连接断开.png"));
                buttonItemConnectStatus.Tooltip = "连接失败";
            }
        }
        #endregion 设置设备连接状态

        private void buttonItem4_Click(object sender, EventArgs e)
        {

        }


    }
}
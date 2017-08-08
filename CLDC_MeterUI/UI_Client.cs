/*
 * FileName UI_Client.cs
 * Description:客户端界面模块
 * 负责客户端（被控模式）下的数据录入、显示
 * LastUpdate:2009-8-14
 * Modify Log:
 * 1,
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using CLDC_Comm;
using CLDC_Comm.Command;
using CLDC_DataCore.Const;
//using ClInterface;
using System.Runtime.InteropServices;
using System.Threading;
using CLDC_DataCore.Command.Txm;
using System.IO;
using System.Reflection;
using CLDC_DataCore.Struct;


namespace CLDC_MeterUI
{
    public delegate bool OnEventSendMessage(CLDC_CTNProtocol.CTNPCommand cmdAsk, out CLDC_CTNProtocol.CTNPCommand cmdAnswer);
    /// <summary>
    /// 客户端被控状态界面
    /// </summary>
    public partial class UI_Client : Office2007Form, CLDC_DataCore.Interfaces.IMeterInfoUpdateDownEnablecs
    {
        #region----------接口事件声明----------
        //网络消息发送
        public event OnEventSendMessage SendMessage;
        //检定操作
        //public event ClInterface.UI.OnEventDoAction DoAction;
        #endregion

        #region----------内部委托----------
        private delegate void OnEventShowLabText(object obj, Label ShowLab, string Message);
        /// <summary>
        /// 录入完毕事件
        /// </summary>
        /// <param name="State">当前检定状态</param>
        public delegate void OnEventInputOver(CLDC_Comm.Enum.Cus_stVerifyStep State);
        public event OnEventInputOver OnInputOver;
        #endregion

        #region ----------变量声明----------

        /// <summary>
        /// 表格控件
        /// </summary>
        private PublicControls.UI_ClientTable ClientTable = null;

        //状态维护
        private CLDC_Comm.Enum.Cus_stVerifyStep m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.参数录入;

        //监视器
        private bool m_LoadOver = false;
        /// <summary>
        /// 网络连接状态图标
        /// </summary>
        private Image[] ImgNetState = new Image[2];

        public bool LoadOver
        {
            get { return m_LoadOver; }
        }

        #endregion

        /// <summary>
        /// 监视器
        /// </summary>
        public CLDC_MeterUI.Monitor UIMonitor = new Monitor();

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

                Pic_NetState.Image = ImgNetState[(int)value];
            }
        }

        public UI_Client()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.Load += new EventHandler(UI_Client_Load);
#if DEBUG
            //Comm.DesignTools.配色工具.Bind(this);
#endif
        }

        void UI_Client_Resize(object sender, EventArgs e)
        {

            if (UIMonitor != null)
            {
                UIMonitor.Btn_Expend_Click(sender, e);
            }
        }

        void UI_Client_Load(object sender, EventArgs e)
        {
            //取表位
            BwCount = GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_BWCOUNT, 24);
            CheckPlan = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan;
            LoadMeters();

            Chk_CheckAll.CheckValueChanged += delegate(object se, EventArgs args)
            {
                ClientTable.SelectAll(Chk_CheckAll.Checked);
            };

            label1.MouseDown += new MouseEventHandler(FormMove);
            label1.MouseDoubleClick += new MouseEventHandler(label1_MouseDoubleClick);
            this.Resize += new EventHandler(UI_Client_Resize);

            m_LoadOver = true;

            //设置监视器
            this.Controls.Add(UIMonitor);
            this.Controls.SetChildIndex(UIMonitor, 0);
            UIMonitor.Visible = true;

            showSchemeInfo();

            //设置状态
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Option == CLDC_CTNProtocol.EnumOption.BZ已连接)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SetOption(CLDC_CTNProtocol.EnumOption.CZ准备扫条码);
            }

            //正式模式 | 演示模式
            Lab_Mode.Text = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("ISDEMO");

            //使顶层窗体最大化
            SetParentMaximized();
            CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(SetParentMaximized), 200);
        }

        //加载表
        private void LoadMeters()
        {
            if (ClientTable == null || ClientTable.MeterNum != BwCount)
            {
                ClientTable = new CLDC_MeterUI.PublicControls.UI_ClientTable();
                ClientTable.MeterNum = BwCount;

                int RowNum = 6;

                for (int i = 6; i >= 4; i--)
                {
                    if (BwCount % i == 0)
                    {
                        RowNum = i;
                        break;
                    }
                }
                ClientTable.RowMeterNum = RowNum;            //每行6只表
                ClientTable.RowHeight = 25;
                ClientTable.TextCellFont = new Font("宋体", 10, FontStyle.Bold);
                ClientTable.RefreshGrid();
                //ClientTable.ReadOnly = true;
                this.GroupBox_Container.Controls.Add(ClientTable);
                ClientTable.Dock = DockStyle.Fill;
                ClientTable.Margin = new System.Windows.Forms.Padding(10);
                ClientTable.TxtInputOver += new CLDC_MeterUI.PublicControls.UI_ClientTable.Event_TxtInputOver(ClientTable_TxtInputOver);
                ClientTable.CheckOver += new CLDC_MeterUI.PublicControls.UI_ClientTable.Event_CheckOver(ClientTable_CheckOver);

            }

            //初始化其他控件
            {
                //按钮事件
                ButtonOk.Click += new EventHandler(ButtonOk_Click);
                ButtonRequest.Click += new EventHandler(ButtonRequest_Click);
                ButtonRequestControl.Click += new EventHandler(ButtonRequestControl_Click);
                ButtonSystemConfig.Click += new EventHandler(ButtonSystemConfig_Click);

                ButtonClose.Click += new EventHandler(ButtonClose_Click);

                labSchemeName.BackColor = labItem.BackColor = labAction.BackColor = Color.Transparent;
            }
            ShowData(false);
        }

        /// <summary>
        /// 输入框输入完毕事件
        /// </summary>
        /// <param name="Bwh">表位号(1-BW)</param>
        /// <param name="Value">输入值</param>
        private bool ClientTable_TxtInputOver(int Bwh, string Value)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            string strMessage;
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS == null) return false;
            if (Bwh < 1 || Bwh > BwCount)
            {
                return false;
            }
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo curMeter = CLDC_DataCore.Const.GlobalUnit.Meter(Bwh - 1);
            if (curMeter == null)
            {
                return false;
            }

            string ItemKey = "";
            if(CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID] is CLDC_DataCore.Struct.StPlan_ZouZi)
            {
                ItemKey = ((CLDC_DataCore.Struct.StPlan_ZouZi)CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID]).PrjID;
            }
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZError = null;
            //在这儿根据参数录入状态进行判断
            switch (m_VerifyStep)
            {
                case CLDC_Comm.Enum.Cus_stVerifyStep.参数录入:
                    {
                        //检查条形码是否重复
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo tmpMeter = null;
                        for (int i = 0; i < BwCount; i++)
                        {
                            if (i == Bwh - 1) continue;
                            tmpMeter = CLDC_DataCore.Const.GlobalUnit.Meter(i);
                            if (tmpMeter != null && Value == tmpMeter.Mb_ChrTxm)
                            {
                                strMessage = string.Format("条形码[{0}]已经在{1}表位存在!", Value, curMeter.ToString());
                                //CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
                                MessageBoxEx.Show(this,strMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                        //语音朗读条码后四位
                        strMessage = Value.Substring(Value.Length - 4, 4);
                       // CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
                        /*条码解析开始*/
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = null;
                        if (OnGetMeterInfo != null)
                            MeterInfo = OnGetMeterInfo(Value);
                        if (MeterInfo != null)
                        {
                            MeterInfo.SetBno(Bwh);
                            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1] = MeterInfo;
                            /*发送到服务器*/
                            CLDC_DataCore.Command.Model.SendMeterData_Ask cmdOneMeter = new CLDC_DataCore.Command.Model.SendMeterData_Ask();
                            //cmdOneMeter.MeterData = MeterInfo;
                            CLDC_CTNProtocol.CTNPCommand cmdResponse;
                            sendMessage(cmdOneMeter, out cmdResponse);
                            if (cmdResponse == null)
                            {
                                MessageBoxEx.Show(this,"上传数据到控制中心出错");
                                return false;
                            }
                        }
                        else
                        {
                            curMeter.Mb_ChrTxm = Value;
                            //上报服务器
                            CLDC_DataCore.Command.Txm.InputTxm_Update_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputTxm_Update_Ask();
                            CLDC_CTNProtocol.CTNPCommand cmdAnswer;
                            cmdAsk.Txm = Value;
                            cmdAsk.Bwh = Bwh - 1;

                            sendMessage(cmdAsk, out cmdAnswer);
                            if (cmdAnswer == null)
                            {
                                MessageBoxEx.Show(this,"操作失败！请重新扫描", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClientTable.SelectedBwh = Bwh;
                            }
                        }
                        GlobalUnit.g_MsgControl.OutMessage(string.Format("第表{0}位扫描完成", Bwh), true);
                        break;
                    }
                case CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录起码:
                    {
                        if (!curMeter.MeterZZErrors.ContainsKey(ItemKey))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("没有发现数据节点", false);
                            return false;
                        }
                        if (!CLDC_DataCore.Function.Number.IsNumeric(Value))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("请输入数字!", false);
                            return false;
                        }
                        GlobalUnit.g_MsgControl.OutMessage(Bwh + "输入完成!", false);
                        ZZError = curMeter.MeterZZErrors[ItemKey];
                        ZZError.Mz_chrQiMa = float.Parse(Value);
                        break;
                    }
                case CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录止码:
                    {
                        if (!curMeter.MeterZZErrors.ContainsKey(ItemKey))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("没有发现数据节点", false);
                            return false;
                        }
                        if (!CLDC_DataCore.Function.Number.IsNumeric(Value))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("请输入数字!", false);
                            return false;
                        }
                        GlobalUnit.g_MsgControl.OutMessage(Bwh + "输入完成!", false);

                        ZZError = curMeter.MeterZZErrors[ItemKey];
                        ZZError.Mz_chrZiMa = float.Parse(Value);
                        break;
                    }
                default:
                    {
                        MessageBoxEx.Show(this,"错误的状态：" + m_VerifyStep.ToString());
                        break;
                    }
            }
            return true;
        }
        /// <summary>
        /// 要检不检事件
        /// </summary>
        /// <param name="Bwh"></param>
        /// <param name="Value"></param>
        private void ClientTable_CheckOver(int Bwh, bool Value)
        {

            if (m_VerifyStep != CLDC_Comm.Enum.Cus_stVerifyStep.参数录入)
            {
                //不是参数录入，不允许切换要检或不检
                ClientTable.SetCheckBoxValue(Bwh, CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1].YaoJianYn);
                return;
            }

            if (CLDC_DataCore.Const.GlobalUnit.g_CUS != null && CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1] != null)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1].YaoJianYn = Value;
            }

            //通知服务器
            CLDC_DataCore.Command.Update.UpdateYaoJian_Ask cmdAsk = new CLDC_DataCore.Command.Update.UpdateYaoJian_Ask();
            cmdAsk.Bwh = Bwh - 1;
            cmdAsk.IsYaoJian = Value;
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = null;
            sendMessage(cmdAsk, out cmdAnswer);
        }

        private void ButtonClose_Click(object obj, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (MessageBoxEx.Show(this,"\r\n\r\n确定要退出吗?    \r\n\r\n", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void SetControlVisiable(object control, object visiblity)
        {
            this.Invoke(new CLDC_DataCore.Function.CallBack_Inc_Para2(SetControlVisiable_Invoke), control, visiblity);
        }

        private void SetControlVisiable_Invoke(object control, object visiblity)
        {
            if (control is Control)
            {
                ((Control)control).Visible = (bool)visiblity;
            }
        }

        #region ----------按钮事件----------

        private void SetLabText(Label lab, string txt)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventSetLabText_Invoke(SetLabText_Invoke), lab, txt);
            }
            else
            {
                lab.Text = txt;
            }
        }

        private delegate void EventSetLabText_Invoke(Label lab, string txt);

        private void SetLabText_Invoke(Label lab, string txt)
        {
            lab.Text = txt;
        }
        //请求扫条码按钮
        private void ButtonRequest_Click(object obj, EventArgs e)
        {
            //控制按钮,检定状态下不允许申请扫描条码
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定 &&
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.未赋值的)
            {
                return;
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoCommand_AskForInputTxm));
        }

        /// <summary>
        /// 请求扫条码操作
        /// </summary>
        /// <param name="objNull"></param>
        private void DoCommand_AskForInputTxm(object objNull)
        {
            string strMessage;
            MessageBoxEx.UseSystemLocalizedString = true;
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
            {
                MessageBoxEx.Show(this,string.Format("{0} 状态下不允许执行此操作!", CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState.ToString()), "操作逻辑错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.参数录入;

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("请求扫条码操作、正在等待服务器返回...");

            CLDC_DataCore.Command.Txm.InputTxm_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputTxm_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = new InputTxm_Answer();
            bool bResponse = true;
            sendMessage(cmdAsk, out cmdAnswer);
            if (cmdAnswer == null) //说明返回失败
            {
                bResponse = false;
            }
            else
            {
                bResponse = ((CLDC_DataCore.Command.Txm.InputTxm_Answer)cmdAnswer).bAgree;
            }
            //根据服务器返回结果设置界面可用状态
            //bResponse = true;

            ClientTable.ReadOnly = !bResponse;

            //修改状态
            if (bResponse)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SetOption(CLDC_CTNProtocol.EnumOption.DZ正在扫描条码);
                CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(-1);      //设置检定ID到参数录入
                SetControlVisiable(ButtonOk, true);

                //报告服务器，ActiveId已经改变
                CLDC_DataCore.Command.Update.UpdateActiveId_Ask cmdAsk2 = new CLDC_DataCore.Command.Update.UpdateActiveId_Ask();
                cmdAsk2.ActiveId = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID;
                CLDC_CTNProtocol.CTNPCommand cmdAnswer2;
                sendMessage(cmdAsk2, out cmdAnswer2);
            }

            //提示服务器返回的结果
            if (bResponse)
            {
                strMessage = "服务器同意你的扫条码操作、你现在可以录入条码";
                SetLabText(labItem, CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Option.ToString().Substring(2));
                SetLabText(labAction, strMessage);
                //CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID = -1;
                ShowData(true);
            }
            else
            {
                strMessage = "服务器不同意你的扫条码操作、请稍候再试";
                SetLabText(labItem, CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Option.ToString().Substring(2));
                SetLabText(labAction, strMessage);
               // CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }


        //录入条码完毕
        private void ButtonOk_Click(object obj, EventArgs e)
        {

            if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.参数录入)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DoCommand_ReportInputTxmComplated));
            }
            else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录起码 ||
                    m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录止码)
            {
                //检测是否所有表位都已经录入
                m_VerifyStep++;
                //向检定器发送消息
                if (OnInputOver != null)
                    OnInputOver(m_VerifyStep);
                CLDC_DataCore.Function.SetControl.SetVisible(ButtonOk, false);
            }
            else
            {
                return;
            }
            ClientTable.ReadOnly = true;
        }

        /// <summary>
        /// 报告录入条码完毕
        /// </summary>
        /// <param name="obj"></param>
        private void DoCommand_ReportInputTxmComplated(object obj)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在报告录入完毕事件...");

            CLDC_DataCore.Command.Txm.InputTxm_Complated_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputTxm_Complated_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnswer;
            bool bResponse = true; //sendMessage(cmdAsk, out cmdAnswer);
            cmdAsk.Option = CLDC_CTNProtocol.EnumOption.EZ扫描条码完毕;
            sendMessage(cmdAsk, out cmdAnswer);
            if (bResponse)
            {
                //修改状态
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SetOption(CLDC_CTNProtocol.EnumOption.EZ扫描条码完毕);
                SetControlVisiable(ButtonOk, false);
            }

            //提示服务器返回的结果
            if (bResponse)
            {
                SetLabText(labAction, "服务器已经成功收到请求，等待下一步操作...");
            }
            else
            {
                SetLabText(labAction, "网络出现错误!请重试");
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }


        //请求主动控制
        private void ButtonRequestControl_Click(object obj, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoCommand_AskForControlling));
        }

        /// <summary>
        /// 请求主动控制
        /// </summary>
        /// <param name="objNull"></param>
        private void DoCommand_AskForControlling(object objNull)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("请求主动控制操作、正在等待服务器返回...");
            CLDC_DataCore.Command.Controlling.RequestControlling_Ask cmdAsk = new CLDC_DataCore.Command.Controlling.RequestControlling_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = null;
            bool bResponse = true;
            if (SendMessage == null) return;
            SendMessage(cmdAsk, out cmdAnswer);
            if (cmdAnswer == null)
            {
                bResponse = false;
            }
            if (bResponse == true)
            {
                bResponse = ((CLDC_DataCore.Command.Controlling.RequestControlling_Answer)cmdAnswer).bAgree;
            }

            if (bResponse)
            {
                SetLabText(labAction, "服务器同意你主动控制的操作,等待切换...");
            }
            else
            {
                SetLabText(labAction, "服务器不同意你主动控制操作、请稍后再试");
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();

        }

        //系统配置按钮
        private void ButtonSystemConfig_Click(object sender, EventArgs e)
        {
            CLDC_MeterUI.UI_SystemManager _FrmSysConfig = new UI_SystemManager(GlobalUnit.g_SystemConfig);
            _FrmSysConfig.Show(true, true);
            sendMeterGoroup();
        }

        #endregion

        #region ----------功能辅助----------

        /// <summary>
        /// 显示运行信息
        /// </summary>
        /// <param name="LabShow">要显示的目标</param>
        /// <param name="Message">要显示的消息内容</param>
        private void ShowRunMessage(Label LabShow, string Message)
        {
            if (this.IsHandleCreated)
                this.BeginInvoke(new OnEventShowLabText(ShowLabText), new object[] { null, LabShow, Message });
        }

        //显示消息
        private void ShowLabText(object obj, Label labShow, string Message)
        {
            labShow.Text = Message;
        }
        //更新表位是否要检
        private void changeYaoJian(int BW, bool bYaoJian)
        {
            ClientTable.SetCheckBoxValue(BW + 1, bYaoJian);

        }
        #endregion

        #region ----------检定组件接口事件----------

        /// <summary>
        /// 显示方案信息
        /// </summary>
        private List<object> CheckPlan = new List<object>();        //方案列表 

        /// <summary>
        /// 显示方案信息
        /// </summary>
        private void showSchemeInfo()
        {
            int FirstYJIndex = CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter;
            if (FirstYJIndex == -1)
            {
                ShowRunMessage(labAction, "没有要检定的电能表");
                FirstYJIndex = 0;
            }

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo
                    firstMeter = CLDC_DataCore.Const.GlobalUnit.Meter(FirstYJIndex);

            string strFAName = string.Empty;                    //方案名称
            strFAName = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.FaName;
            CheckPlan = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan;

            if (firstMeter != null)
            {
                //显示方案名称
                ShowRunMessage(labSchemeName, strFAName);
                //显示检定项目名称
                if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID >= 0 &&
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID < CheckPlan.Count)
                {

                    ShowRunMessage(labItem, CheckPlan[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID].ToString());
                }
            }
        }

        /// <summary>
        /// 刷新显示客户端数据
        /// <param name="ShowDataOnly">是否只刷新数据区域</param>
        /// </summary>
        private void ShowData(bool ShowDataOnly)
        {
            if (!ShowDataOnly)
                CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(showSchemeInfo), 10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(thShowData));
            //控制按钮,检定状态下不允许申请扫描条码
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定 &&
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.未赋值的)
            {
                CLDC_DataCore.Function.SetControl.SetEnabled(ButtonRequest, false);
            }
            else
            {
                CLDC_DataCore.Function.SetControl.SetEnabled(ButtonRequest, true);
            }
        }


        object objShowDataLock = new object();
        private void thShowData(object obj)
        {
            CheckPlan = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan;
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo curMeter = null;
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS == null) return;
            lock (objShowDataLock)
            {
                bool isRead = false;
                string strKey = string.Empty;
                for (int bw = 0; bw < BwCount; bw++)
                {
                    //if (bw > BwCount) break;
                    string strMessageValue = string.Empty;
                    curMeter = CLDC_DataCore.Const.GlobalUnit.Meter(bw);
                    /*表格显示中的表位序号是从1开始*/
                    ClientTable.SetCheckBoxValue(bw + 1, curMeter.YaoJianYn);
                    if (!curMeter.YaoJianYn) continue;
                    if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID < 0 ||
                        CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
                    {
                        //参数录入状态下刷新时显示条形码
                        strMessageValue = curMeter.Mb_ChrTxm;
                    }
                    else
                    {
                        //数据验证
                        //if (curMeter.MeterPlan == null || curMeter.MeterPlan.CheckPlan == null) continue;
                        //if (CheckPlan.Count <= CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID) return;

                        strKey = "";
                        object curPlan = CheckPlan[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID];
                        if (curPlan is CLDC_DataCore.Struct.StPlan_ZouZi)
                        {
                            strKey = ((CLDC_DataCore.Struct.StPlan_ZouZi)curPlan).PrjID;
                        }
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult curResult = null;

                        #region 预热数据显示
                        if (curPlan is StPlan_YuRe)
                        {
                            strMessageValue = "预热中";
                        }
                        #endregion

                        #region 起动/启动数据
                        else if (curPlan is StPlan_QiDong)
                        {
                            strKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString("D3");
                            strKey += ((int)((StPlan_QiDong)curPlan).PowerFangXiang).ToString();

                            if (curMeter.MeterResults.ContainsKey(strKey))
                            {
                                curResult = curMeter.MeterResults[strKey];
                                strMessageValue = curResult.Mr_chrRstValue;
                                isRead = curResult.Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                            }
                            else
                            {
                                strMessageValue = "准备检定";
                            }
                        }
                        #endregion

                        #region 潜动试验显示
                        else if (curPlan is StPlan_QianDong)
                        {
                            strKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString("000");
                            strKey += ((int)((StPlan_QianDong)curPlan).PowerFangXiang).ToString();

                            if (curMeter.MeterResults.ContainsKey(strKey))
                            {
                                curResult = curMeter.MeterResults[strKey];

                                strMessageValue = curResult.Mr_chrRstValue;
                                isRead = curResult.Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                            }
                            else
                            {
                                strMessageValue = "准备检定";
                            }
                        }
                        #endregion

                        #region 基本误差/标准偏差
                        else if (curPlan is StPlan_WcPoint)
                        {
                            //strKey = "P_" + CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID;
                            StPlan_WcPoint _curPoint = (StPlan_WcPoint)curPlan;
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError curErroWc = null;
                            strKey = _curPoint.PrjID;
                            if (curMeter.MeterErrors.ContainsKey(strKey))
                            {
                                curErroWc = curMeter.MeterErrors[strKey];

                                string[] strErrorValue = curErroWc.Me_chrWcMore.Split('|');
                                if (strErrorValue.Length > 0)
                                {
                                    strMessageValue = strErrorValue[0];
                                }
                                if (curErroWc.Me_chrWcJl == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                                {
                                    isRead = true;
                                }
                            }
                        }

                        #endregion

                        #region ----------特殊检定-----------
                        else if (curPlan is StPlan_SpecalCheck)
                        {
                            //Comm.Struct.CheckPoint _curPoint = (Comm.Struct.CheckPoint)curPlan;
                            strKey = "P_" + CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID;
                            
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr curErroWc = null;
                            //strKey = _curPoint.PrjID;
                            if (curMeter.MeterSpecialErrs.ContainsKey(strKey))
                            {
                                curErroWc = curMeter.MeterSpecialErrs[strKey];

                                string[] strErrorValue = curErroWc.Mse_Wc.Split('|');
                                if (strErrorValue.Length > 0)
                                {
                                    strMessageValue = strErrorValue[0];
                                }
                                if (curErroWc.Mse_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                                {
                                    isRead = true;
                                }
                            }
                        }
                        #endregion

                        #region 走字数据
                        else if (curPlan is StPlan_ZouZi)
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError curZZerror = null;
                            if (curMeter.MeterZZErrors.ContainsKey(strKey))
                            {
                                curZZerror = curMeter.MeterZZErrors[strKey];
                                if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录起码 || m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录止码)
                                {
                                    strMessageValue = "";
                                }
                                else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录止码完毕)
                                {
                                    //止码
                                    strMessageValue = curZZerror.Mz_chrZiMa.ToString();
                                }
                                else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录起码完毕)
                                {
                                    //起码
                                    strMessageValue = curZZerror.Mz_chrQiMa.ToString();
                                }
                                else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.计算误差完毕)
                                {
                                    //起码
                                    strMessageValue = curZZerror.Mz_chrWc.ToString();
                                }
                                else
                                {
                                    //中途进入
                                    if (curZZerror.Mz_chrZiMa != -1)
                                        strMessageValue = curZZerror.Mz_chrZiMa.ToString();
                                    else if (curZZerror.Mz_chrQiMa != -1)
                                        strMessageValue = curZZerror.Mz_chrQiMa.ToString();
                                    else
                                        strMessageValue = "";

                                }
                                if (curZZerror.Mz_chrJL == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                                {
                                    isRead = true;
                                }
                            }
                        }
                        #endregion

                        #region 多功能数据
                        else if (curPlan is CLDC_DataCore.Struct.StPlan_Dgn)
                        {
                            strMessageValue = "检定中";
                            CLDC_DataCore.Struct.StPlan_Dgn DgnPlan = (CLDC_DataCore.Struct.StPlan_Dgn)curPlan;

                        }
                        #endregion

                        #region 载波数据
                        else if (curPlan is StPlan_Carrier)
                        {
                            strMessageValue = "检定中";
                            StPlan_Carrier CarrierPlan = (StPlan_Carrier)curPlan;
                        }
                        #endregion

                        else
                        {
                            //MUSTDO:走字，多功能检定客户端显示还没有做
                        }
                    }
                    //更新到UI
                    ClientTable.SetTextValue(bw + 1, strMessageValue);
                    ClientTable.SetTextBackColorValue(bw + 1, isRead);
                }
            }
        }

        #endregion

        #region----------检定控制，网络数据收发----------

        /// <summary>
        /// 发送整体模型
        /// </summary>
        private void sendMeterGoroup()
        {
            CLDC_DataCore.Command.Model.SendMeterGroup_Ask cmdAsk =
                new CLDC_DataCore.Command.Model.SendMeterGroup_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = null;
            //cmdAsk.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            sendMessage(cmdAsk, out cmdAnswer);
            if (cmdAnswer == null)
            {
                //TODO:服务器读数据库去
            }
        }
        /// <summary>
        /// 发送网络数据包
        /// </summary>
        /// <param name="cmdAsk"></param>
        /// <param name="cmdAnswer"></param>
        private void sendMessage(CLDC_CTNProtocol.CTNPCommand cmdAsk, out CLDC_CTNProtocol.CTNPCommand cmdAnswer)
        {
            if (SendMessage == null)
            {
                cmdAnswer = null;
                return;
            }
            SendMessage(cmdAsk, out cmdAnswer);
        }
        #endregion


        #region----------窗体拖曳----------
        public const int WM_SYSCOMMAND = 0x0112;
        [DllImport("user32.dll")]
        public extern static bool ReleaseCapture();
        private void FormMove(object sender, MouseEventArgs e)
        {
            Control hControl = this;
            while (hControl.Parent != null)
            {
                hControl = hControl.Parent;
            }

            if (((Office2007Form)hControl).WindowState == FormWindowState.Maximized) return;

            ReleaseCapture();
            CLDC_Comm.Win32Api.SendMessage(hControl.Handle.ToInt32(), WM_SYSCOMMAND, 0xF017, 0);

        }

        /// <summary>
        /// 双击切换最大化、最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control hControl = this;
            while (hControl.Parent != null)
            {
                hControl = hControl.Parent;
            }
            Office2007Form hFrmParent = (Office2007Form)hControl;
            if (hFrmParent.WindowState == FormWindowState.Maximized)
            {
                hFrmParent.WindowState = FormWindowState.Normal;
            }
            else
            {
                hFrmParent.WindowState = FormWindowState.Maximized;
            }
        }

        void SetParentMaximized()
        {
            Control hControl = this;
            while (hControl.Parent != null)
            {
                hControl = hControl.Parent;
            }
            Office2007Form hFrmParent = (Office2007Form)hControl;
            label1_MouseDoubleClick(label1, new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
            while (hFrmParent.WindowState != FormWindowState.Maximized)
            {
                label1_MouseDoubleClick(label1, new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
            }
        }

        #endregion

        #region ----------ITopUI 成员----------

        /// <summary>
        /// 设置台体表位数量
        /// </summary>
        private int m_bwCount = 0;
        public int BwCount
        {
            get
            {
                return m_bwCount;
            }
            set
            {
                m_bwCount = value;
            }
        }

        /// <summary>
        /// 台体编号
        /// </summary>
        private int m_TaiID = 0;
        public int TaiID
        {
            get
            {
                return m_TaiID;
            }
            set
            {
                m_TaiID = value;
            }
        }
        /// <summary>
        /// 设置外部顶层窗体
        /// </summary>
        private Office2007Form m_parentFrom;
        public Office2007Form ParentFormHandle
        {
            set { m_parentFrom = value as Office2007Form; }
            get { return m_parentFrom; }
        }
        /// <summary>= 
        /// 设置台体类型
        /// </summary>
        private CLDC_Comm.Enum.Cus_TaiType m_EquipType;
        public CLDC_Comm.Enum.Cus_TaiType EquipType
        {
            get
            {
                return m_EquipType;
            }
            set
            {
                m_EquipType = value;
            }
        }
        ///// <summary>
        ///// 刷新数据
        ///// </summary>
        ///// <param name="DnbData">电能表数据对象</param>
        //public void SetData( CLDC_DataCore.Model.DnbModel.DnbGroupInfo DnbData)
        //{
        //    ShowData(true);
        //    //throw new Exception("The method or operation is not implemented.");
        //}
        /// <summary>
        /// 只刷数据区域
        /// </summary>
        public void SetData()
        {
            ShowData(true);
        }

        /// <summary>
        /// 显示状态消息
        /// </summary>
        /// <param name="strMessage"></param>
        public void SetStatus(string strMessage)
        {
            ShowRunMessage(labAction, strMessage);
        }

        /// <summary>
        /// 普通消息队列处理
        /// </summary>
        /// <param name="sourceAdpater">消息发出者</param>
        /// <param name="VerifyDataArgs">消息参数</param>
        public void OnMsgMessage(object sourceAdpater, object VerifyDataArgs)
        {
            CLDC_Comm.MessageArgs.EventMessageArgs _Message = VerifyDataArgs as CLDC_Comm.MessageArgs.EventMessageArgs;
            if (_Message == null) return;
            int FirstYJMeter = CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter;
            _Message.Message = _Message.Message.Replace(@"\r\n", ";");
            switch (_Message.MessageType)
            {

                //检定点切换
                case CLDC_Comm.Enum.Cus_MessageType.检定跳点:
                    {
                        showSchemeInfo();
                        ShowRunMessage(labAction, "正在切换检定点...");
                        return;
                    }
                case CLDC_Comm.Enum.Cus_MessageType.运行时消息:
                    {
                        showSchemeInfo();
                        if (_Message.Message != "null")
                        {
                            //
                            ShowRunMessage(labAction, _Message.Message);
                        }
                        break;
                    }
                case CLDC_Comm.Enum.Cus_MessageType.检定完毕:
                    {
                        ShowRunMessage(labAction, _Message.Message);
                        break;
                    }

                default:
                    {
                        if (_Message.MessageType == CLDC_Comm.Enum.Cus_MessageType.录入电量起码 || _Message.MessageType == CLDC_Comm.Enum.Cus_MessageType.录入电量止码)
                        {
                            ShowData(true);             //先刷新一次显示区域数据 
                            CLDC_Comm.MessageArgs.EventMessageArgs _E = VerifyDataArgs as CLDC_Comm.MessageArgs.EventMessageArgs;
                            if (_E == null) return;
                            ShowData(false);
                            if (_E.Message == "null") return;
                            bool bQiMa = (_Message.MessageType == CLDC_Comm.Enum.Cus_MessageType.录入电量起码 ? true : false);
                            string strDes = string.Empty;
                            if (bQiMa)
                            {
                                strDes = "起码";
                                m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录起码;
                            }
                            else
                            {
                                strDes = "止码";
                                m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.走字试验录止码;
                            }
                            SetControlVisiable(ButtonOk, true);
                            ClientTable.ReadOnly = false;
                            ShowRunMessage(labAction, "请输入被检表的" + strDes + "");
                            MessageBoxEx.UseSystemLocalizedString = true;
                            MessageBoxEx.Show(this,_Message.Message + strDes + "后点击录入完成！", "系统提示");
                            break;
                        }
                        break;
                    }
            }
            //检测是否要刷新数据
            if (_Message.RefreshData)
            {
                ShowData(true);
            }
        }
        #endregion

        #region IMeterInfoUpdateDownEnablecs 成员

        public event CLDC_DataCore.Interfaces.GetMeterInfo OnGetMeterInfo;

        #endregion
    }
}
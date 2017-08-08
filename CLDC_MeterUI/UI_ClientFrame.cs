using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using CLDC_MeterUI;
using CLDC_Comm;
using CLDC_DataCore.Const;
using CLDC_Comm.Command;
using System.Threading;
using System.Drawing;
using CLDC_MeterUI.UI_Detection_New;
using System.Reflection;
using System.IO;

using CLDC_DataCore;

namespace CLDC_MeterUI
{
    public partial class UI_ClientFrame : Office2007Form
    {
        #region ------------变量声明------------

        //发送消息委托
        public delegate bool OnSendMessage(CLDC_CTNProtocol.CTNPCommand cmdAsk, out CLDC_CTNProtocol.CTNPCommand CmdAnswer);
        public event OnSendMessage SendMessage;
        //检定器操作委托
        public delegate void OnStartAdpater(int ItemID);
        public event OnStartAdpater StartAdpater;
        public delegate void OnStopAdpater();
        public event OnStopAdpater StopAdpater;
        public delegate void OnReadPara();
        public event OnReadPara ReadParaer;
        //下载电表信息操作委托
        public delegate void OnDownMeterInfoFromMis();
        public event OnDownMeterInfoFromMis DownMeterInfoer;
        //下载电表信息操作委托
        public delegate void OnDownSchemeInfoFromMis();
        public event OnDownSchemeInfoFromMis DownSchemeInfoer;
        //菜单委托操作(包括UI层操作下层对象方法)
        public delegate bool OnEventMenuClick(CLDC_Comm.Enum.Cus_MenuEventID EventID);
        public event OnEventMenuClick OnMenuClcik;
        //设置窗体标题
        private delegate void OnShowWindowText(string strText);
        private delegate void OnShowMonitor(CLDC_DataCore.Struct.StPower tagPower);
        //是否被控
        private bool m_IsBeControl = true;
        //主控窗体
        public Office2007RibbonForm m_MainControl = null;        
        //台体类型标识
        private int TaiType = -1;
        //台体ID标识
        private int TaiID = -1;
        //窗体切换
        private bool LoadMainControl = false;
        //刷新标识
        private bool isRefsh = false;


        //窗体接口
        //private ClInterface.UI.ITopUI m_MainUI;
        #endregion

        /// <summary>
        /// 监视器
        /// </summary>
        public CLDC_MeterUI.Monitor UIMonitor;
       
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = base.Text = string.Format("CL3000S-H 综合检定一体化平台客户端（{2}） - {0}号 【{1}】", TaiID, (TaiType == 0 ? "三相台" : "单相台"),Application.ProductVersion);
            }
        }

        public UI_ClientFrame()
        {
            InitializeComponent();
            MenuMain.Visible = false;
            //调整一下菜单栏所在布局表格的高度
            tableLayoutPanel1.RowStyles[0].Height = SystemInformation.MenuHeight;
            this.Load += new EventHandler(UI_ClientFrame_Load);
            this.Resize += new EventHandler(UI_ClientFrame_Resize);
        }

        void UI_ClientFrame_Resize(object sender, EventArgs e)
        {

            

            
            if (m_MainControl != null)
            {
                if (m_MainControl.Parent == panel1)
                {
                    this.panel1.Controls.Clear();

                    //DevComponents.DotNetBar.TabControl cts = new DevComponents.DotNetBar.TabControl();
                    //TabItem tbi1 = new TabItem();
                    //tbi1.Name = "第一台架";
                    //tbi1.Text = "第一台架";
                    //TabItem tbi2 = new TabItem();
                    //tbi2.Name = "第二台架";
                    //tbi2.Text = "第二台架";
                    //cts.Tabs.Add(tbi1);
                    //cts.Tabs.Add(tbi2);
                    //cts.Controls.Add(m_MainControl);
                    //cts.Controls.Add(m_MainControl2);
                    //tbi1.AttachedControl = m_MainControl;
                    //m_MainControl2.Width = this.Width;
                    //m_MainControl2.Height = this.Height-20;
                    //m_MainControl2.FormBorderStyle = FormBorderStyle.None;
                    //m_MainControl2.ShowInTaskbar = false;
                    //m_MainControl2.WindowState = FormWindowState.Normal;
                    //m_MainControl2.Parent = this;
                    //m_MainControl2.Dock = DockStyle.Fill;
                    //tbi2.AttachedControl = m_MainControl;
                    //cts.Dock = DockStyle.Fill;
                    //this.panel1.Controls.Add(cts);
                    this.panel1.Controls.Clear();
                    this.panel1.Controls.Add(m_MainControl);
                }
                else
                {
                    m_MainControl.Width = this.Width;
                    m_MainControl.Height = this.Height;

                    
                }
            }
        }

        #region----------等待层----------

        void UI_ClientFrame_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            ThreadPool.QueueUserWorkItem(new WaitCallback(thCancelTopMost));
        }

        void thCancelTopMost(object obj)
        {
            Thread.Sleep(5000);
            try
            {
                this.BeginInvoke(new MethodInvoker(CancelTopMost));
            }
            catch
            {
                //启动五秒以内关闭会出此例外
            }
        }

        void CancelTopMost()
        {
            this.TopMost = false;
        }
        #endregion

        #region ----------属性----------
        public bool IsBeControl
        {
            set
            {
                m_IsBeControl = value;
                CLDC_DataCore.Function.SetControl.InvokeWithNoParams(this, new CLDC_DataCore.Function.EventInvokeWithNoParams(createMainControl));
            }
            get
            {
                return m_IsBeControl;
            }
        }

        //获取被控制界面对象
        public CLDC_MeterUI.UI_Client BeControlUI
        {
            get
            {
                return m_MainControl as CLDC_MeterUI.UI_Client;
            }
        }
        //获取主控界面对象
        private Main ControlUI
        {
            get
            {
                return m_MainControl as Main;
            }
        }

        /// <summary>
        /// 设置网络状态
        /// </summary>
        public CLDC_Comm.Enum.Cus_NetState NetState
        {
            set
            {
                try
                {
                    if (m_MainControl is CLDC_MeterUI.UI_Client)
                    {
                        ((UI_Client)m_MainControl).NetState = value;
                    }
                    else if (m_MainControl is Main)
                    {
                        ((Main)m_MainControl).NetState = value;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    MessageBoxEx.Show(this,ex.Message, "[本框只有DEBUG时出现,211]出现错误、请查看日志", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }

        #endregion


        ////保存数据
        ////网络数据和主控分别调用
        public bool On_SaveData()
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            string outString;
            showRunMessage("开始保存检定数据");
            CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterData = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            #region ----------服务器保存----------
            bool isServer = CLDC_DataCore.Function.File.ReadInIString(Variable.CONST_MANAGERINI,"Server", "Run","0") == "1";
            bool isLocalSaveSuc = true;
            if (isServer)           //服务器访问
            {
                //先保存到服务器
                string sqlIP = GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_SQL_SERVERIP);
                string sqlID = GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_SQL_USERID);
                string sqlPass = GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_SQL_PASSWORD);
                // string sqlDBName = GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_SQL_DATABASENAME);

                if (!CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SaveToDataBase(sqlIP, sqlID, sqlPass, out outString))
                {
                    showRunMessage(outString);
                    MessageBoxEx.Show(this,"本地服务器上传失败,请确认服务器是否打开!");
                    isLocalSaveSuc = false;
                }
                if (isLocalSaveSuc == true)
                {
                    for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
                    {
                        CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[i].Mb_BlnToServer = true;
                    }
                }
            }
            
            {
                //本地保存
                //if (!CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SaveToDataBase(out outString))
                if (!CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SaveToSQL(CLDC_DataCore.Const.GlobalUnit.DBPathOfAccess,out outString,true))
                {
                    showRunMessage("本地数据库保存失败:错误信息：" + outString);
                    isLocalSaveSuc = false;
                }
            }
            if (!isLocalSaveSuc)
            {
            //    if (System.Windows.Forms.MessageBox.Show("本地数据库保存失败，系统一般不建议继续，是否继续将数据上传至MIS中间库？", "重要提示",
            //    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question,
            //    System.Windows.Forms.MessageBoxDefaultButton.Button1) != System.Windows.Forms.DialogResult.Yes)
            //    {
                return false;
            //    }
            }
            else
            {
            //    //if (System.Windows.Forms.MessageBox.Show("本地数据库保存成功，是否继续将数据上传至MIS中间库？", "重要提示",
            //    //    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question,
            //    //    System.Windows.Forms.MessageBoxDefaultButton.Button1) != System.Windows.Forms.DialogResult.Yes)
            //    {
                //return true;
            //    }
            }
            #endregion

            #region 上传到营销 审核存盘并直接上传至营销，也可通过CLDC_DataManager.exe
            //showRunMessage("开始上传数据到营销");
            //CLDC_DataCore.DataBase.clsDataManage DataManage = this.NewConnection();
            //if (DataManage == null)
            //{
            //    MessageBoxEx.Show(this,"数据库访问失败，请检查设置...\n【如果是服务器端，请选择信息设置中的服务器访问】", "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            //foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meter in meterData.MeterGroup)
            //{
            //    if (meter.YaoJianYn)
            //    {
            //        showRunMessage(string.Format("开始上传[{0}]", meter.Mb_ChrCcbh));
            //        #region 获取申请编号和meter_id
            //        string meter_id = "0";
            //        string bar_code = "";
            //        if (meter.Mb_ChrCcbh != null && meter.Mb_ChrCcbh != "")
            //        {
            //            if (!CLMisInterface.Common.DataManager.GetMeterIdFromSG186ByMeterCcbh(meter.Mb_ChrCcbh, out meter_id, out bar_code))
            //            {
            //                showRunMessage(string.Format("电能表[{0}]meter_id获取失败!", meter.Mb_ChrCcbh));
            //                showMessageBox(string.Format("电能表[{0}]meter_id获取失败!", meter.Mb_ChrCcbh));
            //                continue;
            //            }
            //            meter.Mb_ChrTxm = bar_code;
            //        }
            //        else
            //        {
            //            if (meter.Mb_ChrTxm != null && meter.Mb_ChrTxm != "")
            //            {
            //                if ((!CLMisInterface.Common.DataManager.GetMeterIdFromSG186ByMeterTxm(meter.Mb_ChrTxm, out meter_id)))
            //                {
            //                    showRunMessage(string.Format("电能表[{0}]meter_id失败!", meter.Mb_ChrCcbh));
            //                    showMessageBox(string.Format("电能表[{0}]meter_id失败!", meter.Mb_ChrCcbh));
            //                    continue;
            //                }
            //            }
            //            else
            //            {
            //                showRunMessage(string.Format("电能表[{0}]meter_id获取失败!", meter.Mb_ChrCcbh));
            //                showMessageBox(string.Format("电能表[{0}]meter_id获取失败!", meter.Mb_ChrCcbh));
            //                continue;
            //            }
            //        }
            //        #endregion

            //        #region 电能表基础数据
            //        CLMisInterface.Struct.CheckBasicData checkBasic = CLMisInterface.Common.DataManager.ProgressMeterCheckBasicData(meter, meter_id);
            //        if (!CLMisInterface.MisData.OraDataHelper.UpDataBasicData(checkBasic))
            //        {
            //            showRunMessage(string.Format("电能表[{0}]基础数据上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion

            //        #region 获取ReadID
            //        long readID = CLMisInterface.MisData.OraDataHelper.GetReadID();
            //        if (readID == 0)
            //        {
            //            showRunMessage(string.Format("电能表[{0}]检定记录上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion

            //        #region 电能表检定记录
            //        CLMisInterface.Struct.CheckRecords checkRecode = CLMisInterface.Common.DataManager.ProgressMeterCheckRecode(meter, meter_id);
            //        if (!CLMisInterface.MisData.OraDataHelper.UpDataCheckRecords(checkRecode, readID))
            //        {
            //            showRunMessage(string.Format("电能表[{0}]检定记录上传失败!", meter.Mb_ChrCcbh));
            //            showMessageBox(string.Format("电能表[{0}]检定记录上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion

            //        #region 电能表检定多功能检定项目
            //        CLMisInterface.Struct.Dgn_CheckRecords dgnCheckRecord = CLMisInterface.Common.DataManager.ProgressMeterDgnCheckRecords(meter, meter_id);
            //        if (!CLMisInterface.MisData.OraDataHelper.UpDataDgnCheckResult(dgnCheckRecord, readID))
            //        {
            //            showRunMessage(string.Format("电能表[{0}]多功能检定项目上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion

            //        #region 电能表检定结论
            //        CLMisInterface.Struct.CheckResult checkResult = CLMisInterface.Common.DataManager.ProgressMeterResult(meter, meter_id);
            //        if (!CLMisInterface.MisData.OraDataHelper.UpDataCheckResult(checkResult, readID))
            //        {
            //            showRunMessage(string.Format("电能表[{0}]检定结论上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion

            //        #region 电能表检定误差
            //        CLMisInterface.Struct.CheckErr[] checkErr = CLMisInterface.Common.DataManager.ProgressMeterErr(meter, meter_id);
            //        if (!CLMisInterface.MisData.OraDataHelper.UpDataCheckErr(checkErr, readID))
            //        {
            //            showRunMessage(string.Format("电能表[{0}]检定误差上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion

            //        #region 电能表走字记录
            //        CLMisInterface.Struct.ZZ_CheckRecords[] zzCheckRecord = CLMisInterface.Common.DataManager.ProgressMeterZZ(meter, meter_id);
            //        if (!CLMisInterface.MisData.OraDataHelper.UpDataCheckZZRecords(zzCheckRecord))
            //        {
            //            showRunMessage(string.Format("电能表[{0}]走字记录上传失败!", meter.Mb_ChrCcbh));
            //            continue;
            //        }
            //        #endregion
            //        DataManage.UpdateToMisOk(meter._intMyId, meter.Mb_TaiID);         //上传成功后需要修改标志
            //        showRunMessage(string.Format("电能表[{0}]检定记录上传成功!", meter.Mb_ChrCcbh));
            //    }
            //}
            #endregion
            //清理数据
            //showRunMessage("开始清理数据");
            
            //再次初始化数据
            //if (isLocalSaveSuc)
            //{
            //    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.NewMeters();
            //}
            showRunMessage("保存数据完成");
            return true;
        }

        
        #region-----------消息显示调度----------
        /// <summary>
        /// 显示运行信息
        /// </summary>
        /// <param name="str"></param>
        public void showRunMessage(string str)
        {
            try
            {
                if (IsBeControl)
                {
                    if (BeControlUI != null)
                        BeControlUI.SetStatus(str);
                }
                else
                {
                    if (ControlUI != null)
                    {
                        ControlUI.SetStatus(str);
                    }
                }
            }
            catch
            { }
        }
        /// <summary>
        /// 显示运行信息
        /// </summary>
        /// <param name="str"></param>
        public void showMessageBox(string str)
        {
            MessageBoxEx.Show(str);
        }
        #endregion

        #region----------显示监视器数据----------
        public void showMonitor(CLDC_DataCore.Struct.StPower tagPower)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new OnShowMonitor(dtShowMonitor), tagPower);
            }
        }
        //设置监视器数据
        private void dtShowMonitor(CLDC_DataCore.Struct.StPower tagPower)
        {
            if (IsBeControl)
            {
                if (BeControlUI != null)
                {
                    BeControlUI.UIMonitor.SetMonitorData(tagPower);
                }
            }
            else
            {
                if (ControlUI != null)
                {
                    ControlUI.UIMonitor.SetMonitorData(tagPower);
                }
            }
        }
        #endregion

        #region ---------检定器消息处理----------
        private delegate void OnEventInputDl(bool isQiMa);
        private void showInputdl(bool isQiMa)
        {
            ControlUI.Fun_InputZZNumber(isQiMa);
        }

        public void ControlAdpaterMessage(object source, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (IsBeControl)
            {
                if (BeControlUI != null)
                {
                    BeControlUI.OnMsgMessage(source, e);
                }
            }
            else
            {
                CLDC_Comm.MessageArgs.EventMessageArgs _E = e as CLDC_Comm.MessageArgs.EventMessageArgs;
                if (_E.Message == null) return;
                _E.Message = _E.Message.Replace(@"\r\n", ";");
                
                if (_E.RefreshData)
                {
                    MainControlProcess(false);
                }
                if (_E.Message == "null") return;
                if (_E.MessageType == CLDC_Comm.Enum.Cus_MessageType.录入电量起码 ||
                    _E.MessageType == CLDC_Comm.Enum.Cus_MessageType.录入电量止码)
                {
                    bool bQiMa = (_E.MessageType == CLDC_Comm.Enum.Cus_MessageType.录入电量起码 ? true : false);
                    string strDes = bQiMa ? "起码" : "止码";
                    this.Invoke(new OnEventInputDl(showInputdl), bQiMa);
                    MessageBoxEx.Show(this,_E.Message + " " + strDes + " 后点击确认录入按钮！");
                }
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new OnShowWindowText(setWindowText), _E.Message);
                }
            }
        }
        //设置提示消息
        private void setWindowText(string str)
        {
            if (ControlUI != null)
                ControlUI.SetStatus(str);
        }
        #endregion

        #region ----------检定数据处理----------
        //public void ControlAdpaterData(object obj, EventArgs e)
        //{
        //    if (IsBeControl)
        //    {
        //        BeControlUI.VerifyAdpater_VerifyData(obj, e);
        //    }
        //    else
        //    {
        //        MainControlProcess(false);
        //    }
        //}
        #endregion

        #region ----------辅助：创建被控制窗体----------
        private void createMainControl()
        {
            //清理切换前的对象
            if (m_MainControl != null)
            {
                LoadMainControl = true;
                Control ctrParent = m_MainControl.Parent;
                if (ctrParent != null)
                {
                    if (m_MainControl.Parent == this)
                        this.Controls.Remove(m_MainControl);
                    else if (m_MainControl.Parent == this.panel1)
                        this.panel1.Controls.Remove(m_MainControl);
                    m_MainControl.Dispose();
                }
            }
            LoadMainControl = false;
            //启动线程创建对象
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(thCreateMainControl));
        }

        private void thCreateMainControl(object obj)
        {
            if (IsBeControl)
            {
                //集中控制模式，显示客户端简化窗口
                if (m_MainControl != null && m_MainControl.IsHandleCreated && !m_MainControl.IsDisposed)
                {
                    this.Controls.RemoveAt(0);
                    m_MainControl.Dispose();
                }
                m_MainControl = new UI_Client();
                this.BeginInvoke(new EventAddMainControl(AddMainControl), false);
            }
            else
            {
                //脱机控制模式，显示客户端脱机控制窗口
                if (m_MainControl != null && m_MainControl.IsHandleCreated && !m_MainControl.IsDisposed)
                {
                    this.panel1.Controls.Clear();
                    m_MainControl.Dispose();
                }
                m_MainControl = new CLDC_MeterUI.UI_Detection_New.Main();     //第二种UI    
                m_MainControl.Visible = false;

                //m_MainControl2 = new CLDC_MeterUI.UI_Detection_New.Main();
                //m_MainControl2.TopLevel = false;
                this.BeginInvoke(new EventAddMainControl(AddMainControl), true);
            }

        }

        private delegate void EventAddMainControl(bool IsUI_Detection_Main);
        private void AddMainControl(bool IsUI_Detection_Main)
        {
            if (IsUI_Detection_Main)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;

                //fjkfjk this.MenuMain.Visible = true;
                this.tableLayoutPanel1.RowStyles[0].Height = 0;
                m_MainControl.FormBorderStyle = FormBorderStyle.None;
                m_MainControl.ShowInTaskbar = false;
                m_MainControl.WindowState = FormWindowState.Normal;

                #region -----事件----
                ControlUI.Evt_OnYaoJianChanged += new EventOnYaoJianChanged(On_MainControl_ChangeYaoJian);
                //挂新表事件
                ControlUI.Evt_OnHangUpNewMeter += new Event_OnHangUpNewMeter(On_MainControl_InitMeterData);
                //读取参数
                ControlUI.Evt_ReadPara += new Event_ReadPara(On_MainControl_ReadPara);
                // 参数录入完毕事件
                ControlUI.Evt_InputParam_OnOk += new Event_InputParam_OnOk(On_MainControl_InputParaOK);
                //从MIS中下载电表信息事件
                ControlUI.Evt_DownMeterInfoFromMis += new Event_DownMeterInfoFromMis(On_MainControl_DownMeterInfoFromMis);
                //从MIS中下载电表信息事件
                ControlUI.Evt_DwonSchemeInfoFromMis += new Event_DownSchemeInfoFromMis(On_MainControl_DownSchemeInfoFromMis);
                // 方案配置完毕事件
                ControlUI.Evt_LoadFA_OnOk += new Event_LoadFA_OnOk(On_MainControl_LoadFAOK);
                //检定跳点事件
                ControlUI.Evt_OnChangePoint += new Event_OnChangePoint(On_MainControl_ChangePoint);
                //检定跳点事件---单步检定
                ControlUI.Evt_OnStepStart += new Event_OnStepStart(On_MainControl_ChangePoint_Step);
                // 手工录入走字数据
                ControlUI.Evt_OnInputNumberEnd += new Event_OnInputNumberEnd(On_MainControl_InputPara);
                //保存数据
                ControlUI.Evt_OnAuditingSave += new Event_OnAuditingSave(On_MainControl_SaveData);
                // 调表
                ControlUI.OnCheckAdjust += new EventOnCheckAdjust(On_MainControl_CheckAdjust);
                // 调表
                ControlUI.OnProgrammingTipAdjust += new EventOnProgrammingTipAdjust(On_MainControl_ProgrammingTipAdjust);
                //停止
                ControlUI.OnCheckStop += new EventOnCheckStop(On_MainControl_Stop);
                //更新
                ControlUI.Evt_DataInfoChanged += new Event_DataInfoChanged(On_MainControl_DataInfoChange);
                //
                ControlUI.FaParmChanged += new EventFaParmChanged(On_MainControl_FangAnParaChange);
                // 释放
                ControlUI.Disposed += new EventHandler(m_MainControl_Disposed);
                //
                ControlUI.Evt_dlg_MenuClick += new dlg_MenuClick(raiseMenuevent);
                #endregion

                ((Office2007Form)m_MainControl).TopLevel = false;
                m_MainControl.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(m_MainControl);
                ((Control)m_MainControl).Visible = true;
                //ControlUI.UIMonitor.Normal = true;
                ControlUI.UIMonitor.DanXiangTai = (CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("DESKTYPE") == "单相台");
            }
            else
            {
                // ((UI_Client)m_MainControl).StartAdpater += new ClInterface.UI.OnEventStartAdpater(On_MainControl_StartAdpater);
                // ((UI_Client)m_MainControl).StopAdpater += new ClInterface.UI.OnEventStopAdpater(On_MainControl_StopAdpater);
                ((UI_Client)m_MainControl).SendMessage += new OnEventSendMessage(On_MainControl_SendMessage);
                ((UI_Client)m_MainControl).Disposed += new EventHandler(m_MainControl_Disposed);
                ((UI_Client)m_MainControl).OnInputOver += new UI_Client.OnEventInputOver(UI_ClientFrame_OnInputOver);
                //((UI_Client)m_MainControl).Visible = true;
                ((UI_Client)m_MainControl).ShowInTaskbar = false;
                this.MenuMain.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                // this.WindowState = FormWindowState.Maximized;

                ((Office2007Form)m_MainControl).WindowState = FormWindowState.Normal;
                ((Office2007Form)m_MainControl).TopLevel = false;
                m_MainControl.Dock = DockStyle.Fill;
                this.Controls.Add(m_MainControl);
                this.Controls.SetChildIndex(m_MainControl, 0);
                BeControlUI.UIMonitor.DanXiangTai = GlobalUnit.GetConfig(Variable.CTC_DESKTYPE, "单相台") == "单相台" ? true : false;
                ((Control)m_MainControl).Visible = true;
            }
            //CLDC_DataCore.Interfaces.IMeterInfoUpdateDownEnablecs IMisUpdate = m_MainControl as CLDC_DataCore.Interfaces.IMeterInfoUpdateDownEnablecs;
            //if (IMisUpdate != null)
            //{
            //    IMisUpdate.OnGetMeterInfo += new CLDC_DataCore.Interfaces.GetMeterInfo(IMisUpdate_OnGetMeterInfo);
            //}

            if (!IsBeControl)
            {
                MainControlProcess(false);
            }
            else
            {
                while (!BeControlUI.LoadOver)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
            //加载完毕，开始联机

            //raiseMenuevent(Comm.Enum.Cus_MenuEventID.联机);
        }
        /// <summary>
        /// 统一从从营销下载电能表基本修
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo IMisUpdate_OnGetMeterInfo(string barcode)
        //{
        //    //Comm.Interfaces.IDatatomis IDataToMis = LoadToMisInterface();
        //    //if (IDataToMis != null)
        //    //{
        //    //    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterinfo = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo();
        //    //    bool ret = IDataToMis.DownData(key, ref meterinfo);
        //    //    if (ret) return meterinfo;
        //    //}
        //    //return null;
        //    return CLMisInterface.MisData.OraDataHelper.GetMeterModelByMeterNumFromDongRuanSG186(barcode);

        //}
        /// <summary>
        /// 加载营销接口组件
        /// </summary>
        /// <returns></returns>
        private CLDC_DataCore.Interfaces.IDatatomis LoadToMisInterface()
        {
            foreach (string Item in System.IO.Directory.GetFiles(CLDC_DataCore.Function.File.GetPhyPath("Plugins")))
            {
                if (CLDC_DataCore.Function.File.GetExtName(Item).ToLower() == ".dll")
                {
                    System.Reflection.Assembly asb;
                    try
                    {
                        asb = System.Reflection.Assembly.LoadFile(Item);
                    }
                    catch { continue; }
                    Type[] types = asb.GetTypes();

                    foreach (Type TypeItem in types)
                    {
                        if (TypeItem.GetInterface("IDatatomis") != null)
                        {
                            return (CLDC_DataCore.Interfaces.IDatatomis)Activator.CreateInstance(TypeItem);       //创建实例
                        }
                    }

                }
            }

            return null;
        }
        private void UI_ClientFrame_OnInputOver(CLDC_Comm.Enum.Cus_stVerifyStep State)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.录入电量完毕);
        }
        #endregion

        #region------MainControl(主控)检定事件------
        //要检切换
        private bool On_MainControl_ChangeYaoJian(int TaiType, int TaiID, int Bw, bool bYaoJian)
        {
            if (Bw < 0 || Bw > CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws) return false;

            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bw].YaoJianYn = bYaoJian;
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.UpdataMeterIsYaoJian(Bw,bYaoJian );
            MainControlProcess(false);

            //向服务器 报告
            CLDC_DataCore.Command.Update.UpdateYaoJian_Ask cmdAsk = new CLDC_DataCore.Command.Update.UpdateYaoJian_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            cmdAsk.IsYaoJian = bYaoJian;
            cmdAsk.Bwh = Bw;
            sendMessage(cmdAsk, out cmdAnser);

            return true;
        }
        //挂新表

        private bool On_MainControl_InitMeterData(int taiType, int taiId)
        {

            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.NewMeters();
            if ("外部可执行程序接口" == CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_MIS_INTERFACETYPE, ""))
            {
                string msi_exeName=GlobalUnit.GetConfig(Variable.CTC_MIS_EXENAME, "");
                string mis_exeProcessName=GlobalUnit.GetConfig(Variable.CTC_MIS_EXEPROCESSNAME, "");
                if (!string.IsNullOrEmpty(msi_exeName))
                {
                    CLDC_DataCore.Function.File.RunOtherExe(msi_exeName, mis_exeProcessName);
                }
            }
            MainControlProcess(true);
            return true;
        }

        //参数录入完毕
        private bool On_MainControl_InputParaOK(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            //检测测量方式和当前台体类型是否匹配
            CLDC_DataCore.Model.DnbModel.DnbGroupInfo oldGroud = null;
            string _DeskType = GlobalUnit.GetConfig(Variable.CTC_DESKTYPE, "单相台");


            meterGroup.CheckProgressIndex = -1;
            oldGroud = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = meterGroup;
            if (_DeskType == "单相台")
            {
                if (CLDC_DataCore.Const.GlobalUnit.Clfs != CLDC_Comm.Enum.Cus_Clfs.单相)
                {
                    GlobalUnit.g_MsgControl.OutMessage("参数录入有误：\r\n描述：当前电能表测量方式与台体类型不符合！单相台只能选择测量方式为[单相]\r\n错误来源:当前台体类型为单相台，但是电能表测量方式不是单相\r\n解决办法:请修改台体类型或是电能表测量方式。", false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
                    Thread.Sleep(300);
                    //还原模型
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = oldGroud;
                    CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(-2);            //老版本使用，方案配置
                    return false;
                }
            }

            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Save();
            

            //GlobalUnit.UpdateActiveID(-2);            //老版本使用，方案配置

            CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(0);           //新版本，默认放到第一个检定项目

            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.NowMinute = 0;
            //}
            //通知主窗体
            MainControlProcess(true);
            //发送给服务器
            //sendMeterData();

            //向服务器 报告
            CLDC_DataCore.Command.Txm.InputParam_Complated_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputParam_Complated_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            //cmdAsk.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            sendMessage(cmdAsk, out cmdAnser);

            return true;
        }
        //方案加载成功
        private bool On_MainControl_LoadFAOK(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            meterGroup.CheckProgressIndex = -2;
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = meterGroup;
            CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(0);
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.NowMinute = 0;
            // MainControlProcess(true);

            //向服务器 报告
            CLDC_DataCore.Command.Plan.CreatePlan_Complated_Ask cmdAsk = new CLDC_DataCore.Command.Plan.CreatePlan_Complated_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            //cmdAsk.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            sendMessage(cmdAsk, out cmdAnser);
            return true;
        }
        //方案加载成功
        private bool On_MainControl_FangAnParaChange(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = meterGroup;
            MainControlProcess(true);
            return true;
        }
        //跳点
        private bool On_MainControl_ChangePoint(int prjid, int taiType, int taiId)
        {
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState = CLDC_Comm.Enum.Cus_CheckStaute.检定;//状态控制 2013-11-05
            startAdpater(prjid);
            MainControlProcess(false);

            //向服务器 报告
            CLDC_DataCore.Command.CheckMeter.ChangePoint_Ask cmdAsk = new CLDC_DataCore.Command.CheckMeter.ChangePoint_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            cmdAsk.ActiveID = prjid;
            sendMessage(cmdAsk, out cmdAnser);

            return true;
        }
        //单步检定
        private bool On_MainControl_ChangePoint_Step(int prjid, int taiType, int taiId)
        {
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState = CLDC_Comm.Enum.Cus_CheckStaute.单步检定;
            startAdpater(prjid);
            MainControlProcess(false);

            //向服务器 报告
            CLDC_DataCore.Command.Messages.CheckMessage_Ask cmdAsk = new CLDC_DataCore.Command.Messages.CheckMessage_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            cmdAsk.checkState = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState;
            sendMessage(cmdAsk, out cmdAnser);

            return true;
        }
        //手动录入参数:起码止码
        private bool On_MainControl_InputPara(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int TaiType, int TaiID)
        {
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = meterGroup;
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.录入电量完毕);
            //sendMeterData();
            return true;
        }
        //保存数据
        private bool On_MainControl_SaveData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int TaiType, int TaiID)
        {
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = meterGroup;
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Save();
            bool bSaveSuccess = On_SaveData();
            if (bSaveSuccess)
            {
                //删除掉Pulse.xml
                if (System.IO.File.Exists(CLDC_DataCore.Function.File.GetPhyPath(CLDC_DataCore.Const.Variable.CONST_PULSETYPE)))
                    System.IO.File.Delete(CLDC_DataCore.Function.File.GetPhyPath(CLDC_DataCore.Const.Variable.CONST_PULSETYPE));
                MainControlProcess(true);
            }
            return bSaveSuccess;
        }
        //调表
        private bool On_MainControl_CheckAdjust(bool IsTiaoBiao, int taiType, int taiId)
        {
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定 ||
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.未赋值的)
            {
                return false;
            }
            if (IsTiaoBiao)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState |= CLDC_Comm.Enum.Cus_CheckStaute.调表;
            }
            else
            {
                //状态还原
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState &= ~CLDC_Comm.Enum.Cus_CheckStaute.调表;
            }

            //向服务器 报告
            CLDC_DataCore.Command.CheckMeter.CheckAdjust_Ask cmdAsk = new CLDC_DataCore.Command.CheckMeter.CheckAdjust_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            cmdAsk.IsAdjust = IsTiaoBiao;
            sendMessage(cmdAsk, out cmdAnser);
            GlobalUnit.g_MsgControl.OutMessage();
            return true;
        }
        //编程提示
        private bool On_MainControl_ProgrammingTipAdjust(bool IsTiaoBiao, int taiType, int taiId)
        {
            //if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定 ||
            //    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.未赋值的)
            //{
            //    return false;
            //}
            CLDC_DataCore.Struct.StProgrammingTip stpt=new CLDC_DataCore.Struct.StProgrammingTip();
            if (IsTiaoBiao)
            {
                
                stpt.isTip=true;
                GlobalUnit.Tip_Programming = stpt;
            }
            else
            {
                //状态还原
                stpt.isTip = false;
                GlobalUnit.Tip_Programming = stpt;
            }

            //TODO:向服务器 报告调表消息
            //CLDC_DataCore.Command.CheckMeter.CheckAdjust_Ask cmdAsk = new CLDC_DataCore.Command.CheckMeter.CheckAdjust_Ask();
            //CLDC_CTNProtocol.CTNPCommand cmdAnser;
            //cmdAsk.IsAdjust = IsTiaoBiao;
            //sendMessage(cmdAsk, out cmdAnser);
            //GlobalUnit.g_MsgControl.OutMessage();
            return true;
        }
        //停止检定
        private bool On_MainControl_Stop(int TaiType, int TaiID)
        {
            stopAdpater();
            //this.BeginInvoke(new OnShowWindowText(setWindowText), "检定已经停止！");

            //向服务器 报告
            CLDC_DataCore.Command.CheckMeter.CheckStop_Ask cmdAsk = new CLDC_DataCore.Command.CheckMeter.CheckStop_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            sendMessage(cmdAsk, out cmdAnser);

            return true;
        }
        /// <summary>
        /// 下载电表信息
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="TaiType"></param>
        /// <param name="TaiID"></param>
        /// <returns></returns>
        public bool On_MainControl_DownSchemeInfoFromMis(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int TaiType, int TaiID)
        {
            if (DownSchemeInfoer != null)
                DownSchemeInfoer();
            //向服务器 报告
            //CLDC_DataCore.Command.DownSchemeInfo.DownSchemeInfo_Ask cmdAsk = new CLDC_DataCore.Command.DownSchemeInfo.DownSchemeInfo_Ask();
            //CLDC_CTNProtocol.CTNPCommand cmdAnser;
            //cmdAsk.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            //sendMessage(cmdAsk, out cmdAnser);
            MainControlProcess(true);

            return true;
        }
        /// <summary>
        /// 下载电表信息
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="TaiType"></param>
        /// <param name="TaiID"></param>
        /// <returns></returns>
        public bool On_MainControl_DownMeterInfoFromMis(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int TaiType, int TaiID)
        {
            if (DownMeterInfoer != null)
                DownMeterInfoer();
            ////向服务器 报告
            //CLDC_DataCore.Command.DownMeterInfo.DownMeterInfo_Ask cmdAsk = new CLDC_DataCore.Command.DownMeterInfo.DownMeterInfo_Ask();
            //CLDC_CTNProtocol.CTNPCommand cmdAnser;
            //cmdAsk.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            //sendMessage(cmdAsk, out cmdAnser);
            MainControlProcess(true);

            return true;
        }
        /// 读取参数
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="TaiType"></param>
        /// <param name="TaiID"></param>
        /// <returns></returns>
        public bool On_MainControl_ReadPara(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int TaiType, int TaiID)
        {
            ReadPara();
            //检测测量方式和当前台体类型是否匹配
            CLDC_DataCore.Model.DnbModel.DnbGroupInfo oldGroud = null;
            string _DeskType = GlobalUnit.GetConfig(Variable.CTC_DESKTYPE, "单相台");


            meterGroup.CheckProgressIndex = -1;
            oldGroud = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = meterGroup;
            if (_DeskType == "单相台")
            {
                if (CLDC_DataCore.Const.GlobalUnit.Clfs != CLDC_Comm.Enum.Cus_Clfs.单相)
                {
                    GlobalUnit.g_MsgControl.OutMessage("参数录入有误：\r\n描述：当前电能表测量方式与台体类型不符合！单相台只能选择测量方式为[单相]\r\n错误来源:当前台体类型为单相台，但是电能表测量方式不是单相\r\n解决办法:请修改台体类型或是电能表测量方式。", false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
                    Thread.Sleep(300);
                    //还原模型
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData = oldGroud;
                    CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(-2);            //老版本使用，方案配置
                    return false;
                }
            }
            //向服务器 报告
            CLDC_DataCore.Command.ReadPara.ReadPara_Ask cmdAsk = new CLDC_DataCore.Command.ReadPara.ReadPara_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnser;
            //cmdAsk.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            sendMessage(cmdAsk, out cmdAnser);

            return true;
        }
        //局部数据更新
        //public delegate void On_UpdateData(string strKey, object objValue, int bw);
        //public event On_UpdateData OnUpdateData;
        private bool On_MainControl_DataInfoChange(string PropertyName, object ChangeValue, int Bwh, int taiType, int taiId)
        {
            CLDC_DataCore.Update.UpdateVerifyData updateData = new CLDC_DataCore.Update.UpdateVerifyData();
            updateData.DnbGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            CLDC_DataCore.Command.Update.UpdateData_Ask
               Cmd_UpdateData = new CLDC_DataCore.Command.Update.UpdateData_Ask();
            CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(-3);
            if (Bwh != 999)
            {
                Cmd_UpdateData.BW = Bwh - 1;
            }
            else
            {
                Cmd_UpdateData.BW = Bwh;
            }
            Cmd_UpdateData.isDelete = false;
            Cmd_UpdateData.strKey = new string[] { PropertyName };
            Cmd_UpdateData.objData = new object[] { ChangeValue };
            Cmd_UpdateData.DataType = CLDC_Comm.Enum.Cus_MeterDataType.基本信息;
            //发送给服务器
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = new CLDC_CTNProtocol.CTNPCommand();
            sendMessage(Cmd_UpdateData, out cmdAnswer);
            if (cmdAnswer != null)
            {
                //TODO:服务器读取临时库
            }
            //本地更新
            bool changeSuccess = updateData.UpdateData(Cmd_UpdateData);
            MainControlProcess(false);
            return changeSuccess;
        }

        //MainControl释放
        private void m_MainControl_Disposed(object sender, EventArgs e)
        {
            if (LoadMainControl)
                return;
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
            this.Dispose();
            //m_MainControl = null;
        }



        /// <summary>
        /// 通知控制窗体数据发生变化
        /// </summary>
        public void MainControlProcess(bool sendMeterDataToServer)
        {
            if (sendMeterDataToServer)
            {
                sendMeterData();
            }
            //通知主控窗体
            if (ControlUI != null)
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    //try
                    //{
                    this.BeginInvoke(new MethodInvoker(UpdateMainControlData));
                    //}
                    //catch(Exception ex)
                    //{
                    //Comm.Function.ErrorLog.Write(ex);
                    //}
                }
            }
            else
            {/*防止切换时出现真空*/
                //if (BeControlUI == null) return;
                //if (this.IsHandleCreated && !this.IsDisposed && BeControlUI.IsHandleCreated && !BeControlUI.IsDisposed)
                //{
                //TODO:Modify
                //this.Invoke(new MethodInvoker(BeControlUI.SetData));
                //}
            }
        }
        //委托函数
        private object objSetDataLock = new object();
        private void UpdateMainControlData()
        {

            if (TaiID == -1)
                TaiID = int.Parse(GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_DESKNO));
            if (TaiType == -1)
            {
                string strTmp = GlobalUnit.g_SystemConfig.SystemMode.getValue(Variable.CTC_DESKTYPE);
                TaiType = strTmp == "三相台" ? 0 : 1;
            }
            /*
                09/14/2009 11-46-34  By Dy
                内容说明：
                防止多个线程同时刷数据
            */
            lock (objSetDataLock)
            {
                if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID >= 0)
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckProgressIndex = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID;
                CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroupCopy = (CLDC_DataCore.Model.DnbModel.DnbGroupInfo)CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Copy();
                if (ControlUI != null)
                {
                    if (this.isRefsh)
                    {
                        Console.WriteLine("上一次数据刷新还没有完成，放弃");
                        return;
                    }
                    isRefsh = true;
                    try
                    {
                        ControlUI.SetData(CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData, TaiType, TaiID);
                    }
                    catch (Exception e)
                    {
                        //调试状态下记录检定数据
                        string logPath = "/Log/UI/UI_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                        GlobalUnit.Log.WriteLog(logPath, this, "UpdateMainControlData", e.Message + e.StackTrace.ToString());
                    }
                    finally
                    {
                        isRefsh = false;
                    }
                }
            }
            this.Text = "运行状态:" + CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState.ToString();
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定
                && CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.未赋值的)
            {
                this.Text += " 项目编号:" + ((int)(CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID + 1)).ToString();
            }

        }
        #endregion

        #region ----------MainControl(被控)检定事件----------
        private bool On_MainControl_SendMessage(CLDC_CTNProtocol.CTNPCommand cmdAsk, out CLDC_CTNProtocol.CTNPCommand cmdAnswer)
        {
            return sendMessage(cmdAsk, out cmdAnswer);
        }

        private void On_MainControl_StopAdpater()
        {
            stopAdpater();
        }

        private void On_MainControl_StartAdpater(int ItemID)
        {
            startAdpater(ItemID);
        }
        #endregion

        #region ----------辅助:外发事件----------
        //网络数据外发
        private bool sendMessage(CLDC_CTNProtocol.CTNPCommand cmdAsk, out CLDC_CTNProtocol.CTNPCommand cmdAnswer)
        {
            cmdAnswer = null;
            if (SendMessage == null) return false;
            return SendMessage(cmdAsk, out cmdAnswer);
        }
        //启动检定器
        private void startAdpater(int ItemID)
        {
            if (StartAdpater != null)
            {
                StartAdpater(ItemID);
            }
        }
        //停止检定器
        private void stopAdpater()
        {
            if (StopAdpater != null)
            {
                StopAdpater();
                //MainControlProcess(false);
            }
        }
        private void ReadPara()
        {
            if (ReadParaer != null)
                ReadParaer();
        }
        //发送电能表数据模型给服务器
        private void sendMeterData()
        {
            CLDC_DataCore.Command.Model.SendMeterGroup_Ask
                Cmd_SendMeterData = new CLDC_DataCore.Command.Model.SendMeterGroup_Ask();
            //Cmd_SendMeterData.MeterGroup = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData;
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = null;
            sendMessage(Cmd_SendMeterData, out cmdAnswer);
            if (cmdAnswer != null)
            {
                //TO DoSomething
            }
        }
        //菜单事件外发
        private void raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID eventID)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(thMenuEvent), eventID);
        }
        private void thMenuEvent(object eventID)
        {
            if (OnMenuClcik != null)
            {
                CLDC_Comm.Enum.Cus_MenuEventID eID = (CLDC_Comm.Enum.Cus_MenuEventID)eventID;
                OnMenuClcik(eID);
            }
        }
        #endregion

        #region----------菜单事件----------

        #region ----------系统菜单----------
        //系统配置
        private void Menu_SystemConfig_SysConfig_Click(object sender, EventArgs e)
        {
            
            //重新发送固定参数到服务器
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.系统配置);
        }
        //联机
        private void Menu_SystemConfig_Link_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.联机);
        }
        //脱机
        private void Menu_SystemConfig_UnLink_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.脱机);
        }
        //升源
        private void Menu_SystemConfig_PowerOn_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.升源);
        }
        //关源
        private void Menu_SystemConfig_PowerOff_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.关源);
        }
        //退出
        private void Menu_SystemConfig_Exit_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
            {
                MessageBoxEx.AntiAlias = true;
                MessageBoxEx.EnableGlass = true;
                MessageBoxEx.UseSystemLocalizedString = true;
                if (MessageBoxEx.Show(this,"当前还在检定操作中,退出后会影响操作.\n确定要退出系统吗?", "确定提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    stopAdpater();

                    this.Dispose();
                    // return;
                    raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.退出);
                    return;
                }
            }
            //this.Dispose();
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.退出);
        }
        #endregion

        #region ----------方案管理----------
        /// <summary>
        /// 总方案--->单相
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_SchemeManage_Total_DanXiang_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.User_Jyy.Level != 0)
            {
                MessageBoxEx.UseSystemLocalizedString = true;
                MessageBoxEx.Show(this,"当前用户权限不足，请联系管理员", "系统提示");
                return;
            }
            CLDC_MeterUI.UI_FA.Frm_FaSetup _S = new CLDC_MeterUI.UI_FA.Frm_FaSetup(CLDC_Comm.Enum.Cus_TaiType.单相台);
            _S.ShowDialog();
            if (_S != null)
            {
                if (_S.newFAName.Length > 0)
                    UpdateFaAn(_S.newFAName);
            }
        }
        /// <summary>
        /// 总方案--->三相
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_SchemeManage_Total_SanXiang_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.User_Jyy.Level != 0)
            {
                MessageBoxEx.UseSystemLocalizedString = true;
                MessageBoxEx.Show(this,"当前用户权限不足，请联系管理员", "系统提示");
                return;
            }
            CLDC_MeterUI.UI_FA.Frm_FaSetup _S = new CLDC_MeterUI.UI_FA.Frm_FaSetup(CLDC_Comm.Enum.Cus_TaiType.三相台);
            _S.ShowDialog();
            if (_S != null)
            {
                if (_S.newFAName.Length > 0)
                    UpdateFaAn(_S.newFAName);
            }
        }
        public void UpdateScheme(string curScheme)
        {
            if (curScheme.Length > 0)
            {
                UpdateFaAn(curScheme);
            }
        }
        /// <summary>
        /// 更新方案
        /// </summary>
        /// <param name="newFanName"></param>
        private void UpdateFaAn(string newFanName)
        {
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID < 0)
            {
                //MessageBoxEx.UseSystemLocalizedString = true;
                //MessageBoxEx.Show(this,"请先录入表参数后点击录入完成", "系统提示");
                GlobalUnit.g_MsgControl.OutMessage("请先录入表参数后点击录入完成",false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
                return;
            }

            if (!IsBeControl)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CreateFA(TaiType, newFanName);
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckFAChangeAndDataRefrash();
                ControlUI.ReSetSchemeList = true;
                MainControlProcess(true);
            }
        }
        ////预热
        //private void Menu_SchemeManage_PreHot_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_YuRe _S = new UI_FA_YuRe(1);
        //    _S.ShowDialog();
        //}

        //private void Menu_SchemeManage_PreHot_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_YuRe _S = new UI_FA_YuRe(0);
        //    _S.ShowDialog();

        //}

        //UI.UI_FA.UI_FA_WcLimit m_WcLimit = null;
        //private void Menu_BasicError_WuChaXian_Click(object sender, EventArgs e)
        //{
        //    if (m_WcLimit==null)
        //    {
        //        m_WcLimit = new UI.UI_FASetup.UI_FA_WcLimit();
        //    }
        //    m_WcLimit.Show();
        //}
        //private void Menu_SchemeManage_Start_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_QiDong _S = new UI_FA_QiDong(1);
        //    _S.ShowDialog();
        //}
        ////启动
        //private void Menu_SchemeManage_Start_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_QiDong _S = new UI_FA_QiDong(0);
        //    _S.ShowDialog();

        //}
        //private void Menu_SchemeManage_Creep_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_QianDong _S = new UI_FA_QianDong(1);
        //    _S.ShowDialog();
        //}

        //private void Menu_SchemeManage_Creep_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_QianDong _S = new UI_FA_QianDong(0);
        //    _S.ShowDialog();

        //}
        //基本误差
        //private void Menu_SchemeManage_BasicError_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_WC _S = new UI_FA_WC(1);
        //    _S.ShowDialog();
        //}
        //private void Menu_SchemeManage_BasicError_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_WC _S = new UI_FA_WC(0);
        //    _S.ShowDialog();

        //}
        ////多功能
        //private void Menu_SchemeManage_DGN_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_Dgn _S = new UI_FA_Dgn(1);
        //    _S.ShowDialog();
        //}

        //private void Menu_SchemeManage_DGN_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_Dgn _S = new UI_FA_Dgn(0);
        //    _S.ShowDialog();

        //}
        ////走字单相
        //private void Menu_SchemeManage_ConstantVerify_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_ZouZi _S = new UI_FA_ZouZi(1);
        //    _S.ShowDialog();
        //}
        ////走字三相
        //private void Menu_SchemeManage_ConstantVerify_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_ZouZi _S = new UI_FA_ZouZi(0);
        //    _S.ShowDialog();

        //}
        ////特殊检定单相
        //private void Menu_SchemeManage_Special_DanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_Special _S = new UI_FA_Special(1);
        //    _S.ShowDialog();
        //}
        ////特殊检定
        //private void Menu_SchemeManage_Special_SanXiang_Click(object sender, EventArgs e)
        //{
        //    UI.UI_FA_Special _S = new UI_FA_Special(0);
        //    _S.ShowDialog();

        //}

        #endregion

        #region ----------工具----------
        //接线测试
        private void Menu_Tools_CheckMeterUI_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.表位接线测试);
        }
        private void Menu_Tools_ReadStatus_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.读取设备状态);
        }
        /// <summary>
        /// 显示端口配置窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Tools_SetPort_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.RS485通道配置);
        }
        /// <summary>
        /// 设备通道配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Tools_ComAdapter_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.配置设备端口);
        }
        /// <summary>
        /// 显示通讯数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Tools_ShowData_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.显示通讯数据);
        }
        /// <summary>
        /// 谐波设置
        /// </summary>
        private void Menu_Tools_XieBo_Click(object sender, EventArgs e)
        {
            new CLDC_MeterUI.UI_FA.Frm_XieBoSetup().Show();
        }
        /// <summary>
        /// 多功能协议配置
        /// 在框架上启用协议配置需要先脱机后才能操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Tools_DgnProtocolSetup_Click(object sender, EventArgs e)
        {
            raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.多功能协议配置);
        }

        #endregion

        #region ----------查看----------
        /// <summary>
        /// 数据管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tool_DataManage_Click(object sender, EventArgs e)
        {
            string dataPath = GlobalUnit.GetConfig(Variable.CTC_OTHER_DATAMANAGEEXEPATH, "/CLDC_DataManager.exe");
            string dataProcess = GlobalUnit.GetConfig(Variable.CTC_OTHER_DATAMANAGEPROCESSNAME, "CLDC_DataManager");
            CLDC_DataCore.Function.File.RunOtherExe(dataPath, dataProcess);
        }

        #endregion

        #region----------其它----------
        private void Menu_Tool_AboutUs_Click(object sender, EventArgs e)
        {
            UI_AboutUs frmAboutus = new UI_AboutUs();
            frmAboutus.Show();
        }
        #endregion

        #endregion

        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            MessageBoxEx.AntiAlias = true;
            MessageBoxEx.EnableGlass = true;
            if (MessageBoxEx.Show(this,"确定要关闭吗?", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {                
                e.Cancel = true;
            }
            else
            {                
                if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData != null && CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
                {
                    MessageBoxEx.Show(this,"请先停止检定以后再关闭程序!!", "关闭", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    e.Cancel = true;
                }
                //退出前保存一次数据
                CLDC_DataCore.Const.GlobalUnit.g_CUS.Save();
            }
            if (e.Cancel)
            {
                raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.待机灯);
                //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetLightType(CLDC_Comm.Enum.Cus_LightType.黄灯);
            }
            else
            {
                raiseMenuevent(CLDC_Comm.Enum.Cus_MenuEventID.关灯);
                //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetLightType(CLDC_Comm.Enum.Cus_LightType.关灯);
            }
            base.OnClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (this.m_MainControl is Main)
            {
                ((Main)this.m_MainControl).SendMessage(m);
            }
            base.WndProc(ref m);
        }

        private void Menu_Help_HelpDoc_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            Assembly exe = typeof(UI_ClientFrame).Assembly;
            string helpFilePath = string.Format("{0}\\help.pdf", Path.GetDirectoryName(exe.Location));
            try
            {
                if (File.Exists(helpFilePath) == false)
                {
                    MessageBoxEx.Show(this,helpFilePath, "帮助文件不存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                System.Diagnostics.Process.Start(helpFilePath);
            }
            catch
            {
                MessageBox.Show(this,"打开帮助文件失败:\r\n\t 帮助文件不存在或是本机没有安装PDF阅读软件");
            }
        }

















    }
}

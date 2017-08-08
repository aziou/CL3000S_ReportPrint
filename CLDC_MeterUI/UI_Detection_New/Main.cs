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
    #region ����ί��
    /// <summary>
    /// ���������仯�¼�
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool EventFaParmChanged(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);



    /// <summary>
    /// �˵����ӻ��¼� (��������յ��¼��Ժ�������ֵ)
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    /// <returns>�����Ƿ�ɹ�</returns>
    public delegate bool EventOnTabChanged(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, ref int taiType, ref int taiId);

    /// <summary>
    /// �Ƿ�Ҫ���¼�
    /// </summary>
    /// <param name="taiType">̨������</param>
    /// <param name="taiId">̨��ID</param>
    /// <param name="bwh">��λ��</param>
    /// <param name="YaoJianYn">�Ƿ�Ҫ��</param>
    public delegate bool EventOnYaoJianChanged(int taiType, int taiId, int bwh, bool YaoJianYn);

    /// <summary>
    /// ��ֹ�춨
    /// </summary>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    public delegate bool EventOnCheckStop(int taiType, int taiId);

    /// <summary>
    /// ��ͣ�춨
    /// </summary>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    public delegate bool EventOnCheckPause(int taiType, int taiId);

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="IsTiaoBiao">true(����)��false(ֹͣ����)</param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    public delegate bool EventOnCheckAdjust(bool IsTiaoBiao, int taiType, int taiId);
    /// <summary>
    /// �����ʾ
    /// </summary>
    /// <param name="IsTip">true��ʾ��false����ʾ</param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool EventOnProgrammingTipAdjust(bool IsTip, int taiType, int taiId);
    /// <summary>
    /// ¼��������
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    public delegate bool Event_InputParam_OnOk(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);


    /// <summary>
    /// ��MIS��������Ϣ
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_DownMeterInfoFromMis(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// ��MIS�����ط���
    /// </summary>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_DownSchemeInfoFromMis(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);
    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_ReadPara(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup,int taiType,int taiId);
    /// <summary>
    /// �����������(�µ�UI��ʹ�ø��¼�)
    /// </summary>
    public delegate bool Event_LoadFA_OnOk(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="prjid"></param>
    /// <param name="taiId"></param>
    /// <param name="taiType"></param>
    /// <returns></returns>
    public delegate bool Event_OnChangePoint(int prjindex, int taiType, int taiId);

    /// <summary>
    /// �����춨
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnStepStart(int CheckId, int taiType, int taiId);

    /// <summary>
    /// �浵�¼�
    /// </summary>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnAuditingSave(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// ׼���浵�¼�
    /// </summary>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnAuditingSaveBefore(int taiType, int taiId);

    /// <summary>
    /// ¼������ֹ�����
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnInputNumberEnd(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId);

    /// <summary>
    /// ���±�
    /// </summary>
    /// <param name="meterGroup"></param>
    /// <param name="taiType"></param>
    /// <param name="taiId"></param>
    /// <returns></returns>
    public delegate bool Event_OnHangUpNewMeter(int taiType, int taiId);


    /// <summary>
    /// ������Ŀֵ�����仯�¼������¼����ڻ�����ϢUI�����޸���ײ�ͬ����
    /// </summary>
    /// <param name="PropertyName">��������</param>
    /// <param name="ChangeValue">�ı������ֵ</param>
    /// <param name="Bwh">�ı�ֵ�ı�λ�ţ���������б�λ���ı伴Ϊ999</param>
    /// <param name="taiType">̨������</param>
    /// <param name="taiId">̨����</param>
    /// <returns></returns>
    public delegate bool Event_DataInfoChanged(string PropertyName, object ChangeValue, int Bwh, int taiType, int taiId);
    /// <summary>
    /// �˵�����ί��
    /// </summary>
    /// <param name="EventID">ö����</param>
    public delegate void dlg_MenuClick(CLDC_Comm.Enum.Cus_MenuEventID EventID);
    #endregion

    public partial class Main : Office2007Form, CLDC_DataCore.Interfaces.IMeterInfoUpdateDownEnablecs
    {
        #region ����������¼���ö��
        private int TaiType;

        public int TaiId;

        /// <summary>
        /// �Ƿ�ˢ�·����б�
        /// </summary>
        public bool ReSetSchemeList = false;

        /// <summary>
        /// ���ܱ�����
        /// </summary>
        public CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup;

        /// <summary>
        /// ��¼��һ�ε�ToolBarItem��Textֵ��������ֹͣ�춨ʱˢ�����ݵĶ�λ
        /// </summary>
        private string LastToolBarItemText = string.Empty;

        #region ö�� ��Ŀ����
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private enum EnumCheckType
        {
            ¼����� = 1,
            �����춨 = 2,
            ��˴��� = 3
        }
        #endregion

        /// <summary>
        /// �����������޸��¼�
        /// </summary>
        public EventFaParmChanged FaParmChanged;

        /// <summary>
        /// �˵����ӻ��¼� (��������յ��¼��Ժ�������ֵ)
        /// </summary>
        public EventOnTabChanged OnTabChanged;

        /// <summary>
        /// ��ֹ�춨
        /// </summary>
        public EventOnCheckStop OnCheckStop;

        /// <summary>
        /// ��ͣ�춨
        /// </summary>
        public EventOnCheckPause OnCheckPause;

        /// <summary>
        /// �����춨
        /// </summary>
        public Event_OnStepStart Evt_OnStepStart;

        /// <summary>
        /// ����
        /// </summary>
        public EventOnCheckAdjust OnCheckAdjust;

        /// <summary>
        /// �����ʾ
        /// </summary>
        public EventOnProgrammingTipAdjust OnProgrammingTipAdjust;

        /// <summary>
        /// ����
        /// </summary>
        public Event_OnChangePoint Evt_OnChangePoint;

        /// <summary>
        /// �浵�¼�
        /// </summary>
        public Event_OnAuditingSave Evt_OnAuditingSave;

        /// <summary>
        /// ׼���浵�¼�
        /// </summary>
        public Event_OnAuditingSaveBefore Evt_OnAuditingSaveBefore;

        /// <summary>
        /// ¼������ֹ�����
        /// </summary>
        public Event_OnInputNumberEnd Evt_OnInputNumberEnd;

        /// <summary>
        /// �����Ƿ�춨��־�¼�
        /// </summary>
        public EventOnYaoJianChanged Evt_OnYaoJianChanged;

        /// <summary>
        /// ���±�
        /// </summary>
        public Event_OnHangUpNewMeter Evt_OnHangUpNewMeter;

        /// <summary>
        /// ¼��������
        /// </summary>
        public Event_InputParam_OnOk Evt_InputParam_OnOk;

        /// <summary>
        /// ��ȡ����
        /// </summary>
        public Event_ReadPara Evt_ReadPara;

        /// <summary>
        /// ��MIS�����ص����Ϣ
        /// </summary>
        public Event_DownMeterInfoFromMis Evt_DownMeterInfoFromMis;

        /// <summary>
        /// ��MIS�����ط���
        /// </summary>
        public Event_DownSchemeInfoFromMis Evt_DwonSchemeInfoFromMis;

        /// <summary>
        /// �����������
        /// </summary>
        public Event_LoadFA_OnOk Evt_LoadFA_OnOk;

        /// <summary>
        /// ������仯�¼�
        /// </summary>
        public Event_DataInfoChanged Evt_DataInfoChanged;
        /// <summary>
        /// �˵��¼�
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
        /// ����״̬����Ϣ
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
                        ((CheckBase)this.CurrentUIControl).SetCheckMessage(msg);//������Ϣ��Ϣ
                    }
                }
                ShowStepUserMsg(msg);
                ShowStepInputParaMsg(msg);
            }
        }

        

        
        #region ��ʼ����Ŀ�ӿؼ� Fun_SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        /// <summary>
        /// ��ʼ����Ŀ�ӿؼ�
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        /// <param name="CheckType"></param>
        private void Fun_SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("ϵͳ���ڼ���...");
            try
            {
                lock (CurrentUIControl_Lock)
                {
                    this.StatusMain_Proc.Visible = true;//������
                    bool IsNew = false;

                    switch (CheckType)
                    {
                        #region
                        case EnumCheckType.¼�����:
                            {
                                this.StatusMain_Proc.Visible = false;//����������
                                if (Plan_ChildContainer.Controls.Count == 1 && Plan_ChildContainer.Controls[0] is InputPara_V80Style)
                                {
                                    
                                }
                                else
                                {
                                    CurrentUIControl = new InputPara_V80Style(this, meterGroup, taiType, taiId);
                                    this.stepUserControl1.Visible = false;//�����ܽ��ȿ�
                                    this.tableLayoutPanel1.RowStyles[1].Height = 2;
                                    IsNew = true;
                                }
                                break;
                            }
                        case EnumCheckType.�����춨:
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
                        case EnumCheckType.��˴���:
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
                    if (IsNew)              //������±�������Ҫ���Ƴ����б�����׷��
                    {
                        if (CurrentUIControl != null)
                        {
                            #region
                            Init_Stutas();//����״̬��
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,408]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Const.GlobalUnit.Logger.Fatal("��ʼ����:" + typeof(Main).FullName + "ʧ��:", ex);
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }
        #endregion

        #region ˢ������Ŀ���� Fun_RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId, EnumCheckType CheckType)
        /// <summary>
        /// ˢ������Ŀ���� 
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
                //�ػ������ʾ��д����־
                try
                {
                    switch (CheckType)
                    {
                        case EnumCheckType.¼�����:
                            ((InputPara_V80Style)CurrentUIControl).RefreshData(meterGroup, taiType, taiId);
                            break;
                        case EnumCheckType.�����춨:
                            ((CheckBase)CurrentUIControl).RefreshData(meterGroup, taiType, taiId);
                            if (ReSetSchemeList)
                            {
                                ((CheckBase)CurrentUIControl).CreateNewRowCell(meterGroup.CheckPlan);
                                ReSetSchemeList = false;
                            }
                            break;
                        case EnumCheckType.��˴���:
                            ((AuditingSave)CurrentUIControl).RefreshData(meterGroup, taiType, taiId);
                            CLDC_DataCore.Const.GlobalUnit.g_RealTimeDataControl.OutUpdateRealTimeData("", CLDC_Comm.Enum.Cus_MeterDataType.װ��״̬��Ϣ����, false);
                            break;
                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG          
                    MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,457]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }
        #endregion

        #region  Ҫ��¼�����롢ֹ��
        /// <summary>
        /// Ҫ��¼�����롢ֹ��
        /// </summary>
        /// <param name="IsStartNumber">�Ƿ�¼�����룿����¼��ֹ��</param>
        /// <returns>�����Ƿ�ɹ�</returns>
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,488]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="IsAdjust">true:��ʼ����false:ֹͣ����</param>
        public bool CheckAdjust(bool IsAdjust)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            try
            {
                bool bResult = false;
                if (IsAdjust)
                {
                    //Comm.Function.TopWaiting.ShowWaiting("���ڿ�ʼ����...");

                    if (OnCheckAdjust != null)
                    {
                        bResult = OnCheckAdjust(true, TaiType, TaiId);
                    }
                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    //Comm.Function.TopWaiting.ShowWaiting("���ڽ�������...");
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,531]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }
        #endregion

        #region �����ʾ
        /// <summary>
        /// �����ʾ
        /// </summary>
        /// <param name="IsAdjust">true:��ʾ��false:����ʾ</param>
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
                    //Comm.Function.TopWaiting.ShowWaiting("����...");
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,574]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                return false;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¼��ǰ����UI
        /// </summary>
        private Base CurrentUIControl = null;
        /// <summary>
        /// �߳���
        /// </summary>
        private object CurrentUIControl_Lock = new object();

        private object objSetDataLock = new object();

        /// <summary>
        /// ��ǰ�춨��Ŀ�±�
        /// </summary>
        public int ActiveIdByClick = -1;

        /// <summary>
        /// ����ָʾ��
        /// </summary>
        private Image[] ImgNetState = new Image[2];

        /// <summary>
        /// ������
        /// </summary>
        public CLDC_MeterUI.Monitor UIMonitor;

        /// <summary>
        /// �Ƿ����������
        /// </summary>
        public readonly bool PopByServer;
        /// <summary>
        /// ��¼ʱ��/��Ա
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
                base.Text = string.Format("���ض� - {0}�� ��{1}��", TaiId, (TaiType == 0 ? "����̨" : "����̨"));
            }
        }

        /// <summary>
        /// ���� ��������״̬
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
        /// Ĭ�ϵĹ��캯��
        /// </summary>
        public Main()
        {
            InitializeComponent();

            StatusMain_Text.Text = "";

            this.Resize += new EventHandler(Main_Resize);

            this.Load += new EventHandler(Main_Load);

            StatusMain_Mode.Text = "[" + CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("ISDEMO") + "]";

            LoginerAndTime = "   �춨Ա��" + CLDC_DataCore.Const.GlobalUnit.User_Jyy.UserName + "   ����Ա��" + CLDC_DataCore.Const.GlobalUnit.User_Hyy.UserName + "   ��¼ʱ�䣺" + DateTime.Now.ToString("yyyy��MM��dd�� HHʱmm��ss��");

            
            
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
        /// ��д�Ĺ��캯��
        /// </summary>
        /// <param name="byserver">�Ƿ����Է�������</param>
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

        #region �������
        /// <summary>
        /// �������
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
                //�ͻ�������ʱ
                if (tmpParent is CLDC_MeterUI.UI_ClientFrame)
                {
                    CLDC_MeterUI.UI_ClientFrame ClientFrameHandle = (CLDC_MeterUI.UI_ClientFrame)tmpParent;
                    this.UIMonitor = ClientFrameHandle.UIMonitor;

                    UIMonitor = new Monitor();
                    UIMonitor.DanXiangTai = (CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("DESKTYPE") == "����̨");
                    ClientFrameHandle.Controls.Add(UIMonitor);
                    ClientFrameHandle.Controls.SetChildIndex(UIMonitor, 0);
                    UIMonitor.Visible = true;
                }
                else //�������˵���ʱ
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,743]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }

            //ʹ��ǰ��������ǰ��
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

        #region  ���衢ˢ������
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
        /// ========== ���衢ˢ������  ==========
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
                strBasicInfo = "���ܱ�" + CLDC_DataCore.Const.GlobalUnit.Clfs.ToString() + "��" + CLDC_DataCore.Const.GlobalUnit.U + "V��" + CLDC_DataCore.Const.GlobalUnit.Ib + "(" + CLDC_DataCore.Const.GlobalUnit.Imax + ")A��" + meterGroup.MinConst[0] + "imp/Kwh����ǰ������" + meterGroup.FaName;
                this.stepUserControl1.strBisicInfo = strBasicInfo;
                int totalTime = meterGroup.CheckPlan.Count * 115;//miao
                if (totalTime == 0)
                {
                    totalTime = 120;
                }
                if (CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum.Year == 1)
                    CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum = DateTime.Now;
                CLDC_DataCore.Const.GlobalUnit.g_CheckTimeEndSum = CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum.AddSeconds(totalTime);
                string strTotalTime = string.Format("{0}Сʱ{1}��{2}��", totalTime / 3600, (totalTime % 3600) / 60, totalTime % 60);
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
                //�ػ������ʾ����д����־
#if DEBUG
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,840]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }

        public void SetDataInvoke(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            lock (objSetDataLock)
            {
                this.Text = ""; //ˢ�±��� ���������Զ�����
                 StatusMain_TxtStatus2.Text = meterGroup.CheckState.ToString();

                int ActiveId = meterGroup.ActiveItemID;

                SetWindowText(taiId, taiType, meterGroup._Bws, meterGroup.CheckState, ActiveId);
                if (!hasSetTtype && UIMonitor != null)
                {
                    UIMonitor.DanXiangTai = taiType == 1 ? true : false;
                    hasSetTtype = true;
                } //ֹͣ�춨״̬�£���ˢ�����ݲ��л����߲˵�
                //if (meterGroup.CheckState == Comm.Enum.Cus_CheckStaute.ֹͣ�춨
                //    && LastToolBarItemText != string.Empty && meterGroup.ActiveItemID >= 0
                //    && LastToolBarItemText != "¼�����")
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
        /// ========== ���衢ˢ������  ==========
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

                #region ¼�����|��˴���
                if (ActiveId <= 0)           //��ǰIDС��0�������ʾ�ǲ��ڼ춨�����У����Ҽ춨�����Ǵ������ġ�
                {
                    //ToolBtn_StepStart.Enabled = false;
                    //ToolBtn_Start.Enabled = false;
                    //ToolBtn_Stop.Enabled = false;

                    //ֹͣ�춨״̬�¿����л��� [¼�����]
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)
                    {
                        ToolBtn_InputParam.Enabled = true;
                    }
                    else
                    {
                        ToolBtn_InputParam.Enabled = false;
                    }

                    //����ǰ��4�������������ֹͣ��¼�����,0,1,2,3
                    //for (int i = 4; i < ToolStrip_Main.Items.Count; i++)
                    //{
                    //    ToolStrip_Main.Items.RemoveAt(i--);
                    //}

                    #region �������м춨��Ŀ����������Ҫ�춨����Ŀ������
                    //if (meterGroup.ActiveItemID >= 0 || meterGroup.ActiveItemID == -3)            //��ǰ�����ID����0����ʾ���ڼ춨�����У�������������˴��̵�״̬

                    AddToolStripItem("�����춨", Application.StartupPath + @"\Pic\Detection\V90Style\wcjd.png");

                    AddToolStripItem("��˴���", Application.StartupPath + @"\Pic\Detection\V90Style\shcp.png");

                    SetToolBtnEnableByText("�����춨", false);
                    SetToolBtnEnableByText("��˴���", false);

                    if (meterGroup.CheckPlan.Count > 0)
                    {

                        SetToolBtnEnableByText("�����춨", true);
                        SetToolBtnEnableByText("��˴���", true);
                    }
                    #endregion

                }

                #region ¼�����
                if (ActiveId == -1)
                {
                    if (CurrentUIControl is InputPara_V80Style)
                    {
                        Fun_RefreshData(meterGroup, taiType, taiId, EnumCheckType.¼�����);
                    }
                    else
                    {
                        Fun_SetData(meterGroup, taiType, taiId, EnumCheckType.¼�����);
                    }
                    SetCurrentToolBtnStyle("����¼��");
                    //SetToolBtnEnableByText("�����춨", false);
                    //SetToolBtnEnableByText("��˴���", false);
                }
                #endregion

                #region ��˴���
                else if (ActiveId == -3)
                {
                    if (CurrentUIControl is AuditingSave)
                    {
                        Fun_RefreshData(meterGroup, taiType, taiId, EnumCheckType.��˴���);
                    }
                    else
                    {
                        Fun_SetData(meterGroup, taiType, taiId, EnumCheckType.��˴���);

                    }
                    //SetCurrentToolBtnStyle("��˴���");

                }
                #endregion

                #endregion

                #region ���ݼ춨״̬���á������춨��ֹͣ�춨������ť����״̬
                if (ActiveId >= 0)
                {
                    
                    //����ǰ��4�������������ֹͣ��¼�����,0,1,2,3
                    //if (ToolStrip_Main.Items.Count == 4)
                    //{
                    //    AddToolStripItem("�����춨", Application.StartupPath + @"\Pic\Detection\V90Style\wcjd.png");

                    //    AddToolStripItem("��˴���", Application.StartupPath + @"\Pic\Detection\V90Style\shcp.png");
                    //}

                    if (meterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)
                    {
                        SetToolBtnEnableByText("����¼��", true);
                        SetToolBtnEnableByText("Ԥ�ȵ���", true);
                        SetToolBtnEnableByText("��������", true);
                        SetToolBtnEnableByText("��Դ���", true);
                        SetToolBtnEnableByText("��Դֹͣ", true);
                        SetToolBtnEnableByText("��˴���", true);
                    }
                    else
                    {
                        SetToolBtnEnableByText("����¼��", false);
                        SetToolBtnEnableByText("Ԥ�ȵ���", false);
                        SetToolBtnEnableByText("��������", false);
                        SetToolBtnEnableByText("��Դ���", false);
                        SetToolBtnEnableByText("��Դֹͣ", false);
                        SetToolBtnEnableByText("��˴���", false);
                    }


                    switch (meterGroup.CheckState)
                    {
                        case CLDC_Comm.Enum.Cus_CheckStaute.�����춨:
                            this.stepUserControl1.isStepEnabled = false;
                            this.stepUserControl1.isStartEnabled = false;
                            this.stepUserControl1.isStopEnabled = true;
                            break;

                        case CLDC_Comm.Enum.Cus_CheckStaute.����:
                            this.stepUserControl1.isStepEnabled = false;
                            this.stepUserControl1.isStartEnabled = false;
                            this.stepUserControl1.isStopEnabled = true;
                            break;

                        case CLDC_Comm.Enum.Cus_CheckStaute.�춨:
                            this.stepUserControl1.isStepEnabled = false;
                            this.stepUserControl1.isStartEnabled = false;
                            this.stepUserControl1.isStopEnabled = true;
                            break;

                        case CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨:
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

                #region ����״̬Ϊ�����춨�Ժ�

                if (ActiveId >= 0)
                {
                    if (CurrentUIControl is CheckBase)
                    {
                        Fun_RefreshData(meterGroup, taiType, taiId, EnumCheckType.�����춨);
                    }
                    else
                    {
                        Fun_SetData(meterGroup, taiType, taiId, EnumCheckType.�����춨);
                        //SetCurrentToolBtnStyle("�����춨");
                    }
                    //��.Tag���浱ǰ�춨��Ŀ
                    if (CurrentUIControl != null)
                        CurrentUIControl.Tag = objActivePlan;

                }
                #endregion

                #region//ֹͣ�춨״̬�¿����л��� [¼�����]
                if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨/* && ActiveId >= 0*/)
                {
                    ToolBtn_InputParam.Enabled = true;
                    //if (ActiveId == -1)
                    {
                        //if (this.ToolStrip_Main.Items.Count > 4)
                        //{
                        if (meterGroup.CheckPlan.Count > 0 && meterGroup.ActiveItemID >= 0)
                        {
                            SetToolBtnEnableByText("Ԥ�ȵ���", true);
                            SetToolBtnEnableByText("��������", true);
                            SetToolBtnEnableByText("��Դ���", true);
                            SetToolBtnEnableByText("��Դֹͣ", true);
                            SetToolBtnEnableByText("�����춨", true);
                            SetToolBtnEnableByText("��˴���", true);
                        }
                        else
                        {
                            SetToolBtnEnableByText("Ԥ�ȵ���", false);
                            SetToolBtnEnableByText("��������", false);
                            SetToolBtnEnableByText("��Դ���", false);
                            SetToolBtnEnableByText("��Դֹͣ", false);
                            SetToolBtnEnableByText("�����춨", false);
                            SetToolBtnEnableByText("��˴���", false);
                        }
                        //}
                    }
                }
                else
                {
                    //ToolBtn_InputParam.Enabled = false;
                    SetToolBtnEnableByText("Ԥ�ȵ���", false);
                    SetToolBtnEnableByText("��������", false);
                    SetToolBtnEnableByText("��Դ���", false);
                    SetToolBtnEnableByText("��Դֹͣ", false);
                    SetToolBtnEnableByText("����¼��", false);
                    SetToolBtnEnableByText("Ԥ�ȵ���", false);
                    SetToolBtnEnableByText("��˴���", false);
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

        #region ����״̬�������Ⱥ���ʾ���֣���Ϣ
        /// <summary>
        /// ����״̬�������Ⱥ���ʾ���֣���Ϣ
        /// </summary>
        /// <param name="NoticeText"></param>
        public void SetStatus(string NoticeText)
        {
            if (this.CurrentUIControl != null)
            {
                if (this.CurrentUIControl is CLDC_MeterUI.UI_Detection_New.CheckBase)
                {
                    ((CheckBase)this.CurrentUIControl).SetCheckMessage(NoticeText);//������Ϣ��Ϣ
                }
            }
            
            //���ݵ�ǰ�춨��Ŀ�ͽ������ý�����
            try
            {
                float maxProcess = 0;
                int curPorcess = 0;
                if (this.MeterGroup != null)
                {
                    curPorcess = (int)(MeterGroup.NowMinute * 100);
                    if (MeterGroup.ActiveItemID >= 0)
                    {
                        if (/*this.MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨 ||*/
                            this.MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.δ��ֵ��
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
                                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.������).ToString(), ((int)((StPlan_QiDong)objActivePlan).PowerFangXiang).ToString());
                                    if (_firstMeter.MeterResults.ContainsKey(_Key))
                                    {
                                        //TODO:����ʵ����Ŀ�Ľ�����ʾ����
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
                                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.Ǳ������).ToString(), ((int)((StPlan_QianDong)objActivePlan).PowerFangXiang).ToString());
                                    if (_firstMeter.MeterResults.ContainsKey(_Key))
                                    {
                                        //TODO:Ǳ�����������������
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
                                #region----------������������춨����----------
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
                //MessageBoxEx.Show(ex.Message, "[����ֻ��DEBUGʱ����]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// ����״̬�������Ⱥ���ʾ���֣���Ϣ
        /// </summary>
        /// <param name="NoticeText">״̬����ʾ����</param>
        /// <param name="IncProgressValue">����ֵ�Զ��ݼ�1</param>
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
        /// ����״̬�������Ⱥ���ʾ���֣���Ϣ
        /// </summary>
        /// <param name="NoticeText">״̬����ʾ����</param>
        /// <param name="ProgressValue">״̬�����ȣ�Ĭ�� 0-100</param>
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
        /// ����״̬�������Ⱥ���ʾ���֣���Ϣ
        /// </summary>
        /// <param name="NoticeText">״̬����ʾ����</param>
        /// <param name="ProgressValue">״̬�����ȣ�Ĭ�� 0-100</param>
        /// <param name="ProgressMaxValue">���������ֵ</param>
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,1340]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }
        #endregion

        #region �����������¼�

        /// <summary>
        /// ����ָ���Ĳ˵�ѡ��
        /// 1 ֹͣ
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
        /// �����������¼�
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
                //����ײ���������ڹ涨ʱ����û�з��ء����Զ�����ť����Ϊ���á���ȥ����ʾ�ı�

                CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(thResetButton), 30000);
                this.stepUserControl1.isEnabled = false;
                ButtonItem btie = sender as ButtonItem;
                switch (btie.Text.Trim())
                {

                    case "�����춨":
                        {
                            if (CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.IsTipAuxiliaryPress == true)
                            {
                                if(DialogResult.Cancel ==MessageBoxEx.Show(this, "���Ƚ���������ѹ��ѹ�á����ѹ����㡰ȷ����������㡰ȡ����ȡ��������", "��ʾ",MessageBoxButtons.OKCancel))
                                {
                                    break;
                                }
                            }
                            #region
                            CLDC_DataCore.Function.TopWaiting.ShowWaiting("���ڿ�ʼ�춨...");
                            this.stepUserControl1.isEnabled = true;
                            if (Evt_OnChangePoint != null)
                            {
                                if (MeterGroup.ActiveItemID >= 0)
                                {
                                    if (ActiveIdByClick != -1)
                                    {
                                        //���û�ѡ������Ŀ�����춨
                                        Evt_OnChangePoint(ActiveIdByClick, TaiType, TaiId);
                                    }
                                    else
                                    {
                                        //�����ϴ�ֹͣʱ�Ľ��������춨
                                        Evt_OnChangePoint(MeterGroup.ActiveItemID, TaiType, TaiId);
                                    }
                                }
                                else
                                {
                                    //�ӵ�һ����Ŀ�����춨
                                    Evt_OnChangePoint(GetFirstCheckPrjIndexFromGroupInfo(MeterGroup), TaiType, TaiId);
                                }
                            }
                            else
                            {
                                MessageBoxEx.Show(this,"û�д���Evt_OnChangePoint�¼�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            CLDC_DataCore.Function.TopWaiting.HideWaiting();
                            CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning = true;
                            ActiveIdByClick = -1;
                            #endregion
                            break;
                        }

                    case "ֹͣ�춨":
                        {
                            #region
                            CLDC_DataCore.Function.TopWaiting.ShowWaiting("����ֹͣ�춨...");
                            
                            if (OnCheckStop != null)
                            {
                                OnCheckStop(TaiType, TaiId);
                            };
                            
                            ActiveIdByClick = -1;

                            CLDC_DataCore.Function.TopWaiting.HideWaiting();
                            #endregion
                            break;
                        }

                    case "�����춨":
                        {
                            if (CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.IsTipAuxiliaryPress == true)
                            {
                                if (DialogResult.Cancel == MessageBoxEx.Show(this, "���Ƚ���������ѹ��ѹ�á����ѹ����㡰ȷ����������㡰ȡ����ȡ��������", "��ʾ", MessageBoxButtons.OKCancel))
                                {
                                    break;
                                }
                            }
                            #region
                            CLDC_DataCore.Function.TopWaiting.ShowWaiting("���ڿ�ʼ�����춨...");
                            this.stepUserControl1.isEnabled = true;
                            if (Evt_OnStepStart != null)
                            {
                                //˵�����ϴ�[ֹͣ�춨]�Ժ��û�û��ͨ��������л��춨��
                                if (ActiveIdByClick == -1)
                                {
                                    ActiveIdByClick = MeterGroup.ActiveItemID;
                                }

                                if (ActiveIdByClick >= 0)
                                {
                                    Evt_OnStepStart(ActiveIdByClick, TaiType, TaiId);
                                    //���û�ѡ������Ŀ�����춨
                                }
                                else
                                {
                                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                                    MessageBoxEx.Show(this,"��ѡѡһ����Ҫ�����춨����Ŀ!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    ToolStrip_Main.Enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                                MessageBoxEx.Show(this,"û�д����¼�Evt_OnStepStart", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ToolStrip_Main.Enabled = true;
                                return;
                            }
                            CLDC_DataCore.Const.GlobalUnit.TestThreadIsRunning = true;
                            CLDC_DataCore.Function.TopWaiting.HideWaiting();
                            #endregion
                            break;
                        }
                    case "¼�����":
                    case "����¼��":
                        {
                            SetData2(MeterGroup, -1, TaiType, TaiId);
                            break;
                        }
                    //case "Ԥ�ȵ���":
                    //    {
                    //        break;
                    //    }
                    case "��˴���":
                        {
                            #region �������ϸ����ʱ������˴���ǰ����ͻ�������һ��������ģ��
                            //if (this.Evt_OnAuditingSaveBefore != null)
                            //{
                            //    if (Evt_OnAuditingSaveBefore(TaiType, TaiId))
                            //    {
                            //        Thread.Sleep(1000);
                            //    }
                            //}
                            #endregion
                            SetData2(MeterGroup, -3, TaiType, TaiId);
                            //SetCurrentToolBtnStyle("��˴���");
                            break;
                        }
                    case "��������":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.����);
                            break;
                        }
                    case "�����ѹ":
                    case "ֻ����ѹ":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.��Դ);
                            break;
                        }
                    case "�������":
                    case "��ѹ����":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.���궨��ѹ����);
                            break;
                        }
                    case "ֹͣ���":
                    case "��Դֹͣ":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.��Դ);
                            break;
                        }
                    case "���ɿ�Դ":
                    case "�������":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.���ɿ�Դ);
                            break;
                        }
                    case "�߼�����"://��ťĬ�Ͻ���ϵͳ����
                    case "ϵͳ����":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.ϵͳ����);
                            break;
                        }
                    case "��������":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.��������);
                            break;
                        }
                    case "�豸ͨ��":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.�����豸�˿�);
                            break;
                        }
                    case "485ͨ��":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.RS485ͨ������);
                            break;
                        }
                    case "��ʾ����":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.��ʾ����);
                            break;
                        }
                    case "Э������":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.Э������);
                            break;
                        }
                    case "������"://��ťĬ�Ͻ���һ��
                    case "�������":
                    case "�߼�����":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.�������);
                            break;
                        }
                    case "���Ĺ���":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.���Ĺ���);
                        }
                        break;
                    case "Э���ֵ�":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.Э���ֵ�);
                        }
                        break;
                    case "ģ�����":
                        {
                            OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.ģ�����);
                        }
                        break;
                    case "��ʱ����":
                            {
                                OnMenuClick(CLDC_Comm.Enum.Cus_MenuEventID.��ʱ����);
                            }
                        break;
                    case "���ݹ���":
                        CLDC_DataCore.Function.File.RunOtherExe(CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEEXEPATH, "/CLDC_DataManager.exe"), CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEPROCESSNAME, "ClDataManager"));
                        break;
                    //����
                    default:
                        {//fjk 
                            SetStatus(string.Format("{0},{1},{2}", btie.Text, MeterGroup.CheckState, DateTime.Now.ToString()));
                            if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)
                            {
                                //ֱ����ת
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
                MessageBoxEx.Show(this,ex.Message, "[����ֻ��DEBUGʱ����,1614]���ִ�����鿴��־", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
        }
        #endregion

        #region �ָ�������״̬Ϊ����--���ڲ�����ʱ��û�з���ʱ
        private void thResetButton()
        {
            if (ToolStrip_Main.Enabled == false)
            {
                ToolStrip_Main.Enabled = true;
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                SetStatus("������ʱ��û�з��أ������²���");
            }
        }

        #endregion

        #region ���ݱ������ֻ�ȡ��������
        /// <summary>
        /// ��ȡ��������
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

        #region ���ù�������Ŀ���״̬
        /// <summary>
        /// ���ù�������Ŀ���״̬
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

        #region ���ù�������Ϊ��ǰ���ʽ��
        /// <summary>
        /// ���ù�������Ϊ��ǰ���ʽ��
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
                        if (bti.Text == "���ݹ���" || bti.Text == "��������" || bti.Text == "�߼�����" || bti.Text == "������")
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

        #region �� CLDC_DataCore.Model.DnbModel.DnbGroupInfo���ҵ���һ���춨��Ŀ�±�
        /// <summary>
        ///  �� CLDC_DataCore.Model.DnbModel.DnbGroupInfo���ҵ���һ���춨��Ŀ�±�
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

        #region MeterGroup��[ָ����Ŀ]�ĵ�һ���춨��Ŀ�±�(��̬)
        /// <summary>
        /// MeterGroup��[ָ����Ŀ]�ĵ�һ���춨��Ŀ�±�(��̬)
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
                if (objItem is StPlan_PrePareTest && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���)
                {
                    break;
                }
                if (objItem is StPlan_YuRe && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.Ԥ������)
                {
                    break;
                }
                else if (objItem is StPlan_WGJC && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.��ۼ������)
                {
                    break;
                }
                else if (objItem is StPlan_QiDong && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.������)
                {
                    break;
                }
                else if (objItem is StPlan_QianDong && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.Ǳ������)
                {
                    break;
                }
                else if (objItem is StPlan_WcPoint && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.�����������)
                {
                    break;
                }
                else if (objItem is StPlan_SpecalCheck && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������)
                {
                    break;
                }

                else if (objItem is CLDC_DataCore.Struct.StPlan_Dgn && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.�๦������)
                {
                    break;
                }
                else if (objItem is StPlan_Carrier && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.�ز�����)
                {
                    break;
                }
                else if (objItem is StPlan_ZouZi && CheckTypeName == CLDC_Comm.Enum.Cus_FAGroup.��������)
                {
                    break;
                }
            }
            return index;
        }
        #endregion

        #region ��ȡ��һֻҪ�����±ꡢû�з���-1
        /// <summary>
        /// ��ȡ��һֻҪ�����±ꡢû�з���-1
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

        #region ���ô��ڱ�������
        private void SetWindowText(int taiId, int TaiType, int BW, CLDC_Comm.Enum.Cus_CheckStaute State, int ActiveId)
        {
            //string _TaiType = TaiType == 0 ? "����" : "����";
            //string _WindowText = String.Format("{0}��̨,{1}{2}��λ,״̬:", taiId, _TaiType, BW);
            //if (State == Comm.Enum.Cus_CheckStaute.�춨)
            //    _WindowText += String.Format("���ڼ춨��{0}��", ActiveId);
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
        /// ���ͽػ��ϵͳ��Ϣ����ҪΪ�˴�������С�������
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

        #region IMeterInfoUpdateDownEnablecs ��Ա

        public event CLDC_DataCore.Interfaces.GetMeterInfo OnGetMeterInfo;
        /// <summary>
        /// ���ⴥ��ͨ��MIS�ӿڻ�ȡ���ܱ������Ϣ�¼�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo GetMeterInfo(string key)
        {
            if (OnGetMeterInfo == null) return null;
            return OnGetMeterInfo(key);
        }
        #endregion

        #region �����豸����״̬
        //�����첽�̵߳�����������״̬
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
        //�����豸��PC������״̬
        private void SetConnectStatus(bool connectStatus)
        {
            if (connectStatus)
            {
                buttonItemConnectStatus.Image = Image.FromFile(CLDC_DataCore.Function.File.GetPhyPath(@"\Pic\UI\��������.png"));
                buttonItemConnectStatus.Tooltip = "��������";
            }
            else
            {
                buttonItemConnectStatus.Image = Image.FromFile(CLDC_DataCore.Function.File.GetPhyPath(@"\Pic\UI\���ӶϿ�.png"));
                buttonItemConnectStatus.Tooltip = "����ʧ��";
            }
        }
        #endregion �����豸����״̬

        private void buttonItem4_Click(object sender, EventArgs e)
        {

        }


    }
}
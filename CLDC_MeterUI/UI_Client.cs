/*
 * FileName UI_Client.cs
 * Description:�ͻ��˽���ģ��
 * ����ͻ��ˣ�����ģʽ���µ�����¼�롢��ʾ
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
    /// �ͻ��˱���״̬����
    /// </summary>
    public partial class UI_Client : Office2007Form, CLDC_DataCore.Interfaces.IMeterInfoUpdateDownEnablecs
    {
        #region----------�ӿ��¼�����----------
        //������Ϣ����
        public event OnEventSendMessage SendMessage;
        //�춨����
        //public event ClInterface.UI.OnEventDoAction DoAction;
        #endregion

        #region----------�ڲ�ί��----------
        private delegate void OnEventShowLabText(object obj, Label ShowLab, string Message);
        /// <summary>
        /// ¼������¼�
        /// </summary>
        /// <param name="State">��ǰ�춨״̬</param>
        public delegate void OnEventInputOver(CLDC_Comm.Enum.Cus_stVerifyStep State);
        public event OnEventInputOver OnInputOver;
        #endregion

        #region ----------��������----------

        /// <summary>
        /// ���ؼ�
        /// </summary>
        private PublicControls.UI_ClientTable ClientTable = null;

        //״̬ά��
        private CLDC_Comm.Enum.Cus_stVerifyStep m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.����¼��;

        //������
        private bool m_LoadOver = false;
        /// <summary>
        /// ��������״̬ͼ��
        /// </summary>
        private Image[] ImgNetState = new Image[2];

        public bool LoadOver
        {
            get { return m_LoadOver; }
        }

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        public CLDC_MeterUI.Monitor UIMonitor = new Monitor();

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

                Pic_NetState.Image = ImgNetState[(int)value];
            }
        }

        public UI_Client()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.Load += new EventHandler(UI_Client_Load);
#if DEBUG
            //Comm.DesignTools.��ɫ����.Bind(this);
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
            //ȡ��λ
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

            //���ü�����
            this.Controls.Add(UIMonitor);
            this.Controls.SetChildIndex(UIMonitor, 0);
            UIMonitor.Visible = true;

            showSchemeInfo();

            //����״̬
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Option == CLDC_CTNProtocol.EnumOption.BZ������)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SetOption(CLDC_CTNProtocol.EnumOption.CZ׼��ɨ����);
            }

            //��ʽģʽ | ��ʾģʽ
            Lab_Mode.Text = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue("ISDEMO");

            //ʹ���㴰�����
            SetParentMaximized();
            CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(SetParentMaximized), 200);
        }

        //���ر�
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
                ClientTable.RowMeterNum = RowNum;            //ÿ��6ֻ��
                ClientTable.RowHeight = 25;
                ClientTable.TextCellFont = new Font("����", 10, FontStyle.Bold);
                ClientTable.RefreshGrid();
                //ClientTable.ReadOnly = true;
                this.GroupBox_Container.Controls.Add(ClientTable);
                ClientTable.Dock = DockStyle.Fill;
                ClientTable.Margin = new System.Windows.Forms.Padding(10);
                ClientTable.TxtInputOver += new CLDC_MeterUI.PublicControls.UI_ClientTable.Event_TxtInputOver(ClientTable_TxtInputOver);
                ClientTable.CheckOver += new CLDC_MeterUI.PublicControls.UI_ClientTable.Event_CheckOver(ClientTable_CheckOver);

            }

            //��ʼ�������ؼ�
            {
                //��ť�¼�
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
        /// �������������¼�
        /// </summary>
        /// <param name="Bwh">��λ��(1-BW)</param>
        /// <param name="Value">����ֵ</param>
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
            //��������ݲ���¼��״̬�����ж�
            switch (m_VerifyStep)
            {
                case CLDC_Comm.Enum.Cus_stVerifyStep.����¼��:
                    {
                        //����������Ƿ��ظ�
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo tmpMeter = null;
                        for (int i = 0; i < BwCount; i++)
                        {
                            if (i == Bwh - 1) continue;
                            tmpMeter = CLDC_DataCore.Const.GlobalUnit.Meter(i);
                            if (tmpMeter != null && Value == tmpMeter.Mb_ChrTxm)
                            {
                                strMessage = string.Format("������[{0}]�Ѿ���{1}��λ����!", Value, curMeter.ToString());
                                //CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
                                MessageBoxEx.Show(this,strMessage, "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                        //�����ʶ��������λ
                        strMessage = Value.Substring(Value.Length - 4, 4);
                       // CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
                        /*���������ʼ*/
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = null;
                        if (OnGetMeterInfo != null)
                            MeterInfo = OnGetMeterInfo(Value);
                        if (MeterInfo != null)
                        {
                            MeterInfo.SetBno(Bwh);
                            CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1] = MeterInfo;
                            /*���͵�������*/
                            CLDC_DataCore.Command.Model.SendMeterData_Ask cmdOneMeter = new CLDC_DataCore.Command.Model.SendMeterData_Ask();
                            //cmdOneMeter.MeterData = MeterInfo;
                            CLDC_CTNProtocol.CTNPCommand cmdResponse;
                            sendMessage(cmdOneMeter, out cmdResponse);
                            if (cmdResponse == null)
                            {
                                MessageBoxEx.Show(this,"�ϴ����ݵ��������ĳ���");
                                return false;
                            }
                        }
                        else
                        {
                            curMeter.Mb_ChrTxm = Value;
                            //�ϱ�������
                            CLDC_DataCore.Command.Txm.InputTxm_Update_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputTxm_Update_Ask();
                            CLDC_CTNProtocol.CTNPCommand cmdAnswer;
                            cmdAsk.Txm = Value;
                            cmdAsk.Bwh = Bwh - 1;

                            sendMessage(cmdAsk, out cmdAnswer);
                            if (cmdAnswer == null)
                            {
                                MessageBoxEx.Show(this,"����ʧ�ܣ�������ɨ��", "����ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClientTable.SelectedBwh = Bwh;
                            }
                        }
                        GlobalUnit.g_MsgControl.OutMessage(string.Format("�ڱ�{0}λɨ�����", Bwh), true);
                        break;
                    }
                case CLDC_Comm.Enum.Cus_stVerifyStep.��������¼����:
                    {
                        if (!curMeter.MeterZZErrors.ContainsKey(ItemKey))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("û�з������ݽڵ�", false);
                            return false;
                        }
                        if (!CLDC_DataCore.Function.Number.IsNumeric(Value))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("����������!", false);
                            return false;
                        }
                        GlobalUnit.g_MsgControl.OutMessage(Bwh + "�������!", false);
                        ZZError = curMeter.MeterZZErrors[ItemKey];
                        ZZError.Mz_chrQiMa = float.Parse(Value);
                        break;
                    }
                case CLDC_Comm.Enum.Cus_stVerifyStep.��������¼ֹ��:
                    {
                        if (!curMeter.MeterZZErrors.ContainsKey(ItemKey))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("û�з������ݽڵ�", false);
                            return false;
                        }
                        if (!CLDC_DataCore.Function.Number.IsNumeric(Value))
                        {
                            GlobalUnit.g_MsgControl.OutMessage("����������!", false);
                            return false;
                        }
                        GlobalUnit.g_MsgControl.OutMessage(Bwh + "�������!", false);

                        ZZError = curMeter.MeterZZErrors[ItemKey];
                        ZZError.Mz_chrZiMa = float.Parse(Value);
                        break;
                    }
                default:
                    {
                        MessageBoxEx.Show(this,"�����״̬��" + m_VerifyStep.ToString());
                        break;
                    }
            }
            return true;
        }
        /// <summary>
        /// Ҫ�첻���¼�
        /// </summary>
        /// <param name="Bwh"></param>
        /// <param name="Value"></param>
        private void ClientTable_CheckOver(int Bwh, bool Value)
        {

            if (m_VerifyStep != CLDC_Comm.Enum.Cus_stVerifyStep.����¼��)
            {
                //���ǲ���¼�룬�������л�Ҫ��򲻼�
                ClientTable.SetCheckBoxValue(Bwh, CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1].YaoJianYn);
                return;
            }

            if (CLDC_DataCore.Const.GlobalUnit.g_CUS != null && CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1] != null)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[Bwh - 1].YaoJianYn = Value;
            }

            //֪ͨ������
            CLDC_DataCore.Command.Update.UpdateYaoJian_Ask cmdAsk = new CLDC_DataCore.Command.Update.UpdateYaoJian_Ask();
            cmdAsk.Bwh = Bwh - 1;
            cmdAsk.IsYaoJian = Value;
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = null;
            sendMessage(cmdAsk, out cmdAnswer);
        }

        private void ButtonClose_Click(object obj, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (MessageBoxEx.Show(this,"\r\n\r\nȷ��Ҫ�˳���?    \r\n\r\n", "�˳���ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        #region ----------��ť�¼�----------

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
        //����ɨ���밴ť
        private void ButtonRequest_Click(object obj, EventArgs e)
        {
            //���ư�ť,�춨״̬�²���������ɨ������
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨 &&
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.δ��ֵ��)
            {
                return;
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoCommand_AskForInputTxm));
        }

        /// <summary>
        /// ����ɨ�������
        /// </summary>
        /// <param name="objNull"></param>
        private void DoCommand_AskForInputTxm(object objNull)
        {
            string strMessage;
            MessageBoxEx.UseSystemLocalizedString = true;
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)
            {
                MessageBoxEx.Show(this,string.Format("{0} ״̬�²�����ִ�д˲���!", CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState.ToString()), "�����߼�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.����¼��;

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("����ɨ������������ڵȴ�����������...");

            CLDC_DataCore.Command.Txm.InputTxm_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputTxm_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnswer = new InputTxm_Answer();
            bool bResponse = true;
            sendMessage(cmdAsk, out cmdAnswer);
            if (cmdAnswer == null) //˵������ʧ��
            {
                bResponse = false;
            }
            else
            {
                bResponse = ((CLDC_DataCore.Command.Txm.InputTxm_Answer)cmdAnswer).bAgree;
            }
            //���ݷ��������ؽ�����ý������״̬
            //bResponse = true;

            ClientTable.ReadOnly = !bResponse;

            //�޸�״̬
            if (bResponse)
            {
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SetOption(CLDC_CTNProtocol.EnumOption.DZ����ɨ������);
                CLDC_DataCore.Const.GlobalUnit.UpdateActiveID(-1);      //���ü춨ID������¼��
                SetControlVisiable(ButtonOk, true);

                //�����������ActiveId�Ѿ��ı�
                CLDC_DataCore.Command.Update.UpdateActiveId_Ask cmdAsk2 = new CLDC_DataCore.Command.Update.UpdateActiveId_Ask();
                cmdAsk2.ActiveId = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID;
                CLDC_CTNProtocol.CTNPCommand cmdAnswer2;
                sendMessage(cmdAsk2, out cmdAnswer2);
            }

            //��ʾ���������صĽ��
            if (bResponse)
            {
                strMessage = "������ͬ�����ɨ��������������ڿ���¼������";
                SetLabText(labItem, CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Option.ToString().Substring(2));
                SetLabText(labAction, strMessage);
                //CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID = -1;
                ShowData(true);
            }
            else
            {
                strMessage = "��������ͬ�����ɨ������������Ժ�����";
                SetLabText(labItem, CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.Option.ToString().Substring(2));
                SetLabText(labAction, strMessage);
               // CLDC_Comm.Speechs.Speech.Instance.SpeakChina(strMessage);
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }


        //¼���������
        private void ButtonOk_Click(object obj, EventArgs e)
        {

            if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.����¼��)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DoCommand_ReportInputTxmComplated));
            }
            else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.��������¼���� ||
                    m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.��������¼ֹ��)
            {
                //����Ƿ����б�λ���Ѿ�¼��
                m_VerifyStep++;
                //��춨��������Ϣ
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
        /// ����¼���������
        /// </summary>
        /// <param name="obj"></param>
        private void DoCommand_ReportInputTxmComplated(object obj)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("���ڱ���¼������¼�...");

            CLDC_DataCore.Command.Txm.InputTxm_Complated_Ask cmdAsk = new CLDC_DataCore.Command.Txm.InputTxm_Complated_Ask();
            CLDC_CTNProtocol.CTNPCommand cmdAnswer;
            bool bResponse = true; //sendMessage(cmdAsk, out cmdAnswer);
            cmdAsk.Option = CLDC_CTNProtocol.EnumOption.EZɨ���������;
            sendMessage(cmdAsk, out cmdAnswer);
            if (bResponse)
            {
                //�޸�״̬
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.SetOption(CLDC_CTNProtocol.EnumOption.EZɨ���������);
                SetControlVisiable(ButtonOk, false);
            }

            //��ʾ���������صĽ��
            if (bResponse)
            {
                SetLabText(labAction, "�������Ѿ��ɹ��յ����󣬵ȴ���һ������...");
            }
            else
            {
                SetLabText(labAction, "������ִ���!������");
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }


        //������������
        private void ButtonRequestControl_Click(object obj, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoCommand_AskForControlling));
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="objNull"></param>
        private void DoCommand_AskForControlling(object objNull)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("�����������Ʋ��������ڵȴ�����������...");
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
                SetLabText(labAction, "������ͬ�����������ƵĲ���,�ȴ��л�...");
            }
            else
            {
                SetLabText(labAction, "��������ͬ�����������Ʋ��������Ժ�����");
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();

        }

        //ϵͳ���ð�ť
        private void ButtonSystemConfig_Click(object sender, EventArgs e)
        {
            CLDC_MeterUI.UI_SystemManager _FrmSysConfig = new UI_SystemManager(GlobalUnit.g_SystemConfig);
            _FrmSysConfig.Show(true, true);
            sendMeterGoroup();
        }

        #endregion

        #region ----------���ܸ���----------

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="LabShow">Ҫ��ʾ��Ŀ��</param>
        /// <param name="Message">Ҫ��ʾ����Ϣ����</param>
        private void ShowRunMessage(Label LabShow, string Message)
        {
            if (this.IsHandleCreated)
                this.BeginInvoke(new OnEventShowLabText(ShowLabText), new object[] { null, LabShow, Message });
        }

        //��ʾ��Ϣ
        private void ShowLabText(object obj, Label labShow, string Message)
        {
            labShow.Text = Message;
        }
        //���±�λ�Ƿ�Ҫ��
        private void changeYaoJian(int BW, bool bYaoJian)
        {
            ClientTable.SetCheckBoxValue(BW + 1, bYaoJian);

        }
        #endregion

        #region ----------�춨����ӿ��¼�----------

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        private List<object> CheckPlan = new List<object>();        //�����б� 

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        private void showSchemeInfo()
        {
            int FirstYJIndex = CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter;
            if (FirstYJIndex == -1)
            {
                ShowRunMessage(labAction, "û��Ҫ�춨�ĵ��ܱ�");
                FirstYJIndex = 0;
            }

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo
                    firstMeter = CLDC_DataCore.Const.GlobalUnit.Meter(FirstYJIndex);

            string strFAName = string.Empty;                    //��������
            strFAName = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.FaName;
            CheckPlan = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckPlan;

            if (firstMeter != null)
            {
                //��ʾ��������
                ShowRunMessage(labSchemeName, strFAName);
                //��ʾ�춨��Ŀ����
                if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID >= 0 &&
                    CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID < CheckPlan.Count)
                {

                    ShowRunMessage(labItem, CheckPlan[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID].ToString());
                }
            }
        }

        /// <summary>
        /// ˢ����ʾ�ͻ�������
        /// <param name="ShowDataOnly">�Ƿ�ֻˢ����������</param>
        /// </summary>
        private void ShowData(bool ShowDataOnly)
        {
            if (!ShowDataOnly)
                CLDC_DataCore.Function.ThreadCallBack.BeginInvoke(this, new CLDC_DataCore.Function.CallBack(showSchemeInfo), 10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(thShowData));
            //���ư�ť,�춨״̬�²���������ɨ������
            if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨 &&
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.δ��ֵ��)
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
                    /*�����ʾ�еı�λ����Ǵ�1��ʼ*/
                    ClientTable.SetCheckBoxValue(bw + 1, curMeter.YaoJianYn);
                    if (!curMeter.YaoJianYn) continue;
                    if (CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID < 0 ||
                        CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)
                    {
                        //����¼��״̬��ˢ��ʱ��ʾ������
                        strMessageValue = curMeter.Mb_ChrTxm;
                    }
                    else
                    {
                        //������֤
                        //if (curMeter.MeterPlan == null || curMeter.MeterPlan.CheckPlan == null) continue;
                        //if (CheckPlan.Count <= CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID) return;

                        strKey = "";
                        object curPlan = CheckPlan[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID];
                        if (curPlan is CLDC_DataCore.Struct.StPlan_ZouZi)
                        {
                            strKey = ((CLDC_DataCore.Struct.StPlan_ZouZi)curPlan).PrjID;
                        }
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult curResult = null;

                        #region Ԥ��������ʾ
                        if (curPlan is StPlan_YuRe)
                        {
                            strMessageValue = "Ԥ����";
                        }
                        #endregion

                        #region ��/��������
                        else if (curPlan is StPlan_QiDong)
                        {
                            strKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.������).ToString("D3");
                            strKey += ((int)((StPlan_QiDong)curPlan).PowerFangXiang).ToString();

                            if (curMeter.MeterResults.ContainsKey(strKey))
                            {
                                curResult = curMeter.MeterResults[strKey];
                                strMessageValue = curResult.Mr_chrRstValue;
                                isRead = curResult.Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                            }
                            else
                            {
                                strMessageValue = "׼���춨";
                            }
                        }
                        #endregion

                        #region Ǳ��������ʾ
                        else if (curPlan is StPlan_QianDong)
                        {
                            strKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.Ǳ������).ToString("000");
                            strKey += ((int)((StPlan_QianDong)curPlan).PowerFangXiang).ToString();

                            if (curMeter.MeterResults.ContainsKey(strKey))
                            {
                                curResult = curMeter.MeterResults[strKey];

                                strMessageValue = curResult.Mr_chrRstValue;
                                isRead = curResult.Mr_chrRstValue == CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                            }
                            else
                            {
                                strMessageValue = "׼���춨";
                            }
                        }
                        #endregion

                        #region �������/��׼ƫ��
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

                        #region ----------����춨-----------
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

                        #region ��������
                        else if (curPlan is StPlan_ZouZi)
                        {
                            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError curZZerror = null;
                            if (curMeter.MeterZZErrors.ContainsKey(strKey))
                            {
                                curZZerror = curMeter.MeterZZErrors[strKey];
                                if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.��������¼���� || m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.��������¼ֹ��)
                                {
                                    strMessageValue = "";
                                }
                                else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.��������¼ֹ�����)
                                {
                                    //ֹ��
                                    strMessageValue = curZZerror.Mz_chrZiMa.ToString();
                                }
                                else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.��������¼�������)
                                {
                                    //����
                                    strMessageValue = curZZerror.Mz_chrQiMa.ToString();
                                }
                                else if (m_VerifyStep == CLDC_Comm.Enum.Cus_stVerifyStep.����������)
                                {
                                    //����
                                    strMessageValue = curZZerror.Mz_chrWc.ToString();
                                }
                                else
                                {
                                    //��;����
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

                        #region �๦������
                        else if (curPlan is CLDC_DataCore.Struct.StPlan_Dgn)
                        {
                            strMessageValue = "�춨��";
                            CLDC_DataCore.Struct.StPlan_Dgn DgnPlan = (CLDC_DataCore.Struct.StPlan_Dgn)curPlan;

                        }
                        #endregion

                        #region �ز�����
                        else if (curPlan is StPlan_Carrier)
                        {
                            strMessageValue = "�춨��";
                            StPlan_Carrier CarrierPlan = (StPlan_Carrier)curPlan;
                        }
                        #endregion

                        else
                        {
                            //MUSTDO:���֣��๦�ܼ춨�ͻ�����ʾ��û����
                        }
                    }
                    //���µ�UI
                    ClientTable.SetTextValue(bw + 1, strMessageValue);
                    ClientTable.SetTextBackColorValue(bw + 1, isRead);
                }
            }
        }

        #endregion

        #region----------�춨���ƣ����������շ�----------

        /// <summary>
        /// ��������ģ��
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
                //TODO:�����������ݿ�ȥ
            }
        }
        /// <summary>
        /// �����������ݰ�
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


        #region----------������ҷ----------
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
        /// ˫���л���󻯡���С��
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

        #region ----------ITopUI ��Ա----------

        /// <summary>
        /// ����̨���λ����
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
        /// ̨����
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
        /// �����ⲿ���㴰��
        /// </summary>
        private Office2007Form m_parentFrom;
        public Office2007Form ParentFormHandle
        {
            set { m_parentFrom = value as Office2007Form; }
            get { return m_parentFrom; }
        }
        /// <summary>= 
        /// ����̨������
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
        ///// ˢ������
        ///// </summary>
        ///// <param name="DnbData">���ܱ����ݶ���</param>
        //public void SetData( CLDC_DataCore.Model.DnbModel.DnbGroupInfo DnbData)
        //{
        //    ShowData(true);
        //    //throw new Exception("The method or operation is not implemented.");
        //}
        /// <summary>
        /// ֻˢ��������
        /// </summary>
        public void SetData()
        {
            ShowData(true);
        }

        /// <summary>
        /// ��ʾ״̬��Ϣ
        /// </summary>
        /// <param name="strMessage"></param>
        public void SetStatus(string strMessage)
        {
            ShowRunMessage(labAction, strMessage);
        }

        /// <summary>
        /// ��ͨ��Ϣ���д���
        /// </summary>
        /// <param name="sourceAdpater">��Ϣ������</param>
        /// <param name="VerifyDataArgs">��Ϣ����</param>
        public void OnMsgMessage(object sourceAdpater, object VerifyDataArgs)
        {
            CLDC_Comm.MessageArgs.EventMessageArgs _Message = VerifyDataArgs as CLDC_Comm.MessageArgs.EventMessageArgs;
            if (_Message == null) return;
            int FirstYJMeter = CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter;
            _Message.Message = _Message.Message.Replace(@"\r\n", ";");
            switch (_Message.MessageType)
            {

                //�춨���л�
                case CLDC_Comm.Enum.Cus_MessageType.�춨����:
                    {
                        showSchemeInfo();
                        ShowRunMessage(labAction, "�����л��춨��...");
                        return;
                    }
                case CLDC_Comm.Enum.Cus_MessageType.����ʱ��Ϣ:
                    {
                        showSchemeInfo();
                        if (_Message.Message != "null")
                        {
                            //
                            ShowRunMessage(labAction, _Message.Message);
                        }
                        break;
                    }
                case CLDC_Comm.Enum.Cus_MessageType.�춨���:
                    {
                        ShowRunMessage(labAction, _Message.Message);
                        break;
                    }

                default:
                    {
                        if (_Message.MessageType == CLDC_Comm.Enum.Cus_MessageType.¼��������� || _Message.MessageType == CLDC_Comm.Enum.Cus_MessageType.¼�����ֹ��)
                        {
                            ShowData(true);             //��ˢ��һ����ʾ�������� 
                            CLDC_Comm.MessageArgs.EventMessageArgs _E = VerifyDataArgs as CLDC_Comm.MessageArgs.EventMessageArgs;
                            if (_E == null) return;
                            ShowData(false);
                            if (_E.Message == "null") return;
                            bool bQiMa = (_Message.MessageType == CLDC_Comm.Enum.Cus_MessageType.¼��������� ? true : false);
                            string strDes = string.Empty;
                            if (bQiMa)
                            {
                                strDes = "����";
                                m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.��������¼����;
                            }
                            else
                            {
                                strDes = "ֹ��";
                                m_VerifyStep = CLDC_Comm.Enum.Cus_stVerifyStep.��������¼ֹ��;
                            }
                            SetControlVisiable(ButtonOk, true);
                            ClientTable.ReadOnly = false;
                            ShowRunMessage(labAction, "�����뱻����" + strDes + "");
                            MessageBoxEx.UseSystemLocalizedString = true;
                            MessageBoxEx.Show(this,_Message.Message + strDes + "����¼����ɣ�", "ϵͳ��ʾ");
                            break;
                        }
                        break;
                    }
            }
            //����Ƿ�Ҫˢ������
            if (_Message.RefreshData)
            {
                ShowData(true);
            }
        }
        #endregion

        #region IMeterInfoUpdateDownEnablecs ��Ա

        public event CLDC_DataCore.Interfaces.GetMeterInfo OnGetMeterInfo;

        #endregion
    }
}
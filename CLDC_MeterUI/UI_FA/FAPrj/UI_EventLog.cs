using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    /// <summary>
    /// �¼���¼
    /// </summary>
    public partial class UI_EventLog :UI_TableBase//UserControl
    {

        private ControlLocation EventLogControlUI;

        #region ------------------����-----------------
        public UI_EventLog()
        {
            InitializeComponent();
            this.InitUI();
        }

        public UI_EventLog(CLDC_Comm.Enum.Cus_TaiType Ttype)
            :base(Ttype,"")
        {
            InitializeComponent();
            this.InitUI();

        }

        public UI_EventLog(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_EventLog(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_EventLog FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);
        }

        #endregion 

        #region -------------------˽�з���������--------------
        /// <summary>
        /// UI��ʼ��
        /// </summary>
        private void InitUI()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME);
            
            PrjUI.PrjEventLog.EventLogBase Item;

            EventLogControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csEventLogDic _EventLogItem = new CLDC_DataCore.SystemModel.Item.csEventLogDic();
            _EventLogItem.Load();

            //ʧѹ
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.ʧѹ��¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //��ѹ
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.��ѹ��¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //Ƿѹ
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.Ƿѹ��¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //ʧ��
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.ʧ����¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //����
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //����
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.���ؼ�¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //����
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.�����¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);
            

            //����
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.�����¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //ȫʧѹ
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.ȫʧѹ��¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //��ѹ��ƽ���¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.��ѹ��ƽ���¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //������ƽ���¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.������ƽ���¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //��ѹ�������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.��ѹ�������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //�����������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.�����������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //����Ǽ�¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.����Ǽ�¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //����ť�м�¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.����ť�м�¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //���
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.��̼�¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //Уʱ��¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.Уʱ��¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //���������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.���������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //�¼������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.�¼������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //��������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.��������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //���������¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.���������¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //���ʷ����¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.���ʷ����¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);            

            //�������޼�¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.�������޼�¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //�������������޼�¼
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.�������������޼�¼).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.NotParmPrj(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.������¼_ZB).ToString("000")));
            //Item.PanelBackColor = SystemColors.Control;
            //Item.CaptionColor = Color.Silver;
            //this.Panel_Prjs.Controls.Add(Item);
            //EventLogControlUI.Add(Item);

            //EventLogControlUI.Sort();

            this.SizeChanged += new EventHandler(UI_EventLog_SizeChanged);
        }




        #endregion 

        #region---------------��������������--------------

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="faname">��������</param>
        public void LoadFA(string faname)
        {
            CLDC_DataCore.Model.Plan.Plan_EventLog _EventLog = new CLDC_DataCore.Model.Plan.Plan_EventLog((int)base.TaiType, faname);

            this.LoadFA(_EventLog);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_EventLog FaItem)
        {
            base.FaName = FaItem.Name;

            try
            {
                Cmb_Fa.Text = FaItem.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }

            this.EventLogControlUI.LoadFA(FaItem);

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_EventLog Copy()
        {
            return EventLogControlUI.Copy(base.TaiType,base.FaName);
        }

        #endregion 

        #region-------------------�¼�-----------------

        private delegate void DetSizeChanged();

        /// <summary>
        /// ����ߴ�仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_EventLog_SizeChanged(object sender, EventArgs e)
        {
            if (Panel_Prjs.IsHandleCreated)
            {
                Panel_Prjs.BeginInvoke(new DetSizeChanged(DelaySizeChanged));
            }


        }

        private void DelaySizeChanged()
        {
            System.Threading.Thread.Sleep(1);      //ί���ӳ�1���룬��Panel��Create��ȥ��ӦSizeChanged�¼�

            if (Panel_Prjs.VerticalScroll.Visible)
            {
                EventLogControlUI.SetWidth(Panel_Prjs.Width - 16);
            }
            else
            {
                EventLogControlUI.SetWidth(Panel_Prjs.Width);
            }
        }


        /// <summary>
        /// ����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;

            this.LoadFA( Cmb_Fa.Text);
        }

        #endregion 



        #region --------------------˽�У�������������-------------------------------
        /// <summary>
        /// �ؼ�λ�ö�λ��
        /// </summary>
        private class ControlLocation
        {
            /// <summary>
            /// �ؼ������
            /// </summary>
            const int CONST_JIANJU = 3;
            /// <summary>
            /// ��߾�
            /// </summary>
            const int CONST_LEFT = 3;

            private List<PrjUI.PrjEventLog.EventLogBase> EventLogControls;

            /// <summary>
            /// �������
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjEventLog.EventLogBase> EventLogControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                EventLogControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.EventLogBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------�¼�--------------------------
            /// <summary>
            /// Ҫ����Ŀ����
            /// </summary>
            /// <param name="Index"></param>
            private void EventLogControl_PrjSort(object sender, int Index)
            {
                PrjUI.PrjEventLog.EventLogBase Item = sender as PrjUI.PrjEventLog.EventLogBase;

                if (EventLogControls.Contains(Item))
                {
                    EventLogControlsSort.Add(Index, Item);
                }

            }

            /// <summary>
            /// �Ƿ����ƶ�
            /// </summary>
            private bool IsMoved = false;

            /// <summary>
            /// ����϶��¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void EventLogControl_MouseMoves(object sender, MouseEventArgs e)
            {
                IsMoved = true;
                if (_CtrParent.VerticalScroll.Visible)
                {
                    if (((Control)sender).Top < 0)
                    {
                        if (_CtrParent.VerticalScroll.Value > 0)
                        {
                            int MoveValue = 0;
                            MoveValue = _CtrParent.VerticalScroll.Value + ((Control)sender).Top;
                            if (MoveValue > 0)
                            {
                                _CtrParent.VerticalScroll.Value = MoveValue;

                            }
                            else
                            {
                                _CtrParent.VerticalScroll.Value = 0;
                            }
                        }
                    }
                    else if (((Control)sender).Top > _CtrParent.Height)
                    {
                        int MoveValue = 0;
                        MoveValue = _CtrParent.VerticalScroll.Value + ((Control)sender).Top - _CtrParent.Height;
                        if (MoveValue < _CtrParent.VerticalScroll.Maximum)
                        {
                            _CtrParent.VerticalScroll.Value = MoveValue;
                        }
                        else
                        {
                            _CtrParent.VerticalScroll.Value = _CtrParent.VerticalScroll.Maximum;
                        }
                    }
                }
            }
            /// <summary>
            /// �ؼ�����ƶ�����¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void EventLogControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //����ؼ�û���ƶ���
                IsMoved = false;
                bool IsInsert = false;
                PrjUI.PrjEventLog.EventLogBase Item = sender as PrjUI.PrjEventLog.EventLogBase;

                int Index = EventLogControls.FindIndex(delegate(PrjUI.PrjEventLog.EventLogBase DgnItem) { return DgnItem == Item; });

                EventLogControls.Remove(Item);

                for (int i = 0; i < EventLogControls.Count; i++)
                {
                    if (Item.Top < EventLogControls[i].Top + EventLogControls[i].Height)
                    {
                        EventLogControls.Insert(i, Item);
                        IsInsert = true;
                        Index=i>=Index?Index:i;
                        break;
                    }
                }
                if (!IsInsert)
                {
                    EventLogControls.Add(Item);
                }

                int ScrollValue = 0;
                if (_CtrParent.VerticalScroll.Visible)
                {
                    ScrollValue = _CtrParent.VerticalScroll.Value;
                }
                this.Sort(Index);
                if (_CtrParent.VerticalScroll.Visible)
                {
                    _CtrParent.VerticalScroll.Value = ScrollValue;
                }

            }

            #endregion 

            #region  ------------��������-------------
            /// <summary>
            /// ��ӿؼ�Ԫ��
            /// </summary>
            /// <param name="DgnControl"></param>
            public void Add(PrjUI.PrjEventLog.EventLogBase EventLogControl)
            {
                EventLogControl.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.EventLogBase.EventMouseMoveOver(EventLogControl_MouseMoveOver);
                EventLogControl.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.EventLogBase.EventMouseMove(EventLogControl_MouseMoves);
                EventLogControl.PrjSort += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.EventLogBase.EventPrjSort(EventLogControl_PrjSort);
                EventLogControls.Add(EventLogControl);

            }

            public void Sort()
            {
                this.Sort(0);
            }

            /// <summary>
            /// ���򷽷�
            /// <param name="Index">��Ŀ�ؼ��±�</param>
            /// </summary>
            public void Sort(int Index)
            {

                if (_CtrParent.VerticalScroll.Visible)
                {
                    _CtrParent.VerticalScroll.Value = _CtrParent.VerticalScroll.Minimum;
                }

                int intTop = 0;

                for (int i = Index; i < EventLogControls.Count; i++)
                {
                    if (i > 0)
                    {
                        intTop = EventLogControls[i - 1].Top + EventLogControls[i - 1].Height + CONST_JIANJU;
                    }
                    else
                    {
                        intTop = CONST_JIANJU;
                    }

                    EventLogControls[i].Location = new Point(CONST_LEFT, intTop);
                    EventLogControls[i].ID = i + 1 ;
                }


            }

            /// <summary>
            /// ���ÿؼ������
            /// </summary>
            /// <param name="Width"></param>
            public void SetWidth(int Width)
            {
                foreach (PrjUI.PrjEventLog.EventLogBase Item in EventLogControls)
                {
                    Item.Width = Width - CONST_LEFT * 2;
                }
            }

            /// <summary>
            /// ��������
            /// </summary>
            /// <param name="FaItem"></param>
            public void LoadFA(CLDC_DataCore.Model.Plan.Plan_EventLog FaItem)
            {
                EventLogControlsSort = new SortedDictionary<int, CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.EventLogBase>();
                for (int i = 0; i < EventLogControls.Count; i++)
                {
                    EventLogControls[i].LoadFA(FaItem);
                }


                foreach(int i in EventLogControlsSort.Keys)
                {
                    EventLogControls.Remove(EventLogControlsSort[i]);
                    EventLogControls.Insert(i, EventLogControlsSort[i]);
                    
                }
                this.Sort();
                EventLogControlsSort = null;
            }

            /// <summary>
            /// ������Ҫ�춨�ķ���
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_EventLog Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_EventLog EventLog = new CLDC_DataCore.Model.Plan.Plan_EventLog((int)TaiType, "");           //����һ���µĶ๦�ܷ���

                for (int i = 0; i < EventLogControls.Count; i++)
                {
                    PrjUI.PrjEventLog.EventLogBase Item=EventLogControls[i];
                    if (Item.IsCheck)
                    {
                        EventLog.Add(Item.EventLogID, Item.EventLogName, Item.EventLogPlanPrj.OutPramerter.Jion(), Item.Parm);
                    }
                    
                }
                EventLog.SetPram((int)TaiType, FaName);
                return EventLog;
            }


            #endregion


        }
        #endregion 
    }
}

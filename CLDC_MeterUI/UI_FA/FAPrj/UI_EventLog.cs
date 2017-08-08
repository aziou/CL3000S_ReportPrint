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
    /// 事件记录
    /// </summary>
    public partial class UI_EventLog :UI_TableBase//UserControl
    {

        private ControlLocation EventLogControlUI;

        #region ------------------构造-----------------
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

        #region -------------------私有方法、函数--------------
        /// <summary>
        /// UI初始化
        /// </summary>
        private void InitUI()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME);
            
            PrjUI.PrjEventLog.EventLogBase Item;

            EventLogControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csEventLogDic _EventLogItem = new CLDC_DataCore.SystemModel.Item.csEventLogDic();
            _EventLogItem.Load();

            //失压
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.失压记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //过压
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.过压记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //欠压
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.欠压记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //失流
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.失流记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //断流
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.断流记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //过流记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.过流记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //过载
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.过载记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //断相
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.断相记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);
            

            //掉电
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.掉电记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //全失压
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.全失压记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //电压不平衡记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.电压不平衡记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //电流不平衡记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.电流不平衡记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //电压逆相序记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.电压逆相序记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //电流逆相序记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.电流逆相序记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //开表盖记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.开表盖记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //开端钮盒记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.开端钮盒记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //编程
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.编程记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //校时记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.校时记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //需量清零记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.需量清零记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //事件清零记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.事件清零记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //电表清零记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.电表清零记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //潮流反向记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.潮流反向记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //功率反向记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLosePhase(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.功率反向记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);            

            //需量超限记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.需量超限记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //功率因数超下限记录
            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.ELLoseComm(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.功率因数超下限记录).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            EventLogControlUI.Add(Item);

            //Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.NotParmPrj(_EventLogItem.getEventLogPrj(((int)CLDC_Comm.Enum.Cus_EventLogItem.过流记录_ZB).ToString("000")));
            //Item.PanelBackColor = SystemColors.Control;
            //Item.CaptionColor = Color.Silver;
            //this.Panel_Prjs.Controls.Add(Item);
            //EventLogControlUI.Add(Item);

            //EventLogControlUI.Sort();

            this.SizeChanged += new EventHandler(UI_EventLog_SizeChanged);
        }




        #endregion 

        #region---------------公共方法、函数--------------

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="faname">方案名称</param>
        public void LoadFA(string faname)
        {
            CLDC_DataCore.Model.Plan.Plan_EventLog _EventLog = new CLDC_DataCore.Model.Plan.Plan_EventLog((int)base.TaiType, faname);

            this.LoadFA(_EventLog);
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案项目</param>
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
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_EventLog Copy()
        {
            return EventLogControlUI.Copy(base.TaiType,base.FaName);
        }

        #endregion 

        #region-------------------事件-----------------

        private delegate void DetSizeChanged();

        /// <summary>
        /// 窗体尺寸变化
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
            System.Threading.Thread.Sleep(1);      //委托延迟1毫秒，让Panel先Create再去响应SizeChanged事件

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
        /// 方案选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;

            this.LoadFA( Cmb_Fa.Text);
        }

        #endregion 



        #region --------------------私有，面板组件控制类-------------------------------
        /// <summary>
        /// 控件位置定位类
        /// </summary>
        private class ControlLocation
        {
            /// <summary>
            /// 控件见间距
            /// </summary>
            const int CONST_JIANJU = 3;
            /// <summary>
            /// 左边距
            /// </summary>
            const int CONST_LEFT = 3;

            private List<PrjUI.PrjEventLog.EventLogBase> EventLogControls;

            /// <summary>
            /// 父域面板
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjEventLog.EventLogBase> EventLogControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                EventLogControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog.EventLogBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------事件--------------------------
            /// <summary>
            /// 要检项目排序
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
            /// 是否有移动
            /// </summary>
            private bool IsMoved = false;

            /// <summary>
            /// 面板拖动事件
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
            /// 控件面板移动完毕事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void EventLogControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //如果控件没有移动过
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

            #region  ------------公共方法-------------
            /// <summary>
            /// 添加控件元素
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
            /// 排序方法
            /// <param name="Index">项目控件下标</param>
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
            /// 设置控件集宽度
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
            /// 方案加载
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
            /// 拷贝需要检定的方案
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_EventLog Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_EventLog EventLog = new CLDC_DataCore.Model.Plan.Plan_EventLog((int)TaiType, "");           //创建一个新的多功能方案

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

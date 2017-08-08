using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_Freeze : UI_TableBase
    {
        private ControlLocation FreezeControlUI;

        #region ------------------构造函数-----------------

        public UI_Freeze()
        {
            InitializeComponent();
            this.InitUI();
        }

        public UI_Freeze(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            this.InitUI();

        }

        public UI_Freeze(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_Freeze(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_Freeze FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            //this.LoadFA(FaItem);
        }
        #endregion

        #region -------------------私有方法、函数--------------
        /// <summary>
        /// UI初始化
        /// </summary>
        private void InitUI()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_FZ_FOLDERNAME);

            PrjUI.PrjFreeze.FreezeBase Item;

            FreezeControlUI = new ControlLocation(pnl_Prjs);

            CLDC_DataCore.SystemModel.Item.csFreezeDic _FreezeItem = new CLDC_DataCore.SystemModel.Item.csFreezeDic();
            _FreezeItem.Load();

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.FreezeTiming(_FreezeItem.getFreezePrj(((int)CLDC_Comm.Enum.Cus_FreezeItem.定时冻结).ToString("000")));
            this.pnl_Prjs.Controls.Add(Item);
            FreezeControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.NotParmPrj(_FreezeItem.getFreezePrj(((int)CLDC_Comm.Enum.Cus_FreezeItem.约定冻结).ToString("000")));
            this.pnl_Prjs.Controls.Add(Item);
            FreezeControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.NotParmPrj(_FreezeItem.getFreezePrj(((int)CLDC_Comm.Enum.Cus_FreezeItem.瞬时冻结).ToString("000")));
            this.pnl_Prjs.Controls.Add(Item);
            FreezeControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.NotParmPrj(_FreezeItem.getFreezePrj(((int)CLDC_Comm.Enum.Cus_FreezeItem.日冻结).ToString("000")));
            this.pnl_Prjs.Controls.Add(Item);
            FreezeControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.NotParmPrj(_FreezeItem.getFreezePrj(((int)CLDC_Comm.Enum.Cus_FreezeItem.整点冻结).ToString("000")));
            this.pnl_Prjs.Controls.Add(Item);
            FreezeControlUI.Add(Item);

            FreezeControlUI.Sort();

            this.SizeChanged += new EventHandler(UI_Freeze_SizeChanged);
        }
        #endregion

        #region---------------公共方法、函数--------------

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="faname">方案名称</param>
        public void LoadFA(string faname)
        {
            CLDC_DataCore.Model.Plan.Plan_Freeze _Freeze = new CLDC_DataCore.Model.Plan.Plan_Freeze((int)base.TaiType, faname);

            this.LoadFA(_Freeze);
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Freeze FaItem)
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

            this.FreezeControlUI.LoadFA(FaItem);

        }

        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_Freeze Copy()
        {
            return FreezeControlUI.Copy(base.TaiType, base.FaName);
        }

        #endregion

        #region-------------------事件-----------------

        private delegate void DetSizeChanged();

        /// <summary>
        /// 窗体尺寸变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Freeze_SizeChanged(object sender, EventArgs e)
        {
            if (pnl_Prjs.IsHandleCreated)
            {
                pnl_Prjs.BeginInvoke(new DetSizeChanged(DelaySizeChanged));
            }
        }

        private void DelaySizeChanged()
        {
            System.Threading.Thread.Sleep(1);      //委托延迟1毫秒，让Panel先Create再去响应SizeChanged事件

            if (pnl_Prjs.VerticalScroll.Visible)
            {
                FreezeControlUI.SetWidth(pnl_Prjs.Width - 16);
            }
            else
            {
                FreezeControlUI.SetWidth(pnl_Prjs.Width);
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

            this.LoadFA(Cmb_Fa.Text);
        }

        /// <summary>
        /// 清理方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            foreach (Control Item in pnl_Prjs.Controls)
            {
                if (Item is PrjUI.PrjFreeze.FreezeBase)
                {
                    ((PrjUI.PrjFreeze.FreezeBase)Item).SetCheck(false);
                }
            }
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

            private List<PrjUI.PrjFreeze.FreezeBase> FreezeControls;

            /// <summary>
            /// 父域面板
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjFreeze.FreezeBase> FreezeControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                FreezeControls = new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.FreezeBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------事件--------------------------
            /// <summary>
            /// 要检项目排序
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="Index"></param>
            private void FreezeControl_PrjSort(object sender, int Index)
            {
                PrjUI.PrjFreeze.FreezeBase Item = sender as PrjUI.PrjFreeze.FreezeBase;

                if (FreezeControls.Contains(Item))
                {
                    FreezeControlsSort.Add(Index, Item);
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
            private void FreezeControl_MouseMoves(object sender, MouseEventArgs e)
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
            private void FreezeControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //如果控件没有移动过
                IsMoved = false;
                bool IsInsert = false;
                PrjUI.PrjFreeze.FreezeBase Item = sender as PrjUI.PrjFreeze.FreezeBase;

                int Index = FreezeControls.FindIndex(delegate(PrjUI.PrjFreeze.FreezeBase FreezeItem) { return FreezeItem == Item; });

                FreezeControls.Remove(Item);

                for (int i = 0; i < FreezeControls.Count; i++)
                {
                    if (Item.Top < FreezeControls[i].Top + FreezeControls[i].Height)
                    {
                        FreezeControls.Insert(i, Item);
                        IsInsert = true;
                        Index = i >= Index ? Index : i;
                        break;
                    }
                }
                if (!IsInsert)
                {
                    FreezeControls.Add(Item);
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
            /// <param name="FreezeControl"></param>
            public void Add(PrjUI.PrjFreeze.FreezeBase FreezeControl)
            {
                FreezeControl.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.FreezeBase.EventMouseMoveOver(FreezeControl_MouseMoveOver);
                FreezeControl.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.FreezeBase.EventMouseMove(FreezeControl_MouseMoves);
                FreezeControl.PrjSort += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.FreezeBase.EventPrjSort(FreezeControl_PrjSort);
                FreezeControls.Add(FreezeControl);

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

                for (int i = Index; i < FreezeControls.Count; i++)
                {
                    if (i > 0)
                    {
                        intTop = FreezeControls[i - 1].Top + FreezeControls[i - 1].Height + CONST_JIANJU;
                    }
                    else
                    {
                        intTop = CONST_JIANJU;
                    }

                    FreezeControls[i].Location = new Point(CONST_LEFT, intTop);
                    FreezeControls[i].ID = i + 1;
                }


            }

            /// <summary>
            /// 设置控件集宽度
            /// </summary>
            /// <param name="Width"></param>
            public void SetWidth(int Width)
            {
                foreach (PrjUI.PrjFreeze.FreezeBase Item in FreezeControls)
                {
                    Item.Width = Width - CONST_LEFT * 2;
                }
            }

            /// <summary>
            /// 方案加载
            /// </summary>
            /// <param name="FaItem"></param>
            public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Freeze FaItem)
            {
                FreezeControlsSort = new SortedDictionary<int, CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze.FreezeBase>();
                for (int i = 0; i < FreezeControls.Count; i++)
                {
                    FreezeControls[i].LoadFA(FaItem);
                }


                foreach (int i in FreezeControlsSort.Keys)
                {
                    FreezeControls.Remove(FreezeControlsSort[i]);
                    FreezeControls.Insert(i, FreezeControlsSort[i]);
                }
                this.Sort();
                FreezeControlsSort = null;
            }

            /// <summary>
            /// 拷贝需要检定的方案
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_Freeze Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_Freeze Freeze = new CLDC_DataCore.Model.Plan.Plan_Freeze((int)TaiType, "");           //创建一个新的多功能方案

                for (int i = 0; i < FreezeControls.Count; i++)
                {
                    PrjUI.PrjFreeze.FreezeBase Item = FreezeControls[i];
                    if (Item.IsCheck)
                    {
                        Freeze.Add(Item.FreezeID, Item.FreezeName, Item.FreezePlanPrj.OutPramerter.Jion(), Item.Parm);
                    }

                }
                Freeze.SetPram((int)TaiType, FaName);
                return Freeze;
            }
            #endregion
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_Special :UI_TableBase //UserControl
    {



        private ControlLocation SpecialControlUI;

        #region ----------------构造-------------------
        public UI_Special()
        {
            InitializeComponent();
            this.InitUI();
        }

        public UI_Special(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            this.InitUI();
        }

        public UI_Special(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_Special(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_Specal FaItem)
            :base(Ttype,FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);
        }

        #endregion




        #region-------------私有函数、方法-------------
        /// <summary>
        /// 初始化UI
        /// </summary>
        private void InitUI()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_TS_FOLDERNAME);
            Cmb_Fa.SelectionChangeCommitted += new EventHandler(Cmb_Fa_SelectionChangeCommitted); 
            Cmd_ShowXieBo.Click += new EventHandler(Cmd_ShowXieBo_Click);
            Cmd_Clear.Click += new EventHandler(Cmd_Clear_Click);
            Cmd_AddNew.Click += new EventHandler(Cmd_AddNew_Click);
            SpecialControlUI = new ControlLocation(this.Panel_Items);
        }

        #endregion

        #region-----------事件-----------------

        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            SpecialControlUI.Clear();
        }

        /// <summary>
        /// 刷新方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;
            this.LoadFA(Cmb_Fa.Text);
        }

        /// <summary>
        /// 新增一个特殊检定项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_AddNew_Click(object sender, EventArgs e)
        {
            SpecialControlUI.Add(new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj());
        }

        /// <summary>
        /// 显示谐波方案窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_ShowXieBo_Click(object sender, EventArgs e)
        {
            Frm_XieBoSetup Frm = new Frm_XieBoSetup();
            Frm.ShowDialog();
        }

        #endregion 

        #region-------------公有函数、方法------------
        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="faname">方案名称</param>
        public void LoadFA(string faname)
        {
            CLDC_DataCore.Model.Plan.Plan_Specal _Specal = new CLDC_DataCore.Model.Plan.Plan_Specal((int)base.TaiType, faname);

            this.LoadFA(_Specal);
        }

        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Specal FaItem)
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
            SpecialControlUI.LoadFA(FaItem);
        }

        public CLDC_DataCore.Model.Plan.Plan_Specal Copy()
        {
            return SpecialControlUI.Copy(base.TaiType, base.FaName);
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

            private List<PrjUI.UI_SpecialPrj> SpecialControls;

            /// <summary>
            /// 父域面板
            /// </summary>
            private Panel _CtrParent = null;


            public ControlLocation(Panel CtrParent)
            {
                SpecialControls = new List<PrjUI.UI_SpecialPrj>();
                _CtrParent = CtrParent;
                _CtrParent.SizeChanged += new EventHandler(_CtrParent_SizeChanged);
            }



            #region ------------------事件--------------------------

            /// <summary>
            /// 是否有移动
            /// </summary>
            private bool IsMoved = false;

            /// <summary>
            /// 面板拖动事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void SpecialControl_MouseMoves(object sender, MouseEventArgs e)
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
            private void SpecialControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //如果控件没有移动过
                IsMoved = false;
                bool IsInsert = false;
                PrjUI.UI_SpecialPrj Item = sender as PrjUI.UI_SpecialPrj;

                int Index = SpecialControls.FindIndex(delegate(PrjUI.UI_SpecialPrj SpecialItem) { return SpecialItem == Item; });

                SpecialControls.Remove(Item);

                for (int i = 0; i < SpecialControls.Count; i++)
                {
                    if (Item.Top < SpecialControls[i].Top + SpecialControls[i].Height)
                    {
                        SpecialControls.Insert(i, Item);
                        IsInsert = true;
                        Index = i >= Index ? Index : i;
                        break;
                    }
                }
                if (!IsInsert)
                {
                    SpecialControls.Add(Item);
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


            private delegate void DetSizeChanged();
            /// <summary>
            /// 面板尺寸变化
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void _CtrParent_SizeChanged(object sender, EventArgs e)
            {
                if (_CtrParent.IsHandleCreated)
                {
                    _CtrParent.BeginInvoke(new DetSizeChanged(DelaySizeChanged));
                }
            }
            /// <summary>
            /// 委托延迟刷新
            /// </summary>
            private void DelaySizeChanged()
            {
                System.Threading.Thread.Sleep(1);      //委托延迟1毫秒，让Panel先Create再去响应SizeChanged事件

                if (_CtrParent.VerticalScroll.Visible)
                {
                    this.SetWidth(_CtrParent.Width - 16);
                }
                else
                {
                    this.SetWidth(_CtrParent.Width);
                }
            }

            /// <summary>
            /// 面板关闭
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void SpecialControl_PanelClose(object sender, EventArgs e)
            {
                SpecialControls.Remove((PrjUI.UI_SpecialPrj)sender);
                _CtrParent.Controls.Remove((PrjUI.UI_SpecialPrj)sender);
                ((PrjUI.UI_SpecialPrj)sender).MouseMoveOver -= new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMoveOver(SpecialControl_MouseMoveOver);
                ((PrjUI.UI_SpecialPrj)sender).MouseMoves -= new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMove(SpecialControl_MouseMoves);
                ((PrjUI.UI_SpecialPrj)sender).PanelClose -= new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventPanelClose(SpecialControl_PanelClose);
                ((PrjUI.UI_SpecialPrj)sender).Dispose();
                
                this.Sort();
            }

            #endregion

            #region  ------------公共方法-------------

            public void Clear()
            {
                for (int i = 0; i < SpecialControls.Count; i++)
                {
                    _CtrParent.Controls.Remove(SpecialControls[i]);
                    SpecialControls[i].MouseMoveOver -= new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMoveOver(SpecialControl_MouseMoveOver);
                    SpecialControls[i].MouseMoves -= new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMove(SpecialControl_MouseMoves);
                    SpecialControls[i].PanelClose -= new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventPanelClose(SpecialControl_PanelClose);
                    SpecialControls[i].Dispose();
                }
                SpecialControls.Clear();
            }

            /// <summary>
            /// 添加控件元素
            /// </summary>
            /// <param name="DgnControl"></param>
            public void Add(PrjUI.UI_SpecialPrj SpecialControl)
            {
                SpecialControl.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMoveOver(SpecialControl_MouseMoveOver);
                SpecialControl.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMove(SpecialControl_MouseMoves);
                SpecialControl.PanelClose += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventPanelClose(SpecialControl_PanelClose);
                _CtrParent.Controls.Add(SpecialControl);
                SpecialControls.Add(SpecialControl);

                if (_CtrParent.VerticalScroll.Visible)
                {
                    this.SetWidth(_CtrParent.Width - 16,SpecialControls.Count-1);
                }
                else
                {
                    this.SetWidth(_CtrParent.Width,SpecialControls.Count-1);
                }

                this.Sort(SpecialControls.Count-1);

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

                for (int i = Index; i < SpecialControls.Count; i++)
                {
                    if (i > 0)
                    {
                        intTop = SpecialControls[i - 1].Top + SpecialControls[i - 1].Height + CONST_JIANJU;
                    }
                    else
                    {
                        intTop = CONST_JIANJU;
                    }

                    SpecialControls[i].ID = i + 1;

                    SpecialControls[i].Location = new Point(CONST_LEFT, intTop);
                }


            }

            /// <summary>
            /// 设置控件集宽度
            /// </summary>
            /// <param name="Width"></param>
            public void SetWidth(int Width)
            {
                foreach (PrjUI.UI_SpecialPrj Item in SpecialControls)
                {
                    Item.Width = Width - CONST_LEFT * 2;
                }
            }

            public void SetWidth(int Width, int Index)
            {
                SpecialControls[Index].Width = Width - CONST_LEFT * 2;
            }

            /// <summary>
            /// 方案加载
            /// </summary>
            /// <param name="FaItem"></param>
            public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Specal FaItem)
            {
                PrjUI.UI_SpecialPrj Item;
                _CtrParent.Controls.Clear();
                SpecialControls.Clear();
                for (int i = 0; i < FaItem.Count; i++)
                {
                    Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj();
                    Item.SetItemValue(FaItem.getSpecalPrj(i));
                    Item.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMoveOver(SpecialControl_MouseMoveOver);
                    Item.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventMouseMove(SpecialControl_MouseMoves);
                    Item.PanelClose += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_SpecialPrj.EventPanelClose(SpecialControl_PanelClose);
                    _CtrParent.Controls.Add(Item);
                    SpecialControls.Add(Item);
                }

                this._CtrParent_SizeChanged(_CtrParent, new EventArgs());

                this.Sort();
                
            }

            /// <summary>
            /// 拷贝需要检定的方案
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_Specal Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_Specal Specal = new CLDC_DataCore.Model.Plan.Plan_Specal((int)TaiType, "");           //创建一个新的特殊检定方案

                for (int i = 0; i < SpecialControls.Count; i++)
                {
                    PrjUI.UI_SpecialPrj Item = SpecialControls[i];

                    Specal.Add(Item.GetItem());
                }
                Specal.SetPram((int)TaiType, FaName);
                return Specal;
            }


            #endregion


        }
        #endregion 

    }
}

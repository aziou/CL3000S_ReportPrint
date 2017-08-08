using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_Function :UI_TableBase//UserControl
    {

        private ControlLocation FunctionControlUI;

        #region ------------------构造-----------------
        public UI_Function()
        {
            InitializeComponent();
            this.InitUI();
        }

        public UI_Function(CLDC_Comm.Enum.Cus_TaiType Ttype)
            :base(Ttype,"")
        {
            InitializeComponent();
            this.InitUI();

        }

        public UI_Function(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_Function(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_Function FaItem)
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
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_GN_FOLDERNAME);
            
            PrjUI.PrjFunction.FunctionBase Item;

            FunctionControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csFunctionDic _FunctionItem = new CLDC_DataCore.SystemModel.Item.csFunctionDic();
            _FunctionItem.Load();

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionComm(_FunctionItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.计量功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            FunctionControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.NotParmPrj(_FunctionItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.计时功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            FunctionControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FcShow(_FunctionItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.显示功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            FunctionControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.NotParmPrj(_FunctionItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.费率时段功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            FunctionControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.NotParmPrj(_FunctionItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.脉冲输出功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            FunctionControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionComm(_FunctionItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.最大需量功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            FunctionControlUI.Add(Item);

            //FunctionControlUI.Sort();




            this.SizeChanged += new EventHandler(UI_Dgn_SizeChanged);
        }




        #endregion 

        #region---------------公共方法、函数--------------

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="faname">方案名称</param>
        public void LoadFA(string faname)
        {
            CLDC_DataCore.Model.Plan.Plan_Function _Dgn = new CLDC_DataCore.Model.Plan.Plan_Function((int)base.TaiType, faname);

            this.LoadFA(_Dgn);
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Function FaItem)
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

            this.FunctionControlUI.LoadFA(FaItem);

        }
        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_Function Copy()
        {
            return FunctionControlUI.Copy(base.TaiType,base.FaName);
        }

        #endregion 

        #region-------------------事件-----------------

        private delegate void DetSizeChanged();

        /// <summary>
        /// 窗体尺寸变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Dgn_SizeChanged(object sender, EventArgs e)
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
                FunctionControlUI.SetWidth(Panel_Prjs.Width - 16);
            }
            else
            {
                FunctionControlUI.SetWidth(Panel_Prjs.Width);
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

            private List<PrjUI.PrjFunction.FunctionBase> FunctionControls;

            /// <summary>
            /// 父域面板
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjFunction.FunctionBase> FunctionControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                FunctionControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------事件--------------------------
            /// <summary>
            /// 要检项目排序
            /// </summary>
            /// <param name="Index"></param>
            private void FunctionControl_PrjSort(object sender, int Index)
            {
                PrjUI.PrjFunction.FunctionBase Item = sender as PrjUI.PrjFunction.FunctionBase;

                if (FunctionControls.Contains(Item))
                {
                    FunctionControlsSort.Add(Index, Item);
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
            private void FunctionControl_MouseMoves(object sender, MouseEventArgs e)
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
            private void FunctionControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //如果控件没有移动过
                IsMoved = false;
                bool IsInsert = false;
                PrjUI.PrjFunction.FunctionBase Item = sender as PrjUI.PrjFunction.FunctionBase;

                int Index = FunctionControls.FindIndex(delegate(PrjUI.PrjFunction.FunctionBase FunctionItem) { return FunctionItem == Item; });

                FunctionControls.Remove(Item);

                for (int i = 0; i < FunctionControls.Count; i++)
                {
                    if (Item.Top < FunctionControls[i].Top + FunctionControls[i].Height)
                    {
                        FunctionControls.Insert(i, Item);
                        IsInsert = true;
                        Index=i>=Index?Index:i;
                        break;
                    }
                }
                if (!IsInsert)
                {
                    FunctionControls.Add(Item);
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
            /// <param name="FunctionControl"></param>
            public void Add(PrjUI.PrjFunction.FunctionBase FunctionControl)
            {
                FunctionControl.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionBase.EventMouseMoveOver(FunctionControl_MouseMoveOver);
                FunctionControl.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionBase.EventMouseMove(FunctionControl_MouseMoves);
                FunctionControl.PrjSort += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionBase.EventPrjSort(FunctionControl_PrjSort);
                FunctionControls.Add(FunctionControl);

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

                for (int i = Index; i < FunctionControls.Count; i++)
                {
                    if (i > 0)
                    {
                        intTop = FunctionControls[i - 1].Top + FunctionControls[i - 1].Height + CONST_JIANJU;
                    }
                    else
                    {
                        intTop = CONST_JIANJU;
                    }

                    FunctionControls[i].Location = new Point(CONST_LEFT, intTop);
                    FunctionControls[i].ID = i + 1 ;
                }


            }

            /// <summary>
            /// 设置控件集宽度
            /// </summary>
            /// <param name="Width"></param>
            public void SetWidth(int Width)
            {
                foreach (PrjUI.PrjFunction.FunctionBase Item in FunctionControls)
                {
                    Item.Width = Width - CONST_LEFT * 2;
                }
            }

            /// <summary>
            /// 方案加载
            /// </summary>
            /// <param name="FaItem"></param>
            public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Function FaItem)
            {
                FunctionControlsSort = new SortedDictionary<int, CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction.FunctionBase>();
                for (int i = 0; i < FunctionControls.Count; i++)
                {
                    FunctionControls[i].LoadFA(FaItem);
                }


                foreach(int i in FunctionControlsSort.Keys)
                {
                    FunctionControls.Remove(FunctionControlsSort[i]);
                    FunctionControls.Insert(i, FunctionControlsSort[i]);
                    
                }
                this.Sort();
                FunctionControlsSort = null;
            }

            /// <summary>
            /// 拷贝需要检定的方案
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_Function Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_Function Function = new CLDC_DataCore.Model.Plan.Plan_Function((int)TaiType, "");           //创建一个新的多功能方案

                for (int i = 0; i < FunctionControls.Count; i++)
                {
                    PrjUI.PrjFunction.FunctionBase Item=FunctionControls[i];
                    if (Item.IsCheck)
                    {
                        Function.Add(Item.FunctionID, Item.FunctionName, Item.FunctionPlanPrj.OutPramerter.Jion(), Item.Parm);
                    }
                    
                }
                Function.SetPram((int)TaiType, FaName);
                return Function;
            }


            #endregion


        }
        #endregion 
    }
}

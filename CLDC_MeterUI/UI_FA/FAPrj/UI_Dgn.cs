using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_Dgn :UI_TableBase//UserControl
    {

        private ControlLocation DgnControlUI;

        #region ------------------构造-----------------
        public UI_Dgn()
        {
            InitializeComponent();
            this.InitUI();
        }
        
        public UI_Dgn(CLDC_Comm.Enum.Cus_TaiType Ttype)
            :base(Ttype,"")
        {
            InitializeComponent();
            this.InitUI();

        }

        public UI_Dgn(CLDC_Comm.Enum.Cus_TaiType Ttype,string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_Dgn(CLDC_Comm.Enum.Cus_TaiType Ttype,CLDC_DataCore.Model.Plan.Plan_Dgn FaItem)
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
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME);
            
            PrjUI.PrjDgn.DgnBase Item;

            DgnControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csDgnDic _DgnItem = new CLDC_DataCore.SystemModel.Item.csDgnDic();
            _DgnItem.Load();

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.通信测试).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnDayCheckTime(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.日计时误差).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.费率时段检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.时段投切).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.计度器示值组合误差).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.费率时段示值误差).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.GPS对时).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.闰年判断功能).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.事件记录检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.需量清空).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.电压逐渐变化).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.电压跌落).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.时钟示值误差).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnZdxl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.最大需量Imax).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnZdxl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.最大需量10Ib).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnZdxl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.最大需量01Ib).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnReadDl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.读取电量).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.电量清零).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);            

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnDlMemory(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.电量寄存器检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);


            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.需量寄存器检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);


            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.瞬时寄存器检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.状态寄存器检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.失压寄存器检查).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.校对电量).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.校对需量).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.检查电表运行状态).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.预付费检测).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.电压短时中断).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnChangePassword(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.修改密码).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);
            

            //DgnControlUI.Sort();




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
            CLDC_DataCore.Model.Plan.Plan_Dgn _Dgn = new CLDC_DataCore.Model.Plan.Plan_Dgn((int)base.TaiType, faname);

            this.LoadFA(_Dgn);
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Dgn FaItem)
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

            this.DgnControlUI.LoadFA(FaItem);

        }
        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_Dgn Copy()
        {
            return DgnControlUI.Copy(base.TaiType,base.FaName);
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
                DgnControlUI.SetWidth(Panel_Prjs.Width - 16);
            }
            else
            {
                DgnControlUI.SetWidth(Panel_Prjs.Width);
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

            private List<PrjUI.PrjDgn.DgnBase> DgnControls;

            /// <summary>
            /// 父域面板
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjDgn.DgnBase> DgnControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                DgnControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------事件--------------------------
            /// <summary>
            /// 要检项目排序
            /// </summary>
            /// <param name="Index"></param>
            private void DgnControl_PrjSort(object sender,int Index)
            {
                PrjUI.PrjDgn.DgnBase Item = sender as PrjUI.PrjDgn.DgnBase;

                if (DgnControls.Contains(Item))
                {
                    DgnControlsSort.Add(Index, Item);
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
            private void DgnControl_MouseMoves(object sender, MouseEventArgs e)
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
            private void DgnControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //如果控件没有移动过
                IsMoved = false;
                bool IsInsert = false;
                PrjUI.PrjDgn.DgnBase Item = sender as PrjUI.PrjDgn.DgnBase;

                int Index = DgnControls.FindIndex(delegate(PrjUI.PrjDgn.DgnBase DgnItem) { return DgnItem == Item; });

                DgnControls.Remove(Item);

                for (int i = 0; i < DgnControls.Count; i++)
                {
                    if (Item.Top < DgnControls[i].Top + DgnControls[i].Height)
                    {
                        DgnControls.Insert(i, Item);
                        IsInsert = true;
                        Index=i>=Index?Index:i;
                        break;
                    }
                }
                if (!IsInsert)
                {
                    DgnControls.Add(Item);
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
            public void Add(PrjUI.PrjDgn.DgnBase DgnControl)
            {
                DgnControl.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnBase.EventMouseMoveOver(DgnControl_MouseMoveOver);
                DgnControl.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnBase.EventMouseMove(DgnControl_MouseMoves);
                DgnControl.PrjSort += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnBase.EventPrjSort(DgnControl_PrjSort);
                DgnControls.Add(DgnControl);

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

                for (int i = Index; i < DgnControls.Count; i++)
                {
                    if (i > 0)
                    {
                        intTop = DgnControls[i - 1].Top + DgnControls[i - 1].Height + CONST_JIANJU;
                    }
                    else
                    {
                        intTop = CONST_JIANJU;
                    }

                    DgnControls[i].Location = new Point(CONST_LEFT, intTop);
                    DgnControls[i].ID = i + 1 ;
                }


            }

            /// <summary>
            /// 设置控件集宽度
            /// </summary>
            /// <param name="Width"></param>
            public void SetWidth(int Width)
            {
                foreach (PrjUI.PrjDgn.DgnBase Item in DgnControls)
                {
                    Item.Width = Width - CONST_LEFT * 2;
                }
            }

            /// <summary>
            /// 方案加载
            /// </summary>
            /// <param name="FaItem"></param>
            public void LoadFA(CLDC_DataCore.Model.Plan.Plan_Dgn FaItem)
            {
                DgnControlsSort = new SortedDictionary<int, CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnBase>();
                for (int i = 0; i < DgnControls.Count; i++)
                {
                    DgnControls[i].LoadFA(FaItem);
                }


                foreach(int i in DgnControlsSort.Keys)
                {
                    DgnControls.Remove(DgnControlsSort[i]);
                    DgnControls.Insert(i, DgnControlsSort[i]);
                    
                }
                this.Sort();
                DgnControlsSort = null;
            }

            /// <summary>
            /// 拷贝需要检定的方案
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_Dgn Copy(CLDC_Comm.Enum.Cus_TaiType TaiType,string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_Dgn Dgn = new CLDC_DataCore.Model.Plan.Plan_Dgn((int)TaiType,"");           //创建一个新的多功能方案

                for (int i = 0; i < DgnControls.Count; i++)
                {
                    PrjUI.PrjDgn.DgnBase Item=DgnControls[i];
                    if (Item.IsCheck)
                    {
                        Dgn.Add(Item.DgnID, Item.DgnName,Item.DgnPlanPrj.OutPramerter.Jion(), Item.Parm);
                    }
                    
                }
                Dgn.SetPram((int)TaiType, FaName);
                return Dgn;
            }


            #endregion


        }
        #endregion 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_CostControl :UI_TableBase//UserControl
    {

        private ControlLocation CostControlUI;

        #region ------------------构造-----------------
        public UI_CostControl()
        {
            InitializeComponent();
            this.InitUI();
        }

        public UI_CostControl(CLDC_Comm.Enum.Cus_TaiType Ttype)
            :base(Ttype,"")
        {
            InitializeComponent();
            this.InitUI();

        }

        public UI_CostControl(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_CostControl(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_CostControl FaItem)
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
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_FK_FOLDERNAME);
            
            PrjUI.PrjCostControl.CostControlBase Item;

            CostControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csCostControlDic _CostControlItem = new CLDC_DataCore.SystemModel.Item.csCostControlDic();
            _CostControlItem.Load();

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.身份认证).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.远程控制).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.报警功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.保电功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.保电解除).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.远程保电).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.数据回抄).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.参数设置).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.剩余电量递减准确度).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.电价切换).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);


            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.负荷开关).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.电量清零).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.密钥更新).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.密钥恢复).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlFunction(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.控制功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostStepTariff(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.阶梯电价检测).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostRatesTime(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.费率电价检测).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.远程控制直接合闸).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlInitPurse(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.钱包初始化).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);
            //

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostPresetContentSettings(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.预置内容设置).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.远程模式切换本地模式).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.本地模式切换远程模式).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.用户卡开户).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.透支功能).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);


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
            CLDC_DataCore.Model.Plan.Plan_CostControl _CostControl = new CLDC_DataCore.Model.Plan.Plan_CostControl((int)base.TaiType, faname);

            this.LoadFA(_CostControl);
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_CostControl FaItem)
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

            this.CostControlUI.LoadFA(FaItem);

        }
        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_CostControl Copy()
        {
            return CostControlUI.Copy(base.TaiType,base.FaName);
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
                CostControlUI.SetWidth(Panel_Prjs.Width - 16);
            }
            else
            {
                CostControlUI.SetWidth(Panel_Prjs.Width);
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

            private List<PrjUI.PrjCostControl.CostControlBase> CostControlControls;

            /// <summary>
            /// 父域面板
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjCostControl.CostControlBase> CostControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                CostControlControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------事件--------------------------
            /// <summary>
            /// 要检项目排序
            /// </summary>
            /// <param name="Index"></param>
            private void FunctionControl_PrjSort(object sender, int Index)
            {
                PrjUI.PrjCostControl.CostControlBase Item = sender as PrjUI.PrjCostControl.CostControlBase;

                if (CostControlControls.Contains(Item))
                {
                    CostControlsSort.Add(Index, Item);
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
                PrjUI.PrjCostControl.CostControlBase Item = sender as PrjUI.PrjCostControl.CostControlBase;

                int Index = CostControlControls.FindIndex(delegate(PrjUI.PrjCostControl.CostControlBase FunctionItem) { return FunctionItem == Item; });

                CostControlControls.Remove(Item);

                for (int i = 0; i < CostControlControls.Count; i++)
                {
                    if (Item.Top < CostControlControls[i].Top + CostControlControls[i].Height)
                    {
                        CostControlControls.Insert(i, Item);
                        IsInsert = true;
                        Index=i>=Index?Index:i;
                        break;
                    }
                }
                if (!IsInsert)
                {
                    CostControlControls.Add(Item);
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
            public void Add(PrjUI.PrjCostControl.CostControlBase FunctionControl)
            {
                FunctionControl.MouseMoveOver += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlBase.EventMouseMoveOver(FunctionControl_MouseMoveOver);
                FunctionControl.MouseMoves += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlBase.EventMouseMove(FunctionControl_MouseMoves);
                FunctionControl.PrjSort += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlBase.EventPrjSort(FunctionControl_PrjSort);
                CostControlControls.Add(FunctionControl);

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

                for (int i = Index; i < CostControlControls.Count; i++)
                {
                    if (i > 0)
                    {
                        intTop = CostControlControls[i - 1].Top + CostControlControls[i - 1].Height + CONST_JIANJU;
                    }
                    else
                    {
                        intTop = CONST_JIANJU;
                    }

                    CostControlControls[i].Location = new Point(CONST_LEFT, intTop);
                    CostControlControls[i].ID = i + 1 ;
                }


            }

            /// <summary>
            /// 设置控件集宽度
            /// </summary>
            /// <param name="Width"></param>
            public void SetWidth(int Width)
            {
                foreach (PrjUI.PrjCostControl.CostControlBase Item in CostControlControls)
                {
                    Item.Width = Width - CONST_LEFT * 2;
                }
            }

            /// <summary>
            /// 方案加载
            /// </summary>
            /// <param name="FaItem"></param>
            public void LoadFA(CLDC_DataCore.Model.Plan.Plan_CostControl FaItem)
            {
                CostControlsSort = new SortedDictionary<int, CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlBase>();
                for (int i = 0; i < CostControlControls.Count; i++)
                {
                    CostControlControls[i].LoadFA(FaItem);
                }


                foreach(int i in CostControlsSort.Keys)
                {
                    CostControlControls.Remove(CostControlsSort[i]);
                    CostControlControls.Insert(i, CostControlsSort[i]);
                    
                }
                this.Sort();
                CostControlsSort = null;
            }

            /// <summary>
            /// 拷贝需要检定的方案
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_CostControl Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_CostControl Function = new CLDC_DataCore.Model.Plan.Plan_CostControl((int)TaiType, "");           //创建一个新的多功能方案

                for (int i = 0; i < CostControlControls.Count; i++)
                {
                    PrjUI.PrjCostControl.CostControlBase Item=CostControlControls[i];
                    if (Item.IsCheck)
                    {
                        Function.Add(Item.CostControlID, Item.CostControlName, Item.CostControlPlanPrj.OutPramerter.Jion(), Item.Parm);
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

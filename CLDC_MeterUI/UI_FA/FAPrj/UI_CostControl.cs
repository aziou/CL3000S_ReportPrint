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

        #region ------------------����-----------------
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

        #region -------------------˽�з���������--------------
        /// <summary>
        /// UI��ʼ��
        /// </summary>
        private void InitUI()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_FK_FOLDERNAME);
            
            PrjUI.PrjCostControl.CostControlBase Item;

            CostControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csCostControlDic _CostControlItem = new CLDC_DataCore.SystemModel.Item.csCostControlDic();
            _CostControlItem.Load();

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.�����֤).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.Զ�̿���).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.��������).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.���繦��).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.������).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.Զ�̱���).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.���ݻس�).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.��������).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.ʣ������ݼ�׼ȷ��).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.����л�).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);


            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.���ɿ���).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.��������).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.��Կ����).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.��Կ�ָ�).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlFunction(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.���ƹ���).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostStepTariff(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.���ݵ�ۼ��).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostRatesTime(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.���ʵ�ۼ��).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.Զ�̿���ֱ�Ӻ�բ).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlInitPurse(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.Ǯ����ʼ��).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);
            //

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostPresetContentSettings(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.Ԥ����������).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.Զ��ģʽ�л�����ģʽ).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.����ģʽ�л�Զ��ģʽ).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.�û�������).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.NotParmPrj(_CostControlItem.getCostControlPrj(((int)CLDC_Comm.Enum.Cus_CostControlItem.͸֧����).ToString("000")));
            Item.PanelBackColor = SystemColors.Control;
            Item.CaptionColor = Color.Silver;
            this.Panel_Prjs.Controls.Add(Item);
            CostControlUI.Add(Item);


            this.SizeChanged += new EventHandler(UI_Dgn_SizeChanged);
        }




        #endregion 

        #region---------------��������������--------------

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="faname">��������</param>
        public void LoadFA(string faname)
        {
            CLDC_DataCore.Model.Plan.Plan_CostControl _CostControl = new CLDC_DataCore.Model.Plan.Plan_CostControl((int)base.TaiType, faname);

            this.LoadFA(_CostControl);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_CostControl Copy()
        {
            return CostControlUI.Copy(base.TaiType,base.FaName);
        }

        #endregion 

        #region-------------------�¼�-----------------

        private delegate void DetSizeChanged();

        /// <summary>
        /// ����ߴ�仯
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
            System.Threading.Thread.Sleep(1);      //ί���ӳ�1���룬��Panel��Create��ȥ��ӦSizeChanged�¼�

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

            private List<PrjUI.PrjCostControl.CostControlBase> CostControlControls;

            /// <summary>
            /// �������
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjCostControl.CostControlBase> CostControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                CostControlControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl.CostControlBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------�¼�--------------------------
            /// <summary>
            /// Ҫ����Ŀ����
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
            /// �Ƿ����ƶ�
            /// </summary>
            private bool IsMoved = false;

            /// <summary>
            /// ����϶��¼�
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
            /// �ؼ�����ƶ�����¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void FunctionControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //����ؼ�û���ƶ���
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

            #region  ------------��������-------------
            /// <summary>
            /// ��ӿؼ�Ԫ��
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
            /// ���ÿؼ������
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
            /// ��������
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
            /// ������Ҫ�춨�ķ���
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_CostControl Copy(CLDC_Comm.Enum.Cus_TaiType TaiType, string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_CostControl Function = new CLDC_DataCore.Model.Plan.Plan_CostControl((int)TaiType, "");           //����һ���µĶ๦�ܷ���

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

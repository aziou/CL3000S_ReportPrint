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

        #region ------------------����-----------------
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

        #region -------------------˽�з���������--------------
        /// <summary>
        /// UI��ʼ��
        /// </summary>
        private void InitUI()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME);
            
            PrjUI.PrjDgn.DgnBase Item;

            DgnControlUI = new ControlLocation(Panel_Prjs);

            CLDC_DataCore.SystemModel.Item.csDgnDic _DgnItem = new CLDC_DataCore.SystemModel.Item.csDgnDic();
            _DgnItem.Load();

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.ͨ�Ų���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnDayCheckTime(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�ռ�ʱ���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.����ʱ�μ��).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.ʱ��Ͷ��).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�ƶ���ʾֵ������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnSdtq(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.����ʱ��ʾֵ���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.GPS��ʱ).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�����жϹ���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�¼���¼���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.��ѹ�𽥱仯).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.��ѹ����).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.ʱ��ʾֵ���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnZdxl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�������Imax).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnZdxl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�������10Ib).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnZdxl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�������01Ib).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnReadDl(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.��ȡ����).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.��������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);            

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnDlMemory(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�����Ĵ������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);


            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�����Ĵ������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);


            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.˲ʱ�Ĵ������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.״̬�Ĵ������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.ʧѹ�Ĵ������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.У�Ե���).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.У������).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.���������״̬).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.Ԥ���Ѽ��).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.NotParmPrj(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.��ѹ��ʱ�ж�).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);

            Item = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnChangePassword(_DgnItem.getDgnPrj(((int)CLDC_Comm.Enum.Cus_DgnItem.�޸�����).ToString("000")));
            Item.PanelBackColor = Color.LightBlue;
            Item.CaptionColor = Color.White;
            Item.CaptionColorTwo = Color.LightBlue;
            this.Panel_Prjs.Controls.Add(Item);

            DgnControlUI.Add(Item);
            

            //DgnControlUI.Sort();




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
            CLDC_DataCore.Model.Plan.Plan_Dgn _Dgn = new CLDC_DataCore.Model.Plan.Plan_Dgn((int)base.TaiType, faname);

            this.LoadFA(_Dgn);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_Dgn Copy()
        {
            return DgnControlUI.Copy(base.TaiType,base.FaName);
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
                DgnControlUI.SetWidth(Panel_Prjs.Width - 16);
            }
            else
            {
                DgnControlUI.SetWidth(Panel_Prjs.Width);
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

            private List<PrjUI.PrjDgn.DgnBase> DgnControls;

            /// <summary>
            /// �������
            /// </summary>
            private Panel _CtrParent = null;

            private SortedDictionary<int, PrjUI.PrjDgn.DgnBase> DgnControlsSort;


            public ControlLocation(Panel CtrParent)
            {
                DgnControls=new List<CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn.DgnBase>();
                _CtrParent = CtrParent;
            }

            #region ------------------�¼�--------------------------
            /// <summary>
            /// Ҫ����Ŀ����
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
            /// �Ƿ����ƶ�
            /// </summary>
            private bool IsMoved = false;

            /// <summary>
            /// ����϶��¼�
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
            /// �ؼ�����ƶ�����¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void DgnControl_MouseMoveOver(object sender, EventArgs e)
            {
                if (!IsMoved) return;       //����ؼ�û���ƶ���
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

            #region  ------------��������-------------
            /// <summary>
            /// ��ӿؼ�Ԫ��
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
            /// ���ÿؼ������
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
            /// ��������
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
            /// ������Ҫ�춨�ķ���
            /// </summary>
            /// <param name="TaiType"></param>
            /// <param name="FaName"></param>
            /// <returns></returns>
            public CLDC_DataCore.Model.Plan.Plan_Dgn Copy(CLDC_Comm.Enum.Cus_TaiType TaiType,string FaName)
            {
                CLDC_DataCore.Model.Plan.Plan_Dgn Dgn = new CLDC_DataCore.Model.Plan.Plan_Dgn((int)TaiType,"");           //����һ���µĶ๦�ܷ���

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

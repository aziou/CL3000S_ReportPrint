using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;


namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class DgnBase :UserControl
    {
        #region---------ί��-----------
        public delegate void EventMouseMoveOver(object sender, EventArgs e);

        public delegate void EventMouseMove(object sender, MouseEventArgs e);

        public delegate void EventPrjSort(object sender,int Index);

        #endregion

        #region--------����-------------
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public event EventPrjSort PrjSort = null;
        /// <summary>
        /// �����ק�ؼ��ƶ��¼�
        /// </summary>
        public event EventMouseMove MouseMoves = null;

        /// <summary>
        /// �����ק�ƶ�����
        /// </summary>
        public event  EventMouseMoveOver MouseMoveOver = null;

        /// <summary>
        /// �๦����Ŀ��Ϣ�ṹ��
        /// </summary>
        private CLDC_DataCore.Struct.StDgnConfig _DgnItem=new CLDC_DataCore.Struct.StDgnConfig();


        private CLDC_Comm.ExtendedPanel.ExtendedPanel _Panel = null;


        /// <summary>
        /// �춨����
        /// </summary>
        private string _Parm="";

        /// <summary>
        /// �Ƿ�Ҫ��
        /// </summary>
        protected  bool _IsCheck;

        #endregion 

        #region------------------����----------------

        public DgnBase()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="DgnItem"></param>
        public DgnBase(CLDC_DataCore.Struct.StDgnConfig DgnItem)
        {
            InitializeComponent();
            _DgnItem = DgnItem;

        }

        #endregion 

        #region -------------------�¼�---------------------
        /// <summary>
        /// �����ק�ƶ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Panel_EvtMouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseMoves != null)
            {
                this.MouseMoves(this, e);
            }
        }

        /// <summary>
        /// ����ƶ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Panel_EvtMoveOver(object sender, EventArgs e)
        {
            if (MouseMoveOver != null)
            {
                this.MouseMoveOver(this, e);
            }
        }
        /// <summary>
        /// ���ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Panel_CheckedChanged(object sender, EventArgs e)
        {
            _IsCheck = _Panel.Checked;
        }

        #endregion

        #region -------------����-------------------
        /// <summary>
        /// 
        /// </summary>
        protected CLDC_Comm.ExtendedPanel.ExtendedPanel SetPanel
        {
            set
            {
                _Panel = value;
                _Panel.CtrParent = this;
                _Panel.Moveable = true;
                //_Panel.CaptionColorOne = SystemColors.ActiveCaptionText;
                //_Panel.CaptionColorTwo = SystemColors.Control;
                //_Panel.BackColor = SystemColors.ActiveCaptionText;
                _Panel.ShowOrderID = true;
                _Panel.CheckedChanged += new CLDC_Comm.ExtendedPanel.EventCheckedChanged(_Panel_CheckedChanged);
                _Panel.EvtMoveOver += new CLDC_Comm.ExtendedPanel.EventMoveOver(_Panel_EvtMoveOver);
                _Panel.EvtMouseMove += new CLDC_Comm.ExtendedPanel.EventMouseMove(_Panel_EvtMouseMove);
                _Panel.CaptionText = _DgnItem.DgnPrjName;
            }
        }
        /// <summary>
        /// ����������ɫ
        /// </summary>
        public virtual Color PanelBackColor
        {
            set { }
        }

        public virtual Color CaptionColor
        {
            set { }
        }

        public virtual Color CaptionColorTwo
        {
            set { }
        }

        public int ID
        {
            get { return _Panel.ID; }
            set { _Panel.ID = value; }
        }

        /// <summary>
        /// ���ö๦����Ŀ
        /// </summary>
        public CLDC_DataCore.Struct.StDgnConfig DgnItem
        {
            set
            {
                _DgnItem = value;
            }
        }

        /// <summary>
        /// ��ȡ�๦����ĿID
        /// </summary>
        public string DgnID
        {
            get
            {
                return _DgnItem.DgnPrjID;
            }
        }


        /// <summary>
        /// ��ȡ�๦����Ŀ����
        /// </summary>
        public string DgnName
        {
            get
            {
                return _DgnItem.DgnPrjName;
            }
        }

        /// <summary>
        /// ���û��ȡ�๦����Ŀ����
        /// </summary>
        public virtual string Parm
        {
            get
            {
                return _Parm;
            }
            set
            {
                _Parm = value;
            }
        }

        /// <summary>
        /// ��ȡ�๦����Ŀ
        /// </summary>
        public virtual CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                CLDC_DataCore.Struct.StPlan_Dgn Item = new CLDC_DataCore.Struct.StPlan_Dgn();

                Item.DgnPrjID = this.DgnID;
                Item.DgnPrjName = this.DgnName;
                Item.OutPramerter = this._DgnItem.OutPramerter;
                Item.PrjParm = this.Parm;

                return Item;
            }
        
        }

        /// <summary>
        /// ��ȡ�Ƿ�Ҫ��
        /// </summary>
        public bool IsCheck
        {
            get
            {
                return _IsCheck;
            }
        }

        #endregion 

        #region-------------------��������������----------------
        /// <summary>
        /// �����Ƿ�ѡ��
        /// </summary>
        /// <param name="IsCheck"></param>
        public virtual void SetCheck(bool IsCheck)
        {
            _IsCheck = IsCheck;
            if (_Panel != null)
            {
                _Panel.Checked = _IsCheck;
            }
        }

        /// <summary>
        /// ���ط���
        /// </summary>
        /// <param name="FaItem"></param>
        public virtual void LoadFA(CLDC_DataCore.Model.Plan.Plan_Dgn FaItem)
        {
            for (int i = 0; i < FaItem.Count; i++)
            {

                if (this._DgnItem.DgnPrjID == FaItem.getDgnPrj(i).DgnPrjID)
                {
                    this.SetCheck(true);
                    this._Parm = FaItem.getDgnPrj(i).PrjParm;
                    if (this.PrjSort != null)
                    {
                        PrjSort(this,i);
                    }
                    return;
                }
            }
        }
        #endregion
    }
}

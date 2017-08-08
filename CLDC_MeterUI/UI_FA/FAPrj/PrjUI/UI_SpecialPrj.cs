using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    public partial class UI_SpecialPrj : UserControl
    {
        public delegate void EventMouseMoveOver(object sender, EventArgs e);

        public delegate void EventMouseMove(object sender, MouseEventArgs e);

        public delegate void EventPanelClose(object sender, EventArgs e);

        /// <summary>
        /// ���ر�
        /// </summary>
        public event EventPanelClose PanelClose = null;

        /// <summary>
        /// �����ק�ؼ��ƶ��¼�
        /// </summary>
        public event EventMouseMove MouseMoves = null;

        /// <summary>
        /// �����ק�ƶ�����
        /// </summary>
        public event EventMouseMoveOver MouseMoveOver = null;


        #region------------------���캯��----------------
        /// <summary>
        /// ���캯��
        /// </summary>
        public UI_SpecialPrj()
        {
            InitializeComponent();
            this.Panel_Item.ShowCheckBox = false;
            this.Panel_Item.CtrParent = this;
            this.Panel_Item.Moveable = true;
            this.Panel_Item.ShowOrderID = true;
            this.Panel_Item.CaptionText = "";
            this.Panel_Item.PanelClose += new CLDC_Comm.ExtendedPanel.EventPanelClose(Panel_Item_PanelClose);

            this.Panel_Item.EvtMouseMove += new CLDC_Comm.ExtendedPanel.EventMouseMove(Panel_Item_EvtMouseMove);

            this.Panel_Item.EvtMoveOver += new CLDC_Comm.ExtendedPanel.EventMoveOver(Panel_Item_EvtMoveOver);

            this.Txt_PrjName.TextChanged += new EventHandler(Txt_PrjName_TextChanged);
            this.Txt_Ua.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Ub.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Uc.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Ia.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Ib.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Ic.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Pl.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Wcqs.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Txt_Wccs.KeyDown += new KeyEventHandler(OnKeyDown);

            this.Txt_PrjName.Text = "������춨��Ŀ";

            this.Cmb_Fx.SelectedIndex = 0;

            this.Cmb_Xx.SelectedIndex = 0;

            #region --------------��ȡ������������---------------------            

            List<string> GlysItems = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();

            Cmb_Glys.Items.Clear();

            for (int i = 0; i < GlysItems.Count; i++)
            {
                Cmb_Glys.Items.Add(GlysItems[i]);
            }

            Cmb_Glys.SelectedIndex = 0;

            #endregion

            #region ---------------��ȡг����������-----------------
            CLDC_DataCore.SystemModel.Item.csXieBo XieBoMode = new CLDC_DataCore.SystemModel.Item.csXieBo();

            Cmb_XieBo.Items.Clear();

            Cmb_XieBo.Items.Add("����г��");

            for (int i = 0; i < XieBoMode.FaNameList.Count; i++)
            {
                Cmb_XieBo.Items.Add(XieBoMode.FaNameList[i]);
            }

            Cmb_XieBo.SelectedIndex = 0;

            #endregion
        }


        #endregion


        #region --------------�¼�------------------
        /// <summary>
        /// ����ƶ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Item_EvtMoveOver(object sender, EventArgs e)
        {
            if (MouseMoveOver != null)
            {
                this.MouseMoveOver(this, e);
            }
        }
        /// <summary>
        /// ����ƶ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Item_EvtMouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseMoves != null)
            {
                this.MouseMoves(this, e);
            }
        }


        /// <summary>
        /// �ı������갴���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Enter || e.KeyData == Keys.Tab || e.KeyData==Keys.Decimal 
                                       || e.KeyData==Keys.OemPeriod || e.KeyData==Keys.OemMinus || e.KeyData==Keys.Subtract) return;
            if (!CLDC_DataCore.Function.Number.IsNumeric(Convert.ToChar(e.KeyValue).ToString()))
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// ����ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_PrjName_TextChanged(object sender, EventArgs e)
        {
            this.Panel_Item.CaptionText = Txt_PrjName.Text;
        }
        /// <summary>
        /// ���ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Item_PanelClose(object sender, EventArgs e)
        {
            if (PanelClose != null)
            {
                this.PanelClose(this, e);
            }
        }
        #endregion


        #region--------------------��������������----------------------

        public int ID
        {
            get { return Panel_Item.ID; }
            set { Panel_Item.ID = value; }
        }

        /// <summary>
        /// ����UI����ʾ����Ŀ����
        /// </summary>
        /// <param name="Item">���춨������Ŀ</param>
        public void SetItemValue(StPlan_SpecalCheck Item)
        {
            Txt_PrjName.Text = Item.PrjName;
            Cmb_Fx.SelectedIndex = (int)Item.PowerFangXiang - 1;    //���ʷ���
            Cmb_Glys.Text = Item.PowerYinSu;            //��������
            Txt_Ua.Text = Item.xUa.ToString();          //A���ѹ����
            Txt_Ub.Text = Item.xUb.ToString();          //B���ѹ����
            Txt_Uc.Text = Item.xUc.ToString();          //C���ѹ����
            Txt_Ia.Text = Item.xIa.ToString();          //A���������
            Txt_Ib.Text = Item.xIb.ToString();          //B���������
            Txt_Ic.Text = Item.xIc.ToString();          //C���������
            Txt_Pl.Text = Item.PingLv.ToString();
            Cmb_Xx.SelectedIndex = Item.XiangXu == 0 ? 0 : 1;       //����
            if (Item.XieBo == 0)                //�Ƿ��г��
            {
                Cmb_XieBo.SelectedIndex = 0;
            }
            else
            {
                Cmb_XieBo.Text = Item.XieBoFa;
            }
            Txt_Wccs.Text = Item.WcCheckNumic.ToString();           //������
            Txt_Wcqs.Text = Item.LapCount.ToString();               //���Ȧ��
            Txt_WcUp.Text = Item.WuChaXian_Shang.ToString();        //�������
            Txt_WcDown.Text = Item.WuChaXian_Xia.ToString();        //�������    
        }

        /// <summary>
        /// ��ȡ����춨��Ŀ����
        /// </summary>
        /// <returns>���춨������Ŀ</returns>
        public StPlan_SpecalCheck GetItem()
        {
            StPlan_SpecalCheck Item = new StPlan_SpecalCheck();
            if (Txt_PrjName.Text == string.Empty)          //��Ŀ����
            {
                Item.PrjName = "������춨��Ŀ";
            }
            else
            {
                Item.PrjName = Txt_PrjName.Text;
            }
            Item.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)Cmb_Fx.SelectedIndex + 1;       //���ʷ���
            Item.PowerYinSu = Cmb_Glys.Text;                                                    //��������
            Item.xUa =Txt_Ua.Text == string.Empty ?  0 :  float.Parse(Txt_Ua.Text);   //A���ѹ����
            Item.xUb =Txt_Ub.Text == string.Empty ?  0 : float.Parse(Txt_Ub.Text);   //B���ѹ����
            Item.xUc =Txt_Ub.Text == string.Empty ?  0 :  float.Parse(Txt_Uc.Text);   //C���ѹ����
            Item.xIa =Txt_Ia.Text == string.Empty ?  0 :  float.Parse(Txt_Ia.Text);   //A���������
            Item.xIb =Txt_Ib.Text == string.Empty ?  0 :  float.Parse(Txt_Ib.Text);   //B���������
            Item.xIc = Txt_Ic.Text == string.Empty ? 0 : float.Parse(Txt_Ic.Text);   //C���������

            Item.PingLv =Txt_Pl.Text == string.Empty ?  50 :  float.Parse(Txt_Pl.Text);    //Ƶ��
            Item.XiangXu = Cmb_Xx.SelectedIndex;            //����
            if (Cmb_XieBo.SelectedIndex == 0)               //г��
            {
                Item.XieBo = 0;
            }
            else
            {
                Item.XieBo = 1;
            }
            if (Item.XieBo == 1)
            {
                Item.XieBoFa = Cmb_XieBo.Text;              //г������
            }
            Item.WcCheckNumic =Txt_Wccs.Text == string.Empty ?  2 :  int.Parse(Txt_Wccs.Text);       //������
            Item.LapCount = Txt_Wcqs.Text == string.Empty ? 1 : int.Parse(Txt_Wcqs.Text);                //���Ȧ��
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_WcUp.Text.Replace("+", "")))                    //�������
            {
                Txt_WcUp.Text = "1";
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_WcDown.Text.Replace("+", "")))                  //�������
            {
                Txt_WcDown.Text = "-1";
            }
            Item.WuChaXian_Shang = float.Parse(Txt_WcUp.Text.Replace("+", ""));                     
            Item.WuChaXian_Xia = float.Parse(Txt_WcDown.Text.Replace("+", ""));

            return Item;
        }

        #endregion
    }
}

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
        /// 面板关闭
        /// </summary>
        public event EventPanelClose PanelClose = null;

        /// <summary>
        /// 鼠标拖拽控件移动事件
        /// </summary>
        public event EventMouseMove MouseMoves = null;

        /// <summary>
        /// 鼠标拖拽移动结束
        /// </summary>
        public event EventMouseMoveOver MouseMoveOver = null;


        #region------------------构造函数----------------
        /// <summary>
        /// 构造函数
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

            this.Txt_PrjName.Text = "新特殊检定项目";

            this.Cmb_Fx.SelectedIndex = 0;

            this.Cmb_Xx.SelectedIndex = 0;

            #region --------------获取功率因数集合---------------------            

            List<string> GlysItems = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();

            Cmb_Glys.Items.Clear();

            for (int i = 0; i < GlysItems.Count; i++)
            {
                Cmb_Glys.Items.Add(GlysItems[i]);
            }

            Cmb_Glys.SelectedIndex = 0;

            #endregion

            #region ---------------获取谐波方案集合-----------------
            CLDC_DataCore.SystemModel.Item.csXieBo XieBoMode = new CLDC_DataCore.SystemModel.Item.csXieBo();

            Cmb_XieBo.Items.Clear();

            Cmb_XieBo.Items.Add("不加谐波");

            for (int i = 0; i < XieBoMode.FaNameList.Count; i++)
            {
                Cmb_XieBo.Items.Add(XieBoMode.FaNameList[i]);
            }

            Cmb_XieBo.SelectedIndex = 0;

            #endregion
        }


        #endregion


        #region --------------事件------------------
        /// <summary>
        /// 面板移动结束
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
        /// 面板移动
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
        /// 文本框的鼠标按下事件
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
        /// 标题改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_PrjName_TextChanged(object sender, EventArgs e)
        {
            this.Panel_Item.CaptionText = Txt_PrjName.Text;
        }
        /// <summary>
        /// 面板关闭
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


        #region--------------------公共函数、方法----------------------

        public int ID
        {
            get { return Panel_Item.ID; }
            set { Panel_Item.ID = value; }
        }

        /// <summary>
        /// 设置UI上显示的项目参数
        /// </summary>
        /// <param name="Item">误差检定方案项目</param>
        public void SetItemValue(StPlan_SpecalCheck Item)
        {
            Txt_PrjName.Text = Item.PrjName;
            Cmb_Fx.SelectedIndex = (int)Item.PowerFangXiang - 1;    //功率方向
            Cmb_Glys.Text = Item.PowerYinSu;            //功率因素
            Txt_Ua.Text = Item.xUa.ToString();          //A相电压倍数
            Txt_Ub.Text = Item.xUb.ToString();          //B相电压倍数
            Txt_Uc.Text = Item.xUc.ToString();          //C相电压倍数
            Txt_Ia.Text = Item.xIa.ToString();          //A相电流倍数
            Txt_Ib.Text = Item.xIb.ToString();          //B相电流倍数
            Txt_Ic.Text = Item.xIc.ToString();          //C相电流倍数
            Txt_Pl.Text = Item.PingLv.ToString();
            Cmb_Xx.SelectedIndex = Item.XiangXu == 0 ? 0 : 1;       //相序
            if (Item.XieBo == 0)                //是否加谐波
            {
                Cmb_XieBo.SelectedIndex = 0;
            }
            else
            {
                Cmb_XieBo.Text = Item.XieBoFa;
            }
            Txt_Wccs.Text = Item.WcCheckNumic.ToString();           //误差次数
            Txt_Wcqs.Text = Item.LapCount.ToString();               //误差圈数
            Txt_WcUp.Text = Item.WuChaXian_Shang.ToString();        //误差上限
            Txt_WcDown.Text = Item.WuChaXian_Xia.ToString();        //误差下限    
        }

        /// <summary>
        /// 获取特殊检定项目对象
        /// </summary>
        /// <returns>误差检定方案项目</returns>
        public StPlan_SpecalCheck GetItem()
        {
            StPlan_SpecalCheck Item = new StPlan_SpecalCheck();
            if (Txt_PrjName.Text == string.Empty)          //项目名称
            {
                Item.PrjName = "新特殊检定项目";
            }
            else
            {
                Item.PrjName = Txt_PrjName.Text;
            }
            Item.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)Cmb_Fx.SelectedIndex + 1;       //功率方向
            Item.PowerYinSu = Cmb_Glys.Text;                                                    //功率因数
            Item.xUa =Txt_Ua.Text == string.Empty ?  0 :  float.Parse(Txt_Ua.Text);   //A相电压倍数
            Item.xUb =Txt_Ub.Text == string.Empty ?  0 : float.Parse(Txt_Ub.Text);   //B相电压倍数
            Item.xUc =Txt_Ub.Text == string.Empty ?  0 :  float.Parse(Txt_Uc.Text);   //C相电压倍数
            Item.xIa =Txt_Ia.Text == string.Empty ?  0 :  float.Parse(Txt_Ia.Text);   //A相电流倍数
            Item.xIb =Txt_Ib.Text == string.Empty ?  0 :  float.Parse(Txt_Ib.Text);   //B相电流倍数
            Item.xIc = Txt_Ic.Text == string.Empty ? 0 : float.Parse(Txt_Ic.Text);   //C相电流倍数

            Item.PingLv =Txt_Pl.Text == string.Empty ?  50 :  float.Parse(Txt_Pl.Text);    //频率
            Item.XiangXu = Cmb_Xx.SelectedIndex;            //相序
            if (Cmb_XieBo.SelectedIndex == 0)               //谐波
            {
                Item.XieBo = 0;
            }
            else
            {
                Item.XieBo = 1;
            }
            if (Item.XieBo == 1)
            {
                Item.XieBoFa = Cmb_XieBo.Text;              //谐波方案
            }
            Item.WcCheckNumic =Txt_Wccs.Text == string.Empty ?  2 :  int.Parse(Txt_Wccs.Text);       //误差次数
            Item.LapCount = Txt_Wcqs.Text == string.Empty ? 1 : int.Parse(Txt_Wcqs.Text);                //误差圈数
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_WcUp.Text.Replace("+", "")))                    //误差上限
            {
                Txt_WcUp.Text = "1";
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_WcDown.Text.Replace("+", "")))                  //误差下限
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

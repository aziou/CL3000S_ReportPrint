using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
//using ClInterface;

namespace CLDC_MeterUI.UI_Detection_New
{    


    /// <summary>
    /// 检定界面功能部分子UI的基类
    /// </summary>
    public partial class Base : UserControl
    {
        
        

        #region ====== TaiType (get) ======
        
        //台体类型
        private int _TaiType;
        /// <summary>
        /// 台体类型
        /// </summary>
        protected int TaiType
        {
            get
            {
                return _TaiType;
            }
        }

        #endregion

        #region ====== TaiId (get) ======
        private int _TaiId;
        public int TaiId
        {
            get
            {
                return _TaiId;
            }
        }
        #endregion

        #region ====== MeterGroup (get/set) ======
        protected CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup;
        #endregion

        #region ====== ParentMain (get/set) ======
        protected  Main ParentMain;
        #endregion


        /// <summary>
        /// 指示加载是否完成（第一次加载完成以后才响应RefreshData）
        /// </summary>
        protected bool LoadComplated = false;


        protected Cursor CurHand;

        public Base()
        {
            InitializeComponent();
        }

        public Base(
            Main parent
            , CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup
            , int taiType
            , int taiId)
        {
            InitializeComponent();
            CurHand= new Cursor(Application.StartupPath + @"\Pic\hand.ico");
            ParentMain  = parent; 
            MeterGroup  = meterGroup;
            _TaiType    = taiType;
            _TaiId      = taiId;

        }

        public virtual void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            MeterGroup = meterGroup;
            _TaiType = taiType;
            _TaiId = taiId;
        }

        public virtual void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            MeterGroup = meterGroup;
            _TaiType = taiType;
            _TaiId = taiId;
        }

        protected void InitComplatedButton()
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Point BtnPos = new Point();
            BtnPos.X = this.Width - Btn_DoComplated.Width - 20;
            BtnPos.Y = 10;//this.Height - Btn_DoComplated.Height - 20;
            Btn_DoComplated.Location = BtnPos;            
        }

        /// <summary>
        /// 设置检定消息
        /// </summary>
        /// <param name="MsgString"></param>
        public virtual void SetCheckMessage(string MsgString)
        { 
            
        }


    }
}

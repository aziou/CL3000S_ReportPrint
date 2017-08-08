using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_QiDong :UI_TableBase  // UserControl
    {

        #region ------------------构造-------------------------

        /// <summary>
        /// 构造
        /// </summary>
        public UI_QiDong()
        {
            InitializeComponent();
            this.InitUI();
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        public UI_QiDong(CLDC_Comm.Enum.Cus_TaiType Ttype)
                :base(Ttype,"")
        {
            InitializeComponent();
            this.InitUI();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        /// <param name="faname">方案名称</param>
        public UI_QiDong(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }

        public UI_QiDong(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_QiDong FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);

        
        }

        #endregion 

        #region ---------------事件-------------

        /// <summary>
        /// UI控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rb_Custom_CheckedChanged(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name.ToLower())
            { 
                case "rb_custom_pz":        //有功
                    Txt_Pz.Enabled = ((RadioButton)sender).Checked?true:false;
                    break;
                case "rb_customt_pz":       //有功
                    Txt_TPz.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
                case "rb_custom_pf":        //反向有功
                    Txt_Pf.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
                case "rb_customt_pf":       //反向有功
                    Txt_TPf.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
                case "rb_custom_qz":        //正向无功
                    Txt_Qz.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
                case "rb_customt_qz":       //正向无功
                    Txt_TQz.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
                case "rb_custom_qf":        //反向无功
                    Txt_Qf.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
                case "rb_customt_qf":       //反向无功
                    Txt_TQf.Enabled = ((RadioButton)sender).Checked ? true : false;
                    break;
            }
        }



        /// <summary>
        /// 正向有功选中或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Pz_CheckedChanged(object sender, EventArgs e)
        {
            TabLay_Pz.Enabled = Chk_Pz.CheckState == CheckState.Checked ? true : false; 
        }
        /// <summary>
        /// 反向有功选中或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Pf_CheckedChanged(object sender, EventArgs e)
        {
            if (!(Rb_Auto_Pf.Checked || Rb_Custom_Pf.Checked))
                Rb_Auto_Pf.Checked = true;
            if (!(Rb_AutoT_Pf.Checked || Rb_CustomT_Pf.Checked))
                Rb_AutoT_Pf.Checked = true;
            TabLay_Pf.Enabled = Chk_Pf.CheckState == CheckState.Checked ? true : false;
        }
        /// <summary>
        /// 正向无功选中或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Qz_CheckedChanged(object sender, EventArgs e)
        {
            if (!(Rb_Auto_Qz.Checked || Rb_Custom_Qz.Checked))
                Rb_Auto_Qz.Checked = true;
            if (!(Rb_AutoT_Qz.Checked || Rb_CustomT_Qz.Checked))
                Rb_AutoT_Qz.Checked = true;
 
            TabLay_Qz.Enabled = Chk_Qz.CheckState == CheckState.Checked ? true : false;
        }
        /// <summary>
        /// 反向有功选中或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Qf_CheckedChanged(object sender, EventArgs e)
        {
            if (!(Rb_Auto_Qf.Checked || Rb_Custom_Qf.Checked))
                Rb_Auto_Qf.Checked = true;
            if (!(Rb_AutoT_Qf.Checked || Rb_CustomT_Qf.Checked))
                Rb_AutoT_Qf.Checked = true;
            TabLay_Qf.Enabled = Chk_Qf.CheckState == CheckState.Checked ? true : false;
        }

        /// <summary>
        /// 项目清理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            Chk_Pf.CheckState = CheckState.Unchecked;
            Chk_Pz.CheckState = CheckState.Unchecked;
            Chk_Qz.CheckState = CheckState.Unchecked;
            Chk_Qf.CheckState = CheckState.Unchecked;

            Chk_Default_Pz.CheckState = CheckState.Unchecked;
            Chk_Default_Pf.CheckState = CheckState.Unchecked;
            Chk_Default_Qz.CheckState = CheckState.Unchecked;
            Chk_Default_Qf.CheckState = CheckState.Unchecked;

            Rb_Auto_Pz.Checked = false;
            Rb_AutoT_Pz.Checked = false;
            Rb_Custom_Pz.Checked = false;
            Rb_CustomT_Pz.Checked = false;
            Txt_Pz.Text = "";
            Txt_TPz.Text = "";
            Rb_Auto_Pf.Checked = false;
            Rb_AutoT_Pf.Checked = false;
            Rb_Custom_Pf.Checked = false;
            Rb_CustomT_Pf.Checked = false;
            Txt_Pf.Text = "";
            Txt_TPf.Text = "";
            Rb_Auto_Qz.Checked = false;
            Rb_AutoT_Qz.Checked = false;
            Rb_Custom_Qz.Checked = false;
            Rb_CustomT_Qz.Checked = false;
            Txt_Qz.Text = "";
            Txt_TQz.Text = "";
            Rb_Auto_Qf.Checked = false;
            Rb_AutoT_Qf.Checked = false;
            Rb_Custom_Qf.Checked = false;
            Rb_CustomT_Qf.Checked = false;
            Txt_Qf.Text = "";
            Txt_TQf.Text = "";
        }

        /// <summary>
        /// 方案选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;

            this.FAName = Cmb_Fa.Text;
        }

        #endregion

        #region -------------------私有方法、函数--------------
        /// <summary>
        /// UI初始化
        /// </summary>
        private void InitUI()
        {
            Rb_Custom_Pz.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_CustomT_Pz.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_Custom_Pf.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_CustomT_Pf.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_Custom_Qz.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_CustomT_Qz.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_Custom_Qf.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);
            Rb_CustomT_Qf.CheckedChanged += new EventHandler(Rb_Custom_CheckedChanged);

            ///只有管理员可以默认合格
            if (CLDC_DataCore.Const.GlobalUnit.User_Jyy.Level == 0)
            {
                Chk_Default_Pz.Enabled = true;
                Chk_Default_Pf.Enabled = true;
                Chk_Default_Qz.Enabled = true;
                Chk_Default_Qf.Enabled = true;
            }
            else
            {
                Chk_Default_Pz.Enabled = false;
                Chk_Default_Pf.Enabled = false;
                Chk_Default_Qz.Enabled = false;
                Chk_Default_Qf.Enabled = false;
            }



            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME);

        }


        #endregion 

        #region ----------------公共方法、函数-----------------------

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FAName">方案名称</param>
        public  void LoadFA(string FAName)
        {

            CLDC_DataCore.Model.Plan.Plan_QiDong _QiDong = new CLDC_DataCore.Model.Plan.Plan_QiDong((int)base.TaiType, FAName);         //打开一个方案

            this.LoadFA(_QiDong);
        }

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案对象</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_QiDong FaItem)
        { 
            this.Cmd_Clear_Click(this, new EventArgs());

            base.FaName = FaItem.Name;

            try
            {
                Cmb_Fa.Text = FaItem.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }
            for (int i = 0; i < FaItem.Count; i++)
            {
                StPlan_QiDong _Obj = FaItem.getQiDongPrj(i);     //取出一个方案项目

                #region --------------填充数据---------------------------------
                switch (_Obj.PowerFangXiang)
                { 
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功:
                        Chk_Pz.CheckState = CheckState.Checked;
                        Chk_Default_Pz.CheckState = _Obj.DefaultValue == 0 ? CheckState.Unchecked : CheckState.Checked;
                        if (_Obj.FloatxIb == 0)
                        {
                            Rb_Auto_Pz.Checked = true;
                        }
                        else
                        {
                            Rb_Custom_Pz.Checked = true;
                            Txt_Pz.Text = _Obj.FloatxIb.ToString();
                        }
                        if (_Obj.xTime == 0)
                        {
                            Rb_AutoT_Pz.Checked = true;
                        }
                        else
                        {
                            Rb_CustomT_Pz.Checked = true;
                            Txt_TPz.Text = _Obj.xTime.ToString();
                        }
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功:
                        Chk_Pf.CheckState = CheckState.Checked;
                        Chk_Default_Pf.CheckState = _Obj.DefaultValue == 0 ? CheckState.Unchecked : CheckState.Checked;
                        if (_Obj.FloatxIb == 0)
                        {
                            Rb_Auto_Pf.Checked = true;
                        }
                        else
                        {
                            Rb_Custom_Pf.Checked = true;
                            Txt_Pf.Text = _Obj.FloatxIb.ToString();
                        }
                        if (_Obj.xTime == 0)
                        {
                            Rb_AutoT_Pf.Checked = true;
                        }
                        else
                        {
                            Rb_CustomT_Pf.Checked = true;
                            Txt_TPf.Text = _Obj.xTime.ToString();
                        }
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功:
                        Chk_Qz.CheckState = CheckState.Checked;
                        Chk_Default_Qz.CheckState = _Obj.DefaultValue == 0 ? CheckState.Unchecked : CheckState.Checked;
                        if (_Obj.FloatxIb == 0)
                        {
                            Rb_Auto_Qz.Checked = true;
                        }
                        else
                        {
                            Rb_Custom_Qz.Checked = true;
                            Txt_Qz.Text = _Obj.FloatxIb.ToString();
                        }
                        if (_Obj.xTime == 0)
                        {
                            Rb_AutoT_Qz.Checked = true;
                        }
                        else
                        {
                            Rb_CustomT_Qz.Checked = true;
                            Txt_TQz.Text = _Obj.xTime.ToString();
                        }
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功:
                        Chk_Qf.CheckState = CheckState.Checked;
                        Chk_Default_Qf.CheckState = _Obj.DefaultValue == 0 ? CheckState.Unchecked : CheckState.Checked;
                        if (_Obj.FloatxIb == 0)
                        {
                            Rb_Auto_Qf.Checked = true;
                        }
                        else
                        {
                            Rb_Custom_Qf.Checked = true;
                            Txt_Qf.Text = _Obj.FloatxIb.ToString();
                        }
                        if (_Obj.xTime == 0)
                        {
                            Rb_AutoT_Qf.Checked = true;
                        }
                        else
                        {
                            Rb_CustomT_Qf.Checked = true;
                            Txt_TQf.Text = _Obj.xTime.ToString();
                        }
                        break;
                }
                #endregion

            }
        }


        /// <summary>
        /// 方案名称（只写），设置后将自动加载方案：前提是该方案存在
        /// </summary>
        public string FAName
        {
            set
            {
                this.LoadFA(value);
            }
        }
        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_QiDong Copy()
        {
            CLDC_DataCore.Model.Plan.Plan_QiDong _Obj = new CLDC_DataCore.Model.Plan.Plan_QiDong((int)TaiType, "");
            if(Chk_Pz.CheckState== CheckState.Checked)
            {
                _Obj.Add(CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功,  //功率方向
                    Rb_Custom_Pz.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_Pz.Text)?float.Parse(Txt_Pz.Text):0F,  //电流倍数，为0则为规程自动计算
                    Rb_CustomT_Pz.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_TPz.Text)?float.Parse(Txt_TPz.Text):0F, //起动时间，为0则为自动计算
                    Chk_Default_Pz.CheckState== CheckState.Checked?1:0);
            }
            if (Chk_Pf.CheckState == CheckState.Checked)
            { 
                _Obj.Add(CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功,          //功率方向
                    Rb_Custom_Pf.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_Pf.Text) ? float.Parse(Txt_Pf.Text) : 0F,  //电流倍数，为0则为规程自动计算
                    Rb_CustomT_Pf.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_TPf.Text) ? float.Parse(Txt_TPf.Text) : 0F, //起动时间，为0则为自动计算
                    Chk_Default_Pf.CheckState == CheckState.Checked ? 1 : 0);
            }
            if (Chk_Qz.CheckState == CheckState.Checked)
            {
                _Obj.Add(CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功,          //功率方向
                    Rb_Custom_Qz.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_Qz.Text) ? float.Parse(Txt_Qz.Text) : 0F,  //电流倍数，为0则为规程自动计算
                    Rb_CustomT_Qz.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_TQz.Text) ? float.Parse(Txt_TQz.Text) : 0F, //起动时间，为0则为自动计算
                    Chk_Default_Qz.CheckState == CheckState.Checked ? 1 : 0);
            }
            if (Chk_Qf.CheckState == CheckState.Checked)
            {
                _Obj.Add(CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功,          //功率方向
                    Rb_Custom_Qf.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_Qf.Text) ? float.Parse(Txt_Qf.Text) : 0F,  //电流倍数，为0则为规程自动计算
                    Rb_CustomT_Qf.Checked && CLDC_DataCore.Function.Number.IsNumeric(Txt_TQf.Text) ? float.Parse(Txt_TQf.Text) : 0F, //起动时间，为0则为自动计算
                    Chk_Default_Qf.CheckState == CheckState.Checked ? 1 : 0);
            }

            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }


        #endregion
    }
}

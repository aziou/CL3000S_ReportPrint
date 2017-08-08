using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_ZouZi :UI_TableBase //UserControl
    {

        const string CONST_NOTESTRING = "查看或修改请双击";

        const string CONST_ADD = "保存本项";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        #region ----------------构造函数------------------

        public UI_ZouZi()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        public UI_ZouZi(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        public UI_ZouZi(CLDC_Comm.Enum.Cus_TaiType Ttype,string faname):base(Ttype,faname)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(faname);
        }


        public UI_ZouZi(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_ZouZi FAItem)
            : base(Ttype, FAItem.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(FAItem);
        }

        #endregion

        #region -----------------事件-----------------------
        private void Dgv_Data_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dgv_Data_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgv_Data.EndEdit();
        }
        /// <summary>
        /// 设置查看或修改走字内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)           //项目内容
            {
                if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value == null)
                {
                    MessageBoxEx.Show(this,"请选择走字方式...", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CLDC_Comm.Enum.Cus_ZouZiMethod _Tmp = new CLDC_Comm.Enum.Cus_ZouZiMethod();

                if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法;
                }
                else if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.基本走字法.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.基本走字法;
                }
                else if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.计读脉冲法.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.计读脉冲法;
                }
                else if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.校核常数.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.校核常数;
                }

                if (this.Controls[0] is PrjUI.UI_ZouZiFeiLv) return;
                PrjUI.UI_ZouZiFeiLv _Panel;
                if (Dgv_Data.CurrentCell.Tag is List<StPlan_ZouZi.StPrjFellv>)
                {
                    _Panel = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_ZouZiFeiLv(_Tmp, (List<StPlan_ZouZi.StPrjFellv>)Dgv_Data.CurrentCell.Tag);
                }
                else
                {
                    _Panel = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_ZouZiFeiLv(_Tmp);
                }
                _Panel.ClosePanel += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_ZouZiFeiLv.Evt_ClosePanel(FeiLv_ClosePanel);

                this.Controls.Add(_Panel);
                this.Controls.SetChildIndex(_Panel, 0);

                _Panel.Left = Dgv_Data.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left;

                _Panel.Top = Dgv_Data.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Bottom + this.panel1.Height;

                _Panel.BringToFront();

                Dgv_Data.Enabled = false;

            }
        }

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonState(e.RowIndex);

            if (e.ColumnIndex == Dgv_Data.Columns.Count - 1)         //最后一列
            {
                if (Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString() != CONST_ADD)
                {
                    if (MessageBoxEx.Show(this,"您确认要删除该方案项目么？", "删除询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Dgv_Data.Rows.RemoveAt(e.RowIndex);
                        this.CallOrder();
                    }
                    else
                    {
                        Dgv_Data[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    }
                    return;
                }

                if (!CheckOK(e.RowIndex)) return;

                Dgv_Data[e.ColumnIndex, e.RowIndex].Value = "删除";
                Dgv_Data[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[6].Value = CONST_NOTESTRING;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
                this.CallOrder();
            }
            else
            {
                Dgv_Data.BeginEdit(true);
                if (Dgv_Data.CurrentCell is DataGridViewComboBoxCell)
                {
                    if (BeforeRowIndex != e.RowIndex || BeforeColIndex != e.ColumnIndex)
                    {
                        SendKeys.Send("{F4}");
                    }
                }

                BeforeColIndex = e.ColumnIndex;

                BeforeRowIndex = e.RowIndex;
            }
        }

        /// <summary>
        /// 走字内容界面关闭事件
        /// </summary>
        /// <param name="FeiLvItem"></param>
        private void FeiLv_ClosePanel(List<StPlan_ZouZi.StPrjFellv> FeiLvItem)
        {
            PrjUI.UI_ZouZiFeiLv _Panel = this.Controls[0] as PrjUI.UI_ZouZiFeiLv;

            if (FeiLvItem.Count > 0)
            {
                Dgv_Data.CurrentCell.Tag = FeiLvItem;
                string zouziDesp = string.Empty;
                for (int i = 0; i < FeiLvItem.Count; i++)
                {
                    zouziDesp += FeiLvItem[i].FeiLv.ToString() + FeiLvItem[i].ZouZiTime.ToString() + " ";
                }
                Dgv_Data.CurrentCell.Value = zouziDesp;
            }
            this.Controls.RemoveAt(0);
            _Panel.Dispose();
            _Panel = null;

            Dgv_Data.Enabled = true;
        }

        private void Cmd_MoveUp_Click(object sender, EventArgs e)
        {
            base.CmdMoveUp_Click(sender, e);
        }

        private void Cmd_MoveDown_Click(object sender, EventArgs e)
        {
            base.CmdMoveDown_Click(sender, e);
        }
        /// <summary>
        /// 从方案列表中选择方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;

            this.FAName = Cmb_Fa.Text;
        }

        /// <summary>
        /// 清理表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            base.ClearDataView();
            Cmb_Fa.SelectedIndex = 0;
        }

                #endregion 

        #region ------------------私有方法、函数---------------

        /// <summary>
        /// 初始化表格ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME);

            #region ---------功率方向下拉菜单----------------------
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("正向有功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("反向有功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("正向无功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("反向无功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("第一象限无功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("第二象限无功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("第三象限无功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("第四象限无功");
            #endregion 

            #region -----------初始化元件下拉菜单----------------

            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.H.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.A.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.B.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.C.ToString());

            #endregion 

            #region -------------------初始化功率因素下拉菜单---------------

            List<string> _Glyss = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();

            for(int i =0;i<_Glyss.Count;i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[3]).Items.Add(_Glyss[i]);
            }

            _Glyss = null;
            
            #endregion 

            #region ------------初始化电流倍数下拉菜单--------------------
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbCol.Load();
            List<string> _xIbs = _xIbCol.getxIb();

            for (int i = 0; i < _xIbs.Count; i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[4]).Items.Add(_xIbs[i]);
            }

            _xIbs = null;
            _xIbCol = null;
            #endregion

            #region -----------初始化走字方式下拉菜单--------------
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[5]).Items.Add(CLDC_Comm.Enum.Cus_ZouZiMethod.校核常数.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[5]).Items.Add(CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[5]).Items.Add(CLDC_Comm.Enum.Cus_ZouZiMethod.基本走字法.ToString());

            #endregion 

            int RowIndex = Dgv_Data.Rows.Add();
            Dgv_Data.Rows[RowIndex].Cells[6].Value = CONST_NOTESTRING;
            Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
            Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            Dgv_Data.Refresh();
        }

        /// <summary>
        /// 数据准确性校验
        /// </summary>
        /// <param name="RowIndex">表格行</param>
        /// <returns></returns>
        private bool CheckOK(int RowIndex)
        {
            if (Dgv_Data[1, RowIndex].Value == null || Dgv_Data[1, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请选择正确的功率方向...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请选择正确的元件...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || Dgv_Data[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请选择正确的功率因素...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[4, RowIndex].Value == null || Dgv_Data[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请选择正确的电流倍数...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[4, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[5, RowIndex].Value == null || Dgv_Data[5, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请选择正确的走字方式...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[5, RowIndex].Selected = true;
                return false;
            }
            if (!(Dgv_Data[6, RowIndex].Tag is List<StPlan_ZouZi.StPrjFellv>))
            {
                MessageBoxEx.Show(this,"没有具体的走字内容...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        #endregion

        #region  -----------------公有方法、函数-----------

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
        /// 方案加载
        /// </summary>
        /// <param name="FAName">方案名称</param>
        public void LoadFA(string FAName)
        {

            Dgv_Data.Rows.Clear();          //首先清理列表数据

            CLDC_DataCore.Model.Plan.Plan_ZouZi _ZouZi = new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)base.TaiType, FAName);        //打开一个方案

            this.LoadFA(_ZouZi);
        }

        /// <summary>
        /// 加载方案项目
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_ZouZi FaItem)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = FaItem.Name;

            try
            {
                Cmb_Fa.Text = FaItem.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }
            for (int _i = 0; _i < FaItem.Count; _i++)            //循环方案对象
            {
                StPlan_ZouZi _Obj = FaItem.getZouZiPrj(_i);         //取出一个方案项目
                string zouziShortDecript = string.Empty;
                for (int i = 0; i < _Obj.ZouZiPrj.Count; i++)
                {
                    zouziShortDecript += _Obj.ZouZiPrj[i].FeiLv + _Obj.ZouZiPrj[i].ZouZiTime + " ";
                }
                //Dgv_Data.Rows[RowIndex].Cells[6].Value = zouziShortDecript;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Obj.PowerFangXiang.ToString();        //功率方向
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Obj.PowerYj.ToString();               //元件
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Obj.Glys;                             //功率因素
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Obj.xIb;                             //电流倍数
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Obj.ZouZiMethod.ToString();          //走字方法
                Dgv_Data.Rows[RowIndex].Cells[6].Value = zouziShortDecript;
                Dgv_Data.Rows[RowIndex].Cells[6].Tag =_Obj.ZouZiPrj;                            //走字内容
                Dgv_Data.Rows[RowIndex].Cells[7].Value = _Obj.ZuHeWc == "0" ? false : true;            //组合误差
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "删除";       //删除按钮
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //删除按钮为红色
            }

            {
                int RowIndex = Dgv_Data.Rows.Add();                 //最后增加一个空行，用于新增
                Dgv_Data.Rows[RowIndex].Cells[6].Value = CONST_NOTESTRING;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }

            this.UpDownButtonState(0);    //设置上下移动按钮状态

        }


        /// <summary>
        /// 拷贝方案
        /// </summary>
        public CLDC_DataCore.Model.Plan.Plan_ZouZi Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_ZouZi _Obj = new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == CONST_ADD)
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)TaiType, "");

                    CLDC_Comm.Enum.Cus_PowerFangXiang _Tmp = new CLDC_Comm.Enum.Cus_PowerFangXiang();

                    if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.第一象限无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.第一象限无功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.第二象限无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.第二象限无功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.第三象限无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.第三象限无功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.第四象限无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.第四象限无功;
                    else
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;

                    CLDC_Comm.Enum.Cus_ZouZiMethod _TmpMethod = new CLDC_Comm.Enum.Cus_ZouZiMethod();

                    if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法.ToString())
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法;
                    else if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.基本走字法.ToString())
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.基本走字法;
                    else if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.计读脉冲法.ToString())
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.计读脉冲法;
                    else if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.校核常数.ToString())
                    {
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.校核常数;
                    }
                    else
                    {
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法;
                    }
                    CLDC_Comm.Enum.Cus_PowerYuanJian _TmpYuan = new CLDC_Comm.Enum.Cus_PowerYuanJian();

                    if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.H.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.H;
                    else if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.A.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
                    else if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.B.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.B;
                    else if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.C.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.C;
                    else
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.H;



                    _Obj.Add(_Tmp,                                  //功率方向 
                             _TmpMethod,                            //走字方法
                             _TmpYuan,                              //元件
                             Dgv_Data[3, i].Value.ToString(),       //功率因素 
                             Dgv_Data[4, i].Value.ToString(),       //电流倍数    
                             "",                                    //走字描述
                             Dgv_Data[7, i].Value==null?"0":(bool)Dgv_Data[7,i].Value==false?"0":"1",       //组合误差
                             (List<StPlan_ZouZi.StPrjFellv>)Dgv_Data[6, i].Tag);                   //走字项目
                }
            }
            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }

        #endregion 

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

    }
}

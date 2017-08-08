using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using CLDC_DataCore.Const;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_ErrAccord:UI_TableBaseNew
    {
        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        #region ----------------构造------------------
        /// <summary>
        /// 构造函数
        /// </summary>
        public UI_ErrAccord()
        {
            InitializeComponent();

            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        public UI_ErrAccord(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        /// <param name="faname">需要加载的方案名称</param>
        public UI_ErrAccord(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(faname);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        /// <param name="FAItem">误差一致性方案项目</param>
        public UI_ErrAccord(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_ErrAccord FAItem)
            : base(Ttype, FAItem.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(FAItem);
        }
        #endregion

        #region -----------------事件-----------------------

        private void chk_Yzx_CheckedChanged(object sender, EventArgs e)
        {
            pnlYZX.Enabled = chk_Yzx.Checked == true ? true : false;
        }

        private void chk_Bc_CheckedChanged(object sender, EventArgs e)
        {
            pnlBC.Enabled = chk_Bc.Checked == true ? true : false;
        }

        private void chk_Sj_CheckedChanged(object sender, EventArgs e)
        {
            pnlSJ.Enabled = chk_Sj.Checked == true ? true : false;
        }

        private void chk_Gz_CheckedChanged(object sender, EventArgs e)
        {
            pnlGZ.Enabled = chk_Gz.Checked == true ? true : false;
        }

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonState(e.RowIndex);
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

        private void Dgv_Data_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgv_Data.EndEdit();
        }

        private void Cmd_MoveUp_Click(object sender, EventArgs e)
        {
            base.CmdMoveUp_Click(sender, e);
        }

        private void Cmd_MoveDown_Click(object sender, EventArgs e)
        {
            base.CmdMoveDown_Click(sender, e);
        }

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
            chk_Yzx.Checked = false;
            chk_Bc.Checked = false;
            chk_Sj.Checked = false;
            chk_Gz.Checked = false;
            Cmb_Fa.SelectedIndex = 0;
        }

        #endregion 

        #region --------------------私有方法、函数--------------
        /// <summary>
        /// 初始化表格ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            //初始化方案项目名称下拉菜单
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_YZX_FOLDERNAME);

            #region 初始化电流倍数下拉菜单
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbCol.Load();
            List<string> _xIbs = _xIbCol.getxIb();

            for (int i = 0; i < _xIbs.Count; i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add(_xIbs[i]);

                #region 2010-03-26 Add by Gqs:初始化升降选择的电流点下拉菜单
                cbo_Value1.Items.Add(_xIbs[i]);
                cbo_Value2.Items.Add(_xIbs[i]);
                cbo_Value3.Items.Add(_xIbs[i]);
                cbo_Value4.Items.Add(_xIbs[i]);
                #endregion
            }
            #endregion

            #region 初始化功率因数下拉菜单

            List<string> _Glys = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();
            for (int i = 0; i < _Glys.Count; i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(_Glys[i]);
            }
            #endregion

            #region 2010-03-26 Add by Gqs:增加各项的颜色区分
            //tlp_Bc.BackColor = Variable.Color_Grid_Alter;
            //tlp_Gz.BackColor = Variable.Color_Grid_Alter;
            #endregion

            #region 2010-03-26 Add by Gqs:升降电流点给默认值 
            try
            {
                cbo_Value1.Text = "0.01Ib";
                cbo_Value2.Text = "0.05Ib";
                cbo_Value3.Text = "1.0Ib";
                cbo_Value4.Text = "Imax";
            }
            catch
            {
            }
            #endregion

            #region 2010-03-29 Add by Gqs:误差一致性默认为四个检定点
            string[] strXIb = {"1.0Ib","1.0Ib","0.01Ib","0.01Ib"};
            string[] strGlys = { "1.0", "0.5L", "1.0", "0.5L" };
            int RowIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = strXIb[i];
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = strGlys[i];
            }
            Dgv_Data.Refresh();
            #endregion
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
                MessageBoxEx.Show(this,"请选择正确的电流...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请填写正确的功率因数...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            return true;
        }

        #region 2010-03-26 Add by Gqs:增加对变差试验、过载试验时间的判断
        /// <summary>
        /// 数据准确性校验
        /// </summary>
        /// <param name="TypeIndex">类型 1:变差 2:升降 3:过载</param>
        /// <returns></returns>
        private bool CheckDigOK(int TypeIndex)
        {
            if (TypeIndex == 1)
            {
                if (txt_Bc.Text.Trim() == null || !CLDC_DataCore.Function.Number.IsNumeric(txt_Bc.Text.Trim()))
                {
                    MessageBoxEx.Show(this,"请填写正确的测试时间间隔，时间为一个大于零的数字...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_Bc.Focus();
                    return false;
                }
            }
            else if (TypeIndex == 2)
            {
                if (cbo_Value1.Text == null || cbo_Value1.Text == "")
                {
                    MessageBoxEx.Show(this,"请选择正确的电流点...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbo_Value1.Focus();
                    return false;
                }
                if (cbo_Value2.Text == null || cbo_Value2.Text == "")
                {
                    MessageBoxEx.Show(this,"请选择正确的电流点...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbo_Value2.Focus();
                    return false;
                }
                if (cbo_Value3.Text == null || cbo_Value3.Text == "")
                {
                    MessageBoxEx.Show(this,"请选择正确的电流点...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbo_Value3.Focus();
                    return false;
                }
                if (cbo_Value4.Text == null || cbo_Value4.Text == "")
                {
                    MessageBoxEx.Show(this,"请选择正确的电流点...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbo_Value4.Focus();
                    return false;
                }
            }
            else if (TypeIndex == 3)
            {
                if (txt_Gz1.Text.Trim() == null || !CLDC_DataCore.Function.Number.IsNumeric(txt_Gz1.Text.Trim()))
                {
                    MessageBoxEx.Show(this,"请填写正确的过载时间，时间为一个大于零的数字...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_Gz1.Focus();
                    return false;
                }
                if (txt_Gz2.Text.Trim() == null || !CLDC_DataCore.Function.Number.IsNumeric(txt_Gz2.Text.Trim()))
                {
                    MessageBoxEx.Show(this,"请填写正确的恢复等待时间，时间为一个大于零的数字...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_Gz2.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion

        #endregion

        #region ----------公有方法、函数------------

        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_ErrAccord Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_ErrAccord _Obj = new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)TaiType, "");

            string strPara1 = "";
            string strPara2 = "";
            string strPara3 = "";
            string strPara4 = "";

            if (chk_Yzx.Checked)
            {
                CLDC_Comm.Enum.Cus_WcType _WcType = CLDC_Comm.Enum.Cus_WcType.误差一致性;

                for (int i = 0; i < Dgv_Data.Rows.Count; i++)
                {
                    switch (i + 1)
                    {
                        case 1:
                            strPara1 = Dgv_Data[1, 0].Value.ToString() + "|" + Dgv_Data[2, 0].Value.ToString();
                            break;
                        case 2:
                            strPara2 = Dgv_Data[1, 1].Value.ToString() + "|" + Dgv_Data[2, 1].Value.ToString();
                            break;
                        case 3:
                            strPara3 = Dgv_Data[1, 2].Value.ToString() + "|" + Dgv_Data[2, 2].Value.ToString();
                            break;
                        case 4:
                            strPara4 = Dgv_Data[1, 3].Value.ToString() + "|" + Dgv_Data[2, 3].Value.ToString();
                            break;
                    }
                }
                _Obj.Add(_WcType, strPara1, strPara2, strPara3, strPara4, 0, 0);
            }
            if (chk_Bc.Checked)
            {
                if (!this.CheckDigOK(1)) return new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)TaiType, "");

                CLDC_Comm.Enum.Cus_WcType _WcType = CLDC_Comm.Enum.Cus_WcType.误差变差试验;
                _Obj.Add(_WcType, "1.0Ib|1.0", "1.0Ib|0.5L", "", "", float.Parse(txt_Bc.Text), 0);
            }
            if (chk_Sj.Checked)
            {
                if (!this.CheckDigOK(1)) return new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)TaiType, "");

                CLDC_Comm.Enum.Cus_WcType _WcType = CLDC_Comm.Enum.Cus_WcType.电流升降试验;
                _Obj.Add(_WcType, "",
                    cbo_Value2.Text + "|1.0",
                    cbo_Value3.Text + "|1.0",
                    cbo_Value4.Text + "|1.0", 0, 0);//cbo_Value1.Text + "|1.0"
            }
            if (chk_Gz.Checked)
            {
                if (!this.CheckDigOK(3)) return new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)TaiType, "");

                CLDC_Comm.Enum.Cus_WcType _WcType = CLDC_Comm.Enum.Cus_WcType.电流过载试验;
                _Obj.Add(_WcType, "10Ib|1.0", "1.0Ib|1.0", "","",float.Parse(txt_Gz1.Text),float.Parse(txt_Gz2.Text));
            }
            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
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
        /// 方案加载
        /// </summary>
        /// <param name="FAName">方案名称</param>
        public void LoadFA(string FAName)
        {

            //Dgv_Data.Rows.Clear();          //首先清理列表数据

            CLDC_DataCore.Model.Plan.Plan_ErrAccord _ErrAccord = new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)base.TaiType, FAName);        //打开一个方案

            this.LoadFA(_ErrAccord);
        }

        /// <summary>
        /// 加载方案项目
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_ErrAccord FaItem)
        {
            //Dgv_Data.Rows.Clear();

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
                CLDC_DataCore.Struct.StErrAccord _Obj = FaItem.getErrAccordPrj(_i);         //取出一个方案项目
                string[] strPara = _Obj.PrjName.Split(' ');
                string[] strTmp = new string[2];

                if (_Obj.ErrAccordType == 1)
                {
                    chk_Yzx.Checked = true;

                    for (int _row = 0; _row < strPara.Length - 1; _row++)
                    {
                        strTmp = strPara[_row].Split('|');
                        Dgv_Data.Rows[_row].Cells[0].Value = _row + 1;
                        ((DataGridViewComboBoxCell)Dgv_Data.Rows[_row].Cells[1]).Value = strTmp[0];
                        ((DataGridViewComboBoxCell)Dgv_Data.Rows[_row].Cells[2]).Value = strTmp[1];
                    }
                }
                else if (_Obj.ErrAccordType == 2)
                {
                    chk_Bc.Checked = true;
                    txt_Bc.Text = _Obj.Time1.ToString();
                }
                else if (_Obj.ErrAccordType == 3)
                {
                    chk_Sj.Checked = true;

                    for (int _index = 0; _index < strPara.Length - 1; _index++)
                    {
                        strTmp = strPara[_index].Split('|');
                        switch (_index + 1)
                        {
                            case 1:
                                cbo_Value1.Text = strTmp[0];
                                break;
                            case 2:
                                cbo_Value2.Text = strTmp[0];
                                break;
                            case 3:
                                cbo_Value3.Text = strTmp[0];
                                break;
                            case 4:
                                cbo_Value4.Text = strTmp[0];
                                break;
                        }
                    }
                }
                else if (_Obj.ErrAccordType == 4)
                {
                    chk_Gz.Checked = true;
                    txt_Gz1.Text = _Obj.Time1.ToString();
                    txt_Gz2.Text = _Obj.Time2.ToString();
                }
            }

            this.UpDownButtonState(0);    //设置上下移动按钮状态

        }

        #endregion    

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        

        

        

        

        

        

        

        

        
    }
}

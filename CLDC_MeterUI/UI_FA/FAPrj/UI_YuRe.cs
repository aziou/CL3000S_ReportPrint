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
    public partial class UI_YuRe :UI_TableBase // UserControl 
    {

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        #region ----------------构造------------------

        public UI_YuRe()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        public UI_YuRe(CLDC_Comm.Enum.Cus_TaiType Ttype)
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
        public UI_YuRe(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
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
        /// <param name="FAItem">预热方案项目</param>
        public UI_YuRe(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_YuRe FAItem):base(Ttype,FAItem.Name)
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

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonState(e.RowIndex);

            if (e.ColumnIndex == Dgv_Data.Columns.Count - 1)         //最后一列
            {
                if (Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString() != "添加")
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

                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "添加";
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
            Cmb_Fa.SelectedIndex = 0;
        }

        #endregion 

        #region --------------------私有方法、函数--------------
        /// <summary>
        /// 初始化表格ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME);

            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("正向有功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("反向有功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("正向无功");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("反向无功");

            #region 初始化电流倍数下拉菜单
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbCol.Load();
            List<string> _xIbs = _xIbCol.getxIb();

            for (int i = 0; i < _xIbs.Count; i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(_xIbs[i]);
            }
            #endregion

            int RowIndex = Dgv_Data.Rows.Add();
            Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "添加";
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
                MessageBoxEx.Show(this,"请选择正确的预热电流...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || !CLDC_DataCore.Function.Number.IsNumeric(Dgv_Data[3, RowIndex].Value.ToString().Trim()))
            {
                MessageBoxEx.Show(this,"请填写正确的预热时间，时间因为一个大于零的数字...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            return true;
        }



        #endregion 


        #region ----------公有方法、函数------------

        /// <summary>
        /// 拷贝方案
        /// </summary>
        public CLDC_DataCore.Model.Plan.Plan_YuRe Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_YuRe((int)TaiType,"");

            CLDC_DataCore.Model.Plan.Plan_YuRe _Obj = new CLDC_DataCore.Model.Plan.Plan_YuRe((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == "添加")
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_YuRe((int)TaiType,"");

                    CLDC_Comm.Enum.Cus_PowerFangXiang _Tmp = new CLDC_Comm.Enum.Cus_PowerFangXiang();

                    if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功;
                    else
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;

                    _Obj.Add(i, _Tmp, Dgv_Data[2, i].Value.ToString(), float.Parse(Dgv_Data[3, i].Value.ToString()));
                }
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

            Dgv_Data.Rows.Clear();          //首先清理列表数据

            CLDC_DataCore.Model.Plan.Plan_YuRe _YuRe = new CLDC_DataCore.Model.Plan.Plan_YuRe((int)base.TaiType, FAName);        //打开一个方案

            this.LoadFA(_YuRe);
        }

        /// <summary>
        /// 加载方案项目
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_YuRe FaItem)
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
                StPlan_YuRe _Obj = FaItem.getYuRePrj(_i);         //取出一个方案项目

                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Obj.PowerFangXiang.ToString();        //功率方向
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Obj.xIb;              //电流倍数
                Dgv_Data.Rows[RowIndex].Cells[3].Value = _Obj.Times;                            //预热时间 
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "删除";       //删除按钮
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //删除按钮为红色
            }

            {
                int RowIndex = Dgv_Data.Rows.Add();                 //最后增加一个空行，用于新增
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "添加";
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }

            this.UpDownButtonState(0);    //设置上下移动按钮状态

        }

        #endregion 

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}

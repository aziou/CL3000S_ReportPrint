using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CLDC_DataCore.Struct;
using CLDC_DataCore.Model.Plan;
using CLDC_Comm.Enum;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_LoadRecord : UI_TableBase
    {
        const string CONST_NOTESTRING = "删除";

        const string CONST_ADD = "保存本项";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        public UI_LoadRecord()
        {
            InitializeComponent();
            base.Init(dgv_RunningE, null, null);
            InitUI();
        }
        public UI_LoadRecord(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(dgv_RunningE, null, null);
            InitUI();
        }
        public UI_LoadRecord(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            base.Init(dgv_RunningE, null, null);
            InitUI();
            LoadFA(faname);
        }
        public UI_LoadRecord(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_LoadRecord FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            base.Init(dgv_RunningE, null, null);
            InitUI();
            LoadFA(FaItem);
        }
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
        internal void LoadFA(CLDC_DataCore.Model.Plan.Plan_LoadRecord plan_LoadRecord)
        {
            base.FaName = plan_LoadRecord.Name;
            this.LoadPlan(plan_LoadRecord);
        }

        internal void LoadFA(string _FaName)
        {
            //【打开一个方案】
            Plan_LoadRecord pcc_CarrierPlan = new Plan_LoadRecord((int)base.TaiType, _FaName);

            //【加载方案】
            this.LoadPlan(pcc_CarrierPlan);
        }

        private void LoadPlan(CLDC_DataCore.Model.Plan.Plan_LoadRecord FaItem)
        {
            dgv_RunningE.Rows.Clear();

            base.FaName = FaItem.Name;
            StPlan_LoadRecord _ObjA = FaItem.GetCurrentPrj(0);         //取出一个方案项目
            txt_overTime.Text = _ObjA.OverTime.ToString();
            comboBox1.Text = _ObjA.danWei; ;
            txt_marginTime.Text = _ObjA.MarginTime.ToString();
            string moType = _ObjA.ModeByte + "110000";
            for (int i = 0; i < 6; i++)
            {
                dgv_ModeByte[i, 0].Value = moType[i];
            }

            int cut = 0;
            if (_ObjA.RunningEPrj != null)
            {
                cut = _ObjA.RunningEPrj.Count;
            }
            for (int _i = 0; _i < cut; _i++)            //循环方案对象
            {
                StRunningE _Obj = _ObjA.RunningEPrj[_i];

                int RowIndex = dgv_RunningE.Rows.Add();
                dgv_RunningE.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)dgv_RunningE.Rows[RowIndex].Cells[1]).Value = _Obj.PowerFX.ToString();        //功率方向
                ((DataGridViewComboBoxCell)dgv_RunningE.Rows[RowIndex].Cells[2]).Value = _Obj.xIB.ToString();               //元件
                ((DataGridViewComboBoxCell)dgv_RunningE.Rows[RowIndex].Cells[3]).Value = _Obj.Glys;                             //功率因素
                dgv_RunningE.Rows[RowIndex].Cells[4].Value = _Obj.RunningTime;

                dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Value = CONST_NOTESTRING;       //删除按钮
                dgv_RunningE[dgv_RunningE.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //删除按钮为红色
            }

            {
                int RowIndex = dgv_RunningE.Rows.Add();                 //最后增加一个空行，用于新增
                dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Value = CONST_ADD;
                dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }
        }
        #endregion

        /// <summary>
        /// UI初始化
        /// </summary>
        private void InitUI()
        {
            if (dgv_ModeByte.Rows.Count != 1)
            {
                dgv_ModeByte.Rows.Clear();
                dgv_ModeByte.Rows.Add();

            }
            #region ---------功率方向下拉菜单----------------------
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("正向有功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("反向有功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("正向无功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("反向无功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("第一象限无功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("第二象限无功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("第三象限无功");
            ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[1]).Items.Add("第四象限无功");
            #endregion 

            #region ------------初始化电流倍数下拉菜单--------------------
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbCol.Load();
            List<string> _xIbs = _xIbCol.getxIb();

            for (int i = 0; i < _xIbs.Count; i++)
            {
                ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[2]).Items.Add(_xIbs[i]);
            }

            _xIbs = null;
            _xIbCol = null;
            #endregion

            #region -------------------初始化功率因素下拉菜单---------------

            List<string> _Glyss = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();

            for (int i = 0; i < _Glyss.Count; i++)
            {
                ((DataGridViewComboBoxColumn)dgv_RunningE.Columns[3]).Items.Add(_Glyss[i]);
            }

            _Glyss = null;

            #endregion 

            int RowIndex = dgv_RunningE.Rows.Add();
            dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Value = CONST_ADD;
            dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Style.ForeColor = Color.Blue;
            dgv_RunningE.Refresh();
        }

        private void dgv_RunningE_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgv_RunningE.EndEdit();
        }

        private void dgv_RunningE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_RunningE.Columns.Count - 1)         //最后一列
            {
                if (dgv_RunningE[e.ColumnIndex, e.RowIndex].Value.ToString() != CONST_ADD)
                {
                    if (MessageBoxEx.Show(this, "您确认要删除该方案项目么？", "删除询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        dgv_RunningE.Rows.RemoveAt(e.RowIndex);
                        this.CallOrder();
                    }
                    else
                    {
                        dgv_RunningE[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    }
                    return;
                }

                if (!CheckOK(e.RowIndex)) return;

                dgv_RunningE[e.ColumnIndex, e.RowIndex].Value = CONST_NOTESTRING;
                dgv_RunningE[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

                int RowIndex = dgv_RunningE.Rows.Add();

                dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Value = CONST_ADD;
                dgv_RunningE.Rows[RowIndex].Cells[dgv_RunningE.Columns.Count - 1].Style.ForeColor = Color.Blue;
                this.CallOrder();
            }
            else
            {
                dgv_RunningE.BeginEdit(true);
                if (dgv_RunningE.CurrentCell is DataGridViewComboBoxCell)
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

        private bool CheckOK(int RowIndex)
        {
            if (txt_overTime.Text == null || txt_overTime.Text.Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入正确的时间...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_overTime.Text = "45";
                return false;
            }
            if (comboBox1.Text == null || comboBox1.Text.Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入正确的时间...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Text = "分钟";
                return false;
            }
            if (txt_marginTime.Text == null || txt_marginTime.Text.Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入正确的间隔时间...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_marginTime.Text = "15";
                return false;
            }
            if (dgv_RunningE[1, RowIndex].Value == null || dgv_RunningE[1, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请选择正确的功率方向...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_RunningE[1, RowIndex].Selected = true;
                return false;
            }
            if (dgv_RunningE[2, RowIndex].Value == null || dgv_RunningE[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请选择正确的电流倍数...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_RunningE[2, RowIndex].Selected = true;
                return false;
            }
            if (dgv_RunningE[3, RowIndex].Value == null || dgv_RunningE[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请选择正确的功率因素...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_RunningE[3, RowIndex].Selected = true;
                return false;
            }
            if (dgv_RunningE[4, RowIndex].Value == null || dgv_RunningE[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入正确的时间...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_RunningE[4, RowIndex].Selected = true;
                return false;
            }
            return true;
        }

        public Plan_LoadRecord Copy()
        {
            if (dgv_RunningE.Rows.Count == 1) return new Plan_LoadRecord((int)TaiType, "");
            Plan_LoadRecord _Obj = new Plan_LoadRecord((int)TaiType, "");
            int overTime = int.Parse(txt_overTime.Text);
            string danWei = comboBox1.Text;
            int marginTime = int.Parse(txt_marginTime.Text);
            StringBuilder mType =new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                mType.Append(dgv_ModeByte[i, 0].Value.ToString());
            }
            mType.Append("110000");
            string modeType = mType.ToString(0, 6);
            List<StRunningE> lstR = new List<StRunningE>();
            for (int i = 0; i < dgv_RunningE.Rows.Count; i++)
            {
                if (dgv_RunningE[dgv_RunningE.Columns.Count - 1, i].Value.ToString() == CONST_ADD)
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new Plan_LoadRecord((int)TaiType, "");

                    StRunningE stp = new StRunningE();
                    stp.PowerFX = (Cus_PowerFangXiang)Enum.Parse(typeof(Cus_PowerFangXiang), dgv_RunningE[1, i].Value.ToString());
                    stp.xIB = dgv_RunningE[2, i].Value.ToString();
                    stp.Glys = dgv_RunningE[3, i].Value.ToString();
                    stp.RunningTime = dgv_RunningE[4, i].Value.ToString();

                    lstR.Add(stp);
                }
            }
            _Obj.Add(overTime, danWei, marginTime, modeType, lstR);
            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }

        private void dgv_RunningE_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

    }
}

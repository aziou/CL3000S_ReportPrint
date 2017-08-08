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
    /// <summary>
    /// 功能描述：红外数据比对方案组件
    /// 作    者：zzg soinlove@126.com
    /// 编写日期：2014-05-07
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    public partial class UI_Infrared : UI_TableBase
    {
        #region--------------私有变量-----------------
        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;
        
        #endregion------------------------------------

        #region--------------公共属性-----------------
        /// <summary>
        /// 方案名称（只写），设置后将自动加载方案：前提是该方案存在
        /// </summary>
        public string PlanName
        {
            set
            {
                this.LoadPlan(value);
            }
        }
        #endregion------------------------------------

        #region--------------构造函数-----------------
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UI_Infrared()
        {
            InitializeComponent();

            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_ctt_Ttype">台体类型</param>
        public UI_Infrared(CLDC_Comm.Enum.Cus_TaiType p_ctt_Ttype)
            : base(p_ctt_Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_ctt_Ttype">台体类型</param>
        /// <param name="p_str_PlanName">需要加载的方案名称</param>
        public UI_Infrared(CLDC_Comm.Enum.Cus_TaiType p_ctt_Ttype, string p_str_PlanName)
            : base(p_ctt_Ttype, p_str_PlanName)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadPlan(p_str_PlanName);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_ctt_Ttype">台体类型</param>
        /// <param name="p_plc_InfraredName">红外数据比对试验方案项目</param>
        public UI_Infrared(CLDC_Comm.Enum.Cus_TaiType p_ctt_Ttype, CLDC_DataCore.Model.Plan.Plan_Infrared p_plc_InfraredName)
            : base(p_ctt_Ttype, p_plc_InfraredName.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
   
            this.LoadPlan(p_plc_InfraredName);
        }
        #endregion------------------------------------

        #region--------------私有事件-----------------
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

            this.PlanName = Cmb_Fa.Text;
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

        #endregion------------------------------------

        #region--------------私有函数-----------------
        /// <summary>
        /// 初始化表格ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            //【初始化方案项目名称下拉菜单】
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME);
                        

            //【初始化项目名称下拉菜单】
            _csDataFlag = new CLDC_DataCore.SystemModel.Item.csDataFlag();
            _csDataFlag.Load();

            List<string> lst_DataFlagNames = _csDataFlag.GetDataFlagNameList();

            foreach (string name in lst_DataFlagNames)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add(name);
            }            

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
                MessageBoxEx.Show(this,"请填写项目名称...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请填写标识符...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }            

            return true;
        }
        #endregion------------------------------------

        #region--------------公共函数-----------------

        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_Infrared Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_Infrared((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_Infrared _Obj = new CLDC_DataCore.Model.Plan.Plan_Infrared((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == "添加")
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_Infrared((int)TaiType, "");

                    _Obj.Add(i, Dgv_Data.Rows[i].Cells[1].Value.ToString(),
                               Dgv_Data.Rows[i].Cells[2].Value.ToString()
                               );
                }
            }

            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }



        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="p_str_PlanName">方案名称</param>
        public void LoadPlan(string p_str_PlanName)
        {
            //【清理列表数据】
            Dgv_Data.Rows.Clear();

            //【打开一个方案】
            CLDC_DataCore.Model.Plan.Plan_Infrared pcc_InfraredPlan = new CLDC_DataCore.Model.Plan.Plan_Infrared((int)base.TaiType, p_str_PlanName);

            //【加载方案】
            this.LoadPlan(pcc_InfraredPlan);
        }

        /// <summary>
        /// 加载方案项目
        /// </summary>
        /// <param name="p_pcc_Item">方案项目</param>
        public void LoadPlan(CLDC_DataCore.Model.Plan.Plan_Infrared p_pcc_Item)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = p_pcc_Item.Name;

            try
            {
                Cmb_Fa.Text = p_pcc_Item.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }

            //【遍历方案对象】
            for (int _i = 0; _i < p_pcc_Item.Count; _i++)
            {
                //【取出一个方案项目】
                StPlan_Infrared _Obj = p_pcc_Item.GetCarrierPrj(_i);

                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;                
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Obj.str_Name;                 //项目名称
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Obj.str_Code;                 //标识符                
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "删除";                           //删除按钮
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;                         //删除按钮为红色
            }

            //【最后增加一个空行，用于新增】
            {
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "添加";
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }

            //【设置上下移动按钮状态】
            this.UpDownButtonState(0);

        }
        #endregion------------------------------------

        private void Dgv_Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string strDataFlagName = "";
            CLDC_DataCore.Struct.StDataFlagInfo _DataFlag;
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                strDataFlagName = Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString();
                _DataFlag = _csDataFlag.GetDataFlagInfo(strDataFlagName);
                
                ((DataGridViewCell)Dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex + 1]).Value = _DataFlag.DataFlag;
                
            }
        }

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        
       

    }
}

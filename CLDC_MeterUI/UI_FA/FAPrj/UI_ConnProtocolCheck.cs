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
    public partial class UI_ConnProtocolCheck :UI_TableBase //UserControl
    {
        const string CONST_ADD = "保存本项";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;

        #region ----------------构造函数------------------

        public UI_ConnProtocolCheck()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        public UI_ConnProtocolCheck(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        public UI_ConnProtocolCheck(CLDC_Comm.Enum.Cus_TaiType Ttype,string faname):base(Ttype,faname)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(faname);
        }


        public UI_ConnProtocolCheck(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck FAItem)
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
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
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

        private void Cmd_MoveUp_Click(object sender, EventArgs e)
        {
            base.CmdMoveUp_Click(sender, e);
        }

        private void Cmd_MoveDown_Click(object sender, EventArgs e)
        {
            base.CmdMoveDown_Click(sender, e);
        }
        #endregion 

        #region ------------------私有方法、函数---------------

        /// <summary>
        /// 初始化表格ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            //base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_ConnProtocol_FOLDERNAME);


            #region ---------下拉菜单----------------------
            _csDataFlag = new CLDC_DataCore.SystemModel.Item.csDataFlag();
            _csDataFlag.Load();
            List<string> lst_DataFlagNames = _csDataFlag.GetDataFlagNameList();

            foreach (string name in lst_DataFlagNames)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add(name);
            }
                       
            #endregion 

            int RowIndex = Dgv_Data.Rows.Add();
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
                MessageBoxEx.Show(this,"请选择一项数据项名称...", "请选择", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请输入标识...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || Dgv_Data[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请输入数据长度...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[4, RowIndex].Value == null || Dgv_Data[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请输入小数位数...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[4, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[5, RowIndex].Value == null || Dgv_Data[5, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请输入正确的数据格式...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[5, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[6, RowIndex].Value == null || Dgv_Data[6, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"请选择一项功能...", "请选择", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[6, RowIndex].Selected = true;
                return false;
            }
            if ((Dgv_Data[7, RowIndex].Value == null || Dgv_Data[7, RowIndex].Value.ToString().Trim() == "") && Dgv_Data[6, RowIndex].Value.ToString() == "写")
            {
                MessageBoxEx.Show(this,"请输入要写的内容...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[6, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[7, RowIndex].Value == null)
                Dgv_Data[7, RowIndex].Value = "";
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

            CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _ConnProtocol = new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)base.TaiType, FAName);        //打开一个方案

            this.LoadFA(_ConnProtocol);
        }

        /// <summary>
        /// 加载方案项目
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck FaItem)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = FaItem.Name;

            for (int _i = 0; _i < FaItem.Count; _i++)            //循环方案对象
            {
                StPlan_ConnProtocol _Item = FaItem.getConnProtocolPrj(_i);         //取出一个方案项目
                string zouziShortDecript = string.Empty;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Item.ConnProtocolItem.ToString();        //数据项名称
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Item.ItemCode;               //标识编码
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Item.DataLen.ToString();                             //数据长度
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Item.PointIndex.ToString();                             //小数位索引
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Item.StrDataType;          //数据格式
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[6]).Value =_Item.OperType.ToString();              //操作类型,读/写
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[7]).Value = _Item.WriteContent;                    //写入内容
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "删除";       //删除按钮
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //删除按钮为红色
            }

            {
                int RowIndex = Dgv_Data.Rows.Add();                 //最后增加一个空行，用于新增
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }
            this.UpDownButtonState(0);    //设置上下移动按钮状态
        }


        /// <summary>
        /// 拷贝方案
        /// </summary>
        public CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _Obj = new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == CONST_ADD)
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)TaiType, "");

                    _Obj.Add(Dgv_Data[1, i].Value.ToString(),                                //数据项名称 
                             Dgv_Data[2, i].Value.ToString(),                                //标识编码
                             Dgv_Data[3, i].Value.ToString(),                                //数据长度
                             Dgv_Data[4, i].Value.ToString(),                                //小数位索引 
                             Dgv_Data[5, i].Value.ToString(),                                //数据格式    
                             Dgv_Data[6, i].Value.ToString(),                                //操作类型
                             Dgv_Data[7, i].Value.ToString());                               //写入内容
                }
            }
            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }
        #endregion 

        private void Dgv_Data_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void Dgv_Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string strDataFlagName = "";
            CLDC_DataCore.Struct.StDataFlagInfo _DataFlag;
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                strDataFlagName = Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString();
                _DataFlag = _csDataFlag.GetDataFlagInfo(strDataFlagName);
                                
                ((DataGridViewCell)Dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex + 1]).Value = _DataFlag.DataFlag;
                ((DataGridViewCell)Dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex + 2]).Value = _DataFlag.DataLength;
                ((DataGridViewCell)Dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex + 3]).Value = _DataFlag.DataSmallNumber;
                ((DataGridViewCell)Dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex + 4]).Value = _DataFlag.DataFormat;                
            }
        }

        
        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using CLDC_DataCore.Struct;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_DataSendForRelay : UI_TableBase
    {

        const string CONST_ADD = "保存本项";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        
        #region ----------------构造函数------------------
        public UI_DataSendForRelay()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
        }

        public UI_DataSendForRelay(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
        }

        public UI_DataSendForRelay(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.LoadFA(faname);
        }


        public UI_DataSendForRelay(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_DataSendForRelay FAItem)
            : base(Ttype, FAItem.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
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

            CLDC_DataCore.Model.Plan.Plan_DataSendForRelay _ConnProtocol = new CLDC_DataCore.Model.Plan.Plan_DataSendForRelay((int)base.TaiType, FAName);        //打开一个方案

            this.LoadFA(_ConnProtocol);
        }

        /// <summary>
        /// 加载方案项目
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_DataSendForRelay FaItem)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = FaItem.Name;

            for (int _i = 0; _i < FaItem.Count; _i++)            //循环方案对象
            {
                StDataSendForRelay _Item = FaItem.getDataSendForRelay(_i);         //取出一个方案项目
                string zouziShortDecript = string.Empty;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Item.MeterPosition.ToString();        //表位号
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Item.BarCode;               //条形码
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Item.ItemCode.ToString();                             //标志编码
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Item.ConnProtocolItem.ToString();                             //数据项名称
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Item.WriteContent;          //发送内容
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[6]).Value = _Item.PARAMS_LIST.ToString();              //参数值
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[7]).Value = _Item.PROTOCOL;                    //通讯规约
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
        public CLDC_DataCore.Model.Plan.Plan_DataSendForRelay Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_DataSendForRelay((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_DataSendForRelay _Obj = new CLDC_DataCore.Model.Plan.Plan_DataSendForRelay((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == CONST_ADD)
                    break;
                else
                {

                    _Obj.Add(Dgv_Data[4, i].Value.ToString(),                                //数据项名称 
                             Dgv_Data[3, i].Value.ToString(),                                //标识编码
                             Dgv_Data[2, i].Value.ToString(),                                //条形码
                             Dgv_Data[1, i].Value.ToString(),                                //表位号 
                             Dgv_Data[6, i].Value.ToString(),                                //参数值    
                             Dgv_Data[5, i].Value.ToString(),                                //发送内容
                             Dgv_Data[7, i].Value.ToString());                               //通讯规约
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





 


   

 



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction
{
    public partial class FcShow : FunctionBase 
    {
        private const string CONST_ADD = "保存本项";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;

        public FcShow()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            base.SetPanel = Panel_Back;
        }

        public FcShow(CLDC_DataCore.Struct.StFunctionConfig FunctionItem)
            : base(FunctionItem)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            base.SetPanel = Panel_Back;
            this.InitPrj();
        }

        private void InitPrj()
        {
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
        public override Color PanelBackColor
        {
            set
            {
                Panel_Back.BackColor = value;
            }
        }

        public override Color CaptionColor
        {
            set
            {
                Panel_Back.CaptionColorOne = value;
            }
        }


        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_Function FaItem)
        {
            base.LoadFA(FaItem);
            this.Parm = FaItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.显示功能).ToString("000")).PrjParm;
            //FillDgv_Data();
        }

        private void FillDgv_Data()
        {

            Dgv_Data.Rows.Clear();

            for (int _i = 0; _i < _LstShow.Count; _i++)            //循环方案对象
            {
                CLDC_DataCore.Struct.StPlan_ConnProtocol _Item = _LstShow[_i];         //取出一个方案项目
                string zouziShortDecript = string.Empty;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Item.ConnProtocolItem.ToString();        //数据项名称
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Item.ItemCode;               //标识编码
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Item.DataLen.ToString();                             //数据长度
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Item.PointIndex.ToString();                             //小数位索引
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Item.StrDataType;          //数据格式
                //((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[6]).Value = _Item.OperType.ToString();              //操作类型,读/写
                //((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[7]).Value = _Item.WriteContent;                    //写入内容
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
                    if (MessageBoxEx.Show(this, "您确认要删除该方案项目么？", "删除询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
        #endregion
        #region ------------------私有方法、函数---------------
        /// <summary>
        /// 数据准确性校验
        /// </summary>
        /// <param name="RowIndex">表格行</param>
        /// <returns></returns>
        private bool CheckOK(int RowIndex)
        {
            if (Dgv_Data[1, RowIndex].Value == null || Dgv_Data[1, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请选择一项数据项名称...", "请选择", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入标识...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || Dgv_Data[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入数据长度...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[4, RowIndex].Value == null || Dgv_Data[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入小数位数...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[4, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[5, RowIndex].Value == null || Dgv_Data[5, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请输入正确的数据格式...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[5, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[6, RowIndex].Value == null || Dgv_Data[6, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "请选择一项功能...", "请选择", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[6, RowIndex].Selected = true;
                return false;
            }
            //if ((Dgv_Data[7, RowIndex].Value == null || Dgv_Data[7, RowIndex].Value.ToString().Trim() == "") && Dgv_Data[6, RowIndex].Value.ToString() == "写")
            //{
            //    MessageBoxEx.Show(this, "请输入要写的内容...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Dgv_Data[6, RowIndex].Selected = true;
            //    return false;
            //}
            //if (Dgv_Data[7, RowIndex].Value == null)
            //    Dgv_Data[7, RowIndex].Value = "";
            return true;
        }
        #endregion

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        public override string Parm
        {
            get
            {
                string _parm = "";
                for (int i = 0; i < Dgv_Data.Rows.Count - 1; i++)
                {
                    string s = "";
                    for (int j = 0; j < Dgv_Data.Columns.Count - 1; j++)
                    {
                        if (j == Dgv_Data.Columns.Count - 1)
                        {
                            s = s + Dgv_Data.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            s = s + Dgv_Data.Rows[i].Cells[j].Value.ToString() + "|";
                        }
                    }
                    if (i == Dgv_Data.Rows.Count - 2)
                    {
                        _parm = _parm + s;
                    }
                    else
                    {
                        _parm = _parm + s + ",";
                    }
                }
                return _parm;
            }
            set
            {
                setLstShow(value);
                base.Parm = value;
                FillDgv_Data();
            }
        }
        /// <summary>
        /// 显示项目列表
        /// </summary>
        private List<CLDC_DataCore.Struct.StPlan_ConnProtocol> _LstShow = new List<CLDC_DataCore.Struct.StPlan_ConnProtocol>();
        private void setLstShow(string p)
        {
            _LstShow.Clear();
            if (string.IsNullOrEmpty(p)) return;
            string[] _parms = p.Split(',');
            int _pcount = _parms.Length;
            for (int i = 0; i < _pcount; i++)
            {
                string[] pcell = _parms[i].Split('|');
                CLDC_DataCore.Struct.StPlan_ConnProtocol stcn = new CLDC_DataCore.Struct.StPlan_ConnProtocol();
                stcn.ConnProtocolItem = pcell[1];
                stcn.DataLen = int.Parse(pcell[3]);
                stcn.ItemCode = pcell[2];
                stcn.OperType = CLDC_DataCore.Struct.StMeterOperType.读;
                stcn.PointIndex = int.Parse(pcell[4]);
                stcn.StrDataType = pcell[5];
                stcn.WriteContent = "";
                stcn.PrjID = pcell[0];

                _LstShow.Add(stcn);
            }
            
        }

    }
}

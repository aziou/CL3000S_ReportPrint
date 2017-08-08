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
        private const string CONST_ADD = "���汾��";

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
            #region ---------�����˵�----------------------
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
            this.Parm = FaItem.getFunctionPrj(((int)CLDC_Comm.Enum.Cus_FunctionItem.��ʾ����).ToString("000")).PrjParm;
            //FillDgv_Data();
        }

        private void FillDgv_Data()
        {

            Dgv_Data.Rows.Clear();

            for (int _i = 0; _i < _LstShow.Count; _i++)            //ѭ����������
            {
                CLDC_DataCore.Struct.StPlan_ConnProtocol _Item = _LstShow[_i];         //ȡ��һ��������Ŀ
                string zouziShortDecript = string.Empty;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Item.ConnProtocolItem.ToString();        //����������
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Item.ItemCode;               //��ʶ����
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Item.DataLen.ToString();                             //���ݳ���
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Item.PointIndex.ToString();                             //С��λ����
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Item.StrDataType;          //���ݸ�ʽ
                //((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[6]).Value = _Item.OperType.ToString();              //��������,��/д
                //((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[7]).Value = _Item.WriteContent;                    //д������
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "ɾ��";       //ɾ����ť
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //ɾ����ťΪ��ɫ
            }

            {
                int RowIndex = Dgv_Data.Rows.Add();                 //�������һ�����У���������
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }
            this.UpDownButtonState(0);    //���������ƶ���ť״̬
        }

        #region -----------------�¼�-----------------------
        private void Dgv_Data_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dgv_Data_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgv_Data.EndEdit();
        }
        /// <summary>
        /// ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            this.UpDownButtonState(e.RowIndex);

            if (e.ColumnIndex == Dgv_Data.Columns.Count - 1)         //���һ��
            {
                if (Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString() != CONST_ADD)
                {
                    if (MessageBoxEx.Show(this, "��ȷ��Ҫɾ���÷�����Ŀô��", "ɾ��ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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

                Dgv_Data[e.ColumnIndex, e.RowIndex].Value = "ɾ��";
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
        #region ------------------˽�з���������---------------
        /// <summary>
        /// ����׼ȷ��У��
        /// </summary>
        /// <param name="RowIndex">�����</param>
        /// <returns></returns>
        private bool CheckOK(int RowIndex)
        {
            if (Dgv_Data[1, RowIndex].Value == null || Dgv_Data[1, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "��ѡ��һ������������...", "��ѡ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "�������ʶ...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || Dgv_Data[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "���������ݳ���...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[4, RowIndex].Value == null || Dgv_Data[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "������С��λ��...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[4, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[5, RowIndex].Value == null || Dgv_Data[5, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "��������ȷ�����ݸ�ʽ...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[5, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[6, RowIndex].Value == null || Dgv_Data[6, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this, "��ѡ��һ���...", "��ѡ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[6, RowIndex].Selected = true;
                return false;
            }
            //if ((Dgv_Data[7, RowIndex].Value == null || Dgv_Data[7, RowIndex].Value.ToString().Trim() == "") && Dgv_Data[6, RowIndex].Value.ToString() == "д")
            //{
            //    MessageBoxEx.Show(this, "������Ҫд������...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ��ʾ��Ŀ�б�
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
                stcn.OperType = CLDC_DataCore.Struct.StMeterOperType.��;
                stcn.PointIndex = int.Parse(pcell[4]);
                stcn.StrDataType = pcell[5];
                stcn.WriteContent = "";
                stcn.PrjID = pcell[0];

                _LstShow.Add(stcn);
            }
            
        }

    }
}

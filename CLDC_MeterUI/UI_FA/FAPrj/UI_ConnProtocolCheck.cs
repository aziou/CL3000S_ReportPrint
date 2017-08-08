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
        const string CONST_ADD = "���汾��";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;

        #region ----------------���캯��------------------

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
                    if (MessageBoxEx.Show(this,"��ȷ��Ҫɾ���÷�����Ŀô��", "ɾ��ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
        #endregion 

        #region ------------------˽�з���������---------------

        /// <summary>
        /// ��ʼ�����ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            //base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_ConnProtocol_FOLDERNAME);


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

        /// <summary>
        /// ����׼ȷ��У��
        /// </summary>
        /// <param name="RowIndex">�����</param>
        /// <returns></returns>
        private bool CheckOK(int RowIndex)
        {
            if (Dgv_Data[1, RowIndex].Value == null || Dgv_Data[1, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ��һ������������...", "��ѡ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"�������ʶ...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || Dgv_Data[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"���������ݳ���...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[4, RowIndex].Value == null || Dgv_Data[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"������С��λ��...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[4, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[5, RowIndex].Value == null || Dgv_Data[5, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��������ȷ�����ݸ�ʽ...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[5, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[6, RowIndex].Value == null || Dgv_Data[6, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ��һ���...", "��ѡ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[6, RowIndex].Selected = true;
                return false;
            }
            if ((Dgv_Data[7, RowIndex].Value == null || Dgv_Data[7, RowIndex].Value.ToString().Trim() == "") && Dgv_Data[6, RowIndex].Value.ToString() == "д")
            {
                MessageBoxEx.Show(this,"������Ҫд������...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[6, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[7, RowIndex].Value == null)
                Dgv_Data[7, RowIndex].Value = "";
            return true;
        }


        #endregion

        #region  -----------------���з���������-----------

        /// <summary>
        /// �������ƣ�ֻд�������ú��Զ����ط�����ǰ���Ǹ÷�������
        /// </summary>
        public string FAName
        {
            set
            {
                this.LoadFA(value);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FAName">��������</param>
        public void LoadFA(string FAName)
        {

            Dgv_Data.Rows.Clear();          //���������б�����

            CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _ConnProtocol = new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)base.TaiType, FAName);        //��һ������

            this.LoadFA(_ConnProtocol);
        }

        /// <summary>
        /// ���ط�����Ŀ
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck FaItem)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = FaItem.Name;

            for (int _i = 0; _i < FaItem.Count; _i++)            //ѭ����������
            {
                StPlan_ConnProtocol _Item = FaItem.getConnProtocolPrj(_i);         //ȡ��һ��������Ŀ
                string zouziShortDecript = string.Empty;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Item.ConnProtocolItem.ToString();        //����������
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Item.ItemCode;               //��ʶ����
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Item.DataLen.ToString();                             //���ݳ���
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Item.PointIndex.ToString();                             //С��λ����
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Item.StrDataType;          //���ݸ�ʽ
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[6]).Value =_Item.OperType.ToString();              //��������,��/д
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[7]).Value = _Item.WriteContent;                    //д������
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


        /// <summary>
        /// ��������
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

                    _Obj.Add(Dgv_Data[1, i].Value.ToString(),                                //���������� 
                             Dgv_Data[2, i].Value.ToString(),                                //��ʶ����
                             Dgv_Data[3, i].Value.ToString(),                                //���ݳ���
                             Dgv_Data[4, i].Value.ToString(),                                //С��λ���� 
                             Dgv_Data[5, i].Value.ToString(),                                //���ݸ�ʽ    
                             Dgv_Data[6, i].Value.ToString(),                                //��������
                             Dgv_Data[7, i].Value.ToString());                               //д������
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

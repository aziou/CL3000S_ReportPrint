using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA
{
    public partial class Frm_XieBoSetup : Office2007Form
    {

        const string CONST_TITLE = "г����������";

        private string _FaName = "";

        private CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode BeforeNode = null;

        private object BeforeNodeToCopy = null;

        private CLDC_Comm.DrawXieBo.CCLHarmonious DrawXieBoU = null;


        private CLDC_Comm.DrawXieBo.CCLHarmonious DrawXieBoI = null;


        private CLDC_Comm.Enum.Cus_PowerYuanJian YuanU = new CLDC_Comm.Enum.Cus_PowerYuanJian();

        private CLDC_Comm.Enum.Cus_PowerYuanJian YuanI = new CLDC_Comm.Enum.Cus_PowerYuanJian();


        private CLDC_DataCore.SystemModel.Item.csXieBo _XieBo;

        #region -----------���캯��------------------

        public Frm_XieBoSetup()
        {
            InitializeComponent();
            Tb_New.Click += new EventHandler(Tb_New_Click);         //��������
            this.Load += new EventHandler(Frm_XieBoSetup_Load);     //�������
            Tb_Del.Click += new EventHandler(Tb_Del_Click);         //����ɾ��    
            Tb_Close.Click += new EventHandler(Tb_Close_Click);     //����ر�
            Cmd_Cancel.Click += new EventHandler(Cmd_Cancel_Click); //ȡ������
            Tb_Save.Click += new EventHandler(Tb_Save_Click);       //�����浵
            Tb_Copy.Click += new EventHandler(Tb_Copy_Click);       //��������
            Tb_Plaster.Click += new EventHandler(Tb_Plaster_Click); //����ճ��
            Cmd_Save.Click += new EventHandler(Cmd_Save_Click);     //���������浵
            Opt_Ua.CheckedChanged += new EventHandler(Opt_U_CheckedChanged);
            Opt_Ub.CheckedChanged += new EventHandler(Opt_U_CheckedChanged);
            Opt_Uc.CheckedChanged += new EventHandler(Opt_U_CheckedChanged);
            Opt_Ia.CheckedChanged += new EventHandler(Opt_I_CheckedChanged);
            Opt_Ib.CheckedChanged += new EventHandler(Opt_I_CheckedChanged);
            Opt_Ic.CheckedChanged += new EventHandler(Opt_I_CheckedChanged);

            Cmd_AddU.Click += new EventHandler(Cmd_Add_Click);      //������Ŀ��������
            Cmd_AddI.Click += new EventHandler(Cmd_Add_Click);      //������Ŀ��������
            Cmd_RemoveU.Click += new EventHandler(Cmd_Remove_Click);    //������Ŀ����ɾ��
            Cmd_RemoveI.Click += new EventHandler(Cmd_Remove_Click);      //������Ŀ����ɾ��

            Pic_U.SizeChanged += new EventHandler(Pic_SizeChanged);
            Pic_I.SizeChanged += new EventHandler(Pic_SizeChanged);

            Tv_FaList.NodeMouseClick += new TreeNodeMouseClickEventHandler(Tv_FaList_NodeMouseClick);     //����ѡ��
            this.Text = CONST_TITLE;
            _XieBo = new CLDC_DataCore.SystemModel.Item.csXieBo();
            tableLayoutPanel1.Enabled = false;

            DrawXieBoI = new CLDC_Comm.DrawXieBo.CCLHarmonious(Pic_I);
            DrawXieBoI.Draw();
            DrawXieBoU = new CLDC_Comm.DrawXieBo.CCLHarmonious(Pic_U);
            DrawXieBoU.Draw();
        }

        private void Pic_SizeChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                DrawXieBoI.XXSelected[i] = false;
                DrawXieBoU.XXSelected[i] = false;
            }

            if (Opt_Ua.Checked)
            {
                DrawXieBoU.XXSelected[0] = true;
            }
            else if (Opt_Ub.Checked)
            {
                DrawXieBoU.XXSelected[1] = true;
            }
            else if (Opt_Uc.Checked)
            {
                DrawXieBoU.XXSelected[2] = true;
            }
            if (Opt_Ia.Checked)
            {
                DrawXieBoI.XXSelected[3] = true;
            }
            else if (Opt_Ib.Checked)
            {
                DrawXieBoI.XXSelected[4] = true;
            }
            else if (Opt_Ic.Checked)
            {
                DrawXieBoI.XXSelected[5] = true;
            }
        }



        #endregion

        #region ------------�¼�------------------


        #region ------------------����������ճ��-------------------------
        /// <summary>
        /// ����ճ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Plaster_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (this.BeforeNode == null)
            {
                MessageBoxEx.Show(this,"û��ѡ��һ��г���������޷����ճ��...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<CLDC_DataCore.Struct.StXieBo> Items = BeforeNodeToCopy as List<CLDC_DataCore.Struct.StXieBo>;//Clipboard.GetData("XieBoData") as List<CLDC_DataCore.Struct.StXieBo>;

            if (Items == null)
            {
                //Clipboard.Clear();
                return;
            }

            this.BeforeNode.Tag = Items;

            this.SetData(Items);

            MessageBoxEx.Show(this, "ճ���ɹ����뷵��...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Clipboard.Clear();
        }




        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Copy_Click(object sender, EventArgs e)
        {
            if (this.BeforeNode == null)
            {
                MessageBoxEx.Show(this, "û��ѡ��һ��г���������޷���ɸ���...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.SaveTmpDataToTag();
            BeforeNodeToCopy = this.BeforeNode.Tag;
            //Clipboard.SetData("XieBoData", this.BeforeNode.Tag);
        }

        #endregion

        #region ---------������ѡ��ȡ��----------------------
        /// <summary>
        /// ������ѡ�ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_I_CheckedChanged(object sender, EventArgs e)
        {
            if (!tableLayoutPanel1.Enabled) return;
            if (!((RadioButton)sender).Checked)
            {

                ((RadioButton)sender).Tag = this.GetXieBoPrjList((RadioButton)sender, false);
            }
            else
            {
                Gb_I.Text = string.Format("����г������({0})", ((RadioButton)sender).Text);

                if (((RadioButton)sender).Text.ToLower().IndexOf("a") >= 0)
                {
                    YuanI = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
                }
                else if (((RadioButton)sender).Text.ToLower().IndexOf("b") >= 0)
                {
                    YuanI = CLDC_Comm.Enum.Cus_PowerYuanJian.B;
                }
                else if (((RadioButton)sender).Text.ToLower().IndexOf("c") >= 0)
                {
                    YuanI = CLDC_Comm.Enum.Cus_PowerYuanJian.C;
                }
                else
                {
                    YuanI = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
                }


                if (((RadioButton)sender).Tag == null)
                {
                    this.Refresh(new List<CLDC_DataCore.Struct.StXieBo>(), false);
                }
                else
                {
                    this.Refresh(((RadioButton)sender).Tag as List<CLDC_DataCore.Struct.StXieBo>, false);
                }
            }
        }
        #endregion

        #region -----------------��ѹ��ѡ��ȡ��-----------------
        /// <summary>
        /// ��ѹ��ѡ��ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_U_CheckedChanged(object sender, EventArgs e)
        {
            if (!tableLayoutPanel1.Enabled) return;
            if (!((RadioButton)sender).Checked)
            {
                ((RadioButton)sender).Tag = this.GetXieBoPrjList((RadioButton)sender, true);
            }
            else
            {
                Gb_U.Text = string.Format("��ѹг������({0})", ((RadioButton)sender).Text);

                if (((RadioButton)sender).Text.ToLower().IndexOf("a") >= 0)
                {
                    YuanU = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
                }
                else if (((RadioButton)sender).Text.ToLower().IndexOf("b") >= 0)
                {
                    YuanU = CLDC_Comm.Enum.Cus_PowerYuanJian.B;
                }
                else if (((RadioButton)sender).Text.ToLower().IndexOf("c") >= 0)
                {
                    YuanU = CLDC_Comm.Enum.Cus_PowerYuanJian.C;
                }
                else
                {
                    YuanU = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
                }

                if (((RadioButton)sender).Tag == null)
                {
                    this.Refresh(new List<CLDC_DataCore.Struct.StXieBo>(), true);
                }
                else
                {
                    this.Refresh(((RadioButton)sender).Tag as List<CLDC_DataCore.Struct.StXieBo>, true);
                }
            }
        }
        #endregion

        #region-------------�Ƴ�һ�����г����Ŀ-------------------
        /// <summary>
        /// ɾ����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Remove_Click(object sender, EventArgs e)
        {
            DataGridView _DgvData = null;
            if (((ButtonX)sender).Name.ToLower() == "cmd_removeu")
            {
                _DgvData = Dgv_DataU;
            }
            else
            {
                _DgvData = Dgv_DataI;
            }

            if (_DgvData.SelectedRows.Count == 0)
            {
                return;
            }

            if (_DgvData.Name.ToLower() == "dgv_datau")
            {
                DrawXieBoU.UaSelected[int.Parse(_DgvData.SelectedRows[0].Cells[0].Value.ToString())] = false;
                DrawXieBoU.UaValue[int.Parse(_DgvData.SelectedRows[0].Cells[0].Value.ToString())] = 0;
                DrawXieBoU.UaPhase[int.Parse(_DgvData.SelectedRows[0].Cells[0].Value.ToString())] = 0;
                DrawXieBoU.Draw();
            }
            else
            {
                DrawXieBoI.UaSelected[int.Parse(_DgvData.SelectedRows[0].Cells[0].Value.ToString())] = false;
                DrawXieBoI.UaValue[int.Parse(_DgvData.SelectedRows[0].Cells[0].Value.ToString())] = 0;
                DrawXieBoI.UaPhase[int.Parse(_DgvData.SelectedRows[0].Cells[0].Value.ToString())] = 0;
                DrawXieBoI.Draw();
            }



            _DgvData.Rows.RemoveAt(_DgvData.SelectedRows[0].Index);


        }
        #endregion


        #region----------����µ�г������Ŀ------------
        /// <summary>
        /// �����г����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Add_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            TextBox _TxtCs = null;
            TextBox _TxtFd = null;
            TextBox _TxtXw = null;
            DataGridView _DgwData = null;

            if (((ButtonX)sender).Name.ToLower() == "cmd_addu")
            {
                if (Gb_U.Text == "��ѹг������")
                {
                    MessageBoxEx.Show(this, "��ѡ����Ҫ���õĵ�ѹ��...", "���Ӵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Opt_Ua.Checked = true;
                    return;
                }
                _TxtCs = Txt_CsU;
                _TxtFd = Txt_FdU;
                _TxtXw = Txt_XwU;
                _DgwData = Dgv_DataU;
            }
            else
            {
                if (Gb_I.Text == "����г������")
                {
                    MessageBoxEx.Show(this, "��ѡ����Ҫ���õĵ�����...", "���Ӵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Opt_Ia.Checked = true;
                    return;
                }
                _TxtCs = Txt_CsI;
                _TxtFd = Txt_FdI;
                _TxtXw = Txt_XwI;
                _DgwData = Dgv_DataI;
            }

            #region ----------------���ݺϷ�����֤------------------------

            if (!CLDC_DataCore.Function.Number.IsIntNumber(_TxtCs.Text) || int.Parse(_TxtCs.Text) < 2)
            {
                MessageBoxEx.Show(this, "���������������Ϊһ������,��ֻ�ܴ�2��г����ʼ����...", "��Ӵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TxtCs.Focus();
                _TxtCs.SelectAll();
                return;
            }
            for (int i = 0; i < _DgwData.Rows.Count; i++)
            {
                if (_TxtCs.Text == _DgwData.Rows[i].Cells[0].Value.ToString())
                {
                    MessageBoxEx.Show(this, "��������������ܺ͵�ǰ��Ŀ�б��еĴ����ظ�...", "��Ӵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _TxtCs.Focus();
                    _TxtCs.SelectAll();
                    return;
                }
            }

            if (!CLDC_DataCore.Function.Number.IsNumeric(_TxtFd.Text) || float.Parse(_TxtFd.Text) < 0 || float.Parse(_TxtFd.Text) > 40)
            {
                MessageBoxEx.Show(this, "�������г�����ȱ���Ǹ��в�����40������...", "��Ӵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TxtFd.Focus();
                _TxtFd.SelectAll();
                return;
            }

            if (!CLDC_DataCore.Function.Number.IsNumeric(_TxtXw.Text))
            {
                MessageBoxEx.Show(this, "�������г����λ����Ϊһ������...", "��Ӵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TxtXw.Focus();
                _TxtXw.SelectAll();
                return;
            }
            #endregion

            int RowIndex = _DgwData.Rows.Add();
            _DgwData.Rows[RowIndex].Cells[0].Value = _TxtCs.Text;
            _DgwData.Rows[RowIndex].Cells[1].Value = _TxtFd.Text;
            _DgwData.Rows[RowIndex].Cells[2].Value = _TxtXw.Text;



            if (_DgwData.Name.ToLower() == "dgv_datau")
            {

                this.DrawXieBo(true, YuanU, _DgwData);
            }
            else
            {
                this.DrawXieBo(false, YuanI, _DgwData);
            }
        }

        #endregion


        #region-------------��״�б���ѡ�񷽰���Ŀ---------------------
        /// <summary>
        /// ������Ŀѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.BeforeNode != null && e.Node.FullPath == this.BeforeNode.FullPath) return;

            if (this.BeforeNode != null)    //�����ǰѡ��֮ǰ��ѡ�еĽڵ㣬����Ҫ��һ��Tag��¼
            {
                this.SaveTmpDataToTag();
            }

            this.BeforeNode = e.Node as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;       //���浱ǰ�ڵ�

            if (e.Node.Text != "�½�...")
            {
                this._FaName = e.Node.Text;
            }
            else
            {
                this._FaName = "";
            }

            tableLayoutPanel1.Enabled = true;

            if (e.Node.Tag is List<CLDC_DataCore.Struct.StXieBo>)
            {
                this.SetData(e.Node.Tag as List<CLDC_DataCore.Struct.StXieBo>);
            }
            else
            {
                this.SetData(_XieBo.getXieBoFa(e.Node.Text));
            }


        }

        #endregion

        #region --------------�����������浵��ȡ����ɾ��------------------
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_New_Click(object sender, EventArgs e)
        {
            this._FaName = "";
            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�½�...");
            if (Tv_FaList.Nodes.Count == 0)
            {
                Tv_FaList.Nodes.Add(_Node);
            }
            else
            {
                Tv_FaList.Nodes.Insert(0, _Node);
            }

            _Node.Checked = true;
        }

        /// <summary>
        /// �����浵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (this._FaName != "")        //����������Ʋ�Ϊ�գ���ֱ�ӱ���
            {
                if (Tv_FaList.SelectedNode == null)
                {
                    MessageBoxEx.Show(this, "��ѡ����Ҫ����ķ���...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.SaveTmpDataToTag();

                CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node;

                _Node = this.Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;

                if (_Node.Tag == null)
                {
                    MessageBoxEx.Show(this, "�÷�����û���κ���Ŀ���޷���ɱ���...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                List<CLDC_DataCore.Struct.StXieBo> Items = _Node.Tag as List<CLDC_DataCore.Struct.StXieBo>;

                if (Items.Count == 0)
                {
                    MessageBoxEx.Show(this, "��ǰ������û��ʵ��г�����ã��޷���ɱ��棬����...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                _XieBo.Save(this._FaName, Items);

                MessageBoxEx.Show(this, "����Ϊ��" + _FaName + "�ķ�������ɹ�.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _Node.Text = this._FaName;
            }
            else
            {
                Txt_FaName.Text = "";
                this.LockControl(true);
            }

        }
        /// <summary>
        /// ���������浵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Txt_FaName.Text == "")
            {
                MessageBoxEx.Show(this, "����д��Ҫ����ķ�������...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Txt_FaName.Text == "�½�...")
            {
                MessageBoxEx.Show(this, "����ʹ�ùؼ��֡��½�...����Ϊ��������...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Txt_FaName.SelectionStart = 0;
                Txt_FaName.SelectionLength = Txt_FaName.Text.Length;
                Txt_FaName.Focus();
                return;
            }

            this._FaName = Txt_FaName.Text;

            this.LockControl(false);

            this.Tb_Save_Click(sender, e);

        }

        /// <summary>
        /// ȡ�����������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.LockControl(false); ;
        }

        /// <summary>
        /// г������ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Del_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this, "û��ѡ�񷽰���Ŀ���������ɾ������...", "����ɾ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node;

            _Node = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;

            DialogResult _Result = MessageBoxEx.Show(this, "ȷ��Ҫɾ����������Ϊ:" + _Node.Text +
                                                    "�ķ�����\n������ǡ���ɾ���÷�����\n�����ȡ������ȡ��ɾ������.", "����ɾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_Result == DialogResult.Yes)
            {
                _XieBo.Remove(_Node.Text);
                Tv_FaList.Nodes.Remove(_Node);
                this.BeforeNode = null;
                tableLayoutPanel1.Enabled = false;

                this.ClearControlData();

                if (Tv_FaList.Nodes.Count > 0)
                {
                    this.Tv_FaList_NodeMouseClick(Tv_FaList, new TreeNodeMouseClickEventArgs(Tv_FaList.Nodes[0], MouseButtons.Left, 1, 0, 0));
                }

            }

        }

        #endregion

        #region----------�����ʼ��----------------------
        /// <summary>
        /// ��������¼�����ʼ��г�������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_XieBoSetup_Load(object sender, EventArgs e)
        {
            _XieBo.Load();
            for (int i = 0; i < _XieBo.FaNameList.Count; i++)
            {
                Tv_FaList.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(_XieBo.FaNameList[i]));
            }

            if (Tv_FaList.Nodes.Count > 0)
            {
                Tv_FaList.Nodes[0].Checked = true;
                this.Tv_FaList_NodeMouseClick(Tv_FaList, new TreeNodeMouseClickEventArgs(Tv_FaList.Nodes[0], MouseButtons.Left, 1, 0, 0));
            }

        }
        #endregion


        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Close_Click(object sender, EventArgs e)
        {
            //if (MessageBoxEx.Show(this,"�ر�ǰ��ȷ���Ѿ���������Ҫ�洢�ķ�����Ϣ\n�����ȷ�ϡ��رմ��壬�����ȡ�����˳��ر�", "�ر���ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            //{
            //    return;
            //}
            this.Close();
        }


        #endregion

        #region -----------˽�з���������-----------

        /// <summary>
        /// ���ݳ�ʼ������
        /// </summary>
        /// <param name="Items"></param>
        private void SetData(List<CLDC_DataCore.Struct.StXieBo> Items)
        {
            List<CLDC_DataCore.Struct.StXieBo> Ua = new List<CLDC_DataCore.Struct.StXieBo>();          //�ֱ���������������ѹ�����ݼ���
            List<CLDC_DataCore.Struct.StXieBo> Ub = new List<CLDC_DataCore.Struct.StXieBo>();
            List<CLDC_DataCore.Struct.StXieBo> Uc = new List<CLDC_DataCore.Struct.StXieBo>();
            List<CLDC_DataCore.Struct.StXieBo> Ia = new List<CLDC_DataCore.Struct.StXieBo>();
            List<CLDC_DataCore.Struct.StXieBo> Ib = new List<CLDC_DataCore.Struct.StXieBo>();
            List<CLDC_DataCore.Struct.StXieBo> Ic = new List<CLDC_DataCore.Struct.StXieBo>();

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].YuanJian == CLDC_Comm.Enum.Cus_PowerYuanJian.A)        //�����AԪ
                {
                    if (Items[i].IsUb)          //����ǵ�ѹ
                    {
                        Ua.Add(Items[i]);
                    }
                    else
                    {
                        Ia.Add(Items[i]);
                    }
                }
                else if (Items[i].YuanJian == CLDC_Comm.Enum.Cus_PowerYuanJian.B)       //�����BԪ
                {
                    if (Items[i].IsUb)
                    {
                        Ub.Add(Items[i]);
                    }
                    else
                    {
                        Ib.Add(Items[i]);
                    }
                }
                else if (Items[i].YuanJian == CLDC_Comm.Enum.Cus_PowerYuanJian.C)       //�����CԪ
                {
                    if (Items[i].IsUb)
                    {
                        Uc.Add(Items[i]);
                    }
                    else
                    {
                        Ic.Add(Items[i]);
                    }
                }
            }
            Opt_Ua.Checked = false;
            Opt_Ub.Checked = false;
            Opt_Uc.Checked = false;
            Opt_Ia.Checked = false;
            Opt_Ib.Checked = false;
            Opt_Ic.Checked = false;


            Opt_Ua.Tag = Ua;
            Opt_Ub.Tag = Ub;
            Opt_Uc.Tag = Uc;
            Opt_Ia.Tag = Ia;
            Opt_Ib.Tag = Ib;
            Opt_Ic.Tag = Ic;

            Opt_Ua.Checked = true;
            Opt_Ia.Checked = true;

        }


        /// <summary>
        /// ��ȡ��Ŀ�б�����ʱ�洢��
        /// </summary>
        /// <param name="IsUb"></param>
        /// <returns></returns>
        private List<CLDC_DataCore.Struct.StXieBo> GetXieBoPrjList(RadioButton ControlItem, bool IsUb)
        {
            List<CLDC_DataCore.Struct.StXieBo> Items = new List<CLDC_DataCore.Struct.StXieBo>();

            CLDC_Comm.Enum.Cus_PowerYuanJian _Yuan = new CLDC_Comm.Enum.Cus_PowerYuanJian();


            if (ControlItem.Name.ToLower().IndexOf("a") >= 0)
            {
                _Yuan = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
            }
            else if (ControlItem.Name.ToLower().IndexOf("b") >= 0)
            {
                _Yuan = CLDC_Comm.Enum.Cus_PowerYuanJian.B;
            }
            else if (ControlItem.Name.ToLower().IndexOf("c") >= 0)
            {
                _Yuan = CLDC_Comm.Enum.Cus_PowerYuanJian.C;
            }
            else
            {
                return Items;
            }

            DataGridView _DgvData = IsUb ? Dgv_DataU : Dgv_DataI;

            for (int i = 0; i < _DgvData.Rows.Count; i++)
            {
                CLDC_DataCore.Struct.StXieBo Item = new CLDC_DataCore.Struct.StXieBo();
                Item.IsUb = IsUb;                                           //�ǲ��ǵ�ѹ
                Item.Num = int.Parse(_DgvData[0, i].Value.ToString());        //г������
                Item.YuanJian = _Yuan;                                        //��Ԫ
                Item.Extent = float.Parse(_DgvData[1, i].Value.ToString());   //����    
                Item.Xw = float.Parse(_DgvData[2, i].Value.ToString());       //��λ    
                Items.Add(Item);
            }

            return Items;
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        /// <param name="Items"></param>
        private void Refresh(List<CLDC_DataCore.Struct.StXieBo> Items, bool IsUb)
        {
            DataGridView _DgvData = null;

            if (IsUb)
            {
                _DgvData = Dgv_DataU;
            }
            else
            {
                _DgvData = Dgv_DataI;
            }

            _DgvData.Rows.Clear();

            for (int i = 0; i < 6; i++)
            {
                DrawXieBoU.XXSelected[i] = false;
                DrawXieBoI.XXSelected[i] = false;
            }

            this.Pic_SizeChanged(new object(), new EventArgs());        //��������һ��г��ģ��������Ҫ��ʾ����Ԫ

            for (int i = 0; i < Items.Count; i++)
            {
                int RowIndex = _DgvData.Rows.Add();
                _DgvData.Rows[RowIndex].Cells[0].Value = Items[i].Num;
                _DgvData.Rows[RowIndex].Cells[1].Value = Items[i].Extent;
                _DgvData.Rows[RowIndex].Cells[2].Value = Items[i].Xw;
            }

            this.DrawXieBo(IsUb, IsUb ? YuanU : YuanI, _DgvData);
        }


        /// <summary>
        /// ��г��ͼ��
        /// </summary>
        /// <param name="IsUb">�Ƿ��ǵ�ѹ</param>
        /// <param name="Yuan">Ԫ��</param>
        /// <param name="Grid">���ݱ�</param>
        private void DrawXieBo(bool IsUb, CLDC_Comm.Enum.Cus_PowerYuanJian Yuan, DataGridView Grid)
        {
            CLDC_Comm.DrawXieBo.CCLHarmonious _Draw = null;

            if (IsUb)
            {
                _Draw = DrawXieBoU;
            }
            else
            {
                _Draw = DrawXieBoI;
            }

            _Draw.ClearXieBo();

            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                if (_Draw.MaxTimes < int.Parse(Grid.Rows[i].Cells[0].Value.ToString()))
                {
                    _Draw.MaxTimes = int.Parse(Grid.Rows[i].Cells[0].Value.ToString());
                }
            }

            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                switch (Yuan)
                {
                    case CLDC_Comm.Enum.Cus_PowerYuanJian.A:
                        if (IsUb)
                        {
                            _Draw.UaPhase[0] = 0;
                            _Draw.UaValue[0] = 100;
                            _Draw.UaSelected[0] = true;
                            _Draw.UaPhase[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[2, 0].Value.ToString());
                            _Draw.UaValue[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[1, 0].Value.ToString()) / 100f;
                            _Draw.UaSelected[int.Parse(Grid[0, i].Value.ToString()) - 1] = true;
                        }
                        else
                        {
                            _Draw.IaPhase[0] = 0;
                            _Draw.IaValue[0] = 100;
                            _Draw.IaSelected[0] = true;

                            _Draw.IaPhase[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[2, 0].Value.ToString());
                            _Draw.IaValue[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[1, 0].Value.ToString()) / 100f;
                            _Draw.IaSelected[int.Parse(Grid[0, i].Value.ToString()) - 1] = true;
                        }
                        break;
                    case CLDC_Comm.Enum.Cus_PowerYuanJian.B:
                        if (IsUb)
                        {
                            _Draw.UbPhase[0] = 0;
                            _Draw.UbValue[0] = 100;
                            _Draw.UbSelected[0] = true;

                            _Draw.UbPhase[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[2, 0].Value.ToString());
                            _Draw.UbValue[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[1, 0].Value.ToString()) / 100f;
                            _Draw.UbSelected[int.Parse(Grid[0, i].Value.ToString()) - 1] = true;
                        }
                        else
                        {
                            _Draw.IbPhase[0] = 0;
                            _Draw.IbValue[0] = 100;
                            _Draw.IbSelected[0] = true;

                            _Draw.IbPhase[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[2, 0].Value.ToString());
                            _Draw.IbValue[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[1, 0].Value.ToString()) / 100f;
                            _Draw.IbSelected[int.Parse(Grid[0, i].Value.ToString()) - 1] = true;
                        }
                        break;
                    case CLDC_Comm.Enum.Cus_PowerYuanJian.C:
                        if (IsUb)
                        {
                            _Draw.UcPhase[0] = 0;
                            _Draw.UcValue[0] = 100;
                            _Draw.UcSelected[0] = true;

                            _Draw.UcPhase[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[2, 0].Value.ToString());
                            _Draw.UcValue[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[1, 0].Value.ToString()) / 100f;
                            _Draw.UcSelected[int.Parse(Grid[0, i].Value.ToString()) - 1] = true;
                        }
                        else
                        {
                            _Draw.IcPhase[0] = 0;
                            _Draw.IcValue[0] = 100;
                            _Draw.IcSelected[0] = true;

                            _Draw.IcPhase[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[2, 0].Value.ToString());
                            _Draw.IcValue[int.Parse(Grid[0, i].Value.ToString()) - 1] = float.Parse(Grid[1, 0].Value.ToString()) / 100f;
                            _Draw.IcSelected[int.Parse(Grid[0, i].Value.ToString()) - 1] = true;
                        }
                        break;
                }
            }

            _Draw.Draw();

        }

        /// <summary>
        /// ��ʱ�浵
        /// </summary>
        private void SaveTmpDataToTag()
        {
            this.RefreahRadioButton(Opt_Ua, true);
            this.RefreahRadioButton(Opt_Ub, true);
            this.RefreahRadioButton(Opt_Uc, true);
            this.RefreahRadioButton(Opt_Ia, false);
            this.RefreahRadioButton(Opt_Ib, false);
            this.RefreahRadioButton(Opt_Ic, false);

            List<CLDC_DataCore.Struct.StXieBo> Items = new List<CLDC_DataCore.Struct.StXieBo>();

            this.UnionPrj(ref Items, Opt_Ua);
            this.UnionPrj(ref Items, Opt_Ub);
            this.UnionPrj(ref Items, Opt_Uc);
            this.UnionPrj(ref Items, Opt_Ia);
            this.UnionPrj(ref Items, Opt_Ib);
            this.UnionPrj(ref Items, Opt_Ic);

            this.BeforeNode.Tag = Items;

        }


        private void UnionPrj(ref List<CLDC_DataCore.Struct.StXieBo> Items, RadioButton ControlItem)
        {
            List<CLDC_DataCore.Struct.StXieBo> TmpItems = new List<CLDC_DataCore.Struct.StXieBo>();

            if (ControlItem.Tag is List<CLDC_DataCore.Struct.StXieBo>)
            {
                TmpItems = ControlItem.Tag as List<CLDC_DataCore.Struct.StXieBo>;
            }

            for (int i = 0; i < TmpItems.Count; i++)
            {
                Items.Add(TmpItems[i]);
            }

        }


        private void RefreahRadioButton(RadioButton Item, bool IsUb)
        {
            if (Item.Checked)
            {
                Item.Tag = this.GetXieBoPrjList(Item, IsUb);
            }
        }


        /// <summary>
        /// ����ʾ��д�������Ƶı�������ʱ����Ҫ��ס����
        /// </summary>
        /// <param name="Save"></param>
        private void LockControl(bool Save)
        {
            if (Save)
            {
                Panel_Save.Visible = true;

                Tool_FA.Enabled = false;
                this.Tlp_Ole.Enabled = false;
            }
            else
            {
                Panel_Save.Visible = false;
                Tool_FA.Enabled = true;
                this.Tlp_Ole.Enabled = true;
            }
        }

        private void ClearControlData()
        {
            Opt_Ua.Checked = false;
            Opt_Ub.Checked = false;
            Opt_Uc.Checked = false;
            Opt_Ia.Checked = false;
            Opt_Ib.Checked = false;
            Opt_Ic.Checked = false;
            Opt_Ua.Tag = null;
            Opt_Ub.Tag = null;
            Opt_Uc.Tag = null;
            Opt_Ia.Tag = null;
            Opt_Ib.Tag = null;
            Opt_Ic.Tag = null;

            Dgv_DataI.Rows.Clear();
            Dgv_DataU.Rows.Clear();
        }

        #endregion
    }
}
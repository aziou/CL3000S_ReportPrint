using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using CLDC_Comm.Enum;


namespace CLDC_MeterUI.UI_FA
{
    public partial class Frm_FaSetup :Office2007Form
    {

        const string CONST_TITLESTRING = "��½����̨����������";
        const string CONST_HENG = "-----------------";
        private CLDC_Comm.Enum.Cus_TaiType _Ttype = new CLDC_Comm.Enum.Cus_TaiType();

        private List<object> LstCopyFa;

        private string RefreahLastString = "";
        /// <summary>
        /// ���һ��ѡ��ķ�����
        /// </summary>
        private string LastFAName;

        /// <summary>
        /// ��������
        /// </summary>
        private string _FaName = "";

        /// <summary>
        /// �޸ĵķ�������
        /// </summary>
        public string newFAName = "";

        /// <summary>
        /// ��ǰѡ��֮ǰ��ѡ�еĽڵ�
        /// </summary>
        private CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode BeforeNode = null;

        #region ------------���캯��-----------------

        public Frm_FaSetup()
        {
            InitializeComponent();
            this.Text = CONST_TITLESTRING;
        }

        public Frm_FaSetup(CLDC_Comm.Enum.Cus_TaiType Ttype)
        {
            _Ttype = Ttype;
            InitializeComponent();
            this.Text = string.Format("{0}({1})", CONST_TITLESTRING, Ttype.ToString());

        }

        #endregion

        #region ----------------------�¼�-----------------------

        #region ---------------��������------------------
        private void Tb_New_Click(object sender, EventArgs e)
        {
            this.SetTitleText("�½�");
            this._FaName = "";
            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�½�...");
            _Node.ContextMenuStrip = contextMenuStrip4Rename; 
            if (Tv_FaList.Nodes.Count == 0)
            {
                Tv_FaList.Nodes.Add(_Node);
            }
            else
            {
                Tv_FaList.Nodes.Insert(0, _Node);
            }
            _Node.State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked;

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ԥ�ȵ���", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ԥ������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��ۼ������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��Ƶ��ѹ����", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ǳ������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�����������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("ͨѶЭ��������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�๦������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ӱ��������", 1, 2));

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�ز�����", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���һ����", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��������", 1, 2));

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�ѿع�������", 1, 2));

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���ܱ�������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���Ṧ������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�¼���¼����", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("����ת������", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�������ݱȶ�����", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���ɼ�¼����", 1, 2));
            //����˵�
            foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode node in _Node.Nodes)
                node.ContextMenuStrip = contextMenuSort;
        }
        #endregion

        #region -----------------������Ŀѡ��-------------------
        /// <summary>
        /// ������Ŀѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (BeforeNode != null && BeforeNode.FullPath == e.Node.FullPath) return;

            //����Ҽ�����ǽ�ѡ�еĽڵ㸳����ǰ�ڵ�
            if (e.Button == MouseButtons.Right)
            {
                Tv_FaList.SelectedNode = e.Node;
            }

            for (int i = 0; i < Tv_FaList.Nodes.Count; i++)
            {
                ((CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode)Tv_FaList.Nodes[i]).State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;
            }


            if (e.Node.Parent == null)          //���������Ǹ��ڵ㣬���˳�
            {
                this._FaName = e.Node.Text != "�½�..." ? e.Node.Text : "";
                ((CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode)e.Node).State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked;
            }
            else
            {
                this._FaName = e.Node.Parent.Text != "�½�..." ? e.Node.Parent.Text : "";
                ((CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode)e.Node.Parent).State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked;
            }

            this.SetTitleText(this._FaName == "" ? "�½�" : this._FaName);

            if (this.BeforeNode != null)       //�����ǰѡ��֮ǰ��ѡ�еĽڵ㣬����Ҫ��һ��Tag��¼
            {
                this.SaveTmpDataToTag();            //���浽Tag
            }

            this.BeforeNode = e.Node as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;           //���浱ǰ�ڵ�

            UserControl _Control = null;
            #region ---------------------Ԥ�ȵ���-----------------------------
            if (e.Node.Text == "Ԥ�ȵ���")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_PrepareTest)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������е�����������ǰ�����������У�������Ҫ����һ��Tag
                        {
                            ((FAPrj.UI_PrepareTest)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_PrepareTest);
                        }
                        else
                        {
                            ((FAPrj.UI_PrepareTest)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_PrepareTest(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_PrepareTest);
                }
                else
                {
                    _Control = new FAPrj.UI_PrepareTest(_Ttype, _FaName);
                }
            }
            #endregion
            #region ---------------------Ԥ������-----------------------------
            else if (e.Node.Text == "Ԥ������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_YuRe)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������е�����������ǰ�����������У�������Ҫ����һ��Tag
                        {
                            ((FAPrj.UI_YuRe)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_YuRe);
                        }
                        else
                        {
                            ((FAPrj.UI_YuRe)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_YuRe(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_YuRe);
                }
                else
                {
                    _Control = new FAPrj.UI_YuRe(_Ttype, _FaName);
                }
            }
            #endregion
            #region ---------------------��ۼ������-----------------------------
            else if (e.Node.Text == "��ۼ������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_WGJC)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������е�����������ǰ�����������У�������Ҫ����һ��Tag
                        {
                            
                        }
                        else
                        {
                            
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_WGJC();
                }
                else
                {
                    _Control = new FAPrj.UI_WGJC();
                }
            }
            #endregion
            #region----------------------������------------------
            else if (e.Node.Text == "������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_QiDong)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_QiDong)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_QiDong);
                        }
                        else
                        {
                            ((FAPrj.UI_QiDong)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_QiDong(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_QiDong);
                }
                else
                {
                    _Control = new FAPrj.UI_QiDong(_Ttype, _FaName);
                }

            }
            #endregion

            #region---------------------Ǳ������-------------------------
            else if (e.Node.Text == "Ǳ������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_QianDong)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_QianDong)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_QianDong);
                        }
                        else
                        {
                            ((FAPrj.UI_QianDong)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_QianDong(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_QianDong);
                }
                else
                {
                    _Control = new FAPrj.UI_QianDong(_Ttype, _FaName);
                }

            }
            #endregion
            #region -------------------�����������--------------------
            else if (e.Node.Text == "�����������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Error)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_Error)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint);
                        }
                        else
                        {
                            ((FAPrj.UI_Error)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Error(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint);
                }
                else
                {
                    _Control = new FAPrj.UI_Error(_Ttype, _FaName);
                }
            }
            #endregion
            #region -------------------����ʵ��--------------------
            else if (e.Node.Text == "��������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_ZouZi)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_ZouZi)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_ZouZi);
                        }
                        else
                        {
                            ((FAPrj.UI_ZouZi)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_ZouZi(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_ZouZi);
                }
                else
                {
                    _Control = new FAPrj.UI_ZouZi(_Ttype, _FaName);
                }
            }
            #endregion
            #region -------------------�๦������--------------------
            else if (e.Node.Text == "�๦������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Dgn)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_Dgn)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Dgn);
                        }
                        else
                        {
                            ((FAPrj.UI_Dgn)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Dgn(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Dgn);
                }
                else
                {
                    _Control = new FAPrj.UI_Dgn(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------ͨѶЭ��������--------------------
            else if (e.Node.Text == "ͨѶЭ��������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_ConnProtocolCheck)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_ConnProtocolCheck)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck);
                        }
                        else
                        {
                            ((FAPrj.UI_ConnProtocolCheck)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_ConnProtocolCheck(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck);
                }
                else
                {
                    _Control = new FAPrj.UI_ConnProtocolCheck(_Ttype, _FaName);
                }
            }
            #endregion


            #region -------------------����ת������--------------------
            else if (e.Node.Text == "����ת������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_DataSendForRelay)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_DataSendForRelay)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_DataSendForRelay);
                        }
                        else
                        {
                            ((FAPrj.UI_DataSendForRelay)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_DataSendForRelay(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_DataSendForRelay);
                }
                else
                {
                    _Control = new FAPrj.UI_DataSendForRelay(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------����춨--------------------
            else if (e.Node.Text == "Ӱ��������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Special)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_Special)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Specal);
                        }
                        else
                        {
                            ((FAPrj.UI_Special)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Special(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Specal);
                }
                else
                {
                    _Control = new FAPrj.UI_Special(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------�ز�����--------------------
            else if (e.Node.Text == "�ز�����")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Carrier)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������е��ز�������ǰ�������ز��У�������Ҫ����һ��Tag
                        {
                            ((FAPrj.UI_Carrier)Panel_Control.Controls[0]).LoadPlan(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Carrier);
                        }
                        else
                        {
                            ((FAPrj.UI_Carrier)Panel_Control.Controls[0]).LoadPlan(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Carrier(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Carrier);
                }
                else
                {
                    _Control = new FAPrj.UI_Carrier(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------�������ݱȶ�����--------------------
            else if (e.Node.Text == "�������ݱȶ�����")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Infrared)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������е��ز�������ǰ�������ز��У�������Ҫ����һ��Tag
                        {
                            ((FAPrj.UI_Infrared)Panel_Control.Controls[0]).LoadPlan(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Infrared);
                        }
                        else
                        {
                            ((FAPrj.UI_Infrared)Panel_Control.Controls[0]).LoadPlan(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Infrared(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Infrared);
                }
                else
                {
                    _Control = new FAPrj.UI_Infrared(_Ttype, _FaName);
                }
            }
            #endregion
            #region -------------------���һ����--------------------
            else if (e.Node.Text == "���һ����")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_ErrAccord)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������е����һ����������ǰ������������Ҫ����һ��Tag
                        {
                            ((FAPrj.UI_ErrAccord)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_ErrAccord);
                        }
                        else
                        {
                            ((FAPrj.UI_ErrAccord)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_ErrAccord(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_ErrAccord);
                }
                else
                {
                    _Control = new FAPrj.UI_ErrAccord(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------��������--------------------
            else if (e.Node.Text == "��������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_PowerConsume)
                    {
                        if (e.Node.Tag != null)     //���ʱ���п����Ǵ����������еĹ���������ǰ�����Ĺ����У�������Ҫ����һ��Tag
                        {
                            ((FAPrj.UI_PowerConsume)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_PowerConsume);
                        }
                        else
                        {
                            ((FAPrj.UI_PowerConsume)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_PowerConsume(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_PowerConsume);
                }
                else
                {
                    _Control = new FAPrj.UI_PowerConsume(_Ttype, _FaName);
                }
            }
            #endregion


            #region -------------------���ܱ�������--------------------
            else if (e.Node.Text == "���ܱ�������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Function)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_Function)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Function);
                        }
                        else
                        {
                            ((FAPrj.UI_Function)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Function(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Function);
                }
                else
                {
                    _Control = new FAPrj.UI_Function(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------�ѿع�������--------------------
            else if (e.Node.Text == "�ѿع�������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_CostControl)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_CostControl)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_CostControl);
                        }
                        else
                        {
                            ((FAPrj.UI_CostControl)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_CostControl(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_CostControl);
                }
                else
                {
                    _Control = new FAPrj.UI_CostControl(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------�¼���¼����--------------------
            else if (e.Node.Text == "�¼���¼����")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_EventLog)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_EventLog)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_EventLog);
                        }
                        else
                        {
                            ((FAPrj.UI_EventLog)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_EventLog(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_EventLog);
                }
                else
                {
                    _Control = new FAPrj.UI_EventLog(_Ttype, _FaName);
                }
            }
            #endregion

            #region -------------------���Ṧ������--------------------
            else if (e.Node.Text == "���Ṧ������")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Freeze)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_Freeze)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Freeze);
                        }
                        else
                        {
                            ((FAPrj.UI_Freeze)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Freeze(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Freeze);
                }
                else
                {
                    _Control = new FAPrj.UI_Freeze(_Ttype, _FaName);
                }
            }
            #endregion

            #region ��Ƶ��ѹ����
            else if (e.Node.Text == "��Ƶ��ѹ����")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Insulation)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_Insulation)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Insulation);
                        }
                        else
                        {
                            ((FAPrj.UI_Insulation)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_Insulation(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_Insulation);
                }
                else
                {
                    _Control = new FAPrj.UI_Insulation(_Ttype, _FaName);
                }
            }
            #endregion ��Ƶ��ѹ����


            #region -------------------���ɼ�¼����--------------------
            else if (e.Node.Text == "���ɼ�¼����")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_LoadRecord)
                    {
                        if (e.Node.Tag != null)
                        {
                            ((FAPrj.UI_LoadRecord)Panel_Control.Controls[0]).LoadFA(e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_LoadRecord);
                        }
                        else
                        {
                            ((FAPrj.UI_LoadRecord)Panel_Control.Controls[0]).LoadFA(_FaName);
                        }
                        return;
                    }
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }
                if (e.Node.Tag != null)
                {
                    _Control = new FAPrj.UI_LoadRecord(_Ttype, e.Node.Tag as CLDC_DataCore.Model.Plan.Plan_LoadRecord);
                }
                else
                {
                    _Control = new FAPrj.UI_LoadRecord(_Ttype, _FaName);
                }
            }
            #endregion

            else
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    Panel_Control.Controls[0].Dispose();
                    Panel_Control.Controls.Clear();
                }

            }
            if (_Control != null)
            {
                Panel_Control.Controls.Add(_Control);
                _Control.Margin = new System.Windows.Forms.Padding(0);
                _Control.Dock = DockStyle.Fill;
            }

        }

        #endregion

        #region ------------------------������ճ�����------------------------
        /// <summary>
        /// ����ճ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Plaster_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (LstCopyFa == null) return;

            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"��ѡ����Ҫ�������ܷ����������ӷ����ڵ�...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

            if (Tv_FaList.SelectedNode.Parent == null) //����Ǹ���ڵ�
            {
                _FatherNode = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }
            else
            {
                MessageBoxEx.Show(this,"ճ��ʱ����ѡ�����ڵ�,�����޷����ճ������,���ȷ�ϣ����²���...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (int i = 0; i < LstCopyFa.Count; i++)
            {
                string TmpNodeString = "";
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_PrepareTest)
                {
                    TmpNodeString = "Ԥ�ȵ���";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_YuRe)
                {
                    TmpNodeString = "Ԥ������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_WGJC)
                {
                    TmpNodeString = "��ۼ������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_QiDong)
                {
                    TmpNodeString = "������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_QianDong)
                {
                    TmpNodeString = "Ǳ������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_WcPoint)
                {
                    TmpNodeString = "�����������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_ZouZi)
                {
                    TmpNodeString = "��������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Dgn)
                {
                    TmpNodeString = "�๦������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck)
                {
                    TmpNodeString = "ͨѶЭ��������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Specal)
                {
                    TmpNodeString = "Ӱ��������";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Carrier)
                {
                    TmpNodeString = "�ز�����";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Infrared)
                {
                    TmpNodeString = "�������ݱȶ�����";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_ErrAccord)
                {
                    TmpNodeString = "���һ����";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_PowerConsume)
                {
                    TmpNodeString = "��������";
                }


                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Freeze)
                {
                    TmpNodeString = "���Ṧ������";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_EventLog)
                {
                    TmpNodeString = "�¼���¼����";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Function)
                {
                    TmpNodeString = "���ܱ�������";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_CostControl)
                {
                    TmpNodeString = "�ѿع�������";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Insulation )
                {
                    TmpNodeString = "��Ƶ��ѹ����";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_LoadRecord)
                {
                    TmpNodeString = "���ɼ�¼����";
                }
                foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node in _FatherNode.Nodes)
                {
                    if (_Node.Text == TmpNodeString)
                    {
                        _Node.State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked;
                        _Node.Tag = LstCopyFa[i];
                        break;
                    }
                }

            }

            LstCopyFa.Clear();
            LstCopyFa = null;
        }

        /// <summary>
        /// ���׷�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Copy_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;

            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"��ѡ����Ҫ�������ܷ����������ӷ����ڵ�...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.SaveTmpDataToTag();
            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

            LstCopyFa = new List<object>();

            if (Tv_FaList.SelectedNode.Parent == null) //������Ǹ���ڵ�
            {
                _FatherNode = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }
            else
            {
                _FatherNode = Tv_FaList.SelectedNode.Parent as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }

            foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node in _FatherNode.Nodes)
            {
                if (_Node.State == CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked) continue;            //û��ѡ�У���û������

                if (_Node.Tag == null)
                {
                    switch (_Node.Text)
                    {
                        case "Ԥ�ȵ���":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_PrepareTest((int)_Ttype, _FaName));
                            continue;
                        case "Ԥ������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_YuRe((int)_Ttype, _FaName));
                            continue;
                        case "��ۼ������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_WGJC((int)_Ttype, _FaName));
                            continue;
                        case "������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_QiDong((int)_Ttype, _FaName));
                            continue;
                        case "Ǳ������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_QianDong((int)_Ttype, _FaName));
                            continue;
                        case "�����������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_WcPoint((int)_Ttype, _FaName));
                            continue;
                        case "��������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)_Ttype, _FaName));
                            continue;
                        case "�๦������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Dgn((int)_Ttype, _FaName));
                            continue;                        
                        case "ͨѶЭ��������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)_Ttype, _FaName));
                            continue;
                        case "Ӱ��������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Specal((int)_Ttype, _FaName));
                            continue;
                        case "�ز�����":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Carrier((int)_Ttype, _FaName));
                            continue;
                        case "�������ݱȶ�����":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Infrared((int)_Ttype, _FaName));
                            continue;
                        case "���һ����":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)_Ttype, _FaName));
                            continue;
                        case "��������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_PowerConsume((int)_Ttype, _FaName));
                            continue;
                        case "���Ṧ������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Freeze((int)_Ttype, _FaName));
                            continue;
                        case "���ܱ�������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Function((int)_Ttype, _FaName));
                            continue;
                        case "�ѿع�������":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_CostControl((int)_Ttype, _FaName));
                            continue;
                        case "�¼���¼����":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_EventLog((int)_Ttype, _FaName));
                            continue;
                        case "��Ƶ��ѹ����":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Insulation((int)_Ttype, _FaName));
                            continue;
                        case "���ɼ�¼����":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_LoadRecord((int)_Ttype, _FaName));
                            continue; 
                    }
                }
                else
                {
                    LstCopyFa.Add(_Node.Tag);
                }

            }

        }

        #endregion 


        #region ------------�����浵---------

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this, "��ѡ����Ҫ������ܷ����������ӷ����ڵ�...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (this._FaName != "")     //����������Ʋ�Ϊ�գ���ֱ�ӱ��棬����ҪSHOW����д�������Ƶ�Panel
            {

                
                this.SaveTmpDataToTag();

                CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

                if (Tv_FaList.SelectedNode.Parent == null)     //���ѡ�еľ��Ǹ��ڵ�
                {
                    _FatherNode = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                }
                else
                {
                    _FatherNode = Tv_FaList.SelectedNode.Parent as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                }

                CLDC_DataCore.Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan((int)_Ttype, _FaName);

                _Plan.Clear();

                foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node in _FatherNode.Nodes)
                {
                    if (_Node.State == CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked) continue;    //���û�б�ѡ��

                    #region ---------------ֱ���ܷ��������Ŀ�ڵ�---------
                    if (_Node.Tag == null)      //�����ǰ�ڵ���ѡ��״̬������Tag��ֵ��Ϊ�գ���ֱ�����ܷ��������
                    {
                        switch (_Node.Text)
                        {
                            case "Ԥ�ȵ���":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���, _FaName, _Node.Index);
                                continue;
                            case "Ԥ������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ������, _FaName, _Node.Index);
                                continue;
                            case "��ۼ������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��ۼ������, _FaName, _Node.Index);
                                continue;
                            case "������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.������, _FaName, _Node.Index);
                                continue;
                            case "Ǳ������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ǳ������, _FaName, _Node.Index);
                                continue;
                            case "�����������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�����������, _FaName, _Node.Index);
                                continue;
                            case "��������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                                continue;
                            case "�๦������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�๦������, _FaName, _Node.Index);
                                continue;                            
                            case "ͨѶЭ��������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.ͨѶЭ��������, _FaName, _Node.Index);
                                continue;
                            case "Ӱ��������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������, _FaName, _Node.Index);
                                continue;
                            case "�ز�����":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ز�����, _FaName, _Node.Index);
                                continue;
                            case "�������ݱȶ�����":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�������ݱȶ�����, _FaName, _Node.Index);
                                continue;
                            case "���һ����":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���һ����, _FaName, _Node.Index);
                                continue;
                            case "��������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                                continue;
                            case "���Ṧ������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���Ṧ������, _FaName, _Node.Index);
                                continue;
                            case "�ѿع�������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ѿع�������, _FaName, _Node.Index);
                                continue;
                            case "���ܱ�������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ܱ�������, _FaName, _Node.Index);
                                continue;
                            case "�¼���¼����":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�¼���¼����, _FaName, _Node.Index);
                                continue;
                            case "����ת������":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.����ת������, _FaName, _Node.Index);
                                continue;
                            case "��Ƶ��ѹ����":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��Ƶ��ѹ����, _FaName, _Node.Index);
                                continue;
                            case "���ɼ�¼����":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ɼ�¼����, _FaName, _Node.Index);
                                continue;
                        }
                    }
                    #endregion

                    #region-----------����������Ŀ-------------------------
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PrepareTest)      //Ԥ�ȵ���
                    {
                        CLDC_DataCore.Model.Plan.Plan_PrepareTest _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PrepareTest;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_YuRe)            //Ԥ������
                    {
                        CLDC_DataCore.Model.Plan.Plan_YuRe _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_YuRe;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ������, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WGJC)          //��ۼ������
                    {
                        CLDC_DataCore.Model.Plan.Plan_WGJC _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WGJC;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��ۼ������, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QiDong)          //������
                    {
                        CLDC_DataCore.Model.Plan.Plan_QiDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QiDong;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.������, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QianDong)        //Ǳ������
                    {
                        CLDC_DataCore.Model.Plan.Plan_QianDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QianDong;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ǳ������, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WcPoint)      //�����������
                    {
                        CLDC_DataCore.Model.Plan.Plan_WcPoint _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�����������, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ZouZi)            //����
                    {
                        CLDC_DataCore.Model.Plan.Plan_ZouZi _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ZouZi;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Dgn)              //�๦��
                    {
                        CLDC_DataCore.Model.Plan.Plan_Dgn _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Dgn;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�๦������, _FaName, _Node.Index);
                    }

                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck)              //ͨѶЭ��������
                    {
                        CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck;
                        //if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.ͨѶЭ��������, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Specal)           //����춨
                    {
                        CLDC_DataCore.Model.Plan.Plan_Specal _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Specal;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������, _FaName, _Node.Index);
                    }

                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Carrier)            //�ز�����
                    {
                        CLDC_DataCore.Model.Plan.Plan_Carrier _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Carrier;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ز�����, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Infrared)            //�������ݱȶ�����
                    {
                        CLDC_DataCore.Model.Plan.Plan_Infrared _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Infrared;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�������ݱȶ�����, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ErrAccord)        //���һ����
                    {
                        CLDC_DataCore.Model.Plan.Plan_ErrAccord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ErrAccord;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���һ����, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PowerConsume)        //���һ����
                    {
                        CLDC_DataCore.Model.Plan.Plan_PowerConsume _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PowerConsume;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Freeze)  //����ʵ��
                    {
                        CLDC_DataCore.Model.Plan.Plan_Freeze _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Freeze;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���Ṧ������, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Function)         //���ܱ���
                    {
                        CLDC_DataCore.Model.Plan.Plan_Function _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Function;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ܱ�������, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_EventLog)              //�¼���¼
                    {
                        CLDC_DataCore.Model.Plan.Plan_EventLog _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_EventLog;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�¼���¼����, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_CostControl)              //�ѿع���
                    {
                        CLDC_DataCore.Model.Plan.Plan_CostControl _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_CostControl;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ѿع�������, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_DataSendForRelay)              //����ת������
                    {
                        CLDC_DataCore.Model.Plan.Plan_DataSendForRelay _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_DataSendForRelay;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.����ת������, _FaName, _Node.Index);
                    }

                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Insulation)              //��Ƶ��ѹ����
                    {
                        CLDC_DataCore.Model.Plan.Plan_Insulation _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Insulation;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��Ƶ��ѹ����, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_LoadRecord)              //��Ƶ��ѹ����
                    {
                        CLDC_DataCore.Model.Plan.Plan_LoadRecord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_LoadRecord;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ɼ�¼����, _FaName, _Node.Index);
                    }
                    #endregion
                }
                if (_Plan.Count == 0)
                {
                    MessageBoxEx.Show(this,"�÷�����û���κ���Ŀ���޷���ɱ���...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                _Plan.Save();

                //MessageBoxEx.Show(this,"����Ϊ��" + _FaName + "�ķ�������ɹ�.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SetTitleText(_FaName);
                _FatherNode.Text = _FaName;
                if (MessageBoxEx.Show(this,"����Ϊ��" + _FaName + "�ķ�������ɹ�.", "��������", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //newFAName = _FaName;
                    //Close();
                }
            }
            else
            {
                if (Tv_FaList.SelectedNode.Checked == false)
                {
                    MessageBoxEx.Show(this, "��ѡ����Ҫ������ܷ����������ӷ����ڵ�...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Txt_FaName.Text = "";
                this.LockControl(true);
            }


        }

        #endregion

        #region--------���������浵----------

        /// <summary>
        /// �������水ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Txt_FaName.Text == "")
            {
                MessageBoxEx.Show(this,"����д��Ҫ����ķ�������...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Txt_FaName.Text == "�½�...")
            {
                MessageBoxEx.Show(this,"����ʹ�ùؼ��֡��½�...����Ϊ��������...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Txt_FaName.SelectionStart = 0;
                Txt_FaName.SelectionLength = Txt_FaName.Text.Length;
                Txt_FaName.Focus();
                return;
            }

            this._FaName = Txt_FaName.Text;

            this.LockControl(false);

            this.Tb_Save_Click(sender, e);


        }

        #endregion


        #region -----------ȡ������-------------
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            this.LockControl(false);
        }

        #endregion

        private void Frm_FaSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control Item = this.Controls[i];

                this.Controls.RemoveAt(i);
                Item.Dispose();

                Item = null;
            }
        }

        #region -------------����ر�-----------
        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Close_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (MessageBoxEx.Show(this,"�ر�ǰ��ȷ���Ѿ���������Ҫ�洢�ķ�����Ϣ\n�����ȷ�ϡ��رմ��壬�����ȡ�����˳��ر�", "�ر���ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            this.Close();
        }

        #endregion

        #region ------------�������--------------
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_FaSetup_Load(object sender, EventArgs e)
        {
            
            if (CLDC_DataCore.Const.GlobalUnit.Plan_FromMDB == true)
            {
                List<CLDC_DataCore.Model.Plan.PlanModel.Scheme_Check> FaGroups = CLDC_DataCore.Model.Plan.Model_Plan.getFileNamesFromMDB("",0);
                int lstCount = FaGroups.Count;
                for (int i = 0; i < lstCount; i++)
                {
                    this.LoadFaGroup(FaGroups[i].schemeID.ToString());
                    if (this.RefreahLastString != "")
                    {
                        if (this.RefreahLastString == FaGroups[i].chrPlanName)
                        {
                            Tv_FaList.SelectedNode = Tv_FaList.Nodes[Tv_FaList.Nodes.Count - 1];
                            Tv_FaList.SelectedNode.Expand();
                        }
                    }
                }
            }
            else
            {
                List<string> FaGroups = CLDC_DataCore.Model.Plan.Model_Plan.getFileNames(CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);

                for (int i = 0; i < FaGroups.Count; i++)
                {
                    this.LoadFaGroup(FaGroups[i]);
                    if (this.RefreahLastString != "")
                    {
                        if (this.RefreahLastString == FaGroups[i])
                        {
                            Tv_FaList.SelectedNode = Tv_FaList.Nodes[Tv_FaList.Nodes.Count - 1];
                            Tv_FaList.SelectedNode.Expand();
                        }
                    }
                }
            }
            //double dbl_i = 0;
            //while (dbl_i < 100)
            //{
            //    System.Threading.Thread.Sleep(1);
            //    this.Opacity = dbl_i / 100;
            //    this.Refresh();
            //    dbl_i++;
            //}

            if (this.Tv_FaList.Nodes.Count > 0)
            {
                this.Tv_FaList.SelectedNode = this.Tv_FaList.Nodes[0];
                this.Tv_FaList.SelectedNode.Checked = false;
            }

            this.RefreahLastString = "";
        }

        #endregion

        #region -------------����ɾ��-------------
        /// <summary>
        /// ����ɾ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Del_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"û��ѡ�񷽰���Ŀ���������ɾ������...", "����ɾ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node;

            if (Tv_FaList.SelectedNode.Parent == null)
            {
                _Node = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }
            else
            {
                _Node = Tv_FaList.SelectedNode.Parent as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }

            DialogResult _Result = MessageBoxEx.Show(this,"ȷ��Ҫɾ����������Ϊ:" + _Node.Text +
                                                    "�ķ�����\n������ǡ���ɾ���÷����µ�������Ŀ���ݣ�\n������񡿽���ɾ���÷����������������µķ������ݣ�\n�����ȡ������ȡ��ɾ������.", "����ɾ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (_Result == DialogResult.Yes)      //ɾ������
            {
                CLDC_DataCore.Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan((int)this._Ttype, _Node.Text);
                CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);
                for (int i = 0; i < _Plan.Count; i++)
                {
                    if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_PREPARE_FOLDERNAME, (int)this._Ttype);
                    }
                    if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ԥ������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��ۼ������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_WGJC_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ǳ������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�����������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME, (int)this._Ttype);
                    }                    
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�๦������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.ͨѶЭ��������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_CONNPROTOCOL_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_TS_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�ز�����)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_ZB_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�������ݱȶ�����)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���һ����)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_YZX_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME, (int)this._Ttype);
                    }

                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���ܱ�������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GN_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���Ṧ������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_FZ_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�ѿع�������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_FK_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�¼���¼����)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.����ת������)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_DATASEND_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��Ƶ��ѹ����)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_INSULATION_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���ɼ�¼����)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_LOADRECORD_FOLDERNAME, (int)this._Ttype);
                    }
                }
            }
            else if (_Result == DialogResult.No)          //ֻɾ���ܷ���
            {
                CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);
            }
            else   //ȡ��ɾ��
            {
                return;
            }

            Tv_FaList.Nodes.Remove(_Node);
            this.BeforeNode = null;
        }

        #endregion

        #endregion

        #region -------------------˽�з���������------------------

        /// <summary>
        /// ���ñ����ı�
        /// </summary>
        /// <param name="value"></param>
        private void SetTitleText(string value)
        {
            this.Text = string.Format("{0}({1}){2}��{3}��", CONST_TITLESTRING, _Ttype.ToString(), CONST_HENG, value);
        }

        /// <summary>
        /// �洢��ʱ�ļ�����ӦNODE��Tag��
        /// </summary>
        private void SaveTmpDataToTag()
        {
            if (Panel_Control.Controls.Count == 0) return;
            UserControl _Control;
            _Control = Panel_Control.Controls[0] as UserControl;

            if (_Control is FAPrj.UI_PrepareTest)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_PrepareTest)_Control).Copy();
            }
            if (_Control is FAPrj.UI_YuRe)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_YuRe)_Control).Copy();
            }
            if (_Control is FAPrj.UI_QiDong)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_QiDong)_Control).Copy();
            }
            if (_Control is FAPrj.UI_QianDong)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_QianDong)_Control).Copy();
            }
            if (_Control is FAPrj.UI_Error)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Error)_Control).Copy();
            }
            if (_Control is FAPrj.UI_ZouZi)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_ZouZi)_Control).Copy();
            }
            if (_Control is FAPrj.UI_Dgn)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Dgn)_Control).Copy();
            }
            if (_Control is FAPrj.UI_ConnProtocolCheck)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_ConnProtocolCheck)_Control).Copy();
            }
            if (_Control is FAPrj.UI_Special)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Special)_Control).Copy();
            }
            if (_Control is FAPrj.UI_Carrier)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Carrier)_Control).Copy();
            }
            if (_Control is FAPrj.UI_Infrared)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Infrared)_Control).Copy();
            }
            if (_Control is FAPrj.UI_ErrAccord)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_ErrAccord)_Control).Copy();
            }
            if (_Control is FAPrj.UI_PowerConsume)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_PowerConsume)_Control).Copy();
            }


            if (_Control is FAPrj.UI_Function)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Function)_Control).Copy();
            }
            if (_Control is FAPrj.UI_CostControl)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_CostControl)_Control).Copy();
            }
            if (_Control is FAPrj.UI_EventLog)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_EventLog)_Control).Copy();
            }
            if (_Control is FAPrj.UI_Freeze)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Freeze)_Control).Copy();
            }

            if (_Control is FAPrj.UI_DataSendForRelay)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_DataSendForRelay)_Control).Copy();
            }


            if (_Control is FAPrj.UI_Insulation)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_Insulation)_Control).GetPlan();
            }
            if (_Control is FAPrj.UI_WGJC)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_WGJC)_Control).Copy();
            }
            if (_Control is FAPrj.UI_LoadRecord)
            {
                this.BeforeNode.Tag = ((FAPrj.UI_LoadRecord)_Control).Copy();
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

        /// <summary>
        /// �����ܷ�����Ŀ�б���TreeViewѡ����б���л��ޣ�
        /// </summary>
        /// <param name="FaName"></param>
        private void LoadFaGroup(string FaName)
        {
            CLDC_DataCore.Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan((int)this._Ttype, FaName);

            ///�����ܱ༭,ֱ���˳�.��������ĳЩ�����������޸�
            if (!_Plan.isCanModify)
            {
                //MessageBoxEx.Show(_Plan.Name + "����ʾ");
                return;
            }

            bool[] blnFaPrj = new bool[21];  

            for (int i = 0; i < _Plan.Count; i++)
            {
                blnFaPrj[(int)_Plan.getFAPrj(i).FAType - 1] = true;
            }

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _ChildNode;

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(FaName);

            //����һ��Ľڵ����contextMenu
            _Node.ContextMenuStrip = contextMenuStrip4Rename;

            Tv_FaList.Nodes.Add(_Node);



            #region �Ȱ���Ĭ���������

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ԥ�ȵ���", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.Ԥ�ȵ��� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ԥ������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.Ԥ������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��ۼ������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.��ۼ������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��Ƶ��ѹ����", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.��Ƶ��ѹ���� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ǳ������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.Ǳ������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�����������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.����������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("ͨѶЭ��������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.ͨѶЭ�������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�๦������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�๦������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("Ӱ��������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.Ӱ�������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;


            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�ز�����", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�ز����� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���һ����", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.���һ���� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("��������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�ѿع�������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�ѿع������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���ܱ�������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.���ܱ������� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�¼���¼����", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�¼���¼���� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;


            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���Ṧ������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.���Ṧ������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("����ת������", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.����ת������ - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("�������ݱȶ�����", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.�������ݱȶ����� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("���ɼ�¼����", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.���ɼ�¼���� - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            #endregion �Ȱ���Ĭ���������

            #region ��Xml���� 2011/6/2 by netteans@163.com
            if (!flagDefaultSort) //���治ִ�о�ʹ��Ĭ������
            {
                string[] names;
                int[] indexs = _Plan.GetIndexs(out names);

                Dictionary<string, int> dictinary = new Dictionary<string, int>();
                for (int i = 0; i < indexs.Length; i++)
                {
                    if (indexs[i] >= 0)
                        dictinary.Add(names[i], indexs[i]);
                }

                //������������ѭ����������ϣ��вŵİ�æ��һ�� yl
                for (int i = 0; i < blnFaPrj.Length; i++)
                {
                    foreach (KeyValuePair<string, int> pair in dictinary)
                    {
                        if (pair.Value == i)
                        {
                            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode node = null;
                            for (int j = 0; j < _Node.Nodes.Count; j++)
                            {
                                if (_Node.Nodes[j].Text == pair.Key)
                                {
                                    node = _Node.Nodes[j] as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                                    break;
                                }
                            }
                            if (node != null)
                            {
                                _Node.Nodes.Remove(node);
                                _Node.Nodes.Insert(i, node);
                            }
                        }
                    }
                }
            }

            foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode node in _Node.Nodes)
                node.ContextMenuStrip = contextMenuSort;
            #endregion ��Xml���� 2011/6/2 by netteans@163.com

        }


        #endregion

        #region-----------���з���������----------------

        public void SetSelectFa(string FaName)
        {
            this.RefreahLastString = FaName;
        }


        #endregion

        /// <summary>
        /// �ڵ�����Ժ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            if (e.Label.Trim().Equals(string.Empty))
            {
                ///��ԭ
                e.Node.Text = this.LastFAName;
                return;
            }
            ///�ж��Ƿ��޸�����
            if (e.Label != this.LastFAName)
            {
                Tb_Plaster_Click(sender, new EventArgs());
                ///���e.Label����
                _FaName = e.Label;
                if (_FaName != "")     //����������Ʋ�Ϊ�գ���ֱ�ӱ��棬����ҪSHOW����д�������Ƶ�Panel
                {
                    this.SaveTmpDataToTag();

                    CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

                    if (Tv_FaList.SelectedNode.Parent == null)     //���ѡ�еľ��Ǹ��ڵ�
                    {
                        _FatherNode = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                    }
                    else
                    {
                        _FatherNode = Tv_FaList.SelectedNode.Parent as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                    }

                    CLDC_DataCore.Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan((int)_Ttype, _FaName);

                    _Plan.Clear();

                    foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node in _FatherNode.Nodes)
                    {
                        if (_Node.State == CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked) continue;    //���û�б�ѡ��

                        #region ---------------ֱ���ܷ��������Ŀ�ڵ�---------
                        if (_Node.Tag == null)      //�����ǰ�ڵ���ѡ��״̬������Tag��ֵ��Ϊ�գ���ֱ�����ܷ��������
                        {
                            switch (_Node.Text)
                            {
                                case "Ԥ�ȵ���":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���, _FaName, _Node.Index);
                                    continue;
                                case "Ԥ������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ������, _FaName, _Node.Index);
                                    continue;
                                case "��ۼ������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��ۼ������, _FaName, _Node.Index);
                                    continue;
                                case "������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.������, _FaName, _Node.Index);
                                    continue;
                                case "Ǳ������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ǳ������, _FaName, _Node.Index);
                                    continue;
                                case "�����������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�����������, _FaName, _Node.Index);
                                    continue;
                                case "��������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                                    continue;
                                case "�๦������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�๦������, _FaName, _Node.Index);
                                    continue;                                
                                case "ͨѶЭ��������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.ͨѶЭ��������, _FaName, _Node.Index);
                                    continue;
                                case "Ӱ��������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������, _FaName, _Node.Index);
                                    continue;
                                case "�ز�����":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ز�����, _FaName, _Node.Index);
                                    continue;
                                case "�������ݱȶ�����":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�������ݱȶ�����, _FaName, _Node.Index);
                                    continue;
                                case "���һ����":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���һ����, _FaName, _Node.Index);
                                    continue;
                                case "��������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                                    continue;


                                case "���Ṧ������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���Ṧ������, _FaName, _Node.Index);
                                    continue;
                                case "�ѿع�������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ѿع�������, _FaName, _Node.Index);
                                    continue;
                                case "���ܱ�������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ܱ�������, _FaName, _Node.Index);
                                    continue;
                                case "�¼���¼����":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�¼���¼����, _FaName, _Node.Index);
                                    continue;

                                case "����ת������":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.����ת������, _FaName, _Node.Index);
                                    continue;

                                case "��Ƶ��ѹ����":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��Ƶ��ѹ����, _FaName, _Node.Index);
                                    continue;

                                case "���ɼ�¼����":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ɼ�¼����, _FaName, _Node.Index);
                                    continue;
                            }
                        }
                        #endregion

                        #region-----------����������Ŀ-------------------------
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PrepareTest)            //Ԥ�ȵ���
                        {
                            CLDC_DataCore.Model.Plan.Plan_PrepareTest _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PrepareTest;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_YuRe)            //Ԥ������
                        {
                            CLDC_DataCore.Model.Plan.Plan_YuRe _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_YuRe;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ԥ������, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WGJC)          //��ۼ������
                        {
                            CLDC_DataCore.Model.Plan.Plan_WGJC _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WGJC;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��ۼ������, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QiDong)          //������
                        {
                            CLDC_DataCore.Model.Plan.Plan_QiDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QiDong;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.������, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QianDong)        //Ǳ������
                        {
                            CLDC_DataCore.Model.Plan.Plan_QianDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QianDong;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ǳ������, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WcPoint)      //�����������
                        {
                            CLDC_DataCore.Model.Plan.Plan_WcPoint _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�����������, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ZouZi)            //����
                        {
                            CLDC_DataCore.Model.Plan.Plan_ZouZi _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ZouZi;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Dgn)              //�๦��
                        {
                            CLDC_DataCore.Model.Plan.Plan_Dgn _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Dgn;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�๦������, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck)              //ͨѶЭ��������
                        {
                            CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.ͨѶЭ��������, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Specal)           //����춨
                        {
                            CLDC_DataCore.Model.Plan.Plan_Specal _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Specal;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Carrier)            //�ز�����
                        {
                            CLDC_DataCore.Model.Plan.Plan_Carrier _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Carrier;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ز�����, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Infrared)            //�������ݱȶ�����
                        {
                            CLDC_DataCore.Model.Plan.Plan_Infrared _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Infrared;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�������ݱȶ�����, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ErrAccord)          //���һ����
                        {
                            CLDC_DataCore.Model.Plan.Plan_ErrAccord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ErrAccord;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���һ����, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PowerConsume)          //���һ����
                        {
                            CLDC_DataCore.Model.Plan.Plan_PowerConsume _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PowerConsume;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��������, _FaName, _Node.Index);

                        }


                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Function)
                        {
                            CLDC_DataCore.Model.Plan.Plan_Function _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Function;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ܱ�������, _FaName, _Node.Index);
                        }
                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_EventLog)
                        {
                            CLDC_DataCore.Model.Plan.Plan_EventLog _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_EventLog;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�¼���¼����, _FaName, _Node.Index);
                        }


                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_CostControl)
                        {
                            CLDC_DataCore.Model.Plan.Plan_CostControl _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_CostControl;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.�ѿع�������, _FaName, _Node.Index);
                        }
                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Freeze)
                        {
                            CLDC_DataCore.Model.Plan.Plan_Freeze _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Freeze;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���Ṧ������, _FaName, _Node.Index);
                        }


                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_DataSendForRelay)
                        {
                            CLDC_DataCore.Model.Plan.Plan_DataSendForRelay _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_DataSendForRelay;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.����ת������, _FaName, _Node.Index);
                        }

                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Insulation)
                        {
                            CLDC_DataCore.Model.Plan.Plan_Insulation _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Insulation;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.��Ƶ��ѹ����, _FaName, _Node.Index);
                        }
                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_LoadRecord)
                        {
                            CLDC_DataCore.Model.Plan.Plan_LoadRecord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_LoadRecord;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.���ɼ�¼����, _FaName, _Node.Index);
                        }
                        #endregion
                    }
                    if (_Plan.Count == 0)
                    {
                        MessageBoxEx.UseSystemLocalizedString = true;
                        MessageBoxEx.Show(this,"�÷�����û���κ���Ŀ���޷���ɱ���...", "��������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _Plan.Save();
                    this.SetTitleText(_FaName);
                    _FatherNode.Text = _FaName;
                    #region //ɾ������(this.LastFAName)

                    CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _NodeOld;

                    _NodeOld = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                    CLDC_DataCore.Model.Plan.Model_Plan _PlanOld = new CLDC_DataCore.Model.Plan.Model_Plan((int)this._Ttype, this.LastFAName);
                    CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);
                    for (int i = 0; i < _Plan.Count; i++)
                    {
                        if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ԥ�ȵ���)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_PREPARE_FOLDERNAME, (int)this._Ttype);
                        }
                        if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ԥ������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��ۼ������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_WGJC_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��Ƶ��ѹ����)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_INSULATION_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ǳ������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�����������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�๦������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.ͨѶЭ��������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_CONNPROTOCOL_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.Ӱ��������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_TS_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�ز�����)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_ZB_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�������ݱȶ�����)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���һ����)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_YZX_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.��������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME, (int)this._Ttype);
                        }

                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���ܱ�������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_GN_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�ѿع�������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_FK_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���Ṧ������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_FZ_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.�¼���¼����)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.����ת������)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_DATASEND_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.���ɼ�¼����)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_LOADRECORD_FOLDERNAME, (int)this._Ttype);
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// �ڵ�༭ǰ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            this.LastFAName = e.Node.Text;
            Tb_Copy_Click(sender, new EventArgs());
        }
        /// <summary>
        /// ����ڵ��Ժ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                if (toolStripMenuItem4Rename.Visible == false)
                {
                    toolStripMenuItem4Rename.Visible = true;
                    toolStripMenuItem4Rename.Enabled = true;
                    contextMenuStrip4Rename.Enabled = true;
                    Tv_FaList.SelectedNode.Checked = true;
                }

            }
            else
            {
                toolStripMenuItem4Rename.Visible = false;
                toolStripMenuItem4Rename.Enabled = false;
                contextMenuStrip4Rename.Enabled = false;
                Tv_FaList.LabelEdit = false;
            }
        }
        /// <summary>
        /// �Ҽ�����������Ժ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4Rename_Click(object sender, EventArgs e)
        {
            Tv_FaList.LabelEdit = true;
            Tv_FaList.SelectedNode.BeginEdit();
        }

        #region �Է�����������
        private void menuUp_Click(object sender, EventArgs e)
        {
            TreeViewCheckStyel.ThreeStateTreeNode node = Tv_FaList.SelectedNode as TreeViewCheckStyel.ThreeStateTreeNode;
            if (node != null)
            {
                TreeViewCheckStyel.ThreeStateTreeNode nodeParent = node.Parent as TreeViewCheckStyel.ThreeStateTreeNode;
                if (node.PrevNode != null)
                {
                    int index = node.PrevNode.Index;

                    nodeParent.Nodes.Remove(node);
                    nodeParent.Nodes.Insert(index, node);
                    Tv_FaList.SelectedNode = node;
                }
            }
        }

        private void menuDown_Click(object sender, EventArgs e)
        {
            TreeViewCheckStyel.ThreeStateTreeNode node = Tv_FaList.SelectedNode as TreeViewCheckStyel.ThreeStateTreeNode;
            if (node != null)
            {
                TreeViewCheckStyel.ThreeStateTreeNode nodeParent = node.Parent as TreeViewCheckStyel.ThreeStateTreeNode;
                if (node.NextNode != null)
                {
                    int index = node.NextNode.Index;

                    nodeParent.Nodes.Remove(node);
                    nodeParent.Nodes.Insert(index, node);
                    Tv_FaList.SelectedNode = node;
                }
            }
        }

        /// <summary>
        /// ������ı�־
        /// </summary>
        private bool flagDefaultSort = false;
        private void menuDefault_Click(object sender, EventArgs e)
        {
            if (Tv_FaList.SelectedNode != null)
            {
                if (Tv_FaList.SelectedNode.Level == 1)
                {
                    flagDefaultSort = true;
                    string planName = Tv_FaList.SelectedNode.Parent.Text;
                    //Tv_FaList.Nodes.Remove(Tv_FaList.SelectedNode.Parent);
                    LoadFaGroup(planName);
                    flagDefaultSort = false;
                }
            }
        }
        #endregion �Է�����������
    }
}
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

        const string CONST_TITLESTRING = "科陆电能台方案管理器";
        const string CONST_HENG = "-----------------";
        private CLDC_Comm.Enum.Cus_TaiType _Ttype = new CLDC_Comm.Enum.Cus_TaiType();

        private List<object> LstCopyFa;

        private string RefreahLastString = "";
        /// <summary>
        /// 最后一次选择的方案名
        /// </summary>
        private string LastFAName;

        /// <summary>
        /// 方案名称
        /// </summary>
        private string _FaName = "";

        /// <summary>
        /// 修改的方案名称
        /// </summary>
        public string newFAName = "";

        /// <summary>
        /// 当前选中之前所选中的节点
        /// </summary>
        private CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode BeforeNode = null;

        #region ------------构造函数-----------------

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

        #region ----------------------事件-----------------------

        #region ---------------新增方案------------------
        private void Tb_New_Click(object sender, EventArgs e)
        {
            this.SetTitleText("新建");
            this._FaName = "";
            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("新建...");
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

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("预先调试", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("预热试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("外观检查试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("工频耐压试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("起动试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("潜动试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("基本误差试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("走字试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("通讯协议检查试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("多功能试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("影响量试验", 1, 2));

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("载波试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("误差一致性", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("功耗试验", 1, 2));

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("费控功能试验", 1, 2));

            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("智能表功能试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("冻结功能试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("事件记录试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("数据转发试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("红外数据比对试验", 1, 2));
            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("负荷记录试验", 1, 2));
            //排序菜单
            foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode node in _Node.Nodes)
                node.ContextMenuStrip = contextMenuSort;
        }
        #endregion

        #region -----------------方案项目选择-------------------
        /// <summary>
        /// 方案项目选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (BeforeNode != null && BeforeNode.FullPath == e.Node.FullPath) return;

            //鼠标右键点击是将选中的节点赋给当前节点
            if (e.Button == MouseButtons.Right)
            {
                Tv_FaList.SelectedNode = e.Node;
            }

            for (int i = 0; i < Tv_FaList.Nodes.Count; i++)
            {
                ((CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode)Tv_FaList.Nodes[i]).State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;
            }


            if (e.Node.Parent == null)          //如果本身就是父节点，就退出
            {
                this._FaName = e.Node.Text != "新建..." ? e.Node.Text : "";
                ((CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode)e.Node).State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked;
            }
            else
            {
                this._FaName = e.Node.Parent.Text != "新建..." ? e.Node.Parent.Text : "";
                ((CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode)e.Node.Parent).State = CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked;
            }

            this.SetTitleText(this._FaName == "" ? "新建" : this._FaName);

            if (this.BeforeNode != null)       //如果当前选中之前有选中的节点，则需要做一次Tag记录
            {
                this.SaveTmpDataToTag();            //保存到Tag
            }

            this.BeforeNode = e.Node as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;           //保存当前节点

            UserControl _Control = null;
            #region ---------------------预先调试-----------------------------
            if (e.Node.Text == "预先调试")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_PrepareTest)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的试验跳到当前方案的试验中，所以需要加载一次Tag
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
            #region ---------------------预热试验-----------------------------
            else if (e.Node.Text == "预热试验")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_YuRe)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的试验跳到当前方案的试验中，所以需要加载一次Tag
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
            #region ---------------------外观检查试验-----------------------------
            else if (e.Node.Text == "外观检查试验")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_WGJC)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的试验跳到当前方案的试验中，所以需要加载一次Tag
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
            #region----------------------起动试验------------------
            else if (e.Node.Text == "起动试验")
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

            #region---------------------潜动试验-------------------------
            else if (e.Node.Text == "潜动试验")
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
            #region -------------------基本误差试验--------------------
            else if (e.Node.Text == "基本误差试验")
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
            #region -------------------走字实验--------------------
            else if (e.Node.Text == "走字试验")
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
            #region -------------------多功能试验--------------------
            else if (e.Node.Text == "多功能试验")
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

            #region -------------------通讯协议检查试验--------------------
            else if (e.Node.Text == "通讯协议检查试验")
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


            #region -------------------数据转发试验--------------------
            else if (e.Node.Text == "数据转发试验")
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

            #region -------------------特殊检定--------------------
            else if (e.Node.Text == "影响量试验")
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

            #region -------------------载波试验--------------------
            else if (e.Node.Text == "载波试验")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Carrier)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的载波跳到当前方案的载波中，所以需要加载一次Tag
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

            #region -------------------红外数据比对试验--------------------
            else if (e.Node.Text == "红外数据比对试验")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_Infrared)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的载波跳到当前方案的载波中，所以需要加载一次Tag
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
            #region -------------------误差一致性--------------------
            else if (e.Node.Text == "误差一致性")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_ErrAccord)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的误差一致性跳到当前方案，所以需要加载一次Tag
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

            #region -------------------功耗试验--------------------
            else if (e.Node.Text == "功耗试验")
            {
                if (Panel_Control.Controls.Count > 0)
                {
                    if (Panel_Control.Controls[0] is FAPrj.UI_PowerConsume)
                    {
                        if (e.Node.Tag != null)     //这个时候有可能是从其他方案中的功耗跳到当前方案的功耗中，所以需要加载一次Tag
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


            #region -------------------智能表功能试验--------------------
            else if (e.Node.Text == "智能表功能试验")
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

            #region -------------------费控功能试验--------------------
            else if (e.Node.Text == "费控功能试验")
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

            #region -------------------事件记录试验--------------------
            else if (e.Node.Text == "事件记录试验")
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

            #region -------------------冻结功能试验--------------------
            else if (e.Node.Text == "冻结功能试验")
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

            #region 工频耐压试验
            else if (e.Node.Text == "工频耐压试验")
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
            #endregion 工频耐压试验


            #region -------------------负荷记录试验--------------------
            else if (e.Node.Text == "负荷记录试验")
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

        #region ------------------------拷贝、粘贴相关------------------------
        /// <summary>
        /// 数据粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Plaster_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (LstCopyFa == null) return;

            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"请选择需要拷贝的总方案，或者子方案节点...", "方案拷贝", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

            if (Tv_FaList.SelectedNode.Parent == null) //如果是父域节点
            {
                _FatherNode = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }
            else
            {
                MessageBoxEx.Show(this,"粘贴时，请选中主节点,否则无法完成粘贴操作,点击确认，重新操作...", "方案拷贝", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (int i = 0; i < LstCopyFa.Count; i++)
            {
                string TmpNodeString = "";
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_PrepareTest)
                {
                    TmpNodeString = "预先调试";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_YuRe)
                {
                    TmpNodeString = "预热试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_WGJC)
                {
                    TmpNodeString = "外观检查试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_QiDong)
                {
                    TmpNodeString = "起动试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_QianDong)
                {
                    TmpNodeString = "潜动试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_WcPoint)
                {
                    TmpNodeString = "基本误差试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_ZouZi)
                {
                    TmpNodeString = "走字试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Dgn)
                {
                    TmpNodeString = "多功能试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck)
                {
                    TmpNodeString = "通讯协议检查试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Specal)
                {
                    TmpNodeString = "影响量试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Carrier)
                {
                    TmpNodeString = "载波试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Infrared)
                {
                    TmpNodeString = "红外数据比对试验";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_ErrAccord)
                {
                    TmpNodeString = "误差一致性";
                }
                if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_PowerConsume)
                {
                    TmpNodeString = "功耗试验";
                }


                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Freeze)
                {
                    TmpNodeString = "冻结功能试验";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_EventLog)
                {
                    TmpNodeString = "事件记录试验";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Function)
                {
                    TmpNodeString = "智能表功能试验";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_CostControl)
                {
                    TmpNodeString = "费控功能试验";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_Insulation )
                {
                    TmpNodeString = "工频耐压试验";
                }
                else if (LstCopyFa[i] is CLDC_DataCore.Model.Plan.Plan_LoadRecord)
                {
                    TmpNodeString = "负荷记录试验";
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
        /// 整套方案拷贝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Copy_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;

            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"请选择需要拷贝的总方案，或者子方案节点...", "方案拷贝", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.SaveTmpDataToTag();
            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

            LstCopyFa = new List<object>();

            if (Tv_FaList.SelectedNode.Parent == null) //如果不是父域节点
            {
                _FatherNode = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }
            else
            {
                _FatherNode = Tv_FaList.SelectedNode.Parent as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
            }

            foreach (CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node in _FatherNode.Nodes)
            {
                if (_Node.State == CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked) continue;            //没有选中，就没有数据

                if (_Node.Tag == null)
                {
                    switch (_Node.Text)
                    {
                        case "预先调试":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_PrepareTest((int)_Ttype, _FaName));
                            continue;
                        case "预热试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_YuRe((int)_Ttype, _FaName));
                            continue;
                        case "外观检查试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_WGJC((int)_Ttype, _FaName));
                            continue;
                        case "起动试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_QiDong((int)_Ttype, _FaName));
                            continue;
                        case "潜动试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_QianDong((int)_Ttype, _FaName));
                            continue;
                        case "基本误差试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_WcPoint((int)_Ttype, _FaName));
                            continue;
                        case "走字试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)_Ttype, _FaName));
                            continue;
                        case "多功能试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Dgn((int)_Ttype, _FaName));
                            continue;                        
                        case "通讯协议检查试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck((int)_Ttype, _FaName));
                            continue;
                        case "影响量试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Specal((int)_Ttype, _FaName));
                            continue;
                        case "载波试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Carrier((int)_Ttype, _FaName));
                            continue;
                        case "红外数据比对试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Infrared((int)_Ttype, _FaName));
                            continue;
                        case "误差一致性":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_ErrAccord((int)_Ttype, _FaName));
                            continue;
                        case "功耗试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_PowerConsume((int)_Ttype, _FaName));
                            continue;
                        case "冻结功能试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Freeze((int)_Ttype, _FaName));
                            continue;
                        case "智能表功能试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Function((int)_Ttype, _FaName));
                            continue;
                        case "费控功能试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_CostControl((int)_Ttype, _FaName));
                            continue;
                        case "事件记录试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_EventLog((int)_Ttype, _FaName));
                            continue;
                        case "工频耐压试验":
                            LstCopyFa.Add(new CLDC_DataCore.Model.Plan.Plan_Insulation((int)_Ttype, _FaName));
                            continue;
                        case "负荷记录试验":
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


        #region ------------方案存档---------

        /// <summary>
        /// 方案保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this, "请选择需要保存的总方案，或者子方案节点...", "方案保存", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (this._FaName != "")     //如果方案名称不为空，就直接保存，不需要SHOW出填写方案名称的Panel
            {

                
                this.SaveTmpDataToTag();

                CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

                if (Tv_FaList.SelectedNode.Parent == null)     //如果选中的就是父节点
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
                    if (_Node.State == CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked) continue;    //如果没有被选中

                    #region ---------------直接总方案添加项目节点---------
                    if (_Node.Tag == null)      //如果当前节点是选中状态，但是Tag的值又为空，则直接在总方案中添加
                    {
                        switch (_Node.Text)
                        {
                            case "预先调试":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预先调试, _FaName, _Node.Index);
                                continue;
                            case "预热试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预热试验, _FaName, _Node.Index);
                                continue;
                            case "外观检查试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.外观检查试验, _FaName, _Node.Index);
                                continue;
                            case "起动试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.起动试验, _FaName, _Node.Index);
                                continue;
                            case "潜动试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.潜动试验, _FaName, _Node.Index);
                                continue;
                            case "基本误差试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.基本误差试验, _FaName, _Node.Index);
                                continue;
                            case "走字试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.走字试验, _FaName, _Node.Index);
                                continue;
                            case "多功能试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.多功能试验, _FaName, _Node.Index);
                                continue;                            
                            case "通讯协议检查试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.通讯协议检查试验, _FaName, _Node.Index);
                                continue;
                            case "影响量试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.影响量试验, _FaName, _Node.Index);
                                continue;
                            case "载波试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.载波试验, _FaName, _Node.Index);
                                continue;
                            case "红外数据比对试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.红外数据比对试验, _FaName, _Node.Index);
                                continue;
                            case "误差一致性":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.误差一致性, _FaName, _Node.Index);
                                continue;
                            case "功耗试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.功耗试验, _FaName, _Node.Index);
                                continue;
                            case "冻结功能试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.冻结功能试验, _FaName, _Node.Index);
                                continue;
                            case "费控功能试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.费控功能试验, _FaName, _Node.Index);
                                continue;
                            case "智能表功能试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.智能表功能试验, _FaName, _Node.Index);
                                continue;
                            case "事件记录试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.事件记录试验, _FaName, _Node.Index);
                                continue;
                            case "数据转发试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.数据转发试验, _FaName, _Node.Index);
                                continue;
                            case "工频耐压试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.工频耐压试验, _FaName, _Node.Index);
                                continue;
                            case "负荷记录试验":
                                _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.负荷记录试验, _FaName, _Node.Index);
                                continue;
                        }
                    }
                    #endregion

                    #region-----------遍历方案项目-------------------------
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PrepareTest)      //预先调试
                    {
                        CLDC_DataCore.Model.Plan.Plan_PrepareTest _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PrepareTest;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预先调试, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_YuRe)            //预热试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_YuRe _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_YuRe;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预热试验, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WGJC)          //外观检查试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_WGJC _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WGJC;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.外观检查试验, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QiDong)          //起动试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_QiDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QiDong;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.起动试验, _FaName, _Node.Index);

                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QianDong)        //潜动试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_QianDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QianDong;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.潜动试验, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WcPoint)      //基本误差试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_WcPoint _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.基本误差试验, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ZouZi)            //走字
                    {
                        CLDC_DataCore.Model.Plan.Plan_ZouZi _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ZouZi;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.走字试验, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Dgn)              //多功能
                    {
                        CLDC_DataCore.Model.Plan.Plan_Dgn _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Dgn;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.多功能试验, _FaName, _Node.Index);
                    }

                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck)              //通讯协议检查试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck;
                        //if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.通讯协议检查试验, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Specal)           //特殊检定
                    {
                        CLDC_DataCore.Model.Plan.Plan_Specal _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Specal;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.影响量试验, _FaName, _Node.Index);
                    }

                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Carrier)            //载波试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_Carrier _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Carrier;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.载波试验, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Infrared)            //红外数据比对试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_Infrared _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Infrared;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.红外数据比对试验, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ErrAccord)        //误差一致性
                    {
                        CLDC_DataCore.Model.Plan.Plan_ErrAccord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ErrAccord;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.误差一致性, _FaName, _Node.Index);
                    }
                    if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PowerConsume)        //误差一致性
                    {
                        CLDC_DataCore.Model.Plan.Plan_PowerConsume _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PowerConsume;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.功耗试验, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Freeze)  //冻结实验
                    {
                        CLDC_DataCore.Model.Plan.Plan_Freeze _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Freeze;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.冻结功能试验, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Function)         //智能表功能
                    {
                        CLDC_DataCore.Model.Plan.Plan_Function _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Function;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.智能表功能试验, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_EventLog)              //事件记录
                    {
                        CLDC_DataCore.Model.Plan.Plan_EventLog _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_EventLog;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.事件记录试验, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_CostControl)              //费控功能
                    {
                        CLDC_DataCore.Model.Plan.Plan_CostControl _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_CostControl;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.费控功能试验, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_DataSendForRelay)              //数据转发试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_DataSendForRelay _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_DataSendForRelay;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.数据转发试验, _FaName, _Node.Index);
                    }

                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Insulation)              //工频耐压试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_Insulation _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Insulation;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.工频耐压试验, _FaName, _Node.Index);
                    }
                    else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_LoadRecord)              //工频耐压试验
                    {
                        CLDC_DataCore.Model.Plan.Plan_LoadRecord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_LoadRecord;
                        if (_Item.Count == 0) continue;
                        _Item.SetPram((int)_Ttype, _FaName);
                        _Item.Save();

                        _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.负荷记录试验, _FaName, _Node.Index);
                    }
                    #endregion
                }
                if (_Plan.Count == 0)
                {
                    MessageBoxEx.Show(this,"该方案中没有任何项目，无法完成保存...", "方案保存", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                _Plan.Save();

                //MessageBoxEx.Show(this,"名称为：" + _FaName + "的方案保存成功.", "方案保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SetTitleText(_FaName);
                _FatherNode.Text = _FaName;
                if (MessageBoxEx.Show(this,"名称为：" + _FaName + "的方案保存成功.", "方案保存", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //newFAName = _FaName;
                    //Close();
                }
            }
            else
            {
                if (Tv_FaList.SelectedNode.Checked == false)
                {
                    MessageBoxEx.Show(this, "请选择需要保存的总方案，或者子方案节点...", "方案保存", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Txt_FaName.Text = "";
                this.LockControl(true);
            }


        }

        #endregion

        #region--------方案新增存档----------

        /// <summary>
        /// 新增保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Txt_FaName.Text == "")
            {
                MessageBoxEx.Show(this,"请填写需要保存的方案名称...", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Txt_FaName.Text == "新建...")
            {
                MessageBoxEx.Show(this,"不能使用关键字“新建...”作为方案名称...", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


        #region -----------取消保存-------------
        /// <summary>
        /// 取消保存
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

        #region -------------窗体关闭-----------
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Close_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (MessageBoxEx.Show(this,"关闭前请确认已经保存了需要存储的方案信息\n点击【确认】关闭窗体，点击【取消】退出关闭", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            this.Close();
        }

        #endregion

        #region ------------窗体加载--------------
        /// <summary>
        /// 窗体加载
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

        #region -------------方案删除-------------
        /// <summary>
        /// 方案删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_Del_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tv_FaList.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"没有选择方案项目，不能完成删除操作...", "方案删除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            DialogResult _Result = MessageBoxEx.Show(this,"确定要删除方案名称为:" + _Node.Text +
                                                    "的方案吗？\n点击【是】将删除该方案下的所有项目内容；\n点击【否】将仅删除该方案，并保留方案下的方案内容；\n点击【取消】将取消删除操作.", "方案删除", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (_Result == DialogResult.Yes)      //删除所有
            {
                CLDC_DataCore.Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan((int)this._Ttype, _Node.Text);
                CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);
                for (int i = 0; i < _Plan.Count; i++)
                {
                    if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.预先调试)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_PREPARE_FOLDERNAME, (int)this._Ttype);
                    }
                    if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.预热试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.外观检查试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_WGJC_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.起动试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.潜动试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.走字试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.基本误差试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME, (int)this._Ttype);
                    }                    
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.多功能试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.通讯协议检查试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_CONNPROTOCOL_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.影响量试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_TS_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.载波试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_ZB_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.红外数据比对试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.误差一致性)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_YZX_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.功耗试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME, (int)this._Ttype);
                    }

                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.智能表功能试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GN_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.冻结功能试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_FZ_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.费控功能试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_FK_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.事件记录试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.数据转发试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_DATASEND_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.工频耐压试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_INSULATION_FOLDERNAME, (int)this._Ttype);
                    }
                    else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.负荷记录试验)
                    {
                        CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_LOADRECORD_FOLDERNAME, (int)this._Ttype);
                    }
                }
            }
            else if (_Result == DialogResult.No)          //只删除总方案
            {
                CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(_Node.Text, CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);
            }
            else   //取消删除
            {
                return;
            }

            Tv_FaList.Nodes.Remove(_Node);
            this.BeforeNode = null;
        }

        #endregion

        #endregion

        #region -------------------私有方法、函数------------------

        /// <summary>
        /// 设置标题文本
        /// </summary>
        /// <param name="value"></param>
        private void SetTitleText(string value)
        {
            this.Text = string.Format("{0}({1}){2}【{3}】", CONST_TITLESTRING, _Ttype.ToString(), CONST_HENG, value);
        }

        /// <summary>
        /// 存储临时文件到对应NODE的Tag中
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
        /// 当显示填写方案名称的保存面板的时候需要锁住窗体
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
        /// 加载总方案项目列表（在TreeView选择框中标记有或无）
        /// </summary>
        /// <param name="FaName"></param>
        private void LoadFaGroup(string FaName)
        {
            CLDC_DataCore.Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan((int)this._Ttype, FaName);

            ///若不能编辑,直接退出.用于限制某些方案不允许修改
            if (!_Plan.isCanModify)
            {
                //MessageBoxEx.Show(_Plan.Name + "不显示");
                return;
            }

            bool[] blnFaPrj = new bool[21];  

            for (int i = 0; i < _Plan.Count; i++)
            {
                blnFaPrj[(int)_Plan.getFAPrj(i).FAType - 1] = true;
            }

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _ChildNode;

            CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(FaName);

            //给第一层的节点添加contextMenu
            _Node.ContextMenuStrip = contextMenuStrip4Rename;

            Tv_FaList.Nodes.Add(_Node);



            #region 先按照默认排序添加

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("预先调试", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.预先调试 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("预热试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.预热试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("外观检查试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.外观检查试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("工频耐压试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.工频耐压试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("起动试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.起动试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("潜动试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.潜动试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("基本误差试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.基本误差试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("走字试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.走字试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("通讯协议检查试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.通讯协议检查试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("多功能试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.多功能试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("影响量试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.影响量试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;


            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("载波试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.载波试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("误差一致性", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.误差一致性 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("功耗试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.功耗试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("费控功能试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.费控功能试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("智能表功能试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.智能表功能试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("事件记录试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.事件记录试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;


            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("冻结功能试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.冻结功能试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("数据转发试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.数据转发试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("红外数据比对试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.红外数据比对试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            _ChildNode = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("负荷记录试验", 1, 2);
            _Node.Nodes.Add(_ChildNode);
            _ChildNode.State = blnFaPrj[(int)Cus_FAGroup.负荷记录试验 - 1] ? CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Checked : CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked;

            #endregion 先按照默认排序添加

            #region 按Xml排序 2011/6/2 by netteans@163.com
            if (!flagDefaultSort) //下面不执行就使用默认排序
            {
                string[] names;
                int[] indexs = _Plan.GetIndexs(out names);

                Dictionary<string, int> dictinary = new Dictionary<string, int>();
                for (int i = 0; i < indexs.Length; i++)
                {
                    if (indexs[i] >= 0)
                        dictinary.Add(names[i], indexs[i]);
                }

                //这里用了三个循环才排序完毕，有才的帮忙改一下 yl
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
            #endregion 按Xml排序 2011/6/2 by netteans@163.com

        }


        #endregion

        #region-----------公有方法、函数----------------

        public void SetSelectFa(string FaName)
        {
            this.RefreahLastString = FaName;
        }


        #endregion

        /// <summary>
        /// 节点更名以后
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
                ///还原
                e.Node.Text = this.LastFAName;
                return;
            }
            ///判断是否修改名字
            if (e.Label != this.LastFAName)
            {
                Tb_Plaster_Click(sender, new EventArgs());
                ///添加e.Label方案
                _FaName = e.Label;
                if (_FaName != "")     //如果方案名称不为空，就直接保存，不需要SHOW出填写方案名称的Panel
                {
                    this.SaveTmpDataToTag();

                    CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _FatherNode;

                    if (Tv_FaList.SelectedNode.Parent == null)     //如果选中的就是父节点
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
                        if (_Node.State == CLDC_MeterUI.TreeViewCheckStyel.Enumerations.CheckBoxState.Unchecked) continue;    //如果没有被选中

                        #region ---------------直接总方案添加项目节点---------
                        if (_Node.Tag == null)      //如果当前节点是选中状态，但是Tag的值又为空，则直接在总方案中添加
                        {
                            switch (_Node.Text)
                            {
                                case "预先调试":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预先调试, _FaName, _Node.Index);
                                    continue;
                                case "预热试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预热试验, _FaName, _Node.Index);
                                    continue;
                                case "外观检查试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.外观检查试验, _FaName, _Node.Index);
                                    continue;
                                case "起动试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.起动试验, _FaName, _Node.Index);
                                    continue;
                                case "潜动试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.潜动试验, _FaName, _Node.Index);
                                    continue;
                                case "基本误差试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.基本误差试验, _FaName, _Node.Index);
                                    continue;
                                case "走字试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.走字试验, _FaName, _Node.Index);
                                    continue;
                                case "多功能试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.多功能试验, _FaName, _Node.Index);
                                    continue;                                
                                case "通讯协议检查试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.通讯协议检查试验, _FaName, _Node.Index);
                                    continue;
                                case "影响量试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.影响量试验, _FaName, _Node.Index);
                                    continue;
                                case "载波试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.载波试验, _FaName, _Node.Index);
                                    continue;
                                case "红外数据比对试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.红外数据比对试验, _FaName, _Node.Index);
                                    continue;
                                case "误差一致性":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.误差一致性, _FaName, _Node.Index);
                                    continue;
                                case "功耗试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.功耗试验, _FaName, _Node.Index);
                                    continue;


                                case "冻结功能试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.冻结功能试验, _FaName, _Node.Index);
                                    continue;
                                case "费控功能试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.费控功能试验, _FaName, _Node.Index);
                                    continue;
                                case "智能表功能试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.智能表功能试验, _FaName, _Node.Index);
                                    continue;
                                case "事件记录试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.事件记录试验, _FaName, _Node.Index);
                                    continue;

                                case "数据转发试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.数据转发试验, _FaName, _Node.Index);
                                    continue;

                                case "工频耐压试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.工频耐压试验, _FaName, _Node.Index);
                                    continue;

                                case "负荷记录试验":
                                    _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.负荷记录试验, _FaName, _Node.Index);
                                    continue;
                            }
                        }
                        #endregion

                        #region-----------遍历方案项目-------------------------
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PrepareTest)            //预先调试
                        {
                            CLDC_DataCore.Model.Plan.Plan_PrepareTest _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PrepareTest;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预先调试, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_YuRe)            //预热试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_YuRe _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_YuRe;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.预热试验, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WGJC)          //外观检查试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_WGJC _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WGJC;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.外观检查试验, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QiDong)          //起动试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_QiDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QiDong;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.起动试验, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_QianDong)        //潜动试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_QianDong _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_QianDong;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.潜动试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_WcPoint)      //基本误差试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_WcPoint _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.基本误差试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ZouZi)            //走字
                        {
                            CLDC_DataCore.Model.Plan.Plan_ZouZi _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ZouZi;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.走字试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Dgn)              //多功能
                        {
                            CLDC_DataCore.Model.Plan.Plan_Dgn _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Dgn;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.多功能试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck)              //通讯协议检查试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ConnProtocolCheck;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.通讯协议检查试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Specal)           //特殊检定
                        {
                            CLDC_DataCore.Model.Plan.Plan_Specal _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Specal;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.影响量试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Carrier)            //载波试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_Carrier _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Carrier;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.载波试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Infrared)            //红外数据比对试验
                        {
                            CLDC_DataCore.Model.Plan.Plan_Infrared _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Infrared;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.红外数据比对试验, _FaName, _Node.Index);
                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_ErrAccord)          //误差一致性
                        {
                            CLDC_DataCore.Model.Plan.Plan_ErrAccord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_ErrAccord;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.误差一致性, _FaName, _Node.Index);

                        }
                        if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_PowerConsume)          //误差一致性
                        {
                            CLDC_DataCore.Model.Plan.Plan_PowerConsume _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_PowerConsume;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.功耗试验, _FaName, _Node.Index);

                        }


                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Function)
                        {
                            CLDC_DataCore.Model.Plan.Plan_Function _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Function;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.智能表功能试验, _FaName, _Node.Index);
                        }
                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_EventLog)
                        {
                            CLDC_DataCore.Model.Plan.Plan_EventLog _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_EventLog;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.事件记录试验, _FaName, _Node.Index);
                        }


                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_CostControl)
                        {
                            CLDC_DataCore.Model.Plan.Plan_CostControl _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_CostControl;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.费控功能试验, _FaName, _Node.Index);
                        }
                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Freeze)
                        {
                            CLDC_DataCore.Model.Plan.Plan_Freeze _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Freeze;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.冻结功能试验, _FaName, _Node.Index);
                        }


                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_DataSendForRelay)
                        {
                            CLDC_DataCore.Model.Plan.Plan_DataSendForRelay _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_DataSendForRelay;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.数据转发试验, _FaName, _Node.Index);
                        }

                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_Insulation)
                        {
                            CLDC_DataCore.Model.Plan.Plan_Insulation _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_Insulation;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.工频耐压试验, _FaName, _Node.Index);
                        }
                        else if (_Node.Tag is CLDC_DataCore.Model.Plan.Plan_LoadRecord)
                        {
                            CLDC_DataCore.Model.Plan.Plan_LoadRecord _Item = _Node.Tag as CLDC_DataCore.Model.Plan.Plan_LoadRecord;
                            if (_Item.Count == 0) continue;
                            _Item.SetPram((int)_Ttype, _FaName);
                            _Item.Save();

                            _Plan.Add(CLDC_Comm.Enum.Cus_FAGroup.负荷记录试验, _FaName, _Node.Index);
                        }
                        #endregion
                    }
                    if (_Plan.Count == 0)
                    {
                        MessageBoxEx.UseSystemLocalizedString = true;
                        MessageBoxEx.Show(this,"该方案中没有任何项目，无法完成保存...", "方案保存", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _Plan.Save();
                    this.SetTitleText(_FaName);
                    _FatherNode.Text = _FaName;
                    #region //删除方案(this.LastFAName)

                    CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _NodeOld;

                    _NodeOld = Tv_FaList.SelectedNode as CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode;
                    CLDC_DataCore.Model.Plan.Model_Plan _PlanOld = new CLDC_DataCore.Model.Plan.Model_Plan((int)this._Ttype, this.LastFAName);
                    CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, (int)this._Ttype);
                    for (int i = 0; i < _Plan.Count; i++)
                    {
                        if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.预先调试)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_PREPARE_FOLDERNAME, (int)this._Ttype);
                        }
                        if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.预热试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.外观检查试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_WGJC_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.工频耐压试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_INSULATION_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.起动试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.潜动试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.基本误差试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.走字试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.多功能试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.通讯协议检查试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_CONNPROTOCOL_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.影响量试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_TS_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.载波试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_ZB_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.红外数据比对试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.误差一致性)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_YZX_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.功耗试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME, (int)this._Ttype);
                        }

                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.智能表功能试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_GN_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.费控功能试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_FK_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.冻结功能试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_FZ_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.事件记录试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.数据转发试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_DATASEND_FOLDERNAME, (int)this._Ttype);
                        }
                        else if (_Plan.getFAPrj(i).FAType == CLDC_Comm.Enum.Cus_FAGroup.负荷记录试验)
                        {
                            CLDC_DataCore.Model.Plan.Model_Plan.RemoveFA(this.LastFAName, CLDC_DataCore.Const.Variable.CONST_FA_LOADRECORD_FOLDERNAME, (int)this._Ttype);
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 节点编辑前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tv_FaList_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            this.LastFAName = e.Node.Text;
            Tb_Copy_Click(sender, new EventArgs());
        }
        /// <summary>
        /// 点击节点以后
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
        /// 右键点击重命名以后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4Rename_Click(object sender, EventArgs e)
        {
            Tv_FaList.LabelEdit = true;
            Tv_FaList.SelectedNode.BeginEdit();
        }

        #region 对方案进行排序
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
        /// 不排序的标志
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
        #endregion 对方案进行排序
    }
}
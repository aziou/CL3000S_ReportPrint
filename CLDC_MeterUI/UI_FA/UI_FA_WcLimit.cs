using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.Xml;
using CLDC_MeterUI.UI_FA.FAPrj.PrjUI;
namespace CLDC_MeterUI.UI_FA
{
    
    public partial class UI_FA_WcLimit : Office2007Form
    {
        /// <summary>
        /// 快速设置枚举类型
        /// </summary>
        protected enum Enum_FaseSetup
        { 
        
            全部设置相同=0,

            相同功率因素设置相同=1,

            相同电流倍数设置相同=2,

            全部设置统一比例=3,

            相同功率因素统一比例 = 4,

            相同电流倍数统一比例 = 5,
        
        }



        private CLDC_DataCore.DataBase.clsWcLimitDataControl WcLimit;


        private bool DefaultShow=false;

        /// <summary>
        /// 台体类型    0-三相台，1-单相台
        /// </summary>
        private int _TaiType = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        public UI_FA_WcLimit(int TaiType)
        {
            _TaiType = TaiType;
            WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();
            InitializeComponent();
            this.DefaultControlStyle();
        }


        public UI_FA_WcLimit() 
        {
            InitializeComponent();
            WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();
            this.DefaultControlStyle();
        }

        /// <summary>
        /// 初始化控制样式
        /// </summary>
        private void DefaultControlStyle()
        {
            WcLimitSetup_H = new WcLimitSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.H,WcLimit);
            WcLimitSetup_A = new WcLimitSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.A,WcLimit);
            WcLimitSetup_B = new WcLimitSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.B,WcLimit);
            WcLimitSetup_C = new WcLimitSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.C,WcLimit);
            //WcLimitSetup_H.PointCountChange += new WcLimitSetup.EventPointCountChange(WcLimitSetup_H_PointCountChange);
            //WcLimitSetup_A.PointCountChange += new WcLimitSetup.EventPointCountChange(WcLimitSetup_A_PointCountChange);
            //WcLimitSetup_B.PointCountChange += new WcLimitSetup.EventPointCountChange(WcLimitSetup_B_PointCountChange);
            //WcLimitSetup_C.PointCountChange += new WcLimitSetup.EventPointCountChange(WcLimitSetup_C_PointCountChange);
            this.Tab_H.Controls.Add(this.WcLimitSetup_H);
            this.Tab_A.Controls.Add(this.WcLimitSetup_A);
            this.Tab_B.Controls.Add(this.WcLimitSetup_B);
            this.Tab_C.Controls.Add(this.WcLimitSetup_C);
            WcLimitSetup_H.Margin = new System.Windows.Forms.Padding(0);
            WcLimitSetup_H.Dock = DockStyle.Fill;
            WcLimitSetup_A.Margin = new System.Windows.Forms.Padding(0);
            WcLimitSetup_A.Dock = DockStyle.Fill;
            WcLimitSetup_B.Margin = new System.Windows.Forms.Padding(0);
            WcLimitSetup_B.Dock = DockStyle.Fill;
            WcLimitSetup_C.Margin = new System.Windows.Forms.Padding(0);
            WcLimitSetup_C.Dock = DockStyle.Fill;

            if (_TaiType == 1)
            {
                this.Tab_A.Parent = null;
                this.Tab_B.Parent = null;
                this.Tab_C.Parent = null;
            }
            else
            {
                this.Tab_A.Parent = this.Tab_FA;
                this.Tab_B.Parent = this.Tab_FA;
                this.Tab_C.Parent = this.Tab_FA;
            }
        
            Cmb_FastSetup.Items.Clear();
            for (int i = 0; i < 6; i++)
            {
                Cmb_FastSetup.Items.Add(((Enum_FaseSetup)i).ToString());
            }
            Cmb_FastSetup.SelectedIndex = (int)Enum_FaseSetup.全部设置统一比例;

        }



        /// <summary>
        /// 修改或者新增一个内控误差限名称事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tvw_Limit_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            Tvw_Limit.LabelEdit = false;

            if ((e.Label == null || e.Label == "") && Tvw_Limit.Nodes[Tvw_Limit.Nodes.Count - 1].Text == "请输入新的内控误差限名称")
            {
                Tvw_Limit.Nodes.RemoveAt(Tvw_Limit.Nodes.Count - 1);
                Tvw_Limit.SelectedNode = null;
                this.SetWcParmControl(false);
                return;
            }
            if (Tvw_Limit.Nodes[Tvw_Limit.Nodes.Count - 1].Text == "请输入新的内控误差限名称")
            {
                //this.Ltv_Prj.Items.Clear();
                ////return;
            }
            //如果修改完成，进行表单创建和修改
            CLDC_DataCore.DataBase.IDAndValue _NewName = new CLDC_DataCore.DataBase.IDAndValue();
            _NewName.id =WcLimit.InsertWcLimitName(e.Label.ToString());

            if (_NewName.id == -1)      //如果ID=-1则表示写入数据库失败
            {
                MessageBoxEx.Show(this,"增加误差限失败...", "插入失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Tvw_Limit.Nodes.RemoveAt(Tvw_Limit.Nodes.Count - 1);
                Tvw_Limit.SelectedNode = null;
                this.SetWcParmControl(false);
                return;
            }
            
            _NewName.Value = e.Label.ToString();
            
            Tvw_Limit.Nodes[Tvw_Limit.Nodes.Count - 1].Tag = _NewName;

            SetWcLimitInfo(_NewName);
            Lab_Info.Text = string.Format("内控误差限文件数量：  {0:D} 个", Tvw_Limit.Nodes.Count);

        }
        /// <summary>
        /// 添加一个内控误差限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tool_Add_Click(object sender, EventArgs e)
        {
            if (Tvw_Limit.LabelEdit)
                return;
            Tvw_Limit.LabelEdit = true;
            TreeNode _node = Tvw_Limit.Nodes.Add("请输入新的内控误差限名称");
            _node.ImageIndex = 0;
            _node.SelectedImageIndex = 0;
            Tvw_Limit.SelectedNode = _node;
            _node.BeginEdit();
        }

        /// <summary>
        /// 删除一个误差限文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Tool_Del_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tvw_Limit.Nodes.Count < 1)
            {
                MessageBoxEx.Show(this,"当前没有配置任何误差方案", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Tvw_Limit.SelectedNode == null)
            {
                MessageBoxEx.Show(this,"当前没有选中任何误差方案", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.Show(this,"请问是否确认删除误差限文件：" + Tvw_Limit.SelectedNode.Text + "？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //WcLimit.Remove();
            if (!WcLimit.RemoveWcLimitName(Tvw_Limit.SelectedNode.Text))
            {
                MessageBoxEx.Show(this,"删除误差限文件失败...", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Tvw_Limit.Nodes.Remove(Tvw_Limit.SelectedNode);
            Tvw_Limit.SelectedNode = null;
            this.SetWcParmControl(false);
            Lab_Info.Text = string.Format("内控误差限文件数量：  {0:D} 个", Tvw_Limit.Nodes.Count);

        }


        /// <summary>
        /// 信息加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_FA_WcLimit_Load(object sender, EventArgs e)
        {
            CLDC_DataCore.DataBase.clsWcLimitDataControl _Wc=new CLDC_DataCore.DataBase.clsWcLimitDataControl();

            List<CLDC_DataCore.DataBase.IDAndValue> _WcLimitNames =_Wc.WcLimitName();

            for (int i = 0; i < _WcLimitNames.Count; i++)
            {
                TreeNode _Node=Tvw_Limit.Nodes.Add(_WcLimitNames[i].Value);
                _Node.Tag = _WcLimitNames[i];
            }
            if (Tvw_Limit.Nodes.Count > 0)
            {
                Tvw_Limit.SelectedNode=Tvw_Limit.Nodes[0];
                this.SetWcParmControl(true);
                this.SetWcLimitInfo((CLDC_DataCore.DataBase.IDAndValue)Tvw_Limit.SelectedNode.Tag);
            }
            Lab_Info.Text = string.Format("内控误差限文件数量：  {0:D} 个", Tvw_Limit.Nodes.Count);
        }


        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_FA_WcLimit_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Tvw_Limit.SelectedNode == e.Node) return;           //如果是点击的是当前选中节点，则退出
            Tvw_Limit.SelectedNode = e.Node;

            if (Tvw_Limit.SelectedNode.IsEditing) return;           //如果是在编辑状态就直接返回

            SetWcLimitInfo((CLDC_DataCore.DataBase.IDAndValue)e.Node.Tag);
        }

        /// <summary>
        /// 保存方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Tool_Save_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            MessageBoxEx.Show(this,"内控误差限：" + Tvw_Limit.SelectedNode.Text + "保存成功！", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Lab_Info.Text = string.Format("内控误差限文件数量：  {0:D} 个", Tvw_Limit.Nodes.Count);
        }

        /// <summary>
        /// 设置生成误差限参数控件是否可显示
        /// </summary>
        /// <param name="Visible"></param>
        private void SetWcParmControl(bool Visible)
        {
            if (!Visible)
            {
                Lab_Dj.Visible = false;
                Lab_GuiCheng.Visible = false;
                Lab_Tmp1.Visible = false;
                Lab_Tmp2.Visible = false;
                Lab_Tmp3.Visible = false;
                Lab_Hgq.Visible = false;
                Lab_YouGong.Visible = false;
                Cmb_Dj.Visible = false;
                Cmb_GuiCheng.Visible = false;
                Cmb_YouGong.Visible = false;
                Cmb_Hgq.Visible = false;
                ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).Clear();
                Tab_FA.Enabled = false;

            }
            else
            {
                Lab_Dj.Visible = true;
                Lab_GuiCheng.Visible = true;
                Lab_Tmp1.Visible = true;
                Lab_Tmp2.Visible = true;
                Lab_Tmp3.Visible = true;
                Lab_Hgq.Visible = true;
                Lab_YouGong.Visible = true;
                Cmb_Dj.Visible = true;
                Cmb_GuiCheng.Visible = true;
                Cmb_YouGong.Visible = true;
                Cmb_Hgq.Visible = true;
                Tab_FA.Enabled = true;
            }
        }

        /// <summary>
        /// 初始化ComboBoxEx
        /// </summary>
        /// <param name="Xml_Limit"></param>
        private void SetWcLimitInfo(CLDC_DataCore.DataBase.IDAndValue WcFileName)
        {
            DefaultShow = true;
            this.SetWcParmControl(true);
            #region ----------------------填充规程ComboBoxEx------------------------------------
            List<CLDC_DataCore.DataBase.IDAndValue> _GuiChengNames = WcLimit.GuiChengNames();
            Cmb_GuiCheng.Items.Clear();
            for (int i = 0; i < _GuiChengNames.Count; i++)
            {
                Cmb_GuiCheng.Items.Add(_GuiChengNames[i]);
            }
            Cmb_GuiCheng.SelectedIndex = 0;
            _GuiChengNames = null;
            #endregion


            #region ----------------------------填充等级ComboBoxEx----------------------

            List<CLDC_DataCore.DataBase.IDAndValue> _DjStrings = WcLimit.DjNames();
            Cmb_Dj.Items.Clear();
            CLDC_DataCore.DataBase.IDAndValue _Tmp = new CLDC_DataCore.DataBase.IDAndValue();
            _Tmp.id = -1;
            _Tmp.Value = "--新增--";
            Cmb_Dj.Items.Add(_Tmp);
            for (int i = 0; i < _DjStrings.Count; i++)
            {
                Cmb_Dj.Items.Add(_DjStrings[i]);
            }
            Cmb_Dj.SelectedIndex = 1;
            
            #endregion

            Cmb_Hgq.SelectedIndex = 0;

            Cmb_YouGong.SelectedIndex = 1;

            DefaultShow = false;

            ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).SetWcx(WcFileName
                                                                  , (CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem
                                                                  , (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem
                                                                  , Cmb_Hgq.SelectedIndex == 0 ? false : true
                                                                  , Cmb_YouGong.SelectedIndex == 0 ? false : true);

            Txt_Pc.Text=WcLimit.getPcx(WcFileName, (CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem, (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem); 
        }

        private void Tab_FA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tvw_Limit.SelectedNode == null || Cmb_GuiCheng.SelectedItem == null) return;
            CLDC_DataCore.Function.TopWaiting.ShowWaiting();
            ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).SetWcx((CLDC_DataCore.DataBase.IDAndValue)Tvw_Limit.SelectedNode.Tag
                                                                  , (CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem
                                                                  , (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem
                                                                  , Cmb_Hgq.SelectedIndex == 0 ? false : true
                                                                  , Cmb_YouGong.SelectedIndex == 0 ? false : true);
        }

        /// <summary>
        /// 规程下拉列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_GuiCheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_GuiCheng.Text.ToUpper().Substring(0, 6) == "JJG596")
            {
                Cmb_YouGong.Visible = false;
                Lab_YouGong.Visible = false;
                Lab_Tmp3.Visible = false;
                Cmb_YouGong.SelectedIndex = 1;
            }
            else
            {
                Cmb_YouGong.Visible = true;
                Lab_YouGong.Visible = true;
                Lab_Tmp3.Visible = true;
            }
            if (DefaultShow) return;
            Lock(true);
            ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).GuiChengName =(CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem;
            Txt_Pc.Text = WcLimit.getPcx((CLDC_DataCore.DataBase.IDAndValue)Tvw_Limit.SelectedNode.Tag, (CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem, (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem); 
            Lock(false);


        }
        /// <summary>
        /// 等级下拉列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Dj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_Dj.SelectedIndex == 0)
            {
                Cmb_Dj.DropDownStyle = ComboBoxStyle.DropDown;
                return;
            }
            else
            {
                Cmb_Dj.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            if (DefaultShow) return;
            Lock(true);
            ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).Dj = (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem;
            Txt_Pc.Text = WcLimit.getPcx((CLDC_DataCore.DataBase.IDAndValue)Tvw_Limit.SelectedNode.Tag, (CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem, (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem); 
            Lock(false);
        }
        /// <summary>
        /// 有无功下拉列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_YouGong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Cmb_YouGong.Visible) return;
            Lock(true);
            ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).Yg = Cmb_YouGong.SelectedIndex == 1 ? true : false;
            Lock(false);
        }
        /// <summary>
        /// 是否经互感器下拉列表选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Hgq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DefaultShow) return;

            Lock (true);
            ((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]).Hgq = Cmb_Hgq.SelectedIndex == 1 ? true : false;
            Lock (false);
        }


        private void Lock(bool Value)
        {
            Tab_FA.Enabled = Value?false:true;
        }

        /// <summary>
        /// 新增等级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Dj_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if(e.KeyChar=='\r')
            {
                
                if (Cmb_Dj.Text == "--新增--")
                {
                    Cmb_Dj.SelectedIndex = 1;
                    Cmb_Dj.DropDownStyle = ComboBoxStyle.DropDownList;
                    return;
                }
                if (!CLDC_DataCore.Function.Number.IsNumeric(Cmb_Dj.Text))
                {
                    MessageBoxEx.Show(this,"对不起，你输入的等级不是一个数字，操作被取消...", "新增操作", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cmb_Dj.SelectedIndex = 1;
                    Cmb_Dj.DropDownStyle = ComboBoxStyle.DropDownList;
                    return;
                }

                CLDC_DataCore.DataBase.IDAndValue _NewDj=new CLDC_DataCore.DataBase.IDAndValue();
                _NewDj.id = WcLimit.InsertDjName(Cmb_Dj.Text);
                if (_NewDj.id == -1)
                {
                    Cmb_Dj.SelectedIndex = 1;
                    Cmb_Dj.DropDownStyle = ComboBoxStyle.DropDownList;
                    return;
                }
                _NewDj.Value = Cmb_Dj.Text;
                Cmb_Dj.Items.Add(_NewDj);
                Cmb_Dj.SelectedItem = _NewDj;

            }
        }
        
        /// <summary>
        /// 偏差限修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Pc_Leave(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (this.Tvw_Limit.SelectedNode == null || this.Tvw_Limit.SelectedNode.Tag == null)
            {
                return;
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Pc.Text))
            {
                MessageBoxEx.Show(this,"偏差限必须为大于零的一个数字...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_Pc.Text = WcLimit.getPcx((CLDC_DataCore.DataBase.IDAndValue)Tvw_Limit.SelectedNode.Tag
                                           , (CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem
                                           , (CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem);
                return;
            }

   
            WcLimit.SavePcx(((CLDC_DataCore.DataBase.IDAndValue)Tvw_Limit.SelectedNode.Tag).id
                            , ((CLDC_DataCore.DataBase.IDAndValue)Cmb_GuiCheng.SelectedItem).id
                            , ((CLDC_DataCore.DataBase.IDAndValue)Cmb_Dj.SelectedItem).id, float.Parse(Txt_Pc.Text));
        }

        private void Txt_Pc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')     //如果是回车
            {
                this.Txt_Pc_Leave(sender, new EventArgs());
            }
        }

        private void Cmb_FastSetup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_Item.Items.Clear();
            if (Cmb_FastSetup.SelectedIndex == -1)
            {
                Cmb_Item.Items.Clear();
                return;
            }
            if (Cmb_FastSetup.SelectedIndex == 0 || Cmb_FastSetup.SelectedIndex == 3)       //全部
            {
                Cmb_Item.Items.Add("All");
            }

            if (Cmb_FastSetup.SelectedIndex == 1 || Cmb_FastSetup.SelectedIndex == 4)        //功率因素
            {
                Cmb_Item.Items.AddRange(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName().ToArray());

            }

            if (Cmb_FastSetup.SelectedIndex == 2 || Cmb_FastSetup.SelectedIndex == 5)            //电流倍数
            {
                CLDC_DataCore.SystemModel.Item.csxIbDic _xIb = new CLDC_DataCore.SystemModel.Item.csxIbDic();
                _xIb.Load();
                Cmb_Item.Items.AddRange(_xIb.getxIb().ToArray());

                _xIb = null;
            }

            Cmb_Item.SelectedIndex = 0;
        }

        private void Cmd_Setup_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Cmb_FastSetup.SelectedIndex < 0) return;
            
            if(Txt_WcLimit.Text=="") return;

            if (Cmb_FastSetup.SelectedIndex < 3)                //小于3才是统一设置
            {
                string[] _TmpValue = Txt_WcLimit.Text.Split('|');
                if (_TmpValue.Length != 2)
                {
                    MessageBoxEx.Show(this,"修改的误差限格式不正确，正确的格式为[误差上限|误差下限],例：+0.1|-0.1");
                    Txt_WcLimit.Focus();
                    Txt_WcLimit.Select(0, Txt_WcLimit.Text.Length);
                    return;
                }
                if (!CLDC_DataCore.Function.Number.IsNumeric(_TmpValue[0].Replace("+", "")) 
                        || !CLDC_DataCore.Function.Number.IsNumeric(_TmpValue[1].Replace("+", "")))
                {
                    MessageBoxEx.Show(this,"修改的误差限格式不正确，正确的格式为[误差上限|误差下限],例：+0.1|-0.1");
                    Txt_WcLimit.Focus();
                    Txt_WcLimit.Select(0, Txt_WcLimit.Text.Length);
                    return;
                }
            }
            else//大于等于3是统一比例设置
            {

                if (Txt_WcLimit.Text.Substring(Txt_WcLimit.Text.Length - 1) != "%" 
                        || !CLDC_DataCore.Function.Number.IsNumeric(Txt_WcLimit.Text.Replace("%","")))
                {
                    MessageBoxEx.Show(this,"请输入正确的比例，比例格式为\"数字%\"例：70%");
                    Txt_WcLimit.Focus();
                    Txt_WcLimit.Select(0, Txt_WcLimit.Text.Length);
                    return;
                }
            }
            CLDC_DataCore.Function.TopWaiting.ShowWaiting();
            WcLimitSetup _LimitControl=((WcLimitSetup)Tab_FA.SelectedTab.Controls[0]);
            switch ((Enum_FaseSetup)Cmb_FastSetup.SelectedIndex)
            { 
                case Enum_FaseSetup.全部设置相同:
                    for(int i=0;i<_LimitControl.ColumnsCount;i++)
                        for (int j = 0; j < _LimitControl.RowsCount; j++)
                        {
                            _LimitControl.SetCellValue(j, i, Txt_WcLimit.Text);
                        
                        }
                    break;
                case Enum_FaseSetup.相同功率因素设置相同:
                    for (int i = 0; i < _LimitControl.RowsCount; i++)
                    {
                        _LimitControl.SetCellValue(i, Cmb_Item.SelectedIndex, Txt_WcLimit.Text);
                    }
                    break;
                case Enum_FaseSetup.相同电流倍数设置相同:
                    for(int i=0;i<_LimitControl.ColumnsCount;i++)
                    {
                        _LimitControl.SetCellValue(Cmb_Item.SelectedIndex,i,Txt_WcLimit.Text);
                    }
                    break;
                case Enum_FaseSetup.全部设置统一比例:
                    {
                        float _BlValue = float.Parse(Txt_WcLimit.Text.Replace("%", "")) / 100F;
                        for (int i = 0; i < _LimitControl.ColumnsCount; i++)
                            for (int j = 0; j < _LimitControl.RowsCount; j++)
                            {
                                string[] _TmpValue = _LimitControl.getCellValue(j, i).Split('|');

                                if (_TmpValue.Length != 2) return;

                                _TmpValue[0] = (float.Parse(_TmpValue[0].Replace("+", "")) * _BlValue).ToString();
                                _TmpValue[1] = (float.Parse(_TmpValue[1].Replace("+", "")) * _BlValue).ToString();
                                if (float.Parse(_TmpValue[0]) > 0)
                                    _TmpValue[0] = "+" + _TmpValue[0];
                                if (float.Parse(_TmpValue[1]) > 0)
                                    _TmpValue[1] = "+" + _TmpValue[1];
                                _LimitControl.SetCellValue(j, i, string.Join("|", _TmpValue));

                            }
                        break;
                    }
                case Enum_FaseSetup.相同功率因素统一比例:
                    {
                        float _BlValue = float.Parse(Txt_WcLimit.Text.Replace("%", "")) / 100F;
                        for (int i = 0; i < _LimitControl.RowsCount; i++)
                        {
                            string[] _TmpValue = _LimitControl.getCellValue(i, Cmb_Item.SelectedIndex).Split('|');

                            if (_TmpValue.Length != 2) return;

                            _TmpValue[0] = (float.Parse(_TmpValue[0].Replace("+", "")) * _BlValue).ToString();
                            _TmpValue[1] = (float.Parse(_TmpValue[1].Replace("+", "")) * _BlValue).ToString();
                            if (float.Parse(_TmpValue[0]) > 0)
                                _TmpValue[0] = "+" + _TmpValue[0];
                            if (float.Parse(_TmpValue[1]) > 0)
                                _TmpValue[1] = "+" + _TmpValue[1];

                            _LimitControl.SetCellValue(i, Cmb_Item.SelectedIndex, string.Join("|", _TmpValue));

                        }
                        break;
                    }
                case Enum_FaseSetup.相同电流倍数统一比例:
                    {
                        float _BlValue = float.Parse(Txt_WcLimit.Text.Replace("%", "")) / 100F;
                        for (int i = 0; i < _LimitControl.ColumnsCount; i++)
                        {
                            string[] _TmpValue = _LimitControl.getCellValue(Cmb_Item.SelectedIndex, i).Split('|');

                            if (_TmpValue.Length != 2) return;

                            _TmpValue[0] = (float.Parse(_TmpValue[0].Replace("+", "")) * _BlValue).ToString();
                            _TmpValue[1] = (float.Parse(_TmpValue[1].Replace("+", "")) * _BlValue).ToString();
                            if (float.Parse(_TmpValue[0]) > 0)
                                _TmpValue[0] = "+" + _TmpValue[0];
                            if (float.Parse(_TmpValue[1]) > 0)
                                _TmpValue[1] = "+" + _TmpValue[1];

                            _LimitControl.SetCellValue(Cmb_Item.SelectedIndex, i, string.Join("|", _TmpValue));

                        }
                        break;
                    }
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();

        }

        private void UI_FA_WcLimit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WcLimit != null) WcLimit.Close();
            WcLimit = null;
        }



    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using CLDC_DataCore.SystemModel;
using CLDC_DataCore.SystemModel.Item;
using CLDC_DataCore.Struct;
namespace CLDC_MeterUI
{
    public partial class UI_SystemManager : Office2007Form
    {
        private static UI_SystemManager instance;
        private static object syncRoot = new Object();
        public static UI_SystemManager Instance(SystemInfo Item)
        {

            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new UI_SystemManager(Item);
                    }
                }
                return instance;
            }
        }
        public void ShowD()
        {
            if (instance != null)
            {
                if (instance.Visible != true)
                {
                    instance.ShowDialog();
                }

            }
        }

        private SystemInfo _SystemCol;
        /// <summary>
        /// 功率因素角度配置文本框列表
        /// </summary>
        private List<TextBox> Txt_GlysCol;
        /// <summary>
        /// 是否是新增功率因素的窗体控制描述
        /// </summary>
        private bool bln_Addin = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Item">系统信息对象</param>
        public UI_SystemManager(SystemInfo Item)
        {
            InitializeComponent();
            this.TopMost = true;
            string strComName = "";
            List<string> listComName = new List<string>();
            for (int i = 1; i <= 64; i++)
            {
                strComName = string.Format("COM{0}", i);
                listComName.Add(strComName);
            }
            cbo_Com.DataSource = listComName;


            Lab_xIbRem.Text = "在列表中选中图标或文字可修改或删除电流倍数，也可在空白处点击鼠标\r\n右键选择添加新的电流倍数...";
            this.SystemCol = Item;
            this.initTxtBox();  //初始化功率因素角度配置表格    
            this.Txt_Name.Tag = "";
            this.txt_ZbName.Tag = "";
            this.txt_DataFlagName.Tag = "";

            Lst_Dgn.Click -= new EventHandler(Lst_Dgn_Click);
            Cmd_DgnEdit.Click -= new EventHandler(Cmd_DgnEdit_Click);
            Lst_Dgn.Click += new EventHandler(Lst_Dgn_Click);
            Cmd_DgnEdit.Click += new EventHandler(Cmd_DgnEdit_Click);
        }

        /// <summary>
        /// 系统信息模型赋值
        /// </summary>
        private SystemInfo SystemCol
        {
            get
            {
                return _SystemCol;
            }
            set
            {
                _SystemCol = value;
                this.DefaultSystemGrid(_SystemCol.SystemMode);                                                        //初始化系统信息列表
                this.DefaultUserGrid(_SystemCol.UserGroup);                                                                  //初始化用户列表             
                this.DefaultDicGrid(_SystemCol.ZiDianGroup);                                                                 //初始化字典列表 
                this.DefaultGlysDic(_SystemCol.GlysZiDian);                                                                     //初始化功率因素列表
                this.DefaultxIbDic(_SystemCol.xIbDic);                                                                                //初始化电流倍数字典
                this.DefaultDgnDic(_SystemCol.DgnDicInfo);                                                                //初始化多功能项目参数字典
                this.DefaultZaiboGrid(_SystemCol.ZaiBoInfo);                                                                 //初始化载波协议方案列表
                this.DefaultDataFlagGrid(_SystemCol.DataFlagInfo);                                                    //初始化数据标识列表   
                this.DefaultMethodBasisGrid(_SystemCol.methodAndBasis);                                  //初始化实验方法与依据
                this.DefaultTestSettingGrid(_SystemCol.testSetting);                                                   //初始化实验参数
            }
        }
        /// <summary>
        /// 确认保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            this.SaveSystemGridInfo();
            this.SaveMethodBasisGridInfo();
            this.SaveTestSettingGridInfo();
            _SystemCol.Save();
            this.Close();
        }
        /// <summary>
        /// 直接退出，不保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            _SystemCol.Load();

        }
        /// <summary>
        /// TAB切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_Control_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Text)
            {
                #region
                case "多功能项目":
                case "实验参数": //多功能项目配置
                    {
                        Cmb_Glys.Items.Clear();
                        Cmb_GlFX.Items.Clear();
                        Cmb_Yj.Items.Clear();
                        Cmb_xIb.Items.Clear();
                        for (int i = 1; i < 5; i++)
                        {
                            CLDC_Comm.Enum.Cus_PowerFangXiang _p = (CLDC_Comm.Enum.Cus_PowerFangXiang)i;
                            Cmb_GlFX.Items.Add(_p.ToString());
                        }
                        for (int i = 1; i < 5; i++)
                        {
                            CLDC_Comm.Enum.Cus_PowerYuanJian _p = (CLDC_Comm.Enum.Cus_PowerYuanJian)i;
                            Cmb_Yj.Items.Add(_p.ToString());
                        }
                        List<string> _Tmp = _SystemCol.GlysZiDian.getGlysName();
                        for (int i = 0; i < _Tmp.Count; i++)
                        {
                            Cmb_Glys.Items.Add(_Tmp[i]);
                        }
                        _Tmp = _SystemCol.xIbDic.getxIb();

                        for (int i = 0; i < _Tmp.Count; i++)
                        {
                            Cmb_xIb.Items.Add(_Tmp[i]);
                        }
                        Cmb_xIb.Items.Add("0Ib");
                        _Tmp = null;

                        break;
                    }
                #endregion
                case "误差限配置": //误差限配置
                    {
                        //Cmb_WcLimitGC.Items.Clear();
                        //Cmb_WcLimitGlys.Items.Clear();
                        //Cmb_WcLimitDj.Items.Clear();
                        //Cmb_WcLimitYj.Items.Clear();
                        //List<string> _TmpList = _SystemCol.WcLimit.getGuiChengName();
                        //for (int i = 0; i < _TmpList.Count; i++)
                        //    Cmb_WcLimitGC.Items.Add(_TmpList[i]);
                        //_TmpList = null;
                        //_TmpList = _SystemCol.GlysZiDian.getGlysName();
                        //for (int i = 0; i < _TmpList.Count; i++)
                        //    Cmb_WcLimitGlys.Items.Add(_TmpList[i]);
                        //_TmpList = null;
                        //Cmb_WcLimitYj.Items.AddRange(new object[]{"H元","A元","B元","C元"});
                        //Cmb_WcLimitDj.Items.AddRange(new object[] { "0.2", "0.5", "1.0", "2.0", "3.0" });
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 窗体大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_SystemManager_SizeChanged(object sender, EventArgs e)
        {
            System .Drawing .Size  minSize=new Size (503, 466);
            if (this.Size.Width < minSize.Width || this.Size.Height < minSize.Height)
            {
                this.Size = minSize;
            }
        }
        /// <summary>
        /// 仅仅显示用户管理
        /// </summary>
        public void OnlyShowUserInfo()
        {
            int tabCount = Tab_Control.TabPages.Count;
            for (int i = tabCount - 1; i >= 0; i--)
            {
                if (Tab_Control.TabPages[i].Name != "Page_User")
                {
                    Tab_Control.TabPages.Remove(Tab_Control.TabPages[i]);
                }
            }
            ShowDialog();
        }


        /// <summary>
        /// 仅显示系统配置Tab
        /// </summary>
        /// <param name="OnlySetInfo"></param>
        public void Show(bool OnlySetInfo)
        {
            if (!OnlySetInfo)
            {
                this.Show();
                return;
            }
            for (int i = Tab_Control.TabPages.Count - 1; i > 0; i--)            //将其他部分移除掉
            {
                Tab_Control.TabPages.RemoveAt(i);
            }
            this.Show();
        }

        public void Show(bool OnlySetInfo, bool ShowModel)
        {
            if (!OnlySetInfo)
            {
                if (ShowModel)
                {
                    this.ShowDialog();
                }
                else
                {
                    this.Show();
                }
                return;
            }
            for (int i = Tab_Control.TabPages.Count - 1; i > 0; i--)            //将其他部分移除掉
            {
                Tab_Control.TabPages.RemoveAt(i);
            }

            if (ShowModel)
            {
                this.ShowDialog();
            }
            else
            {
                this.Show();
            }

        }

        #region 系统信息配置
        /// <summary>
        /// 初始化系统信息列表
        /// </summary>
        /// <param name="Item">系统配置信息对象</param>
        private void DefaultSystemGrid(SystemConfigure Item)
        {
            List<string> _Keys = Item.getKeyNames();
            SystemProperty.Item.Clear();
            SystemProperty.ShowCustomProperties = true;
            for (int i = 0; i < _Keys.Count; i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = Item.getItem(_Keys[i]);
                SystemProperty.Item.Add(_Item.Name, _Item.Value, false, _Item.ClassName, _Item.Description, true);
                SystemProperty.Item[SystemProperty.Item.Count - 1].Tag = _Keys[i];
                string[] _Arr = _Item.DataSource.Split('|');
                if (_Arr.Length > 1)
                    SystemProperty.Item[SystemProperty.Item.Count - 1].Choices = new PropertyGridEx.CustomChoices(_Arr);

            }
            SystemProperty.Refresh();
        }
        /// <summary>
        /// 转化系统配置信息
        /// </summary>
        private void SaveSystemGridInfo()
        {
            _SystemCol.SystemMode.Clear();
            for (int i = 0; i < SystemProperty.Item.Count; i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = new StSystemInfo();
                _Item.Name = SystemProperty.Item[i].Name;
                _Item.Value = SystemProperty.Item[i].Value.ToString();
                _Item.Description = SystemProperty.Item[i].Description;
                _Item.ClassName = SystemProperty.Item[i].Category;
                if (SystemProperty.Item[i].Choices != null)
                {
                    string _TmpString = "";
                    for (int j = 0; j < SystemProperty.Item[i].Choices.Count; j++)
                    {
                        if (j == 0)
                            _TmpString = SystemProperty.Item[i].Choices[j].ToString();
                        else
                            _TmpString = string.Format("{0}|{1}", _TmpString, SystemProperty.Item[i].Choices[j].ToString());
                    }
                    _Item.DataSource = _TmpString;

                }
                else
                {
                    _Item.DataSource = "";
                }
                _SystemCol.SystemMode.Add(SystemProperty.Item[i].Tag.ToString(), _Item);

            }

        }
        #endregion

        #region 用户信息设置
        /// <summary>
        /// 初始化用户管理列表
        /// </summary>
        /// <param name="Item">用户信息对象</param>
        private void DefaultUserGrid(csUserInfo Item)
        {
            List<StUserInfo> _Users = Item.getUsers();
            Lst_User.Items.Clear();
            for (int i = 0; i < _Users.Count; i++)
            {
                InsertListView(_Users[i]);
            }
            _Users = null;
        }

        private void InsertListView(StUserInfo Item)
        {
            ListViewItem _LstItem = new ListViewItem(Item.UserName);
            _LstItem.SubItems.Add(Item.Level == 0 ? "管理员" : "操作员");
            _LstItem.Tag = (string)Item.Pwd;
            _LstItem.ImageIndex = Item.Level;
            Lst_User.Items.Add(_LstItem);
        }
        private void InsertListView(StCarrierInfo Item)
        {
            ListViewItem _LsvItem = new ListViewItem(Item.CarrierName);
            _LsvItem.SubItems.Add(Item.CarrierType);
            _LsvItem.SubItems.Add(Item.RdType);
            _LsvItem.SubItems.Add(Item.CommuType);
            _LsvItem.SubItems.Add(Item.BaudRate);
            _LsvItem.SubItems.Add(Item.Comm);
            _LsvItem.SubItems.Add(Item.CmdTime);
            _LsvItem.SubItems.Add(Item.ByteTime);
            _LsvItem.SubItems.Add(Item.RouterID.ToString("D"));

            lsv_Zaibo.Items.Add(_LsvItem);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_AddUser_Click(object sender, EventArgs e)
        {
            Cmd_Insert.Text = "确认添加";
            Panel_User.Visible = true;

        }
        /// <summary>
        /// 取消添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            Txt_Name.Text = "";
            Txt_Pwd.Text = "";
            Txt_PwdOk.Text = "";
            Txt_Name.Tag = "";
            Cmb_Level.Text = "";
            Chk_Next.Checked = false;
            Panel_User.Visible = false;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Insert_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Txt_Name.Text == "")
            {
                MessageBoxEx.Show(this, "请输入正确的用户名称...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_Name.Focus();
                return;
            }
            if (Txt_Name.Tag.ToString() != Txt_Name.Text && _SystemCol.UserGroup.FindUser(Txt_Name.Text))
            {
                MessageBoxEx.Show(this, "用户名已经存在，请重新输入...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_Name.Text = "";
                Txt_Name.Focus();
                return;
            }

            if (Txt_Pwd.Text != Txt_PwdOk.Text)
            {
                MessageBoxEx.Show(this, "两次密码输入不匹配...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_PwdOk.Text = "";
                Txt_Pwd.Text = "";
                Txt_Pwd.Focus();
                return;
            }
            if (Cmb_Level.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择用户级别...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_Level.Focus();
                return;
            }
            StUserInfo _User = new StUserInfo();
            _User.Level = Cmb_Level.SelectedIndex;
            _User.Pwd = Txt_Pwd.Text;
            _User.UserName = Txt_Name.Text;
            if (Cmd_Insert.Text == "确认编辑")
            {
                _SystemCol.UserGroup.Remove(Txt_Name.Tag.ToString());
                Lst_User.Items.Remove(Lst_User.SelectedItems[0]);
            }
            _SystemCol.UserGroup.Add(_User);

            this.InsertListView(_User);

            if (Txt_Name.Tag.ToString() != "" || !Chk_Next.Checked)
                Cmd_Cancel_Click(sender, e);
            else
            {
                Txt_Name.Text = "";
                Txt_Pwd.Text = "";
                Txt_PwdOk.Text = "";
                Txt_Name.Tag = "";
                Cmb_Level.Text = "";

            }
        }
        /// <summary>
        /// 修改用户信息，包括密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_EditUser_Click(object sender, EventArgs e)
        {
            if (Lst_User.SelectedItems.Count == 0)
                return;
            Panel_User.Visible = true;
            Txt_Name.Text = Lst_User.SelectedItems[0].Text;
            Txt_Name.Tag = Lst_User.SelectedItems[0].Text;
            Txt_Pwd.Text = Lst_User.SelectedItems[0].Tag.ToString();
            Txt_PwdOk.Text = Txt_Pwd.Text;
            Cmb_Level.Text = Lst_User.SelectedItems[0].SubItems[1].Text;
            Cmd_Insert.Text = "确认编辑";
        }
        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_DelUser_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Lst_User.SelectedItems.Count == 0)
                return;
            if (MessageBoxEx.Show(this, "你确认要移除用户名为：" + Lst_User.SelectedItems[0].Text + "的用户吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _SystemCol.UserGroup.Remove(Lst_User.SelectedItems[0].Text);
                Lst_User.Items.Remove(Lst_User.SelectedItems[0]);
            }
        }

        #endregion

        #region 字典表设置
        /// <summary>
        /// 初始化字典列表
        /// </summary>
        /// <param name="Items"></param>
        private void DefaultDicGrid(csDictionary Items)
        {
            List<string> _ItemNames = Items.getZiDianName();
            Tvw_DicName.Nodes.Clear();
            for (int i = 0; i < _ItemNames.Count; i++)
            {
                Tvw_DicName.Nodes.Add("K_" + _ItemNames[i], _ItemNames[i]);
            }
            return;
        }
        private void Tvw_DicName_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            List<string> _Item = _SystemCol.ZiDianGroup.getValues(e.Node.Text);
            Lst_Value.Tag = (string)e.Node.Text;
            Lst_Value.Items.Clear();
            for (int i = 0; i < _Item.Count; i++)
            {
                Lst_Value.Items.Add(_Item[i], 2);
            }
            Tvw_DicName.SelectedNode = e.Node;
            return;
        }
        /// <summary>
        /// 增加一个新的字典值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_InsertDic_Click(object sender, EventArgs e)
        {
            ListViewItem _Item = Lst_Value.Items.Add("请输入一个字典值...", 2);
            _Item.Selected = true;
            _Item.BeginEdit();
        }
        /// <summary>
        /// 编辑字典值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lst_Value_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if ((e.Label == null || e.Label == "") && Lst_Value.SelectedItems[0].Text == "请输入一个字典值...")
            {
                e.CancelEdit = true;
                Lst_Value.Items.Remove(Lst_Value.SelectedItems[0]);
                return;
            }
            if (e.Label == null || e.Label == "")
            {
                e.CancelEdit = true;
                return;
            }
            if (Lst_Value.SelectedItems[0].Text != "请输入一个字典值...")
                _SystemCol.ZiDianGroup.Remove(Lst_Value.Tag.ToString(), Lst_Value.SelectedItems[0].Text);
            _SystemCol.ZiDianGroup.Add(Lst_Value.Tag.ToString(), e.Label, true);
        }
        /// <summary>
        /// 移除一个字典值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_RemoveDic_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Lst_Value.SelectedItems.Count == 0)
                return;
            if (MessageBoxEx.Show(this, "确认删除" + Lst_Value.SelectedItems[0].Text + "?", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            _SystemCol.ZiDianGroup.Remove(Lst_Value.Tag.ToString(), Lst_Value.SelectedItems[0].Text, true);
            Lst_Value.Items.Remove(Lst_Value.SelectedItems[0]);
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Lst_Value.View = View.LargeIcon;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Lst_Value.View = View.Tile;
        }
        #endregion

        #region  功率因素角度设置
        /// <summary>
        /// 初始化功率因素角度配置表样式
        /// </summary>
        private void initTxtBox()
        {
            Txt_GlysCol = new List<TextBox>();
            for (int i = 0; i < 32; i++)
            {
                CLDC_Comm.Enum.Cus_Clfs _Clfs;
                CLDC_Comm.Enum.Cus_Ywg _Ywg = CLDC_Comm.Enum.Cus_Ywg.Q;
                if (i < 8)     //小于8是三相4线，小于4是有功
                {
                    _Clfs = CLDC_Comm.Enum.Cus_Clfs.三相四线;
                    if (i < 4)
                        _Ywg = CLDC_Comm.Enum.Cus_Ywg.P;
                }
                else if (i < 16)   //小于16是三相3线，小于12是有功
                {
                    _Clfs = CLDC_Comm.Enum.Cus_Clfs.三相三线;
                    if (i < 12)
                        _Ywg = CLDC_Comm.Enum.Cus_Ywg.P;
                }
                else if (i < 20)   //小于20是二元件无功跨相60°
                    _Clfs = CLDC_Comm.Enum.Cus_Clfs.二元件跨相60;
                else if (i < 24)   //小于24是二元件无功跨相90°
                    _Clfs = CLDC_Comm.Enum.Cus_Clfs.二元件跨相90;
                else if (i < 28)       //小于28是三元件无功跨相90°
                    _Clfs = CLDC_Comm.Enum.Cus_Clfs.三元件跨相90;
                else//大于等于28是单向
                {
                    _Clfs = CLDC_Comm.Enum.Cus_Clfs.单相;
                    _Ywg = CLDC_Comm.Enum.Cus_Ywg.P;
                }

                CLDC_Comm.Enum.Cus_PowerYuanJian _Yj = (CLDC_Comm.Enum.Cus_PowerYuanJian)((i % 4) + 1);

                TextBox Txt_Glys = new TextBox();
                Txt_Glys.Visible = true;
                Txt_Glys.Dock = DockStyle.Fill;
                Txt_Glys.Name = CLDC_DataCore.SystemModel.Item.csGlys.XID(_Clfs, _Ywg, _Yj);   //文本框的名称等于角度ID
                tableLayoutPanel1.Controls.Add(Txt_Glys);
                Txt_Glys.Margin = new System.Windows.Forms.Padding(0, 6, 0, 4);
                Txt_Glys.TextAlign = HorizontalAlignment.Center;
                Txt_Glys.Leave += new EventHandler(Txt_Glys_Leave);
                Txt_GlysCol.Add(Txt_Glys);
            }
        }
        /// <summary>
        /// 修改角度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Glys_Leave(object sender, EventArgs e)
        {
            TextBox _Txt = (TextBox)sender;
            if (_Txt.Tag == null)
                return;
            if (_Txt.Tag.ToString() == _Txt.Text || _Txt.Text == "")
            {
                _Txt.Text = _Txt.Tag.ToString();
                return;
            }
            _SystemCol.GlysZiDian.EditJiaoDu(Tvw_GLYS.Tag.ToString(), _Txt.Name, _Txt.Text, true);
        }
        /// <summary>
        /// 初始化功率因素TreeView
        /// </summary>
        /// <param name="Item"></param>
        private void DefaultGlysDic(csGlys Item)
        {
            List<string> _Glys = Item.getGlysName();
            Tvw_GLYS.Nodes.Clear();
            for (int i = 0; i < _Glys.Count; i++)
            {
                Tvw_GLYS.Nodes.Add(_Glys[i]);
            }
        }
        /// <summary>
        /// 选择功率因素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tvw_GLYS_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Tvw_GLYS.SelectedNode = e.Node;
            Tvw_GLYS.Tag = e.Node.Text;
            for (int i = 0; i < Txt_GlysCol.Count; i++)
            {

                Txt_GlysCol[i].Text = _SystemCol.GlysZiDian.getJiaoDu(Tvw_GLYS.Tag.ToString()
                                                                    , Txt_GlysCol[i].Name);
                Txt_GlysCol[i].Tag = Txt_GlysCol[i].Text;       //做一个标记
            }
        }
        /// <summary>
        /// 添加新的功率因素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_New_Click(object sender, EventArgs e)
        {
            TreeNode Item = Tvw_GLYS.Nodes.Add("请填写功率因素...");
            Tvw_GLYS.LabelEdit = true;          //打开可编辑模式
            Item.Checked = true;
            Item.BeginEdit();
        }
        /// <summary>
        /// 填写新的功率因素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tvw_GLYS_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Tvw_GLYS.LabelEdit = false;     //关闭可编辑模式
            if ((e.Label == null || e.Label == "") && Tvw_GLYS.Nodes[Tvw_GLYS.Nodes.Count - 1].Text == "请填写功率因素...")
            {
                e.CancelEdit = true;
                Tvw_GLYS.Nodes.RemoveAt(Tvw_GLYS.Nodes.Count - 1);
                return;
            }
            if (!_SystemCol.GlysZiDian.Add(e.Label))
            {
                Tvw_GLYS.Nodes.RemoveAt(Tvw_GLYS.Nodes.Count - 1);
                return;
            }

            Tvw_GLYS.Nodes[Tvw_GLYS.Nodes.Count - 1].Text = e.Label;
            Tvw_GLYS.Nodes[Tvw_GLYS.Nodes.Count - 1].EndEdit(false);
            TreeNodeMouseClickEventArgs _e = new TreeNodeMouseClickEventArgs(Tvw_GLYS.Nodes[Tvw_GLYS.Nodes.Count - 1], MouseButtons.Left, 1, 0, 0);
            this.Tvw_GLYS_NodeMouseClick(null, _e);

            Tvw_GLYS.SelectedNode = Tvw_GLYS.Nodes[Tvw_GLYS.Nodes.Count - 1];
            bln_Addin = true;
            return;
        }

        private void Tvw_GLYS_MouseDown(object sender, MouseEventArgs e)
        {
            if (!bln_Addin)
                return;
            Tvw_GLYS.SelectedNode = Tvw_GLYS.Nodes[Tvw_GLYS.Nodes.Count - 1];
            bln_Addin = false;
            return;
        }
        /// <summary>
        /// 移除一个功率因素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Remove_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Tvw_GLYS.SelectedNode == null)
                return;
            if (MessageBoxEx.Show(this, "是否移除功率因素：" + Tvw_GLYS.SelectedNode.Text + "?", "移除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (_SystemCol.GlysZiDian.Remove(Tvw_GLYS.SelectedNode.Text, true))     //移除一个功率因素并及时保存
            {
                Tvw_GLYS.Nodes.Remove(Tvw_GLYS.SelectedNode);           //移除功率因素成功后，在Treeview中移除显示
                Tvw_GLYS.SelectedNode = Tvw_GLYS.Nodes[0];
                TreeNodeMouseClickEventArgs _e = new TreeNodeMouseClickEventArgs(Tvw_GLYS.Nodes[0], MouseButtons.Left, 1, 0, 0);
                this.Tvw_GLYS_NodeMouseClick(null, _e);
                Tvw_GLYS.Select();
            }
            return;
        }
        #endregion

        #region  电流倍数字典配置
        /// <summary>
        /// 初始化电流倍数字典
        /// </summary>
        /// <param name="Item"></param>
        private void DefaultxIbDic(csxIbDic Item)
        {
            List<string> _xIbs = Item.getxIb();
            Lst_xIbDic.Items.Clear();
            for (int i = 0; i < _xIbs.Count; i++)
            {
                ListViewItem _Lst = new ListViewItem(_xIbs[i], 3);
                Lst_xIbDic.Items.Add(_Lst);
            }
            _xIbs = null;
            return;
        }
        /// <summary>
        /// 鼠标事件，显示右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lst_xIbDic_MouseDown(object sender, MouseEventArgs e)
        {
            if (Lst_xIbDic.SelectedItems.Count == 0)        //如果没有选中的元素
            {
                Menu_xIbAdd.Visible = true;        //显示添加菜单
                Menu_xIbDel.Visible = false;        //隐藏移除菜单
                Menu_xIbEdit.Visible = false;       //隐藏修改菜单
            }
            else
            {
                if (int.Parse(_SystemCol.xIbDic.getxIbID(Lst_xIbDic.SelectedItems[0].Text)) < 15)   //如果ID号小于15则表示是系统默认点，不能被删除
                {
                    Menu_xIbAdd.Visible = false;        //显示添加菜单
                    Menu_xIbDel.Visible = false;        //隐藏移除菜单
                    Menu_xIbEdit.Visible = false;       //隐藏修改菜单
                    return;
                }
                Menu_xIbAdd.Visible = false;
                Menu_xIbDel.Visible = true;
                Menu_xIbEdit.Visible = true;
            }
        }
        /// <summary>
        /// 增加一个新的电流倍数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_xIbAdd_Click(object sender, EventArgs e)
        {
            Lst_xIbDic.LabelEdit = true;
            Lst_xIbDic.Items.Add("请输入一个新的电流倍数", 3).Selected = true;
            Lst_xIbDic.SelectedItems[0].BeginEdit();
        }
        /// <summary>
        /// 增加和编辑电流倍数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lst_xIbDic_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string _EditString = "";
            Lst_xIbDic.LabelEdit = false;

            #region 处理输入的字符串，转化成要求格式的电流倍数
            if (e.Label != null)
            {
                if (e.Label.ToLower().IndexOf("ib") >= 0)
                {
                    _EditString = e.Label.ToLower().Replace("ib", "Ib");
                }
                else if (e.Label.ToLower().IndexOf("imax") >= 0)
                    _EditString = e.Label.ToLower().Replace("imax", "Imax");
                else if (e.Label.ToLower().IndexOf("imax-ib") >= 0)
                    _EditString = e.Label.ToLower().Replace("(imax-ib)", "(Imax-Ib)");
                else
                    if (!CLDC_DataCore.Function.Number.IsNumeric(e.Label))
                        _EditString = "";
                    else
                    {
                        if (e.Label.IndexOf('.') >= 0)
                            _EditString = e.Label + "Ib";
                        else
                            _EditString = string.Format("{0}.0Ib", e.Label);
                    }

            }
            #endregion

            if ((e.Label == null || _EditString == "") && Lst_xIbDic.SelectedItems[0].Text == "请输入一个新的电流倍数")  //如果是新增一个电流倍数，而电流倍数字符串为空或没有值输入，则删除刚才新增的点
            {
                e.CancelEdit = true;
                Lst_xIbDic.Items.Remove(Lst_xIbDic.SelectedItems[0]);
                return;
            }
            if (e.Label == null || _EditString == "")       //如果没有被修改或者修改格式化后的字符串为空，则不进行修改
            {
                e.CancelEdit = true;
                return;
            }
            if (Lst_xIbDic.SelectedItems[0].Text != "请输入一个新的电流倍数")       //如果不是新增则需要删除原来的电流倍数
                _SystemCol.xIbDic.Remove(Lst_xIbDic.SelectedItems[0].Text);
            e.CancelEdit = true;
            Lst_xIbDic.SelectedItems[0].Text = _EditString;
            if (!_SystemCol.xIbDic.Add(_EditString, true))           //如果新增失败，则移除刚才新增的点
                Lst_xIbDic.Items.Remove(Lst_xIbDic.SelectedItems[0]);
            return;
        }
        /// <summary>
        /// 修改电流倍数，按要求只在ID号14以后才能被修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_xIbEdit_Click(object sender, EventArgs e)
        {
            if (int.Parse(_SystemCol.xIbDic.getxIbID(Lst_xIbDic.SelectedItems[0].Text)) < 15)   //如果ID号小于15则表示是系统默认点，不能被修改
            {
                MessageBoxEx.UseSystemLocalizedString = true;
                MessageBoxEx.Show(this, "系统默认电流倍数不能被编辑...", "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Lst_xIbDic.LabelEdit = true;
            Lst_xIbDic.SelectedItems[0].BeginEdit();        //打开可编辑状态
        }

        /// <summary>
        /// 移除一个电流点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_xIbDel_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (int.Parse(_SystemCol.xIbDic.getxIbID(Lst_xIbDic.SelectedItems[0].Text)) < 15)   //如果ID号小于15则表示是系统默认点，不能被删除
            {
                MessageBoxEx.Show(this, "系统默认电流倍数不能被移除...", "移除失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBoxEx.Show(this, "你确认要移除电流倍数为:" + Lst_xIbDic.SelectedItems[0].Text + "的点吗？", "移除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            _SystemCol.xIbDic.Remove(Lst_xIbDic.SelectedItems[0].Text, true);
            Lst_xIbDic.Items.Remove(Lst_xIbDic.SelectedItems[0]);
        }
        #endregion

        #region   多功能项目字典配置
        
        private void DefaultDgnDic(csDgnDic Item)
        {
            List<StDgnConfig> _Dgn = Item.getDgnPrj();
            Lst_Dgn.Items.Clear();
            Lst_DgnPrjID.Items.Clear();
            for (int i = 0; i < _Dgn.Count; i++)
            {
                Lst_Dgn.Items.Add(_Dgn[i].DgnPrjName);
                Lst_DgnPrjID.Items.Add(_Dgn[i].DgnPrjID);
            }
            Lst_Dgn.Refresh();
        }
        /// <summary>
        /// 选择一个多功能项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lst_Dgn_Click(object sender, EventArgs e)
        {
            if (Lst_Dgn.SelectedIndex == -1)
                return;
            CLDC_DataCore.Struct.StDgnConfig _PowerPram = _SystemCol.DgnDicInfo.getDgnPrj(Lst_DgnPrjID.Items[Lst_Dgn.SelectedIndex].ToString());
            Cmb_GlFX.Text = _PowerPram.OutPramerter.GLFX.ToString();
            Cmb_Yj.Text = _PowerPram.OutPramerter.YJ.ToString();
            Cmb_xU.Text = _PowerPram.OutPramerter.xU.ToString();
            Cmb_xIb.Text = _PowerPram.OutPramerter.xIb;
            Cmb_Glys.Text = _PowerPram.OutPramerter.GLYS;
            Cmd_DgnEdit.Visible = true;
            Lst_DgnPrjID.Tag = _PowerPram;
        }
        /// <summary>
        /// 多功能项目参数修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_DgnEdit_Click(object sender, EventArgs e)
        {
            Cmd_DgnEdit.Visible = false;
            if (Lst_DgnPrjID.Tag == null)
                return;
            StDgnConfig _Dgn = (StDgnConfig)Lst_DgnPrjID.Tag;
            _Dgn.OutPramerter.GLFX = (CLDC_Comm.Enum.Cus_PowerFangXiang)(Cmb_GlFX.SelectedIndex + 1);
            _Dgn.OutPramerter.YJ = (CLDC_Comm.Enum.Cus_PowerYuanJian)(Cmb_Yj.SelectedIndex + 1);
            if (CLDC_DataCore.Function.Number.IsNumeric(Cmb_xU.Text))
                _Dgn.OutPramerter.xU = float.Parse(Cmb_xU.Text);
            else
                _Dgn.OutPramerter.xU = 1F;
            _Dgn.OutPramerter.xIb = Cmb_xIb.Text;
            _Dgn.OutPramerter.GLYS = Cmb_Glys.Text;
            _SystemCol.DgnDicInfo.Add(_Dgn);
            return;
        }
        
        #endregion

        #region 载波协议字典配置
        private void btn_AddZb_Click(object sender, EventArgs e)
        {
            btn_Insert.Text = "确认添加";
            pnl_Zaibo.Visible = true;
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (txt_ZbName.Text == "")
            {
                MessageBoxEx.Show(this, "请输入正确的载波协议名称...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ZbName.Focus();
                return;
            }
            if (txt_ZbName.Tag.ToString() != txt_ZbName.Text && _SystemCol.ZaiBoInfo.FindCarrierInfo(txt_ZbName.Text))
            {
                MessageBoxEx.Show(this, "载波协议名已经存在，请重新输入...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ZbName.Text = "";
                txt_ZbName.Focus();
                return;
            }

            if (cbo_RdType.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择抄表器类型...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo_RdType.Focus();
                return;
            }
            if (Cmb_RouterID.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择路由标识...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_RouterID.Focus();
                return;
            }
            if (cbo_CarrierType.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择通讯介质...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo_CarrierType.Focus();
                return;
            }
            if (cbo_CommuType.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择通讯方式...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo_CommuType.Focus();
                return;
            }

            if (cbo_BaudRate.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择波特率...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo_BaudRate.Focus();
                return;
            }
            if (cbo_Com.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择通讯端口...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo_Com.Focus();
                return;
            }
            if (txt_CmdTime.Text == "" || !CLDC_DataCore.Function.Number.IsNumeric(txt_CmdTime.Text))
            {
                MessageBoxEx.Show(this, "请输入正确的命令延时(ms)，时间为一个大于零的数字", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_CmdTime.Focus();
                return;
            }
            if (txt_ByteTime.Text == "" || !CLDC_DataCore.Function.Number.IsNumeric(txt_ByteTime.Text))
            {
                MessageBoxEx.Show(this, "请输入正确的字节延时(ms)，时间为一个大于零的数字", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ByteTime.Focus();
                return;
            }

            StCarrierInfo _Zaibo = new StCarrierInfo();
            _Zaibo.CarrierName = txt_ZbName.Text;
            _Zaibo.CarrierType = cbo_CarrierType.Text;
            _Zaibo.RdType = cbo_RdType.Text;
            _Zaibo.CommuType = cbo_CommuType.Text;
            _Zaibo.BaudRate = cbo_BaudRate.Text;
            _Zaibo.Comm = cbo_Com.Text;
            _Zaibo.CmdTime = txt_CmdTime.Text;
            _Zaibo.ByteTime = txt_ByteTime.Text;
            _Zaibo.RouterID = (byte)Cmb_RouterID.SelectedIndex;

            if (btn_Insert.Text == "确认编辑")
            {
                _SystemCol.ZaiBoInfo.Remove(txt_ZbName.Tag.ToString());
                lsv_Zaibo.Items.Remove(lsv_Zaibo.SelectedItems[0]);
            }
            _SystemCol.ZaiBoInfo.Add(_Zaibo);

            this.InsertListView(_Zaibo);

            if (txt_ZbName.Tag.ToString() != "" || !chk_Continue.Checked)
                btn_Cancel_Click(sender, e);
            else
            {
                txt_ZbName.Text = "";
                txt_ZbName.Tag = "";
                cbo_CarrierType.Text = "";
                cbo_RdType.Text = "";
                cbo_CommuType.Text = "";
                cbo_BaudRate.Text = "";
                cbo_Com.Text = "";
                //txt_CmdTime.Text = "";
                //txt_ByteTime.Text = "";
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_ZbName.Text = "";
            txt_ZbName.Tag = "";
            cbo_CarrierType.Text = "";
            cbo_RdType.Text = "";
            cbo_CommuType.Text = "";
            cbo_BaudRate.Text = "";
            cbo_Com.Text = "";
            txt_CmdTime.Text = "";
            txt_ByteTime.Text = "";

            chk_Continue.Checked = false;
            pnl_Zaibo.Visible = false;
        }

        private void btn_EditZb_Click(object sender, EventArgs e)
        {
            if (lsv_Zaibo.SelectedItems.Count == 0)
                return;
            pnl_Zaibo.Visible = true;
            txt_ZbName.Text = lsv_Zaibo.SelectedItems[0].Text;
            txt_ZbName.Tag = lsv_Zaibo.SelectedItems[0].Text;
            cbo_CarrierType.Text = lsv_Zaibo.SelectedItems[0].SubItems[1].Text;
            cbo_RdType.Text = lsv_Zaibo.SelectedItems[0].SubItems[2].Text;
            cbo_CommuType.Text = lsv_Zaibo.SelectedItems[0].SubItems[3].Text;
            cbo_BaudRate.Text = lsv_Zaibo.SelectedItems[0].SubItems[4].Text;
            cbo_Com.Text = lsv_Zaibo.SelectedItems[0].SubItems[5].Text;
            txt_CmdTime.Text = lsv_Zaibo.SelectedItems[0].SubItems[6].Text;
            txt_ByteTime.Text = lsv_Zaibo.SelectedItems[0].SubItems[7].Text;

            Cmb_RouterID.SelectedIndex = int.Parse(lsv_Zaibo.SelectedItems[0].SubItems[8].Text);

            btn_Insert.Text = "确认编辑";
        }

        private void btn_DelZb_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (lsv_Zaibo.SelectedItems.Count == 0)
                return;
            if (MessageBoxEx.Show(this, "你确认要移除方案名为：" + lsv_Zaibo.SelectedItems[0].Text + "的载波方案吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _SystemCol.ZaiBoInfo.Remove(lsv_Zaibo.SelectedItems[0].Text);
                lsv_Zaibo.Items.Remove(lsv_Zaibo.SelectedItems[0]);
            }
        }
        /// <summary>
        /// 初始化载波方案列表
        /// </summary>
        /// <param name="Item">载波方案对象</param>
        private void DefaultZaiboGrid(csCarrier Item)
        {
            List<StCarrierInfo> _Zaibos = Item.GetCarrierList();
            lsv_Zaibo.Items.Clear();
            for (int i = 0; i < _Zaibos.Count; i++)
            {
                InsertListView(_Zaibos[i]);
            }
            _Zaibos = null;
        }
        private void pnl_Zaibo_VisibleChanged(object sender, EventArgs e)
        {
            if (pnl_Zaibo.Visible == true)
            {
                List<string> _Item = _SystemCol.ZiDianGroup.getValues("波特率");
                cbo_BaudRate.DataSource = null;
                cbo_BaudRate.Items.Clear();
                if (_Item.Count > 0)
                {
                    CLDC_DataCore.Function.BindCombox.BindComboxItem(cbo_BaudRate, _Item);
                }


            }
        }
        #endregion

        #region 数据标识字典配置

        private void DefaultDataFlagGrid(csDataFlag Item)
        {
            List<StDataFlagInfo> _DataFlags = Item.GetDataFlagList();
            lsv_DataFlag.Items.Clear();
            for (int i = 0; i < _DataFlags.Count; i++)
            {
                InsertListView(_DataFlags[i]);
            }
            _DataFlags = null;
        }
        private void InsertListView(StDataFlagInfo Item)
        {
            ListViewItem _LsvItem = new ListViewItem(Item.DataFlagName);
            _LsvItem.SubItems.Add(Item.DataFlag);
            _LsvItem.SubItems.Add(Item.DataLength);
            _LsvItem.SubItems.Add(Item.DataSmallNumber);
            _LsvItem.SubItems.Add(Item.DataFormat);

            lsv_DataFlag.Items.Add(_LsvItem);
        }

        private void btn_DataFlagInsert_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (txt_DataFlagName.Text == "")
            {
                MessageBoxEx.Show(this, "请输入正确的数据标识名称...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_DataFlagName.Focus();
                return;
            }
            if (txt_DataFlagName.Tag.ToString() != txt_DataFlagName.Text && _SystemCol.DataFlagInfo.FindDataFlagInfo(txt_DataFlagName.Text))
            {
                MessageBoxEx.Show(this, "数据标识名已经存在，请重新输入...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_DataFlagName.Text = "";
                txt_DataFlagName.Focus();
                return;
            }

            if (txt_DataFlag.Text == "")
            {
                MessageBoxEx.Show(this, "请输入正确的数据标识...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_DataFlag.Focus();
                return;
            }
            if (txt_DataLength.Text == "" || !CLDC_DataCore.Function.Number.IsNumeric(txt_DataLength.Text))
            {
                MessageBoxEx.Show(this, "请输入正确的数据标识长度...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_DataLength.Focus();
                return;
            }
            if (txt_SmallNumber.Text == "" || !CLDC_DataCore.Function.Number.IsNumeric(txt_SmallNumber.Text))
            {
                MessageBoxEx.Show(this, "请选择小数位...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_SmallNumber.Focus();
                return;
            }

            if (txt_DataFormat.Text == "")
            {
                MessageBoxEx.Show(this, "请输入正确的数据格式...", "添加出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_DataFormat.Focus();
                return;
            }


            StDataFlagInfo _DataFlag = new StDataFlagInfo();
            _DataFlag.DataFlagName = txt_DataFlagName.Text;
            _DataFlag.DataFlag = txt_DataFlag.Text;
            _DataFlag.DataLength = txt_DataLength.Text;
            _DataFlag.DataSmallNumber = txt_SmallNumber.Text;
            _DataFlag.DataFormat = txt_DataFormat.Text;

            if (btn_DataFlagInsert.Text == "确认编辑")
            {
                _SystemCol.DataFlagInfo.Remove(txt_DataFlagName.Tag.ToString());
                lsv_DataFlag.Items.Remove(lsv_DataFlag.SelectedItems[0]);
            }
            _SystemCol.DataFlagInfo.Add(_DataFlag);

            this.InsertListView(_DataFlag);

            if (txt_DataFlagName.Tag.ToString() != "" || !chk_DataFlagContinue.Checked)
                btn_DataFlagCancel_Click(sender, e);
            else
            {
                txt_DataFlagName.Text = "";
                txt_DataFlagName.Tag = "";
                txt_DataFlag.Text = "";
                txt_DataLength.Text = "";
                txt_SmallNumber.Text = "";
                txt_DataFormat.Text = "";
            }
        }

        private void btn_DataFlagCancel_Click(object sender, EventArgs e)
        {
            txt_DataFlagName.Text = "";
            txt_DataFlagName.Tag = "";
            txt_DataFlag.Text = "";
            txt_DataLength.Text = "";
            txt_SmallNumber.Text = "";
            txt_DataFormat.Text = "";

            chk_DataFlagContinue.Checked = false;
            pnl_DataFlag.Visible = false;
        }

        private void btn_AddDataFlag_Click(object sender, EventArgs e)
        {
            btn_DataFlagInsert.Text = "确认添加";
            pnl_DataFlag.Visible = true;
        }

        private void btn_EditDataFlag_Click(object sender, EventArgs e)
        {
            if (lsv_DataFlag.SelectedItems.Count == 0)
                return;
            pnl_DataFlag.Visible = true;
            txt_DataFlagName.Text = lsv_DataFlag.SelectedItems[0].Text;
            txt_DataFlagName.Tag = lsv_DataFlag.SelectedItems[0].Text;
            txt_DataFlag.Text = lsv_DataFlag.SelectedItems[0].SubItems[1].Text;
            txt_DataLength.Text = lsv_DataFlag.SelectedItems[0].SubItems[2].Text;
            txt_SmallNumber.Text = lsv_DataFlag.SelectedItems[0].SubItems[3].Text;
            txt_DataFormat.Text = lsv_DataFlag.SelectedItems[0].SubItems[4].Text;

            btn_DataFlagInsert.Text = "确认编辑";
        }

        private void btn_DelDataFlag_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (lsv_DataFlag.SelectedItems.Count == 0)
                return;
            //这里要马上把selecteditems取出，不然messagebox显示时selecetditems里面没有了
            ListViewItem currentItem = lsv_DataFlag.SelectedItems[0];
            if (MessageBoxEx.Show(this, "你确认要移除数据标识名称为：" + currentItem.Text + "的数据标识吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _SystemCol.DataFlagInfo.Remove(currentItem.Text);
                lsv_DataFlag.Items.Remove(currentItem);
            }
        }
        #endregion

        #region 实验方法与依据配置
        /// <summary>
        /// 实验方法与依据配置信息列表
        /// </summary>
        /// <param name="Item">实验方法与依据配置对象</param>
        private void DefaultMethodBasisGrid(MethodAndBasis Item)
        {
            List<string> _Keys = Item.getKeyNames();
            MethodBasisProperty.Item.Clear();
            MethodBasisProperty.ShowCustomProperties = true;
            for (int i = 0; i < _Keys.Count; i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = Item.getItem(_Keys[i]);
                MethodBasisProperty.Item.Add(_Item.Name, _Item.Value, false, _Item.ClassName, _Item.Description, true);
                MethodBasisProperty.Item[MethodBasisProperty.Item.Count - 1].Tag = _Keys[i];
                string[] _Arr = _Item.DataSource.Split('|');
                if (_Arr.Length > 1)
                    MethodBasisProperty.Item[MethodBasisProperty.Item.Count - 1].Choices = new PropertyGridEx.CustomChoices(_Arr);

            }
            MethodBasisProperty.Refresh();
        }

        /// <summary>
        /// 转化实验方法与依据 信息
        /// </summary>
        private void SaveMethodBasisGridInfo()
        {
            PropertyGridEx.PropertyGridEx Property;
            Property = MethodBasisProperty;
            _SystemCol.methodAndBasis.Clear();
            for (int i = 0; i < Property.Item.Count; i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = new StSystemInfo();
                _Item.Name = Property.Item[i].Name;
                _Item.Value = Property.Item[i].Value.ToString();
                _Item.Description = Property.Item[i].Description;
                _Item.ClassName = Property.Item[i].Category;
                if (Property.Item[i].Choices != null)
                {
                    string _TmpString = "";
                    for (int j = 0; j < Property.Item[i].Choices.Count; j++)
                    {
                        if (j == 0)
                            _TmpString = Property.Item[i].Choices[j].ToString();
                        else
                            _TmpString = string.Format("{0}|{1}", _TmpString, Property.Item[i].Choices[j].ToString());
                    }
                    _Item.DataSource = _TmpString;

                }
                else
                {
                    _Item.DataSource = "";
                }
                _SystemCol.methodAndBasis.Add(Property.Item[i].Tag.ToString(), _Item);

            }
            Property = null;
        }
        #endregion

        #region 实验参数
        /// <summary>
        /// 实验参数配置信息列表
        /// </summary>
        /// <param name="Item">实验参数配置对象</param>
        private void DefaultTestSettingGrid(TestSetting  Item)
        {
            List<string> _Keys = Item.getKeyNames();
            TestSetProperty.Item.Clear();
            TestSetProperty.ShowCustomProperties = true;
            for (int i = 0; i < _Keys.Count; i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = Item.getItem(_Keys[i]);
                TestSetProperty.Item.Add(_Item.Name, _Item.Value, false, _Item.ClassName, _Item.Description, true);
                TestSetProperty.Item[TestSetProperty.Item.Count - 1].Tag = _Keys[i];
                string[] _Arr = _Item.DataSource.Split('|');
                if (_Arr.Length > 1)
                    TestSetProperty.Item[TestSetProperty.Item.Count - 1].Choices = new PropertyGridEx.CustomChoices(_Arr);
            }
            TestSetProperty.Refresh();
        }

        /// <summary>
        /// 转化实验参数 信息
        /// </summary>
        private void SaveTestSettingGridInfo()
        {
            PropertyGridEx.PropertyGridEx Property;
            Property = TestSetProperty;
            _SystemCol.testSetting.Clear();
            for (int i = 0; i < Property.Item.Count; i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = new StSystemInfo();
                _Item.Name = Property.Item[i].Name;
                _Item.Value = Property.Item[i].Value.ToString();
                _Item.Description = Property.Item[i].Description;
                _Item.ClassName = Property.Item[i].Category;
                if (Property.Item[i].Choices != null)
                {
                    string _TmpString = "";
                    for (int j = 0; j < Property.Item[i].Choices.Count; j++)
                    {
                        if (j == 0)
                            _TmpString = Property.Item[i].Choices[j].ToString();
                        else
                            _TmpString = string.Format("{0}|{1}", _TmpString, Property.Item[i].Choices[j].ToString());
                    }
                    _Item.DataSource = _TmpString;

                }
                else
                {
                    _Item.DataSource = "";
                }
                _SystemCol.testSetting.Add(Property.Item[i].Tag.ToString(), _Item);
            }
            Property = null;
        }
        #endregion

 
        #region ----------------------------------一下为注释不要的东西---------------------------------------------------

        /*

        #region   误差限字典列表配置

        ///// <summary>
        ///// 初始化误差限列表
        ///// </summary>
        ///// <param name="Item"></param>
        //private void DefaultWcLimit(csWcLimit Item)
        //{
        //    Ltv_Wclimit.Items.Clear();
        //    List<string> _xIbs = _SystemCol.xIbDic.getxIb();
        //    Ltv_Wclimit.Items.Clear();
        //    for (int i = 0; i < _xIbs.Count; i++)
        //    {
        //        ListViewItem _Lst = new ListViewItem(_xIbs[i],0);
        //        Ltv_Wclimit.Items.Add(_Lst);
        //    }
        //    _xIbs = null;
        //    return;
        //}
        /// <summary>
        /// 鼠标右键事件，产生菜单变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltv_Wclimit_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    Menu_WcLimit.Items.Clear();

            //    if (Ltv_Wclimit.View == View.Details)           //如果是列表样式，则需要在菜单中加入返回的菜单元素
            //    {
            //        ToolStripMenuItem _ReturnItem = new ToolStripMenuItem("返    回");
            //        _ReturnItem.Click+=new EventHandler(Menu_Click);
            //        Menu_WcLimit.Items.AddRange(new ToolStripMenuItem[] { _ReturnItem });
            //    }
            //    List<string> _GuiCheng = _SystemCol.WcLimit.getGuiChengName();
            //    ToolStripMenuItem[] _ItemGuiCheng = new ToolStripMenuItem[_GuiCheng.Count]; 
            //    for (int i = 0; i < _GuiCheng.Count; i++)
            //    {
            //        _ItemGuiCheng[i] = new ToolStripMenuItem(_GuiCheng[i]);
            //        _ItemGuiCheng[i].Tag = _ItemGuiCheng[i].Text;
            //        _ItemGuiCheng[i].MouseMove += new MouseEventHandler(MenuGuicheng_MouseMove);
            //    }
            //    Menu_WcLimit.Items.AddRange(_ItemGuiCheng);
            //}
            //else
                return;
        }
        /// <summary>
        /// 菜单鼠标移动事件，产生子菜单（元件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuGuicheng_MouseMove(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            if (Item.DropDownItems.Count != 0 || Item.Tag==null)
                return;
            ToolStripMenuItem[] _ItemYj = new ToolStripMenuItem[4];
            for (int i = 0; i < 4; i++)
            {
                _ItemYj[i] = new ToolStripMenuItem(((Comm.Enum.Cus_PowerYuanJiang)i + 1).ToString());
                _ItemYj[i].Tag = string.Format("{0}|{1}", Item.Tag.ToString(), _ItemYj[i].Text);
                _ItemYj[i].MouseMove += new MouseEventHandler(MenuGlys_MouseMove);
            }
            Item.DropDownItems.AddRange(_ItemYj);
        }
        /// <summary>
        /// 菜单鼠标移动事件，产生子菜单（功率因素）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuGlys_MouseMove(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            if (Item.DropDownItems.Count != 0)
                return;
            List<string> _Glys = _SystemCol.GlysZiDian.getGlysName();
            ToolStripMenuItem[] _ItemGlys = new ToolStripMenuItem[_Glys.Count];
            for (int _IndexGlys = 0; _IndexGlys < _Glys.Count; _IndexGlys++)
            {
                _ItemGlys[_IndexGlys] = new ToolStripMenuItem(_Glys[_IndexGlys]);
                _ItemGlys[_IndexGlys].Tag = string.Format("{0}|{1}", Item.Tag, _ItemGlys[_IndexGlys].Text);
                _ItemGlys[_IndexGlys].MouseMove += new MouseEventHandler(MenuDj_MouseMove);
            }
            Item.DropDownItems.AddRange(_ItemGlys);
        }
        /// <summary>
        /// 菜单鼠标移动事件，产生子菜单（等级）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDj_MouseMove(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            if (Item.DropDownItems.Count != 0)
                return;
            string[] _Dj ={ "0.2", "0.5", "1.0", "2.0", "3.0" };
            ToolStripMenuItem[] _ItemDj = new ToolStripMenuItem[_Dj.Length];
            for (int _IndexDj = 0; _IndexDj < _Dj.Length; _IndexDj++)
            {
                _ItemDj[_IndexDj] = new ToolStripMenuItem(_Dj[_IndexDj]);
                _ItemDj[_IndexDj].Tag = string.Format("{0}|{1}", Item.Tag, _ItemDj[_IndexDj].Text);
                _ItemDj[_IndexDj].MouseMove += new MouseEventHandler(MenuHgq_MouseMove);
            }
            Item.DropDownItems.AddRange(_ItemDj);
        }
        /// <summary>
        /// 菜单鼠标移动事件，产生子菜单（互感器）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuHgq_MouseMove(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            if (Item.DropDownItems.Count != 0)
                return;
            ToolStripMenuItem[] _Item = new ToolStripMenuItem[2];
            _Item[0] = new ToolStripMenuItem("直接接入");
            _Item[1] = new ToolStripMenuItem("经互感器接入");
            
            if (Item.Tag.ToString().ToLower().IndexOf("307") >= 0)
            {
                _Item[0].Tag = string.Format("{0}|{1}", Item.Tag.ToString(), _Item[0].Text);
                _Item[1].Tag = string.Format("{0}|{1}", Item.Tag.ToString(), _Item[1].Text);
                _Item[0].MouseMove += new MouseEventHandler(MenuYwg_MouseMove);
                _Item[1].MouseMove += new MouseEventHandler(MenuYwg_MouseMove);
            }
            else
            {
                _Item[0].Tag = string.Format("{0}|{1}|{2}", Item.Tag.ToString(), _Item[0].Text, "0");
                _Item[1].Tag = string.Format("{0}|{1}|{2}", Item.Tag.ToString(), _Item[1].Text,"0");
                _Item[0].Click += new EventHandler(Menu_Click);
                _Item[1].Click += new EventHandler(Menu_Click);
            }
            Item.DropDownItems.AddRange(_Item);

        }
        /// <summary>
        /// 菜单鼠标移动事件，产生子菜单（有无功）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuYwg_MouseMove(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            if (Item.DropDownItems.Count != 0)
                return;
            ToolStripMenuItem[] _Item = new ToolStripMenuItem[2];
            _Item[0] = new ToolStripMenuItem("有功");
            _Item[1] = new ToolStripMenuItem("无功");
            _Item[0].Tag = string.Format("{0}|{1}", Item.Tag.ToString(), _Item[0].Text);
            _Item[1].Tag = string.Format("{0}|{1}", Item.Tag.ToString(), _Item[1].Text);
            _Item[0].Click += new EventHandler(Menu_Click);
            _Item[1].Click += new EventHandler(Menu_Click);
            Item.DropDownItems.AddRange(_Item);
        }

        /// <summary>
        /// 菜单单击事件，显示误差
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem Item = (ToolStripMenuItem)sender;
            if (Item.Tag == null)               //如果选择的返回，则返回到原始状态
            {
                Lab_WcLimit.Text = "";
                Ltv_Wclimit.View = View.LargeIcon;
                return;
            }

            Lab_WcLimit.Text = Item.Tag.ToString();
            string[] _Pram = Lab_WcLimit.Text.Split('|');   //规程|元件|功率因素|等级|互感器|有无功
            Ltv_Wclimit.View = View.Details;
            for (int i = 0; i < Ltv_Wclimit.Items.Count; i++)
            {

                Comm.Enum.Cus_PowerYuanJiang _Yj;
                switch (_Pram[1])
                {
                    case "H":
                        {
                            _Yj = Comm.Enum.Cus_PowerYuanJian.H;
                            break;
                        }
                    case "A":
                        {
                            _Yj = Comm.Enum.Cus_PowerYuanJian.A;
                            break;
                        }
                    case "B":
                        {
                            _Yj = Comm.Enum.Cus_PowerYuanJian.B;
                            break;
                        }
                    case "C":
                        {
                            _Yj = Comm.Enum.Cus_PowerYuanJian.C;
                            break;
                        }
                    default:
                        {
                            _Yj = Comm.Enum.Cus_PowerYuanJian.H;
                            break;
                        }
                }

                string[] _Wcx=_SystemCol.WcLimit.getWcx(_Pram[0]
                                                        ,int.Parse(_Pram[5]=="有功"?"1":"0")
                                                        ,_Pram[3]
                                                        ,int.Parse(_Pram[4]=="直接接入"?"0":"1")
                                                        , _Yj
                                                        ,_Pram[2]
                                                        ,Ltv_Wclimit.Items[i].Text);
                try
                {
                    Ltv_Wclimit.Items[i].SubItems[1].Text = _Wcx[0];
                    Ltv_Wclimit.Items[i].SubItems[2].Text = _Wcx[1];
                }
                catch
                {
                    Ltv_Wclimit.Items[i].SubItems.Add( _Wcx[0]);
                    Ltv_Wclimit.Items[i].SubItems.Add(_Wcx[1]);
                }
            }
        
        }

        /// <summary>
        /// 保存误差限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_WcLimit_Click(object sender, EventArgs e)
        {
            if (IsAllSet())
            {
                string _Max=Txt_Max.Text;
                string _Min=Txt_Min.Text;
                if (!Comm.Function.Number.IsNumeric(_Max.Replace("+", "")))
                {
                    MessageBoxEx.Show(this,"误差上限需要输入一个大于0的数字...", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                    _Max = string.Format("+{0}", _Max.Replace("+",""));
                if (!Comm.Function.Number.IsNumeric(_Min.Replace("-", "")))
                {
                    MessageBoxEx.Show(this,"误差下限需要输入一个小于0的数字...", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                    _Min = string.Format("-{0}", _Min.Replace("-",""));

                _SystemCol.WcLimit.SetWcx(Cmb_WcLimitGC.Text
                                          ,Chk_YWG.Checked == true ? 1 : 0
                                          ,Cmb_WcLimitDj.Text
                                          ,Chk_HGQ.Checked == true ? 1 : 0
                                          ,(Comm.Enum.Cus_PowerYuanJiang)(Cmb_WcLimitYj.SelectedIndex + 1)
                                          , Cmb_WcLimitGlys.Text
                                          , GB_WcLimit.Tag.ToString()
                                          ,_Max,_Min);
                if (Ltv_Wclimit.View == View.Details)
                {
                    Ltv_Wclimit.SelectedItems[0].SubItems[1].Text = _Max;
                    Ltv_Wclimit.SelectedItems[0].SubItems[2].Text = _Min;
                }
            }

            Panel_WcLimit.Visible = false;
            Ltv_Wclimit.Enabled = true;
        }
        /// <summary>
        /// 双击选中一个电流倍数，显示该电流倍数下的误差限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltv_Wclimit_ItemActivate(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItems.Count == 0)
                return;
            ListViewItem _Item = ((ListView)sender).SelectedItems[0];
            GB_WcLimit.Text = string.Format("当前电流倍数：{0}",_Item.Text);
            GB_WcLimit.Tag = _Item.Text;
            if (Ltv_Wclimit.View == View.LargeIcon)
            {
                Cmb_WcLimitYj.SelectedIndex=-1;
                Cmb_WcLimitGlys.SelectedIndex = -1;
                Cmb_WcLimitDj.SelectedIndex = -1;
                Cmb_WcLimitGC.SelectedIndex = -1;
                Chk_HGQ.Checked = false;
                Chk_YWG.Checked = false;
                Txt_Min.Text="";
                Txt_Max.Text = "";
            }
            else
            {
                string[] _Pram = Lab_WcLimit.Text.Split('|');   //规程|元件|功率因素|等级|互感器|有无功
                Cmb_WcLimitGC.Text = _Pram[0];
                Cmb_WcLimitYj.Text = _Pram[1] + "元";
                Cmb_WcLimitGlys.Text = _Pram[2];
                Cmb_WcLimitDj.Text = _Pram[3];
                Chk_HGQ.Checked = _Pram[4] == "直接接入" ? false : true;
                Chk_YWG.Checked = _Pram[5] == "有功" ? true : false ;
                Txt_Max.Text = _Item.SubItems[1].Text;
                Txt_Min.Text = _Item.SubItems[2].Text;

            }
            Panel_WcLimit.Visible = true;
            Ltv_Wclimit.Enabled = false;
        }

        /// <summary>
        /// 规程列表下拉菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_WcLimitGC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsAllSet())
                this.SetTxtWcxValue();
        }

        private void Cmb_WcLimitDj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsAllSet())
                this.SetTxtWcxValue();
        }
        private void Cmb_WcLimitYj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsAllSet())
                this.SetTxtWcxValue();
        }

        private void Cmb_WcLimitGlys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsAllSet())
                this.SetTxtWcxValue();
        }

        private void Chk_HGQ_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllSet())
                this.SetTxtWcxValue();
        }

        private void Chk_YWG_CheckedChanged(object sender, EventArgs e)
        {
            if (IsAllSet())
                this.SetTxtWcxValue();
        }
        /// <summary>
        /// 判断是否都已经选中
        /// </summary>
        /// <returns></returns>
        private bool IsAllSet()
        {
            if (Cmb_WcLimitGC.SelectedIndex == -1)
                return false;
            if (Cmb_WcLimitDj.SelectedIndex == -1)
                return false;
            if (Cmb_WcLimitGlys.SelectedIndex == -1)
                return false;
            if (Cmb_WcLimitYj.SelectedIndex == -1)
                return false;
            return true;
        }

        /// <summary>
        /// 在TEXT文本框中显示误差限
        /// </summary>
        private void SetTxtWcxValue()
        { 
            string[] _Wcx=_SystemCol.WcLimit.getWcx(Cmb_WcLimitGC.Text
                                                    ,Chk_YWG.Checked==true?1:0
                                                    ,Cmb_WcLimitDj.Text
                                                    ,Chk_HGQ.Checked==true?1:0
                                                    ,(Comm.Enum.Cus_PowerYuanJiang)(Cmb_WcLimitYj.SelectedIndex+1)
                                                    ,Cmb_WcLimitGlys.Text
                                                    ,GB_WcLimit.Tag.ToString());

            Txt_Max.Text = _Wcx[0];
            Txt_Min.Text = _Wcx[1];
        }

        #endregion

        */

        #endregion


    }
}
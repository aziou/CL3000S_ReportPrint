using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//源控制组件和多功能通讯组件
using CLDC_VerifyAdapter;
using CLDC_VerifyAdapter.Multi;
using CLDC_Comm;
using CLDC_DataCore.Const;

namespace CLDC_MeterUI
{
    public partial class Frm_Setup : Form
    {
        const string FrmTitle = "科陆电能台多功能通信协议配置器";

        //声明一个一表位的多功能检定控制器
        //private VerifyAdapter.EquipUnit m_EquipUnit = null;
        //消息线程
        Thread thMessageMsg = null;

        private string ProtocolName = "";

        public Frm_Setup()
        {
            InitializeComponent();

            initDgnControl();

            ConsoleRedirect.Instance.OnMessage += delegate(string msg)
            {
                if (IsHandleCreated)
                    this.BeginInvoke(new InsertList(dteInsertList), new object[] { msg });
            };
        }
        /// <summary>
        /// 初始化多功能参数
        /// </summary>
        private void initDgnControl()
        {
            //加载当前系统配置
            CLDC_DataCore.Const.GlobalUnit.g_SystemConfig = new CLDC_DataCore.SystemModel.SystemInfo();
            CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.Load();
            CLDC_DataCore.Const.GlobalUnit.Log = new CLDC_DataCore.Function.RunLog();
            CLDC_DataCore.Const.GlobalUnit.FrameLog = new CLDC_DataCore.Function.RunLogFrame();
            int bwCount = GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_BWCOUNT, 24);
            CLDC_DataCore.Const.GlobalUnit.g_CUS = new CLDC_DataCore.CusModel(bwCount, 1);
            CLDC_DataCore.Const.GlobalUnit.g_CUS.Load();

            //通讯数据线程
            //Comm.GlobalUnit.m_485DataControl = new Comm.VerifyMsgControl(); //485消息组件
            //Comm.GlobalUnit.m_485DataControl.IsMsg = true;
            //Comm.GlobalUnit.m_485DataControl.ShowMsg += new Comm.VerifyMsgControl.OnShowMsg(dteShow485Message);
            //消息数据线程
            CLDC_DataCore.Const.GlobalUnit.g_MsgControl = new CLDC_DataCore.VerifyMsgControl();     //普通消息组件
            CLDC_DataCore.Const.GlobalUnit.g_MsgControl.IsMsg = true;
            CLDC_DataCore.Const.GlobalUnit.g_MsgControl.ShowMsg += new CLDC_DataCore.VerifyMsgControl.OnShowMsg(dteMessage);

            //  th485Msg = new Thread(Comm.GlobalUnit.m_485DataControl.DoWork);
            //  th485Msg.Start();
            thMessageMsg = new Thread(CLDC_DataCore.Const.GlobalUnit.g_MsgControl.DoWork);
            thMessageMsg.Start();

            //测试按钮不可用
            Menu_Test.Enabled = false;
            //联机
            //CLDC_DataCore.Function.ThreadCallBack.Call(new CLDC_DataCore.Function.CallBack(InitEquip), 1000);
        }
        /// <summary>
        /// 初始化设备
        /// </summary>
        private void InitEquip()
        {
            //CLDC_VerifyAdapter.Adapter.Instance.BwCount = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_BWCOUNT, 24);
            ////加载缓存数据
            //CLDC_DataCore.Const.GlobalUnit.g_CUS = new CLDC_DataCore.CusModel(CLDC_VerifyAdapter.Adapter.Instance.BwCount, 1);
            //CLDC_DataCore.Const.GlobalUnit.g_CUS.Load();

            //////m_EquipUnit = new EquipUnit(, 3); //初始化设备控件
            //bool linkResult = CLDC_VerifyAdapter.Helper.EquipHelper.Instance.Link();// m_EquipUnit.Link(true);
            //////Comm.Function.SetControl.SetEnabled(Menu_Test, linkResult);
            //Action<object> method = delegate(object ret)
            //{
            //    Menu_Test.Enabled = (bool)ret;
            //};
            //if (IsHandleCreated)
            //    this.BeginInvoke(method, new object[] { linkResult});
        }


        #region----------消息处理----------
        //private void dteShow485Message(object sender, object E)
        //{
        //   // Comm.MessageArgs.EventMessageArgs _M = E as Comm.MessageArgs.EventMessageArgs;
        //   // if (_M == null) return;
        //    if (this.IsHandleCreated)
        //    {
        //        /*

        //         服务器消息类型已经用来记录2036通讯数据。这儿只需要显示485数据
        //         */
        //        //if (_M.MessageType != Comm.Enum.Cus_MessageType.服务器消息)
        //            this.BeginInvoke(new InsertList(dteInsertList), new object[] { _M.Message });
        //    }
        //}

        private delegate void InsertList(string strData);
        private void dteInsertList(string strData)
        {
            Lst_BaoWen.Items.Add(strData);
        }


        private void dteMessage(object sender, object E)
        {
            
            CLDC_Comm.MessageArgs.EventMessageArgs _E = E as CLDC_Comm.MessageArgs.EventMessageArgs;
            if (_E == null) return;
            if (_E.MessageType == CLDC_Comm.Enum.Cus_MessageType.提示消息)
            {

                MessageBox.Show(_E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CLDC_DataCore.Function.SetControl.SetText(this, _E.Message);
        }



        #endregion



        /// <summary>
        /// 读取字典中制造厂家信息
        /// </summary>
        private void LoadfactoryString()
        {
            this.LoadInfo("制造厂家", ref Cmb_ZZCJ);


        }


        private void LoadMeterSize()
        {
            this.LoadInfo("表型号", ref Cmb_Bxh);
        }

        /// <summary>
        /// 加载数据集合
        /// </summary>
        /// <param name="DictionaryName"></param>
        private void LoadInfo(string DictionaryName, ref ComboBox Item)
        {
            CLDC_DataCore.SystemModel.Item.csDictionary _Dictionary = new CLDC_DataCore.SystemModel.Item.csDictionary();

            _Dictionary.Load();

            List<string> _Infos = _Dictionary.getValues(DictionaryName);     //获取字典中项目字典集合

            CLDC_DataCore.Function.BindCombox.BindComboxItem(Item, _Infos, true, DictionaryName, @".+?", true, _Dictionary);

            _Infos = null;

            _Dictionary = null;

        }

        /// <summary>
        /// 清空报文事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstMenu_Clear_Click(object sender, EventArgs e)
        {
            Lst_BaoWen.Items.Clear();
        }
        /// <summary>
        /// 复制报文内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstMenu_Copy_Click(object sender, EventArgs e)
        {
            string _TmpCopyData = "";

            for (int i = 0; i < Lst_BaoWen.Items.Count; i++)
            {
                _TmpCopyData += Lst_BaoWen.Items[i].ToString() + '\n';
            }

            Clipboard.SetDataObject(_TmpCopyData, true);
        }

        private void Frm_Setup_Load(object sender, EventArgs e)
        {
            this.Text = FrmTitle + "----------【新建】";
            this.LoadfactoryString();               //加载生产厂家信息
            this.LoadMeterSize();                   //加载表类型
            this.ProtocolName = "";                 //清空协议名称
            this.Txt_Clock.Text = "1";              //时钟频率默认1HZ/S
            this.Cmb_Protocol.SelectedIndex = 0;
            this.Cmb_Protocol_SelectionChangeCommitted(Cmb_Protocol, new EventArgs());
        }

        private void Cmb_Protocol_SelectionChangeCommitted(object sender, EventArgs e)
        {

            Object _Item;

            Cmb_ProtocolClass.SelectedIndex = Cmb_Protocol.SelectedIndex;

            if (Panel_Data.Controls.Count > 0)
                Panel_Data.Controls.Clear();

            switch (Cmb_Protocol.SelectedIndex)
            {
                case 0:             //645-1997
                    _Item = new ProtocolView.DLT645();
                    Panel_Data.Controls.Add((ProtocolView.DLT645)_Item);
                    ((ProtocolView.DLT645)_Item).Dock = DockStyle.Fill;
                    ((ProtocolView.DLT645)_Item).ReturnMessage += new ProtocolView.DLT645.EventReturnMessage(Frm_Setup_ReturnMessage);
                    break;
                case 1:
                    _Item = new ProtocolView.DLT6452007();
                    Panel_Data.Controls.Add((ProtocolView.DLT6452007)_Item);
                    ((ProtocolView.DLT6452007)_Item).Dock = DockStyle.Fill;
                    ((ProtocolView.DLT6452007)_Item).ReturnMessage += new ProtocolView.DLT6452007.EventReturnMessage(Frm_Setup_ReturnMessage);
                    break;
                case 2:
                    _Item = new ProtocolView.EDMIMK();
                    Panel_Data.Controls.Add((ProtocolView.EDMIMK)_Item);
                    ((ProtocolView.EDMIMK)_Item).Dock = DockStyle.Fill;
                    ((ProtocolView.EDMIMK)_Item).ReturnMessage += new ProtocolView.EDMIMK.EventReturnMessage(Frm_Setup_ReturnMessage);
                    break;
                case 3:
                    _Item = new ProtocolView.IEC1107St();
                    Panel_Data.Controls.Add((ProtocolView.IEC1107St)_Item);
                    ((ProtocolView.IEC1107St)_Item).Dock = DockStyle.Fill;
                    ((ProtocolView.IEC1107St)_Item).ReturnMessage += new ProtocolView.IEC1107St.EventReturnMessage(Frm_Setup_ReturnMessage);
                    break;
                case 4:
                    _Item = new ProtocolView.A1600();
                    Panel_Data.Controls.Add((ProtocolView.A1600)_Item);
                    ((ProtocolView.A1600)_Item).Dock = DockStyle.Fill;
                    ((ProtocolView.A1600)_Item).ReturnMessage += new ProtocolView.A1600.EventReturnMessage(Frm_Setup_ReturnMessage);
                    break;
            }
        }

        /// <summary>
        /// 报文返回事件
        /// </summary>
        /// <param name="MessageString"></param>
        /// <param name="Tx"></param>
        private void Frm_Setup_ReturnMessage(string MessageString, bool Tx)
        {
            Lst_BaoWen.Items.Add(Tx ? "Tx:" : "Rx" + MessageString);
        }

        /// <summary>
        /// 新建立一个协议方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_New_Click(object sender, EventArgs e)
        {
            this.Frm_Setup_Load(this, new EventArgs());
            Menu_Del.Enabled = false;                    //使删除菜单按钮不可用
        }
        /// <summary>
        /// 存盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Save_Click(object sender, EventArgs e)
        {
            this.SaveProtocolInfo(false);
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_SaveAs_Click(object sender, EventArgs e)
        {
            this.SaveProtocolInfo(true);
        }

        /// <summary>
        /// 存储协议方法
        /// </summary>
        /// <param name="SaveAs">是否是另存为</param>
        private void SaveProtocolInfo(bool SaveAs)
        {
            if (Panel_Data.Controls.Count == 0) return;


            Txt_ProtocolName.Text = "";
            Cmd_Save.Tag = null;
            Panel_SaveName.Tag = null;
            Txt_ProtocolName.Tag = null;

            object _Item = null;

            bool _FindFactory = false;                  //是否发现存在相同制造厂家和表型号的协议

            _Item = Panel_Data.Controls[0];
            if (_Item is ProtocolView.DLT645)
                if (!((ProtocolView.DLT645)_Item).CheckNull()) return;
            if (_Item is ProtocolView.DLT6452007)
                if (!((ProtocolView.DLT6452007)_Item).CheckNull()) return;
            if (_Item is ProtocolView.EDMIMK)
                if (!((ProtocolView.EDMIMK)_Item).CheckNull()) return;
            if (_Item is ProtocolView.IEC1107St)
                if (!((ProtocolView.IEC1107St)_Item).CheckNull()) return;
            if (_Item is ProtocolView.A1600)
                if (!((ProtocolView.A1600)_Item).CheckNull()) return;

            CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _Protocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();

            _Protocol.DnbFactroy = Cmb_ZZCJ.Text;           //制造厂家
            _Protocol.DnbSize = Cmb_Bxh.Text;               //表型号
            _Protocol.DllFile = "ClAmMeterProtocol";        //这个是固定的！协议库名称
            _Protocol.ClassName = Cmb_ProtocolClass.Text;   //协议类名称 

            try
            {
                _Protocol.ClockPL = float.Parse(Txt_Clock.Text.Trim() == "" ? "1" : Txt_Clock.Text.Trim());       //时钟频率
            }
            catch
            {
                MessageBox.Show("时钟频率必须为一个数字，请重新输入...", "保存出错", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_Item is ProtocolView.DLT645)
                if (!((ProtocolView.DLT645)_Item).getInfo(ref _Protocol)) return;
            if (_Item is ProtocolView.DLT6452007)
                if (!((ProtocolView.DLT6452007)_Item).getInfo(ref _Protocol)) return;
            if (_Item is ProtocolView.EDMIMK)
                if (!((ProtocolView.EDMIMK)_Item).getInfo(ref _Protocol)) return;
            if (_Item is ProtocolView.IEC1107St)
                if (!((ProtocolView.IEC1107St)_Item).getInfo(ref _Protocol)) return;
            if (_Item is ProtocolView.A1600)
                if (!((ProtocolView.A1600)_Item).getInfo(ref _Protocol)) return;

            if (CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.ContainsNodeByFactroy(Cmb_ZZCJ.Text, Cmb_Bxh.Text))
            {
                if (SaveAs)          //如果是另存为
                {
                    if (MessageBox.Show(string.Format("已经存在制造厂家为：{0}，表型号为：{1}的通信协议，是否覆盖原有协议？\n点“否”退出保存", Cmb_ZZCJ.Text, Cmb_Bxh.Text), "保存询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _FindFactory = true;
                    }
                }
                else
                {
                    CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.RemoveProtocol(Cmb_ZZCJ.Text, Cmb_Bxh.Text);
                }
            }

            Panel_SaveName.Tag = _Protocol;         //把协议对象放入Tag

            Txt_ProtocolName.Tag = _FindFactory;    //把是否存在相同的表协议的厂家和型号的判断放入文本框的Tag

            if (SaveAs || this.ProtocolName == "")          //如果是另存为，或者ProtocolName为空(这个表示新增)
            {
                tableLayoutPanel1.Enabled = false;      //锁定表单中所有可操作部分

                Panel_SaveName.Visible = true;          //显示存储协议名称输入界面

                Txt_ProtocolName.Text = "";
            }
            else if (!SaveAs && this.ProtocolName != "")           //如果是保存
            {
                Txt_ProtocolName.Text = this.ProtocolName;
                Cmd_Save.Tag = false;                               //使用存盘按钮的Tag标志该存盘不是由另存为触发
                this.Cmd_Save_Click(Cmd_Save, new EventArgs());
            }
        }

        /// <summary>
        /// 输入协议名称后的保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Save_Click(object sender, EventArgs e)
        {
            if (Txt_ProtocolName.Text == "")
            {
                MessageBox.Show("请输入一个协议名称...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Txt_ProtocolName.Focus();
                return;
            }
            CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _ProtocolInfo = (CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo)Panel_SaveName.Tag;

            if (CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.ContainsNodeBypName(Txt_ProtocolName.Text))
            {
                if (Cmd_Save.Tag == null)         //该标示如果是Null,则标示是另存为，如果不为空则表示是保存，不需要询问直接删除存在的协议
                {
                    if (MessageBox.Show(string.Format("已经存在协议名称为：{0}的协议文件，请问是否覆盖？\n如果选择不覆盖，将放弃本次保存", Txt_ProtocolName.Text), "保存询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.RemoveProtocol(Txt_ProtocolName.Text);
            }
            if (Cmd_Save.Tag == null && (bool)Txt_ProtocolName.Tag)
            {
                CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.RemoveProtocol(Cmb_ZZCJ.Text, Cmb_Bxh.Text);
            }


            _ProtocolInfo.ProtocolName = Txt_ProtocolName.Text;
            try
            {
                if (_ProtocolInfo.AddNewProtocol())
                {
                    MessageBox.Show("保存成功，请返回...", "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.SetFormTitle(_ProtocolInfo.ProtocolName);
                }
                else
                {
                    MessageBox.Show("保存失败，请联系厂家...", "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cmd_CancelSave_Click(sender, e);
        }
        /// <summary>
        /// 取消保存的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_CancelSave_Click(object sender, EventArgs e)
        {
            Panel_SaveName.Visible = false;
            tableLayoutPanel1.Enabled = true;
        }

        /// <summary>
        /// 删除协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Del_Click(object sender, EventArgs e)
        {
            if (this.ProtocolName == "") return;
            if (MessageBox.Show(string.Format("您确认要删除协议名称为：{0}的通信协议吗？", this.ProtocolName), "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.RemoveProtocol(this.ProtocolName))
                this.Menu_New_Click(sender, e);
            else
            {
                MessageBox.Show("删除失败，请联系厂家技术人员.....", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 打开协议按钮事件，该事件为创建文件中所有协议的下拉菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Open_DropDownOpening(object sender, EventArgs e)
        {
            Dictionary<string, string> _Names = CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.getProtocolString();
            Menu_Open.DropDownItems.Clear();
            foreach (string _Key in _Names.Keys)
            {
                ToolStripItem _Item = Menu_Open.DropDownItems.Add(_Names[_Key]);
                _Item.Tag = _Key;
                _Item.Click += new EventHandler(ToolMenuItem_Click);
            }
        }

        /// <summary>
        /// 菜单按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolMenuItem_Click(object sender, EventArgs e)
        {
            CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _Protocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo(((ToolStripItem)sender).Tag.ToString());
            if (!_Protocol.Loading)          //加载失败
            {
                MessageBox.Show("打开通信协议出错，请联系厂家技术人员.....", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.SetFormTitle(_Protocol.ProtocolName);

            Cmb_ZZCJ.Text = _Protocol.DnbFactroy;
            Cmb_Bxh.Text = _Protocol.DnbSize;
            Cmb_ProtocolClass.Text = _Protocol.ClassName;
            Txt_Clock.Text = _Protocol.ClockPL.ToString();
            object _Item = null;
            _Item = Panel_Data.Controls[0];
            if (_Item is ProtocolView.DLT645)
            {
                if (((ProtocolView.DLT645)_Item).setInfo(_Protocol)) return;
            }
            if (_Item is ProtocolView.DLT6452007)
                if (((ProtocolView.DLT6452007)_Item).setInfo(_Protocol)) return;
            if (_Item is ProtocolView.EDMIMK)
                if (((ProtocolView.EDMIMK)_Item).setInfo(_Protocol)) return;
            if (_Item is ProtocolView.IEC1107St)
                if (((ProtocolView.IEC1107St)_Item).setInfo(_Protocol)) return;
            if (_Item is ProtocolView.A1600)
                if (((ProtocolView.A1600)_Item).setInfo(_Protocol)) return;

            MessageBox.Show("协议信息加载失败....", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Cmb_ProtocolClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_Protocol.SelectedIndex = Cmb_ProtocolClass.SelectedIndex;
            Cmb_Protocol_SelectionChangeCommitted(Cmb_Protocol, e);
        }

        /// <summary>
        /// 设置窗体标题
        /// </summary>
        /// <param name="ProtocolName"></param>
        private void SetFormTitle(string ProtocolName)
        {
            this.ProtocolName = ProtocolName;          //模块保存协议名称，用于删除等其他操作
            this.Text = FrmTitle + "----------【" + this.ProtocolName + "】";
            Menu_Del.Enabled = true;                    //使删除菜单按钮可用
        }
        /// <summary>
        /// 开始测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Test_Click(object sender, EventArgs e)
        {
            Panel_Test.Visible = true;

        }
        /// <summary>
        /// 停止测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Stop_Click(object sender, EventArgs e)
        {
            //Comm.GlobalUnit.ForceVerifyStop = true;

            ProtocolView.BaseControl _Item = null;
            _Item = Panel_Data.Controls[0] as ProtocolView.BaseControl;
            _Item.StopTest();
        }


        /// <summary>
        /// 开始测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Start_Click(object sender, EventArgs e)
        {
            if (Txt_Adr.Text == "")
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage("请输入一个正确的测试表地址", false, CLDC_Comm.Enum.Cus_MessageType.提示消息);

                return;
            }
            if (txtBW.Text == "" || !CLDC_DataCore.Function.Number.IsIntNumber(txtBW.Text))
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage("请输入一个正确的测试表位表", false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
                return;
            }
            if (cmbU.Text == "")
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage("请选择一个测试电压", false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
                return;
            }
            float u = float.Parse(cmbU.Text);
            int Index = int.Parse(txtBW.Text) - 1;
            //隐藏测试面板
            CLDC_DataCore.Function.SetControl.SetVisible(Panel_Test, false);

            // Panel_Test.Visible = false;
            ProtocolView.BaseControl _Item = null;
            _Item = Panel_Data.Controls[0] as ProtocolView.BaseControl;
            if (!(_Item is ProtocolView.BaseControl)) return;

            // if (_Item.EquipmentUnit == null)
            //{
            //_Item.EquipmentUnit = m_EquipUnit;
            //_Item.EquipmentUnit = m_EquipUnit;
            //}
            CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo TmpProtocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();
            //init verifyadapter


            TmpProtocol.DllFile = "ClAmMeterProtocol";        //这个是固定的！协议库名称
            TmpProtocol.ClassName = Cmb_ProtocolClass.Text;   //协议类名称 

            _Item.TestProtocolPra = TmpProtocol;
            _Item.StartTest(Txt_Adr.Text, u, Index);

            //if (_Item is ProtocolView.DLT645)
            //{
            //    //指定多功能组件
            //    //((ProtocolView.DLT645)_Item).m_DgnControl = m_DgnControl;
            //    ((ProtocolView.DLT645)_Item).StartTest(Txt_Adr.Text);
            //}
            //if (_Item is ProtocolView.DLT6452007)
            //{
            //    ((ProtocolView.DLT6452007)_Item).StartTest(Txt_Adr.Text);
            //} if (_Item is ProtocolView.EDMIMK)
            //{
            //    ((ProtocolView.EDMIMK)_Item).StartTest(Txt_Adr.Text);
            //} if (_Item is ProtocolView.IEC1107St)
            //{
            //    ((ProtocolView.IEC1107St)_Item).StartTest(Txt_Adr.Text);
            //} if (_Item is ProtocolView.A1600)
            //{
            //    ((ProtocolView.A1600)_Item).StartTest(Txt_Adr.Text);
            //}
        }
        /// <summary>
        /// 取消测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_End_Click(object sender, EventArgs e)
        {
            Panel_Test.Visible = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.UnLink();
            //m_EquipUnit.Link(false);
            //Thread.Sleep(2000);
            //Comm.GlobalUnit.ForceVerifyStop = true;
            CLDC_DataCore.Const.GlobalUnit.ApplicationIsOver = true;
            //Thread.Sleep(1000);
            base.OnClosing(e);
        }

        private void Menu_Open_ButtonClick(object sender, EventArgs e)
        {
            Menu_Open_DropDownOpening(sender, e);
            Menu_Open.DropDown.Show();
        }
    }
}
namespace CLDC_MeterUI.UI_Detection_New
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if(UIMonitor !=null)
                    UIMonitor.Dispose();
                
            }
            catch { }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.StatusMain = new System.Windows.Forms.StatusStrip();
            this.StatusMain_Proc = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusMain_Text = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMain_Mode = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMain_TxtStatus1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMain_TxtStatus2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMain_LabLoginMeg = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Light = new System.Windows.Forms.ToolStripStatusLabel();
            this.Plan_ChildContainer = new System.Windows.Forms.Panel();
            this.ToolStrip_Main = new DevComponents.DotNetBar.PanelEx();
            this.ribbonBar5 = new DevComponents.DotNetBar.RibbonBar();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.btn_ElectControl = new DevComponents.DotNetBar.ButtonItem();
            this.btn_FrameSee = new DevComponents.DotNetBar.ButtonItem();
            this.btn_FrmPROTOCOL = new DevComponents.DotNetBar.ButtonItem();
            this.btn_ModuleTest = new DevComponents.DotNetBar.ButtonItem();
            this.btn_MotorDelay = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBar4 = new DevComponents.DotNetBar.RibbonBar();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBar3 = new DevComponents.DotNetBar.RibbonBar();
            this.buttonItemConnectStatus = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.UOnOnly = new DevComponents.DotNetBar.ButtonItem();
            this.UIon = new DevComponents.DotNetBar.ButtonItem();
            this.FreeControl = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.ToolBtn_InputParam = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.stepUserControl1 = new CLDC_MeterUI.UI_Detection_New.ShowDataView.StepUserControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.StatusMain.SuspendLayout();
            this.ToolStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.StatusMain, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Plan_ChildContainer, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ToolStrip_Main, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.stepUserControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1285, 552);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // StatusMain
            // 
            this.StatusMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.StatusMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMain_Proc,
            this.StatusMain_Text,
            this.StatusMain_Mode,
            this.StatusMain_TxtStatus1,
            this.StatusMain_TxtStatus2,
            this.StatusMain_LabLoginMeg,
            this.Status_Light});
            this.StatusMain.Location = new System.Drawing.Point(0, 527);
            this.StatusMain.Name = "StatusMain";
            this.StatusMain.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.StatusMain.Size = new System.Drawing.Size(1285, 25);
            this.StatusMain.TabIndex = 0;
            this.StatusMain.Text = "statusStrip1";
            // 
            // StatusMain_Proc
            // 
            this.StatusMain_Proc.AutoSize = false;
            this.StatusMain_Proc.Name = "StatusMain_Proc";
            this.StatusMain_Proc.Size = new System.Drawing.Size(400, 19);
            this.StatusMain_Proc.Value = 100;
            // 
            // StatusMain_Text
            // 
            this.StatusMain_Text.AutoSize = false;
            this.StatusMain_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.StatusMain_Text.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.StatusMain_Text.Name = "StatusMain_Text";
            this.StatusMain_Text.Size = new System.Drawing.Size(56, 20);
            this.StatusMain_Text.Text = "提示文字";
            this.StatusMain_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusMain_Mode
            // 
            this.StatusMain_Mode.AutoSize = false;
            this.StatusMain_Mode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.StatusMain_Mode.Name = "StatusMain_Mode";
            this.StatusMain_Mode.Size = new System.Drawing.Size(60, 20);
            this.StatusMain_Mode.Text = "XX模式";
            // 
            // StatusMain_TxtStatus1
            // 
            this.StatusMain_TxtStatus1.AutoSize = false;
            this.StatusMain_TxtStatus1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.StatusMain_TxtStatus1.Name = "StatusMain_TxtStatus1";
            this.StatusMain_TxtStatus1.Size = new System.Drawing.Size(55, 20);
            this.StatusMain_TxtStatus1.Text = "当前状态";
            this.StatusMain_TxtStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusMain_TxtStatus2
            // 
            this.StatusMain_TxtStatus2.AutoSize = false;
            this.StatusMain_TxtStatus2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.StatusMain_TxtStatus2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.StatusMain_TxtStatus2.Name = "StatusMain_TxtStatus2";
            this.StatusMain_TxtStatus2.Size = new System.Drawing.Size(100, 20);
            this.StatusMain_TxtStatus2.Text = "停止检定";
            // 
            // StatusMain_LabLoginMeg
            // 
            this.StatusMain_LabLoginMeg.AutoSize = false;
            this.StatusMain_LabLoginMeg.Name = "StatusMain_LabLoginMeg";
            this.StatusMain_LabLoginMeg.Size = new System.Drawing.Size(492, 20);
            this.StatusMain_LabLoginMeg.Text = "    检定员：xxxxxx    核验员：xxxxxx    登录时间：xxxx年xx月xx日 HH时mm分ss秒        ";
            // 
            // Status_Light
            // 
            this.Status_Light.AutoSize = false;
            this.Status_Light.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Status_Light.Image = ((System.Drawing.Image)(resources.GetObject("Status_Light.Image")));
            this.Status_Light.ImageTransparentColor = System.Drawing.Color.Black;
            this.Status_Light.Name = "Status_Light";
            this.Status_Light.Size = new System.Drawing.Size(18, 20);
            this.Status_Light.Text = "  ";
            // 
            // Plan_ChildContainer
            // 
            this.Plan_ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plan_ChildContainer.Location = new System.Drawing.Point(0, 151);
            this.Plan_ChildContainer.Margin = new System.Windows.Forms.Padding(0);
            this.Plan_ChildContainer.Name = "Plan_ChildContainer";
            this.Plan_ChildContainer.Size = new System.Drawing.Size(1285, 376);
            this.Plan_ChildContainer.TabIndex = 2;
            // 
            // ToolStrip_Main
            // 
            this.ToolStrip_Main.CanvasColor = System.Drawing.Color.Transparent;
            this.ToolStrip_Main.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ToolStrip_Main.Controls.Add(this.ribbonBar5);
            this.ToolStrip_Main.Controls.Add(this.ribbonBar4);
            this.ToolStrip_Main.Controls.Add(this.ribbonBar3);
            this.ToolStrip_Main.Controls.Add(this.ribbonBar1);
            this.ToolStrip_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip_Main.Margin = new System.Windows.Forms.Padding(0);
            this.ToolStrip_Main.Name = "ToolStrip_Main";
            this.ToolStrip_Main.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.ToolStrip_Main.Size = new System.Drawing.Size(1285, 82);
            this.ToolStrip_Main.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.ToolStrip_Main.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.ToolStrip_Main.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.ToolStrip_Main.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.ToolStrip_Main.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ToolStrip_Main.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.ToolStrip_Main.Style.GradientAngle = 90;
            this.ToolStrip_Main.TabIndex = 3;
            // 
            // ribbonBar5
            // 
            this.ribbonBar5.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar5.BackgroundMouseOverStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar5.BackgroundStyle.Class = "";
            this.ribbonBar5.ContainerControlProcessDialogKey = true;
            this.ribbonBar5.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar5.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem5,
            this.buttonItem13});
            this.ribbonBar5.Location = new System.Drawing.Point(779, 0);
            this.ribbonBar5.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonBar5.Name = "ribbonBar5";
            this.ribbonBar5.Size = new System.Drawing.Size(168, 81);
            this.ribbonBar5.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar5.TabIndex = 4;
            this.ribbonBar5.Text = "Third String";
            // 
            // 
            // 
            this.ribbonBar5.TitleStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar5.TitleStyleMouseOver.Class = "";
            this.ribbonBar5.TitleVisible = false;
            this.ribbonBar5.ItemClick += new System.EventHandler(this.ToolStrip_Main_ItemClicked);
            // 
            // buttonItem5
            // 
            this.buttonItem5.BeginGroup = true;
            this.buttonItem5.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem5.Image")));
            this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.PopupSide = DevComponents.DotNetBar.ePopupSide.Bottom;
            this.buttonItem5.Stretch = true;
            this.buttonItem5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem12,
            this.buttonItem6,
            this.buttonItem7,
            this.buttonItem15,
            this.buttonItem16});
            this.buttonItem5.Text = "高级配置";
            // 
            // buttonItem12
            // 
            this.buttonItem12.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.SubItemsExpandWidth = 14;
            this.buttonItem12.Text = "系统配置";
            // 
            // buttonItem6
            // 
            this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.SubItemsExpandWidth = 14;
            this.buttonItem6.Text = "设备通道";
            // 
            // buttonItem7
            // 
            this.buttonItem7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.SubItemsExpandWidth = 14;
            this.buttonItem7.Text = "485通道";
            this.buttonItem7.Visible = false;
            // 
            // buttonItem15
            // 
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.Text = "显示配置";
            // 
            // buttonItem16
            // 
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.Text = "协议配置";
            // 
            // buttonItem13
            // 
            this.buttonItem13.BeginGroup = true;
            this.buttonItem13.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem13.Image")));
            this.buttonItem13.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.PopupSide = DevComponents.DotNetBar.ePopupSide.Bottom;
            this.buttonItem13.Stretch = true;
            this.buttonItem13.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btn_ElectControl,
            this.btn_FrameSee,
            this.btn_FrmPROTOCOL,
            this.btn_ModuleTest,
            this.btn_MotorDelay});
            this.buttonItem13.Text = "工具箱";
            // 
            // btn_ElectControl
            // 
            this.btn_ElectControl.Name = "btn_ElectControl";
            this.btn_ElectControl.Text = "高级控制";
            // 
            // btn_FrameSee
            // 
            this.btn_FrameSee.Name = "btn_FrameSee";
            this.btn_FrameSee.Text = "报文工具";
            // 
            // btn_FrmPROTOCOL
            // 
            this.btn_FrmPROTOCOL.Name = "btn_FrmPROTOCOL";
            this.btn_FrmPROTOCOL.Text = "协议字典";
            // 
            // btn_ModuleTest
            // 
            this.btn_ModuleTest.Name = "btn_ModuleTest";
            this.btn_ModuleTest.Text = "模块测试";
            // 
            // btn_MotorDelay
            // 
            this.btn_MotorDelay.Name = "btn_MotorDelay";
            this.btn_MotorDelay.Text = "延时设置";
            // 
            // ribbonBar4
            // 
            this.ribbonBar4.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar4.BackgroundMouseOverStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar4.BackgroundStyle.Class = "";
            this.ribbonBar4.ContainerControlProcessDialogKey = true;
            this.ribbonBar4.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar4.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem11,
            this.buttonItem8});
            this.ribbonBar4.Location = new System.Drawing.Point(601, 0);
            this.ribbonBar4.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonBar4.Name = "ribbonBar4";
            this.ribbonBar4.Size = new System.Drawing.Size(178, 81);
            this.ribbonBar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar4.TabIndex = 3;
            this.ribbonBar4.Text = "Third String";
            // 
            // 
            // 
            this.ribbonBar4.TitleStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar4.TitleStyleMouseOver.Class = "";
            this.ribbonBar4.TitleVisible = false;
            this.ribbonBar4.ItemClick += new System.EventHandler(this.ToolStrip_Main_ItemClicked);
            // 
            // buttonItem11
            // 
            this.buttonItem11.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem11.Image")));
            this.buttonItem11.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.SubItemsExpandWidth = 14;
            this.buttonItem11.Text = "数据管理";
            // 
            // buttonItem8
            // 
            this.buttonItem8.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem8.Image")));
            this.buttonItem8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.SubItemsExpandWidth = 14;
            this.buttonItem8.Text = "方案管理";
            // 
            // ribbonBar3
            // 
            this.ribbonBar3.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar3.BackgroundMouseOverStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar3.BackgroundStyle.Class = "";
            this.ribbonBar3.ContainerControlProcessDialogKey = true;
            this.ribbonBar3.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemConnectStatus,
            this.buttonItem9,
            this.buttonItem10});
            this.ribbonBar3.Location = new System.Drawing.Point(339, 0);
            this.ribbonBar3.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonBar3.Name = "ribbonBar3";
            this.ribbonBar3.Size = new System.Drawing.Size(262, 81);
            this.ribbonBar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar3.TabIndex = 2;
            this.ribbonBar3.Text = "控制工具";
            // 
            // 
            // 
            this.ribbonBar3.TitleStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar3.TitleStyleMouseOver.Class = "";
            this.ribbonBar3.TitleVisible = false;
            this.ribbonBar3.ItemClick += new System.EventHandler(this.ToolStrip_Main_ItemClicked);
            // 
            // buttonItemConnectStatus
            // 
            this.buttonItemConnectStatus.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemConnectStatus.Image")));
            this.buttonItemConnectStatus.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItemConnectStatus.Name = "buttonItemConnectStatus";
            this.buttonItemConnectStatus.SubItemsExpandWidth = 14;
            this.buttonItemConnectStatus.Text = "重新联机";
            this.buttonItemConnectStatus.Tooltip = "重新联机";
            // 
            // buttonItem9
            // 
            this.buttonItem9.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem9.Image")));
            this.buttonItem9.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.UOnOnly,
            this.UIon,
            this.FreeControl});
            this.buttonItem9.Text = "升源输出";
            // 
            // UOnOnly
            // 
            this.UOnOnly.Name = "UOnOnly";
            this.UOnOnly.Text = "只升电压";
            // 
            // UIon
            // 
            this.UIon.Name = "UIon";
            this.UIon.Text = "电压电流";
            // 
            // FreeControl
            // 
            this.FreeControl.Name = "FreeControl";
            this.FreeControl.Text = "自由输出";
            // 
            // buttonItem10
            // 
            this.buttonItem10.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem10.Image")));
            this.buttonItem10.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.SubItemsExpandWidth = 14;
            this.buttonItem10.Text = "关源停止";
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundMouseOverStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar1.BackgroundStyle.Class = "";
            this.ribbonBar1.ContainerControlProcessDialogKey = true;
            this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ToolBtn_InputParam,
            this.buttonItem2,
            this.buttonItem3,
            this.buttonItem4});
            this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
            this.ribbonBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.Size = new System.Drawing.Size(339, 81);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar1.TabIndex = 0;
            this.ribbonBar1.Text = "开始检定的基本功能";
            // 
            // 
            // 
            this.ribbonBar1.TitleStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar1.TitleStyleMouseOver.Class = "";
            this.ribbonBar1.TitleVisible = false;
            this.ribbonBar1.ItemClick += new System.EventHandler(this.ToolStrip_Main_ItemClicked);
            // 
            // ToolBtn_InputParam
            // 
            this.ToolBtn_InputParam.Image = ((System.Drawing.Image)(resources.GetObject("ToolBtn_InputParam.Image")));
            this.ToolBtn_InputParam.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.ToolBtn_InputParam.Name = "ToolBtn_InputParam";
            this.ToolBtn_InputParam.SubItemsExpandWidth = 14;
            this.ToolBtn_InputParam.Text = "参数录入";
            this.ToolBtn_InputParam.Tooltip = "1";
            // 
            // buttonItem2
            // 
            this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
            this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItemsExpandWidth = 14;
            this.buttonItem2.Text = "预先调试";
            this.buttonItem2.Tooltip = "2";
            // 
            // buttonItem3
            // 
            this.buttonItem3.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem3.Image")));
            this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.SubItemsExpandWidth = 14;
            this.buttonItem3.Text = "方案检定";
            this.buttonItem3.Tooltip = "3";
            // 
            // buttonItem4
            // 
            this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
            this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.SubItemsExpandWidth = 14;
            this.buttonItem4.Text = "审核存盘";
            this.buttonItem4.Tooltip = "4";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // buttonItem14
            // 
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.SubItemsExpandWidth = 14;
            this.buttonItem14.Text = "buttonItem14";
            // 
            // stepUserControl1
            // 
            this.stepUserControl1.BackColor = System.Drawing.Color.Transparent;
            this.stepUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepUserControl1.Location = new System.Drawing.Point(0, 82);
            this.stepUserControl1.Margin = new System.Windows.Forms.Padding(0);
            this.stepUserControl1.Name = "stepUserControl1";
            this.stepUserControl1.Size = new System.Drawing.Size(1285, 69);
            this.stepUserControl1.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1285, 552);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.StatusMain.ResumeLayout(false);
            this.StatusMain.PerformLayout();
            this.ToolStrip_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        
        private System.Windows.Forms.StatusStrip StatusMain;
        private System.Windows.Forms.ToolStripStatusLabel StatusMain_Text;
        private System.Windows.Forms.ToolStripProgressBar StatusMain_Proc;
        private System.Windows.Forms.ToolStripStatusLabel Status_Light;
        private System.Windows.Forms.Panel Plan_ChildContainer;
        private System.Windows.Forms.ToolStripStatusLabel StatusMain_TxtStatus1;
        private System.Windows.Forms.ToolStripStatusLabel StatusMain_TxtStatus2;
        private System.Windows.Forms.ToolStripStatusLabel StatusMain_Mode;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.PanelEx ToolStrip_Main;
        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem ToolBtn_InputParam;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.RibbonBar ribbonBar3;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItemConnectStatus;
        private DevComponents.DotNetBar.RibbonBar ribbonBar4;
        private DevComponents.DotNetBar.RibbonBar ribbonBar5;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private CLDC_MeterUI.UI_Detection_New.ShowDataView.StepUserControl stepUserControl1;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonItem UOnOnly;
        private DevComponents.DotNetBar.ButtonItem UIon;
        private DevComponents.DotNetBar.ButtonItem FreeControl;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem btn_ElectControl;
        private System.Windows.Forms.ToolStripStatusLabel StatusMain_LabLoginMeg;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.ButtonItem btn_FrameSee;
        private DevComponents.DotNetBar.ButtonItem btn_FrmPROTOCOL;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.ButtonItem btn_ModuleTest;
        private DevComponents.DotNetBar.ButtonItem btn_MotorDelay;
    }
}
namespace CLDC_MeterUI
{
    partial class Frm_Setup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Setup));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Menu_New = new System.Windows.Forms.ToolStripButton();
            this.Menu_Open = new System.Windows.Forms.ToolStripSplitButton();
            this.Menu_Save = new System.Windows.Forms.ToolStripButton();
            this.Menu_SaveAs = new System.Windows.Forms.ToolStripButton();
            this.Menu_Del = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menu_Copy = new System.Windows.Forms.ToolStripButton();
            this.Menu_Plaster = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menu_Test = new System.Windows.Forms.ToolStripButton();
            this.Menu_Stop = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LstMenu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.LstMenu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Lst_BaoWen = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.Txt_Clock = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Cmb_ProtocolClass = new System.Windows.Forms.ComboBox();
            this.Cmb_Protocol = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cmb_Bxh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cmb_ZZCJ = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Data = new System.Windows.Forms.Panel();
            this.Panel_SaveName = new System.Windows.Forms.Panel();
            this.Cmd_CancelSave = new System.Windows.Forms.Button();
            this.Cmd_Save = new System.Windows.Forms.Button();
            this.Txt_ProtocolName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Panel_Test = new System.Windows.Forms.Panel();
            this.txtBW = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbU = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Cmd_End = new System.Windows.Forms.Button();
            this.Cmd_Start = new System.Windows.Forms.Button();
            this.Txt_Adr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Panel_SaveName.SuspendLayout();
            this.Panel_Test.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_New,
            this.Menu_Open,
            this.Menu_Save,
            this.Menu_SaveAs,
            this.Menu_Del,
            this.toolStripSeparator1,
            this.Menu_Copy,
            this.Menu_Plaster,
            this.toolStripButton1,
            this.Menu_Test,
            this.Menu_Stop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(807, 48);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Menu_New
            // 
            this.Menu_New.Image = ((System.Drawing.Image)(resources.GetObject("Menu_New.Image")));
            this.Menu_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_New.Name = "Menu_New";
            this.Menu_New.Size = new System.Drawing.Size(36, 45);
            this.Menu_New.Text = "新建";
            this.Menu_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_New.Click += new System.EventHandler(this.Menu_New_Click);
            // 
            // Menu_Open
            // 
            this.Menu_Open.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Open.Image")));
            this.Menu_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Open.Name = "Menu_Open";
            this.Menu_Open.Size = new System.Drawing.Size(48, 45);
            this.Menu_Open.Text = "打开";
            this.Menu_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_Open.ButtonClick += new System.EventHandler(this.Menu_Open_ButtonClick);
            this.Menu_Open.DropDownOpening += new System.EventHandler(this.Menu_Open_DropDownOpening);
            // 
            // Menu_Save
            // 
            this.Menu_Save.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Save.Image")));
            this.Menu_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Save.Name = "Menu_Save";
            this.Menu_Save.Size = new System.Drawing.Size(36, 45);
            this.Menu_Save.Text = "保存";
            this.Menu_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_Save.Click += new System.EventHandler(this.Menu_Save_Click);
            // 
            // Menu_SaveAs
            // 
            this.Menu_SaveAs.Image = ((System.Drawing.Image)(resources.GetObject("Menu_SaveAs.Image")));
            this.Menu_SaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_SaveAs.Name = "Menu_SaveAs";
            this.Menu_SaveAs.Size = new System.Drawing.Size(36, 45);
            this.Menu_SaveAs.Text = "另存";
            this.Menu_SaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_SaveAs.Click += new System.EventHandler(this.Menu_SaveAs_Click);
            // 
            // Menu_Del
            // 
            this.Menu_Del.Enabled = false;
            this.Menu_Del.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Del.Image")));
            this.Menu_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Del.Name = "Menu_Del";
            this.Menu_Del.Size = new System.Drawing.Size(36, 45);
            this.Menu_Del.Text = "删除";
            this.Menu_Del.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_Del.Click += new System.EventHandler(this.Menu_Del_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // Menu_Copy
            // 
            this.Menu_Copy.Enabled = false;
            this.Menu_Copy.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Copy.Image")));
            this.Menu_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Copy.Name = "Menu_Copy";
            this.Menu_Copy.Size = new System.Drawing.Size(36, 45);
            this.Menu_Copy.Text = "复制";
            this.Menu_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Menu_Plaster
            // 
            this.Menu_Plaster.Enabled = false;
            this.Menu_Plaster.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Plaster.Image")));
            this.Menu_Plaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Plaster.Name = "Menu_Plaster";
            this.Menu_Plaster.Size = new System.Drawing.Size(36, 45);
            this.Menu_Plaster.Text = "粘贴";
            this.Menu_Plaster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 48);
            // 
            // Menu_Test
            // 
            this.Menu_Test.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Test.Image")));
            this.Menu_Test.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Test.Name = "Menu_Test";
            this.Menu_Test.Size = new System.Drawing.Size(36, 45);
            this.Menu_Test.Text = "测试";
            this.Menu_Test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_Test.Click += new System.EventHandler(this.Menu_Test_Click);
            // 
            // Menu_Stop
            // 
            this.Menu_Stop.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Stop.Image")));
            this.Menu_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_Stop.Name = "Menu_Stop";
            this.Menu_Stop.Size = new System.Drawing.Size(36, 45);
            this.Menu_Stop.Text = "停止";
            this.Menu_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Menu_Stop.Click += new System.EventHandler(this.Menu_Stop_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LstMenu_Copy,
            this.LstMenu_Clear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // LstMenu_Copy
            // 
            this.LstMenu_Copy.Name = "LstMenu_Copy";
            this.LstMenu_Copy.Size = new System.Drawing.Size(124, 22);
            this.LstMenu_Copy.Text = "复制报文";
            this.LstMenu_Copy.Click += new System.EventHandler(this.LstMenu_Copy_Click);
            // 
            // LstMenu_Clear
            // 
            this.LstMenu_Clear.Name = "LstMenu_Clear";
            this.LstMenu_Clear.Size = new System.Drawing.Size(124, 22);
            this.LstMenu_Clear.Text = "清空报文";
            this.LstMenu_Clear.Click += new System.EventHandler(this.LstMenu_Clear_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.Lst_BaoWen, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Panel_Data, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 468);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // Lst_BaoWen
            // 
            this.Lst_BaoWen.ContextMenuStrip = this.contextMenuStrip1;
            this.Lst_BaoWen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lst_BaoWen.FormattingEnabled = true;
            this.Lst_BaoWen.HorizontalScrollbar = true;
            this.Lst_BaoWen.ItemHeight = 12;
            this.Lst_BaoWen.Location = new System.Drawing.Point(607, 0);
            this.Lst_BaoWen.Margin = new System.Windows.Forms.Padding(0);
            this.Lst_BaoWen.Name = "Lst_BaoWen";
            this.tableLayoutPanel1.SetRowSpan(this.Lst_BaoWen, 2);
            this.Lst_BaoWen.ScrollAlwaysVisible = true;
            this.Lst_BaoWen.Size = new System.Drawing.Size(200, 460);
            this.Lst_BaoWen.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.Txt_Clock);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.Cmb_ProtocolClass);
            this.panel1.Controls.Add(this.Cmb_Protocol);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Cmb_Bxh);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Cmb_ZZCJ);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 60);
            this.panel1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(377, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "（Hz/s）";
            // 
            // Txt_Clock
            // 
            this.Txt_Clock.Location = new System.Drawing.Point(332, 32);
            this.Txt_Clock.Name = "Txt_Clock";
            this.Txt_Clock.Size = new System.Drawing.Size(43, 21);
            this.Txt_Clock.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(271, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "时钟频率：";
            // 
            // Cmb_ProtocolClass
            // 
            this.Cmb_ProtocolClass.FormattingEnabled = true;
            this.Cmb_ProtocolClass.Items.AddRange(new object[] {
            "CDLT6451997",
            "CDLT6452007",
            "CEDMIMK",
            "CIEC1107Standard",
            "ClA1600"});
            this.Cmb_ProtocolClass.Location = new System.Drawing.Point(436, 33);
            this.Cmb_ProtocolClass.Name = "Cmb_ProtocolClass";
            this.Cmb_ProtocolClass.Size = new System.Drawing.Size(151, 20);
            this.Cmb_ProtocolClass.TabIndex = 6;
            this.Cmb_ProtocolClass.Visible = false;
            this.Cmb_ProtocolClass.SelectedIndexChanged += new System.EventHandler(this.Cmb_ProtocolClass_SelectedIndexChanged);
            // 
            // Cmb_Protocol
            // 
            this.Cmb_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Protocol.FormattingEnabled = true;
            this.Cmb_Protocol.Items.AddRange(new object[] {
            "DL/T 645-1997",
            "DL/T 645-2007",
            "红相（MK）",
            "IEC1107（标准）",
            "ABB(A1600)"});
            this.Cmb_Protocol.Location = new System.Drawing.Point(73, 33);
            this.Cmb_Protocol.Name = "Cmb_Protocol";
            this.Cmb_Protocol.Size = new System.Drawing.Size(194, 20);
            this.Cmb_Protocol.TabIndex = 5;
            this.Cmb_Protocol.SelectionChangeCommitted += new System.EventHandler(this.Cmb_Protocol_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "参照协议：";
            // 
            // Cmb_Bxh
            // 
            this.Cmb_Bxh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Bxh.FormattingEnabled = true;
            this.Cmb_Bxh.Location = new System.Drawing.Point(332, 7);
            this.Cmb_Bxh.Name = "Cmb_Bxh";
            this.Cmb_Bxh.Size = new System.Drawing.Size(165, 20);
            this.Cmb_Bxh.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "电表型号：";
            // 
            // Cmb_ZZCJ
            // 
            this.Cmb_ZZCJ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_ZZCJ.FormattingEnabled = true;
            this.Cmb_ZZCJ.Location = new System.Drawing.Point(73, 7);
            this.Cmb_ZZCJ.Name = "Cmb_ZZCJ";
            this.Cmb_ZZCJ.Size = new System.Drawing.Size(194, 20);
            this.Cmb_ZZCJ.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "制造厂家：";
            // 
            // Panel_Data
            // 
            this.Panel_Data.AutoScroll = true;
            this.Panel_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Data.Location = new System.Drawing.Point(0, 60);
            this.Panel_Data.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Data.Name = "Panel_Data";
            this.Panel_Data.Size = new System.Drawing.Size(607, 408);
            this.Panel_Data.TabIndex = 2;
            // 
            // Panel_SaveName
            // 
            this.Panel_SaveName.Controls.Add(this.Cmd_CancelSave);
            this.Panel_SaveName.Controls.Add(this.Cmd_Save);
            this.Panel_SaveName.Controls.Add(this.Txt_ProtocolName);
            this.Panel_SaveName.Controls.Add(this.label4);
            this.Panel_SaveName.Location = new System.Drawing.Point(73, 43);
            this.Panel_SaveName.Name = "Panel_SaveName";
            this.Panel_SaveName.Size = new System.Drawing.Size(223, 60);
            this.Panel_SaveName.TabIndex = 3;
            this.Panel_SaveName.Visible = false;
            // 
            // Cmd_CancelSave
            // 
            this.Cmd_CancelSave.Location = new System.Drawing.Point(117, 34);
            this.Cmd_CancelSave.Name = "Cmd_CancelSave";
            this.Cmd_CancelSave.Size = new System.Drawing.Size(103, 19);
            this.Cmd_CancelSave.TabIndex = 3;
            this.Cmd_CancelSave.Text = "取消保存(&C)";
            this.Cmd_CancelSave.UseVisualStyleBackColor = true;
            this.Cmd_CancelSave.Click += new System.EventHandler(this.Cmd_CancelSave_Click);
            // 
            // Cmd_Save
            // 
            this.Cmd_Save.Location = new System.Drawing.Point(7, 34);
            this.Cmd_Save.Name = "Cmd_Save";
            this.Cmd_Save.Size = new System.Drawing.Size(103, 19);
            this.Cmd_Save.TabIndex = 2;
            this.Cmd_Save.Text = "确认保存(&S)";
            this.Cmd_Save.UseVisualStyleBackColor = true;
            this.Cmd_Save.Click += new System.EventHandler(this.Cmd_Save_Click);
            // 
            // Txt_ProtocolName
            // 
            this.Txt_ProtocolName.Location = new System.Drawing.Point(68, 5);
            this.Txt_ProtocolName.Name = "Txt_ProtocolName";
            this.Txt_ProtocolName.Size = new System.Drawing.Size(152, 21);
            this.Txt_ProtocolName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "协议名称：";
            // 
            // Panel_Test
            // 
            this.Panel_Test.Controls.Add(this.txtBW);
            this.Panel_Test.Controls.Add(this.label7);
            this.Panel_Test.Controls.Add(this.cmbU);
            this.Panel_Test.Controls.Add(this.label6);
            this.Panel_Test.Controls.Add(this.Cmd_End);
            this.Panel_Test.Controls.Add(this.Cmd_Start);
            this.Panel_Test.Controls.Add(this.Txt_Adr);
            this.Panel_Test.Controls.Add(this.label5);
            this.Panel_Test.Location = new System.Drawing.Point(261, 43);
            this.Panel_Test.Name = "Panel_Test";
            this.Panel_Test.Size = new System.Drawing.Size(342, 60);
            this.Panel_Test.TabIndex = 0;
            this.Panel_Test.Visible = false;
            // 
            // txtBW
            // 
            this.txtBW.Location = new System.Drawing.Point(58, 32);
            this.txtBW.Name = "txtBW";
            this.txtBW.Size = new System.Drawing.Size(56, 21);
            this.txtBW.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "表位号：";
            // 
            // cmbU
            // 
            this.cmbU.FormattingEnabled = true;
            this.cmbU.Items.AddRange(new object[] {
            "57.7",
            "100",
            "220",
            "380"});
            this.cmbU.Location = new System.Drawing.Point(161, 32);
            this.cmbU.Name = "cmbU";
            this.cmbU.Size = new System.Drawing.Size(69, 20);
            this.cmbU.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(120, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "电压:";
            // 
            // Cmd_End
            // 
            this.Cmd_End.Location = new System.Drawing.Point(236, 35);
            this.Cmd_End.Name = "Cmd_End";
            this.Cmd_End.Size = new System.Drawing.Size(103, 19);
            this.Cmd_End.TabIndex = 4;
            this.Cmd_End.Text = "取消测试(&C)";
            this.Cmd_End.UseVisualStyleBackColor = true;
            this.Cmd_End.Click += new System.EventHandler(this.Cmd_End_Click);
            // 
            // Cmd_Start
            // 
            this.Cmd_Start.Location = new System.Drawing.Point(236, 8);
            this.Cmd_Start.Name = "Cmd_Start";
            this.Cmd_Start.Size = new System.Drawing.Size(103, 19);
            this.Cmd_Start.TabIndex = 3;
            this.Cmd_Start.Text = "开始测试(&T)";
            this.Cmd_Start.UseVisualStyleBackColor = true;
            this.Cmd_Start.Click += new System.EventHandler(this.Cmd_Start_Click);
            // 
            // Txt_Adr
            // 
            this.Txt_Adr.Location = new System.Drawing.Point(58, 6);
            this.Txt_Adr.Name = "Txt_Adr";
            this.Txt_Adr.Size = new System.Drawing.Size(172, 21);
            this.Txt_Adr.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "表地址：";
            // 
            // Frm_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(807, 516);
            this.Controls.Add(this.Panel_Test);
            this.Controls.Add(this.Panel_SaveName);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Frm_Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电能表通信协议配置器";
            this.Load += new System.EventHandler(this.Frm_Setup_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel_SaveName.ResumeLayout(false);
            this.Panel_SaveName.PerformLayout();
            this.Panel_Test.ResumeLayout(false);
            this.Panel_Test.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Menu_New;
        private System.Windows.Forms.ToolStripButton Menu_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Menu_Del;
        private System.Windows.Forms.ToolStripButton Menu_Copy;
        private System.Windows.Forms.ToolStripButton Menu_Plaster;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton Menu_Test;
        private System.Windows.Forms.ToolStripButton Menu_Stop;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem LstMenu_Copy;
        private System.Windows.Forms.ToolStripMenuItem LstMenu_Clear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox Lst_BaoWen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox Cmb_Protocol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Cmb_Bxh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Cmb_ZZCJ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmb_ProtocolClass;
        private System.Windows.Forms.Panel Panel_Data;
        private System.Windows.Forms.Panel Panel_SaveName;
        private System.Windows.Forms.TextBox Txt_ProtocolName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Cmd_CancelSave;
        private System.Windows.Forms.Button Cmd_Save;
        private System.Windows.Forms.ToolStripSplitButton Menu_Open;
        private System.Windows.Forms.ToolStripButton Menu_SaveAs;
        private System.Windows.Forms.Panel Panel_Test;
        private System.Windows.Forms.Button Cmd_End;
        private System.Windows.Forms.Button Cmd_Start;
        private System.Windows.Forms.TextBox Txt_Adr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbU;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Txt_Clock;
        private System.Windows.Forms.Label label8;
    }
}


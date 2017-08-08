namespace CLDC_MeterUI.UI_FA
{
    partial class Frm_FaSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FaSetup));
            this.Tool_FA = new System.Windows.Forms.ToolStrip();
            this.Tb_New = new System.Windows.Forms.ToolStripButton();
            this.Tb_Save = new System.Windows.Forms.ToolStripButton();
            this.Tb_Open = new System.Windows.Forms.ToolStripButton();
            this.Tb_Del = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Tb_Copy = new System.Windows.Forms.ToolStripButton();
            this.Tb_Plaster = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Tb_Close = new System.Windows.Forms.ToolStripButton();
            this.Tlp_Ole = new System.Windows.Forms.TableLayoutPanel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Panel_Control = new System.Windows.Forms.Panel();
            this.contextMenuStrip4Rename = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Save = new System.Windows.Forms.Panel();
            this.Cmd_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_Save = new DevComponents.DotNetBar.ButtonX();
            this.Txt_FaName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuSort = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.Tv_FaList = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeView();
            this.Tool_FA.SuspendLayout();
            this.Tlp_Ole.SuspendLayout();
            this.contextMenuStrip4Rename.SuspendLayout();
            this.Panel_Save.SuspendLayout();
            this.contextMenuSort.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tool_FA
            // 
            this.Tool_FA.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Tool_FA.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tb_New,
            this.Tb_Save,
            this.Tb_Open,
            this.Tb_Del,
            this.toolStripSeparator1,
            this.Tb_Copy,
            this.Tb_Plaster,
            this.toolStripSeparator2,
            this.Tb_Close});
            this.Tool_FA.Location = new System.Drawing.Point(0, 0);
            this.Tool_FA.Name = "Tool_FA";
            this.Tool_FA.Size = new System.Drawing.Size(1057, 48);
            this.Tool_FA.TabIndex = 1;
            this.Tool_FA.Text = "新建";
            // 
            // Tb_New
            // 
            this.Tb_New.Image = ((System.Drawing.Image)(resources.GetObject("Tb_New.Image")));
            this.Tb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_New.Name = "Tb_New";
            this.Tb_New.Size = new System.Drawing.Size(54, 45);
            this.Tb_New.Text = "新建(&N)";
            this.Tb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_New.ToolTipText = "新建一个方案";
            this.Tb_New.Click += new System.EventHandler(this.Tb_New_Click);
            // 
            // Tb_Save
            // 
            this.Tb_Save.Image = ((System.Drawing.Image)(resources.GetObject("Tb_Save.Image")));
            this.Tb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_Save.Name = "Tb_Save";
            this.Tb_Save.Size = new System.Drawing.Size(51, 45);
            this.Tb_Save.Text = "保存(&S)";
            this.Tb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_Save.ToolTipText = "保存方案";
            this.Tb_Save.Click += new System.EventHandler(this.Tb_Save_Click);
            // 
            // Tb_Open
            // 
            this.Tb_Open.Image = ((System.Drawing.Image)(resources.GetObject("Tb_Open.Image")));
            this.Tb_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_Open.Name = "Tb_Open";
            this.Tb_Open.Size = new System.Drawing.Size(66, 45);
            this.Tb_Open.Text = "另存为(&O)";
            this.Tb_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_Open.ToolTipText = "将方案另存";
            this.Tb_Open.Visible = false;
            // 
            // Tb_Del
            // 
            this.Tb_Del.Image = ((System.Drawing.Image)(resources.GetObject("Tb_Del.Image")));
            this.Tb_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_Del.Name = "Tb_Del";
            this.Tb_Del.Size = new System.Drawing.Size(53, 45);
            this.Tb_Del.Text = "删除(&D)";
            this.Tb_Del.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_Del.ToolTipText = "删除一个方案";
            this.Tb_Del.Click += new System.EventHandler(this.Tb_Del_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // Tb_Copy
            // 
            this.Tb_Copy.Image = ((System.Drawing.Image)(resources.GetObject("Tb_Copy.Image")));
            this.Tb_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_Copy.Name = "Tb_Copy";
            this.Tb_Copy.Size = new System.Drawing.Size(52, 45);
            this.Tb_Copy.Text = "拷贝(&C)";
            this.Tb_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_Copy.ToolTipText = "拷贝整个方案内容";
            this.Tb_Copy.Click += new System.EventHandler(this.Tb_Copy_Click);
            // 
            // Tb_Plaster
            // 
            this.Tb_Plaster.Image = ((System.Drawing.Image)(resources.GetObject("Tb_Plaster.Image")));
            this.Tb_Plaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_Plaster.Name = "Tb_Plaster";
            this.Tb_Plaster.Size = new System.Drawing.Size(52, 45);
            this.Tb_Plaster.Text = "粘贴(&V)";
            this.Tb_Plaster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_Plaster.ToolTipText = "粘贴整个方案内容";
            this.Tb_Plaster.Click += new System.EventHandler(this.Tb_Plaster_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // Tb_Close
            // 
            this.Tb_Close.Image = ((System.Drawing.Image)(resources.GetObject("Tb_Close.Image")));
            this.Tb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tb_Close.Name = "Tb_Close";
            this.Tb_Close.Size = new System.Drawing.Size(52, 45);
            this.Tb_Close.Text = "退出(&X)";
            this.Tb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tb_Close.Click += new System.EventHandler(this.Tb_Close_Click);
            // 
            // Tlp_Ole
            // 
            this.Tlp_Ole.ColumnCount = 2;
            this.Tlp_Ole.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 214F));
            this.Tlp_Ole.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tlp_Ole.Controls.Add(this.Tv_FaList, 0, 0);
            this.Tlp_Ole.Controls.Add(this.Panel_Control, 1, 0);
            this.Tlp_Ole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tlp_Ole.Location = new System.Drawing.Point(0, 48);
            this.Tlp_Ole.Name = "Tlp_Ole";
            this.Tlp_Ole.RowCount = 1;
            this.Tlp_Ole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Tlp_Ole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 475F));
            this.Tlp_Ole.Size = new System.Drawing.Size(1057, 513);
            this.Tlp_Ole.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "00279.ico");
            this.imageList1.Images.SetKeyName(1, "03119.ico");
            this.imageList1.Images.SetKeyName(2, "00387.ico");
            // 
            // Panel_Control
            // 
            this.Panel_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Control.Location = new System.Drawing.Point(217, 3);
            this.Panel_Control.Name = "Panel_Control";
            this.Panel_Control.Size = new System.Drawing.Size(837, 507);
            this.Panel_Control.TabIndex = 1;
            // 
            // contextMenuStrip4Rename
            // 
            this.contextMenuStrip4Rename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.contextMenuStrip4Rename.Enabled = false;
            this.contextMenuStrip4Rename.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4Rename});
            this.contextMenuStrip4Rename.Name = "contextMenuStrip4Rename";
            this.contextMenuStrip4Rename.ShowImageMargin = false;
            this.contextMenuStrip4Rename.Size = new System.Drawing.Size(88, 26);
            // 
            // toolStripMenuItem4Rename
            // 
            this.toolStripMenuItem4Rename.Enabled = false;
            this.toolStripMenuItem4Rename.Name = "toolStripMenuItem4Rename";
            this.toolStripMenuItem4Rename.Size = new System.Drawing.Size(87, 22);
            this.toolStripMenuItem4Rename.Tag = "";
            this.toolStripMenuItem4Rename.Text = "重命名";
            this.toolStripMenuItem4Rename.Click += new System.EventHandler(this.toolStripMenuItem4Rename_Click);
            // 
            // Panel_Save
            // 
            this.Panel_Save.Controls.Add(this.Cmd_Cancel);
            this.Panel_Save.Controls.Add(this.Cmd_Save);
            this.Panel_Save.Controls.Add(this.Txt_FaName);
            this.Panel_Save.Controls.Add(this.label1);
            this.Panel_Save.Location = new System.Drawing.Point(127, 43);
            this.Panel_Save.Name = "Panel_Save";
            this.Panel_Save.Size = new System.Drawing.Size(200, 52);
            this.Panel_Save.TabIndex = 3;
            this.Panel_Save.Visible = false;
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Cancel.Location = new System.Drawing.Point(101, 29);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(96, 20);
            this.Cmd_Cancel.TabIndex = 3;
            this.Cmd_Cancel.Text = "取  消";
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // Cmd_Save
            // 
            this.Cmd_Save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Save.Location = new System.Drawing.Point(5, 29);
            this.Cmd_Save.Name = "Cmd_Save";
            this.Cmd_Save.Size = new System.Drawing.Size(96, 20);
            this.Cmd_Save.TabIndex = 2;
            this.Cmd_Save.Text = "确  认";
            this.Cmd_Save.Click += new System.EventHandler(this.Cmd_Save_Click);
            // 
            // Txt_FaName
            // 
            // 
            // 
            // 
            this.Txt_FaName.Border.Class = "";
            this.Txt_FaName.Location = new System.Drawing.Point(67, 4);
            this.Txt_FaName.Name = "Txt_FaName";
            this.Txt_FaName.Size = new System.Drawing.Size(128, 21);
            this.Txt_FaName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "方案名称：";
            // 
            // contextMenuSort
            // 
            this.contextMenuSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.contextMenuSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUp,
            this.menuDown,
            this.menuDefault});
            this.contextMenuSort.Name = "contextMenuSort";
            this.contextMenuSort.ShowImageMargin = false;
            this.contextMenuSort.Size = new System.Drawing.Size(100, 70);
            // 
            // menuUp
            // 
            this.menuUp.Name = "menuUp";
            this.menuUp.Size = new System.Drawing.Size(99, 22);
            this.menuUp.Text = "上移";
            this.menuUp.Click += new System.EventHandler(this.menuUp_Click);
            // 
            // menuDown
            // 
            this.menuDown.Name = "menuDown";
            this.menuDown.Size = new System.Drawing.Size(99, 22);
            this.menuDown.Text = "下移";
            this.menuDown.Click += new System.EventHandler(this.menuDown_Click);
            // 
            // menuDefault
            // 
            this.menuDefault.Name = "menuDefault";
            this.menuDefault.Size = new System.Drawing.Size(99, 22);
            this.menuDefault.Text = "默认排序";
            this.menuDefault.Visible = false;
            this.menuDefault.Click += new System.EventHandler(this.menuDefault_Click);
            // 
            // Tv_FaList
            // 
            this.Tv_FaList.CheckBoxes = true;
            this.Tv_FaList.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Tv_FaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tv_FaList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tv_FaList.FullRowSelect = true;
            this.Tv_FaList.HideSelection = false;
            this.Tv_FaList.HotTracking = true;
            this.Tv_FaList.ImageIndex = 0;
            this.Tv_FaList.ImageList = this.imageList1;
            this.Tv_FaList.Indent = 22;
            this.Tv_FaList.ItemHeight = 20;
            this.Tv_FaList.LineColor = System.Drawing.Color.Maroon;
            this.Tv_FaList.Location = new System.Drawing.Point(3, 3);
            this.Tv_FaList.Name = "Tv_FaList";
            this.Tv_FaList.SelectedImageIndex = 0;
            this.Tv_FaList.Size = new System.Drawing.Size(208, 507);
            this.Tv_FaList.TabIndex = 0;
            this.Tv_FaList.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.Tv_FaList_AfterLabelEdit);
            this.Tv_FaList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tv_FaList_AfterSelect);
            this.Tv_FaList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Tv_FaList_NodeMouseClick);
            this.Tv_FaList.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.Tv_FaList_BeforeLabelEdit);
            // 
            // Frm_FaSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 561);
            this.Controls.Add(this.Panel_Save);
            this.Controls.Add(this.Tlp_Ole);
            this.Controls.Add(this.Tool_FA);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_FaSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_FaSetup";
            this.Load += new System.EventHandler(this.Frm_FaSetup_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_FaSetup_FormClosed);
            this.Tool_FA.ResumeLayout(false);
            this.Tool_FA.PerformLayout();
            this.Tlp_Ole.ResumeLayout(false);
            this.contextMenuStrip4Rename.ResumeLayout(false);
            this.Panel_Save.ResumeLayout(false);
            this.Panel_Save.PerformLayout();
            this.contextMenuSort.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip Tool_FA;
        private System.Windows.Forms.ToolStripButton Tb_New;
        private System.Windows.Forms.ToolStripButton Tb_Save;
        private System.Windows.Forms.ToolStripButton Tb_Del;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Tb_Copy;
        private System.Windows.Forms.ToolStripButton Tb_Plaster;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton Tb_Close;
        private System.Windows.Forms.TableLayoutPanel Tlp_Ole;
        private CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeView Tv_FaList;
        private System.Windows.Forms.Panel Panel_Control;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel Panel_Save;
        private DevComponents.DotNetBar.ButtonX Cmd_Cancel;
        private DevComponents.DotNetBar.ButtonX Cmd_Save;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_FaName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton Tb_Open;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4Rename;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4Rename;
        private System.Windows.Forms.ContextMenuStrip contextMenuSort;
        private System.Windows.Forms.ToolStripMenuItem menuUp;
        private System.Windows.Forms.ToolStripMenuItem menuDown;
        private System.Windows.Forms.ToolStripMenuItem menuDefault;
    }
}
namespace CLDC_MeterUI.UI_Detection_New
{
    partial class FrameYouSee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameYouSee));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.ctMS_Header = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imglst_IsChecked = new System.Windows.Forms.ImageList(this.components);
            this.lbl_txt = new System.Windows.Forms.Label();
            this.txt_time2 = new System.Windows.Forms.TextBox();
            this.txt_No = new System.Windows.Forms.TextBox();
            this.txt_time1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTiaojian = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.cmb_Type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Grid_seebaowen = new CLDC_MeterUI.CLZDataGridView.DataGrid();
            this.端口号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.项目名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作消息 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.发帧字符串 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.发帧解析 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.发帧时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.收帧字符串 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.收帧解析 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.收帧时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备用 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_seebaowen)).BeginInit();
            this.SuspendLayout();
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Name = "dockContainerItem1";
            this.dockContainerItem1.Text = "dockContainerItem1";
            // 
            // ctMS_Header
            // 
            this.ctMS_Header.Name = "ctMS_Header";
            this.ctMS_Header.Size = new System.Drawing.Size(61, 4);
            // 
            // imglst_IsChecked
            // 
            this.imglst_IsChecked.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst_IsChecked.ImageStream")));
            this.imglst_IsChecked.TransparentColor = System.Drawing.Color.Transparent;
            this.imglst_IsChecked.Images.SetKeyName(0, "photo_add_watermark_pop_03.gif");
            this.imglst_IsChecked.Images.SetKeyName(1, "BOk4.bmp");
            // 
            // lbl_txt
            // 
            this.lbl_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_txt.AutoSize = true;
            this.lbl_txt.Location = new System.Drawing.Point(651, 23);
            this.lbl_txt.Name = "lbl_txt";
            this.lbl_txt.Size = new System.Drawing.Size(11, 12);
            this.lbl_txt.TabIndex = 6;
            this.lbl_txt.Text = "-";
            this.lbl_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_time2
            // 
            this.txt_time2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_time2.Location = new System.Drawing.Point(668, 18);
            this.txt_time2.Name = "txt_time2";
            this.txt_time2.Size = new System.Drawing.Size(119, 21);
            this.txt_time2.TabIndex = 7;
            this.txt_time2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_time2.Click += new System.EventHandler(this.txt_time2_Click);
            // 
            // txt_No
            // 
            this.txt_No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_No.Location = new System.Drawing.Point(841, 18);
            this.txt_No.Name = "txt_No";
            this.txt_No.Size = new System.Drawing.Size(56, 21);
            this.txt_No.TabIndex = 8;
            this.txt_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_time1
            // 
            this.txt_time1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_time1.Location = new System.Drawing.Point(516, 18);
            this.txt_time1.Name = "txt_time1";
            this.txt_time1.Size = new System.Drawing.Size(129, 21);
            this.txt_time1.TabIndex = 2;
            this.txt_time1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_time1.Click += new System.EventHandler(this.txt_time1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(818, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "前";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(433, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "请填发帧时间";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbTiaojian
            // 
            this.cmbTiaojian.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTiaojian.DropDownHeight = 150;
            this.cmbTiaojian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTiaojian.FormattingEnabled = true;
            this.cmbTiaojian.IntegralHeight = false;
            this.cmbTiaojian.Location = new System.Drawing.Point(217, 20);
            this.cmbTiaojian.Name = "cmbTiaojian";
            this.cmbTiaojian.Size = new System.Drawing.Size(185, 20);
            this.cmbTiaojian.TabIndex = 3;
            this.cmbTiaojian.Click += new System.EventHandler(this.cmbTiaojian_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(910, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "条";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Location = new System.Drawing.Point(932, 15);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(86, 29);
            this.btn_Search.TabIndex = 4;
            this.btn_Search.Text = "查找";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // cmb_Type
            // 
            this.cmb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.ItemHeight = 12;
            this.cmb_Type.Location = new System.Drawing.Point(115, 20);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(96, 20);
            this.cmb_Type.TabIndex = 10;
            this.cmb_Type.SelectedIndexChanged += new System.EventHandler(this.cmb_Type_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "请选择查找条件";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb_Type);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbTiaojian);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_time1);
            this.groupBox1.Controls.Add(this.txt_No);
            this.groupBox1.Controls.Add(this.txt_time2);
            this.groupBox1.Controls.Add(this.lbl_txt);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1041, 50);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找条件";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Grid_seebaowen, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1047, 438);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 50);
            this.panel1.TabIndex = 0;
            // 
            // Grid_seebaowen
            // 
            this.Grid_seebaowen.AllowUserToAddRows = false;
            this.Grid_seebaowen.AllowUserToDeleteRows = false;
            this.Grid_seebaowen.AllowUserToResizeRows = false;
            this.Grid_seebaowen.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Grid_seebaowen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid_seebaowen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.端口号,
            this.设备名称,
            this.项目名称,
            this.操作消息,
            this.发帧字符串,
            this.发帧解析,
            this.发帧时间,
            this.收帧字符串,
            this.收帧解析,
            this.收帧时间,
            this.备用});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_seebaowen.DefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_seebaowen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_seebaowen.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Grid_seebaowen.Location = new System.Drawing.Point(3, 63);
            this.Grid_seebaowen.MinimumSize = new System.Drawing.Size(120, 30);
            this.Grid_seebaowen.MultiSelect = false;
            this.Grid_seebaowen.Name = "Grid_seebaowen";
            this.Grid_seebaowen.RowTemplate.Height = 23;
            this.Grid_seebaowen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid_seebaowen.ShowCellErrors = false;
            this.Grid_seebaowen.ShowRowErrors = false;
            this.Grid_seebaowen.Size = new System.Drawing.Size(1041, 372);
            this.Grid_seebaowen.TabIndex = 9;
            this.Grid_seebaowen.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grid_seebaowen_ColumnHeaderMouseClick);
            this.Grid_seebaowen.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Grid_seebaowen_RowPostPaint);
            // 
            // 端口号
            // 
            this.端口号.DataPropertyName = "chrPortNo";
            this.端口号.HeaderText = "端口号";
            this.端口号.MinimumWidth = 60;
            this.端口号.Name = "端口号";
            this.端口号.Width = 91;
            // 
            // 设备名称
            // 
            this.设备名称.DataPropertyName = "chrEquipName";
            this.设备名称.HeaderText = "设备名称";
            this.设备名称.MinimumWidth = 60;
            this.设备名称.Name = "设备名称";
            this.设备名称.Width = 90;
            // 
            // 项目名称
            // 
            this.项目名称.DataPropertyName = "chrItemName";
            this.项目名称.HeaderText = "项目名称";
            this.项目名称.MinimumWidth = 60;
            this.项目名称.Name = "项目名称";
            this.项目名称.Width = 91;
            // 
            // 操作消息
            // 
            this.操作消息.DataPropertyName = "chrMessage";
            this.操作消息.HeaderText = "操作消息";
            this.操作消息.MinimumWidth = 60;
            this.操作消息.Name = "操作消息";
            this.操作消息.Width = 91;
            // 
            // 发帧字符串
            // 
            this.发帧字符串.DataPropertyName = "chrSFrame";
            this.发帧字符串.HeaderText = "发帧字符串";
            this.发帧字符串.MinimumWidth = 60;
            this.发帧字符串.Name = "发帧字符串";
            this.发帧字符串.Width = 91;
            // 
            // 发帧解析
            // 
            this.发帧解析.DataPropertyName = "chrSMeaning";
            this.发帧解析.HeaderText = "发帧解析";
            this.发帧解析.MinimumWidth = 60;
            this.发帧解析.Name = "发帧解析";
            this.发帧解析.Width = 90;
            // 
            // 发帧时间
            // 
            this.发帧时间.DataPropertyName = "chrSTime";
            this.发帧时间.HeaderText = "发帧时间";
            this.发帧时间.MinimumWidth = 60;
            this.发帧时间.Name = "发帧时间";
            this.发帧时间.Width = 91;
            // 
            // 收帧字符串
            // 
            this.收帧字符串.DataPropertyName = "chrRFrame";
            this.收帧字符串.HeaderText = "收帧字符串";
            this.收帧字符串.MinimumWidth = 60;
            this.收帧字符串.Name = "收帧字符串";
            this.收帧字符串.Width = 91;
            // 
            // 收帧解析
            // 
            this.收帧解析.DataPropertyName = "chrRMeaning";
            this.收帧解析.HeaderText = "收帧解析";
            this.收帧解析.MinimumWidth = 60;
            this.收帧解析.Name = "收帧解析";
            this.收帧解析.Width = 91;
            // 
            // 收帧时间
            // 
            this.收帧时间.DataPropertyName = "chrRTime";
            this.收帧时间.HeaderText = "收帧时间";
            this.收帧时间.MinimumWidth = 60;
            this.收帧时间.Name = "收帧时间";
            this.收帧时间.Width = 90;
            // 
            // 备用
            // 
            this.备用.DataPropertyName = "chrOther";
            this.备用.HeaderText = "备用";
            this.备用.MinimumWidth = 60;
            this.备用.Name = "备用";
            this.备用.Width = 91;
            // 
            // FrameYouSee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 438);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrameYouSee";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "报文查看";
            this.Load += new System.EventHandler(this.FrameYouSee_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_seebaowen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        private CLDC_MeterUI.CLZDataGridView.DataGrid Grid_seebaowen;
        private System.Windows.Forms.ContextMenuStrip ctMS_Header;
        private System.Windows.Forms.ImageList imglst_IsChecked;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_Type;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTiaojian;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_time1;
        private System.Windows.Forms.TextBox txt_No;
        private System.Windows.Forms.TextBox txt_time2;
        private System.Windows.Forms.Label lbl_txt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 端口号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项目名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 操作消息;
        private System.Windows.Forms.DataGridViewTextBoxColumn 发帧字符串;
        private System.Windows.Forms.DataGridViewTextBoxColumn 发帧解析;
        private System.Windows.Forms.DataGridViewTextBoxColumn 发帧时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收帧字符串;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收帧解析;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收帧时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备用;

    }
}
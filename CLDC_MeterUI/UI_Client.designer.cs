namespace CLDC_MeterUI
{
    partial class UI_Client
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Client));
            this.labSchemeName = new System.Windows.Forms.Label();
            this.labItem = new System.Windows.Forms.Label();
            this.labAction = new System.Windows.Forms.Label();
            this.table_main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonOk = new DevComponents.DotNetBar.ButtonX();
            this.ButtonRequest = new DevComponents.DotNetBar.ButtonX();
            this.ButtonRequestControl = new DevComponents.DotNetBar.ButtonX();
            this.ButtonSystemConfig = new DevComponents.DotNetBar.ButtonX();
            this.ButtonClose = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GroupBox_Container = new DevComponents.DotNetBar.Controls.GroupPanel(); //DevComponents.DotNetBar.Controls.GroupPanel();
            this.Chk_CheckAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Lab_Mode = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip_TopButtons = new System.Windows.Forms.ToolTip(this.components);
            this.Pic_NetState = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.table_main.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.GroupBox_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_NetState)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // labSchemeName
            // 
            this.labSchemeName.AutoSize = true;
            this.labSchemeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.labSchemeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labSchemeName.Font = new System.Drawing.Font("黑体", 25F);
            this.labSchemeName.ForeColor = System.Drawing.Color.Green;
            this.labSchemeName.Location = new System.Drawing.Point(150, 0);
            this.labSchemeName.Margin = new System.Windows.Forms.Padding(0);
            this.labSchemeName.Name = "labSchemeName";
            this.labSchemeName.Size = new System.Drawing.Size(782, 37);
            this.labSchemeName.TabIndex = 8;
            this.labSchemeName.Text = "---";
            this.labSchemeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labItem
            // 
            this.labItem.AutoSize = true;
            this.labItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.labItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labItem.Font = new System.Drawing.Font("黑体", 25F);
            this.labItem.ForeColor = System.Drawing.Color.Green;
            this.labItem.Location = new System.Drawing.Point(150, 37);
            this.labItem.Margin = new System.Windows.Forms.Padding(0);
            this.labItem.Name = "labItem";
            this.labItem.Size = new System.Drawing.Size(782, 37);
            this.labItem.TabIndex = 9;
            this.labItem.Text = "---";
            this.labItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labAction
            // 
            this.labAction.AutoSize = true;
            this.labAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.labAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labAction.Font = new System.Drawing.Font("黑体", 25F);
            this.labAction.ForeColor = System.Drawing.Color.Green;
            this.labAction.Location = new System.Drawing.Point(150, 74);
            this.labAction.Margin = new System.Windows.Forms.Padding(0);
            this.labAction.Name = "labAction";
            this.labAction.Size = new System.Drawing.Size(782, 39);
            this.labAction.TabIndex = 10;
            this.labAction.Text = "设备就绪，等待操作指令";
            this.labAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // table_main
            // 
            this.table_main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.table_main.ColumnCount = 1;
            this.table_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_main.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.table_main.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.table_main.Controls.Add(this.GroupBox_Container, 0, 2);
            this.table_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table_main.Location = new System.Drawing.Point(0, 0);
            this.table_main.Margin = new System.Windows.Forms.Padding(0);
            this.table_main.Name = "table_main";
            this.table_main.RowCount = 3;
            this.table_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.table_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.table_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table_main.Size = new System.Drawing.Size(938, 590);
            this.table_main.TabIndex = 14;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labSchemeName, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labItem, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.labAction, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(932, 113);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "方案名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("宋体", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(1, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 37);
            this.label3.TabIndex = 12;
            this.label3.Text = "当前项目";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 39);
            this.label4.TabIndex = 13;
            this.label4.Text = "当前操作";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.ButtonClose, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(934, 25);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.ButtonOk, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ButtonRequest, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ButtonRequestControl, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.ButtonSystemConfig, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(474, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(400, 25);
            this.tableLayoutPanel2.TabIndex = 18;
            // 
            // ButtonOk
            // 
            this.ButtonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonOk.Location = new System.Drawing.Point(0, 0);
            this.ButtonOk.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(100, 25);
            this.ButtonOk.TabIndex = 13;
            this.ButtonOk.Text = "录入完毕";
            this.toolTip_TopButtons.SetToolTip(this.ButtonOk, "报告操作完毕");
            this.ButtonOk.Visible = false;
            // 
            // ButtonRequest
            // 
            this.ButtonRequest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonRequest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonRequest.Location = new System.Drawing.Point(100, 0);
            this.ButtonRequest.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonRequest.Name = "ButtonRequest";
            this.ButtonRequest.Size = new System.Drawing.Size(100, 25);
            this.ButtonRequest.TabIndex = 14;
            this.ButtonRequest.Text = "申请扫条码";
            this.toolTip_TopButtons.SetToolTip(this.ButtonRequest, "申请扫条码");
            // 
            // ButtonRequestControl
            // 
            this.ButtonRequestControl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonRequestControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonRequestControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonRequestControl.Location = new System.Drawing.Point(200, 0);
            this.ButtonRequestControl.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonRequestControl.Name = "ButtonRequestControl";
            this.ButtonRequestControl.Size = new System.Drawing.Size(100, 25);
            this.ButtonRequestControl.TabIndex = 15;
            this.ButtonRequestControl.Text = "申请控制";
            this.toolTip_TopButtons.SetToolTip(this.ButtonRequestControl, "请求主动控制");
            // 
            // ButtonSystemConfig
            // 
            this.ButtonSystemConfig.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.ButtonSystemConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonSystemConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSystemConfig.Location = new System.Drawing.Point(300, 0);
            this.ButtonSystemConfig.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonSystemConfig.Name = "ButtonSystemConfig";
            this.ButtonSystemConfig.Size = new System.Drawing.Size(100, 25);
            this.ButtonSystemConfig.TabIndex = 16;
            this.ButtonSystemConfig.Text = "系统配置";
            this.toolTip_TopButtons.SetToolTip(this.ButtonSystemConfig, "打开系统配置");
            // 
            // ButtonClose
            // 
            this.ButtonClose.AutoSize = true;
            this.ButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.ButtonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ButtonClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("ButtonClose.Image")));
            this.ButtonClose.Location = new System.Drawing.Point(904, 0);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(30, 25);
            this.ButtonClose.TabIndex = 19;
            this.ButtonClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(474, 25);
            this.tableLayoutPanel7.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(35, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "欢迎使用CL3000S-H智能综合计量检定平台客户端";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 25);
            this.label5.TabIndex = 2;
            // 
            // GroupBox_Container
            // 
            this.GroupBox_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.GroupBox_Container.Controls.Add(this.Chk_CheckAll);
            this.GroupBox_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.GroupBox_Container.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GroupBox_Container.Location = new System.Drawing.Point(2, 146);
            this.GroupBox_Container.Margin = new System.Windows.Forms.Padding(0);
            this.GroupBox_Container.Name = "GroupBox_Container";
            this.GroupBox_Container.Size = new System.Drawing.Size(934, 442);
            this.GroupBox_Container.TabIndex = 20;
            this.GroupBox_Container.TabStop = false;
            // 
            // Chk_CheckAll
            // 
            this.Chk_CheckAll.AutoSize = true;
            // 
            // 
            // 
            this.Chk_CheckAll.BackgroundStyle.Class = "";
            this.Chk_CheckAll.Location = new System.Drawing.Point(6, 1);
            this.Chk_CheckAll.Name = "Chk_CheckAll";
            this.Chk_CheckAll.Size = new System.Drawing.Size(82, 18);
            this.Chk_CheckAll.TabIndex = 17;
            this.Chk_CheckAll.Text = "全选/反选   ";
            this.Chk_CheckAll.Visible = false;
            // 
            // Lab_Mode
            // 
            this.Lab_Mode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lab_Mode.Location = new System.Drawing.Point(814, 1);
            this.Lab_Mode.Margin = new System.Windows.Forms.Padding(1);
            this.Lab_Mode.Name = "Lab_Mode";
            this.Lab_Mode.Size = new System.Drawing.Size(98, 18);
            this.Lab_Mode.TabIndex = 19;
            this.Lab_Mode.Text = "正式模式";
            this.Lab_Mode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(294, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 138);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Pic_NetState
            // 
            this.Pic_NetState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pic_NetState.Image = ((System.Drawing.Image)(resources.GetObject("Pic_NetState.Image")));
            this.Pic_NetState.Location = new System.Drawing.Point(914, 1);
            this.Pic_NetState.Margin = new System.Windows.Forms.Padding(1);
            this.Pic_NetState.Name = "Pic_NetState";
            this.Pic_NetState.Size = new System.Drawing.Size(18, 18);
            this.Pic_NetState.TabIndex = 0;
            this.Pic_NetState.TabStop = false;
            this.toolTip_TopButtons.SetToolTip(this.Pic_NetState, "网络状态");
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel4.Controls.Add(this.Pic_NetState, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.Lab_Mode, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 590);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(938, 20);
            this.tableLayoutPanel4.TabIndex = 21;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.table_main, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(938, 610);
            this.tableLayoutPanel6.TabIndex = 15;
            // 
            // UI_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(938, 610);
            this.Controls.Add(this.tableLayoutPanel6);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UI_Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.table_main.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.GroupBox_Container.ResumeLayout(false);
            this.GroupBox_Container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_NetState)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labSchemeName;
        private System.Windows.Forms.Label labItem;
        private System.Windows.Forms.Label labAction;
        ////private Monitor Monitor;
        private System.Windows.Forms.TableLayoutPanel table_main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.ButtonX ButtonOk;
        private DevComponents.DotNetBar.ButtonX ButtonRequest;
        private DevComponents.DotNetBar.ButtonX ButtonRequestControl;
        private DevComponents.DotNetBar.ButtonX ButtonSystemConfig;
        private DevComponents.DotNetBar.Controls.CheckBoxX Chk_CheckAll;
        private DevComponents.DotNetBar.Controls.GroupPanel GroupBox_Container;
        private System.Windows.Forms.ToolTip toolTip_TopButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label Lab_Mode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.PictureBox Pic_NetState;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label ButtonClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        // private UI.MarqueeLabel.Marquee marquee1;

    }
}
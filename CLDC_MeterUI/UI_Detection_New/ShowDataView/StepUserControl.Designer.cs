namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    partial class StepUserControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StepUserControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
            this.ToolBtn_StepStart = new DevComponents.DotNetBar.ButtonItem();
            this.ToolBtn_Start = new DevComponents.DotNetBar.ButtonItem();
            this.ToolBtn_Stop = new DevComponents.DotNetBar.ButtonItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.stateBarCircle1 = new CLDC_MeterUI.UI_Detection_New.ShowDataView.StateBarCircle();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.99472F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.00528F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.ribbonBar2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelX3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.stateBarCircle1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelX2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 62);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ribbonBar2
            // 
            this.ribbonBar2.AutoOverflowEnabled = false;
            // 
            // 
            // 
            this.ribbonBar2.BackgroundMouseOverStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar2.BackgroundStyle.Class = "";
            this.ribbonBar2.ContainerControlProcessDialogKey = true;
            this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ToolBtn_StepStart,
            this.ToolBtn_Start,
            this.ToolBtn_Stop});
            this.ribbonBar2.Location = new System.Drawing.Point(562, 1);
            this.ribbonBar2.Margin = new System.Windows.Forms.Padding(1);
            this.ribbonBar2.Name = "ribbonBar2";
            this.tableLayoutPanel1.SetRowSpan(this.ribbonBar2, 2);
            this.ribbonBar2.ShowToolTips = false;
            this.ribbonBar2.Size = new System.Drawing.Size(208, 58);
            this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.ribbonBar2.TabIndex = 2;
            this.ribbonBar2.Text = "方案检定相关功能";
            // 
            // 
            // 
            this.ribbonBar2.TitleStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar2.TitleStyleMouseOver.Class = "";
            this.ribbonBar2.TitleVisible = false;
            this.ribbonBar2.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            this.ribbonBar2.ItemClick += new System.EventHandler(this.ribbonBar2_ItemClick);
            // 
            // ToolBtn_StepStart
            // 
            this.ToolBtn_StepStart.Image = global::CLDC_MeterUI.Properties.Resources.StepStart;
            this.ToolBtn_StepStart.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.ToolBtn_StepStart.Name = "ToolBtn_StepStart";
            this.ToolBtn_StepStart.SubItemsExpandWidth = 14;
            this.ToolBtn_StepStart.Text = "单步检定";
            // 
            // ToolBtn_Start
            // 
            this.ToolBtn_Start.Image = global::CLDC_MeterUI.Properties.Resources.kaishi;
            this.ToolBtn_Start.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.ToolBtn_Start.Name = "ToolBtn_Start";
            this.ToolBtn_Start.SubItemsExpandWidth = 14;
            this.ToolBtn_Start.Text = "连续检定";
            // 
            // ToolBtn_Stop
            // 
            this.ToolBtn_Stop.Image = global::CLDC_MeterUI.Properties.Resources.tingzhi;
            this.ToolBtn_Stop.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.ToolBtn_Stop.Name = "ToolBtn_Stop";
            this.ToolBtn_Stop.SubItemsExpandWidth = 14;
            this.ToolBtn_Stop.Text = "停止检定";
            // 
            // pictureBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 4);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(771, 2);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.tableLayoutPanel1.SetColumnSpan(this.labelX3, 2);
            this.labelX3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(227, 30);
            this.labelX3.Margin = new System.Windows.Forms.Padding(0);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(334, 30);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "当前状态";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.tableLayoutPanel1.SetColumnSpan(this.labelX2, 2);
            this.labelX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(227, 0);
            this.labelX2.Margin = new System.Windows.Forms.Padding(0);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(334, 30);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "当前项目";
            // 
            // stateBarCircle1
            // 
            this.stateBarCircle1.BackColor = System.Drawing.Color.Transparent;
            this.stateBarCircle1.Location = new System.Drawing.Point(0, 0);
            this.stateBarCircle1.Margin = new System.Windows.Forms.Padding(0);
            this.stateBarCircle1.Name = "stateBarCircle1";
            this.tableLayoutPanel1.SetRowSpan(this.stateBarCircle1, 2);
            this.stateBarCircle1.Size = new System.Drawing.Size(225, 60);
            this.stateBarCircle1.TabIndex = 7;
            // 
            // StepUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "StepUserControl";
            this.Size = new System.Drawing.Size(792, 62);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.RibbonBar ribbonBar2;
        private DevComponents.DotNetBar.ButtonItem ToolBtn_StepStart;
        private DevComponents.DotNetBar.ButtonItem ToolBtn_Start;
        private DevComponents.DotNetBar.ButtonItem ToolBtn_Stop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private StateBarCircle stateBarCircle1;
    }
}

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl
{
    partial class CostRatesTime
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel_Back = new CLDC_Comm.ExtendedPanel.ExtendedPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelStepCount = new DevComponents.DotNetBar.LabelX();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxWriteTest = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxWrite1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxReadTest = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Panel_Back.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BackColor = System.Drawing.Color.LightBlue;
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.Color.White;
            this.Panel_Back.CaptionColorTwo = System.Drawing.Color.LightBlue;
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.CaptionSize = 25;
            this.Panel_Back.CaptionTextColor = System.Drawing.Color.Black;
            this.Panel_Back.Controls.Add(this.tableLayoutPanel1);
            this.Panel_Back.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.Panel_Back.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.Panel_Back.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.Panel_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Back.Location = new System.Drawing.Point(0, 0);
            this.Panel_Back.Name = "Panel_Back";
            this.Panel_Back.ShowCheckBox = true;
            this.Panel_Back.Size = new System.Drawing.Size(598, 168);
            this.Panel_Back.TabIndex = 3;
            this.Panel_Back.CheckedChanged += new CLDC_Comm.ExtendedPanel.EventCheckedChanged(this.Panel_Back_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewX1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 29);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 40, 5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 217F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(588, 134);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewX1.ColumnHeadersHeight = 20;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column2});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewX1.Size = new System.Drawing.Size(202, 131);
            this.dataGridViewX1.TabIndex = 3;
            this.dataGridViewX1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewX1_CellBeginEdit);
            this.dataGridViewX1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellEndEdit);
            // 
            // Column3
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.HeaderText = "序号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 40;
            // 
            // Column2
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column2.HeaderText = "费率电价(元)";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.labelStepCount, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonAdd, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonX1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxWriteTest, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxWrite1, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxReadTest, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(208, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(375, 99);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // labelStepCount
            // 
            // 
            // 
            // 
            this.labelStepCount.BackgroundStyle.Class = "";
            this.labelStepCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStepCount.Location = new System.Drawing.Point(3, 3);
            this.labelStepCount.Name = "labelStepCount";
            this.labelStepCount.Size = new System.Drawing.Size(114, 24);
            this.labelStepCount.TabIndex = 1;
            this.labelStepCount.Text = "费率时段数量：";
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Location = new System.Drawing.Point(125, 5);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(70, 20);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "添加一行";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Enabled = false;
            this.buttonX1.Location = new System.Drawing.Point(205, 5);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(68, 20);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "删除一行";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // checkBoxWriteTest
            // 
            this.checkBoxWriteTest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxWriteTest.AutoSize = true;
            // 
            // 
            // 
            this.checkBoxWriteTest.BackgroundStyle.Class = "";
            this.checkBoxWriteTest.Enabled = false;
            this.checkBoxWriteTest.Location = new System.Drawing.Point(3, 70);
            this.checkBoxWriteTest.Name = "checkBoxWriteTest";
            this.checkBoxWriteTest.Size = new System.Drawing.Size(64, 18);
            this.checkBoxWriteTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxWriteTest.TabIndex = 4;
            this.checkBoxWriteTest.Text = "写测试";
            this.checkBoxWriteTest.CheckedChanged += new System.EventHandler(this.checkBoxWriteTest_CheckedChanged);
            // 
            // checkBoxWrite1
            // 
            this.checkBoxWrite1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxWrite1.AutoSize = true;
            // 
            // 
            // 
            this.checkBoxWrite1.BackgroundStyle.Class = "";
            this.checkBoxWrite1.Enabled = false;
            this.checkBoxWrite1.Location = new System.Drawing.Point(123, 70);
            this.checkBoxWrite1.Name = "checkBoxWrite1";
            this.checkBoxWrite1.Size = new System.Drawing.Size(224, 18);
            this.checkBoxWrite1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxWrite1.TabIndex = 4;
            this.checkBoxWrite1.Text = "读到的信息与配置信息一致继续写入";
            // 
            // checkBoxReadTest
            // 
            this.checkBoxReadTest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxReadTest.AutoSize = true;
            // 
            // 
            // 
            this.checkBoxReadTest.BackgroundStyle.Class = "";
            this.checkBoxReadTest.Enabled = false;
            this.checkBoxReadTest.Location = new System.Drawing.Point(3, 36);
            this.checkBoxReadTest.Name = "checkBoxReadTest";
            this.checkBoxReadTest.Size = new System.Drawing.Size(64, 18);
            this.checkBoxReadTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxReadTest.TabIndex = 3;
            this.checkBoxReadTest.Text = "读测试";
            // 
            // DgnRatesTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Back);
            this.Name = "DgnRatesTime";
            this.Size = new System.Drawing.Size(598, 168);
            this.Panel_Back.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.LabelX labelStepCount;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxReadTest;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxWriteTest;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxWrite1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

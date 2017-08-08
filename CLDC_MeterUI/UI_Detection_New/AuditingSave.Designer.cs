namespace CLDC_MeterUI.UI_Detection_New
{
    partial class AuditingSave
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditingSave));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Tab_Data = new System.Windows.Forms.TabControl();
            this.Dgv_Result = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Txt_Month = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Cmd_Save = new DevComponents.DotNetBar.ButtonX();
            this.Cmb_Hyy = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.Cmb_Jyy = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.Cmb_Master = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_Humidity = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Temperature = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Result)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_DoComplated
            // 
            this.Btn_DoComplated.Location = new System.Drawing.Point(570, 10);
            this.Btn_DoComplated.Size = new System.Drawing.Size(120, 27);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tableLayoutPanel1.Controls.Add(this.Tab_Data, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Dgv_Result, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(710, 430);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Tab_Data
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Tab_Data, 2);
            this.Tab_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Data.Location = new System.Drawing.Point(0, 220);
            this.Tab_Data.Margin = new System.Windows.Forms.Padding(0);
            this.Tab_Data.Name = "Tab_Data";
            this.Tab_Data.SelectedIndex = 0;
            this.Tab_Data.Size = new System.Drawing.Size(710, 210);
            this.Tab_Data.TabIndex = 0;
            // 
            // Dgv_Result
            // 
            this.Dgv_Result.AllowUserToAddRows = false;
            this.Dgv_Result.AllowUserToDeleteRows = false;
            this.Dgv_Result.AllowUserToResizeColumns = false;
            this.Dgv_Result.AllowUserToResizeRows = false;
            this.Dgv_Result.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Dgv_Result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Dgv_Result.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Result.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Result.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Result.DefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Result.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Dgv_Result.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Result.Margin = new System.Windows.Forms.Padding(0);
            this.Dgv_Result.MultiSelect = false;
            this.Dgv_Result.Name = "Dgv_Result";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Result.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Result.RowHeadersWidth = 90;
            this.Dgv_Result.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Result.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Dgv_Result.RowTemplate.Height = 23;
            this.Dgv_Result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Result.Size = new System.Drawing.Size(555, 220);
            this.Dgv_Result.TabIndex = 1;
            this.Dgv_Result.SelectionChanged += new System.EventHandler(this.Dgv_Result_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Txt_Month);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Cmd_Save);
            this.groupBox1.Controls.Add(this.Cmb_Hyy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Cmb_Jyy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Cmb_Master);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Txt_Humidity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_Temperature);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(558, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 217);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "存盘操作";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(126, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "月";
            // 
            // Txt_Month
            // 
            // 
            // 
            // 
            this.Txt_Month.Border.Class = "";
            this.Txt_Month.Location = new System.Drawing.Point(55, 146);
            this.Txt_Month.Name = "Txt_Month";
            this.Txt_Month.Size = new System.Drawing.Size(69, 21);
            this.Txt_Month.TabIndex = 14;
            this.Txt_Month.Text = "60";
            this.Txt_Month.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 151);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "有效期：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "℃";
            // 
            // Cmd_Save
            // 
            this.Cmd_Save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Save.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Save.Image")));
            this.Cmd_Save.Location = new System.Drawing.Point(6, 175);
            this.Cmd_Save.Name = "Cmd_Save";
            this.Cmd_Save.Size = new System.Drawing.Size(134, 36);
            this.Cmd_Save.TabIndex = 10;
            this.Cmd_Save.Text = "确认存盘";
            this.Cmd_Save.Click += new System.EventHandler(this.Cmd_Save_Click);
            // 
            // Cmb_Hyy
            // 
            this.Cmb_Hyy.FormattingEnabled = true;
            this.Cmb_Hyy.Location = new System.Drawing.Point(55, 120);
            this.Cmb_Hyy.Name = "Cmb_Hyy";
            this.Cmb_Hyy.Size = new System.Drawing.Size(87, 20);
            this.Cmb_Hyy.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "核验员：";
            // 
            // Cmb_Jyy
            // 
            this.Cmb_Jyy.FormattingEnabled = true;
            this.Cmb_Jyy.Location = new System.Drawing.Point(55, 94);
            this.Cmb_Jyy.Name = "Cmb_Jyy";
            this.Cmb_Jyy.Size = new System.Drawing.Size(87, 20);
            this.Cmb_Jyy.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "检验员：";
            // 
            // Cmb_Master
            // 
            this.Cmb_Master.FormattingEnabled = true;
            this.Cmb_Master.Location = new System.Drawing.Point(55, 68);
            this.Cmb_Master.Name = "Cmb_Master";
            this.Cmb_Master.Size = new System.Drawing.Size(87, 20);
            this.Cmb_Master.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "主  管：";
            // 
            // Txt_Humidity
            // 
            // 
            // 
            // 
            this.Txt_Humidity.Border.Class = "";
            this.Txt_Humidity.Location = new System.Drawing.Point(55, 41);
            this.Txt_Humidity.Name = "Txt_Humidity";
            this.Txt_Humidity.Size = new System.Drawing.Size(69, 21);
            this.Txt_Humidity.TabIndex = 3;
            this.Txt_Humidity.Text = "60";
            this.Txt_Humidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "湿  度：";
            // 
            // Txt_Temperature
            // 
            // 
            // 
            // 
            this.Txt_Temperature.Border.Class = "";
            this.Txt_Temperature.Location = new System.Drawing.Point(55, 14);
            this.Txt_Temperature.Name = "Txt_Temperature";
            this.Txt_Temperature.Size = new System.Drawing.Size(69, 21);
            this.Txt_Temperature.TabIndex = 1;
            this.Txt_Temperature.Text = "21";
            this.Txt_Temperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "温  度：";
            // 
            // AuditingSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AuditingSave";
            this.Size = new System.Drawing.Size(710, 430);
            this.Load += new System.EventHandler(this.AuditingSave_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.Btn_DoComplated, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Result)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl Tab_Data;
        private DevComponents.DotNetBar.Controls.DataGridViewX Dgv_Result;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_Humidity;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_Temperature;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX Cmd_Save;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Hyy;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Jyy;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Master;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_Month;
        private System.Windows.Forms.Label label9;
    }
}

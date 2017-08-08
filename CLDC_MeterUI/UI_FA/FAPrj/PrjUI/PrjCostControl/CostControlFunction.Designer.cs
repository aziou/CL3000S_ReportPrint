namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl
{
    partial class CostControlFunction
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
            this.Panel_Back = new CLDC_Comm.ExtendedPanel.ExtendedPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWarnMoney1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWarnMoney2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbZzi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Panel_Back.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.Color.White;
            this.Panel_Back.CaptionColorTwo = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.Panel_Back.Size = new System.Drawing.Size(486, 207);
            this.Panel_Back.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtWarnMoney1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtWarnMoney2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbZzi, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 207);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 4);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "    当电能表内剩余金额小于或等于设定的报警金额时，电能表应能以声、光或其他方式提醒用户；透支金额应实时记录，当透支金额低于设定的透支门限金额时，电能表应发出断" +
                "电信号，控制负荷开关中断供电；当电能表接收到有效的续交电费信息后，应首先扣除透支金额，当剩余金额大于设定值（默认为零）时，方可通过远程或本地方式使电能表处于允许" +
                "合闸状态，由人工本地恢复供电。\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 10, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "报警金额1";
            // 
            // txtWarnMoney1
            // 
            this.txtWarnMoney1.Location = new System.Drawing.Point(93, 30);
            this.txtWarnMoney1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.txtWarnMoney1.Name = "txtWarnMoney1";
            this.txtWarnMoney1.Size = new System.Drawing.Size(84, 21);
            this.txtWarnMoney1.TabIndex = 2;
            this.txtWarnMoney1.Text = "20";
            this.txtWarnMoney1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWarnMoney1.TextChanged += new System.EventHandler(this.txtWarnMoney1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 10, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "报警金额2";
            // 
            // txtWarnMoney2
            // 
            this.txtWarnMoney2.Location = new System.Drawing.Point(93, 62);
            this.txtWarnMoney2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.txtWarnMoney2.Name = "txtWarnMoney2";
            this.txtWarnMoney2.Size = new System.Drawing.Size(84, 21);
            this.txtWarnMoney2.TabIndex = 4;
            this.txtWarnMoney2.Text = "10";
            this.txtWarnMoney2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWarnMoney2.TextChanged += new System.EventHandler(this.txtWarnMoney1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 8, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "走字电流";
            // 
            // cmbZzi
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbZzi, 2);
            this.cmbZzi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZzi.FormattingEnabled = true;
            this.cmbZzi.Location = new System.Drawing.Point(93, 94);
            this.cmbZzi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.cmbZzi.Name = "cmbZzi";
            this.cmbZzi.Size = new System.Drawing.Size(114, 20);
            this.cmbZzi.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "元";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 67);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "元";
            // 
            // CostControlFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Back);
            this.Name = "CostControlFunction";
            this.Size = new System.Drawing.Size(486, 207);
            this.Panel_Back.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWarnMoney1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWarnMoney2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbZzi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

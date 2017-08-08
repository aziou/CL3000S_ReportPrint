namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    partial class UI_InsulationItem
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
            this.container = new CLDC_Comm.ExtendedPanel.ExtendedPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxXVoltage = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxXCurrent = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxXTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxXCurrentAll = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label8 = new System.Windows.Forms.Label();
            this.container.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.BackColor = System.Drawing.Color.LightBlue;
            this.container.BorderColor = System.Drawing.Color.Gray;
            this.container.CaptionColorOne = System.Drawing.Color.White;
            this.container.CaptionColorTwo = System.Drawing.Color.LightBlue;
            this.container.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.container.CaptionSize = 25;
            this.container.CaptionTextColor = System.Drawing.Color.Black;
            this.container.Controls.Add(this.tableLayoutPanel1);
            this.container.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.container.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.container.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Name = "container";
            this.container.ShowCheckBox = true;
            this.container.Size = new System.Drawing.Size(657, 120);
            this.container.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxXVoltage, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxXCurrent, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxXTime, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxXCurrentAll, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 5, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(454, 80);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // textBoxXVoltage
            // 
            this.textBoxXVoltage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.textBoxXVoltage.Border.Class = "TextBoxBorder";
            this.textBoxXVoltage.Location = new System.Drawing.Point(82, 9);
            this.textBoxXVoltage.Name = "textBoxXVoltage";
            this.textBoxXVoltage.Size = new System.Drawing.Size(75, 21);
            this.textBoxXVoltage.TabIndex = 2;
            this.textBoxXVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXVoltage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXTime_KeyPress);
            this.textBoxXVoltage.TextChanged += new System.EventHandler(this.textBoxX_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "伏";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "耐压电压值";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "误差板漏电流";
            // 
            // textBoxXCurrent
            // 
            this.textBoxXCurrent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.textBoxXCurrent.Border.Class = "TextBoxBorder";
            this.textBoxXCurrent.Location = new System.Drawing.Point(314, 9);
            this.textBoxXCurrent.Name = "textBoxXCurrent";
            this.textBoxXCurrent.Size = new System.Drawing.Size(75, 21);
            this.textBoxXCurrent.TabIndex = 4;
            this.textBoxXCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXCurrent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXTime_KeyPress);
            this.textBoxXCurrent.TextChanged += new System.EventHandler(this.textBoxX_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "毫安";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "耐压时间";
            // 
            // textBoxXTime
            // 
            this.textBoxXTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.textBoxXTime.Border.Class = "TextBoxBorder";
            this.textBoxXTime.Location = new System.Drawing.Point(82, 49);
            this.textBoxXTime.Name = "textBoxXTime";
            this.textBoxXTime.Size = new System.Drawing.Size(75, 21);
            this.textBoxXTime.TabIndex = 2;
            this.textBoxXTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXTime_KeyPress);
            this.textBoxXTime.TextChanged += new System.EventHandler(this.textBoxX_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "秒";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "耐压仪漏电流";
            // 
            // textBoxXCurrentAll
            // 
            this.textBoxXCurrentAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.textBoxXCurrentAll.Border.Class = "TextBoxBorder";
            this.textBoxXCurrentAll.Location = new System.Drawing.Point(314, 49);
            this.textBoxXCurrentAll.Name = "textBoxXCurrentAll";
            this.textBoxXCurrentAll.Size = new System.Drawing.Size(75, 21);
            this.textBoxXCurrentAll.TabIndex = 4;
            this.textBoxXCurrentAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxXCurrentAll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxXTime_KeyPress);
            this.textBoxXCurrentAll.TextChanged += new System.EventHandler(this.textBoxX_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(397, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "毫安";
            // 
            // UI_InsulationItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Name = "UI_InsulationItem";
            this.Size = new System.Drawing.Size(657, 120);
            this.container.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel container;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXCurrent;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXTime;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXVoltage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxXCurrentAll;
        private System.Windows.Forms.Label label8;
    }
}

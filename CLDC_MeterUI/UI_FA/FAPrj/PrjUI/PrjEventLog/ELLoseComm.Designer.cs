namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjEventLog
{
    partial class ELLoseComm
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
            this.label20 = new System.Windows.Forms.Label();
            this.Txt_TestNum = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.Txt_Recover = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Txt_Keep = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Panel_Back.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BackColor = System.Drawing.SystemColors.Control;
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.Color.Silver;
            this.Panel_Back.CaptionColorTwo = System.Drawing.SystemColors.Control;
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.CaptionSize = 25;
            this.Panel_Back.CaptionTextColor = System.Drawing.Color.Black;
            this.Panel_Back.Controls.Add(this.label20);
            this.Panel_Back.Controls.Add(this.Txt_TestNum);
            this.Panel_Back.Controls.Add(this.label21);
            this.Panel_Back.Controls.Add(this.label18);
            this.Panel_Back.Controls.Add(this.Txt_Recover);
            this.Panel_Back.Controls.Add(this.label19);
            this.Panel_Back.Controls.Add(this.label16);
            this.Panel_Back.Controls.Add(this.label17);
            this.Panel_Back.Controls.Add(this.Txt_Keep);
            this.Panel_Back.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.Panel_Back.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.Panel_Back.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.Panel_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Back.Location = new System.Drawing.Point(0, 0);
            this.Panel_Back.Name = "Panel_Back";
            this.Panel_Back.ShowCheckBox = true;
            this.Panel_Back.Size = new System.Drawing.Size(452, 85);
            this.Panel_Back.TabIndex = 1;
            this.Panel_Back.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Back_Paint);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Enabled = false;
            this.label20.Location = new System.Drawing.Point(396, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 12);
            this.label20.TabIndex = 22;
            this.label20.Text = "（次）";
            // 
            // Txt_TestNum
            // 
            // 
            // 
            // 
            this.Txt_TestNum.Border.Class = "";
            this.Txt_TestNum.Enabled = false;
            this.Txt_TestNum.Location = new System.Drawing.Point(363, 44);
            this.Txt_TestNum.Name = "Txt_TestNum";
            this.Txt_TestNum.Size = new System.Drawing.Size(30, 21);
            this.Txt_TestNum.TabIndex = 21;
            this.Txt_TestNum.Text = "1";
            this.Txt_TestNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Enabled = false;
            this.label21.Location = new System.Drawing.Point(304, 49);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 20;
            this.label21.Text = "试验次数：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Enabled = false;
            this.label18.Location = new System.Drawing.Point(262, 49);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 19;
            this.label18.Text = "（分）";
            // 
            // Txt_Recover
            // 
            // 
            // 
            // 
            this.Txt_Recover.Border.Class = "";
            this.Txt_Recover.Enabled = false;
            this.Txt_Recover.Location = new System.Drawing.Point(229, 44);
            this.Txt_Recover.Name = "Txt_Recover";
            this.Txt_Recover.Size = new System.Drawing.Size(30, 21);
            this.Txt_Recover.TabIndex = 18;
            this.Txt_Recover.Text = "1";
            this.Txt_Recover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Enabled = false;
            this.label19.Location = new System.Drawing.Point(170, 49);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 17;
            this.label19.Text = "恢复时间：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Enabled = false;
            this.label16.Location = new System.Drawing.Point(128, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 16;
            this.label16.Text = "（分）";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Enabled = false;
            this.label17.Location = new System.Drawing.Point(28, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 14;
            this.label17.Text = "保持时间：";
            // 
            // Txt_Keep
            // 
            // 
            // 
            // 
            this.Txt_Keep.Border.Class = "";
            this.Txt_Keep.Enabled = false;
            this.Txt_Keep.Location = new System.Drawing.Point(95, 44);
            this.Txt_Keep.Name = "Txt_Keep";
            this.Txt_Keep.Size = new System.Drawing.Size(30, 21);
            this.Txt_Keep.TabIndex = 15;
            this.Txt_Keep.Text = "1";
            this.Txt_Keep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ELLoseComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Back);
            this.Name = "ELLoseComm";
            this.Size = new System.Drawing.Size(452, 85);
            this.Panel_Back.ResumeLayout(false);
            this.Panel_Back.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private System.Windows.Forms.Label label20;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_TestNum;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label18;
        /// <summary>
        /// 恢复等待时间
        /// </summary>
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_Recover;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        /// <summary>
        /// 保持时间
        /// </summary>
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_Keep;
        private System.Windows.Forms.Label label17;
        
    }
}

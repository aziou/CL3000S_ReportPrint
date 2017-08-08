namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjPrePare
{
    partial class PreDayClock
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
            this.label2 = new System.Windows.Forms.Label();
            this.TxtTestCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtWcCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_Wcx = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Panel_Back.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.Color.Silver;
            this.Panel_Back.CaptionColorTwo = System.Drawing.Color.SeaShell;
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.CaptionSize = 25;
            this.Panel_Back.CaptionTextColor = System.Drawing.Color.Black;
            this.Panel_Back.Controls.Add(this.label2);
            this.Panel_Back.Controls.Add(this.TxtTestCount);
            this.Panel_Back.Controls.Add(this.label4);
            this.Panel_Back.Controls.Add(this.TxtWcCount);
            this.Panel_Back.Controls.Add(this.label3);
            this.Panel_Back.Controls.Add(this.Txt_Wcx);
            this.Panel_Back.Controls.Add(this.label1);
            this.Panel_Back.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.Panel_Back.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.Panel_Back.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.Panel_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Back.Location = new System.Drawing.Point(0, 0);
            this.Panel_Back.Name = "Panel_Back";
            this.Panel_Back.ShowCheckBox = true;
            this.Panel_Back.Size = new System.Drawing.Size(598, 62);
            this.Panel_Back.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(195, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "（注：比例应该为数字）";
            // 
            // TxtTestCount
            // 
            // 
            // 
            // 
            this.TxtTestCount.Border.Class = "";
            this.TxtTestCount.Enabled = false;
            this.TxtTestCount.Location = new System.Drawing.Point(542, 31);
            this.TxtTestCount.Name = "TxtTestCount";
            this.TxtTestCount.Size = new System.Drawing.Size(35, 21);
            this.TxtTestCount.TabIndex = 7;
            this.TxtTestCount.Text = "60";
            this.TxtTestCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(471, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "检定圈数：";
            // 
            // TxtWcCount
            // 
            // 
            // 
            // 
            this.TxtWcCount.Border.Class = "";
            this.TxtWcCount.Enabled = false;
            this.TxtWcCount.Location = new System.Drawing.Point(409, 31);
            this.TxtWcCount.Name = "TxtWcCount";
            this.TxtWcCount.Size = new System.Drawing.Size(35, 21);
            this.TxtWcCount.TabIndex = 5;
            this.TxtWcCount.Text = "5";
            this.TxtWcCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtWcCount.Leave += new System.EventHandler(this.TxtWcCount_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(338, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "误差次数：";
            // 
            // Txt_Wcx
            // 
            // 
            // 
            // 
            this.Txt_Wcx.Border.Class = "";
            this.Txt_Wcx.Enabled = false;
            this.Txt_Wcx.Location = new System.Drawing.Point(158, 28);
            this.Txt_Wcx.Name = "Txt_Wcx";
            this.Txt_Wcx.Size = new System.Drawing.Size(35, 21);
            this.Txt_Wcx.TabIndex = 2;
            this.Txt_Wcx.Text = "1";
            this.Txt_Wcx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "日计时误差限控制比例：";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "";
            this.textBoxX1.Location = new System.Drawing.Point(0, 0);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(100, 21);
            this.textBoxX1.TabIndex = 0;
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "";
            this.textBoxX2.Location = new System.Drawing.Point(0, 0);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(100, 21);
            this.textBoxX2.TabIndex = 0;
            // 
            // DgnDayCheckTime
            // 
            this.Controls.Add(this.Panel_Back);
            this.Name = "DgnDayCheckTime";
            this.Size = new System.Drawing.Size(598, 62);
            this.Panel_Back.ResumeLayout(false);
            this.Panel_Back.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_Wcx;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX TxtTestCount;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.TextBoxX TxtWcCount;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
    }
}

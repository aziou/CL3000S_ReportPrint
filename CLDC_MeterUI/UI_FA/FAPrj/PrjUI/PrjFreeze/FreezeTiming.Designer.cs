namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFreeze
{
    partial class FreezeTiming
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
            this.chkHour = new System.Windows.Forms.CheckBox();
            this.chk_Day = new System.Windows.Forms.CheckBox();
            this.lblName = new System.Windows.Forms.Label();
            this.chk_Month = new System.Windows.Forms.CheckBox();
            this.Panel_Back.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.SystemColors.Control;
            this.Panel_Back.CaptionColorTwo = System.Drawing.Color.LightBlue;
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.CaptionSize = 25;
            this.Panel_Back.CaptionTextColor = System.Drawing.Color.Black;
            this.Panel_Back.Controls.Add(this.chkHour);
            this.Panel_Back.Controls.Add(this.chk_Day);
            this.Panel_Back.Controls.Add(this.lblName);
            this.Panel_Back.Controls.Add(this.chk_Month);
            this.Panel_Back.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.Panel_Back.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.Panel_Back.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.Panel_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Back.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.Location = new System.Drawing.Point(0, 0);
            this.Panel_Back.Name = "Panel_Back";
            this.Panel_Back.ShowCheckBox = true;
            this.Panel_Back.Size = new System.Drawing.Size(509, 84);
            this.Panel_Back.TabIndex = 0;
            // 
            // chkHour
            // 
            this.chkHour.AutoSize = true;
            this.chkHour.Enabled = false;
            this.chkHour.Location = new System.Drawing.Point(169, 53);
            this.chkHour.Name = "chkHour";
            this.chkHour.Size = new System.Drawing.Size(40, 18);
            this.chkHour.TabIndex = 20;
            this.chkHour.Text = "时";
            this.chkHour.UseVisualStyleBackColor = true;
            // 
            // chk_Day
            // 
            this.chk_Day.AutoSize = true;
            this.chk_Day.Enabled = false;
            this.chk_Day.Location = new System.Drawing.Point(101, 53);
            this.chk_Day.Name = "chk_Day";
            this.chk_Day.Size = new System.Drawing.Size(40, 18);
            this.chk_Day.TabIndex = 19;
            this.chk_Day.Text = "日";
            this.chk_Day.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 32);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(77, 14);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "冻结周期：";
            // 
            // chk_Month
            // 
            this.chk_Month.AutoSize = true;
            this.chk_Month.Checked = true;
            this.chk_Month.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Month.Enabled = false;
            this.chk_Month.Location = new System.Drawing.Point(33, 53);
            this.chk_Month.Name = "chk_Month";
            this.chk_Month.Size = new System.Drawing.Size(40, 18);
            this.chk_Month.TabIndex = 17;
            this.chk_Month.Text = "月";
            this.chk_Month.UseVisualStyleBackColor = true;
            // 
            // FreezeTiming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.Panel_Back);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FreezeTiming";
            this.Size = new System.Drawing.Size(509, 84);
            this.Panel_Back.ResumeLayout(false);
            this.Panel_Back.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox chk_Month;
        private System.Windows.Forms.CheckBox chkHour;
        private System.Windows.Forms.CheckBox chk_Day;
    }
}

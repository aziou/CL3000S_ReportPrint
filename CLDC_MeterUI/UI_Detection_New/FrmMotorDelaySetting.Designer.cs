namespace CLDC_MeterUI.UI_Detection_New
{
    partial class FrmMotorDelaySetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMotorDelaySetting));
            this.motorDelayTimeControl1 = new CLDC_MeterUI.MotorDelayTimeControl();
            this.SuspendLayout();
            // 
            // motorDelayTimeControl1
            // 
            this.motorDelayTimeControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.motorDelayTimeControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.motorDelayTimeControl1.Location = new System.Drawing.Point(0, 0);
            this.motorDelayTimeControl1.MinimumSize = new System.Drawing.Size(800, 360);
            this.motorDelayTimeControl1.Name = "motorDelayTimeControl1";
            this.motorDelayTimeControl1.Size = new System.Drawing.Size(936, 416);
            this.motorDelayTimeControl1.TabIndex = 0;
            // 
            // FrmMotorDelaySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(936, 416);
            this.Controls.Add(this.motorDelayTimeControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMotorDelaySetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "电机延时设置";
            this.ResumeLayout(false);

        }

        #endregion

        private MotorDelayTimeControl motorDelayTimeControl1;
    }
}
namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    partial class StateBarCircle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateBarCircle));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lab_TotalTime = new DevComponents.DotNetBar.LabelX();
            this.lab_LastTime = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(31, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 1;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lab_TotalTime
            // 
            // 
            // 
            // 
            this.lab_TotalTime.BackgroundStyle.Class = "";
            this.lab_TotalTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_TotalTime.Location = new System.Drawing.Point(66, 3);
            this.lab_TotalTime.Margin = new System.Windows.Forms.Padding(0);
            this.lab_TotalTime.Name = "lab_TotalTime";
            this.lab_TotalTime.Size = new System.Drawing.Size(159, 28);
            this.lab_TotalTime.TabIndex = 2;
            this.lab_TotalTime.Text = "预计：1小时17分17秒";
            this.lab_TotalTime.TextLineAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lab_LastTime
            // 
            // 
            // 
            // 
            this.lab_LastTime.BackgroundStyle.Class = "";
            this.lab_LastTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_LastTime.Location = new System.Drawing.Point(66, 35);
            this.lab_LastTime.Margin = new System.Windows.Forms.Padding(0);
            this.lab_LastTime.Name = "lab_LastTime";
            this.lab_LastTime.Size = new System.Drawing.Size(159, 28);
            this.lab_LastTime.TabIndex = 3;
            this.lab_LastTime.Text = "剩余：1小时17分17秒";
            this.lab_LastTime.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StateBarCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lab_LastTime);
            this.Controls.Add(this.lab_TotalTime);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "StateBarCircle";
            this.Size = new System.Drawing.Size(225, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.LabelX lab_TotalTime;
        private DevComponents.DotNetBar.LabelX lab_LastTime;
    }
}

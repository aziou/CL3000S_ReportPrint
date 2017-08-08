namespace CLDC_ManagerStart
{
    partial class ManagerStart
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbManagerName = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtManagerPass = new System.Windows.Forms.TextBox();
            this.labPass = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbManagerName);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtManagerPass);
            this.groupBox1.Controls.Add(this.labPass);
            this.groupBox1.Controls.Add(this.labName);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(326, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报表打印登陆";
            // 
            // cmbManagerName
            // 
            this.cmbManagerName.FormattingEnabled = true;
            this.cmbManagerName.Location = new System.Drawing.Point(100, 46);
            this.cmbManagerName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbManagerName.Name = "cmbManagerName";
            this.cmbManagerName.Size = new System.Drawing.Size(158, 29);
            this.cmbManagerName.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 126);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "登陆";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtManagerPass
            // 
            this.txtManagerPass.Location = new System.Drawing.Point(100, 82);
            this.txtManagerPass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtManagerPass.Name = "txtManagerPass";
            this.txtManagerPass.PasswordChar = '*';
            this.txtManagerPass.Size = new System.Drawing.Size(158, 29);
            this.txtManagerPass.TabIndex = 1;
            // 
            // labPass
            // 
            this.labPass.AutoSize = true;
            this.labPass.Location = new System.Drawing.Point(44, 85);
            this.labPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPass.Name = "labPass";
            this.labPass.Size = new System.Drawing.Size(52, 21);
            this.labPass.TabIndex = 0;
            this.labPass.Text = "密  码";
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(44, 48);
            this.labName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(58, 21);
            this.labName.TabIndex = 0;
            this.labName.Text = "用户名";
            // 
            // ManagerStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 197);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ManagerStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登陆";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbManagerName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtManagerPass;
        private System.Windows.Forms.Label labPass;
        private System.Windows.Forms.Label labName;
    }
}


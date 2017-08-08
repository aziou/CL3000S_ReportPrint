namespace CLDC_MeterUI.UI_Detection_New
{
    partial class FrmNewFA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewFA));
            this.Lab_FaName = new System.Windows.Forms.Label();
            this.Cmb_FaList = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Cmd_Ok = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_Cancel = new DevComponents.DotNetBar.ButtonX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Lab_FaName
            // 
            this.Lab_FaName.AutoSize = true;
            this.Lab_FaName.BackColor = System.Drawing.Color.Transparent;
            this.Lab_FaName.Location = new System.Drawing.Point(15, 9);
            this.Lab_FaName.Name = "Lab_FaName";
            this.Lab_FaName.Size = new System.Drawing.Size(77, 12);
            this.Lab_FaName.TabIndex = 0;
            this.Lab_FaName.Text = "请选择方案：";
            // 
            // Cmb_FaList
            // 
            this.Cmb_FaList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_FaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_FaList.FormattingEnabled = true;
            this.Cmb_FaList.Location = new System.Drawing.Point(89, 5);
            this.Cmb_FaList.Name = "Cmb_FaList";
            this.Cmb_FaList.Size = new System.Drawing.Size(199, 22);
            this.Cmb_FaList.TabIndex = 1;
            // 
            // Cmd_Ok
            // 
            this.Cmd_Ok.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Ok.Location = new System.Drawing.Point(89, 31);
            this.Cmd_Ok.Name = "Cmd_Ok";
            this.Cmd_Ok.Size = new System.Drawing.Size(83, 23);
            this.Cmd_Ok.TabIndex = 2;
            this.Cmd_Ok.Text = "确  认(&O)";
            this.Cmd_Ok.Click += new System.EventHandler(this.Cmd_Ok_Click);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Cancel.Location = new System.Drawing.Point(205, 31);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(83, 23);
            this.Cmd_Cancel.TabIndex = 3;
            this.Cmd_Cancel.Text = "取  消(&C)";
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(13, 61);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Location = new System.Drawing.Point(293, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(13, 61);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // FrmNewFA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(306, 61);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Cmd_Cancel);
            this.Controls.Add(this.Cmd_Ok);
            this.Controls.Add(this.Cmb_FaList);
            this.Controls.Add(this.Lab_FaName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNewFA";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmNewFA";
            this.Load += new System.EventHandler(this.FrmNewFA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_FaName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_FaList;
        private DevComponents.DotNetBar.ButtonX Cmd_Ok;
        private DevComponents.DotNetBar.ButtonX Cmd_Cancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
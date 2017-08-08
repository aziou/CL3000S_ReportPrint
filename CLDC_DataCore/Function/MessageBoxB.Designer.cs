namespace CLDC_DataCore.Function
{
    partial class MessageBoxB
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_Yes = new System.Windows.Forms.Button();
            this.Btn_No = new System.Windows.Forms.Button();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_Yes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Btn_No, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Lab_Msg, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(369, 105);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Btn_Yes
            // 
            this.Btn_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Btn_Yes.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Yes.Location = new System.Drawing.Point(77, 65);
            this.Btn_Yes.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Yes.Name = "Btn_Yes";
            this.Btn_Yes.Size = new System.Drawing.Size(102, 30);
            this.Btn_Yes.TabIndex = 0;
            this.Btn_Yes.Text = "是(&Yes)";
            this.Btn_Yes.UseVisualStyleBackColor = true;
            // 
            // Btn_No
            // 
            this.Btn_No.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Btn_No.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_No.Location = new System.Drawing.Point(189, 65);
            this.Btn_No.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_No.Name = "Btn_No";
            this.Btn_No.Size = new System.Drawing.Size(100, 30);
            this.Btn_No.TabIndex = 1;
            this.Btn_No.Text = "否(&No)";
            this.Btn_No.UseVisualStyleBackColor = true;
            // 
            // Lab_Msg
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Lab_Msg, 3);
            this.Lab_Msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lab_Msg.Location = new System.Drawing.Point(3, 0);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(363, 65);
            this.Lab_Msg.TabIndex = 2;
            this.Lab_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageBoxB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 105);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Btn_Yes;
        private System.Windows.Forms.Button Btn_No;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.Timer timer1;
    }
}
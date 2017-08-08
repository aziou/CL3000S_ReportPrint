namespace CLDC_DataCore.Function
{
    partial class BindCombox_NewValue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BindCombox_NewValue));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_Value = new System.Windows.Forms.TextBox();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Txt_Value, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Ok, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Cancel, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(257, 99);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入新的选项数据";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_Value
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Txt_Value, 2);
            this.Txt_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_Value.Font = new System.Drawing.Font("宋体", 12F);
            this.Txt_Value.Location = new System.Drawing.Point(3, 28);
            this.Txt_Value.MaxLength = 20;
            this.Txt_Value.Name = "Txt_Value";
            this.Txt_Value.Size = new System.Drawing.Size(251, 26);
            this.Txt_Value.TabIndex = 1;
            this.Txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Value_KeyDown);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Ok.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Ok.Image")));
            this.Btn_Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Ok.Location = new System.Drawing.Point(27, 58);
            this.Btn_Ok.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(91, 38);
            this.Btn_Ok.TabIndex = 2;
            this.Btn_Ok.Text = "确定";
            this.Btn_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Cancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Cancel.Image")));
            this.Btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Cancel.Location = new System.Drawing.Point(128, 58);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(90, 38);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // BindCombox_NewValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 99);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BindCombox_NewValue";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加新项";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BindCombox_NewValue_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// 
        /// </summary>
        public  System.Windows.Forms.TextBox Txt_Value;
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.Button Btn_Cancel;
    }
}
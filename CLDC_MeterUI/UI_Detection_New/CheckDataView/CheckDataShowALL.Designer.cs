namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    partial class CheckDataShowALL
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX_ShowALL = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX_ShowALL)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX_ShowALL
            // 
            this.dataGridViewX_ShowALL.AllowUserToAddRows = false;
            this.dataGridViewX_ShowALL.AllowUserToDeleteRows = false;
            this.dataGridViewX_ShowALL.AllowUserToResizeRows = false;
            this.dataGridViewX_ShowALL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewX_ShowALL.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewX_ShowALL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX_ShowALL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX_ShowALL.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX_ShowALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX_ShowALL.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX_ShowALL.HighlightSelectedColumnHeaders = false;
            this.dataGridViewX_ShowALL.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewX_ShowALL.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewX_ShowALL.MultiSelect = false;
            this.dataGridViewX_ShowALL.Name = "dataGridViewX_ShowALL";
            this.dataGridViewX_ShowALL.PaintEnhancedSelection = false;
            this.dataGridViewX_ShowALL.ReadOnly = true;
            this.dataGridViewX_ShowALL.RowHeadersWidth = 120;
            this.dataGridViewX_ShowALL.RowTemplate.Height = 23;
            this.dataGridViewX_ShowALL.ShowCellErrors = false;
            this.dataGridViewX_ShowALL.ShowRowErrors = false;
            this.dataGridViewX_ShowALL.Size = new System.Drawing.Size(529, 425);
            this.dataGridViewX_ShowALL.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 486;
            // 
            // CheckDataShowALL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.dataGridViewX_ShowALL);
            this.Name = "CheckDataShowALL";
            this.Size = new System.Drawing.Size(529, 425);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX_ShowALL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX_ShowALL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}

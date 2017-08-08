namespace CLDC_MeterUI.DisplayInfo
{
    partial class DisplayESAMDataReturn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Data_View = new System.Windows.Forms.DataGridView();
            this.表位 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.ESAM数据内容 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.Data_View)).BeginInit();
            this.SuspendLayout();
            // 
            // Data_View
            // 
            this.Data_View.AllowUserToAddRows = false;
            this.Data_View.AllowUserToDeleteRows = false;
            this.Data_View.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Data_View.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Data_View.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_View.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Data_View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Data_View.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.表位,
            this.ESAM数据内容});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_View.DefaultCellStyle = dataGridViewCellStyle3;
            this.Data_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data_View.Location = new System.Drawing.Point(0, 0);
            this.Data_View.Margin = new System.Windows.Forms.Padding(0);
            this.Data_View.MultiSelect = false;
            this.Data_View.Name = "Data_View";
            this.Data_View.ReadOnly = true;
            this.Data_View.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.Data_View.RowTemplate.Height = 23;
            this.Data_View.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Data_View.Size = new System.Drawing.Size(433, 327);
            this.Data_View.TabIndex = 4;
            // 
            // 表位
            // 
            this.表位.HeaderText = "表位";
            this.表位.Name = "表位";
            this.表位.ReadOnly = true;
            this.表位.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.表位.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ESAM数据内容
            // 
            this.ESAM数据内容.HeaderText = "ESAM数据内容";
            this.ESAM数据内容.Name = "ESAM数据内容";
            this.ESAM数据内容.ReadOnly = true;
            this.ESAM数据内容.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ESAM数据内容.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ESAM数据内容.Width = 200;
            // 
            // DisplayESAMDataReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Data_View);
            this.Name = "DisplayESAMDataReturn";
            this.Size = new System.Drawing.Size(433, 327);
            ((System.ComponentModel.ISupportInitialize)(this.Data_View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Data_View;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 表位;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx ESAM数据内容;
    }
}

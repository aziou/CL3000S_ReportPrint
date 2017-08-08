namespace CLDC_MeterUI.DisplayInfo
{
    partial class DisplayConnProtocol
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Conn_Data = new System.Windows.Forms.DataGridView();
            this.表位 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.项目名称 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.项目数据 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Conn_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // Conn_Data
            // 
            this.Conn_Data.AllowUserToAddRows = false;
            this.Conn_Data.AllowUserToDeleteRows = false;
            this.Conn_Data.AllowUserToResizeColumns = false;
            this.Conn_Data.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Conn_Data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Conn_Data.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Conn_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Conn_Data.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Conn_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Conn_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Conn_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.表位,
            this.项目名称,
            this.项目数据});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Conn_Data.DefaultCellStyle = dataGridViewCellStyle5;
            this.Conn_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Conn_Data.Location = new System.Drawing.Point(0, 0);
            this.Conn_Data.Name = "Conn_Data";
            this.Conn_Data.ReadOnly = true;
            this.Conn_Data.RowHeadersVisible = false;
            this.Conn_Data.RowTemplate.Height = 23;
            this.Conn_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Conn_Data.Size = new System.Drawing.Size(438, 252);
            this.Conn_Data.TabIndex = 1;
            // 
            // 表位
            // 
            this.表位.HeaderText = "表位";
            this.表位.Name = "表位";
            this.表位.ReadOnly = true;
            // 
            // 项目名称
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.项目名称.DefaultCellStyle = dataGridViewCellStyle3;
            this.项目名称.FillWeight = 10F;
            this.项目名称.HeaderText = "项目名称";
            this.项目名称.Name = "项目名称";
            this.项目名称.ReadOnly = true;
            this.项目名称.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.项目名称.Width = 250;
            // 
            // 项目数据
            // 
            this.项目数据.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.项目数据.DefaultCellStyle = dataGridViewCellStyle4;
            this.项目数据.HeaderText = "项目数据";
            this.项目数据.Name = "项目数据";
            this.项目数据.ReadOnly = true;
            this.项目数据.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.FillWeight = 10F;
            this.dataGridViewTextBoxColumn1.HeaderText = "项目名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "项目结论";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DisplayConnProtocol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Conn_Data);
            this.Name = "DisplayConnProtocol";
            this.Size = new System.Drawing.Size(438, 252);
            ((System.ComponentModel.ISupportInitialize)(this.Conn_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Conn_Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 表位;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 项目名称;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 项目数据;
    }
}

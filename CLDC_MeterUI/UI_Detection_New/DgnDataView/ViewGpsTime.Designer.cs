namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    partial class ViewGpsTime
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Data_View = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.DatCol_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Data_View)).BeginInit();
            this.SuspendLayout();
            // 
            // Data_View
            // 
            this.Data_View.AllowUserToAddRows = false;
            this.Data_View.AllowUserToDeleteRows = false;
            this.Data_View.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_View.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Data_View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Data_View.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DatCol_1,
            this.DatCol_2,
            this.DatCol_3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_View.DefaultCellStyle = dataGridViewCellStyle4;
            this.Data_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data_View.Location = new System.Drawing.Point(0, 0);
            this.Data_View.Margin = new System.Windows.Forms.Padding(0);
            this.Data_View.MultiSelect = false;
            this.Data_View.Name = "Data_View";
            this.Data_View.ReadOnly = true;
            this.Data_View.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.Data_View.RowTemplate.Height = 23;
            this.Data_View.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Data_View.Size = new System.Drawing.Size(593, 390);
            this.Data_View.TabIndex = 5;
            // 
            // DatCol_1
            // 
            this.DatCol_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DatCol_1.FillWeight = 34F;
            this.DatCol_1.HeaderText = "被检表时间";
            this.DatCol_1.Name = "DatCol_1";
            this.DatCol_1.ReadOnly = true;
            this.DatCol_1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DatCol_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DatCol_2
            // 
            this.DatCol_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DatCol_2.DefaultCellStyle = dataGridViewCellStyle2;
            this.DatCol_2.FillWeight = 33F;
            this.DatCol_2.HeaderText = "GPS时间";
            this.DatCol_2.Name = "DatCol_2";
            this.DatCol_2.ReadOnly = true;
            this.DatCol_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DatCol_3
            // 
            this.DatCol_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DatCol_3.DefaultCellStyle = dataGridViewCellStyle3;
            this.DatCol_3.FillWeight = 33F;
            this.DatCol_3.HeaderText = "时间差";
            this.DatCol_3.Name = "DatCol_3";
            this.DatCol_3.ReadOnly = true;
            this.DatCol_3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ViewGpsTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Data_View);
            this.Name = "ViewGpsTime";
            this.Size = new System.Drawing.Size(593, 390);
            ((System.ComponentModel.ISupportInitialize)(this.Data_View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX Data_View;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_3;
    }
}

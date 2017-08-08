namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    partial class ViewMemoryCheck
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Data_View = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.DatCol_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.DatCol_3,
            this.DatCol_5,
            this.Column1});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_View.DefaultCellStyle = dataGridViewCellStyle6;
            this.Data_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data_View.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Data_View.Location = new System.Drawing.Point(0, 0);
            this.Data_View.Margin = new System.Windows.Forms.Padding(0);
            this.Data_View.MultiSelect = false;
            this.Data_View.Name = "Data_View";
            this.Data_View.ReadOnly = true;
            this.Data_View.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.Data_View.RowTemplate.Height = 23;
            this.Data_View.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Data_View.Size = new System.Drawing.Size(488, 378);
            this.Data_View.TabIndex = 4;
            // 
            // DatCol_1
            // 
            this.DatCol_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DatCol_1.FillWeight = 20F;
            this.DatCol_1.HeaderText = "正向无功总";
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
            this.DatCol_2.FillWeight = 20F;
            this.DatCol_2.HeaderText = "一象限无功总";
            this.DatCol_2.Name = "DatCol_2";
            this.DatCol_2.ReadOnly = true;
            this.DatCol_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DatCol_3
            // 
            this.DatCol_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DatCol_3.DefaultCellStyle = dataGridViewCellStyle3;
            this.DatCol_3.FillWeight = 20F;
            this.DatCol_3.HeaderText = "二象限无功总";
            this.DatCol_3.Name = "DatCol_3";
            this.DatCol_3.ReadOnly = true;
            this.DatCol_3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DatCol_5
            // 
            this.DatCol_5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DatCol_5.DefaultCellStyle = dataGridViewCellStyle4;
            this.DatCol_5.FillWeight = 20F;
            this.DatCol_5.HeaderText = "三象限无功总";
            this.DatCol_5.Name = "DatCol_5";
            this.DatCol_5.ReadOnly = true;
            this.DatCol_5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "四象限无功总";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ViewMemoryCheck
            // 
            this.Controls.Add(this.Data_View);
            this.Name = "ViewMemoryCheck";
            this.Size = new System.Drawing.Size(488, 378);
            ((System.ComponentModel.ISupportInitialize)(this.Data_View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX Data_View;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}

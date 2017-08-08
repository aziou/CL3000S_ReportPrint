namespace CLDC_MeterUI.PublicControls
{
    partial class UI_ClientTable
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
            this.Dgw = new DevComponents.DotNetBar.Controls.DataGridViewX();
            ((System.ComponentModel.ISupportInitialize)(this.Dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgw
            // 
            this.Dgw.AllowUserToAddRows = false;
            this.Dgw.AllowUserToDeleteRows = false;
            this.Dgw.AllowUserToResizeColumns = false;
            this.Dgw.AllowUserToResizeRows = false;
            this.Dgw.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Dgw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dgw.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.Dgw.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgw.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.Dgw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Dgw.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Dgw.Location = new System.Drawing.Point(0, 0);
            this.Dgw.Margin = new System.Windows.Forms.Padding(0);
            this.Dgw.MultiSelect = false;
            this.Dgw.Name = "Dgw";
            this.Dgw.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgw.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Dgw.RowHeadersVisible = false;
            this.Dgw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Dgw.RowTemplate.Height = 23;
            this.Dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Dgw.ShowEditingIcon = false;
            this.Dgw.Size = new System.Drawing.Size(766, 482);
            this.Dgw.TabIndex = 3;
            this.Dgw.TabStop = false;
            this.Dgw.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgw_CellMouseUp);
            this.Dgw.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgw_CellLeave);
            this.Dgw.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgw_CellMouseDown);
            this.Dgw.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Dgw_CellPainting);
            this.Dgw.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgw_CellEnter);
            // 
            // UI_ClientTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Dgw);
            this.Name = "UI_ClientTable";
            this.Size = new System.Drawing.Size(766, 482);
            ((System.ComponentModel.ISupportInitialize)(this.Dgw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX Dgw;
    }
}

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    partial class UI_ZouZi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_ZouZi));
            this.Cmb_Fa = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Dgv_Data = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Col_Data4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Item = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Col_Data1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Col_Control = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cmd_MoveDown = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_MoveUp = new DevComponents.DotNetBar.ButtonX();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new DevComponents.DotNetBar.ButtonX();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cmb_Fa
            // 
            this.Cmb_Fa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_Fa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Fa.FormattingEnabled = true;
            this.Cmb_Fa.Location = new System.Drawing.Point(113, 8);
            this.Cmb_Fa.Name = "Cmb_Fa";
            this.Cmb_Fa.Size = new System.Drawing.Size(168, 22);
            this.Cmb_Fa.TabIndex = 1;
            this.Cmb_Fa.SelectionChangeCommitted += new System.EventHandler(this.Cmb_Fa_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可选择的走字方案：";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Controls.Add(this.Dgv_Data, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 430F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(692, 430);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Dgv_Data
            // 
            this.Dgv_Data.AllowUserToAddRows = false;
            this.Dgv_Data.AllowUserToDeleteRows = false;
            this.Dgv_Data.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(250)))), ((int)(((byte)(235)))));
            this.Dgv_Data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Data.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Dgv_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Data4,
            this.Col_Item,
            this.Column1,
            this.Column2,
            this.Col_Data1,
            this.Column4,
            this.Column3,
            this.Column5,
            this.Col_Control});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Data.DefaultCellStyle = dataGridViewCellStyle7;
            this.Dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Data.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Dgv_Data.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Dgv_Data.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Data.Margin = new System.Windows.Forms.Padding(0);
            this.Dgv_Data.MultiSelect = false;
            this.Dgv_Data.Name = "Dgv_Data";
            this.Dgv_Data.RowHeadersVisible = false;
            this.Dgv_Data.RowTemplate.Height = 23;
            this.Dgv_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Data.ShowEditingIcon = false;
            this.Dgv_Data.Size = new System.Drawing.Size(662, 430);
            this.Dgv_Data.TabIndex = 0;
            this.Dgv_Data.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellLeave);
            this.Dgv_Data.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellMouseLeave);
            this.Dgv_Data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellDoubleClick);
            this.Dgv_Data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellClick);
            this.Dgv_Data.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Dgv_Data_DataError);
            // 
            // Col_Data4
            // 
            this.Col_Data4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Col_Data4.FillWeight = 10F;
            this.Col_Data4.HeaderText = "顺序";
            this.Col_Data4.Name = "Col_Data4";
            this.Col_Data4.ReadOnly = true;
            this.Col_Data4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_Item
            // 
            this.Col_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Item.DefaultCellStyle = dataGridViewCellStyle4;
            this.Col_Item.FillWeight = 10F;
            this.Col_Item.HeaderText = "功率方向";
            this.Col_Item.Name = "Col_Item";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 8F;
            this.Column1.HeaderText = "元件";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 10F;
            this.Column2.HeaderText = "功率因素";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Col_Data1
            // 
            this.Col_Data1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Col_Data1.FillWeight = 10F;
            this.Col_Data1.HeaderText = "电流倍数";
            this.Col_Data1.Name = "Col_Data1";
            this.Col_Data1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 10F;
            this.Column4.HeaderText = "走字方式";
            this.Column4.Name = "Column4";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 15F;
            this.Column3.HeaderText = "走字内容";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.FillWeight = 10F;
            this.Column5.HeaderText = "组合误差";
            this.Column5.Name = "Column5";
            // 
            // Col_Control
            // 
            this.Col_Control.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Control.DefaultCellStyle = dataGridViewCellStyle6;
            this.Col_Control.FillWeight = 10F;
            this.Col_Control.HeaderText = "操作";
            this.Col_Control.Name = "Col_Control";
            this.Col_Control.ReadOnly = true;
            this.Col_Control.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col_Control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel2.Controls.Add(this.Cmd_MoveDown);
            this.panel2.Controls.Add(this.Cmd_MoveUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(662, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 430);
            this.panel2.TabIndex = 1;
            // 
            // Cmd_MoveDown
            // 
            this.Cmd_MoveDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_MoveDown.Enabled = false;
            this.Cmd_MoveDown.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_MoveDown.Image")));
            this.Cmd_MoveDown.Location = new System.Drawing.Point(3, 56);
            this.Cmd_MoveDown.Name = "Cmd_MoveDown";
            this.Cmd_MoveDown.Size = new System.Drawing.Size(24, 37);
            this.Cmd_MoveDown.TabIndex = 1;
            this.Cmd_MoveDown.Click += new System.EventHandler(this.Cmd_MoveDown_Click);
            // 
            // Cmd_MoveUp
            // 
            this.Cmd_MoveUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_MoveUp.Enabled = false;
            this.Cmd_MoveUp.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_MoveUp.Image")));
            this.Cmd_MoveUp.Location = new System.Drawing.Point(3, 19);
            this.Cmd_MoveUp.Name = "Cmd_MoveUp";
            this.Cmd_MoveUp.Size = new System.Drawing.Size(24, 37);
            this.Cmd_MoveUp.TabIndex = 0;
            this.Cmd_MoveUp.Click += new System.EventHandler(this.Cmd_MoveUp_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(692, 465);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel1.Controls.Add(this.Cmd_Clear);
            this.panel1.Controls.Add(this.Cmb_Fa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 35);
            this.panel1.TabIndex = 0;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Clear.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Clear.Image")));
            this.Cmd_Clear.Location = new System.Drawing.Point(288, 6);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(102, 23);
            this.Cmd_Clear.TabIndex = 2;
            this.Cmd_Clear.Text = "清理所有项目";
            this.Cmd_Clear.Click += new System.EventHandler(this.Cmd_Clear_Click);
            // 
            // UI_ZouZi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UI_ZouZi";
            this.Size = new System.Drawing.Size(692, 465);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX Cmd_Clear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Fa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX Dgv_Data;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX Cmd_MoveDown;
        private DevComponents.DotNetBar.ButtonX Cmd_MoveUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Data4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Col_Item;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Col_Data1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Control;
    }
}

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    partial class UI_Carrier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Carrier));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Dgv_Data = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cmd_MoveDown = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_MoveUp = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new DevComponents.DotNetBar.ButtonX();
            this.Cmb_Fa = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.Col_Data4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Data1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Col_Data2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Col_Control = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(829, 444);
            this.tableLayoutPanel1.TabIndex = 1;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(829, 409);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Data4,
            this.Col_Data1,
            this.Col_Data2,
            this.Column1,
            this.Col_Control});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.Dgv_Data.Size = new System.Drawing.Size(799, 409);
            this.Dgv_Data.TabIndex = 0;
            this.Dgv_Data.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellValueChanged);
            this.Dgv_Data.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellLeave);
            this.Dgv_Data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellClick);
            this.Dgv_Data.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Dgv_Data_DataError);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel2.Controls.Add(this.Cmd_MoveDown);
            this.panel2.Controls.Add(this.Cmd_MoveUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(799, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 409);
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
            this.panel1.Size = new System.Drawing.Size(829, 35);
            this.panel1.TabIndex = 0;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Clear.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Clear.Image")));
            this.Cmd_Clear.Location = new System.Drawing.Point(345, 7);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(123, 23);
            this.Cmd_Clear.TabIndex = 2;
            this.Cmd_Clear.Text = "清理所有项目";
            // 
            // Cmb_Fa
            // 
            this.Cmb_Fa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_Fa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Fa.FormattingEnabled = true;
            this.Cmb_Fa.Location = new System.Drawing.Point(121, 9);
            this.Cmb_Fa.Name = "Cmb_Fa";
            this.Cmb_Fa.Size = new System.Drawing.Size(218, 22);
            this.Cmb_Fa.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可选择的载波方案：";
            // 
            // Col_Data4
            // 
            this.Col_Data4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Col_Data4.FillWeight = 18.97194F;
            this.Col_Data4.HeaderText = "顺序";
            this.Col_Data4.Name = "Col_Data4";
            this.Col_Data4.ReadOnly = true;
            this.Col_Data4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_Data1
            // 
            this.Col_Data1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Col_Data1.FillWeight = 24.96044F;
            this.Col_Data1.HeaderText = "项目名称";
            this.Col_Data1.Name = "Col_Data1";
            this.Col_Data1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Col_Data2
            // 
            this.Col_Data2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Col_Data2.FillWeight = 31.27858F;
            this.Col_Data2.HeaderText = "标识符";
            this.Col_Data2.Name = "Col_Data2";
            this.Col_Data2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Col_Data2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 44.17991F;
            this.Column1.HeaderText = "载波模块互换";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Col_Control
            // 
            this.Col_Control.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Control.DefaultCellStyle = dataGridViewCellStyle6;
            this.Col_Control.FillWeight = 40.60914F;
            this.Col_Control.HeaderText = "操作";
            this.Col_Control.Name = "Col_Control";
            this.Col_Control.ReadOnly = true;
            this.Col_Control.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col_Control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UI_Carrier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UI_Carrier";
            this.Size = new System.Drawing.Size(829, 444);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX Dgv_Data;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX Cmd_MoveDown;
        private DevComponents.DotNetBar.ButtonX Cmd_MoveUp;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX Cmd_Clear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Fa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Data4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Col_Data1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Data2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Control;
    }
}

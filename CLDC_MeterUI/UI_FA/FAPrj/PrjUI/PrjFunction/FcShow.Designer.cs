namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjFunction
{
    partial class FcShow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FcShow));
            this.Panel_Back = new CLDC_Comm.ExtendedPanel.ExtendedPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Dgv_Data = new System.Windows.Forms.DataGridView();
            this.Col_Data4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Item = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Data1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Control = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cmd_MoveDown = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_MoveUp = new DevComponents.DotNetBar.ButtonX();
            this.Panel_Back.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.Color.Silver;
            this.Panel_Back.CaptionColorTwo = System.Drawing.Color.SeaShell;
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.CaptionSize = 25;
            this.Panel_Back.CaptionTextColor = System.Drawing.Color.Black;
            this.Panel_Back.Controls.Add(this.tableLayoutPanel1);
            this.Panel_Back.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.Panel_Back.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.Panel_Back.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.Panel_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Back.Location = new System.Drawing.Point(0, 0);
            this.Panel_Back.Name = "Panel_Back";
            this.Panel_Back.ShowCheckBox = true;
            this.Panel_Back.Size = new System.Drawing.Size(891, 258);
            this.Panel_Back.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(891, 231);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Controls.Add(this.Dgv_Data, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 430F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(891, 231);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Dgv_Data
            // 
            this.Dgv_Data.AllowUserToAddRows = false;
            this.Dgv_Data.AllowUserToDeleteRows = false;
            this.Dgv_Data.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(250)))), ((int)(((byte)(235)))));
            this.Dgv_Data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Dgv_Data.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Dgv_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Data4,
            this.Col_Item,
            this.Column1,
            this.Column2,
            this.Col_Data1,
            this.Column4,
            this.Col_Control});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Data.DefaultCellStyle = dataGridViewCellStyle12;
            this.Dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Data.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Dgv_Data.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Data.Margin = new System.Windows.Forms.Padding(0);
            this.Dgv_Data.MultiSelect = false;
            this.Dgv_Data.Name = "Dgv_Data";
            this.Dgv_Data.RowHeadersVisible = false;
            this.Dgv_Data.RowTemplate.Height = 23;
            this.Dgv_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Data.ShowEditingIcon = false;
            this.Dgv_Data.Size = new System.Drawing.Size(861, 231);
            this.Dgv_Data.TabIndex = 0;
            this.Dgv_Data.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellValueChanged);
            this.Dgv_Data.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellLeave);
            this.Dgv_Data.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellMouseLeave);
            this.Dgv_Data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellDoubleClick);
            this.Dgv_Data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellClick);
            this.Dgv_Data.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Dgv_Data_DataError);
            // 
            // Col_Data4
            // 
            this.Col_Data4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data4.DefaultCellStyle = dataGridViewCellStyle9;
            this.Col_Data4.FillWeight = 8F;
            this.Col_Data4.HeaderText = "顺序";
            this.Col_Data4.Name = "Col_Data4";
            this.Col_Data4.ReadOnly = true;
            this.Col_Data4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_Item
            // 
            this.Col_Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Item.DefaultCellStyle = dataGridViewCellStyle10;
            this.Col_Item.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Col_Item.FillWeight = 25F;
            this.Col_Item.HeaderText = "数据项名称";
            this.Col_Item.Name = "Col_Item";
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 14F;
            this.Column1.HeaderText = "标识编码";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 6F;
            this.Column2.HeaderText = "长度";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Col_Data1
            // 
            this.Col_Data1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Col_Data1.DefaultCellStyle = dataGridViewCellStyle11;
            this.Col_Data1.FillWeight = 8F;
            this.Col_Data1.HeaderText = "小数位";
            this.Col_Data1.Name = "Col_Data1";
            this.Col_Data1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Col_Data1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 10F;
            this.Column4.HeaderText = "数据格式";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_Control
            // 
            this.Col_Control.HeaderText = "操作";
            this.Col_Control.Name = "Col_Control";
            this.Col_Control.ReadOnly = true;
            this.Col_Control.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Col_Control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_Control.Width = 80;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Cmd_MoveDown);
            this.panel2.Controls.Add(this.Cmd_MoveUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(861, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 231);
            this.panel2.TabIndex = 1;
            // 
            // Cmd_MoveDown
            // 
            this.Cmd_MoveDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_MoveDown.Enabled = false;
            this.Cmd_MoveDown.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_MoveDown.Image")));
            this.Cmd_MoveDown.Location = new System.Drawing.Point(3, 57);
            this.Cmd_MoveDown.Name = "Cmd_MoveDown";
            this.Cmd_MoveDown.Size = new System.Drawing.Size(24, 37);
            this.Cmd_MoveDown.TabIndex = 3;
            this.Cmd_MoveDown.Click += new System.EventHandler(this.Cmd_MoveDown_Click);
            // 
            // Cmd_MoveUp
            // 
            this.Cmd_MoveUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_MoveUp.Enabled = false;
            this.Cmd_MoveUp.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_MoveUp.Image")));
            this.Cmd_MoveUp.Location = new System.Drawing.Point(3, 20);
            this.Cmd_MoveUp.Name = "Cmd_MoveUp";
            this.Cmd_MoveUp.Size = new System.Drawing.Size(24, 37);
            this.Cmd_MoveUp.TabIndex = 2;
            this.Cmd_MoveUp.Click += new System.EventHandler(this.Cmd_MoveUp_Click);
            // 
            // FcShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Back);
            this.Name = "FcShow";
            this.Size = new System.Drawing.Size(891, 258);
            this.Panel_Back.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView Dgv_Data;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX Cmd_MoveDown;
        private DevComponents.DotNetBar.ButtonX Cmd_MoveUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Data4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Col_Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Data1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Control;
    }
}

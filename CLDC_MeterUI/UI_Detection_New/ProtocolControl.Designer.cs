namespace CLDC_MeterUI.UI_Detection_New
{
    partial class ProtocolControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TV_Protocol = new System.Windows.Forms.TreeView();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.cmb_text = new System.Windows.Forms.ComboBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Dgv_Data = new CLDC_MeterUI.CLZDataGridView.DataGrid();
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.规约名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.编码类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.项名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数据标识 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.权限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.长度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.小数位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作方式 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.格式串 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.默认值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // TV_Protocol
            // 
            this.TV_Protocol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Protocol.Location = new System.Drawing.Point(3, 3);
            this.TV_Protocol.Name = "TV_Protocol";
            this.tableLayoutPanel1.SetRowSpan(this.TV_Protocol, 3);
            this.TV_Protocol.Size = new System.Drawing.Size(49, 458);
            this.TV_Protocol.TabIndex = 0;
            this.TV_Protocol.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_Protocol_AfterSelect);
            // 
            // btn_Add
            // 
            this.btn_Add.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Add.Enabled = false;
            this.btn_Add.Location = new System.Drawing.Point(75, 399);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(80, 54);
            this.btn_Add.TabIndex = 3;
            this.btn_Add.Text = "新增一行";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_delete.Enabled = false;
            this.btn_delete.Location = new System.Drawing.Point(163, 399);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(78, 54);
            this.btn_delete.TabIndex = 4;
            this.btn_delete.Text = "删除数据";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // cmb_text
            // 
            this.cmb_text.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_text.FormattingEnabled = true;
            this.cmb_text.Location = new System.Drawing.Point(28, 196);
            this.cmb_text.Name = "cmb_text";
            this.cmb_text.Size = new System.Drawing.Size(96, 20);
            this.cmb_text.TabIndex = 2;
            this.cmb_text.Visible = false;
            this.cmb_text.SelectedIndexChanged += new System.EventHandler(this.cmb_text_SelectedIndexChanged);
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Save.Enabled = false;
            this.btn_Save.Location = new System.Drawing.Point(247, 399);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 54);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "保存数据";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.82587F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.17413F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 424F));
            this.tableLayoutPanel1.Controls.Add(this.TV_Protocol, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Save, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_delete, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Add, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 464);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.cmb_text);
            this.panel1.Controls.Add(this.Dgv_Data);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(58, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 390);
            this.panel1.TabIndex = 7;
            // 
            // Dgv_Data
            // 
            this.Dgv_Data.AllowUserToAddRows = false;
            this.Dgv_Data.AllowUserToDeleteRows = false;
            this.Dgv_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_Data.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编号,
            this.规约名称,
            this.编码类型,
            this.项名称,
            this.数据标识,
            this.权限,
            this.长度,
            this.小数位,
            this.操作方式,
            this.格式串,
            this.默认值});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Data.DefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_Data.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Dgv_Data.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Data.Name = "Dgv_Data";
            this.Dgv_Data.RowHeadersVisible = false;
            this.Dgv_Data.RowTemplate.Height = 20;
            this.Dgv_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Data.ShowCellErrors = false;
            this.Dgv_Data.ShowRowErrors = false;
            this.Dgv_Data.Size = new System.Drawing.Size(608, 390);
            this.Dgv_Data.TabIndex = 1;
            this.Dgv_Data.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Dgv_Data_Scroll);
            this.Dgv_Data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Data_CellClick);
            // 
            // 编号
            // 
            this.编号.DataPropertyName = "PK_LNG_DLT_ID ";
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.Visible = false;
            // 
            // 规约名称
            // 
            this.规约名称.HeaderText = "规约名称";
            this.规约名称.Name = "规约名称";
            this.规约名称.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 编码类型
            // 
            this.编码类型.DataPropertyName = " ";
            this.编码类型.HeaderText = "编码类型";
            this.编码类型.Name = "编码类型";
            this.编码类型.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 项名称
            // 
            this.项名称.DataPropertyName = "AVR_ITEM_NAME ";
            this.项名称.HeaderText = "项名称";
            this.项名称.Name = "项名称";
            // 
            // 数据标识
            // 
            this.数据标识.DataPropertyName = "AVR_ID ";
            this.数据标识.HeaderText = "数据标识";
            this.数据标识.Name = "数据标识";
            // 
            // 权限
            // 
            this.权限.HeaderText = "权限";
            this.权限.Name = "权限";
            this.权限.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 长度
            // 
            this.长度.DataPropertyName = "LNG_LENGTH";
            this.长度.HeaderText = "长度";
            this.长度.Name = "长度";
            // 
            // 小数位
            // 
            this.小数位.DataPropertyName = "LNG_DOT";
            this.小数位.HeaderText = "小数位";
            this.小数位.Name = "小数位";
            // 
            // 操作方式
            // 
            this.操作方式.DataPropertyName = "CHR_TYPE";
            this.操作方式.HeaderText = "操作方式";
            this.操作方式.Name = "操作方式";
            this.操作方式.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 格式串
            // 
            this.格式串.DataPropertyName = "AVR_FORMAT";
            this.格式串.HeaderText = "格式串";
            this.格式串.Name = "格式串";
            // 
            // 默认值
            // 
            this.默认值.DataPropertyName = "AVR_DEF_VALUE ";
            this.默认值.HeaderText = "默认值";
            this.默认值.Name = "默认值";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PK_LNG_DLT_ID";
            this.dataGridViewTextBoxColumn1.FillWeight = 14F;
            this.dataGridViewTextBoxColumn1.HeaderText = "标识编码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "AVR_ITEM_NAME";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn2.FillWeight = 6F;
            this.dataGridViewTextBoxColumn2.HeaderText = "长度";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "AVR_ID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.FillWeight = 8F;
            this.dataGridViewTextBoxColumn3.HeaderText = "小数位";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "LNG_LENGTH";
            this.dataGridViewTextBoxColumn4.FillWeight = 10F;
            this.dataGridViewTextBoxColumn4.HeaderText = "数据格式";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "LNG_DOT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn5.FillWeight = 10F;
            this.dataGridViewTextBoxColumn5.HeaderText = "写入内容";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "AVR_FORMAT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.FillWeight = 10F;
            this.dataGridViewTextBoxColumn6.HeaderText = "操作";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "AVR_DEF_VALUE";
            this.dataGridViewTextBoxColumn7.FillWeight = 10F;
            this.dataGridViewTextBoxColumn7.HeaderText = "写入内容";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn8.FillWeight = 10F;
            this.dataGridViewTextBoxColumn8.HeaderText = "操作";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProtocolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProtocolControl";
            this.Size = new System.Drawing.Size(669, 464);
            this.Load += new System.EventHandler(this.ProtocolControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TV_Protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.ComboBox cmb_text;
        private CLDC_MeterUI.CLZDataGridView.DataGrid Dgv_Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn 编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 规约名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 编码类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 项名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数据标识;
        private System.Windows.Forms.DataGridViewTextBoxColumn 权限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 长度;
        private System.Windows.Forms.DataGridViewTextBoxColumn 小数位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 操作方式;
        private System.Windows.Forms.DataGridViewTextBoxColumn 格式串;
        private System.Windows.Forms.DataGridViewTextBoxColumn 默认值;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;

    }
}

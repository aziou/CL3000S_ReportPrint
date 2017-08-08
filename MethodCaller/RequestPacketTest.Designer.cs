namespace MethodCaller
{
    partial class RequestPacketTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSerach = new System.Windows.Forms.Button();
            this.tbSerach = new System.Windows.Forms.TextBox();
            this.btnCall = new System.Windows.Forms.Button();
            this.cbbClass = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvTypeInfo = new System.Windows.Forms.DataGridView();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtbData = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypeInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSerach);
            this.splitContainer1.Panel1.Controls.Add(this.tbSerach);
            this.splitContainer1.Panel1.Controls.Add(this.btnCall);
            this.splitContainer1.Panel1.Controls.Add(this.cbbClass);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(452, 408);
            this.splitContainer1.SplitterDistance = 65;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnSerach
            // 
            this.btnSerach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSerach.Location = new System.Drawing.Point(373, 7);
            this.btnSerach.Name = "btnSerach";
            this.btnSerach.Size = new System.Drawing.Size(75, 23);
            this.btnSerach.TabIndex = 4;
            this.btnSerach.Text = "查找";
            this.btnSerach.UseVisualStyleBackColor = true;
            this.btnSerach.Click += new System.EventHandler(this.btnSerach_Click);
            // 
            // tbSerach
            // 
            this.tbSerach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSerach.Location = new System.Drawing.Point(3, 7);
            this.tbSerach.Name = "tbSerach";
            this.tbSerach.Size = new System.Drawing.Size(363, 21);
            this.tbSerach.TabIndex = 3;
            this.tbSerach.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSerach_KeyUp);
            // 
            // btnCall
            // 
            this.btnCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCall.Location = new System.Drawing.Point(372, 32);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(75, 23);
            this.btnCall.TabIndex = 2;
            this.btnCall.Text = "调用";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // cbbClass
            // 
            this.cbbClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbClass.FormattingEnabled = true;
            this.cbbClass.Location = new System.Drawing.Point(3, 34);
            this.cbbClass.Name = "cbbClass";
            this.cbbClass.Size = new System.Drawing.Size(363, 20);
            this.cbbClass.TabIndex = 1;
            this.cbbClass.SelectedIndexChanged += new System.EventHandler(this.cbbClass_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvTypeInfo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rtbData);
            this.splitContainer2.Size = new System.Drawing.Size(452, 339);
            this.splitContainer2.SplitterDistance = 204;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvTypeInfo
            // 
            this.dgvTypeInfo.AllowUserToAddRows = false;
            this.dgvTypeInfo.AllowUserToDeleteRows = false;
            this.dgvTypeInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTypeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypeInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitle,
            this.colType,
            this.colValue});
            this.dgvTypeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTypeInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvTypeInfo.Name = "dgvTypeInfo";
            this.dgvTypeInfo.RowTemplate.Height = 23;
            this.dgvTypeInfo.Size = new System.Drawing.Size(452, 204);
            this.dgvTypeInfo.TabIndex = 10;
            // 
            // colTitle
            // 
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTitle.HeaderText = "项目";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            this.colTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTitle.Width = 35;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colType.HeaderText = "类型";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colType.Width = 35;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "值";
            this.colValue.Name = "colValue";
            this.colValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rtbData
            // 
            this.rtbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbData.Location = new System.Drawing.Point(0, 0);
            this.rtbData.Name = "rtbData";
            this.rtbData.Size = new System.Drawing.Size(452, 131);
            this.rtbData.TabIndex = 0;
            this.rtbData.Text = "";
            // 
            // RequestPacketTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 408);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RequestPacketTest";
            this.Text = "请求数据包测试工具";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypeInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox rtbData;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.ComboBox cbbClass;
        private System.Windows.Forms.Button btnSerach;
        private System.Windows.Forms.TextBox tbSerach;
        protected System.Windows.Forms.DataGridView dgvTypeInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
}
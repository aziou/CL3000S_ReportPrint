namespace MethodCaller.DataAnalysis
{
    partial class DataAnalysiser
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnOpen1 = new System.Windows.Forms.Button();
            this.cbbSerialPort1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnOpen2 = new System.Windows.Forms.Button();
            this.cbbSerialPort2 = new System.Windows.Forms.ComboBox();
            this.串吕 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(891, 435);
            this.splitContainer1.SplitterDistance = 461;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnOpen1);
            this.splitContainer2.Panel1.Controls.Add(this.cbbSerialPort1);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer2.Size = new System.Drawing.Size(461, 435);
            this.splitContainer2.SplitterDistance = 43;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnOpen1
            // 
            this.btnOpen1.Location = new System.Drawing.Point(186, 10);
            this.btnOpen1.Name = "btnOpen1";
            this.btnOpen1.Size = new System.Drawing.Size(75, 23);
            this.btnOpen1.TabIndex = 5;
            this.btnOpen1.Text = "打开";
            this.btnOpen1.UseVisualStyleBackColor = true;
            this.btnOpen1.Click += new System.EventHandler(this.btnOpen1_Click);
            // 
            // cbbSerialPort1
            // 
            this.cbbSerialPort1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSerialPort1.FormattingEnabled = true;
            this.cbbSerialPort1.Location = new System.Drawing.Point(50, 12);
            this.cbbSerialPort1.Name = "cbbSerialPort1";
            this.cbbSerialPort1.Size = new System.Drawing.Size(121, 20);
            this.cbbSerialPort1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "串口:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(461, 388);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btnOpen2);
            this.splitContainer3.Panel1.Controls.Add(this.cbbSerialPort2);
            this.splitContainer3.Panel1.Controls.Add(this.串吕);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainer3.Size = new System.Drawing.Size(426, 435);
            this.splitContainer3.SplitterDistance = 43;
            this.splitContainer3.TabIndex = 0;
            // 
            // btnOpen2
            // 
            this.btnOpen2.Location = new System.Drawing.Point(199, 6);
            this.btnOpen2.Name = "btnOpen2";
            this.btnOpen2.Size = new System.Drawing.Size(75, 23);
            this.btnOpen2.TabIndex = 5;
            this.btnOpen2.Text = "打开";
            this.btnOpen2.UseVisualStyleBackColor = true;
            this.btnOpen2.Click += new System.EventHandler(this.btnOpen2_Click);
            // 
            // cbbSerialPort2
            // 
            this.cbbSerialPort2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSerialPort2.FormattingEnabled = true;
            this.cbbSerialPort2.Location = new System.Drawing.Point(59, 8);
            this.cbbSerialPort2.Name = "cbbSerialPort2";
            this.cbbSerialPort2.Size = new System.Drawing.Size(121, 20);
            this.cbbSerialPort2.TabIndex = 4;
            // 
            // 串吕
            // 
            this.串吕.AutoSize = true;
            this.串吕.Location = new System.Drawing.Point(12, 15);
            this.串吕.Name = "串吕";
            this.串吕.Size = new System.Drawing.Size(35, 12);
            this.串吕.TabIndex = 3;
            this.串吕.Text = "串口:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(426, 388);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // DataAnalysiser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 435);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataAnalysiser";
            this.Text = "DataAnalysiser";
            this.Load += new System.EventHandler(this.DataAnalysiser_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataAnalysiser_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnOpen1;
        private System.Windows.Forms.ComboBox cbbSerialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnOpen2;
        private System.Windows.Forms.ComboBox cbbSerialPort2;
        private System.Windows.Forms.Label 串吕;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}
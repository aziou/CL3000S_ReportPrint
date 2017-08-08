using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 输入框
    /// </summary>
    public class InputBox:Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private Label Lab_Text;
        /// <summary>
        /// 
        /// </summary>
        public TextBox Txt_Value;
        private Button Btn_Ok;
        private Button Btn_Cancel;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static bool Show(string Text,string Title,ref string DefaultValue)
        {
            InputBox Dlg = new InputBox(Text, Title, DefaultValue);
            Dlg.TopMost = true;
            if (Dlg.ShowDialog() != DialogResult.OK)
            {
                DefaultValue = string.Empty;
                Dlg.Dispose();
                return false;
            }
            DefaultValue = Dlg.Txt_Value.Text;
            Dlg.Dispose();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public InputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        public InputBox(string Text, string Title, string DefaultValue):this()
        {
            this.Text = Title;
            this.Lab_Text.Text = Text;
            this.Txt_Value.Text = DefaultValue;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Txt_Value = new System.Windows.Forms.TextBox();
            this.Lab_Text = new System.Windows.Forms.Label();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Txt_Value, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Lab_Text, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Ok, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Cancel, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 97);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Txt_Value
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Txt_Value, 2);
            this.Txt_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_Value.Font = new System.Drawing.Font("宋体", 12F);
            this.Txt_Value.Location = new System.Drawing.Point(3, 28);
            this.Txt_Value.MaxLength = 50;
            this.Txt_Value.Name = "Txt_Value";
            this.Txt_Value.Size = new System.Drawing.Size(252, 30);
            this.Txt_Value.TabIndex = 1;
            this.Txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Value_KeyDown);
            // 
            // Lab_Text
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Lab_Text, 2);
            this.Lab_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lab_Text.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Text.ForeColor = System.Drawing.Color.Maroon;
            this.Lab_Text.Location = new System.Drawing.Point(3, 3);
            this.Lab_Text.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Lab_Text.Name = "Lab_Text";
            this.Lab_Text.Size = new System.Drawing.Size(252, 22);
            this.Lab_Text.TabIndex = 0;
            this.Lab_Text.Text = "提示语";
            this.Lab_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Ok.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Ok.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Ok.Image")));
            this.Btn_Ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Ok.Location = new System.Drawing.Point(28, 58);
            this.Btn_Ok.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(91, 36);
            this.Btn_Ok.TabIndex = 2;
            this.Btn_Ok.Text = "确定";
            this.Btn_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Cancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Cancel.Image")));
            this.Btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Cancel.Location = new System.Drawing.Point(129, 58);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(95, 36);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // InputBox
            // 
            this.ClientSize = new System.Drawing.Size(258, 97);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InputBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "标题";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Txt_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                Btn_Ok_Click(Btn_Ok, e);
            }
            else if ((int)e.KeyCode == 27)
            {
                Btn_Cancel_Click(sender, e);
            }
        }


    }
}

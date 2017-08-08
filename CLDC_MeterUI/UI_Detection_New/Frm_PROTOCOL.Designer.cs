namespace CLDC_MeterUI.UI_Detection_New
{
    partial class Frm_PROTOCOL
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
            this.TV_Protocol = new CLDC_MeterUI.UI_Detection_New.ProtocolControl();
            this.SuspendLayout();
            // 
            // TV_Protocol
            // 
            this.TV_Protocol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.TV_Protocol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Protocol.Location = new System.Drawing.Point(0, 0);
            this.TV_Protocol.Name = "TV_Protocol";
            this.TV_Protocol.Size = new System.Drawing.Size(1098, 467);
            this.TV_Protocol.TabIndex = 0;
            // 
            // Frm_PROTOCOL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1098, 467);
            this.Controls.Add(this.TV_Protocol);
            this.Name = "Frm_PROTOCOL";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "协议字典";
            this.ResumeLayout(false);

        }

        #endregion

        private ProtocolControl TV_Protocol;



    }
}
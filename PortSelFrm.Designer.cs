
namespace PipeLib
{
    partial class PortSelFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_sPortSelUc = new PipeLib.PortSelUc();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_sPortSelUc
            // 
            this.m_sPortSelUc.Location = new System.Drawing.Point(29, 31);
            this.m_sPortSelUc.Name = "m_sPortSelUc";
            this.m_sPortSelUc.Size = new System.Drawing.Size(237, 248);
            this.m_sPortSelUc.TabIndex = 4;
            this.m_sPortSelUc.Load += new System.EventHandler(this.portSelUc1_Load);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(182, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 39);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(78, 311);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 39);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // PortSelFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 388);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_sPortSelUc);
            this.Name = "PortSelFrm";
            this.Text = "タイプ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion
        private PortSelUc m_sPortSelUc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}



namespace CmniLib
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.portEditUc1 = new CmniLib.CmniEditUc();
            this.m_sRtbRcv = new System.Windows.Forms.RichTextBox();
            this.m_sRtbSend = new System.Windows.Forms.RichTextBox();
            this.m_sDgvPrsList = new System.Windows.Forms.DataGridView();
            this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portSelUc1 = new CmniLib.PortSelUc();
            this.button1 = new System.Windows.Forms.Button();
            this.portEditUc2 = new CmniLib.CmniEditUc();
            this.button2 = new System.Windows.Forms.Button();
            this.cmniCtrlUc1 = new CmniLib.CmniCtrlUc(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_sDgvPrsList)).BeginInit();
            this.SuspendLayout();
            // 
            // portEditUc1
            // 
            this.portEditUc1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.portEditUc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portEditUc1.Location = new System.Drawing.Point(0, 423);
            this.portEditUc1.MaximumSize = new System.Drawing.Size(268, 26);
            this.portEditUc1.MinimumSize = new System.Drawing.Size(168, 26);
            this.portEditUc1.Name = "portEditUc1";
            this.portEditUc1.Size = new System.Drawing.Size(201, 26);
            this.portEditUc1.TabIndex = 6;
            this.portEditUc1.Text = "通常";
            this.portEditUc1.Load += new System.EventHandler(this.portEditUc1_Load);
            // 
            // m_sRtbRcv
            // 
            this.m_sRtbRcv.Location = new System.Drawing.Point(662, 12);
            this.m_sRtbRcv.Name = "m_sRtbRcv";
            this.m_sRtbRcv.Size = new System.Drawing.Size(129, 426);
            this.m_sRtbRcv.TabIndex = 8;
            this.m_sRtbRcv.Text = "";
            // 
            // m_sRtbSend
            // 
            this.m_sRtbSend.Location = new System.Drawing.Point(527, 12);
            this.m_sRtbSend.Name = "m_sRtbSend";
            this.m_sRtbSend.Size = new System.Drawing.Size(129, 426);
            this.m_sRtbSend.TabIndex = 7;
            this.m_sRtbSend.Text = "";
            // 
            // m_sDgvPrsList
            // 
            this.m_sDgvPrsList.AllowUserToAddRows = false;
            this.m_sDgvPrsList.AllowUserToDeleteRows = false;
            this.m_sDgvPrsList.AllowUserToResizeColumns = false;
            this.m_sDgvPrsList.AllowUserToResizeRows = false;
            this.m_sDgvPrsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_sDgvPrsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Name,
            this._Id});
            this.m_sDgvPrsList.Location = new System.Drawing.Point(12, 12);
            this.m_sDgvPrsList.Name = "m_sDgvPrsList";
            this.m_sDgvPrsList.RowHeadersVisible = false;
            this.m_sDgvPrsList.RowTemplate.Height = 25;
            this.m_sDgvPrsList.Size = new System.Drawing.Size(321, 122);
            this.m_sDgvPrsList.TabIndex = 9;
            // 
            // _Name
            // 
            this._Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._Name.HeaderText = "アプリ";
            this._Name.Name = "_Name";
            // 
            // _Id
            // 
            this._Id.HeaderText = "ID";
            this._Id.Name = "_Id";
            // 
            // portSelUc1
            // 
            this.portSelUc1.Location = new System.Drawing.Point(261, 277);
            this.portSelUc1.Name = "portSelUc1";
            this.portSelUc1.Size = new System.Drawing.Size(8, 8);
            this.portSelUc1.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 41);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // portEditUc2
            // 
            this.portEditUc2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.portEditUc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portEditUc2.Location = new System.Drawing.Point(207, 423);
            this.portEditUc2.MaximumSize = new System.Drawing.Size(268, 26);
            this.portEditUc2.MinimumSize = new System.Drawing.Size(168, 26);
            this.portEditUc2.Name = "portEditUc2";
            this.portEditUc2.Size = new System.Drawing.Size(201, 26);
            this.portEditUc2.TabIndex = 12;
            this.portEditUc2.Text = "通常2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(230, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 41);
            this.button2.TabIndex = 13;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmniCtrlUc1
            // 
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.portEditUc2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.portSelUc1);
            this.Controls.Add(this.m_sDgvPrsList);
            this.Controls.Add(this.m_sRtbRcv);
            this.Controls.Add(this.m_sRtbSend);
            this.Controls.Add(this.portEditUc1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_sDgvPrsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CmniEditUc portEditUc1;
        private System.Windows.Forms.RichTextBox m_sRtbRcv;
        private System.Windows.Forms.RichTextBox m_sRtbSend;
        private System.Windows.Forms.DataGridView m_sDgvPrsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Id;
        private PortSelUc portSelUc1;
        private System.Windows.Forms.Button button1;
        private CmniEditUc portEditUc2;
        private System.Windows.Forms.Button button2;
        private CmniCtrlUc cmniCtrlUc1;
    }
}
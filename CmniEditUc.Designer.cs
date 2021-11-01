
namespace CmniLib
{
    partial class CmniEditUc
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.m_sLblName = new System.Windows.Forms.Label();
            this.m_sBtnPort = new System.Windows.Forms.Button();
            this.m_sChkCmni = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_sLblName
            // 
            this.m_sLblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sLblName.AutoSize = true;
            this.m_sLblName.Location = new System.Drawing.Point(0, 5);
            this.m_sLblName.Name = "m_sLblName";
            this.m_sLblName.Size = new System.Drawing.Size(31, 15);
            this.m_sLblName.TabIndex = 0;
            this.m_sLblName.Text = "通常";
            // 
            // m_sBtnPort
            // 
            this.m_sBtnPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sBtnPort.Location = new System.Drawing.Point(99, 1);
            this.m_sBtnPort.Name = "m_sBtnPort";
            this.m_sBtnPort.Size = new System.Drawing.Size(119, 22);
            this.m_sBtnPort.TabIndex = 1;
            this.m_sBtnPort.Text = "button1";
            this.m_sBtnPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_sBtnPort.UseVisualStyleBackColor = true;
            this.m_sBtnPort.Click += new System.EventHandler(this.m_sBtnPort_Click);
            // 
            // m_sChkCmni
            // 
            this.m_sChkCmni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sChkCmni.AutoSize = true;
            this.m_sChkCmni.Location = new System.Drawing.Point(84, 6);
            this.m_sChkCmni.Name = "m_sChkCmni";
            this.m_sChkCmni.Size = new System.Drawing.Size(15, 14);
            this.m_sChkCmni.TabIndex = 2;
            this.m_sChkCmni.UseVisualStyleBackColor = true;
            this.m_sChkCmni.CheckedChanged += new System.EventHandler(this.m_sChkCmni_CheckedChanged);
            // 
            // CmniEditUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.m_sChkCmni);
            this.Controls.Add(this.m_sBtnPort);
            this.Controls.Add(this.m_sLblName);
            this.MaximumSize = new System.Drawing.Size(320, 26);
            this.MinimumSize = new System.Drawing.Size(168, 26);
            this.Name = "CmniEditUc";
            this.Size = new System.Drawing.Size(220, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_sLblName;
        private System.Windows.Forms.Button m_sBtnPort;
        private System.Windows.Forms.CheckBox m_sChkCmni;
    }
}

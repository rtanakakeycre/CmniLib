﻿
namespace CmniLib
{
    partial class PortSelUc
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_sCmbPortType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_sCmbPort = new System.Windows.Forms.ComboBox();
            this.m_sCmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_sCmbDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_sCmbStopBits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_sCmbParity = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_sCmbHandShake = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "タイプ";
            // 
            // m_sCmbPortType
            // 
            this.m_sCmbPortType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbPortType.FormattingEnabled = true;
            this.m_sCmbPortType.Location = new System.Drawing.Point(81, 6);
            this.m_sCmbPortType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbPortType.Name = "m_sCmbPortType";
            this.m_sCmbPortType.Size = new System.Drawing.Size(90, 20);
            this.m_sCmbPortType.TabIndex = 6;
            this.m_sCmbPortType.SelectedIndexChanged += new System.EventHandler(this.m_sCmbPortType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "ポート";
            // 
            // m_sCmbPort
            // 
            this.m_sCmbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbPort.FormattingEnabled = true;
            this.m_sCmbPort.Location = new System.Drawing.Point(81, 29);
            this.m_sCmbPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbPort.Name = "m_sCmbPort";
            this.m_sCmbPort.Size = new System.Drawing.Size(90, 20);
            this.m_sCmbPort.TabIndex = 8;
            this.m_sCmbPort.DropDown += new System.EventHandler(this.m_sCmbPort_DropDown);
            this.m_sCmbPort.SelectedIndexChanged += new System.EventHandler(this.m_sCmbPort_SelectedIndexChanged);
            // 
            // m_sCmbBaudRate
            // 
            this.m_sCmbBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbBaudRate.FormattingEnabled = true;
            this.m_sCmbBaudRate.Location = new System.Drawing.Point(81, 52);
            this.m_sCmbBaudRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbBaudRate.Name = "m_sCmbBaudRate";
            this.m_sCmbBaudRate.Size = new System.Drawing.Size(90, 20);
            this.m_sCmbBaudRate.TabIndex = 10;
            this.m_sCmbBaudRate.SelectedIndexChanged += new System.EventHandler(this.m_sCmbBaudRate_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "ボーレート";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "データ長";
            // 
            // m_sCmbDataBits
            // 
            this.m_sCmbDataBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbDataBits.FormattingEnabled = true;
            this.m_sCmbDataBits.Location = new System.Drawing.Point(81, 76);
            this.m_sCmbDataBits.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbDataBits.Name = "m_sCmbDataBits";
            this.m_sCmbDataBits.Size = new System.Drawing.Size(90, 20);
            this.m_sCmbDataBits.TabIndex = 12;
            this.m_sCmbDataBits.SelectedIndexChanged += new System.EventHandler(this.m_sCmbDataBits_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "ストップビット";
            // 
            // m_sCmbStopBits
            // 
            this.m_sCmbStopBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbStopBits.FormattingEnabled = true;
            this.m_sCmbStopBits.Location = new System.Drawing.Point(82, 98);
            this.m_sCmbStopBits.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbStopBits.Name = "m_sCmbStopBits";
            this.m_sCmbStopBits.Size = new System.Drawing.Size(89, 20);
            this.m_sCmbStopBits.TabIndex = 14;
            this.m_sCmbStopBits.SelectedIndexChanged += new System.EventHandler(this.m_sCmbStopBits_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "パリティ";
            // 
            // m_sCmbParity
            // 
            this.m_sCmbParity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbParity.FormattingEnabled = true;
            this.m_sCmbParity.Location = new System.Drawing.Point(82, 122);
            this.m_sCmbParity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbParity.Name = "m_sCmbParity";
            this.m_sCmbParity.Size = new System.Drawing.Size(89, 20);
            this.m_sCmbParity.TabIndex = 16;
            this.m_sCmbParity.SelectedIndexChanged += new System.EventHandler(this.m_sCmbParity_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "ハンドシェイク";
            // 
            // m_sCmbHandShake
            // 
            this.m_sCmbHandShake.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_sCmbHandShake.FormattingEnabled = true;
            this.m_sCmbHandShake.Location = new System.Drawing.Point(82, 146);
            this.m_sCmbHandShake.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_sCmbHandShake.Name = "m_sCmbHandShake";
            this.m_sCmbHandShake.Size = new System.Drawing.Size(89, 20);
            this.m_sCmbHandShake.TabIndex = 18;
            this.m_sCmbHandShake.SelectedIndexChanged += new System.EventHandler(this.m_sCmbHandShake_SelectedIndexChanged);
            // 
            // PortSelUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_sCmbHandShake);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_sCmbParity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_sCmbStopBits);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_sCmbDataBits);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_sCmbBaudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_sCmbPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_sCmbPortType);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PortSelUc";
            this.Size = new System.Drawing.Size(181, 174);
            this.Load += new System.EventHandler(this.PortSelUc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_sCmbPortType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_sCmbPort;
        private System.Windows.Forms.ComboBox m_sCmbBaudRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_sCmbDataBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox m_sCmbStopBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox m_sCmbParity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox m_sCmbHandShake;
    }
}

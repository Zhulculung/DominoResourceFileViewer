namespace DominoResourceFileViewer
{
  partial class frmHeader
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
      this.label1 = new System.Windows.Forms.Label();
      this.lMagic = new System.Windows.Forms.Label();
      this.DisplayPanel = new System.Windows.Forms.Panel();
      this.lChunkCount = new System.Windows.Forms.Label();
      this.lFileSize = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.lCRC = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lVersion = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.DisplayPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 36);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(39, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Magic:";
      // 
      // lMagic
      // 
      this.lMagic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lMagic.Location = new System.Drawing.Point(92, 32);
      this.lMagic.Name = "lMagic";
      this.lMagic.Size = new System.Drawing.Size(52, 20);
      this.lMagic.TabIndex = 1;
      this.lMagic.Text = "label2";
      this.lMagic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // DisplayPanel
      // 
      this.DisplayPanel.AutoScroll = true;
      this.DisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.DisplayPanel.Controls.Add(this.lChunkCount);
      this.DisplayPanel.Controls.Add(this.lFileSize);
      this.DisplayPanel.Controls.Add(this.label4);
      this.DisplayPanel.Controls.Add(this.label6);
      this.DisplayPanel.Controls.Add(this.label5);
      this.DisplayPanel.Controls.Add(this.lCRC);
      this.DisplayPanel.Controls.Add(this.label2);
      this.DisplayPanel.Controls.Add(this.lVersion);
      this.DisplayPanel.Controls.Add(this.label3);
      this.DisplayPanel.Controls.Add(this.label7);
      this.DisplayPanel.Controls.Add(this.lMagic);
      this.DisplayPanel.Controls.Add(this.label1);
      this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DisplayPanel.Location = new System.Drawing.Point(0, 0);
      this.DisplayPanel.Name = "DisplayPanel";
      this.DisplayPanel.Size = new System.Drawing.Size(322, 320);
      this.DisplayPanel.TabIndex = 2;
      // 
      // lChunkCount
      // 
      this.lChunkCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lChunkCount.Location = new System.Drawing.Point(92, 128);
      this.lChunkCount.Name = "lChunkCount";
      this.lChunkCount.Size = new System.Drawing.Size(52, 20);
      this.lChunkCount.TabIndex = 1;
      this.lChunkCount.Text = "label2";
      this.lChunkCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lFileSize
      // 
      this.lFileSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lFileSize.Location = new System.Drawing.Point(92, 104);
      this.lFileSize.Name = "lFileSize";
      this.lFileSize.Size = new System.Drawing.Size(52, 20);
      this.lFileSize.TabIndex = 1;
      this.lFileSize.Text = "label2";
      this.lFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 132);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(79, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Chunk number:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(148, 108);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(32, 13);
      this.label6.TabIndex = 0;
      this.label6.Text = "bytes";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 108);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(47, 13);
      this.label5.TabIndex = 0;
      this.label5.Text = "File size:";
      // 
      // lCRC
      // 
      this.lCRC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lCRC.Location = new System.Drawing.Point(92, 80);
      this.lCRC.Name = "lCRC";
      this.lCRC.Size = new System.Drawing.Size(52, 20);
      this.lCRC.TabIndex = 1;
      this.lCRC.Text = "label2";
      this.lCRC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 84);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "CRC:";
      // 
      // lVersion
      // 
      this.lVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lVersion.Location = new System.Drawing.Point(92, 56);
      this.lVersion.Name = "lVersion";
      this.lVersion.Size = new System.Drawing.Size(52, 20);
      this.lVersion.TabIndex = 1;
      this.lVersion.Text = "label2";
      this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 60);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(45, 13);
      this.label3.TabIndex = 0;
      this.label3.Text = "Version:";
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.label7.Location = new System.Drawing.Point(4, 4);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(308, 20);
      this.label7.TabIndex = 1;
      this.label7.Text = "File Header";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmHeader
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(322, 320);
      this.ControlBox = false;
      this.Controls.Add(this.DisplayPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmHeader";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "frmHeader";
      this.DisplayPanel.ResumeLayout(false);
      this.DisplayPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lMagic;
    private System.Windows.Forms.Panel DisplayPanel;
    private System.Windows.Forms.Label lVersion;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lFileSize;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label lCRC;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lChunkCount;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
  }
}
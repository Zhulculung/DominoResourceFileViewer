namespace DominoResourceFileViewer
{
  partial class frmUnknown
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
      this.pDisplayPanel = new System.Windows.Forms.Panel();
      this.rtbHexList = new System.Windows.Forms.RichTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.lLength = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lChunkID = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.pDisplayPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // pDisplayPanel
      // 
      this.pDisplayPanel.AutoScroll = true;
      this.pDisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pDisplayPanel.Controls.Add(this.rtbHexList);
      this.pDisplayPanel.Controls.Add(this.label7);
      this.pDisplayPanel.Controls.Add(this.lLength);
      this.pDisplayPanel.Controls.Add(this.label4);
      this.pDisplayPanel.Controls.Add(this.label2);
      this.pDisplayPanel.Controls.Add(this.lChunkID);
      this.pDisplayPanel.Controls.Add(this.label1);
      this.pDisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pDisplayPanel.Location = new System.Drawing.Point(0, 0);
      this.pDisplayPanel.Name = "pDisplayPanel";
      this.pDisplayPanel.Size = new System.Drawing.Size(292, 266);
      this.pDisplayPanel.TabIndex = 3;
      // 
      // rtbHexList
      // 
      this.rtbHexList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rtbHexList.Location = new System.Drawing.Point(4, 80);
      this.rtbHexList.Name = "rtbHexList";
      this.rtbHexList.ReadOnly = true;
      this.rtbHexList.Size = new System.Drawing.Size(280, 176);
      this.rtbHexList.TabIndex = 4;
      this.rtbHexList.Text = "";
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.label7.Location = new System.Drawing.Point(4, 4);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(278, 20);
      this.label7.TabIndex = 1;
      this.label7.Text = "Unknown Chunk";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lLength
      // 
      this.lLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lLength.Location = new System.Drawing.Point(92, 56);
      this.lLength.Name = "lLength";
      this.lLength.Size = new System.Drawing.Size(50, 20);
      this.lLength.TabIndex = 1;
      this.lLength.Text = "label2";
      this.lLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 60);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Length:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(148, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "bytes";
      // 
      // lChunkID
      // 
      this.lChunkID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lChunkID.Location = new System.Drawing.Point(92, 32);
      this.lChunkID.Name = "lChunkID";
      this.lChunkID.Size = new System.Drawing.Size(184, 20);
      this.lChunkID.TabIndex = 1;
      this.lChunkID.Text = "label2";
      this.lChunkID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 36);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Chunk ID:";
      // 
      // frmUnknown
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.pDisplayPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmUnknown";
      this.Text = "frmUnknown";
      this.pDisplayPanel.ResumeLayout(false);
      this.pDisplayPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pDisplayPanel;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label lChunkID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lLength;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RichTextBox rtbHexList;
  }
}
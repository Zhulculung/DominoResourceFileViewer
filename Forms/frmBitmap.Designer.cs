namespace DominoResourceFileViewer
{
  partial class frmBitmap
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
      this.pBitmapDisplay = new System.Windows.Forms.Panel();
      this.cbZoom = new System.Windows.Forms.ComboBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pbDisplayBitmap = new System.Windows.Forms.PictureBox();
      this.lBitmapNumber = new System.Windows.Forms.Label();
      this.lNumberOfColors = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.lBitmapDimension = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.pBitmapGeneral = new System.Windows.Forms.Panel();
      this.lChunkSize = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.lNumberOfBitmaps = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.pBitmapDisplay.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbDisplayBitmap)).BeginInit();
      this.pBitmapGeneral.SuspendLayout();
      this.SuspendLayout();
      // 
      // pBitmapDisplay
      // 
      this.pBitmapDisplay.AutoScroll = true;
      this.pBitmapDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pBitmapDisplay.Controls.Add(this.cbZoom);
      this.pBitmapDisplay.Controls.Add(this.panel1);
      this.pBitmapDisplay.Controls.Add(this.lBitmapNumber);
      this.pBitmapDisplay.Controls.Add(this.lNumberOfColors);
      this.pBitmapDisplay.Controls.Add(this.label2);
      this.pBitmapDisplay.Controls.Add(this.label4);
      this.pBitmapDisplay.Controls.Add(this.lBitmapDimension);
      this.pBitmapDisplay.Controls.Add(this.label1);
      this.pBitmapDisplay.Location = new System.Drawing.Point(292, 0);
      this.pBitmapDisplay.Name = "pBitmapDisplay";
      this.pBitmapDisplay.Size = new System.Drawing.Size(284, 266);
      this.pBitmapDisplay.TabIndex = 4;
      // 
      // cbZoom
      // 
      this.cbZoom.FormattingEnabled = true;
      this.cbZoom.Items.AddRange(new object[] {
            "10%",
            "25%",
            "50%",
            "100%",
            "150%",
            "200%",
            "300%",
            "400%",
            "500%"});
      this.cbZoom.Location = new System.Drawing.Point(220, 56);
      this.cbZoom.Name = "cbZoom";
      this.cbZoom.Size = new System.Drawing.Size(52, 21);
      this.cbZoom.TabIndex = 4;
      this.cbZoom.Text = "100%";
      this.cbZoom.SelectedIndexChanged += new System.EventHandler(this.cbZoom_SelectedIndexChanged);
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.AutoScroll = true;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.pbDisplayBitmap);
      this.panel1.Location = new System.Drawing.Point(4, 84);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(268, 172);
      this.panel1.TabIndex = 3;
      // 
      // pbDisplayBitmap
      // 
      this.pbDisplayBitmap.Location = new System.Drawing.Point(0, 0);
      this.pbDisplayBitmap.Name = "pbDisplayBitmap";
      this.pbDisplayBitmap.Size = new System.Drawing.Size(80, 76);
      this.pbDisplayBitmap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pbDisplayBitmap.TabIndex = 2;
      this.pbDisplayBitmap.TabStop = false;
      // 
      // lBitmapNumber
      // 
      this.lBitmapNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lBitmapNumber.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.lBitmapNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lBitmapNumber.Location = new System.Drawing.Point(4, 4);
      this.lBitmapNumber.Name = "lBitmapNumber";
      this.lBitmapNumber.Size = new System.Drawing.Size(270, 20);
      this.lBitmapNumber.TabIndex = 1;
      this.lBitmapNumber.Text = "Bitmap #";
      this.lBitmapNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lNumberOfColors
      // 
      this.lNumberOfColors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lNumberOfColors.Location = new System.Drawing.Point(108, 56);
      this.lNumberOfColors.Name = "lNumberOfColors";
      this.lNumberOfColors.Size = new System.Drawing.Size(50, 20);
      this.lNumberOfColors.TabIndex = 1;
      this.lNumberOfColors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(180, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Zoom:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 60);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(91, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Number of Colors:";
      // 
      // lBitmapDimension
      // 
      this.lBitmapDimension.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lBitmapDimension.Location = new System.Drawing.Point(108, 32);
      this.lBitmapDimension.Name = "lBitmapDimension";
      this.lBitmapDimension.Size = new System.Drawing.Size(164, 20);
      this.lBitmapDimension.TabIndex = 1;
      this.lBitmapDimension.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 36);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Bitmap dimension:";
      // 
      // pBitmapGeneral
      // 
      this.pBitmapGeneral.AutoScroll = true;
      this.pBitmapGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pBitmapGeneral.Controls.Add(this.lChunkSize);
      this.pBitmapGeneral.Controls.Add(this.label6);
      this.pBitmapGeneral.Controls.Add(this.label8);
      this.pBitmapGeneral.Controls.Add(this.lNumberOfBitmaps);
      this.pBitmapGeneral.Controls.Add(this.label9);
      this.pBitmapGeneral.Controls.Add(this.label3);
      this.pBitmapGeneral.Location = new System.Drawing.Point(4, 0);
      this.pBitmapGeneral.Name = "pBitmapGeneral";
      this.pBitmapGeneral.Size = new System.Drawing.Size(284, 260);
      this.pBitmapGeneral.TabIndex = 4;
      // 
      // lChunkSize
      // 
      this.lChunkSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lChunkSize.Location = new System.Drawing.Point(124, 52);
      this.lChunkSize.Name = "lChunkSize";
      this.lChunkSize.Size = new System.Drawing.Size(50, 20);
      this.lChunkSize.TabIndex = 5;
      this.lChunkSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(24, 56);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(62, 13);
      this.label6.TabIndex = 3;
      this.label6.Text = "Chunk size:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(180, 56);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(32, 13);
      this.label8.TabIndex = 4;
      this.label8.Text = "bytes";
      // 
      // lNumberOfBitmaps
      // 
      this.lNumberOfBitmaps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lNumberOfBitmaps.Location = new System.Drawing.Point(124, 28);
      this.lNumberOfBitmaps.Name = "lNumberOfBitmaps";
      this.lNumberOfBitmaps.Size = new System.Drawing.Size(50, 20);
      this.lNumberOfBitmaps.TabIndex = 6;
      this.lNumberOfBitmaps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(24, 32);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(98, 13);
      this.label9.TabIndex = 2;
      this.label9.Text = "Number of bitmaps:";
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.label3.Location = new System.Drawing.Point(4, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(270, 20);
      this.label3.TabIndex = 1;
      this.label3.Text = "\'BMPS\' - Bitmap chunk";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmBitmap
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(758, 266);
      this.Controls.Add(this.pBitmapGeneral);
      this.Controls.Add(this.pBitmapDisplay);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmBitmap";
      this.Text = "frmFont";
      this.pBitmapDisplay.ResumeLayout(false);
      this.pBitmapDisplay.PerformLayout();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbDisplayBitmap)).EndInit();
      this.pBitmapGeneral.ResumeLayout(false);
      this.pBitmapGeneral.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pBitmapDisplay;
    private System.Windows.Forms.Label lBitmapNumber;
    private System.Windows.Forms.Label lNumberOfColors;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lBitmapDimension;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel pBitmapGeneral;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lChunkSize;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lNumberOfBitmaps;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.PictureBox pbDisplayBitmap;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox cbZoom;
    private System.Windows.Forms.Label label2;
  }
}
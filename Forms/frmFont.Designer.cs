namespace DominoResourceFileViewer
{
  partial class frmFont
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
			this.pFontDisplay = new System.Windows.Forms.Panel();
			this.lMaxAscii = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cbZoom = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pbDisplayBitmap = new System.Windows.Forms.PictureBox();
			this.lFontNumber = new System.Windows.Forms.Label();
			this.lMinAscii = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lFontFlags = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pFontGeneral = new System.Windows.Forms.Panel();
			this.lChunkSize = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lNumberOfFonts = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lHeight = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.pFontDisplay.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbDisplayBitmap)).BeginInit();
			this.pFontGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// pFontDisplay
			// 
			this.pFontDisplay.AutoScroll = true;
			this.pFontDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pFontDisplay.Controls.Add(this.lHeight);
			this.pFontDisplay.Controls.Add(this.label10);
			this.pFontDisplay.Controls.Add(this.lMaxAscii);
			this.pFontDisplay.Controls.Add(this.label7);
			this.pFontDisplay.Controls.Add(this.cbZoom);
			this.pFontDisplay.Controls.Add(this.panel1);
			this.pFontDisplay.Controls.Add(this.lFontNumber);
			this.pFontDisplay.Controls.Add(this.lMinAscii);
			this.pFontDisplay.Controls.Add(this.label2);
			this.pFontDisplay.Controls.Add(this.label4);
			this.pFontDisplay.Controls.Add(this.lFontFlags);
			this.pFontDisplay.Controls.Add(this.label1);
			this.pFontDisplay.Location = new System.Drawing.Point(292, 0);
			this.pFontDisplay.Name = "pFontDisplay";
			this.pFontDisplay.Size = new System.Drawing.Size(284, 266);
			this.pFontDisplay.TabIndex = 4;
			// 
			// lMaxAscii
			// 
			this.lMaxAscii.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lMaxAscii.Location = new System.Drawing.Point(108, 80);
			this.lMaxAscii.Name = "lMaxAscii";
			this.lMaxAscii.Size = new System.Drawing.Size(50, 20);
			this.lMaxAscii.TabIndex = 6;
			this.lMaxAscii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 84);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "Max ASCII:";
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
			this.cbZoom.Location = new System.Drawing.Point(108, 103);
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
			this.panel1.Location = new System.Drawing.Point(4, 161);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(268, 95);
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
			// lFontNumber
			// 
			this.lFontNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lFontNumber.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lFontNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lFontNumber.Location = new System.Drawing.Point(4, 4);
			this.lFontNumber.Name = "lFontNumber";
			this.lFontNumber.Size = new System.Drawing.Size(270, 20);
			this.lFontNumber.TabIndex = 1;
			this.lFontNumber.Text = "Font #";
			this.lFontNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lMinAscii
			// 
			this.lMinAscii.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lMinAscii.Location = new System.Drawing.Point(108, 57);
			this.lMinAscii.Name = "lMinAscii";
			this.lMinAscii.Size = new System.Drawing.Size(50, 20);
			this.lMinAscii.TabIndex = 1;
			this.lMinAscii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(68, 107);
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
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Min ASCII:";
			// 
			// lFontFlags
			// 
			this.lFontFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lFontFlags.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lFontFlags.Location = new System.Drawing.Point(108, 32);
			this.lFontFlags.Name = "lFontFlags";
			this.lFontFlags.Size = new System.Drawing.Size(164, 20);
			this.lFontFlags.TabIndex = 1;
			this.lFontFlags.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Flags:";
			// 
			// pFontGeneral
			// 
			this.pFontGeneral.AutoScroll = true;
			this.pFontGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pFontGeneral.Controls.Add(this.lChunkSize);
			this.pFontGeneral.Controls.Add(this.label6);
			this.pFontGeneral.Controls.Add(this.label8);
			this.pFontGeneral.Controls.Add(this.lNumberOfFonts);
			this.pFontGeneral.Controls.Add(this.label9);
			this.pFontGeneral.Controls.Add(this.label3);
			this.pFontGeneral.Location = new System.Drawing.Point(4, 0);
			this.pFontGeneral.Name = "pFontGeneral";
			this.pFontGeneral.Size = new System.Drawing.Size(284, 260);
			this.pFontGeneral.TabIndex = 4;
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
			// lNumberOfFonts
			// 
			this.lNumberOfFonts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lNumberOfFonts.Location = new System.Drawing.Point(124, 28);
			this.lNumberOfFonts.Name = "lNumberOfFonts";
			this.lNumberOfFonts.Size = new System.Drawing.Size(50, 20);
			this.lNumberOfFonts.TabIndex = 6;
			this.lNumberOfFonts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(24, 32);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(85, 13);
			this.label9.TabIndex = 2;
			this.label9.Text = "Number of fonts:";
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
			this.label3.Text = "\'FONT\' - Font chunk";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lHeight
			// 
			this.lHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lHeight.Location = new System.Drawing.Point(222, 60);
			this.lHeight.Name = "lHeight";
			this.lHeight.Size = new System.Drawing.Size(50, 20);
			this.lHeight.TabIndex = 8;
			this.lHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(175, 64);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(41, 13);
			this.label10.TabIndex = 7;
			this.label10.Text = "Height:";
			// 
			// frmFont
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(758, 266);
			this.Controls.Add(this.pFontGeneral);
			this.Controls.Add(this.pFontDisplay);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmFont";
			this.Text = "frmFont";
			this.pFontDisplay.ResumeLayout(false);
			this.pFontDisplay.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbDisplayBitmap)).EndInit();
			this.pFontGeneral.ResumeLayout(false);
			this.pFontGeneral.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pFontDisplay;
    private System.Windows.Forms.Label lFontNumber;
    private System.Windows.Forms.Label lMinAscii;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lFontFlags;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel pFontGeneral;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lChunkSize;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lNumberOfFonts;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.PictureBox pbDisplayBitmap;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox cbZoom;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lMaxAscii;
    private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lHeight;
		private System.Windows.Forms.Label label10;
  }
}
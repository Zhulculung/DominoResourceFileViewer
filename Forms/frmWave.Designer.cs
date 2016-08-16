namespace DominoResourceFileViewer
{
  partial class frmWave
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
			this.pWaveDisplay = new System.Windows.Forms.Panel();
			this.bAll = new System.Windows.Forms.Button();
			this.bZoom = new System.Windows.Forms.Button();
			this.bStop = new System.Windows.Forms.Button();
			this.bPlay = new System.Windows.Forms.Button();
			this.sbWavePos = new System.Windows.Forms.HScrollBar();
			this.pbWaveDisplay = new System.Windows.Forms.PictureBox();
			this.lWaveNumber = new System.Windows.Forms.Label();
			this.lNumberOfSamples = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lWaveFormat = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pWaveGeneral = new System.Windows.Forms.Panel();
			this.lChunkSize = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lNumberOfWave = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pWaveDisplay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbWaveDisplay)).BeginInit();
			this.pWaveGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// pWaveDisplay
			// 
			this.pWaveDisplay.AutoScroll = true;
			this.pWaveDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pWaveDisplay.Controls.Add(this.bAll);
			this.pWaveDisplay.Controls.Add(this.bZoom);
			this.pWaveDisplay.Controls.Add(this.bStop);
			this.pWaveDisplay.Controls.Add(this.bPlay);
			this.pWaveDisplay.Controls.Add(this.sbWavePos);
			this.pWaveDisplay.Controls.Add(this.pbWaveDisplay);
			this.pWaveDisplay.Controls.Add(this.lWaveNumber);
			this.pWaveDisplay.Controls.Add(this.lNumberOfSamples);
			this.pWaveDisplay.Controls.Add(this.label4);
			this.pWaveDisplay.Controls.Add(this.lWaveFormat);
			this.pWaveDisplay.Controls.Add(this.label1);
			this.pWaveDisplay.Location = new System.Drawing.Point(292, 0);
			this.pWaveDisplay.Name = "pWaveDisplay";
			this.pWaveDisplay.Size = new System.Drawing.Size(284, 280);
			this.pWaveDisplay.TabIndex = 6;
			// 
			// bAll
			// 
			this.bAll.Location = new System.Drawing.Point(156, 248);
			this.bAll.Name = "bAll";
			this.bAll.Size = new System.Drawing.Size(75, 23);
			this.bAll.TabIndex = 8;
			this.bAll.Text = "All";
			this.bAll.UseVisualStyleBackColor = true;
			// 
			// bZoom
			// 
			this.bZoom.Location = new System.Drawing.Point(76, 248);
			this.bZoom.Name = "bZoom";
			this.bZoom.Size = new System.Drawing.Size(75, 23);
			this.bZoom.TabIndex = 8;
			this.bZoom.Text = "Zoom";
			this.bZoom.UseVisualStyleBackColor = true;
			// 
			// bStop
			// 
			this.bStop.Image = global::DominoResourceFileViewer.Properties.Resources.Stop;
			this.bStop.Location = new System.Drawing.Point(32, 248);
			this.bStop.Name = "bStop";
			this.bStop.Size = new System.Drawing.Size(24, 23);
			this.bStop.TabIndex = 7;
			this.bStop.UseVisualStyleBackColor = true;
			this.bStop.Click += new System.EventHandler(this.bStop_Click);
			// 
			// bPlay
			// 
			this.bPlay.Image = global::DominoResourceFileViewer.Properties.Resources.Play;
			this.bPlay.Location = new System.Drawing.Point(4, 248);
			this.bPlay.Name = "bPlay";
			this.bPlay.Size = new System.Drawing.Size(24, 23);
			this.bPlay.TabIndex = 6;
			this.bPlay.UseVisualStyleBackColor = true;
			this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
			// 
			// sbWavePos
			// 
			this.sbWavePos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sbWavePos.Location = new System.Drawing.Point(4, 228);
			this.sbWavePos.Name = "sbWavePos";
			this.sbWavePos.Size = new System.Drawing.Size(272, 16);
			this.sbWavePos.TabIndex = 5;
			this.sbWavePos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sbWavePos_Scroll);
			// 
			// pbWaveDisplay
			// 
			this.pbWaveDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbWaveDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbWaveDisplay.Location = new System.Drawing.Point(4, 84);
			this.pbWaveDisplay.Name = "pbWaveDisplay";
			this.pbWaveDisplay.Size = new System.Drawing.Size(272, 144);
			this.pbWaveDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbWaveDisplay.TabIndex = 2;
			this.pbWaveDisplay.TabStop = false;
			this.pbWaveDisplay.SizeChanged += new System.EventHandler(this.pbWaveDisplay_SizeChanged);
			// 
			// lWaveNumber
			// 
			this.lWaveNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lWaveNumber.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lWaveNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lWaveNumber.Location = new System.Drawing.Point(4, 4);
			this.lWaveNumber.Name = "lWaveNumber";
			this.lWaveNumber.Size = new System.Drawing.Size(272, 20);
			this.lWaveNumber.TabIndex = 1;
			this.lWaveNumber.Text = "Wave #";
			this.lWaveNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lNumberOfSamples
			// 
			this.lNumberOfSamples.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lNumberOfSamples.Location = new System.Drawing.Point(112, 56);
			this.lNumberOfSamples.Name = "lNumberOfSamples";
			this.lNumberOfSamples.Size = new System.Drawing.Size(50, 20);
			this.lNumberOfSamples.TabIndex = 1;
			this.lNumberOfSamples.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Number of samples:";
			// 
			// lWaveFormat
			// 
			this.lWaveFormat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lWaveFormat.Location = new System.Drawing.Point(112, 32);
			this.lWaveFormat.Name = "lWaveFormat";
			this.lWaveFormat.Size = new System.Drawing.Size(164, 20);
			this.lWaveFormat.TabIndex = 1;
			this.lWaveFormat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Wave format:";
			// 
			// pWaveGeneral
			// 
			this.pWaveGeneral.AutoScroll = true;
			this.pWaveGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pWaveGeneral.Controls.Add(this.lChunkSize);
			this.pWaveGeneral.Controls.Add(this.label6);
			this.pWaveGeneral.Controls.Add(this.label8);
			this.pWaveGeneral.Controls.Add(this.lNumberOfWave);
			this.pWaveGeneral.Controls.Add(this.label9);
			this.pWaveGeneral.Controls.Add(this.label3);
			this.pWaveGeneral.Location = new System.Drawing.Point(4, 0);
			this.pWaveGeneral.Name = "pWaveGeneral";
			this.pWaveGeneral.Size = new System.Drawing.Size(284, 280);
			this.pWaveGeneral.TabIndex = 5;
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
			// lNumberOfWave
			// 
			this.lNumberOfWave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lNumberOfWave.Location = new System.Drawing.Point(124, 28);
			this.lNumberOfWave.Name = "lNumberOfWave";
			this.lNumberOfWave.Size = new System.Drawing.Size(50, 20);
			this.lNumberOfWave.TabIndex = 6;
			this.lNumberOfWave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(24, 32);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(93, 13);
			this.label9.TabIndex = 2;
			this.label9.Text = "Number of waves:";
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
			this.label3.Text = "\'WAVE\' - Wave chunk";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// frmWave
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(583, 286);
			this.Controls.Add(this.pWaveDisplay);
			this.Controls.Add(this.pWaveGeneral);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmWave";
			this.Text = "frmWave";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWave_FormClosed);
			this.pWaveDisplay.ResumeLayout(false);
			this.pWaveDisplay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbWaveDisplay)).EndInit();
			this.pWaveGeneral.ResumeLayout(false);
			this.pWaveGeneral.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pWaveDisplay;
    private System.Windows.Forms.PictureBox pbWaveDisplay;
    private System.Windows.Forms.Label lWaveNumber;
    private System.Windows.Forms.Label lNumberOfSamples;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lWaveFormat;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel pWaveGeneral;
    private System.Windows.Forms.Label lChunkSize;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lNumberOfWave;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.HScrollBar sbWavePos;
    private System.Windows.Forms.Button bStop;
    private System.Windows.Forms.Button bPlay;
    private System.Windows.Forms.Button bAll;
    private System.Windows.Forms.Button bZoom;
  }
}
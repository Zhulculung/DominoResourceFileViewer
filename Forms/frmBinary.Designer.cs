namespace DominoResourceFileViewer
{
  partial class frmBinary
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
      this.pBinaryDisplay = new System.Windows.Forms.Panel();
      this.pbConversion = new System.Windows.Forms.ProgressBar();
      this.cbConverter = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lBinaryNumber = new System.Windows.Forms.Label();
      this.lLength = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pBinaryGeneral = new System.Windows.Forms.Panel();
      this.lChunkSize = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.lNumberOfEntries = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
      this.wbBinaryView = new System.Windows.Forms.WebBrowser();
      this.pBinaryView = new System.Windows.Forms.Panel();
      this.pBinaryDisplay.SuspendLayout();
      this.pBinaryGeneral.SuspendLayout();
      this.pBinaryView.SuspendLayout();
      this.SuspendLayout();
      // 
      // pBinaryDisplay
      // 
      this.pBinaryDisplay.AutoScroll = true;
      this.pBinaryDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pBinaryDisplay.Controls.Add(this.pBinaryView);
      this.pBinaryDisplay.Controls.Add(this.pbConversion);
      this.pBinaryDisplay.Controls.Add(this.cbConverter);
      this.pBinaryDisplay.Controls.Add(this.label1);
      this.pBinaryDisplay.Controls.Add(this.lBinaryNumber);
      this.pBinaryDisplay.Controls.Add(this.lLength);
      this.pBinaryDisplay.Controls.Add(this.label4);
      this.pBinaryDisplay.Controls.Add(this.label2);
      this.pBinaryDisplay.Location = new System.Drawing.Point(292, 4);
      this.pBinaryDisplay.Name = "pBinaryDisplay";
      this.pBinaryDisplay.Size = new System.Drawing.Size(419, 266);
      this.pBinaryDisplay.TabIndex = 3;
      // 
      // pbConversion
      // 
      this.pbConversion.Location = new System.Drawing.Point(220, 28);
      this.pbConversion.Name = "pbConversion";
      this.pbConversion.Size = new System.Drawing.Size(155, 20);
      this.pbConversion.TabIndex = 7;
      this.pbConversion.Visible = false;
      // 
      // cbConverter
      // 
      this.cbConverter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbConverter.FormattingEnabled = true;
      this.cbConverter.Location = new System.Drawing.Point(220, 28);
      this.cbConverter.Name = "cbConverter";
      this.cbConverter.Size = new System.Drawing.Size(155, 21);
      this.cbConverter.TabIndex = 6;
      this.cbConverter.SelectedIndexChanged += new System.EventHandler(this.cbConverter_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(166, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Show as:";
      // 
      // lBinaryNumber
      // 
      this.lBinaryNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lBinaryNumber.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.lBinaryNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lBinaryNumber.Location = new System.Drawing.Point(4, 4);
      this.lBinaryNumber.Name = "lBinaryNumber";
      this.lBinaryNumber.Size = new System.Drawing.Size(405, 20);
      this.lBinaryNumber.TabIndex = 1;
      this.lBinaryNumber.Text = "Bin #";
      this.lBinaryNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lLength
      // 
      this.lLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lLength.Location = new System.Drawing.Point(61, 28);
      this.lLength.Name = "lLength";
      this.lLength.Size = new System.Drawing.Size(50, 20);
      this.lLength.TabIndex = 1;
      this.lLength.Text = "label2";
      this.lLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 32);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Length:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(117, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "bytes";
      // 
      // pBinaryGeneral
      // 
      this.pBinaryGeneral.AutoScroll = true;
      this.pBinaryGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pBinaryGeneral.Controls.Add(this.lChunkSize);
      this.pBinaryGeneral.Controls.Add(this.label6);
      this.pBinaryGeneral.Controls.Add(this.label8);
      this.pBinaryGeneral.Controls.Add(this.lNumberOfEntries);
      this.pBinaryGeneral.Controls.Add(this.label9);
      this.pBinaryGeneral.Controls.Add(this.label3);
      this.pBinaryGeneral.Location = new System.Drawing.Point(2, 4);
      this.pBinaryGeneral.Name = "pBinaryGeneral";
      this.pBinaryGeneral.Size = new System.Drawing.Size(284, 260);
      this.pBinaryGeneral.TabIndex = 6;
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
      // lNumberOfEntries
      // 
      this.lNumberOfEntries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lNumberOfEntries.Location = new System.Drawing.Point(124, 28);
      this.lNumberOfEntries.Name = "lNumberOfEntries";
      this.lNumberOfEntries.Size = new System.Drawing.Size(50, 20);
      this.lNumberOfEntries.TabIndex = 6;
      this.lNumberOfEntries.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(24, 32);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(93, 13);
      this.label9.TabIndex = 2;
      this.label9.Text = "Number of entries:";
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
      this.label3.Text = "\'BINA\' - Binary chunk";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tmrRefresh
      // 
      this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
      // 
      // wbBinaryView
      // 
      this.wbBinaryView.AllowWebBrowserDrop = false;
      this.wbBinaryView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.wbBinaryView.Location = new System.Drawing.Point(0, 0);
      this.wbBinaryView.MinimumSize = new System.Drawing.Size(20, 20);
      this.wbBinaryView.Name = "wbBinaryView";
      this.wbBinaryView.Size = new System.Drawing.Size(392, 196);
      this.wbBinaryView.TabIndex = 19;
      // 
      // pBinaryView
      // 
      this.pBinaryView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pBinaryView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pBinaryView.Controls.Add(this.wbBinaryView);
      this.pBinaryView.Location = new System.Drawing.Point(15, 54);
      this.pBinaryView.Name = "pBinaryView";
      this.pBinaryView.Size = new System.Drawing.Size(394, 198);
      this.pBinaryView.TabIndex = 20;
      // 
      // frmBinary
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(719, 270);
      this.Controls.Add(this.pBinaryGeneral);
      this.Controls.Add(this.pBinaryDisplay);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmBinary";
      this.Text = "frmBinary";
      this.pBinaryDisplay.ResumeLayout(false);
      this.pBinaryDisplay.PerformLayout();
      this.pBinaryGeneral.ResumeLayout(false);
      this.pBinaryGeneral.PerformLayout();
      this.pBinaryView.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pBinaryDisplay;
    private System.Windows.Forms.Label lBinaryNumber;
    private System.Windows.Forms.Label lLength;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel pBinaryGeneral;
    private System.Windows.Forms.Label lChunkSize;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lNumberOfEntries;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbConverter;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ProgressBar pbConversion;
    private System.Windows.Forms.Timer tmrRefresh;
    private System.Windows.Forms.WebBrowser wbBinaryView;
    private System.Windows.Forms.Panel pBinaryView;
  }
}
namespace DominoResourceFileViewer
{
  partial class frmString
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.DisplayPanel = new System.Windows.Forms.Panel();
      this.lLength = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.dgvStringList = new System.Windows.Forms.DataGridView();
      this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label7 = new System.Windows.Forms.Label();
      this.DisplayPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvStringList)).BeginInit();
      this.SuspendLayout();
      // 
      // DisplayPanel
      // 
      this.DisplayPanel.AutoScroll = true;
      this.DisplayPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.DisplayPanel.Controls.Add(this.lLength);
      this.DisplayPanel.Controls.Add(this.label4);
      this.DisplayPanel.Controls.Add(this.label2);
      this.DisplayPanel.Controls.Add(this.dgvStringList);
      this.DisplayPanel.Controls.Add(this.label7);
      this.DisplayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DisplayPanel.Location = new System.Drawing.Point(0, 0);
      this.DisplayPanel.Name = "DisplayPanel";
      this.DisplayPanel.Size = new System.Drawing.Size(292, 266);
      this.DisplayPanel.TabIndex = 4;
      // 
      // lLength
      // 
      this.lLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lLength.Location = new System.Drawing.Point(76, 28);
      this.lLength.Name = "lLength";
      this.lLength.Size = new System.Drawing.Size(50, 20);
      this.lLength.TabIndex = 5;
      this.lLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(8, 32);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 4;
      this.label4.Text = "Chunk Size:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(132, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "bytes";
      // 
      // dgvStringList
      // 
      this.dgvStringList.AllowUserToAddRows = false;
      this.dgvStringList.AllowUserToDeleteRows = false;
      this.dgvStringList.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.dgvStringList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dgvStringList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvStringList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvStringList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Column1,
            this.Column2,
            this.Column3});
      this.dgvStringList.Location = new System.Drawing.Point(4, 52);
      this.dgvStringList.Name = "dgvStringList";
      this.dgvStringList.ReadOnly = true;
      this.dgvStringList.RowHeadersVisible = false;
      this.dgvStringList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvStringList.RowTemplate.ReadOnly = true;
      this.dgvStringList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvStringList.Size = new System.Drawing.Size(276, 204);
      this.dgvStringList.TabIndex = 2;
      // 
      // Number
      // 
      this.Number.HeaderText = "#";
      this.Number.Name = "Number";
      this.Number.ReadOnly = true;
      this.Number.Width = 50;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Index";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Width = 50;
      // 
      // Column2
      // 
      this.Column2.HeaderText = "Length";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Width = 50;
      // 
      // Column3
      // 
      this.Column3.HeaderText = "String";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 400;
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
      this.label7.Text = "String Chunk";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmString
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.DisplayPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmString";
      this.Text = "frmString";
      this.DisplayPanel.ResumeLayout(false);
      this.DisplayPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvStringList)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel DisplayPanel;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.DataGridView dgvStringList;
    private System.Windows.Forms.Label lLength;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Number;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
  }
}
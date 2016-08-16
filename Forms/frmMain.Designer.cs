namespace DominoResourceFileViewer
{
  partial class frmMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.tvMainTree = new System.Windows.Forms.TreeView();
      this.ilTreeControl = new System.Windows.Forms.ImageList(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.msMain = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.odfResourceFile = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.msMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // tvMainTree
      // 
      this.tvMainTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMainTree.ImageIndex = 0;
      this.tvMainTree.ImageList = this.ilTreeControl;
      this.tvMainTree.Location = new System.Drawing.Point(0, 0);
      this.tvMainTree.Name = "tvMainTree";
      this.tvMainTree.SelectedImageIndex = 0;
      this.tvMainTree.Size = new System.Drawing.Size(152, 402);
      this.tvMainTree.TabIndex = 0;
      this.tvMainTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMainTree_AfterSelect);
      // 
      // ilTreeControl
      // 
      this.ilTreeControl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeControl.ImageStream")));
      this.ilTreeControl.TransparentColor = System.Drawing.Color.Transparent;
      this.ilTreeControl.Images.SetKeyName(0, "Domino.ico");
      this.ilTreeControl.Images.SetKeyName(1, "Unknown.ico");
      this.ilTreeControl.Images.SetKeyName(2, "String.ico");
      this.ilTreeControl.Images.SetKeyName(3, "java.ico");
      this.ilTreeControl.Images.SetKeyName(4, "Bitmap.ico");
      this.ilTreeControl.Images.SetKeyName(5, "Wave.ico");
      this.ilTreeControl.Images.SetKeyName(6, "Binary.ico");
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 24);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tvMainTree);
      this.splitContainer1.Size = new System.Drawing.Size(727, 402);
      this.splitContainer1.SplitterDistance = 152;
      this.splitContainer1.TabIndex = 1;
      // 
      // msMain
      // 
      this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
      this.msMain.Location = new System.Drawing.Point(0, 0);
      this.msMain.Name = "msMain";
      this.msMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.msMain.Size = new System.Drawing.Size(727, 24);
      this.msMain.TabIndex = 2;
      this.msMain.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
      this.openToolStripMenuItem.Text = "&Open...";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
      this.exitToolStripMenuItem.Text = "&Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // odfResourceFile
      // 
      this.odfResourceFile.DefaultExt = "bin";
      this.odfResourceFile.Filter = "Binary Resource File|*.bin|Rom file|*.rom|C header file|*.h|All files|*.*";
      this.odfResourceFile.Title = "Open Domino Resource File...";
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(727, 426);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.msMain);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Domino Resource File Viewer";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.msMain.ResumeLayout(false);
      this.msMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TreeView tvMainTree;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.MenuStrip msMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog odfResourceFile;
    private System.Windows.Forms.ImageList ilTreeControl;
  }
}


///////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2005-2016 Laszlo Arvai. All rights reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software Foundation,
// Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
///////////////////////////////////////////////////////////////////////////////
// File description
// ----------------
// Main form
///////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmMain : Form
  {
    fileResourceLoader m_resource_file;
		chkCDecl m_c_declaration_parser;
    int m_last_used_chunk_index = -1;
		string m_window_title;

    public frmMain()
    {
      string[] args;

      InitializeComponent();

			m_window_title = this.Text;
      m_resource_file = new fileResourceLoader();
      m_c_declaration_parser = new chkCDecl();

      args = Environment.GetCommandLineArgs();

      if (args.Length > 2)
      {
        MessageBox.Show("Error", "Invalid command line argument.", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else
      {
        if (args.Length == 2)
        {
					LoadFile(args[1]);
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    void ClearResourceFile()
    {
      // clear nodes
      tvMainTree.Nodes.Clear();

      // clear display
      splitContainer1.Panel2.Controls.Clear();

      // clear chunks
      if (m_resource_file.Chunks != null)
      {
        foreach (chkChunkBase chunk in m_resource_file.Chunks)
        {
          chunk.ReleaseDisplayForm();
        }
      }

      m_resource_file.Clear();

      m_last_used_chunk_index = -1;
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (odfResourceFile.ShowDialog() == DialogResult.OK)
      {
				LoadFile(odfResourceFile.FileName);
      }
    }

		private void UpdateWindowTitle(string in_file_name)
		{
			this.Text = m_window_title + " - " + in_file_name;
		}
    
    private void LoadFile(string in_name)
    {
			// decide resource file type
			string ext = Path.GetExtension(in_name).ToUpper();
			string filename;
			bool success = false;

			if (ext == ".H")
			{
				m_c_declaration_parser.Load(in_name);
				
				filename = m_c_declaration_parser.GetOutputFile();
				if(filename!="")
				{
					filename = Path.Combine(Path.GetDirectoryName(in_name),filename);
					success = LoadResourceFile(filename);
				}
			}
			else
				success = LoadResourceFile(odfResourceFile.FileName);

			if (success)
			{
				UpdateWindowTitle(in_name);
			}
			else
			{
				MessageBox.Show(this, "Can't load resource file (" + in_name + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

    private bool LoadResourceFile(string in_name)
    {
      bool success = false;
      byte[] resource_file = null;
      string ext;

      // clear data
      ClearResourceFile();

      // decide resource file type
      ext = Path.GetExtension(in_name).ToUpper();

      // load C file type
      if (ext == ".C")
      {
        try
        {
          string line;
          int i;
          bool inside_comment = false;
          bool inside_data = false;
          int index = 0;

          TextReader file = new StreamReader(in_name);

          do
          {
            line = file.ReadLine();

            i = 0;
            while (line != null && i < line.Length)
            {
              // skip comment if we are inside the comment section
              if (inside_comment)
              {
                // if comment ended
                if (line[i] == '*' && line.Length > i + 1 && line[i + 1] == '/')
                {
                  inside_comment = false;
                  i++;
                }
              }
              else
              {
                // check character
                switch (line[i])
                {
                  // check for comment start
                  case '/':
                    if (line.Length > i + 1)
                    {
                      switch (line[i + 1])
                      {
                        // single line comment
                        case '/':
                          i = line.Length;  // skip this line
                          break;

                        case '*':
                          inside_comment = true;
                          i++;
                          break;
                      }
                    }
                    break;

                  // check for array size
                  case '[':
                    {
                      int pos = line.IndexOf(']', i);

                      if (pos != -1)
                      {
                        int size;

                        // get array size
                        int.TryParse(line.Substring(i + 1, pos - i - 1), out size);

                        // alloate buffer
                        resource_file = new byte[size];

                        i = pos + 1;
                      }
                    }
                    break;

                  // check for data section
                  case '{':
                    inside_data = true;
                    break;

                  case '0':
                    if (inside_data)
                    {
                      // load data
                      if (i + 3 < line.Length)
                      {
                        string data = line.Substring(i + 2, 2);

                        resource_file[index] = (byte)int.Parse(data, System.Globalization.NumberStyles.HexNumber);
                        index++;
                        i += 4;
                      }
                    }
                    break;
                }
              }
              i++;
            }
          } while (line != null);

          file.Close();

          success = true;
        }
        catch
        {
          success = false;
        }
      }
      else
      {
        // load intel hex file
        if (ext == ".HEX")
        {

        }
        else
        {
          // load binary file
          try
          {
            BinaryReader stream = new BinaryReader(File.Open(in_name, FileMode.Open, FileAccess.Read, FileShare.Read));

            resource_file = new byte[stream.BaseStream.Length];
            stream.Read(resource_file, 0, resource_file.Length);

            stream.Close();

            success = true;
          }
          catch
          {
          }
        }
      }

      // process resource file
      if(success)
        success = m_resource_file.ProcessResourceFile(resource_file, m_c_declaration_parser,tvMainTree);

			return success;
    }

    private void tvMainTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      int new_index = -1;
      Panel panel;
      int first_level_index = -1;
      string name;

      name = e.Node.Name;
      first_level_index=name.IndexOf("\\");

      if (first_level_index > 0)
        name = name.Remove(first_level_index);

      int.TryParse(name, out new_index);
      
      // clear previous panel
      splitContainer1.Panel2.Controls.Clear();

      if (m_last_used_chunk_index != -1)
        m_resource_file.Chunks[m_last_used_chunk_index].ReleaseDisplayForm();

      // add new panel
      if (new_index != -1)
      {
        panel = m_resource_file.Chunks[new_index].GetDisplayPanel((string)e.Node.Name);
        if (panel != null)
        {
          panel.Dock = DockStyle.Fill;
          splitContainer1.Panel2.Controls.Add(panel);
        }
      }
      
      m_last_used_chunk_index = new_index;
    }
  }
}
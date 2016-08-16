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
// String chunk display class
///////////////////////////////////////////////////////////////////////////////
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmString : Form
  {
    public frmString()
    {
      InitializeComponent();
    }

    public Panel FillControls(byte[] in_buffer)
    {
      int pos;
      int length;
      string buffer;
      int row_index;
      int start_pos;

      // fill length
      lLength.Text = in_buffer.Length.ToString();

      // create string list
      pos = 0;
      while (pos < in_buffer.Length)
      {
        // start new line
        start_pos = pos;

        // get length
        length = in_buffer[pos++];

        if (length > 128)
        {
          length -= 128;

          length += in_buffer[pos++] * 128;

          if (length >= 16384)
          {
            length -= 16384;

            length += in_buffer[pos++];
          }
        }

        // get string
        buffer = "";

        for (int i = 0; i < length && pos < in_buffer.Length; i++)
        {
          buffer += (char)(in_buffer[pos++]);
        }

        // add string to list
        row_index = dgvStringList.Rows.Add();
        dgvStringList.Rows[row_index].Cells[0].Value = row_index + 1;
        dgvStringList.Rows[row_index].Cells[1].Value = start_pos.ToString();
        dgvStringList.Rows[row_index].Cells[2].Value = length.ToString();
        dgvStringList.Rows[row_index].Cells[3].Value = buffer;
      }

      return DisplayPanel;
    }

  }
}
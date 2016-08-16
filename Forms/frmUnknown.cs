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
// Unknown chunk display class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmUnknown : Form
  {
    public frmUnknown()
    {
      InitializeComponent();
    }

    public void DisplayChunkID(UInt32 in_chunk_id)
    {
      string chunk_id;

      chunk_id = "";

      chunk_id += (char)((in_chunk_id) & 0xff);
      chunk_id += (char)((in_chunk_id >> 8) & 0xff);
      chunk_id += (char)((in_chunk_id >> 16) & 0xff);
      chunk_id += (char)((in_chunk_id >> 24) & 0xff);


      lChunkID.Text = "'" + chunk_id + "'  0x" + in_chunk_id.ToString("X8");
    }

    public Panel FillControls(byte[] in_buffer)
    {
      /*
      lLength.Text = in_buffer.Length.ToString();

      // create hex view
      UInt16 address;
      string line_buffer = "";
      string document = "<TT>";
      
      for (address = 0; address < in_buffer.Length; address++)
      {
        if (address % 16 == 0)
        {
          // add this line to the list
          if (line_buffer.Length > 0)
            document += line_buffer + "<br>";

          line_buffer = "";
        }

        // start new line
        if (line_buffer.Length == 0)
          line_buffer = "<b>" + address.ToString("X4") + ":</b> ";

        line_buffer += in_buffer[address].ToString("X2") + " ";
      }

      if (line_buffer.Length > 0)
        document += line_buffer + "<br>";

      wbList.DocumentText = document + "</TT>";
*/
      lLength.Text = in_buffer.Length.ToString();

      // create hex view
      UInt16 address;
      string line_buffer = "";
      string document = "{\\rtf1\\ansi{\\fonttbl\\f0\\fs10\\fswiss Courier New;}\\f0\\fs20";
      
      for (address = 0; address < in_buffer.Length; address++)
      {
        if (address % 16 == 0)
        {
          // add this line to the list
          if (line_buffer.Length > 0)
            document += "\\pard" + line_buffer + "\\par";

          line_buffer = "";
        }

        // start new line
        if (line_buffer.Length == 0)
          line_buffer = "{\\b " + address.ToString("X4") + ":} ";

        line_buffer += in_buffer[address].ToString("X2") + " ";
      }

      if (line_buffer.Length > 0)
        document += "\\pard" + line_buffer + "\\par";

      rtbHexList.Rtf = document + "}";

      return pDisplayPanel;
    }

  }
}
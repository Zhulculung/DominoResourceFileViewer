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
// Resource file header display class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmHeader : Form
  {
    public frmHeader()
    {
      InitializeComponent();
    }

    public Panel FillControls(byte[] in_buffer)
    {
      string label;

      // fill data
      label = "";
      label += ((char)in_buffer[0]);
      label += ((char)in_buffer[1]);
      lMagic.Text = label;
      lVersion.Text = in_buffer[2].ToString() + "." + in_buffer[3].ToString();
      lCRC.Text = "0x" + BitConverter.ToUInt16(in_buffer, 4).ToString("X4");
      lFileSize.Text = BitConverter.ToUInt32(in_buffer, 6).ToString();
      lChunkCount.Text = BitConverter.ToUInt16(in_buffer, 10).ToString();

      return DisplayPanel;
    }
  }
}
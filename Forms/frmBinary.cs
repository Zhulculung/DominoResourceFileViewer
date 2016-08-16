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
// Binary chunk display class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Display form for binary chunk
	/// </summary>
	public partial class frmBinary : Form
  {
    #region · Data members ·
    private chkBinary.BinaryInfo m_binary_info;
    #endregion

    #region · Constructor ·
    public frmBinary()
    {
      InitializeComponent();

      cbConverter.Items.Add(new dispBinaryHex(dispBinaryHex.DisplayMode.Byte));
      cbConverter.Items.Add(new dispBinaryHex(dispBinaryHex.DisplayMode.Word));
      cbConverter.Items.Add(new dispBinaryHex(dispBinaryHex.DisplayMode.DoubleWord));
      cbConverter.Items.Add(new dispZ80Disassembler());
      cbConverter.Items.Add(new dispM6502Disassembler());
      cbConverter.Items.Add(new dispI8080Disassembler());

      cbConverter.SelectedIndex = 0;
    }
    #endregion

		/// <summary>
		/// Fill (updates) displayed control contents with the actual chunk data content
		/// </summary>
		/// <param name="in_binary"></param>
		/// <param name="in_path"></param>
		/// <returns></returns>
    public Panel FillControls(List<chkBinary.BinaryInfo> in_binary, string in_path)
    {
      string[] split;
      UInt32 size;

      split = in_path.Split('\\');

      switch (split.Length)
      {
				// chunk header information
        case 1:
          lNumberOfEntries.Text = in_binary.Count.ToString();

          size = 0;
          for (int i = 0; i < in_binary.Count; i++)
          {
            size += in_binary[i].EntrySize;
          }

          lChunkSize.Text = size.ToString();

          return pBinaryGeneral;

        // chunk entry
        case 2:
          {
            int binary_index;

            if (int.TryParse(split[1], out binary_index))
            {
              // store binary info
              m_binary_info = in_binary[binary_index - 1];

              // wave number
              lBinaryNumber.Text = lBinaryNumber.Text + binary_index.ToString() + " '" + in_binary[binary_index - 1].ReferenceName + "'";

              // length
              lLength.Text = m_binary_info.EntrySize.ToString();

              // generate display
              UpdateDisplayMode();

              return pBinaryDisplay;
            }
          }
          break;
      }

      return null;
    }

    private void cbConverter_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateDisplayMode();
    }

		/// <summary>
		/// Updates data display mode
		/// </summary>
    private void UpdateDisplayMode()
    {
      if (cbConverter.SelectedItem != null && m_binary_info != null)
      {
        IBinaryViewer viewer = (IBinaryViewer)cbConverter.SelectedItem;
        string html;
          
        // adapt GUI to wait mode
        pBinaryView.UseWaitCursor = true;
        cbConverter.Visible = false;
        pbConversion.Value = 0;
        pbConversion.Visible = true;
        tmrRefresh.Enabled = true;

        html = viewer.ConvertToHTML(m_binary_info.Buffer);

        // prepare for update
        tmrRefresh.Enabled = false;
        cbConverter.Visible = true;
        pbConversion.Visible = false;
        Application.DoEvents();

        // update HTML
        wbBinaryView.DocumentText = html;

        // restore GUI
        pBinaryView.UseWaitCursor=false;
        wbBinaryView.Focus();
      }
    }

		/// <summary>
		/// Display preparaation progress bar handling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
    private void tmrRefresh_Tick(object sender, EventArgs e)
    {
      if (cbConverter.SelectedItem != null)
      {
        IBinaryViewer viewer = (IBinaryViewer)cbConverter.SelectedItem;

        pbConversion.Value = viewer.GetPercentageReady();
      }
    }
  }
}
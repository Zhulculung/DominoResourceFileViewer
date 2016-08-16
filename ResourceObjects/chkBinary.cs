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
// Binary chunk handling class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public class chkBinary : chkChunkBase
  {
    #region · Constants ·
    const int TreeIconID = 6;
    #endregion

    #region · Types ·
    /// <summary>
    /// Entry info in binary chunk
    /// </summary>
    public class BinaryInfo
    {
      public UInt32 EntrySize;
      public byte[] Buffer;
      public string ReferenceName;
    }
    #endregion

    #region · Data members ·
    List<BinaryInfo> m_binary_entries;		/// list of the chunk entries
    int m_offset;													/// offset of the chun kwithin the file
    frmBinary m_display_form;							/// form used to display chunk data
    #endregion

    #region · Constructor&Destructor ·
		/// Default ocnstructor
    public chkBinary()
    {
      m_display_form = null;
      m_binary_entries = new List<BinaryInfo>();
    }
    #endregion

    #region · Load function ·

    /// <summary>
    /// Loads binary chunk
    /// </summary>
    /// <param name="in_chunk_id">ID of the chunk</param>
    /// <param name="in_buffer">buffer contains the whiole chunk</param>
    /// <param name="in_offset">Offset of the chunk within the resource file</param>
    /// <returns>True is success</returns>
    public override bool Load(UInt32 in_chunk_id, byte[] in_buffer, int in_offset)
    {
      BinaryInfo binary_info;
      UInt32 pos = 0;
      UInt32 binary_length = 0;
      UInt32 entry_size = 0;

      m_offset = in_offset;

      // init array
      m_binary_entries = new List<BinaryInfo>();

      // load data
      while (pos < in_buffer.Length)
      {
        // create new wave info
        binary_info = new BinaryInfo();
 
        // get reference id
        binary_info.ReferenceName = m_declaration_parser.GetReferenceName(in_chunk_id, (UInt32)in_offset, (UInt32)pos);

        // load length
        binary_length = (UInt32)(in_buffer[pos] + ((in_buffer[pos + 1] & 0x7f) << 8));

        if ((in_buffer[pos + 1] & 0x80) != 0)
        {
          pos += 2;

          binary_length += (UInt32)((in_buffer[pos] + (in_buffer[pos + 1] << 8))<<16);

          entry_size = 4;
        }
        else
        {
          pos += 2;

          entry_size = 2;
        }

        // load data
        binary_info.Buffer = new byte[binary_length];

        Array.Copy(in_buffer, pos, binary_info.Buffer, 0, binary_length);

        // update pointer
        pos += binary_length;

        // update size
        entry_size += binary_length;
        binary_info.EntrySize = entry_size;

        // add to the array
        m_binary_entries.Add(binary_info);
      }

      // load data
      m_offset = in_offset;

      return true;
    }
    #endregion

    #region · Chunk GUI handling ·

		/// <summary>
		/// Adds chunk to the tree control
		/// </summary>
		/// <param name="in_treeview">Tree control</param>
		/// <param name="in_chunk_index">Index of the chunk within the file</param>
    public override void AddToTreeControl(TreeView in_treeview, int in_chunk_index)
    {
      UInt32 offset = 0;
      string name;

      // add binary main node
      TreeNode binary_chunk_node = in_treeview.Nodes.Add(in_chunk_index.ToString(), "Binary (0x" + m_offset.ToString("X8") + ")", TreeIconID, TreeIconID);

      // add binary buffers
      for (int binary_index = 0; binary_index < m_binary_entries.Count; binary_index++)
      {
        // get name
        name = m_binary_entries[binary_index].ReferenceName;
        if (name == null)
          name = "Binary #" + (binary_index + 1).ToString();

        // add class node
        binary_chunk_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (binary_index + 1).ToString(), name + " (0x" + offset.ToString("X8") + ")", TreeIconID, TreeIconID);

        offset += m_binary_entries[binary_index].EntrySize;
      }
    }

    /// <summary>
    /// Gets display panels
    /// </summary>
    /// <param name="in_path">Path of the display panel</param>
    /// <returns>Panel to display</returns>
    public override System.Windows.Forms.Panel GetDisplayPanel(string in_path)
    {
      m_display_form = new frmBinary();

      return m_display_form.FillControls(m_binary_entries, in_path);
    }

    /// <summary>
    /// Releases current display panel/form
    /// </summary>
    public override void ReleaseDisplayForm()
    {
      if (m_display_form != null)
        m_display_form.Dispose();

      m_display_form = null;
    }
    #endregion
  }
}

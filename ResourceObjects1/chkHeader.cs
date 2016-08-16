/*****************************************************************************/
/* Resource file header handling                                             */
/*                                                                           */
/* Copyright (C) 2013 Laszlo Arvai                                           */
/* All rights reserved.                                                      */
/*                                                                           */
/* This software may be modified and distributed under the terms             */
/* of the BSD license.  See the LICENSE file for details.                    */
/*****************************************************************************/
using System;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Handles resource file header chunk
	/// </summary>
  public class chkHeader : chkChunkBase
	{
		#region · Data members ·
		private byte[] m_buffer;	/// Header binary buffer
    int m_offset;							/// Offset of the file header (should be zero)
    frmHeader m_display_form;	/// Form used to display header data
		#endregion

		#region · Constructor ·

		/// Default constructor
		public chkHeader()
    {
      m_display_form = null;
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
      // load data
      m_buffer = in_buffer;
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
      in_treeview.Nodes.Add(in_chunk_index.ToString(), "Header (0x" + m_offset.ToString("X8") + ")", 0, 0);
    }

		/// <summary>
		/// Gets display panels
		/// </summary>
		/// <param name="in_path">Path of the display panel</param>
		/// <returns>Panel to display</returns>
		public override System.Windows.Forms.Panel GetDisplayPanel(string in_path)
    {
      m_display_form = new frmHeader();

      return m_display_form.FillControls(m_buffer );
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

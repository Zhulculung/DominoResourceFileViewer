/*****************************************************************************/
/* Unknown chunk handling class                                              */
/*                                                                           */
/* Copyright (C) 2013 Laszlo Arvai                                           */
/* All rights reserved.                                                      */
/*                                                                           */
/* This software may be modified and distributed under the terms             */
/* of the BSD license.  See the LICENSE file for details.                    */
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Handles unknown chunk in the resource file
	/// </summary>
  public class chkUnknown : chkChunkBase
	{
		#region · Data members ·
		private byte[] m_buffer;						/// Binary buffe rof the chunk
    private int m_offset;								/// Offset oif the chunk
    private frmUnknown m_display_form;	/// Form used to display chunk data
    private UInt32 m_chunk_id;					/// ID of the chunk
		#endregion

		#region · Constructor ·
		/// Default constructor
		public chkUnknown()
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
      m_chunk_id = in_chunk_id;

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
      in_treeview.Nodes.Add(in_chunk_index.ToString(), "Unknown (0x" + m_offset.ToString("X8") + ")", 1, 1);
    }

    /// <summary>
    /// Gets display panels
    /// </summary>
    /// <param name="in_path">Path of the display panel</param>
    /// <returns>Panel to display</returns>
    public override System.Windows.Forms.Panel GetDisplayPanel(string in_path)
    {
      m_display_form = new frmUnknown();

      m_display_form.DisplayChunkID(m_chunk_id);

      return m_display_form.FillControls( m_buffer );
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

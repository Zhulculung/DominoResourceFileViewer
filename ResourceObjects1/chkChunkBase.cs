/*****************************************************************************/
/* Base class for chunk handling classes                                     */
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
using System.IO;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Base class for chunk handling classes
	/// </summary>
  public abstract class chkChunkBase
  {
		protected chkCDecl m_declaration_parser;
		
		public void SetDeclarationParser(chkCDecl in_parser)
		{
			m_declaration_parser = in_parser;
		}
		
    abstract public bool Load(UInt32 in_chunk_id, byte[] in_buffer, int in_offset);
    abstract public Panel GetDisplayPanel(string in_path);
    abstract public void ReleaseDisplayForm();
    abstract public void AddToTreeControl(TreeView in_treeview, int in_chunk_index);
  }
}

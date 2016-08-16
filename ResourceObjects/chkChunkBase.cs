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
// Base class for chunk handling classes
///////////////////////////////////////////////////////////////////////////////
using System;
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

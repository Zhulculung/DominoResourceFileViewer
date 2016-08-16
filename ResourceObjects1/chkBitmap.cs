/*****************************************************************************/
/* Bitmap chunk handling class                                               */
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
using System.Drawing;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Handles Bitmap chunk
	/// </summary>
  public class chkBitmap : chkChunkBase
	{
		#region · Types ·

		/// <summary>
		/// Data of the bitmapo chunk entry (one bitmap)
		/// </summary>
		public class BitmapInfo
    {
      public int Width;
      public int Height;
      public int BitPerPixel;
      public Bitmap DisplayBitmap;
      public string ReferenceName;

      public int GetBitmapBinarySize()
      {
        return (Width * BitPerPixel + 7) / 8 * Height + sizeof(UInt16) + sizeof(UInt16) + sizeof(byte);
      }
    }
		#endregion

		#region · Data members ·
		List<BitmapInfo> m_bitmap_info;		/// List of the entries of the chunk
    frmBitmap m_display_form;					/// Form used to display chunk data
    int m_offset;											/// Offset of the chun kwithin the file
		#endregion

		#region · Constructor ·
		/// Default constructor
		public chkBitmap()
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
      BitmapInfo bitmap_info;
      int pos = 0;
      int bitmap_pos;

      m_offset = in_offset;

      // init array
      m_bitmap_info = new List<BitmapInfo>();

      // load data
      while (pos < in_buffer.Length)
      {
        // create new bitmapinfo
        bitmap_info = new BitmapInfo();
        bitmap_pos = pos;

				// get reference id
				bitmap_info.ReferenceName = m_declaration_parser.GetReferenceName(in_chunk_id, (UInt32)in_offset, (UInt32)bitmap_pos);

        // load width
        bitmap_info.Width = BitConverter.ToUInt16(in_buffer, pos);
        pos += sizeof(UInt16);

        // load height
        bitmap_info.Height = BitConverter.ToUInt16(in_buffer, pos);
        pos += sizeof(UInt16);

        // load bits per pixel
        bitmap_info.BitPerPixel = in_buffer[pos++];

        // create bitmap
        bitmap_info.DisplayBitmap = new Bitmap(bitmap_info.Width, bitmap_info.Height);

				switch (bitmap_info.BitPerPixel)
				{
					// bw bitmap handling
					case 1:
						{
							int line_start = pos;

							for (int y = 0; y < bitmap_info.Height; y++)
							{
								for (int x = 0; x < bitmap_info.Width; x++)
								{
									if ((in_buffer[line_start + x / 8] & (1 << (7 - (x % 8)))) != 0)
										bitmap_info.DisplayBitmap.SetPixel(x, y, Color.Black);
									else
										bitmap_info.DisplayBitmap.SetPixel(x, y, Color.White);
								}

								line_start += (bitmap_info.Width + 7) / 8;
							}
						}
						break;

					// rgb565 bitmap handling
					case 16:
						{
							int line_start = pos;
							int rgb;
							int r,g,b;

							for (int y = 0; y < bitmap_info.Height; y++)
							{
								for (int x = 0; x < bitmap_info.Width; x++)
								{
									rgb = in_buffer[line_start + x * sizeof(UInt16)] + ((in_buffer[line_start + x * sizeof(UInt16) + 1]) << 8);
									r = (rgb >> 8) & 0xf8;
									g = (rgb >> 3) & 0xfc;
									b = (rgb << 3) & 0xf8;

									bitmap_info.DisplayBitmap.SetPixel(x, y, Color.FromArgb(r,g,b) );
								}

								line_start += bitmap_info.Width * sizeof(UInt16);
							}
						}
						break;
				}

        // update pointer
        pos = bitmap_pos + bitmap_info.GetBitmapBinarySize();

        // add to the array
        m_bitmap_info.Add(bitmap_info);
      }

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
      int offset = 0;
			string name;
 
      // add bitmap main node
      TreeNode bitmap_chunk_node = in_treeview.Nodes.Add(in_chunk_index.ToString(), "Bitmap (0x" + m_offset.ToString("X8") + ")", 4, 4);

      // add bitmaps
      for (int bitmap_index = 0; bitmap_index < m_bitmap_info.Count; bitmap_index++)
      {
				// get name
				name = m_bitmap_info[bitmap_index].ReferenceName;
				if (name == null)
					name = "Bitmap #" + (bitmap_index + 1).ToString();
				
				// add class node
        bitmap_chunk_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (bitmap_index + 1).ToString(), name + " (0x" + offset.ToString("X8") + ")", 4, 4);

        offset += m_bitmap_info[bitmap_index].GetBitmapBinarySize();
      }
    }

		/// <summary>
		/// Gets display panels
		/// </summary>
		/// <param name="in_path"></param>
		/// <returns></returns>
    public override Panel GetDisplayPanel(string in_path)
    {
      m_display_form = new frmBitmap();

      m_display_form.ParentClass = this;

      return m_display_form.FillControls(m_bitmap_info, in_path);
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

/*****************************************************************************/
/* Loads file loader class                                                   */
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
	/// Resource file loader
	/// </summary>
  public class fileResourceLoader
	{
		#region · Data member ·
		private List<chkChunkBase> m_chunks;		/// Array of chunks
		#endregion

		/// <summary>
    /// Gets chunks of the resource file
    /// </summary>
    public List<chkChunkBase> Chunks
    {
      get { return m_chunks; }
    }

    /// <summary>
    /// Clears all chunks
    /// </summary>
    public void Clear()
    {
      m_chunks = null;
    }

		#region · Load function ·
		/// <summary>
    /// Process a resource file, loads chunks from it
    /// </summary>
    /// <param name="in_resource_file">Resource file in a binary buffer</param>
    /// <param name="in_declaration_parser">Declaration parser</param>
    /// <param name="in_chunk_tree">Main Treeview</param>
    /// <returns></returns>
    public bool ProcessResourceFile( byte[] in_resource_file, chkCDecl in_declaration_parser, TreeView in_chunk_tree)
    {
      // try to load file
      bool success = true;
      byte[] buffer;
      byte[] chunk_buffer;
      int chunk_count;
      UInt32 chunk_id;
      UInt32 chunk_pos;
      UInt32 file_length;
      UInt32 chunk_length;

			// init
			m_chunks = new List<chkChunkBase>();

      // check header
      if (in_resource_file[0] == 0x44 || in_resource_file[1] == 0x52)
      {
        chkHeader header = new chkHeader();

        buffer = new byte[12];
        Array.Copy(in_resource_file, buffer, 12);

        if (header.Load(0, buffer, 0))
        {
          m_chunks.Add(header);

          header.AddToTreeControl(in_chunk_tree, m_chunks.Count - 1);
        }

        // load chunks
        file_length = BitConverter.ToUInt32(buffer, 6);
        chunk_count = BitConverter.ToUInt16(buffer, 10);
        buffer = new byte[8 * chunk_count];
        Array.Copy(in_resource_file, 12, buffer, 0, buffer.Length);

        for (int chunk_index = 0; chunk_index < chunk_count; chunk_index++)
        {
          // get chunk id and pis
          chunk_id = BitConverter.ToUInt32(buffer, chunk_index * 8);
          chunk_pos = BitConverter.ToUInt32(buffer, chunk_index * 8 + 4);

          // calculate chunk length
          if (chunk_index + 1 == chunk_count)
          {
            chunk_length = file_length - chunk_pos;
          }
          else
          {
            chunk_length = BitConverter.ToUInt32(buffer, (chunk_index + 1) * 8 + 4) - chunk_pos;
          }

          // get chunk data
          chunk_buffer = new byte[chunk_length];
          Array.Copy(in_resource_file, chunk_pos, chunk_buffer, 0, chunk_buffer.Length);

          chkChunkBase current_chunk;

          switch (chunk_id)
          {
            // string chunk
            case 0x47525453:
              current_chunk = new chkString();
              break;

            // Bitmaps chunk
            case 0x53504D42:
              current_chunk = new chkBitmap();
              break;

            // Binary chunk
            case 0x414E4942:
              current_chunk= new chkBinary();
              break;

            // Wave chunk
            case 0x45564157:
              current_chunk = new chkWave();
              break;

            // Font chunk
            case 0x544e4f46: 
              current_chunk = new chkFont();
              break;

            // Java chunk
            case 0x534C434A:
              current_chunk = new chkJava();
              break;

            // unknown chunk
            default:
              current_chunk = new chkUnknown();
              break;
          }

          // prepare chunk
          current_chunk.SetDeclarationParser(in_declaration_parser);

          if (current_chunk.Load(chunk_id, chunk_buffer, (int)chunk_pos))
          {
            m_chunks.Add(current_chunk);

            current_chunk.AddToTreeControl(in_chunk_tree, m_chunks.Count - 1);
          }
        }
      }
      else
        success = false;

      return success;
    }
		#endregion
  }
}

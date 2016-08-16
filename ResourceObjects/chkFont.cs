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
// Font chunk handling class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Handles front chunk
	/// </summary>
	public class chkFont : chkChunkBase
	{
		#region · Constants ·
		const int drcFF_DONTCARE = 1;     // Font family not known or not important.
    const int drcFF_ROMAN = 2;        // Proportionally spaced fonts with serifs.
    const int drcFF_SWISS = 3;        // Proportionally spaced fonts without serifs.
    const int drcFF_MODERN = 4;       // Fixed-pitch fonts
    const int drcFF_SCRIPT = 5;       // Script
    const int drcFF_DECORATIVE = 6;   // Decorative
    const int drfFF_FAMILY_MASK = 7;
    const int drcFF_FIXED = (1<<7);   // Set if the font is fixed
    const int drcFF_BOLD = (1<<6);    // Set if the font is bold
    const int drcFF_ITALIC = (1<<5);  // Set if the font is italic
    const int drcFF_INVALID = 0;      // Invalid family type
		#endregion

		#region · Types ·

		/// <summary>
		/// Character information
		/// </summary>
		public class CharacterInfoEntry
    {
      public int Width;
      public byte[] CharacterData;
      
      public int GetBinarySize()
      {
        return sizeof(byte) + CharacterData.Length;
      }
    }
    
		/// <summary>
		/// Font information
		/// </summary>
    public class FontInfo
    {
			public byte Type;
      public byte Flags;
      public byte Width;
      public byte Height;
      public byte BaseLine;
			public byte HorizontalCharacterGap;
      public byte MinAscii;
      public byte MaxAscii;
      public byte DefaultChar;
      public UInt16 UnicodeCharCount;
			public string ReferenceName;
			public Bitmap DisplayBitmap;
      
      public CharacterInfoEntry[] CharacterInfo;

      public int GetFontBinarySize()
      {
        int character_data_length = 0;
        
        for( int i = MinAscii; i <= MaxAscii;i++ )
          character_data_length += CharacterInfo[i-MinAscii].GetBinarySize();
        
        return 	sizeof(byte) +  // type
								sizeof(byte) +  // Flags
                sizeof(byte) +  // Width
                sizeof(byte) +  // Height
                sizeof(byte) +  // BaseLine
								sizeof(byte) +  // Horizontal character gap
                sizeof(byte) +  // MinASCII
                sizeof(byte) +  // MaxASCII
                sizeof(byte) +  // DefaultChar
                sizeof(UInt16) +  // UnicodeCharCount
                sizeof(UInt16) * (MaxAscii-MinAscii+1) +
                character_data_length;
      }
    }
		#endregion

		#region · Data members ·
		private List<FontInfo> m_font_info;			/// Font entry collection
    private frmFont m_display_form;					/// Form used for chunk data display
    int m_offset;														/// Ofset of the chunk within the file
		#endregion

		#region · Constructor ·

		/// Default constructor
		public chkFont()
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
      FontInfo font_info;
      int pos = 0;
      int font_pos;
      int character_table_pos;
      int character_data_pos;
      int character_data_length;

      m_offset = in_offset;
      
      // init array
			m_font_info = new List<FontInfo>();

      // load data
      while (pos < in_buffer.Length)
      {
        // create new fontinfo
        font_info = new FontInfo();
        font_pos = pos;
        
	      // get reference id
				font_info.ReferenceName = m_declaration_parser.GetReferenceName(in_chunk_id, (UInt32)in_offset, (UInt32)font_pos);

				// load type
				font_info.Type = in_buffer[pos];
				pos += sizeof(byte);
				
				// load flags
        font_info.Flags = in_buffer[pos];
        pos += sizeof(byte);
      
        // load width        
        font_info.Width = in_buffer[pos];
        pos += sizeof(byte);

        // load height
        font_info.Height = in_buffer[pos];
        pos += sizeof(byte);

        // load baseline
        font_info.BaseLine = in_buffer[pos];
        pos += sizeof(byte);

				// load horizontal character gap
				font_info.HorizontalCharacterGap = in_buffer[pos];
				pos += sizeof(byte);

        // load min ascii
        font_info.MinAscii = in_buffer[pos];
        pos += sizeof(byte);
        
        // load max ascii
        font_info.MaxAscii = in_buffer[pos];
        pos += sizeof(byte);
        
        // load default character
        font_info.DefaultChar = in_buffer[pos];
        pos += sizeof(byte);
        
        // load unicode character count
        font_info.UnicodeCharCount = BitConverter.ToUInt16(in_buffer, pos );
        pos += sizeof(UInt16);
        
        character_table_pos = pos;
        
        // allocate character data
        font_info.CharacterInfo = new CharacterInfoEntry[font_info.MaxAscii-font_info.MinAscii+1];
        
        // load character data
        for( int i = font_info.MinAscii; i <= font_info.MaxAscii;i++)
        {
          font_info.CharacterInfo[i-font_info.MinAscii] = new CharacterInfoEntry();
          
          character_data_pos = BitConverter.ToUInt16(in_buffer, character_table_pos + sizeof(UInt16) * (i-font_info.MinAscii)) + font_pos;
          
          font_info.CharacterInfo[i-font_info.MinAscii].Width = in_buffer[character_data_pos];
          character_data_pos += sizeof(byte);

          character_data_length = ((font_info.CharacterInfo[i - font_info.MinAscii].Width + 7) / 8) * font_info.Height;
          font_info.CharacterInfo[i-font_info.MinAscii].CharacterData = new byte[character_data_length];
          
          Array.Copy( in_buffer, character_data_pos, font_info.CharacterInfo[i-font_info.MinAscii].CharacterData, 0, character_data_length );
        }

        // update pointer
        pos = font_pos + font_info.GetFontBinarySize();

				// calculate preview bitmap size
				int character_count = 0;
				int bitmap_width = 0;
				int bitmap_height = ((font_info.MaxAscii - font_info.MinAscii) / 16 + 1) * font_info.Height;
				int width = 0;
				for (int ascii = font_info.MinAscii; ascii <= font_info.MaxAscii; ascii++)
				{
					width += font_info.CharacterInfo[ascii - font_info.MinAscii].Width;
					character_count++;

					if (character_count == 16)
					{
						character_count = 0;
						if (width > bitmap_width)
							bitmap_width = width;

						width = 0;
					}
				}

				// create and clear bitmap
				font_info.DisplayBitmap = new Bitmap(bitmap_width, bitmap_height);
				for (int y = 0; y < bitmap_height; y++)
					for (int x = 0; x < bitmap_width; x++)
						font_info.DisplayBitmap.SetPixel(x, y, Color.White);

				// generate preview bitmap
				int character_x = 0;
				int character_y = 0;
				int line_start = 0;
				character_count = 0;
				for (int ascii = font_info.MinAscii; ascii <= font_info.MaxAscii; ascii++)
				{
					line_start = 0; 
					for (int y = 0; y < font_info.Height; y++)
					{
						for (int x = 0; x < font_info.CharacterInfo[ascii - font_info.MinAscii].Width; x++)
						{
							if ((font_info.CharacterInfo[ascii - font_info.MinAscii].CharacterData[line_start + x / 8] & (1 << (7 - (x % 8)))) != 0)
								font_info.DisplayBitmap.SetPixel(character_x + x, character_y + y, Color.Black);
							else
								font_info.DisplayBitmap.SetPixel(character_x + x, character_y + y, Color.White);
						}

						line_start += (font_info.CharacterInfo[ascii - font_info.MinAscii].Width + 7) / 8;
					}

					character_x += font_info.CharacterInfo[ascii - font_info.MinAscii].Width;
					character_count++;

					if (character_count == 16)
					{
						character_count = 0;
						character_x = 0;
						character_y += font_info.Height;
					}
				}

        // add to the array
				m_font_info.Add(font_info);
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
      
      // add font main node
      TreeNode font_chunk_node = in_treeview.Nodes.Add(in_chunk_index.ToString(), "Font (0x" + m_offset.ToString("X8") + ")", 4, 4);

      // add fonts
      for (int font_index = 0; font_index < m_font_info.Count; font_index++)
      {
				// get name
				name = m_font_info[font_index].ReferenceName;
				if(name == null)
					name = "Font #" + (font_index + 1).ToString();
				
        // add class node
        font_chunk_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (font_index + 1).ToString(), name + " (0x" + offset.ToString("X8") + ")", 4, 4);

        offset += m_font_info[font_index].GetFontBinarySize();
      }
    }

		/// <summary>
		/// Gets display panels
		/// </summary>
		/// <param name="in_path"></param>
		/// <returns></returns>
    public override Panel GetDisplayPanel(string in_path)
    {
      m_display_form = new frmFont();

      m_display_form.ParentClass = this;

      return m_display_form.FillControls(m_font_info, in_path);
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
    
		/// <summary>
		/// Converts font flags to string
		/// </summary>
		/// <param name="in_flags"></param>
		/// <returns></returns>
    public string FlagsToString(byte in_flags)
    {
			string name = "";
			string[] family = { "FF_DONTCARE", "FF_ROMAN", "FF_SWISS", "FF_MODERN", "FF_SCRIPT", "FF_DECORATIVE" };
			
			// family name
			name = family[in_flags & drfFF_FAMILY_MASK];

			// flags			
			if( (in_flags & drcFF_FIXED) != 0 )
				name += "| FF_FIXED";

			if ((in_flags & drcFF_BOLD) != 0)
				name += "| FF_BOLD";

			if ((in_flags & drcFF_ITALIC) != 0)
				name += "| FF_ITALIC";

			return name;
		}
		#endregion
	}
}

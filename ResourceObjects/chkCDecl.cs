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
// Load resource file using informaion stored in C declaration (header) file
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;

namespace DominoResourceFileViewer
{
	public class chkCDecl
  {
    #region · Types ·
		/// <summary>
		/// Reference Declaration Information
		/// </summary>
    public class ReferenceDeclaration
    {
      public string Name;
      public UInt32 ChunkID;
      public UInt32 Reference;

      public override bool Equals(Object in_object)
      {
        if (in_object == null || GetType() != in_object.GetType())
          return false;

        ReferenceDeclaration p = (ReferenceDeclaration)in_object;
        return (ChunkID == p.ChunkID) && (Reference == p.Reference);
      }

      public override int GetHashCode()
      {
        return (int)(ChunkID ^ Reference);
      }
    }
    #endregion

    #region · Data Members ·
    List<ReferenceDeclaration> m_references;			/// Collection of references
    bool m_absolute_mode;													/// True if references are absoute
    string m_output_file;													/// Resource file name
    #endregion

    #region · Constructor & Destructor ·

		/// Default constructor
    public chkCDecl()
    {
      m_absolute_mode = true;
      m_output_file="";
    }
    #endregion

		#region · Load declaration file ·

		/// <summary>
		/// Loads declaration file
		/// </summary>
		/// <param name="in_name"></param>
		/// <returns></returns>
		public bool Load(string in_name)
    {
      string[] info_name =
	    {
		    "addressmode", "chunkid", "outputfile"
	    };

      bool success = true;
      string[] buffer;
      UInt32 current_chunk_id = 0;

      m_references = new List<ReferenceDeclaration>();

      try
      {
        StreamReader header_file = File.OpenText(in_name);
        string input = null;
        while ((input = header_file.ReadLine()) != null)
        {
          input = input.Trim();

          // skip empty lines
          if (input.Length > 0)
          {
            // check for comment information
            if (input.StartsWith("//#"))
            {
              input = input.Remove(0, 3);

              string name;
              string value;
              int pos = input.IndexOf(':');

              // separate parameter name and value
              if (pos != -1)
              {
                name = input.Substring(0, pos);
                value = input.Substring(pos + 1, input.Length - pos - 1);
              }
              else
              {
                name = input;
                value = "";
              }

              // find comment info name
              int info_index = Array.IndexOf(info_name, name);

              // process comment information
              switch (info_index)
              {
                // addressmode
                case 0:
                  if (value == "absolute")
                    m_absolute_mode = true;
                  else
                    m_absolute_mode = false;
                  break;

                // chunkid
                case 1:
                  if (value.ToLower().StartsWith("0x"))
                  {
                    int end_pos = value.IndexOf(' ');
                    if (end_pos < 0)
                      end_pos = value.Length;

                    UInt32.TryParse(value.Substring(2, end_pos - 2), System.Globalization.NumberStyles.AllowHexSpecifier, null, out current_chunk_id);
                  }
                  break;

                // outputfile
                case 2:
                  m_output_file = value;
                  break;
              }
            }
            else
            {
              // check for define
              if (input.StartsWith("#define"))
              {
                buffer = input.Split(' ');

                if (buffer.Length == 3)
                {
                  // new reference found
                  if (buffer[1].StartsWith("REF_"))
                  {
                    UInt32 value;

                    if (UInt32.TryParse(buffer[2], out value))
                    {
                      ReferenceDeclaration new_reference = new ReferenceDeclaration();

                      new_reference.Name = buffer[1].Trim();
                      new_reference.Reference = value;
                      new_reference.ChunkID = current_chunk_id;

                      // add to reference list
                      m_references.Add(new_reference);
                    }
                  }
                }
              }
            }
          }
        }

        header_file.Close();
      }
      catch
      {
        success = false;
      }
      return success;
    }
		#endregion

		#region · Reference handling ·
		/// <summary>
		/// Get reference name from the chunk id and entry pos
		/// </summary>
		/// <param name="in_chunk_id"></param>
		/// <param name="in_chunk_pos"></param>
		/// <param name="in_reference"></param>
		/// <returns></returns>
		public string GetReferenceName(UInt32 in_chunk_id, UInt32 in_chunk_pos, UInt32 in_reference)
    {
      UInt32 pos;

      // calculate reference value
      if (m_absolute_mode)
				pos = in_chunk_pos + in_reference;
      else
				pos = in_reference;

      // lookup for name
      ReferenceDeclaration decl = new ReferenceDeclaration();
      decl.ChunkID = in_chunk_id;
      decl.Reference = pos;

      int index = m_references.IndexOf(decl);

      // if found return name else return empty string
      if (index != -1)
        return m_references[index].Name;
      else
        return null;
    }
		#endregion

		/// <summary>
		/// Gets resource file name
		/// </summary>
		/// <returns></returns>
		public string GetOutputFile()
    {
			return m_output_file;
    }
  }
}

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
// Wave chunk handling class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Wave chunk handling class
	/// </summary>
	public class chkWave : chkChunkBase
  {
    #region · Constants ·
    const int TreeIconID = 5;
    #endregion

    #region · Types ·
		/// <summary>
		/// Wave format information
		/// </summary>
    public class WaveInfo
    {
      // Wave format constants
      public const byte WFFormatLengthMask = 0x03;
      public const byte WFMono = 0;
      public const byte WFStereo = (1 << 2);
      public const byte WFMonoStereoMask = (1 << 2);
      public const byte WF8bit = 0;
      public const byte WF16bit = (1 << 3);
      public const byte WF8bit16bitMask = (1 << 3);
      public const byte WF8000Hz = 0x00;
      public const byte WF11025Hz = 0x10;
      public const byte WF22050Hz = 0x20;
      public const byte WF44100Hz = 0x30;
      public const byte WFCustomSampleRate = 0xf0;
      public const byte WFSampleRateMask = 0xf0;

      public byte Format;
      public UInt16 SampleRate;
      public UInt32 NumberOfSamples;
      public Int16[] Samples;
      public string ReferenceName;

      public int GetWaveBinarySize()
      {
        int size;

        size = Format & WFFormatLengthMask;

        size += (int)(NumberOfSamples * (((Format & WFMonoStereoMask) == WFStereo) ? 2 : 1) * (((Format & WF8bit16bitMask) == WF16bit) ? 2 : 1));

        return size;
      }

      /// <summary>
      /// Number of channels
      /// </summary>
      public int NumberOfChannels
      {
        get
        {
          return ((Format & WFMonoStereoMask) == WFStereo) ? 2 : 1;
        }
      }

      /// <summary>
      /// Sample resolution in bits
      /// </summary>
      public int SampleResolution
      {
        get
        {
          return ((Format & WF8bit16bitMask) == WF16bit) ? 2 : 1;
        }
      }


      /// <summary>
      /// Converts wave format to human readableinformation
      /// </summary>
      /// <returns></returns>
      public string FormatToString()
      {
        string format_string = "";

        // sample rate
        format_string = SampleRate.ToString() + "Hz";

        // sample width
        if ((Format & WF8bit16bitMask) == WF16bit)
        {
          format_string += ", 16bit";
        }
        else
        {
          format_string += ", 8bit";
        }

        // stereo/mono
        if ((Format & WFMonoStereoMask) == WFStereo)
        {
          format_string += ", stereo";
        }
        else
        {
          format_string += ", mono";
        }

        return format_string;
      }
    }
    #endregion

    #region · Data members ·
    private List<WaveInfo> m_wave_info;		/// Array of the wave chunk entries
    private frmWave m_display_form;				/// Form used to display wave data
    private int m_offset;									/// Offset of the chunk within the resource file
    #endregion

    #region · Constructor ·
    /// Default constructor
		public chkWave()
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
      WaveInfo wave_info;
      int pos = 0;
      int wave_pos;

      m_offset = in_offset;

      // init array
      m_wave_info = new List<WaveInfo>();

      // load data
      while (pos < in_buffer.Length)
      {
        // create new wave info
        wave_info = new WaveInfo();
        wave_pos = pos;

        // get reference id
        wave_info.ReferenceName = m_declaration_parser.GetReferenceName(in_chunk_id, (UInt32)in_offset, (UInt32)wave_pos);

        // load format
        wave_info.Format = in_buffer[pos];

        switch (wave_info.Format & WaveInfo.WFSampleRateMask)
        {
          case WaveInfo.WF8000Hz:
            wave_info.SampleRate = 8000;
            break;

          case WaveInfo.WF11025Hz:
            wave_info.SampleRate = 11025;
            break;

          case WaveInfo.WF22050Hz:
            wave_info.SampleRate = 22050;
            break;

          case WaveInfo.WF44100Hz:
            wave_info.SampleRate = 44100;
            break;

          case WaveInfo.WFCustomSampleRate:
            wave_info.SampleRate = (UInt16)(in_buffer[pos + 1] + 256 * in_buffer[pos + 2]);
            break;
        }

        pos += wave_info.Format & WaveInfo.WFFormatLengthMask;

        // load number of samples 
        wave_info.NumberOfSamples = BitConverter.ToUInt32(in_buffer, pos);
        pos += sizeof(UInt32);

        // load samples
        wave_info.Samples = new Int16[wave_info.NumberOfSamples];

        for (UInt32 index = 0; index < wave_info.NumberOfSamples; index++)
        {
          wave_info.Samples[index] = (Int16)(in_buffer[pos] - 128);

          wave_info.Samples[index] *= 256;

          pos++;
        }

        // update pointer
        //pos = wave_pos + wave_info.GetWaveBinarySize();

        // add to the array
        m_wave_info.Add(wave_info);
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

      // add wave main node
      TreeNode wave_chunk_node = in_treeview.Nodes.Add(in_chunk_index.ToString(), "Wave (0x" + m_offset.ToString("X8") + ")", TreeIconID, TreeIconID);

      // add waves
      for (int wave_index = 0; wave_index < m_wave_info.Count; wave_index++)
      {
        // get name
        name = m_wave_info[wave_index].ReferenceName;
        if (name == null)
          name = "Wave #" + (wave_index + 1).ToString();

        // add class node
        wave_chunk_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (wave_index + 1).ToString(), name + " (0x" + offset.ToString("X8") + ")", TreeIconID, TreeIconID);

        offset += m_wave_info[wave_index].GetWaveBinarySize();
      }
    }

		/// <summary>
		/// Gets display panels
		/// </summary>
		/// <param name="in_path">Path of the display panel</param>
		/// <returns>Panel to display</returns>
		public override Panel GetDisplayPanel(string in_path)
    {
      m_display_form = new frmWave();

      m_display_form.ParentClass = this;

      return m_display_form.FillControls(m_wave_info, in_path);
    }

		/// <summary>
		/// Releases current display panel/form
		/// </summary>
		public override void ReleaseDisplayForm()
    {
      if (m_display_form != null)
      {
        m_display_form.ReleaseResources();
        m_display_form.Close();
        m_display_form.Dispose();
      }

      m_display_form = null;
    }
    #endregion
  }
}

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
// Wave chunk display class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmWave : Form
  {
    #region · Data members ·
    public chkWave ParentClass;
    private chkWave.WaveInfo m_wave_info;
    private SoundPlayer m_player = null;
    #endregion

    #region · Constructor&Destructor ·

    /// <summary>
    /// Default constructor
    /// </summary>
    public frmWave()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Release unmanaged resources
    /// </summary>
    /// <param name="disposing"></param>
    public void ReleaseResources()
    {
      // release resources
      if (m_player != null)
      {
        m_player.Stop();

        m_player.Stream.Close();
        m_player.Stream.Dispose();

        m_player.Dispose();
        m_player = null;
      }
    }

    #endregion

    #region · Controll filler function ·

    /// <summary>
    /// Fill panel  controls
    /// </summary>
    /// <param name="in_waves"></param>
    /// <param name="in_path"></param>
    /// <returns></returns>
    public Panel FillControls(List<chkWave.WaveInfo> in_waves, string in_path)
    {
      string[] split;
      int size;

      split = in_path.Split('\\');

      switch (split.Length)
      {
        // main WAVE info
        case 1:
          lNumberOfWave.Text = in_waves.Count.ToString();

          size = 0;
          for (int i = 0; i < in_waves.Count; i++)
          {
            size += in_waves[i].GetWaveBinarySize();
          }

          lChunkSize.Text = size.ToString();

          return pWaveGeneral;

        // wave info
        case 2:
          {
            int wave_index;

            if (int.TryParse(split[1], out wave_index))
            {
              // store wave info
              m_wave_info = in_waves[wave_index - 1];

              // wave number
              lWaveNumber.Text = lWaveNumber.Text + wave_index.ToString() + " '" + in_waves[wave_index - 1].ReferenceName + "'";

              // number of samples
              lNumberOfSamples.Text = m_wave_info.NumberOfSamples.ToString();

              // format
              lWaveFormat.Text = m_wave_info.FormatToString();

              // generate wave bitmap and wave player
              UpdateWaveBitmap();
              UpdateWavePlayer();

              return pWaveDisplay;
            }
          }
          break;
      }

      return null;
    }
    #endregion

    #region · Helper functions ·

    /// <summary>
    /// Updates bitmap showing the waveform
    /// </summary>
    private void UpdateWaveBitmap()
    {
      int width = pbWaveDisplay.ClientRectangle.Width;
      int height = pbWaveDisplay.ClientRectangle.Height;
      int wave_pos;

      // wave pos
      if (sbWavePos.Visible)
      {
        wave_pos = sbWavePos.Value;
      }
      else
        wave_pos = 0;

      Bitmap image = new Bitmap(width, height);
      Graphics g = Graphics.FromImage(image);

      // background
      g.FillRectangle(Brushes.White, 0, 0, width, height);

      // grid
      Pen grid_pen = new Pen(Color.LightGray);
      g.DrawLine(grid_pen, 0, height / 2, width, height / 2);

      // waveform
      Pen wave_pen = new Pen(Color.Green);
      int prev_y = m_wave_info.Samples[wave_pos] * height / 32768 / 2 + height / 2;
      int y;

      for (int x = 1; x < width; x++)
      {
        // draw grid
        if (((x + wave_pos) % 100) == 0)
        {
          g.DrawLine(grid_pen, x, 0, x, height);
        }

        // draw wave data
        if (wave_pos + x < m_wave_info.Samples.Length)
        {
          y = m_wave_info.Samples[wave_pos + x] * height / 32768 / 2 + height / 2;

          g.DrawLine(wave_pen, x - 1, prev_y, x, y);

          prev_y = y;
        }
      }

      // free resources
      grid_pen.Dispose();
      wave_pen.Dispose();
      g.Dispose();

      // update bitmap
      pbWaveDisplay.Image = image;
    }

    /// <summary>
    /// Updates internal memory file contains playable wave file
    /// </summary>
    private void UpdateWavePlayer()
    {
      // release resources
      ReleaseResources();

      // generate header
      string header_GroupID = "RIFF";  // RIFF
      uint header_FileLength = 0;      // total file length minus 8, which is taken up by RIFF
      string header_RiffType = "WAVE"; // always WAVE

      string fmt_ChunkID = "fmt ";                                      // Four bytes: "fmt "
      uint fmt_ChunkSize = 16;                                          // Length of header in bytes
      ushort fmt_FormatTag = 1;                                         // 1 for PCM
      ushort fmt_Channels = (ushort)m_wave_info.NumberOfChannels;       // Number of channels, 2=stereo
      uint fmt_SamplesPerSec = m_wave_info.SampleRate;                  // sample rate, e.g. CD=44100
      ushort fmt_BitsPerSample = (ushort)(m_wave_info.SampleResolution * 8);// Bits per sample
      ushort fmt_BlockAlign = (ushort)(fmt_Channels * (fmt_BitsPerSample / 8)); // sample frame size, in bytes
      uint fmt_AvgBytesPerSec =
          fmt_SamplesPerSec * fmt_BlockAlign; // for estimating RAM allocation

      // data chunk
      string data_ChunkID = "data";  // "data"
      uint data_ChunkSize;           // Length of header in bytes
      byte[] data_ByteArray;

      // Fill the data array with sample data

      // Number of samples = sample rate * channels * bytes per sample * duration in seconds
      uint numSamples = m_wave_info.NumberOfSamples;
      data_ByteArray = new byte[numSamples];

      for (uint i = 0; i < numSamples; i++)
      {
        data_ByteArray[i] = Convert.ToByte(m_wave_info.Samples[i] / 256 + 128);
      }

      // Calculate file and data chunk size in bytes
      data_ChunkSize = (uint)(data_ByteArray.Length * (fmt_BitsPerSample / 8));
      header_FileLength = 4 + (8 + fmt_ChunkSize) + (8 + data_ChunkSize);

      // write data to a MemoryStream with BinaryWriter
      MemoryStream audioStream = new MemoryStream();
      BinaryWriter writer = new BinaryWriter(audioStream);

      // Write the header
      writer.Write(header_GroupID.ToCharArray());
      writer.Write(header_FileLength);
      writer.Write(header_RiffType.ToCharArray());

      // Write the format chunk
      writer.Write(fmt_ChunkID.ToCharArray());
      writer.Write(fmt_ChunkSize);
      writer.Write(fmt_FormatTag);
      writer.Write(fmt_Channels);
      writer.Write(fmt_SamplesPerSec);
      writer.Write(fmt_AvgBytesPerSec);
      writer.Write(fmt_BlockAlign);
      writer.Write(fmt_BitsPerSample);

      // Write the data chunk
      writer.Write(data_ChunkID.ToCharArray());
      writer.Write(data_ChunkSize);
      foreach (byte dataPoint in data_ByteArray)
      {
        writer.Write(dataPoint);
      }
      m_player = new SoundPlayer(audioStream);
    }
    #endregion

    #region · Event handlers ·

    /// <summary>
    /// Handles resize request of wave display form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void pbWaveDisplay_SizeChanged(object sender, EventArgs e)
    {
      // update scrollbar
      int max = (int)(m_wave_info.NumberOfSamples - pbWaveDisplay.ClientRectangle.Width);

      if (max > 0)
      {
        sbWavePos.Visible = true;
        sbWavePos.Maximum = max;
      }
      else
        sbWavePos.Visible = false;

      // update image
      UpdateWaveBitmap();
    }

    /// <summary>
    /// Handles wave display scrollbar event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void sbWavePos_Scroll(object sender, ScrollEventArgs e)
    {
      UpdateWaveBitmap();
    }

    /// <summary>
    /// Plays wave sample
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bPlay_Click(object sender, EventArgs e)
    {
      if (m_player != null)
      {
        m_player.Stream.Seek(0, SeekOrigin.Begin); // rewind stream
        m_player.Play();
      }
    }

    /// <summary>
    /// Stops waveform playing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bStop_Click(object sender, EventArgs e)
    {
      if (m_player != null)
      {
        m_player.Stop();
      }
    }

    /// <summary>
    /// Release resources
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void frmWave_FormClosed(object sender, FormClosedEventArgs e)
    {
      ReleaseResources();
    }
    #endregion
  }
}
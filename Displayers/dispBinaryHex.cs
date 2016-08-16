/*****************************************************************************/
/* Binary data to hex display converter                                     */
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

namespace DominoResourceFileViewer
{
  /// <summary>
  /// Displays binary buffer as bytes
  /// </summary>
  public class dispBinaryHex : IBinaryViewer
  {
    #region · Types ·

    /// <summary>
    /// Hex data display mode
    /// </summary>
    public enum DisplayMode
    {
      Byte,
      Word,
      DoubleWord
    }

    #endregion

    #region · Data members ·
    private DisplayMode m_display_mode;		/// Mode of the display (byte, word, double word)
    private int m_percentage_ready = 0;		/// Percentage ready of the list preparation
    #endregion

    #region · Constructor ·
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="in_mode"></param>
    public dispBinaryHex(DisplayMode in_mode)
    {
      m_display_mode = in_mode;
    }
    #endregion

    #region · Public functions ·

    /// <summary>
    /// Gets percentage ready of the conversion
    /// </summary>
    /// <returns></returns>
    public int GetPercentageReady()
    {
      return m_percentage_ready;
    }

    /// <summary>
    /// Convert buffer content to hex representation in RTF format
    /// </summary>
    /// <param name="in_buffer">Boffer to convert</param>
    /// <returns>RTF representation of the binary data</returns>
    public string ConvertToRTF(byte[] in_buffer)
    {
      // create hex view
      UInt16 address;
      string line_buffer = "";
      string document = "{\\rtf1\\ansi{\\fonttbl\\f0\\fs10\\fswiss Courier New;}\\f0\\fs20";

      for (address = 0; address < in_buffer.Length; address++)
      {
        if (address % 16 == 0)
        {
          // add this line to the list
          if (line_buffer.Length > 0)
            document += "\\pard" + line_buffer + "\\par";

          line_buffer = "";
        }

        // start new line
        if (line_buffer.Length == 0)
          line_buffer = "{\\b " + address.ToString("X4") + ":} ";

        switch(m_display_mode)
        {
          case DisplayMode.Byte:
            line_buffer += in_buffer[address].ToString("X2") + " ";
            break;

          case DisplayMode.Word:
            line_buffer += in_buffer[address+1].ToString("X2") + in_buffer[address].ToString("X2") + " ";
            address++;
            break;

          case DisplayMode.DoubleWord:
            line_buffer += in_buffer[address + 3].ToString("X2") + in_buffer[address + 2].ToString("X2") + in_buffer[address + 1].ToString("X2") + in_buffer[address].ToString("X2") + " ";
            address += 3;
            break;
        }
      }

      if (line_buffer.Length > 0)
        document += "\\pard" + line_buffer + "\\par";

      return document + "}";
    }

    /// <summary>
    /// Converts byte to hex in HTML format
    /// </summary>
    /// <param name="in_buffer">Buffer to convert</param>
    /// <returns>HTML representation of the buffer data</returns>
    public string ConvertToHTML(byte[] in_buffer)
    {
      // create HTML hex view
      UInt16 address;
      string line_buffer = "";
      StringBuilder document = new StringBuilder();

      // document header
      document.Append("<html><TT>");

      for (address = 0; address < in_buffer.Length; address++)
      {
        if (address % 16 == 0)
        {
          // add this line to the list
          if (line_buffer.Length > 0)
          {
            line_buffer += "<br>";
            document.Append(line_buffer);
          }

          line_buffer = "";
        }

        // start new line
        if (line_buffer.Length == 0)
          line_buffer = "<span style=\"color:blue\">" + address.ToString("X4") + ":</span> ";

        switch (m_display_mode)
        {
          case DisplayMode.Byte:
            line_buffer += in_buffer[address].ToString("X2") + " ";
            break;

          case DisplayMode.Word:
            line_buffer += in_buffer[address + 1].ToString("X2") + in_buffer[address].ToString("X2") + " ";
            address++;
            break;

          case DisplayMode.DoubleWord:
            line_buffer += in_buffer[address + 3].ToString("X2") + in_buffer[address + 2].ToString("X2") + in_buffer[address + 1].ToString("X2") + in_buffer[address].ToString("X2") + " ";
            address += 3;
            break;
        }
      }

      if (line_buffer.Length > 0)
      {
        line_buffer += "<br>";
        document.Append(line_buffer);
      }

      document.Append("</TT></html>");

      return document.ToString();
    }


    /// <summary>
    /// Gets converter name
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      switch (m_display_mode)
      {
        case DisplayMode.Byte:
          return "Bytes";

        case DisplayMode.Word:
          return "Word";

        case DisplayMode.DoubleWord:
          return "Double Word";
      }

      return "";
    }

    #endregion
  }
}

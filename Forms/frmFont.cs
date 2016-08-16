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
// Font chunk display class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmFont : Form
  {
    public chkFont ParentClass;
    private int BitmapWidth;
    private int BitmapHeight;

    public frmFont()
    {
      InitializeComponent();
      BitmapWidth = 1;
      BitmapHeight = 1;
    }

    public Panel FillControls( List<chkFont.FontInfo> in_fonts, string in_path)
    {
      string[] split;
      int size;
        
      split = in_path.Split('\\');

      switch (split.Length)
      {
        // main BMPS info
        case 1:
          lNumberOfFonts.Text = in_fonts.Count.ToString();

          size = 0;
          for (int i = 0; i < in_fonts.Count; i++)
          {
            size += in_fonts[i].GetFontBinarySize();
          }
          
          lChunkSize.Text = size.ToString();

          return pFontGeneral;

        // bitmap info
        case 2:
        {
          int font_index;
                                             
          if (int.TryParse(split[1], out font_index))
          {
						// font number
            lFontNumber.Text = lFontNumber.Text + font_index.ToString() + " '" + in_fonts[font_index-1].ReferenceName + "'";
            
            // flags
            lFontFlags.Text = ParentClass.FlagsToString(in_fonts[font_index-1].Flags);

						// min, max ASCII
						lMinAscii.Text = in_fonts[font_index - 1].MinAscii.ToString();
						lMaxAscii.Text = in_fonts[font_index - 1].MaxAscii.ToString();

						// height
						lHeight.Text = in_fonts[font_index - 1].Height.ToString();

            // bitmap dimension
            //lBitmapDimension.Text = in_bitmaps[bitmap_index - 1].Width.ToString() + "x" + in_bitmaps[bitmap_index - 1].Height.ToString();

            // number of colors
            //lNumberOfColors.Text = in_bitmaps[bitmap_index - 1].NumberOfColors.ToString();

            // display bitmap
						BitmapWidth = in_fonts[font_index - 1].DisplayBitmap.Width;
            BitmapHeight = in_fonts[font_index - 1].DisplayBitmap.Height;
            UpdateBitmapControlSize();
            pbDisplayBitmap.Image = in_fonts[font_index - 1].DisplayBitmap;

            return pFontDisplay;
          }
            /*
          // create bitmap
          bitmap_info.DisplayBitmap = new Bitmap(bitmap_info.Width, bitmap_info.Height);

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
                      */
              
           } 
          break;
      }
        
      return null;
    }
    
    private void UpdateBitmapControlSize()
    {
      int zoom = 100;
      string zoom_text = cbZoom.Text;

      zoom_text = zoom_text.Trim();
      zoom_text = zoom_text.TrimEnd('%');
      int.TryParse(zoom_text, out zoom);

      pbDisplayBitmap.Width = BitmapWidth * zoom / 100;
      pbDisplayBitmap.Height = BitmapHeight * zoom / 100;
    }

    private void cbZoom_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateBitmapControlSize();
    }
  }

  
}
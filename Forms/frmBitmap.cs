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
// Bitmap chunk display form
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	public partial class frmBitmap : Form
  {
    public chkBitmap ParentClass;
    private int BitmapWidth;
    private int BitmapHeight;

    public frmBitmap()
    {
      InitializeComponent();
      BitmapWidth = 1;
      BitmapHeight = 1;
    }

    public Panel FillControls( List<chkBitmap.BitmapInfo> in_bitmaps, string in_path)
    {
      string[] split;
      int size;

      split = in_path.Split('\\');

      switch (split.Length)
      {
        // main BMPS info
        case 1:
          lNumberOfBitmaps.Text = in_bitmaps.Count.ToString();

          size = 0;
          for (int i = 0; i < in_bitmaps.Count; i++)
          {
            size += in_bitmaps[i].GetBitmapBinarySize();
          }
          
          lChunkSize.Text = size.ToString();

          return pBitmapGeneral;

        // bitmap info
        case 2:
          {
            int bitmap_index;

            if (int.TryParse(split[1], out bitmap_index))
            {
              // bitmap number
							lBitmapNumber.Text = lBitmapNumber.Text + bitmap_index.ToString() + " '" + in_bitmaps[bitmap_index - 1].ReferenceName + "'"; ;

              // bitmap dimension
              lBitmapDimension.Text = in_bitmaps[bitmap_index - 1].Width.ToString() + "x" + in_bitmaps[bitmap_index - 1].Height.ToString();

              // number of colors
              lNumberOfColors.Text = in_bitmaps[bitmap_index - 1].BitPerPixel.ToString();

              // display bitmap
              BitmapWidth = in_bitmaps[bitmap_index - 1].Width;
              BitmapHeight = in_bitmaps[bitmap_index - 1].Height;
              UpdateBitmapControlSize();
              pbDisplayBitmap.Image = in_bitmaps[bitmap_index - 1].DisplayBitmap;

              return pBitmapDisplay;
            }
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
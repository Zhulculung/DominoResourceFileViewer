/*****************************************************************************/
/* Binary data conveter/viewer base class                                    */
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
  public interface IBinaryViewer
  {
    string ConvertToRTF(byte[] in_buffer);
    string ConvertToHTML(byte[] in_buffer);
    int GetPercentageReady();
  }
}

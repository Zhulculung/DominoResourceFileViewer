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
// Java chunk display class
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
  public partial class frmJava : Form
  {
    public chkJava ParentClass;

    public frmJava()
    {
      InitializeComponent();
    }

    public Panel FillControls(byte[][] m_buffer, string in_path)
    {
      string[] split;
      int size;

      split = in_path.Split('\\');

      switch (split.Length)
      {
        // main JCLS info
        case 1:

          // geberal information
          lNumberOfClasses.Text = m_buffer.Length.ToString();

          size = 0;
          for (int i = 0; i < m_buffer.Length; i++)
          {
            size += m_buffer[i].Length;
          }

          lLength.Text = size.ToString();

          // callback method table
          chkJava.JavaChunkHeader chunk_header = ParentClass.ChunkHeader;
          int callback_method_index = 0;

          dgvCallbackMethodTable.Rows.Clear();

          for (int i = 0; i < chunk_header.CallbackFunctionTable.Length; i++)
          {
            dgvCallbackMethodTable.Rows.Add();
            dgvCallbackMethodTable.Rows[callback_method_index].Cells[0].Value = (callback_method_index + 1).ToString();
            dgvCallbackMethodTable.Rows[callback_method_index].Cells[1].Value = "0x" + chunk_header.CallbackFunctionTable[i].ToString("X4");
            dgvCallbackMethodTable.Rows[callback_method_index].Cells[2].Value = chunk_header.CallbackFunctionTable[i].ToString();
            callback_method_index++;
          }

          return pJavaGeneral;

        // class info
        case 2:
          {
            int class_index;

            if (int.TryParse(split[1], out class_index))
            {
              chkJava.JavaClassHeader header;

              header = ParentClass.GetJavaClassHeader(m_buffer[class_index - 1]);
              lClassHeaderSize.Text = chkJava.JavaClassHeader.JavaClassHeaderLength.ToString();
              lClassNumber.Text = lClassNumber.Text + class_index.ToString();
              lClassSize.Text = header.ClassSize.ToString();
              lConstantPoolTableSize.Text = (header.ConstantStorageAreaAddress - header.ConstantPoolTableAddress).ToString();
              lConstantStorageAreaSize.Text = (header.MethodStorageAreaAddress - header.ConstantStorageAreaAddress).ToString();
              lMethodStorageAreaSize.Text = (header.ClassSize - header.MethodStorageAreaAddress).ToString();

              return pJavaClass;
            }
          }
          break;

        // constant pool table
        case 3:
          {
            int class_index;

            if (int.TryParse(split[1], out class_index))
            {
              chkJava.JavaClassHeader header = ParentClass.GetJavaClassHeader(m_buffer[class_index - 1]);

              switch (split[2])
              {
                case "cpt":
                  {
                    // update header
                    lConstantPoolTable.Text = lConstantPoolTable.Text + class_index.ToString();

                    // add data to data grib view
                    dgvConstantPoolTable.Rows.Clear();

                    int pos = header.ConstantPoolTableAddress;
                    int index = 0;

                    while (pos < header.ConstantStorageAreaAddress)
                    {
                      dgvConstantPoolTable.Rows.Add();
                      dgvConstantPoolTable.Rows[index].Cells[0].Value = index;
                      dgvConstantPoolTable.Rows[index].Cells[1].Value = "0x" + (pos - header.ConstantPoolTableAddress).ToString("X4");
                      dgvConstantPoolTable.Rows[index].Cells[2].Value = BitConverter.ToUInt16(m_buffer[class_index - 1], pos).ToString() + " (0x" + BitConverter.ToUInt16(m_buffer[class_index - 1], pos).ToString("X4") + ")";
                      index++;

                      pos += sizeof(UInt16);
                    }

                    return pConstantPoolTable;
                  }


                case "cps":
                  {
                    // update header
                    lConstantStorage.Text = lConstantStorage.Text + class_index.ToString();

                    // create hex view
                    UInt16 address;
                    string line_buffer = "";
                    string document = "{\\rtf1\\ansi{\\fonttbl\\f0\\fs10\\fswiss Courier New;}\\f0\\fs20";
                    int length = header.MethodStorageAreaAddress - header.ConstantStorageAreaAddress;

                    for (address = 0; address < length; address++)
                    {
                      if (address % 16 == 0)
                      {
                        // add this line to the list
                        if (line_buffer.Length > 0)
                          document += "\\pard " + line_buffer + "\\par";

                        line_buffer = "";
                      }

                      // start new line
                      if (line_buffer.Length == 0)
                        line_buffer = "{\\b " + address.ToString("X4") + ":} ";

                      line_buffer += m_buffer[class_index - 1][address + header.ConstantStorageAreaAddress].ToString("X2") + " ";
                    }

                    if (line_buffer.Length > 0)
                      document += "\\pard " + line_buffer + "\\par";

                    rtbConstantStorage.Rtf = document + "}";

                    return pConstantStorage;
                  }
              }
            }
            break;
          }

        case 5:
        {
          int class_index;
          int pos;
          int method_index;

          if (int.TryParse(split[1], out class_index) && split[2] == "mts" && int.TryParse(split[3], out method_index) && int.TryParse(split[4], out pos))
          {
            dispJavaDisassembler disassembler = new dispJavaDisassembler();

            chkJava.JavaMethodHeader header = ParentClass.GetMethodHeader(m_buffer[class_index - 1], ref pos);

            // generate title
            lMethod.Text = "Method #" + method_index.ToString() + " - Class #" + class_index.ToString();

            // fill header information
            lClassAddress.Text = "0x" + header.ClassAddress.ToString("X4");
            lAccessFlag.Text = chkJava.AccessFlagToString(header.AccessFlag);

            // if native method
            if ((header.AccessFlag & chkJava.ACC_NATIVE) != 0)
            {
              // stack rewind
              lStackRewindLabel.Visible = true;
              lLabel1.Text = header.StackRewind.ToString();

              // native method index
              lNativeMethodIndex.Visible = true;
              lLabel2.Text = header.NativeMethodIndex.ToString();
              lLabel2.Visible = true;
            }
            else
            {
              // bytecode length
              lBytecodeSize.Visible = true;
              lLabel1.Text = header.BytecodeLength.ToString();
              lBytecodeLengthBytes.Visible = true;
              byte[] buffer = new byte[header.BytecodeLength];

              Array.Copy( m_buffer[class_index-1],pos,buffer,0,buffer.Length);

              // method disassembly list
              wbBytecodeList.DocumentText = disassembler.Disassemble(buffer, 0, header.BytecodeLength);
              pBytecodeList.Visible = true;
              
              pos += header.BytecodeLength;
            }

            return pMethod;
          }
        }
        break;
      }

      return null;
    }
  }
}
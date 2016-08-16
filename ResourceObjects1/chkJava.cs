/*****************************************************************************/
/* Java bytecode chunk handling class                                        */
/*                                                                           */
/* Copyright (C) 2013 Laszlo Arvai                                           */
/* All rights reserved.                                                      */
/*                                                                           */
/* This software may be modified and distributed under the terms             */
/* of the BSD license.  See the LICENSE file for details.                    */
/*****************************************************************************/
using System;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Java bytecode chunk handling
	/// </summary>
  public class chkJava : chkChunkBase
	{
		#region · Constants ·
		/// <summary>
    /// Access flags
    /// </summary>
    public const int ACC_PUBLIC = 0x01;
    public const int ACC_PRIVATE = 0x02;
    public const int ACC_PROTECTED = 0x04;
    public const int ACC_STATIC = 0x08;
    public const int ACC_FINAL = 0x10;
    public const int ACC_SYNCHRONIZED = 0x20;
    public const int ACC_VOLATILE = 0x40;
    public const int ACC_TRANSIENT = 0x80;
    public const int ACC_NATIVE = 0x100;
    public const int ACC_INTERFACE = 0x200;
    public const int ACC_ABSTRACT = 0x400;
		#endregion

		#region · Types ·

		/// <summary>
		/// Java class hader
		/// </summary>
		public class JavaClassHeader
		{
			public const int JavaClassHeaderLength = 8;

			public UInt16 ClassSize;
			public UInt16 ConstantPoolTableAddress;
			public UInt16 ConstantStorageAreaAddress;
			public UInt16 MethodStorageAreaAddress;
		};

		/// <summary>
		/// Java method header
		/// </summary>
		public class JavaMethodHeader
		{
			public UInt16 ClassAddress;
			public UInt16 AccessFlag;

			public UInt16 StackRewind;
			public UInt16 NativeMethodIndex;

			public UInt16 CodeMaxStack;
			public UInt16 CodeMaxLocals;
			public UInt16 BytecodeLength;
		};

		/// <summary>
		/// Java chunk header
		/// </summary>
		public class JavaChunkHeader
		{
			public UInt16 CallbackFunctionTablePos;
			public UInt16 JavaClassesPos;
			public UInt16[] CallbackFunctionTable;
		};

		#endregion

		#region · Data members ·
		private byte[][] m_classes;
    int m_offset;
    frmJava m_display_form;
    JavaChunkHeader m_chunk_header;
    int m_java_classes_pos;
		#endregion

		#region · Constructor ·

		public chkJava()
    {
      m_display_form = null;
    }
		#endregion 

		#region · Properties ·

		/// <summary>
		/// Gets chunk header
		/// </summary>
		public JavaChunkHeader ChunkHeader
		{
			get
			{
				return m_chunk_header;
			}
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
      int pos = 0;
      int length;

      m_classes = null;
      m_offset = in_offset;

      // load chunk header
      m_chunk_header = GetJavaChunkHeader(in_buffer);
      pos = m_java_classes_pos = m_chunk_header.JavaClassesPos;

      // load classes
      while (pos < in_buffer.Length)
      {
        // get length
        length = BitConverter.ToUInt16(in_buffer, pos);
        
        if (pos + length > in_buffer.Length)
          length = in_buffer.Length - pos;

        if (m_classes == null)
        {
          m_classes = new byte[1][];
        }
        else
        {
          Array.Resize(ref m_classes, m_classes.Length + 1);
        }

        m_classes[m_classes.Length - 1] = new byte[length];

        Array.Copy(in_buffer, pos, m_classes[m_classes.Length - 1], 0, length);

        pos += length;
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
      int method_index;
      int offset = m_offset;
      
      // add chunk node
      TreeNode java_chunk_node = in_treeview.Nodes.Add(in_chunk_index.ToString(), "JAVA Class (0x" + offset.ToString("X8") + ")" , 3, 3);

      // add class nodes
      JavaClassHeader class_header;
      for (int class_index = 0; class_index < m_classes.Length; class_index++)
      {
        TreeNode class_node;
        class_header = GetJavaClassHeader(m_classes[class_index]);

        // add class node
        class_node = java_chunk_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (class_index + 1).ToString(), "Class #" + (class_index + 1).ToString() + " (0x" + (offset - m_offset + m_java_classes_pos).ToString("X8") + ")", 3, 3);

        // add constant pool table
        class_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (class_index + 1).ToString() + "\\cpt", "Constant pool table (0x" + (offset - m_offset + class_header.ConstantPoolTableAddress + m_java_classes_pos).ToString("X8") + ")", 3, 3);
                
        // add constant pool storage
        class_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (class_index + 1).ToString() + "\\cps", "Constant storage (0x" + (offset - m_offset + class_header.ConstantStorageAreaAddress + m_java_classes_pos).ToString("X8") + ")", 3, 3);

        // add methods
        JavaClassHeader header = GetJavaClassHeader(m_classes[class_index]);
        JavaMethodHeader method_header;
        int pos;

        pos = header.MethodStorageAreaAddress;
        method_index = 0;
        while (pos < header.ClassSize)
        {
          class_node.Nodes.Add(in_chunk_index.ToString() + "\\" + (class_index + 1).ToString() + "\\mts\\" + (method_index + 1).ToString() + "\\" + pos.ToString(), "Method #" + (method_index + 1).ToString() + " (0x" + (offset - m_offset + pos + m_java_classes_pos).ToString("X8") + ")", 3, 3);

          method_header = GetMethodHeader(m_classes[class_index], ref pos);
          method_index++;

          if ((method_header.AccessFlag & ACC_NATIVE) == 0)
            pos += method_header.BytecodeLength;
        }

        offset += m_classes[class_index].Length;
      }
    }

		/// <summary>
    /// Gets display panels
    /// </summary>
    /// <param name="in_path">Path of the display panel</param>
    /// <returns>Panel to display</returns>
		public override System.Windows.Forms.Panel GetDisplayPanel(string in_path)
		{
			m_display_form = new frmJava();

			m_display_form.ParentClass = this;

			return m_display_form.FillControls(m_classes, in_path);
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
		#endregion

		#region · Java display helper functions ·

		/// <summary>
		/// Gets method header information
		/// </summary>
		/// <param name="in_buffer"></param>
		/// <param name="in_pos"></param>
		/// <returns></returns>
		public JavaMethodHeader GetMethodHeader(byte[] in_buffer, ref int in_pos)
    {
      JavaMethodHeader header = new JavaMethodHeader();

      // class address
      header.ClassAddress = BitConverter.ToUInt16(in_buffer, in_pos);
      in_pos += sizeof(UInt16);

      // access flag
      header.AccessFlag = BitConverter.ToUInt16(in_buffer, in_pos);
      in_pos += sizeof(UInt16);

      // if native method
      if ((header.AccessFlag & ACC_NATIVE) != 0)
      {
        // stack rewind
        header.StackRewind = BitConverter.ToUInt16(in_buffer, in_pos);
        in_pos += sizeof(UInt16);

        // native method index
        header.NativeMethodIndex = BitConverter.ToUInt16(in_buffer, in_pos);
        in_pos += sizeof(UInt16);
      }
      else
      {
        // code max stack
        header.CodeMaxStack = BitConverter.ToUInt16(in_buffer, in_pos);
        in_pos += sizeof(UInt16);

        // code max locals
        header.CodeMaxLocals = BitConverter.ToUInt16(in_buffer, in_pos);
        in_pos += sizeof(UInt16);

        // bytecode length
        header.BytecodeLength = BitConverter.ToUInt16(in_buffer, in_pos);
        in_pos += sizeof(UInt16);
      }

      return header;
    }

		/// <summary>
		/// Gets Java chunk header
		/// </summary>
		/// <param name="in_buffer"></param>
		/// <returns></returns>
		public JavaChunkHeader GetJavaChunkHeader(byte[] in_buffer)
    {
      int pos = 0;
      JavaChunkHeader retval = new JavaChunkHeader();

      // load pointers
      retval.CallbackFunctionTablePos = BitConverter.ToUInt16(in_buffer, pos);
      pos += sizeof(UInt16);

      retval.JavaClassesPos = BitConverter.ToUInt16(in_buffer, sizeof(UInt16));
      pos += sizeof(UInt16);

      // load callback address table
      int entry_count = (retval.JavaClassesPos - retval.CallbackFunctionTablePos) / sizeof(UInt16);

      retval.CallbackFunctionTable = new UInt16[entry_count];

      for (int i = 0; i < entry_count; i++)
      {
        retval.CallbackFunctionTable[i] = BitConverter.ToUInt16(in_buffer, pos);
        pos += sizeof(UInt16);
      }

      return retval;
    }

		/// <summary>
		/// Gets Java class header
		/// </summary>
		/// <param name="in_buffer"></param>
		/// <returns></returns>
    public JavaClassHeader GetJavaClassHeader(byte[] in_buffer)
    {
      JavaClassHeader header = new JavaClassHeader();
      int pos = 0;

      // class size
      header.ClassSize = BitConverter.ToUInt16(in_buffer, pos);
      pos += sizeof(UInt16);

      // constant pool table address
      header.ConstantPoolTableAddress = BitConverter.ToUInt16(in_buffer, pos);
      pos += sizeof(UInt16);
      if (header.ConstantPoolTableAddress == 0)
        header.ConstantPoolTableAddress = JavaClassHeader.JavaClassHeaderLength; // header length

      // constant storage area address
      header.ConstantStorageAreaAddress = BitConverter.ToUInt16(in_buffer, pos);
      pos += sizeof(UInt16);
      if (header.ConstantStorageAreaAddress == 0)
        header.ConstantStorageAreaAddress = header.ConstantPoolTableAddress;

      // method storage area address
      header.MethodStorageAreaAddress = BitConverter.ToUInt16(in_buffer, pos);
      pos += sizeof(UInt16);
      if (header.MethodStorageAreaAddress == 0)
        header.MethodStorageAreaAddress = header.ConstantStorageAreaAddress;

      return header;
    }

		/// <summary>
		/// Converts access flag to string
		/// </summary>
		/// <param name="in_access_flag"></param>
		/// <returns></returns>
    static public string AccessFlagToString( UInt16 in_access_flag)
    {
      string access_flag_string = "";

      if(( in_access_flag & ACC_PUBLIC) != 0)
        access_flag_string += " ACC_PUBLIC |";

      if(( in_access_flag & ACC_PRIVATE) != 0)
        access_flag_string += " ACC_PRIVATE |";

      if(( in_access_flag & ACC_PROTECTED) != 0)
        access_flag_string += " ACC_PROTECTED |";

      if(( in_access_flag & ACC_STATIC) != 0)
        access_flag_string += " ACC_STATIC |";

      if(( in_access_flag & ACC_FINAL ) != 0)
        access_flag_string += " ACC_FINAL |";

      if(( in_access_flag & ACC_SYNCHRONIZED) != 0)
        access_flag_string += " ACC_SYNCHRONIZED |";

      if(( in_access_flag & ACC_VOLATILE) != 0)
        access_flag_string += " ACC_VOLATILE |";
      
      if(( in_access_flag & ACC_TRANSIENT) != 0)
        access_flag_string += " ACC_TRANSIENT |";
      
      if(( in_access_flag & ACC_NATIVE ) != 0)
        access_flag_string += " ACC_NATIVE |";
      
      if(( in_access_flag & ACC_INTERFACE) != 0)
        access_flag_string += " ACC_INTERFACE |";
      
      if(( in_access_flag & ACC_ABSTRACT) != 0)
        access_flag_string += " ACC_ABSTRACT |";

      if (access_flag_string.Length > 0 && access_flag_string[access_flag_string.Length - 1] == '|')
        access_flag_string = access_flag_string.Remove(access_flag_string.Length - 1);

      return access_flag_string;
		}
		#endregion

	}
}

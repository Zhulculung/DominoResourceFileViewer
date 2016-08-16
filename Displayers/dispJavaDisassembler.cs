/*****************************************************************************/
/* Java bytecode disassembler                                                */
/*                                                                           */
/* Copyright (C) 2013 Laszlo Arvai                                           */
/* All rights reserved.                                                      */
/*                                                                           */
/* This software may be modified and distributed under the terms             */
/* of the BSD license.  See the LICENSE file for details.                    */
/*****************************************************************************/
using System;

public class dispJavaDisassembler
{
  const int CommentPos = 40;

  public delegate void ParameterHandlerFunction(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length);

  public class DisassemblerTableEntry
  {
    public ParameterHandlerFunction ParameterHandler;
    public string Instruction;
    public string Comment;

    public DisassemblerTableEntry(ParameterHandlerFunction in_handler, string in_instruction, string in_comment)
    {
      ParameterHandler = in_handler;
      Instruction = in_instruction;
      Comment = in_comment;
    }
  };

  public string Disassemble(byte[] in_buffer, int in_pos, int in_length)
  {
    string document = "<html><body nowrap><samp>";
    int pos;
    string instruction_string;
    byte bytecode;
    string parameter;
    int parameter_length;
    string address;

    // disassemble list
    pos = in_pos;
    while (pos < in_pos + in_length)
    {
      // get bytecode
      bytecode = in_buffer[pos];
      address = "0x" + (pos - in_pos).ToString("X4");
      document += "<a name=\"" + address + "\"></a>" + address + ": <b>"; 
      pos++;
      instruction_string = m_disassembler_table[bytecode].Instruction;
      parameter_length = 0;
      parameter = "";

      // handle parameters
      if (m_disassembler_table[bytecode].ParameterHandler != null)
      {
        m_disassembler_table[bytecode].ParameterHandler(in_buffer, ref pos, out parameter, out parameter_length);
      }

      // add comment
      int space_count = CommentPos - 8 - (instruction_string.Length + parameter_length);
      instruction_string += parameter;
      while (space_count > 0)
      {
        instruction_string += "&nbsp";
        space_count--;
      }

      document += instruction_string + "</b>";
      document += "<span style=\"color:green\"><i>// " + m_disassembler_table[bytecode].Comment + "</i></span><br />";
    }

    document += "</samp></body></html>";

    return document;

  }

  private void GetConstantInt8(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    int value = in_buffer[in_buffer_pos];
    in_buffer_pos++;

    if (value > 127)
      value -= 256;

    out_parameter = " " + value.ToString();
    out_parameter_length = out_parameter.Length;
    out_parameter = "<span style=\"color:blue\">" + out_parameter + "</span>";
  } 

  private void GetConstantInt16(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    byte[] buffer = new byte[2];
    int value;

    buffer[0] = in_buffer[in_buffer_pos+1];
    buffer[1] = in_buffer[in_buffer_pos];

    in_buffer_pos += 2;

    value = BitConverter.ToInt16(buffer, 0);

    out_parameter = " " + value.ToString();

    out_parameter_length = out_parameter.Length;

    out_parameter = "<span style=\"color:blue\">" + out_parameter + "</span>";
  }

  private void GetBranchOffset16(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    byte[] buffer = new byte[2];
    int value;
    string jump_address;
    string jump_offset;

    buffer[0] = in_buffer[in_buffer_pos + 1];
    buffer[1] = in_buffer[in_buffer_pos];

    in_buffer_pos += 2;

    value = BitConverter.ToInt16(buffer, 0);

    jump_address = "0x" + (in_buffer_pos + value - 3).ToString("X4");
    jump_offset = " (" + value.ToString() + ")";

    out_parameter_length = jump_address.Length + jump_offset.Length + 1;

    out_parameter = " <a href=\"#" + jump_address + "\">" + jump_address + "</a><span style=\"color:gray\">" + jump_offset.ToString() + "</span>";
  }

  private void GetIndex(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    out_parameter = " #" + ((in_buffer[in_buffer_pos] << 8) + in_buffer[in_buffer_pos + 1]).ToString();
    in_buffer_pos += 2;

    out_parameter_length = out_parameter.Length;

    out_parameter = "<span style=\"color:green\">" + out_parameter + "</span>";
  }

  private void GetVariableIndex(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    out_parameter = " " + in_buffer[in_buffer_pos].ToString();
    in_buffer_pos += 1;

    out_parameter_length = out_parameter.Length;

    out_parameter = "<span style=\"color:magenta\">" + out_parameter + "</span>";
  }

  DisassemblerTableEntry[] m_disassembler_table;

  private void prmBipush(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetConstantInt8(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmSipush(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetConstantInt16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmLdc(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmLdcW(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmLdc2W(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIload(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmLload(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmFload(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmDload(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmAload(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIstore(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmLstore(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmFstore(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmDstore(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmAstore(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetVariableIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIinc(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    string index;
    string constant;

    // variable index
    index = " $" + in_buffer[in_buffer_pos].ToString();
    in_buffer_pos ++;

    // increment value
    int value = in_buffer[in_buffer_pos];
    in_buffer_pos++;

    if (value > 127)
      value -= 256;

    constant = value.ToString();

    out_parameter = "<span style=\"color:green\">" + index + "</span> by <span style=\"color:blue\">" + constant + "</span>";

    out_parameter_length = index.Length + constant.Length + 4;
  }

  private void prmIfeq(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIfne(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIflt(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIfge(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIfgt(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIfle(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_icmpeq(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_icmpne(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_icmplt(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void mIf_icmpge(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_icmpgt(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_icmple(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_acmpeq(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIf_acmpne(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmGoto(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetBranchOffset16(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmJsr(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmRet(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmTableswtich(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmLookupswitch(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmGetstatic(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmPutstatic(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmGetfield(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmPutfield(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmInvokevirtual(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmInvokespecial(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmInvokestaic(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmInvokeinterface(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmNew(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmNewarray(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmAnewarray(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmCheckcast(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmInstanceof(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmWide(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmMultianewarray(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmIfnull(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmInonull(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmGoto_w(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  private void prmJsr_W(byte[] in_buffer, ref int in_buffer_pos, out string out_parameter, out int out_parameter_length)
  {
    GetIndex(in_buffer, ref in_buffer_pos, out out_parameter, out out_parameter_length);
  }

  public dispJavaDisassembler()
  {
    m_disassembler_table = new DisassemblerTableEntry[]
    {
      new DisassemblerTableEntry(null,"nop","performs no operation"),
      new DisassemblerTableEntry(null,"aconst_null","pushes a null reference onto the stack"),
      new DisassemblerTableEntry(null,"iconst_m1","loads the int value -1 onto the stack"),
      new DisassemblerTableEntry(null,"iconst_0","loads the int value 0 onto the stack"),
      new DisassemblerTableEntry(null,"iconst_1","loads the int value 1 onto the stack"),
      new DisassemblerTableEntry(null,"iconst_2","loads the int value 2 onto the stack"),
      new DisassemblerTableEntry(null,"iconst_3","loads the int value 3 onto the stack"),
      new DisassemblerTableEntry(null,"iconst_4","loads the int value 4 onto the stack"),
      new DisassemblerTableEntry(null,"iconst_5","loads the int value 5 onto the stack"),
      new DisassemblerTableEntry(null,"lconst_0","pushes the long 0 onto the stack"),
      new DisassemblerTableEntry(null,"lconst_1","pushes the long 1 onto the stack"),
      new DisassemblerTableEntry(null,"fconst_0","pushes 0.0f on the stack"),
      new DisassemblerTableEntry(null,"fconst_1","pushes 1.0f on the stack"),
      new DisassemblerTableEntry(null,"fconst_2","pushes 2.0f on the stack"),
      new DisassemblerTableEntry(null,"dconst_0","pushes the constant 0.0 onto the stack"),
      new DisassemblerTableEntry(null,"dconst_1","pushes the constant 1.0 onto the stack"),
      new DisassemblerTableEntry(prmBipush,"bipush","pushes a byte onto the stack as an integer value"),
      new DisassemblerTableEntry(prmSipush,"sipush","pushes a signed integer (byte1 << 8 + byte2) onto the stack"),
      new DisassemblerTableEntry(prmLdc,"ldc","pushes a constant #index from a constant pool (String, int, float or class type) onto the stack"),
      new DisassemblerTableEntry(prmLdcW,"ldc_w","pushes a constant #index from a constant pool (String, int, float or class type) onto the stack (wide index is constructed as indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmLdc2W,"ldc2_w","pushes a constant #index from a constant pool (double or long) onto the stack (wide index is constructed as indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmIload,"iload","loads an int value from a variable #index"),
      new DisassemblerTableEntry(prmLload,"lload","load a long value from a local variable #index"),
      new DisassemblerTableEntry(prmFload,"fload","loads a float value from a local variable #index"),
      new DisassemblerTableEntry(prmDload,"dload","loads a double value from a local variable #index"),
      new DisassemblerTableEntry(prmAload,"aload","loads a reference onto the stack from a local variable #index"),
      new DisassemblerTableEntry(null,"iload_0","loads an int value from variable 0"),
      new DisassemblerTableEntry(null,"iload_1","loads an int value from variable 1"),
      new DisassemblerTableEntry(null,"iload_2","loads an int value from variable 2"),
      new DisassemblerTableEntry(null,"iload_3","loads an int value from variable 3"),
      new DisassemblerTableEntry(null,"lload_0","load a long value from a local variable 0"),
      new DisassemblerTableEntry(null,"lload_1","load a long value from a local variable 1"),
      new DisassemblerTableEntry(null,"lload_2","load a long value from a local variable 2"),
      new DisassemblerTableEntry(null,"lload_3","load a long value from a local variable 3"),
      new DisassemblerTableEntry(null,"fload_0","loads a float value from local variable 0"),
      new DisassemblerTableEntry(null,"fload_1","loads a float value from local variable 1"),
      new DisassemblerTableEntry(null,"fload_2","loads a float value from local variable 2"),
      new DisassemblerTableEntry(null,"fload_3","loads a float value from local variable 3"),
      new DisassemblerTableEntry(null,"dload_0","loads a double from local variable 0"),
      new DisassemblerTableEntry(null,"dload_1","loads a double from local variable 1"),
      new DisassemblerTableEntry(null,"dload_2","loads a double from local variable 2"),
      new DisassemblerTableEntry(null,"dload_3","loads a double from local variable 3"),
      new DisassemblerTableEntry(null,"aload_0","loads a reference onto the stack from local variable 0"),
      new DisassemblerTableEntry(null,"aload_1","loads a reference onto the stack from local variable 1"),
      new DisassemblerTableEntry(null,"aload_2","loads a reference onto the stack from local variable 2"),
      new DisassemblerTableEntry(null,"aload_3","loads a reference onto the stack from local variable 3"),
      new DisassemblerTableEntry(null,"iaload","loads an int from an array"),
      new DisassemblerTableEntry(null,"laload","load a long from an array"),
      new DisassemblerTableEntry(null,"faload","loads a float from an array"),
      new DisassemblerTableEntry(null,"daload","loads a double from an array"),
      new DisassemblerTableEntry(null,"aaload","loads onto the stack a reference from an array"),
      new DisassemblerTableEntry(null,"baload","loads a byte or Boolean value from an array"),
      new DisassemblerTableEntry(null,"caload","loads a char from an array"),
      new DisassemblerTableEntry(null,"saload","load short from array"),
      new DisassemblerTableEntry(prmIstore,"istore","store int value into variable #index"),
      new DisassemblerTableEntry(prmLstore,"lstore","store a long value in a local variable #index"),
      new DisassemblerTableEntry(prmFstore,"fstore","stores a float value into a local variable #index"),
      new DisassemblerTableEntry(prmDstore,"dstore","stores a double value into a local variable #index"),
      new DisassemblerTableEntry(prmAstore,"astore","stores a reference into a local variable #index"),
      new DisassemblerTableEntry(null,"istore_0","store int value into variable 0"),
      new DisassemblerTableEntry(null,"istore_1","store int value into variable 1"),
      new DisassemblerTableEntry(null,"istore_2","store int value into variable 2"),
      new DisassemblerTableEntry(null,"istore_3","store int value into variable 3"),
      new DisassemblerTableEntry(null,"lstore_0","store a long value in a local variable 0"),
      new DisassemblerTableEntry(null,"lstore_1","store a long value in a local variable 1"),
      new DisassemblerTableEntry(null,"lstore_2","store a long value in a local variable 2"),
      new DisassemblerTableEntry(null,"lstore_3","store a long value in a local variable 3"),
      new DisassemblerTableEntry(null,"fstore_0","stores a float value into local variable 0"),
      new DisassemblerTableEntry(null,"fstore_1","stores a float value into local variable 1"),
      new DisassemblerTableEntry(null,"fstore_2","stores a float value into local variable 2"),
      new DisassemblerTableEntry(null,"fstore_3","stores a float value into local variable 3"),
      new DisassemblerTableEntry(null,"dstore_0","stores a double into local variable 0"),
      new DisassemblerTableEntry(null,"dstore_1","stores a double into local variable 1"),
      new DisassemblerTableEntry(null,"dstore_2","stores a double into local variable 2"),
      new DisassemblerTableEntry(null,"dstore_3","stores a double into local variable 3"),
      new DisassemblerTableEntry(null,"astore_0","stores a reference into local variable 0"),
      new DisassemblerTableEntry(null,"astore_1","stores a reference into local variable 1"),
      new DisassemblerTableEntry(null,"astore_2","stores a reference into local variable 2"),
      new DisassemblerTableEntry(null,"astore_3","stores a reference into local variable 3"),
      new DisassemblerTableEntry(null,"iastore","stores an int into an array"),
      new DisassemblerTableEntry(null,"lastore","store a long to an array"),
      new DisassemblerTableEntry(null,"fastore","stores a float in an array"),
      new DisassemblerTableEntry(null,"dastore","stores a double into an array"),
      new DisassemblerTableEntry(null,"aastore","stores a reference into an array"),
      new DisassemblerTableEntry(null,"bastore","stores a byte or Boolean value into an array"),
      new DisassemblerTableEntry(null,"castore","stores a char into an array"),
      new DisassemblerTableEntry(null,"sastore","store short to array"),
      new DisassemblerTableEntry(null,"pop","discards the top value on the stack"),
      new DisassemblerTableEntry(null,"pop2","discards the top two values on the stack (or one value, if it is a double or long)"),
      new DisassemblerTableEntry(null,"dup","duplicates the value on top of the stack"),
      new DisassemblerTableEntry(null,"dup_x1","inserts a copy of the top value into the stack two values from the top"),
      new DisassemblerTableEntry(null,"dup_x2","inserts a copy of the top value into the stack two (if value2 is double or long it takes up the entry of value3, too) or three values (if value2 is neither double nor long) from the top"),
      new DisassemblerTableEntry(null,"dup2","duplicate top two stack words (two values, if value1 is not double nor long; a single value, if value1 is double or long)"),
      new DisassemblerTableEntry(null,"dup2_x1","duplicate two words and insert beneath third word (see explanation above)"),
      new DisassemblerTableEntry(null,"dup2_x2","duplicate two words and insert beneath fourth word"),
      new DisassemblerTableEntry(null,"swap","swaps two top words on the stack (note that value1 and value2 must not be double or long)"),
      new DisassemblerTableEntry(null,"iadd","adds two ints together"),
      new DisassemblerTableEntry(null,"ladd","add two longs"),
      new DisassemblerTableEntry(null,"fadd","adds two floats"),
      new DisassemblerTableEntry(null,"dadd","adds two doubles"),
      new DisassemblerTableEntry(null,"isub","int subtract"),
      new DisassemblerTableEntry(null,"lsub","subtract two longs"),
      new DisassemblerTableEntry(null,"fsub","subtracts two floats"),
      new DisassemblerTableEntry(null,"dsub","subtracts a double from another"),
      new DisassemblerTableEntry(null,"imul","multiply two integers"),
      new DisassemblerTableEntry(null,"lmul","multiplies two longs"),
      new DisassemblerTableEntry(null,"fmul","multiplies two floats"),
      new DisassemblerTableEntry(null,"dmul","multiplies two doubles"),
      new DisassemblerTableEntry(null,"idiv","divides two integers"),
      new DisassemblerTableEntry(null,"ldiv","divide two longs"),
      new DisassemblerTableEntry(null,"fdiv","divides two floats"),
      new DisassemblerTableEntry(null,"ddiv","divides two doubles"),
      new DisassemblerTableEntry(null,"irem","logical int remainder"),
      new DisassemblerTableEntry(null,"lrem","remainder of division of two longs"),
      new DisassemblerTableEntry(null,"frem","gets the remainder from a division between two floats"),
      new DisassemblerTableEntry(null,"drem","gets the remainder from a division between two doubles"),
      new DisassemblerTableEntry(null,"ineg","negate int"),
      new DisassemblerTableEntry(null,"lneg","negates a long"),
      new DisassemblerTableEntry(null,"fneg","negates a float"),
      new DisassemblerTableEntry(null,"dneg","negates a double"),
      new DisassemblerTableEntry(null,"ishl","int shift left"),
      new DisassemblerTableEntry(null,"lshl","bitwise shift left of a long value1 by value2 positions"),
      new DisassemblerTableEntry(null,"ishr","int shift right"),
      new DisassemblerTableEntry(null,"lshr","bitwise shift right of a long value1 by value2 positions"),
      new DisassemblerTableEntry(null,"iushr","int shift right"),
      new DisassemblerTableEntry(null,"lushr","bitwise shift right of a long value1 by value2 positions, unsigned"),
      new DisassemblerTableEntry(null,"iand","performs a logical and on two integers"),
      new DisassemblerTableEntry(null,"land","bitwise and of two longs"),
      new DisassemblerTableEntry(null,"ior","logical int or"),
      new DisassemblerTableEntry(null,"lor","bitwise or of two longs"),
      new DisassemblerTableEntry(null,"ixor","int xor"),
      new DisassemblerTableEntry(null,"lxor","bitwise exclusive or of two longs"),
      new DisassemblerTableEntry(prmIinc,"iinc","increment local variable #index by signed byte const"),
      new DisassemblerTableEntry(null,"i2l","converts an int into a long"),
      new DisassemblerTableEntry(null,"i2f","converts an int into a float"),
      new DisassemblerTableEntry(null,"i2d","converts an int into a double"),
      new DisassemblerTableEntry(null,"l2i","converts a long to an int"),
      new DisassemblerTableEntry(null,"l2f","converts a long to a float"),
      new DisassemblerTableEntry(null,"l2d","converts a long to a double"),
      new DisassemblerTableEntry(null,"f2i","converts a float to an int"),
      new DisassemblerTableEntry(null,"f2l","converts a float to a long"),
      new DisassemblerTableEntry(null,"f2d","converts a float to a double"),
      new DisassemblerTableEntry(null,"d2i","converts a double to an int"),
      new DisassemblerTableEntry(null,"d2l","converts a double to a long"),
      new DisassemblerTableEntry(null,"d2f","converts a double to a float"),
      new DisassemblerTableEntry(null,"i2b","converts an int into a byte"),
      new DisassemblerTableEntry(null,"i2c","converts an int into a character"),
      new DisassemblerTableEntry(null,"i2s","converts an int into a short"),
      new DisassemblerTableEntry(null,"lcmp","compares two longs values"),
      new DisassemblerTableEntry(null,"fcmpl","compares two floats"),
      new DisassemblerTableEntry(null,"fcmpg","compares two floats"),
      new DisassemblerTableEntry(null,"dcmpl","compares two doubles"),
      new DisassemblerTableEntry(null,"dcmpg","compares two doubles"),
      new DisassemblerTableEntry(prmIfeq,"ifeq","if value is 0, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIfne,"ifne","if value is not 0, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIflt,"iflt","if value is less than 0, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIfge,"ifge","if value is greater than or equal to 0, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIfgt,"ifgt","if value is greater than 0, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIfle,"ifle","if value is less than or equal to 0, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_icmpeq,"if_icmpeq","if ints are equal, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_icmpne,"if_icmpne","if ints are not equal, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_icmplt,"if_icmplt","if value1 is less than value2, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(mIf_icmpge,"if_icmpge","if value1 is greater than or equal to value2, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_icmpgt,"if_icmpgt","if value1 is greater than value2, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_icmple,"if_icmple","if value1 is less than or equal to value2, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_acmpeq,"if_acmpeq","if references are equal, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmIf_acmpne,"if_acmpne","if references are not equal, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmGoto,"goto","goes to another instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmJsr,"jsr","jump to subroutine at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2) and place the return address on the stack"),
      new DisassemblerTableEntry(prmRet,"ret","continue execution from address taken from a local variable #index (the asymmetry with jsr is intentional)"),
      new DisassemblerTableEntry(prmTableswtich,"tableswitch","continue execution from an address in the table at offset index"),
      new DisassemblerTableEntry(prmLookupswitch,"lookupswitch","a target address is looked up from a table using a key and execution continues from the instruction at that address"),
      new DisassemblerTableEntry(null,"ireturn","returns an integer from a method"),
      new DisassemblerTableEntry(null,"lreturn","returns a long value"),
      new DisassemblerTableEntry(null,"freturn","returns a float from method"),
      new DisassemblerTableEntry(null,"dreturn","returns a double from a method"),
      new DisassemblerTableEntry(null,"areturn","returns a reference from a method"),
      new DisassemblerTableEntry(null,"return","return void from method"),
      new DisassemblerTableEntry(prmGetstatic,"getstatic","gets a static field value of a class, where the field is identified by field reference in the constant pool index (index1 << 8 + index2)"),
      new DisassemblerTableEntry(prmPutstatic,"putstatic","set static field to value in a class, where the field is identified by a field reference index in constant pool (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmGetfield,"getfield","gets a field value of an object objectref, where the field is identified by field reference in the constant pool index (index1 << 8 + index2)"),
      new DisassemblerTableEntry(prmPutfield,"putfield","set field to value in an object objectref, where the field is identified by a field reference index in constant pool (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmInvokevirtual,"invokevirtual","invoke virtual method on object objectref, where the method is identified by method reference index in constant pool (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmInvokespecial,"invokespecial","invoke instance method on object objectref requiring special handling (instance initialization method, a private method, or a superclass method), where the method is identified by method reference index in constant pool (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmInvokestaic,"invokestatic","invoke a static method, where the method is identified by method reference index in constant pool (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmInvokeinterface,"invokeinterface","invokes an interface method on object objectref, where the interface method is identified by method reference index in constant pool (indexbyte1 << 8 + indexbyte2) and count is the number of arguments to pop from the stack frame including the object on which the method is being called and must always be greater than or equal to 1"),
      new DisassemblerTableEntry(null,"xxxunusedxxx","this opcode is reserved for historical reasons"),
      new DisassemblerTableEntry(prmNew,"new","creates new object of type identified by class reference in constant pool index (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmNewarray,"newarray","creates new array with count elements of primitive type identified by atype"),
      new DisassemblerTableEntry(prmAnewarray,"anewarray","creates a new array of references of length count and component type identified by the class reference index (indexbyte1 << 8 + indexbyte2) in the constant pool"),
      new DisassemblerTableEntry(null,"arraylength","gets the length of an array"),
      new DisassemblerTableEntry(null,"athrow","throws an error or exception (notice that the rest of the stack is cleared, leaving only a reference to the Throwable)"),
      new DisassemblerTableEntry(prmCheckcast,"checkcast","checks whether an objectref is of a certain type, the class reference of which is in the constant pool at index (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(prmInstanceof,"instanceof","determines if an object objectref is of a given type, identified by class reference index in constant pool (indexbyte1 << 8 + indexbyte2)"),
      new DisassemblerTableEntry(null,"monitorenter","enter monitor for object (\"grab the lock\" - start of synchronized() section)"),
      new DisassemblerTableEntry(null,"monitorexit","exit monitor for object (\"release the lock\" - end of synchronized() section)"),
      new DisassemblerTableEntry(prmWide,"wide","execute opcode, where opcode is either iload, fload, aload, lload, dload, istore, fstore, astore, lstore, dstore, or ret, but assume the index is 16 bit; or execute iinc, where the index is 16 bits and the constant to increment by is a signed 16 bit short"),
      new DisassemblerTableEntry(prmMultianewarray,"multianewarray","create a new array of dimensions dimensions with elements of type identified by class reference in constant pool index (indexbyte1 << 8 + indexbyte2); the sizes of each dimension is identified by count1, [count2, etc]"),
      new DisassemblerTableEntry(prmIfnull,"ifnull","if value is null, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmInonull,"ifnonnull","if value is not null, branch to instruction at branchoffset (signed short constructed from unsigned bytes branchbyte1 << 8 + branchbyte2)"),
      new DisassemblerTableEntry(prmGoto_w,"goto_w","goes to another instruction at branchoffset (signed int constructed from unsigned bytes branchbyte1 << 24 + branchbyte2 << 16 + branchbyte3 << 8 + branchbyte4)"),
      new DisassemblerTableEntry(prmJsr_W,"jsr_w","jump to subroutine at branchoffset (signed int constructed from unsigned bytes branchbyte1 << 24 + branchbyte2 << 16 + branchbyte3 << 8 + branchbyte4) and place the return address on the stack"),
      new DisassemblerTableEntry(null,"breakpoint","reserved for breakpoints in Java debuggers; should not appear in any class file"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"unknown","these values are currently unassigned for opcodes and are reserved for future use"),
      new DisassemblerTableEntry(null,"impdep1","reserved for implementation-dependent operations within debuggers; should not appear in any class file"),
      new DisassemblerTableEntry(null,"impdep2","reserved for implementation-dependent operations within debuggers; should not appear in any class file") };
  }
}
/*****************************************************************************/
/* Binary data to M6502 disassembler                                         */
/*                                                                           */
/* Copyright (C) 2013 Laszlo Arvai                                           */
/* All rights reserved.                                                      */
/*                                                                           */
/* This software may be modified and distributed under the terms             */
/* of the BSD license.  See the LICENSE file for details.                    */
/*****************************************************************************/
using System.Windows.Forms;
using System.Text;

namespace DominoResourceFileViewer
{
  /// <summary>
  /// Displays byte buffer content as Z80 disassembled list
  /// The disassembler code is based on BizHawk.Emulation library
  /// </summary>
  public class dispM6502Disassembler : IBinaryViewer
  {
    #region · Data members ·
    private byte [] m_buffer;		/// Binary data buffer
    private int m_pc;						/// Internal Program Counter
    #endregion

    #region · Public functions ·

    /// <summary>
    /// Gets percentage ready of the conversion
    /// </summary>
    /// <returns></returns>
    public int GetPercentageReady()
    {
      if (m_buffer.Length == 0)
        return 0;
      else
      {
        return m_pc * 100 / m_buffer.Length;
      }
    }

    /// <summary>
    /// Convert buffer to Z80 disassembled list in RTF representation
    /// </summary>
    /// <param name="in_buffer"></param>
    /// <returns></returns>
    public string ConvertToRTF(byte[] in_buffer)
    {
      int bytes_to_advance;
      string disassembly_buffer;
      string line_buffer = "";
      int line_count;
      StringBuilder document = new StringBuilder();
      
      document.Append("{\\rtf1\\ansi{\\fonttbl\\f0\\fs10\\fswiss Courier New;}{\\colortbl;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red128\\green128\\blue128;}\\f0\\fs20");

      m_buffer = in_buffer;
      m_pc = 0;
      line_count = 0;
      while (m_pc < m_buffer.Length)
      {
        // add address
        line_buffer = "{\\cf2 " + m_pc.ToString("X4") + "\\cf1}: ";

        disassembly_buffer = "{\\b " + DisassembleOneInstruction((ushort)m_pc, out bytes_to_advance) + "}";

        line_buffer += "{\\cf3 ";
        for (int i = 0; i < bytes_to_advance; i++)
        {
          line_buffer += m_buffer[m_pc++].ToString("X2") + " ";
        }
        line_buffer += "\\cf1} " + "\\tqr\\tx2500\\tab " + disassembly_buffer;

        // add line to the document
        document.Append("\\pard" + line_buffer + "\\par");

        if (line_count % 256 == 0)
          Application.DoEvents();

        line_count++;
      }

      // close document
      document.Append("}");

      return document.ToString();
    }

    /// <summary>
    /// Disassembles to HTML
    /// </summary>
    /// <param name="in_buffer"></param>
    /// <returns></returns>
    public string ConvertToHTML(byte[] in_buffer)
    {
      string opcode_string;
      string disassembly_buffer;
      int bytes_to_advance;
      int line_count;
      StringBuilder document = new StringBuilder();

      // document header
      document.Append("<html><body nowrap><samp>");

      m_buffer = in_buffer;
      m_pc = 0;
      line_count = 0;
      // diassemble all opcodes
      while (m_pc < m_buffer.Length)
      {
        // add address
        document.Append("<span style=\"color:blue\">" + m_pc.ToString("X4") + ": </span>");

        // disassemble instruction
        disassembly_buffer = "<b>" + DisassembleOneInstruction((ushort)m_pc, out bytes_to_advance) + "</b>";

        // instruction opcodes
        opcode_string = "";
        for (int i = 0; i < bytes_to_advance; i++)
        {
          opcode_string += m_buffer[m_pc++].ToString("X2") + " ";
        }

        int opcode_length = opcode_string.Length;
        while (opcode_length < 12)
        {
          opcode_string += "&nbsp";
          opcode_length++;
        }

        // opcode string
        document.Append("<span style=\"color:gray\">");
        document.Append(opcode_string);
        document.Append("</span>");

        // disassembledinstruction
        document.Append(disassembly_buffer);
        document.Append("<br />");

        if (line_count % 256 == 0)
          Application.DoEvents();

        line_count++;
      }

      // close HTML
      document.Append("</samp></body></html>");

      // return document
      return document.ToString();
    }

    /// <summary>
    /// Gets converter name
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return "M6502 Disassembler";
    }

    #endregion

    #region · Internal functions ·

    /// <summary>
    /// Reads byte from the byte buffer
    /// </summary>
    /// <returns></returns>
    private byte ReadMemory(ushort in_pc)
    {
      byte opcode = 0xff;

      if (in_pc < m_buffer.Length)
      {
        opcode = m_buffer[in_pc];
      }

      return opcode;
    }

    /// <summary>
    /// Reads word from the byte buffer
    /// </summary>
    /// <param name="in_pc"></param>
    /// <returns></returns>
    private ushort ReadWord(ushort in_pc)
    {
      ushort value = 0xffff;

      if ((in_pc + 1) < m_buffer.Length)
      {
        value = (ushort)(m_buffer[in_pc] + (m_buffer[in_pc+1] << 8));
      }

      return value;
    }

    /// <summary>
    /// Disassembles one isntruction from the given address
    /// </summary>
    /// <param name="in_pc"></param>
    /// <param name="out_bytes_to_advance"></param>
    /// <returns></returns>
    private string DisassembleOneInstruction(ushort in_pc, out int out_bytes_to_advance)
    {
      byte op = ReadMemory(in_pc);
      switch (op)
      {
        case 0x00: out_bytes_to_advance = 1; return "BRK";
        case 0x01: out_bytes_to_advance = 2; return string.Format("ORA (${0:X2},X)", ReadMemory(++in_pc));
        case 0x04: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2}", ReadMemory(++in_pc));
        case 0x05: out_bytes_to_advance = 2; return string.Format("ORA ${0:X2}", ReadMemory(++in_pc));
        case 0x06: out_bytes_to_advance = 2; return string.Format("ASL ${0:X2}", ReadMemory(++in_pc));
        case 0x08: out_bytes_to_advance = 1; return "PHP";
        case 0x09: out_bytes_to_advance = 2; return string.Format("ORA #${0:X2}", ReadMemory(++in_pc));
        case 0x0A: out_bytes_to_advance = 1; return "ASL A";
        case 0x0C: out_bytes_to_advance = 3; return string.Format("NOP (${0:X4})", ReadWord(++in_pc));
        case 0x0D: out_bytes_to_advance = 3; return string.Format("ORA ${0:X4}", ReadWord(++in_pc));
        case 0x0E: out_bytes_to_advance = 3; return string.Format("ASL ${0:X4}", ReadWord(++in_pc));
        case 0x10: out_bytes_to_advance = 2; return string.Format("BPL ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0x11: out_bytes_to_advance = 2; return string.Format("ORA (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0x14: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2},X", ReadMemory(++in_pc));
        case 0x15: out_bytes_to_advance = 2; return string.Format("ORA ${0:X2},X", ReadMemory(++in_pc));
        case 0x16: out_bytes_to_advance = 2; return string.Format("ASL ${0:X2},X", ReadMemory(++in_pc));
        case 0x18: out_bytes_to_advance = 1; return "CLC";
        case 0x19: out_bytes_to_advance = 3; return string.Format("ORA ${0:X4},Y *", ReadWord(++in_pc));
        case 0x1A: out_bytes_to_advance = 1; return "NOP";
        case 0x1C: out_bytes_to_advance = 2; return string.Format("NOP (${0:X2},X)", ReadMemory(++in_pc));
        case 0x1D: out_bytes_to_advance = 3; return string.Format("ORA ${0:X4},X *", ReadWord(++in_pc));
        case 0x1E: out_bytes_to_advance = 3; return string.Format("ASL ${0:X4},X", ReadWord(++in_pc));
        case 0x20: out_bytes_to_advance = 3; return string.Format("JSR ${0:X4}", ReadWord(++in_pc));
        case 0x21: out_bytes_to_advance = 2; return string.Format("AND (${0:X2},X)", ReadMemory(++in_pc));
        case 0x24: out_bytes_to_advance = 2; return string.Format("BIT ${0:X2}", ReadMemory(++in_pc));
        case 0x25: out_bytes_to_advance = 2; return string.Format("AND ${0:X2}", ReadMemory(++in_pc));
        case 0x26: out_bytes_to_advance = 2; return string.Format("ROL ${0:X2}", ReadMemory(++in_pc));
        case 0x28: out_bytes_to_advance = 1; return "PLP";
        case 0x29: out_bytes_to_advance = 2; return string.Format("AND #${0:X2}", ReadMemory(++in_pc));
        case 0x2A: out_bytes_to_advance = 1; return "ROL A";
        case 0x2C: out_bytes_to_advance = 3; return string.Format("BIT ${0:X4}", ReadWord(++in_pc));
        case 0x2D: out_bytes_to_advance = 3; return string.Format("AND ${0:X4}", ReadWord(++in_pc));
        case 0x2E: out_bytes_to_advance = 3; return string.Format("ROL ${0:X4}", ReadWord(++in_pc));
        case 0x30: out_bytes_to_advance = 2; return string.Format("BMI ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0x31: out_bytes_to_advance = 2; return string.Format("AND (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0x34: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2},X", ReadMemory(++in_pc));
        case 0x35: out_bytes_to_advance = 2; return string.Format("AND ${0:X2},X", ReadMemory(++in_pc));
        case 0x36: out_bytes_to_advance = 2; return string.Format("ROL ${0:X2},X", ReadMemory(++in_pc));
        case 0x38: out_bytes_to_advance = 1; return "SEC";
        case 0x39: out_bytes_to_advance = 3; return string.Format("AND ${0:X4},Y *", ReadWord(++in_pc));
        case 0x3A: out_bytes_to_advance = 1; return "NOP";
        case 0x3C: out_bytes_to_advance = 2; return string.Format("NOP (${0:X2},X)", ReadMemory(++in_pc));
        case 0x3D: out_bytes_to_advance = 3; return string.Format("AND ${0:X4},X *", ReadWord(++in_pc));
        case 0x3E: out_bytes_to_advance = 3; return string.Format("ROL ${0:X4},X", ReadWord(++in_pc));
        case 0x40: out_bytes_to_advance = 1; return "RTI";
        case 0x41: out_bytes_to_advance = 2; return string.Format("EOR (${0:X2},X)", ReadMemory(++in_pc));
        case 0x44: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2}", ReadMemory(++in_pc));
        case 0x45: out_bytes_to_advance = 2; return string.Format("EOR ${0:X2}", ReadMemory(++in_pc));
        case 0x46: out_bytes_to_advance = 2; return string.Format("LSR ${0:X2}", ReadMemory(++in_pc));
        case 0x48: out_bytes_to_advance = 1; return "PHA";
        case 0x49: out_bytes_to_advance = 2; return string.Format("EOR #${0:X2}", ReadMemory(++in_pc));
        case 0x4A: out_bytes_to_advance = 1; return "LSR A";
        case 0x4C: out_bytes_to_advance = 3; return string.Format("JMP ${0:X4}", ReadWord(++in_pc));
        case 0x4D: out_bytes_to_advance = 3; return string.Format("EOR ${0:X4}", ReadWord(++in_pc));
        case 0x4E: out_bytes_to_advance = 3; return string.Format("LSR ${0:X4}", ReadWord(++in_pc));
        case 0x50: out_bytes_to_advance = 2; return string.Format("BVC ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0x51: out_bytes_to_advance = 2; return string.Format("EOR (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0x54: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2},X", ReadMemory(++in_pc));
        case 0x55: out_bytes_to_advance = 2; return string.Format("EOR ${0:X2},X", ReadMemory(++in_pc));
        case 0x56: out_bytes_to_advance = 2; return string.Format("LSR ${0:X2},X", ReadMemory(++in_pc));
        case 0x58: out_bytes_to_advance = 1; return "CLI";
        case 0x59: out_bytes_to_advance = 3; return string.Format("EOR ${0:X4},Y *", ReadWord(++in_pc));
        case 0x5A: out_bytes_to_advance = 1; return "NOP";
        case 0x5C: out_bytes_to_advance = 2; return string.Format("NOP (${0:X2},X)", ReadMemory(++in_pc));
        case 0x5D: out_bytes_to_advance = 3; return string.Format("EOR ${0:X4},X *", ReadWord(++in_pc));
        case 0x5E: out_bytes_to_advance = 3; return string.Format("LSR ${0:X4},X", ReadWord(++in_pc));
        case 0x60: out_bytes_to_advance = 1; return "RTS";
        case 0x61: out_bytes_to_advance = 2; return string.Format("ADC (${0:X2},X)", ReadMemory(++in_pc));
        case 0x64: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2}", ReadMemory(++in_pc));
        case 0x65: out_bytes_to_advance = 2; return string.Format("ADC ${0:X2}", ReadMemory(++in_pc));
        case 0x66: out_bytes_to_advance = 2; return string.Format("ROR ${0:X2}", ReadMemory(++in_pc));
        case 0x68: out_bytes_to_advance = 1; return "PLA";
        case 0x69: out_bytes_to_advance = 2; return string.Format("ADC #${0:X2}", ReadMemory(++in_pc));
        case 0x6A: out_bytes_to_advance = 1; return "ROR A";
        case 0x6C: out_bytes_to_advance = 3; return string.Format("JMP (${0:X4})", ReadWord(++in_pc));
        case 0x6D: out_bytes_to_advance = 3; return string.Format("ADC ${0:X4}", ReadWord(++in_pc));
        case 0x6E: out_bytes_to_advance = 3; return string.Format("ROR ${0:X4}", ReadWord(++in_pc));
        case 0x70: out_bytes_to_advance = 2; return string.Format("BVS ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0x71: out_bytes_to_advance = 2; return string.Format("ADC (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0x74: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2},X", ReadMemory(++in_pc));
        case 0x75: out_bytes_to_advance = 2; return string.Format("ADC ${0:X2},X", ReadMemory(++in_pc));
        case 0x76: out_bytes_to_advance = 2; return string.Format("ROR ${0:X2},X", ReadMemory(++in_pc));
        case 0x78: out_bytes_to_advance = 1; return "SEI";
        case 0x79: out_bytes_to_advance = 3; return string.Format("ADC ${0:X4},Y *", ReadWord(++in_pc));
        case 0x7A: out_bytes_to_advance = 1; return "NOP";
        case 0x7C: out_bytes_to_advance = 2; return string.Format("NOP (${0:X2},X)", ReadMemory(++in_pc));
        case 0x7D: out_bytes_to_advance = 3; return string.Format("ADC ${0:X4},X *", ReadWord(++in_pc));
        case 0x7E: out_bytes_to_advance = 3; return string.Format("ROR ${0:X4},X", ReadWord(++in_pc));
        case 0x80: out_bytes_to_advance = 2; return string.Format("NOP #${0:X2}", ReadMemory(++in_pc));
        case 0x81: out_bytes_to_advance = 2; return string.Format("STA (${0:X2},X)", ReadMemory(++in_pc));
        case 0x82: out_bytes_to_advance = 2; return string.Format("NOP #${0:X2}", ReadMemory(++in_pc));
        case 0x84: out_bytes_to_advance = 2; return string.Format("STY ${0:X2}", ReadMemory(++in_pc));
        case 0x85: out_bytes_to_advance = 2; return string.Format("STA ${0:X2}", ReadMemory(++in_pc));
        case 0x86: out_bytes_to_advance = 2; return string.Format("STX ${0:X2}", ReadMemory(++in_pc));
        case 0x88: out_bytes_to_advance = 1; return "DEY";
        case 0x89: out_bytes_to_advance = 2; return string.Format("NOP #${0:X2}", ReadMemory(++in_pc));
        case 0x8A: out_bytes_to_advance = 1; return "TXA";
        case 0x8C: out_bytes_to_advance = 3; return string.Format("STY ${0:X4}", ReadWord(++in_pc));
        case 0x8D: out_bytes_to_advance = 3; return string.Format("STA ${0:X4}", ReadWord(++in_pc));
        case 0x8E: out_bytes_to_advance = 3; return string.Format("STX ${0:X4}", ReadWord(++in_pc));
        case 0x90: out_bytes_to_advance = 2; return string.Format("BCC ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0x91: out_bytes_to_advance = 2; return string.Format("STA (${0:X2}),Y", ReadMemory(++in_pc));
        case 0x94: out_bytes_to_advance = 2; return string.Format("STY ${0:X2},X", ReadMemory(++in_pc));
        case 0x95: out_bytes_to_advance = 2; return string.Format("STA ${0:X2},X", ReadMemory(++in_pc));
        case 0x96: out_bytes_to_advance = 2; return string.Format("STX ${0:X2},Y", ReadMemory(++in_pc));
        case 0x98: out_bytes_to_advance = 1; return "TYA";
        case 0x99: out_bytes_to_advance = 3; return string.Format("STA ${0:X4},Y", ReadWord(++in_pc));
        case 0x9A: out_bytes_to_advance = 1; return "TXS";
        case 0x9D: out_bytes_to_advance = 3; return string.Format("STA ${0:X4},X", ReadWord(++in_pc));
        case 0xA0: out_bytes_to_advance = 2; return string.Format("LDY #${0:X2}", ReadMemory(++in_pc));
        case 0xA1: out_bytes_to_advance = 2; return string.Format("LDA (${0:X2},X)", ReadMemory(++in_pc));
        case 0xA2: out_bytes_to_advance = 2; return string.Format("LDX #${0:X2}", ReadMemory(++in_pc));
        case 0xA4: out_bytes_to_advance = 2; return string.Format("LDY ${0:X2}", ReadMemory(++in_pc));
        case 0xA5: out_bytes_to_advance = 2; return string.Format("LDA ${0:X2}", ReadMemory(++in_pc));
        case 0xA6: out_bytes_to_advance = 2; return string.Format("LDX ${0:X2}", ReadMemory(++in_pc));
        case 0xA8: out_bytes_to_advance = 1; return "TAY";
        case 0xA9: out_bytes_to_advance = 2; return string.Format("LDA #${0:X2}", ReadMemory(++in_pc));
        case 0xAA: out_bytes_to_advance = 1; return "TAX";
        case 0xAC: out_bytes_to_advance = 3; return string.Format("LDY ${0:X4}", ReadWord(++in_pc));
        case 0xAD: out_bytes_to_advance = 3; return string.Format("LDA ${0:X4}", ReadWord(++in_pc));
        case 0xAE: out_bytes_to_advance = 3; return string.Format("LDX ${0:X4}", ReadWord(++in_pc));
        case 0xB0: out_bytes_to_advance = 2; return string.Format("BCS ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0xB1: out_bytes_to_advance = 2; return string.Format("LDA (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0xB4: out_bytes_to_advance = 2; return string.Format("LDY ${0:X2},X", ReadMemory(++in_pc));
        case 0xB5: out_bytes_to_advance = 2; return string.Format("LDA ${0:X2},X", ReadMemory(++in_pc));
        case 0xB6: out_bytes_to_advance = 2; return string.Format("LDX ${0:X2},Y", ReadMemory(++in_pc));
        case 0xB8: out_bytes_to_advance = 1; return "CLV";
        case 0xB9: out_bytes_to_advance = 3; return string.Format("LDA ${0:X4},Y *", ReadWord(++in_pc));
        case 0xBA: out_bytes_to_advance = 1; return "TSX";
        case 0xBC: out_bytes_to_advance = 3; return string.Format("LDY ${0:X4},X *", ReadWord(++in_pc));
        case 0xBD: out_bytes_to_advance = 3; return string.Format("LDA ${0:X4},X *", ReadWord(++in_pc));
        case 0xBE: out_bytes_to_advance = 3; return string.Format("LDX ${0:X4},Y *", ReadWord(++in_pc));
        case 0xC0: out_bytes_to_advance = 2; return string.Format("CPY #${0:X2}", ReadMemory(++in_pc));
        case 0xC1: out_bytes_to_advance = 2; return string.Format("CMP (${0:X2},X)", ReadMemory(++in_pc));
        case 0xC2: out_bytes_to_advance = 2; return string.Format("NOP #${0:X2}", ReadMemory(++in_pc));
        case 0xC4: out_bytes_to_advance = 2; return string.Format("CPY ${0:X2}", ReadMemory(++in_pc));
        case 0xC5: out_bytes_to_advance = 2; return string.Format("CMP ${0:X2}", ReadMemory(++in_pc));
        case 0xC6: out_bytes_to_advance = 2; return string.Format("DEC ${0:X2}", ReadMemory(++in_pc));
        case 0xC8: out_bytes_to_advance = 1; return "INY";
        case 0xC9: out_bytes_to_advance = 2; return string.Format("CMP #${0:X2}", ReadMemory(++in_pc));
        case 0xCA: out_bytes_to_advance = 1; return "DEX";
        case 0xCC: out_bytes_to_advance = 3; return string.Format("CPY ${0:X4}", ReadWord(++in_pc));
        case 0xCD: out_bytes_to_advance = 3; return string.Format("CMP ${0:X4}", ReadWord(++in_pc));
        case 0xCE: out_bytes_to_advance = 3; return string.Format("DEC ${0:X4}", ReadWord(++in_pc));
        case 0xD0: out_bytes_to_advance = 2; return string.Format("BNE ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0xD1: out_bytes_to_advance = 2; return string.Format("CMP (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0xD4: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2},X", ReadMemory(++in_pc));
        case 0xD5: out_bytes_to_advance = 2; return string.Format("CMP ${0:X2},X", ReadMemory(++in_pc));
        case 0xD6: out_bytes_to_advance = 2; return string.Format("DEC ${0:X2},X", ReadMemory(++in_pc));
        case 0xD8: out_bytes_to_advance = 1; return "CLD";
        case 0xD9: out_bytes_to_advance = 3; return string.Format("CMP ${0:X4},Y *", ReadWord(++in_pc));
        case 0xDA: out_bytes_to_advance = 1; return "NOP";
        case 0xDC: out_bytes_to_advance = 2; return string.Format("NOP (${0:X2},X)", ReadMemory(++in_pc));
        case 0xDD: out_bytes_to_advance = 3; return string.Format("CMP ${0:X4},X *", ReadWord(++in_pc));
        case 0xDE: out_bytes_to_advance = 3; return string.Format("DEC ${0:X4},X", ReadWord(++in_pc));
        case 0xE0: out_bytes_to_advance = 2; return string.Format("CPX #${0:X2}", ReadMemory(++in_pc));
        case 0xE1: out_bytes_to_advance = 2; return string.Format("SBC (${0:X2},X)", ReadMemory(++in_pc));
        case 0xE2: out_bytes_to_advance = 2; return string.Format("NOP #${0:X2}", ReadMemory(++in_pc));
        case 0xE4: out_bytes_to_advance = 2; return string.Format("CPX ${0:X2}", ReadMemory(++in_pc));
        case 0xE5: out_bytes_to_advance = 2; return string.Format("SBC ${0:X2}", ReadMemory(++in_pc));
        case 0xE6: out_bytes_to_advance = 2; return string.Format("INC ${0:X2}", ReadMemory(++in_pc));
        case 0xE8: out_bytes_to_advance = 1; return "INX";
        case 0xE9: out_bytes_to_advance = 2; return string.Format("SBC #${0:X2}", ReadMemory(++in_pc));
        case 0xEA: out_bytes_to_advance = 1; return "NOP";
        case 0xEC: out_bytes_to_advance = 3; return string.Format("CPX ${0:X4}", ReadWord(++in_pc));
        case 0xED: out_bytes_to_advance = 3; return string.Format("SBC ${0:X4}", ReadWord(++in_pc));
        case 0xEE: out_bytes_to_advance = 3; return string.Format("INC ${0:X4}", ReadWord(++in_pc));
        case 0xF0: out_bytes_to_advance = 2; return string.Format("BEQ ${0:X4}", in_pc + 2 + (sbyte)ReadMemory(++in_pc));
        case 0xF1: out_bytes_to_advance = 2; return string.Format("SBC (${0:X2}),Y *", ReadMemory(++in_pc));
        case 0xF4: out_bytes_to_advance = 2; return string.Format("NOP ${0:X2},X", ReadMemory(++in_pc));
        case 0xF5: out_bytes_to_advance = 2; return string.Format("SBC ${0:X2},X", ReadMemory(++in_pc));
        case 0xF6: out_bytes_to_advance = 2; return string.Format("INC ${0:X2},X", ReadMemory(++in_pc));
        case 0xF8: out_bytes_to_advance = 1; return "SED";
        case 0xF9: out_bytes_to_advance = 3; return string.Format("SBC ${0:X4},Y *", ReadWord(++in_pc));
        case 0xFA: out_bytes_to_advance = 1; return "NOP";
        case 0xFC: out_bytes_to_advance = 2; return string.Format("NOP (${0:X2},X)", ReadMemory(++in_pc));
        case 0xFD: out_bytes_to_advance = 3; return string.Format("SBC ${0:X4},X *", ReadWord(++in_pc));
        case 0xFE: out_bytes_to_advance = 3; return string.Format("INC ${0:X4},X", ReadWord(++in_pc));
      }
      out_bytes_to_advance = 1;
      return "???";
    }
    #endregion 

  }
}
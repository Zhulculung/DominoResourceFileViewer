/*****************************************************************************/
/* Binary data to I8080 disassembly converter                                */
/*                                                                           */
/* Copyright (C) 2013 Laszlo Arvai                                           */
/* All rights reserved.                                                      */
/*                                                                           */
/* This software may be modified and distributed under the terms             */
/* of the BSD license.  See the LICENSE file for details.                    */
/*****************************************************************************/
using System.Text;
using System.Windows.Forms;

namespace DominoResourceFileViewer
{
	/// <summary>
	/// Displays byte buffer content as Intel 8080 disassembled list
	/// </summary>
	public class dispI8080Disassembler : IBinaryViewer
	{
		#region · Data members ·
		private byte[] m_buffer;
		private int m_pc;
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
			return "I8080 Disassembler";
		}

		#endregion

		#region · Disassembler functions ·

		/// <summary>
		/// Disassembles one isntruction from the given address
		/// </summary>
		/// <param name="in_pc"></param>uint16
		/// <param name="out_bytes_to_advance"></param>
		/// <returns></returns>
		private string DisassembleOneInstruction(ushort in_pc, out int out_bytes_to_advance)
		{
			byte opcode = m_buffer[in_pc];
			string imm8;
			string imm16;
			string cmd = instructions[opcode];

			out_bytes_to_advance = 1;

			// get 8 bit operand
			if (in_pc + 1 < m_buffer.Length)
			{
				imm8 = (m_buffer[in_pc + 1]).ToString("X2") + "h";
			}
			else
			{
				imm8 = "??";
			}

			// get 16 bit operand
			if (in_pc + 2 < m_buffer.Length)
			{
				imm16 = ((m_buffer[in_pc + 1] | (m_buffer[in_pc + 2] << 8))).ToString("X2") + "h";
			}
			else
			{
				imm16 = "????";
			}

			// check for 16 bit operand
			int pos = cmd.IndexOf("xxxx");
			if (pos >= 0)
			{
				cmd = cmd.Substring(0, pos) + imm16 + cmd.Substring(pos + 4);
				out_bytes_to_advance += 2;
			}

			pos = cmd.IndexOf("xx");
			if (pos >= 0)
			{
				cmd = cmd.Substring(0, pos) + imm8 + cmd.Substring(pos + 2);
				out_bytes_to_advance += 1;
			}


			return cmd;
		}
		#endregion

		#region · Disassembler table

		/// <summary>
		/// Disassembly table
		/// </summary>
		private readonly string[] instructions = 
    {
			"NOP","LXI B,xxxx","STAX B","INX B", 
			"INR B","DCR B","MVI B,xx","RLC", 
			"NOP?","DAD B","LDAX B","DCX B", 
			"INR C","DCR C","MVI C,xx","RRC", 
			"NOP?","LXI D,xxxx","STAX D","INX D", 
			"INR D","DCR D","MVI D,xx","RAL", 
			"NOP?","DAD D","LDAX D","DCX D", 
			"INR E","DCR E","MVI E,xx","RAR", 
			"RIM","LXI H,xxxx","SHLD xxxx","INX H", 
			"INR H","DCR H","MVI H,xx","DAA", 
			"NOP?","DAD H","LHLD xxxx","DCX H", 
			"INR L","DCR L","MVI L,xx","CMA", 
			"SIM","LXI SP,xxxx","STA xxxx","INX SP", 
			"INR M","DCR M","MVI M,xx","STC", 
			"NOP?","DAD SP","LDA xxxx","DCX SP", 
			"INR A","DCR A","MVI A,xx","CMC", 
			"MOV B,B","MOV B,C","MOV B,D","MOV B,E", 
			"MOV B,H","MOV B,L","MOV B,M","MOV B,A", 
			"MOV C,B","MOV C,C","MOV C,D","MOV C,E", 
			"MOV C,H","MOV C,L","MOV C,M","MOV C,A", 
			"MOV D,B","MOV D,C","MOV D,D","MOV D,E", 
			"MOV D,H","MOV D,L","MOV D,M","MOV D,A", 
			"MOV E,B","MOV E,C","MOV E,D","MOV E,E", 
			"MOV E,H","MOV E,L","MOV E,M","MOV E,A", 
			"MOV H,B","MOV H,C","MOV H,D","MOV H,E", 
			"MOV H,H","MOV H,L","MOV H,M","MOV H,A", 
			"MOV L,B","MOV L,C","MOV L,D","MOV L,E", 
			"MOV L,H","MOV L,L","MOV L,M","MOV L,A",
			"MOV M,B","MOV M,C","MOV M,D","MOV M,E", 
			"MOV M,H","MOV M,L","HLT","MOV M,A", 
			"MOV A,B","MOV A,C","MOV A,D","MOV A,E", 
			"MOV A,H","MOV A,L","MOV A,M","MOV A,A", 
			"ADD B","ADD C","ADD D","ADD E", 
			"ADD H","ADD L","ADD M","ADD A", 
			"ADC B","ADC C","ADC D","ADC E", 
			"ADC H","ADC L","ADC M","ADC A", 
			"SUB B","SUB C","SUB D","SUB E", 
			"SUB H","SUB L","SUB M","SUB A", 
			"SBB B","SBB C","SBB D","SBB E", 
			"SBB H","SBB L","SBB M","SBB A", 
			"ANA B","ANA C","ANA D","ANA E", 
			"ANA H","ANA L","ANA M","ANA A", 
			"XRA B","XRA C","XRA D","XRA E", 
			"XRA H","XRA L","XRA M","XRA A", 
			"ORA B","ORA C","ORA D","ORA E", 
			"ORA H","ORA L","ORA M","ORA A", 
			"CMP B","CMP C","CMP D","CMP E", 
			"CMP H","CMP L","CMP M","CMP A", 
			"RNZ","POP B","JNZ xxxx","JMP xxxx",
			"CNZ xxxx","PUSH B","ADI xx","RST 0", 
			"RZ","RET","JZ xxxx","NOP?", 
			"CZ xxxx","CALL xxxx","ACI xx","RST 1", 
			"RNC","POP D","JNC xxxx","OUT xx", 
			"CNC xxxx","PUSH D","SUI xx","RST 2", 
			"RC","NOP?","JC xxxx","IN xx", 
			"CC xxxx","NOP?","SBI xx","RST 3", 
			"RPO","POP H","JPO xxxx","XTHL", 
			"CPO xxxx","PUSH H","ANI xx","RST 4", 
			"RPE","PCHL","JPE xxxx","XCHG","CPE xxxx",
			"NOP?","XRI xx","RST 5", 
			"RP","POP PSW","JP xxxx","DI", 
			"CP xxxx","PUSH PSW","ORI xx","RST 6", 
			"RM","SPHL","JM xxxx","EI", 
			"CM xxxx","NOP?","CPI xx","RST 7"
		};

		#endregion
	}
}
/*
const string[] instructions = {
 
 
byte opcode = m_buffer[in_pc];
string imm8 = (m_buffer[in_pc + 1]).ToString("x2") + "h";
string imm16 = ((m_buffer[in_pc + 1] | (m_buffer[in_pc + 2] << 8))).ToString("x2") + "h";
string cmd;

switch (opcode)
{
		case 0x00: cmd = "NOP"; out_bytes_to_advance = 1; break;
		case 0x08: cmd = "NOP?"; out_bytes_to_advance = 1; break;
		case 0x10: cmd = "NOP?"; out_bytes_to_advance = 1; break;
		case 0x20: cmd = "NOP?"; out_bytes_to_advance = 1; break;
		case 0x18: cmd = "NOP?"; out_bytes_to_advance = 1; break;
		case 0x28: cmd = "NOP?"; out_bytes_to_advance = 1; break;
		case 0x30: cmd = "NOP?"; out_bytes_to_advance = 1; break;
		case 0x38: cmd = "NOP?"; out_bytes_to_advance = 1; break;

		case 0x01: out_bytes_to_advance = 3; return "LXI B," + imm16;
		case 0x02: out_bytes_to_advance = 1; return "STAX B";
		case 0x03: out_bytes_to_advance = 1; return "INX B";
		case 0x04: out_bytes_to_advance = 1; return "INR B";
		case 0x05: out_bytes_to_advance = 1; return "DCR B";
		case 0x06: out_bytes_to_advance = 2; return "MVI B," + imm8;
		case 0x07: out_bytes_to_advance = 1; return "RLC";
		case 0x09: out_bytes_to_advance = 1; return "DAD B";
		case 0x0a: out_bytes_to_advance = 1; return "LDAX B";
		case 0x0b: out_bytes_to_advance = 1; return "DCX B";
		case 0x0c: out_bytes_to_advance = 1; return "INR C";
		case 0x0d: out_bytes_to_advance = 1; return "DCR C";
		case 0x0e: out_bytes_to_advance = 2; return "MVI C" + imm8; ;
		case 0x0f: out_bytes_to_advance = 1; return "RRC";

		case 0x11: out_bytes_to_advance = 3; return "LXI D," + imm16;
		case 0x12: out_bytes_to_advance = 1; return "STAX D";
		case 0x13: out_bytes_to_advance = 1; return "INX D";
		case 0x14: out_bytes_to_advance = 1; return "INR D";
		case 0x15: out_bytes_to_advance = 1; return "DCR D";
		case 0x16: out_bytes_to_advance = 2; return "MVI D," + imm8;
		case 0x17: out_bytes_to_advance = 1; return "RAL";
		case 0x19: out_bytes_to_advance = 1; return "DAD D";
		case 0x1a: out_bytes_to_advance = 1; return "LDAX D";
		case 0x1b: out_bytes_to_advance = 1; return "DCX D";
		case 0x1c: out_bytes_to_advance = 1; return "INR E";
		case 0x1d: out_bytes_to_advance = 1; return "DCR E";
		case 0x1e: out_bytes_to_advance = 2; return "MVI E" + imm8;
		case 0x1f: out_bytes_to_advance = 1; return "RAR";

		case 0x21: out_bytes_to_advance = 3; return "LXI H," + imm16;
		case 0x22: out_bytes_to_advance = 3; return "SHLD " + imm16;
		case 0x23: return "INX"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x24: return "INR"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x25: return "DCR"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x26: return "MVI"; out_bytes_to_advance = 2; arg1 = "H"; arg2 = imm8; break;
		case 0x27: return "DAA"; out_bytes_to_advance = 1; break;
		case 0x29: return "DAD"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x2a: return "LHLD"; out_bytes_to_advance = 3; arg1 = imm16; data1 = true; break;
		case 0x2b: return "DCX"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x2c: return "INR"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0x2d: return "DCR"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0x2e: return "MVI"; out_bytes_to_advance = 2; arg1 = "L"; arg2 = imm8; break;
		case 0x2f: return "CMA"; out_bytes_to_advance = 1; break;

		case 0x31: return "LXI"; out_bytes_to_advance = 3; arg1 = "SP"; arg2 = imm16; data2 = true; break;
		case 0x32: return "STA"; out_bytes_to_advance = 3; arg1 = imm16; data1 = true; break;
		case 0x33: return "INX"; out_bytes_to_advance = 1; arg1 = "SP"; break;
		case 0x34: return "INR"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0x35: return "DCR"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0x36: return "MVI"; out_bytes_to_advance = 2; arg1 = "M"; arg2 = imm8; break;
		case 0x37: return "STC"; out_bytes_to_advance = 1; break;
		case 0x39: return "DAD"; out_bytes_to_advance = 1; arg1 = "SP"; break;
		case 0x3a: return "LDA"; out_bytes_to_advance = 3; arg1 = imm16; data1 = true; break;
		case 0x3b: return "DCX"; out_bytes_to_advance = 1; arg1 = "SP"; break;
		case 0x3c: return "INR"; out_bytes_to_advance = 1; arg1 = "A"; break;
		case 0x3d: return "DCR"; out_bytes_to_advance = 1; arg1 = "A"; break;
		case 0x3e: return "MVI"; out_bytes_to_advance = 2; arg1 = "A"; arg2 = imm8; break;
		case 0x3f: return "CMC"; out_bytes_to_advance = 1; break;

		case 0x76: return "HLT"; out_bytes_to_advance = 1; break;

		case 0xc3: return "JMP"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xcb: return "JMP?"; out_bytes_to_advance = 3; arg1 = imm16; code = true; bad = true; break;

		case 0xcd: return "CALL"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xfd: return "CALL?"; out_bytes_to_advance = 3; arg1 = imm16; code = true; bad = true; break;

		case 0xc9: return "RET"; out_bytes_to_advance = 1; break;

		case 0x40: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "B"; break;
		case 0x41: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "C"; break;
		case 0x42: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "D"; break;
		case 0x43: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "E"; break;
		case 0x44: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "H"; break;
		case 0x45: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "L"; break;
		case 0x46: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "M"; break;
		case 0x47: return "MOV"; out_bytes_to_advance = 1; arg1 = "B"; arg2 = "A"; break;
		case 0x48: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "B"; break;
		case 0x49: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "C"; break;
		case 0x4a: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "D"; break;
		case 0x4b: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "E"; break;
		case 0x4c: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "H"; break;
		case 0x4d: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "L"; break;
		case 0x4e: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "M"; break;
		case 0x4f: return "MOV"; out_bytes_to_advance = 1; arg1 = "C"; arg2 = "A"; break;

		case 0x50: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "B"; break;
		case 0x51: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "C"; break;
		case 0x52: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "D"; break;
		case 0x53: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "E"; break;
		case 0x54: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "H"; break;
		case 0x55: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "L"; break;
		case 0x56: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "M"; break;
		case 0x57: return "MOV"; out_bytes_to_advance = 1; arg1 = "D"; arg2 = "A"; break;
		case 0x58: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "B"; break;
		case 0x59: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "C"; break;
		case 0x5a: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "D"; break;
		case 0x5b: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "E"; break;
		case 0x5c: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "H"; break;
		case 0x5d: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "L"; break;
		case 0x5e: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "M"; break;
		case 0x5f: return "MOV"; out_bytes_to_advance = 1; arg1 = "E"; arg2 = "A"; break;

		case 0x60: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "B"; break;
		case 0x61: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "C"; break;
		case 0x62: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "D"; break;
		case 0x63: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "E"; break;
		case 0x64: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "H"; break;
		case 0x65: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "L"; break;
		case 0x66: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "M"; break;
		case 0x67: return "MOV"; out_bytes_to_advance = 1; arg1 = "H"; arg2 = "A"; break;
		case 0x68: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "B"; break;
		case 0x69: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "C"; break;
		case 0x6a: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "D"; break;
		case 0x6b: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "E"; break;
		case 0x6c: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "H"; break;
		case 0x6d: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "L"; break;
		case 0x6e: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "M"; break;
		case 0x6f: return "MOV"; out_bytes_to_advance = 1; arg1 = "L"; arg2 = "A"; break;

		case 0x70: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "B"; break;
		case 0x71: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "C"; break;
		case 0x72: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "D"; break;
		case 0x73: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "E"; break;
		case 0x74: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "H"; break;
		case 0x75: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "L"; break;

		case 0x0f: return "HLT"; out_bytes_to_advance = 1; break;

		case 0x77: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "A"; break;
		case 0x78: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "B"; break;
		case 0x79: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "C"; break;
		case 0x7a: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "D"; break;
		case 0x7b: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "E"; break;
		case 0x7c: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "H"; break;
		case 0x7d: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "L"; break;
		case 0x7e: return "MOV"; out_bytes_to_advance = 1; arg1 = "A"; arg2 = "M"; break;
		case 0x7f: return "MOV"; out_bytes_to_advance = 1; arg1 = "M"; arg2 = "M"; break;

		case 0x80: return "ADD"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0x81: return "ADD"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0x82: return "ADD"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0x83: return "ADD"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0x84: return "ADD"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x85: return "ADD"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0x86: return "ADD"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0x87: return "ADD"; out_bytes_to_advance = 1; arg1 = "A"; break;
		case 0x88: return "ADC"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0x89: return "ADC"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0x8a: return "ADC"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0x8b: return "ADC"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0x8c: return "ADC"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x8d: return "ADC"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0x8e: return "ADC"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0x8f: return "ADC"; out_bytes_to_advance = 1; arg1 = "A"; break;

		case 0x90: return "SUB"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0x91: return "SUB"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0x92: return "SUB"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0x93: return "SUB"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0x94: return "SUB"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x95: return "SUB"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0x96: return "SUB"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0x97: return "SUB"; out_bytes_to_advance = 1; arg1 = "A"; break;
		case 0x98: return "SBB"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0x99: return "SBB"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0x9a: return "SBB"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0x9b: return "SBB"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0x9c: return "SBB"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0x9d: return "SBB"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0x9e: return "SBB"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0x9f: return "SBB"; out_bytes_to_advance = 1; arg1 = "A"; break;

		case 0xa0: return "ANA"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0xa1: return "ANA"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0xa2: return "ANA"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0xa3: return "ANA"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0xa4: return "ANA"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0xa5: return "ANA"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0xa6: return "ANA"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0xa7: return "ANA"; out_bytes_to_advance = 1; arg1 = "A"; break;
		case 0xa8: return "XRA"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0xa9: return "XRA"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0xaa: return "XRA"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0xab: return "XRA"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0xac: return "XRA"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0xad: return "XRA"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0xae: return "XRA"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0xaf: return "XRA"; out_bytes_to_advance = 1; arg1 = "A"; break;

		case 0xb0: return "ORA"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0xb1: return "ORA"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0xb2: return "ORA"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0xb3: return "ORA"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0xb4: return "ORA"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0xb5: return "ORA"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0xb6: return "ORA"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0xb7: return "ORA"; out_bytes_to_advance = 1; arg1 = "A"; break;
		case 0xb8: return "CMP"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0xb9: return "CMP"; out_bytes_to_advance = 1; arg1 = "C"; break;
		case 0xba: return "CMP"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0xbb: return "CMP"; out_bytes_to_advance = 1; arg1 = "E"; break;
		case 0xbc: return "CMP"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0xbd: return "CMP"; out_bytes_to_advance = 1; arg1 = "L"; break;
		case 0xbe: return "CMP"; out_bytes_to_advance = 1; arg1 = "M"; break;
		case 0xbf: return "CMP"; out_bytes_to_advance = 1; arg1 = "A"; break;

		case 0xc0: return "RNZ"; out_bytes_to_advance = 1; break;
		case 0xc1: return "POP"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0xc2: return "JNZ"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;

		case 0xc3: return "JMP"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xcb: return "JMP?"; out_bytes_to_advance = 3; arg1 = imm16; code = true; bad = true; break;

		case 0xc4: return "CNZ"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xc5: return "PUSH"; out_bytes_to_advance = 1; arg1 = "B"; break;
		case 0xc6: return "ADI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xc7: return "RST"; out_bytes_to_advance = 1; arg1 = "0"; break;
		case 0xc8: return "RZ"; out_bytes_to_advance = 1; break;

		case 0xc9: return "RET"; out_bytes_to_advance = 1; break;
		case 0xd9: return "RET?"; out_bytes_to_advance = 1; bad = true; break;

		case 0xca: return "JZ"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xcc: return "CZ"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;

		case 0xcd: return "CALL"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xdd: return "CALL?"; out_bytes_to_advance = 3; arg1 = imm16; code = true; bad = true; break;
		case 0xed: return "CALL?"; out_bytes_to_advance = 3; arg1 = imm16; code = true; bad = true; break;
		case 0xfd: return "CALL?"; out_bytes_to_advance = 3; arg1 = imm16; code = true; bad = true; break;

		case 0xce: return "ACI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xcf: return "RST"; out_bytes_to_advance = 1; arg1 = "1"; break;
		case 0xd0: return "RNC"; out_bytes_to_advance = 1; break;
		case 0xd1: return "POP"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0xd2: return "JNC"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xd3: return "OUT"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xd4: return "CNC"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xd5: return "PUSH"; out_bytes_to_advance = 1; arg1 = "D"; break;
		case 0xd6: return "SUI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xd7: return "RST"; out_bytes_to_advance = 1; arg1 = "2"; break;
		case 0xd8: return "RC"; out_bytes_to_advance = 1; break;
		case 0xda: return "JC"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xdb: return "IN"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xdc: return "CC"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xde: return "SBI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xdf: return "RST"; out_bytes_to_advance = 1; arg1 = "3"; break;
		case 0xe0: return "RPO"; out_bytes_to_advance = 1; break;
		case 0xe1: return "POP"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0xe2: return "JPO"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xe3: return "XTXL"; out_bytes_to_advance = 1; break;
		case 0xe4: return "CPO"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xe5: return "PUSH"; out_bytes_to_advance = 1; arg1 = "H"; break;
		case 0xe6: return "ANI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xe7: return "RST"; out_bytes_to_advance = 1; arg1 = "4"; break;
		case 0xe8: return "RPE"; out_bytes_to_advance = 1; break;
		case 0xe9: return "PCHL"; out_bytes_to_advance = 1; break;
		case 0xea: return "JPE"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xeb: return "XCHG"; out_bytes_to_advance = 1; break;
		case 0xec: return "CPE"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xee: return "XRI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xef: return "RST"; out_bytes_to_advance = 1; arg1 = "5"; break;
		case 0xf0: return "RP"; out_bytes_to_advance = 1; break;
		case 0xf1: return "POP"; out_bytes_to_advance = 1; arg1 = "PSW"; break;
		case 0xf2: return "JP"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xf3: return "DI"; out_bytes_to_advance = 1; break;
		case 0xf4: return "CP"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xf5: return "PUSH"; out_bytes_to_advance = 1; arg1 = "PSW"; break;
		case 0xf6: return "ORI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xf7: return "RST"; out_bytes_to_advance = 1; arg1 = "6"; break;
		case 0xf8: return "RM"; out_bytes_to_advance = 1; break;
		case 0xf9: return "SPHL"; out_bytes_to_advance = 1; break;
		case 0xfa: return "JM"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xfb: return "EI"; out_bytes_to_advance = 1; break;
		case 0xfc: return "CM"; out_bytes_to_advance = 3; arg1 = imm16; code = true; break;
		case 0xfe: return "CPI"; out_bytes_to_advance = 2; arg1 = imm8; break;
		case 0xff: return "RST"; out_bytes_to_advance = 1; arg1 = "7"; break;
}

 */ 




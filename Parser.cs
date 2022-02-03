using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class Parser
    {
        public string[] lines { get; set; }
        public List<string> text = new List<string>();
        public List<string> bitss = new List<string>();
        public Parser()
        {
            ReadFile("hej.txt");
            SymbolTable symbolTable = new SymbolTable();
            int varnum = 16;
            Code code = new Code();
            
            for (int i = 0; i < lines.Length; i++)
            {
                text.Add(lines[i].Trim());
            }
            for (int i = text.Count-1; i >= 0 ; i--)
            {
               
                if (text[i].StartsWith("//")==true|| text[i] == "")
                {
                    text.RemoveAt(i);
                }
                else if(text[i].Contains("//"))
                {
                    text[i] = text[i].Remove(text[i].IndexOf("/")).Trim();
                }



            }
            //checks for loops and inserts it into symboltabel
            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].StartsWith('(')==true)
                {
                    var c = text[i].Remove(text[i].IndexOf(")"));
                    if (!symbolTable.Contains(c[1..]))
                    {
                        symbolTable.AddEntry(c[1..], i + 1);

                    }
                }
                
            }
            for (int i = 0; i < text.Count; i++)
            {
                string start = text[i].Substring(0,1);
                string bits;
                switch (start)
                {
                    case "@":
                        if (!int.TryParse(text[i][1..], out int result))
                        {
                            if (!symbolTable.Contains(text[i][1..]))
                            {
                                symbolTable.AddEntry(text[i][1..], varnum);
                                varnum++;
                            }
                            int temp = symbolTable.GetAddress(text[i][1..]);
                            bits = Convert.ToString(temp, 2).PadLeft(16, '0');
                        }
                        else
                        {
                            if (!symbolTable.Contains(text[i][1..]))
                            {
                                symbolTable.AddEntry(text[i][1..], result);
                            }
                            result = symbolTable.GetAddress(text[i][1..]);
                            bits = Convert.ToString(result, 2).PadLeft(16, '0');
                        }
  
                        break;
                    case "(":
                        var c= text[i].Remove(text[i].IndexOf(")"));
                        if (!symbolTable.Contains(c[1..]))
                        {
                            symbolTable.AddEntry(c[1..], i+1);
                            
                        }
                        bits = Convert.ToString(i + 1, 2).PadLeft(16, '0');
                        break;

                    default:
                        string destination;
                        string computation;
                        string jump;
                        if (text[i].Contains("="))
                        {
                            destination = code.GetDest(text[i].Remove(text[i].IndexOf("=")).Trim());
                        }
                        else
                        {
                            destination = code.GetDest("null");
                        }

                        if (text[i].Contains(";"))
                        {
                            var split = text[i].Split(";");
                            jump = code.GetJump(split[1].Trim());
                        }
                        else
                        {
                            jump = code.GetJump("null");
                        }

                        var comp = text[i];

                        if (comp.Contains(";"))
                        {
                            comp = comp.Remove(comp.IndexOf(";")).Trim();
                        }
                        

                        if (text[i].Contains("="))
                        {
                            var split = text[i].Split("=");
                            comp = split[1];
                        }
                        computation = code.GetComp(comp);
                        
                        bits = $"111{computation}{destination}{jump}";
                        break;
                }
                bitss.Add(bits);

            }
            File.WriteAllLines(@".\bit.hack", bitss);
        }
      
        public void ReadFile(string Filename)
        {
            string textFile= @"C:/programming/Assembler/" + Filename;
            
            using (StreamReader file = new StreamReader(textFile))
            {
                 lines = File.ReadAllLines(textFile);

                
            }
            
        }
    }
}

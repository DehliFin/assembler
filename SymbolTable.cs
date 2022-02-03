using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    
    class SymbolTable
    {
        private Dictionary<string, int> symbols = new Dictionary<string, int>() 
        {
            {"R0",0 },
            {"R1",1 },
            {"R2",2 },
            {"R3",3 },
            {"R4",4 },
            {"R5",5 },
            {"R6",6 },
            {"R7",7 },
            {"R8",8 },
            {"R9",9 },
            {"R10",10 },
            {"R11",11 },
            {"R12",12 },
            {"R13",13 },
            {"R14",14 },
            {"R15",15 },
            {"SCREEN",16384 },
            {"KBD",24576 },
            {"SP",0 },
            {"LCL",1 },
            {"ARG",2 },
            {"THIS",3 },
            {"THAT",4 }
           


        };
        public SymbolTable()
        {

        }
        public void AddEntry(string symbol, int address)
        {
            symbols.Add(symbol, address);
        }
        public Boolean Contains(string symbol)
        {
            if (symbols.ContainsKey(symbol))
            {
                return true;
            }
            return false;
        }
        public int GetAddress(string symbol)
        {
            symbols.TryGetValue(symbol, out int result);

            return result;
        }
    }
}

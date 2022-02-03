using System;

namespace Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            
            foreach (string line in parser.bitss)
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }
    }
}

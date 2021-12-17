using System;
using Microsoft.VisualBasic.FileIO;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            string line = new string(" ");
            var centralBank = new CentralBank();
            var consoleInterface = new ConsoleInterface(centralBank);
            while (!line.Equals("quit"))
            {
                line = Console.ReadLine();
                consoleInterface.NewLine(line);
            }
        }
    }
}

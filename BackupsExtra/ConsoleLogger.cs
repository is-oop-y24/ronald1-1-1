using System;

namespace BackupsExtra
{
    public class ConsoleLogger : ILogger
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
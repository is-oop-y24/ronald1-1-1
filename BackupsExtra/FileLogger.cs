using System.IO;

namespace BackupsExtra
{
    public class FileLogger : ILogger
    {
        private readonly string _path;

        public FileLogger(string path)
        {
            _path = path;
        }

        public void SendMessage(string message)
        {
            File.AppendAllText(_path, message);
        }
    }
}
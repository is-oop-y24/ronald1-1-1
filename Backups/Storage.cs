using System;
using System.IO;

namespace Backups
{
    [Serializable]
    public class Storage
    {
        private string _path;
        private string _name;

        public Storage(string path, string name)
        {
            _path = path;
            _name = name;
        }

        public string Path => _path;
        public string Name => _name;
    }
}
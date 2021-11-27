using System;
using System.Collections.Generic;

namespace Backups
{
    [Serializable]
    public class RestorePoint : IComparable
    {
        public RestorePoint(DateTime dateTime, List<Storage> storages, string name, string path)
        {
            DateTime = dateTime;
            Storages = storages;
            Name = name;
            Path = path;
        }

        public List<Storage> Storages { get; }

        public string Name { get; }

        public string Path { get; }

        public string FullName => Path + "\\" + Name;
        public DateTime DateTime { get; }
        public BackupJob BackupJob { get; set; }
        public int CompareTo(object obj)
        {
            return DateTime.CompareTo(((RestorePoint)obj).DateTime);
        }
    }
}
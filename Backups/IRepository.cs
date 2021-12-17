using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        public string RootPath { get; }
        public Storage PutFile(string oldPath, string newPath);
        public void DeleteFile(string path);
        public Storage GetFile(string path);

        public void DeleteDirectory(string path);
        public void CreateArchive(List<Storage> storages, string path, string name);
    }
}
using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        public Storage PutFile(string oldPath, string newPath);
        public void DeleteFile(string path);
        public Storage GetFile(string path);
        public void CreateArchive(List<Storage> storages, string path, string name);
    }
}
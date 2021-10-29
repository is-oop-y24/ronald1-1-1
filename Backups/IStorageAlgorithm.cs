using System.Collections.Generic;

namespace Backups
{
    public interface IStorageAlgorithm
    {
        public RestorePoint CreateRestorePoint(List<Storage> jobObject, IRepository repository, string path, string name);
    }
}
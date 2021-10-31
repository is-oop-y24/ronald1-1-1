using System;
using System.Collections.Generic;

namespace Backups
{
    public class SingleStorage : IStorageAlgorithm
    {
        public RestorePoint CreateRestorePoint(List<Storage> jobObject, IRepository repository, string path, string name)
        {
            repository.CreateArchive(jobObject, path + "\\" + name + "\\", name + ".zip");
            return new RestorePoint(DateTime.Now, jobObject, name, path);
        }
    }
}
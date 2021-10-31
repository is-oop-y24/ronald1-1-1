﻿using System;
using System.Collections.Generic;

namespace Backups
{
    public class SplitStorage : IStorageAlgorithm
    {
        public RestorePoint CreateRestorePoint(List<Storage> jobObject, IRepository repository, string path, string name)
        {
            foreach (var storage in jobObject)
            {
                repository.CreateArchive(new List<Storage>() { storage }, path + "\\" + name, storage.Name + ".zip");
            }

            return new RestorePoint(DateTime.Now, jobObject, name, path);
        }
    }
}
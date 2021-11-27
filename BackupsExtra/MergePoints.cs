using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class MergePoints
    {
        public void Merge(RestorePoint restorePoint1, RestorePoint restorePoint2)
        {
            if (DateTime.Compare(restorePoint1.DateTime, restorePoint2.DateTime) > 0)
            {
                (restorePoint2, restorePoint1) = (restorePoint1, restorePoint2);
            }

            if (Directory.EnumerateFiles(restorePoint1.BackupJob.Repository.RootPath + restorePoint1.FullName).Count() == 1)
            {
                foreach (string archive in Directory.EnumerateFiles(restorePoint1.BackupJob.Repository.RootPath + restorePoint1.FullName))
                {
                    if (new FileInfo(archive).Name == restorePoint1.Name + ".zip")
                    {
                        restorePoint1.BackupJob.RemoveRestorePoint(restorePoint1);
                        return;
                    }
                }
            }

            foreach (Storage storage1 in restorePoint1.Storages)
            {
                bool intersection = false;
                foreach (Storage storage2 in restorePoint2.Storages)
                {
                    if (storage1.Path.Equals(storage2.Path))
                    {
                        intersection = true;
                    }
                }

                if (!intersection)
                {
                    restorePoint2.BackupJob.Repository.CreateArchive(
                        new List<Storage> { storage1 },
                        restorePoint2.FullName,
                        storage1.Name + ".zip");
                    restorePoint2.Storages.Add(storage1);
                }
            }

            restorePoint1.BackupJob.RemoveRestorePoint(restorePoint1);
        }
    }
}
using System.IO;
using System.IO.Compression;
using Backups;

namespace BackupsExtra
{
    public class Recovery
    {
        public void ToOriginalLocation(RestorePoint restorePoint)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                foreach (string archiveName in Directory.EnumerateFiles(restorePoint.BackupJob.Repository.RootPath + restorePoint.FullName))
                {
                    using ZipArchive archive = ZipFile.OpenRead(archiveName);
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == storage.Name)
                        {
                            entry.ExtractToFile(storage.Path);
                        }
                    }
                }
            }
        }

        public void ToDifferentLocation(RestorePoint restorePoint, string directoryPath)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                foreach (string archiveName in Directory.EnumerateFiles(restorePoint.BackupJob.Repository.RootPath + restorePoint.FullName))
                {
                    using ZipArchive archive = ZipFile.OpenRead(archiveName);
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name == storage.Name)
                        {
                            entry.ExtractToFile(directoryPath + storage.Name);
                        }
                    }
                }
            }
        }
    }
}
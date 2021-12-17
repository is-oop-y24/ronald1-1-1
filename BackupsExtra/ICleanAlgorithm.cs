using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    public interface ICleanAlgorithm
    {
        public List<RestorePoint> GetRestorePoints(BackupJob backupJob);

        public void DeleteRestorePoints(BackupJob backupJob)
        {
            List<RestorePoint> restorePoints = GetRestorePoints(backupJob);
            foreach (RestorePoint restorePoint in restorePoints)
            {
                backupJob.RemoveRestorePoint(restorePoint);
            }
        }
    }
}
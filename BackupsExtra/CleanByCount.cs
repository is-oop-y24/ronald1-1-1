using System.Collections.Generic;
using System.Data.Common;
using Backups;

namespace BackupsExtra
{
    public class CleanByCount : ICleanAlgorithm
    {
        public CleanByCount(int count)
        {
            Count = count;
        }

        public int Count { get; set; }

        public List<RestorePoint> GetRestorePoints(BackupJob backupJob)
        {
            var restorePoints = new List<RestorePoint>();
            if (backupJob.RestorePoints.Count > Count)
            {
                backupJob.SortRestorePoints();
                for (int i = 0; i < backupJob.RestorePoints.Count - Count; i++)
                {
                    restorePoints.Add(backupJob.RestorePoints[i]);
                }
            }

            return restorePoints;
        }
    }
}
using System;
using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    public class CleanByDate : ICleanAlgorithm
    {
        public CleanByDate(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }

        public List<RestorePoint> GetRestorePoints(BackupJob backupJob)
        {
            var restorePoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in backupJob.RestorePoints)
            {
                if (DateTime.Compare(restorePoint.DateTime, DateTime) < 0)
                {
                    restorePoints.Add(restorePoint);
                }
            }

            return restorePoints;
        }
    }
}
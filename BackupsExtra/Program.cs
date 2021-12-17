using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var backupService = new BackupService(new ConsoleLogger());
            BackupJob backupJob = backupService.AddBackupJob(
                new SplitStorage(),
                new List<Storage>(),
                new FileSystem("E:\\test\\test1\\"),
                "pics");
            var storage1 = new Storage("E:\\pics\\3k1bPV2aS54.jpg", "3k1bPV2aS54.jpg");
            backupService.AddStorageToBackupJob(backupJob, storage1);
            backupService.AddRestorePoint(backupJob, "first");
            backupService.RemoveStorageFromBackupJob(backupJob, storage1);
            var storage2 = new Storage("E:\\pics\\channels4_profile.jpg", "channels4_profile.jpg");
            backupService.AddStorageToBackupJob(backupJob, storage2);
            backupService.AddRestorePoint(backupJob, "second");
            backupService.MergePoints(backupJob.RestorePoints[0], backupJob.RestorePoints[1]);
            backupService.RecoverToDifferentLocation(backupJob.RestorePoints[0], "E:\\test\\");
        }
    }
}

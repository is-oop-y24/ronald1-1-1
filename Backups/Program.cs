using System.Collections.Generic;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            BackupJob backupJob = new BackupJob(
                new SplitStorage(),
                new List<Storage>(),
                new FileSystem("E:\\test\\test1\\"),
                "pics");
            Storage file1 = new Storage("E:\\pics\\3k1bPV2aS54.jpg", "3k1bPV2aS54.jpg");
            Storage file2 = new Storage("E:\\pics\\channels4_profile.jpg", "channels4_profile.jpg");
            backupJob.AddStorage(file1);
            backupJob.AddStorage(file2);
            backupJob.AddRestorePoint("first");
            backupJob.RemoveStorageFromJobObject(file1);
            backupJob.AddRestorePoint("second");

            BackupJob backupJob2 = new BackupJob(
                new SingleStorage(),
                new List<Storage>(),
                new FileSystem("E:\\test\\test2\\"),
                "pics");
            backupJob2.AddStorage(file1);
            backupJob2.AddStorage(file2);
            backupJob2.AddRestorePoint("first");
        }
    }
}

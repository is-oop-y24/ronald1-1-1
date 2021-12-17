using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Backups;
using Backups.Tools;

namespace BackupsExtra
{
    public class BackupService
    {
        private ILogger _logger;
        private List<BackupJob> _backupJobs;
        private MergePoints _mergePoints;
        private Recovery _recovery;

        public BackupService(ILogger logger)
        {
            _logger = logger;
            _backupJobs = new List<BackupJob>();
            _mergePoints = new MergePoints();
            _recovery = new Recovery();
        }

        public void SaveBackupJob(BackupJob backupJob, string path)
        {
            try
            {
                FileStream stream = File.Create(path);
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, backupJob);
                stream.Close();
                _logger.Info("BackupJob saved");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }

        public BackupJob GetBackupJob(string path)
        {
            try
            {
                FileStream stream = File.OpenRead(path);
                var formatter = new BinaryFormatter();
                var backupJob = formatter.Deserialize(stream) as BackupJob;
                return backupJob;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }

        public BackupJob AddBackupJob(IStorageAlgorithm storageAlgorithm, List<Storage> jobObject, IRepository repository, string path)
        {
            try
            {
                var backupJob = new BackupJob(storageAlgorithm, jobObject, repository, path);
                _backupJobs.Add(backupJob);
                _logger.Info("Backup Job was created");
                return backupJob;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw new BackupException(e.Message);
            }
        }

        public void AddStorageToBackupJob(BackupJob backupJob, Storage storage)
        {
            backupJob.AddStorage(storage);
            _logger.Info("New storage:" + storage.Name);
        }

        public void RemoveStorageFromBackupJob(BackupJob backupJob, Storage storage)
        {
            backupJob.RemoveStorageFromJobObject(storage);
            _logger.Info("Storage was deleted: " + storage.Name);
        }

        public void AddRestorePoint(BackupJob backupJob, string name)
        {
            try
            {
                backupJob.AddRestorePoint(name);
                _logger.Info("New restore point: " + name);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }

        public void MergePoints(RestorePoint restorePoint1, RestorePoint restorePoint2)
        {
            _mergePoints.Merge(restorePoint1, restorePoint2);
            _logger.Info(restorePoint1.Name + " and " + restorePoint2 + " was merged");
        }

        public void CleanRestorePoints(BackupJob backupJob, ICleanAlgorithm cleanAlgorithm)
        {
            try
            {
                cleanAlgorithm.DeleteRestorePoints(backupJob);
                _logger.Info("Backup job was cleaned");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }

        public void RecoverToOriginalLocation(RestorePoint restorePoint)
        {
            try
            {
                _recovery.ToOriginalLocation(restorePoint);
                _logger.Info("Restore point was recovered: " + restorePoint.Name);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }

        public void RecoverToDifferentLocation(RestorePoint restorePoint, string path)
        {
            try
            {
                _recovery.ToDifferentLocation(restorePoint, path);
                _logger.Info("Restore point was recovered: " + restorePoint.Name);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace Backups
{
    [Serializable]
    public class BackupJob
    {
        private IStorageAlgorithm _storageAlgorithm;
        private List<RestorePoint> _restorePoints;
        private List<Storage> _jobObject;
        private IRepository _repository;

        public BackupJob(IStorageAlgorithm storageAlgorithm, List<Storage> jobObject, IRepository repository, string path)
        {
            _storageAlgorithm = storageAlgorithm;
            _jobObject = jobObject;
            _repository = repository;
            Path = path;
            _restorePoints = new List<RestorePoint>();
        }

        public string Path { get; }

        public List<RestorePoint> RestorePoints => new List<RestorePoint>(_restorePoints);
        public List<Storage> JobObject => new List<Storage>(_jobObject);

        public IRepository Repository => _repository;

        public void AddRestorePoint(string name)
        {
            RestorePoint restorePoint = _storageAlgorithm.CreateRestorePoint(_jobObject, _repository, Path, name);
            restorePoint.BackupJob = this;
            _restorePoints.Add(restorePoint);
        }

        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
            _repository.DeleteDirectory(restorePoint.Path + "\\" + restorePoint.Name);
            _restorePoints.Remove(restorePoint);
        }

        public void RemoveStorageFromJobObject(Storage storage)
        {
            _jobObject.Remove(storage);
        }

        public void AddStorage(Storage storage)
        {
            _jobObject.Add(storage);
        }

        public void SortRestorePoints()
        {
            _restorePoints.Sort();
        }
    }
}
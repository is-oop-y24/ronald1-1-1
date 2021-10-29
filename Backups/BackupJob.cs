using System.Collections.Generic;

namespace Backups
{
    public class BackupJob
    {
        private IStorageAlgorithm _storageAlgorithm;
        private List<RestorePoint> _restorePoints;
        private List<Storage> _jobObject;
        private IRepository _repository;
        private string _path;

        public BackupJob(IStorageAlgorithm storageAlgorithm, List<Storage> jobObject, IRepository repository, string path)
        {
            _storageAlgorithm = storageAlgorithm;
            _jobObject = jobObject;
            _repository = repository;
            _path = path;
            _restorePoints = new List<RestorePoint>();
        }

        public List<RestorePoint> RestorePoints => new List<RestorePoint>(_restorePoints);
        public List<Storage> JobObject => new List<Storage>(_jobObject);

        public void AddRestorePoint(string name)
        {
            _restorePoints.Add(_storageAlgorithm.CreateRestorePoint(_jobObject, _repository, _path, name));
        }

        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
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
    }
}
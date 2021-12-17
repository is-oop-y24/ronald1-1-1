using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class HybridClean : ICleanAlgorithm
    {
        private List<ICleanAlgorithm> _algorithms;
        private bool _allOf;

        public HybridClean(List<ICleanAlgorithm> algorithms, bool allOf = true)
        {
            _algorithms = algorithms;
            _allOf = allOf;
        }

        public List<RestorePoint> GetRestorePoints(BackupJob backupJob)
        {
            if (_algorithms.Count == 0) return new List<RestorePoint>();
            List<RestorePoint> restorePoints = _algorithms[0].GetRestorePoints(backupJob);
            foreach (ICleanAlgorithm algorithm in _algorithms)
            {
                if (_allOf)
                {
                    restorePoints = restorePoints.Intersect(algorithm.GetRestorePoints(backupJob)).ToList();
                }
                else
                {
                    restorePoints = restorePoints.Union(algorithm.GetRestorePoints(backupJob)).ToList();
                }
            }

            return restorePoints;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Backups
{
    public class RestorePoint
    {
        private DateTime _dateTime;
        private List<Storage> _storages;
        private string _name;
        private string _path;

        public RestorePoint(DateTime dateTime, List<Storage> storages, string name, string path)
        {
            _dateTime = dateTime;
            _storages = storages;
            _name = name;
            _path = path;
        }
    }
}
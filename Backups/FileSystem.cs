using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Tools;

namespace Backups
{
    [Serializable]
    public class FileSystem : IRepository
    {
        private string _rootPath;

        public FileSystem(string rootPath)
        {
            _rootPath = rootPath;
            try
            {
                Directory.CreateDirectory(_rootPath);
            }
            catch (Exception)
            {
                throw new BackupException("Directory already exist: " + _rootPath);
            }
        }

        public string RootPath => _rootPath;
        public Storage PutFile(string oldPath, string newPath)
        {
            File.Copy(oldPath, newPath);
            var fileInfo = new FileInfo(newPath);
            return new Storage(fileInfo.FullName, fileInfo.Name);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public Storage GetFile(string path)
        {
            var fileInfo = new FileInfo(path);
            return new Storage(fileInfo.FullName, fileInfo.Name);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(_rootPath + path, true);
        }

        public void CreateArchive(List<Storage> storages, string path, string name)
        {
            Directory.CreateDirectory("temp");
            foreach (var storage in storages)
            {
                PutFile(storage.Path,  "temp\\" + storage.Name);
            }

            try
            {
                Directory.CreateDirectory(_rootPath + "\\" + path);
            }
            catch (Exception)
            {
                Directory.Delete("temp", true);
                throw new BackupException("Directory already exist: " + _rootPath + "\\" + path);
            }

            try
            {
                ZipFile.CreateFromDirectory("temp", _rootPath + "\\" + path + "\\" + name);
            }
            catch (Exception)
            {
                Directory.Delete("temp", true);
                throw new BackupException("Archive already exist: " + _rootPath + "\\" + path + "\\" + name);
            }

            Directory.Delete("temp", true);
        }
    }
}
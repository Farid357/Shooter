using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Shooter.SaveSystem
{
    public sealed class BinaryStorage : IStorage
    {
        private readonly BinaryFormatter _formatter = new();

        public void DeleteSave(string path)
        {
            var allPath = CreatePath(path);

            if (Exists(allPath) == false)
                throw new InvalidOperationException(nameof(DeleteSave));

            File.Delete(allPath);
        }

        public T Load<T>(string path)
        {
            var allPath = CreatePath(path);

            if (Exists(path) == false)
                throw new InvalidOperationException(nameof(Load));

            using var file = File.Open(allPath, FileMode.Open);
            return (T)_formatter.Deserialize(file);
        }

        public bool Exists(string key) => File.Exists(CreatePath(key));

        public void Save<T>(string path, T saveObject)
        {
            var allPath = CreatePath(path);
            using var file = File.Create(allPath);
            _formatter.Serialize(file, saveObject);
        }

        private string CreatePath(string name) => Path.Combine(Application.persistentDataPath, name);
    }
}
using System;
using UnityEngine;
using System.IO;

namespace Shooter.SaveSystem
{
    public sealed class JsonStorage : IStorage
    {
        public bool Exists(string key) => File.Exists(CreatePath(key));

        public void DeleteSave(string path)
        {
            var allPath = CreatePath(path);

            if (Exists(allPath) == false)
                throw new InvalidOperationException(nameof(DeleteSave));

            File.Delete(allPath);
        }

        public T Load<T>(string name)
        {
            var jsonPath = CreatePath(name);

            if (Exists(name) == false)
                throw new InvalidOperationException(nameof(Load));

            var saveJson = File.ReadAllText(jsonPath);
            return JsonUtility.FromJson<T>(saveJson);
        }

        public void Save<T>(string name, T saveObject)
        {
            var jsonPath = CreatePath(name);
            var saveJson = JsonUtility.ToJson(saveObject);
            File.WriteAllText(jsonPath, saveJson);
        }

        private string CreatePath(string name) => Path.Combine(Application.persistentDataPath, name);
    }
}
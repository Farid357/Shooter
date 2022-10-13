using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Shooter.SaveSystem
{
    public sealed class XmlStorage : IStorage
    {
        public T Load<T>(string name)
        {
            if (Exists(name) == false)
                throw new InvalidOperationException(nameof(Load));

            var serializer = new XmlSerializer(typeof(T));
            var path = CreatePath(name);
            var fileText = File.ReadAllText(path);
            using var fileStream = new FileStream(path, FileMode.Create);
            using var stringReader = new StringReader(fileText);
            return (T)serializer.Deserialize(stringReader);
        }

        public void Save<T>(string name, T saveObject)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var fileStream = new FileStream(CreatePath(name), FileMode.Create);
            serializer.Serialize(fileStream, saveObject);
        }

        public bool Exists(string name) => File.Exists(CreatePath(name));

        public void DeleteSave(string name)
        {
            var path = CreatePath(name);
            
            if (Exists(path) == false)
                throw new InvalidOperationException(nameof(DeleteSave));
            
            File.Delete(path);
        }

        private string CreatePath(string name) => Application.persistentDataPath + name;
    }
}
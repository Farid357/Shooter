using System;
using System.Collections.Generic;

namespace Shooter.SaveSystem
{
    public sealed class CollectionStorage<T> : ICollectionStorage<T>
    {
        private readonly List<T> _allSavedObject = new();
        private readonly IStorage _storage;

        public CollectionStorage(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public IEnumerable<T> Load(string key)
        {
            if (Exists(key) == false)
                throw new InvalidOperationException("Storage doesn't have save!");
            
            return _storage.Load<IEnumerable<T>>(key);
        }

        public void Save(string key, T saveObject)
        {
          _allSavedObject.Add(saveObject);
          _storage.Save(key, _allSavedObject);
        }

        public bool Exists(string key) => _storage.Exists(key);
    }
}
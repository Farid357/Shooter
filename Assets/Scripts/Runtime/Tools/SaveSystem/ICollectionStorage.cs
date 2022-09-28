using System.Collections.Generic;

namespace Shooter.SaveSystem
{
    public interface ICollectionStorage<T>
    {
        public IEnumerable<T> Load(string key);
        
        public void Save(string key, T saveObject);
        
        public bool Exists(string key);
    }
}
using Shooter.SaveSystem;

namespace Shooter.Test
{
    public sealed class DummyStorage : IStorage
    {
        public T Load<T>(string key) => default;

        public void Save<T>(string key, T saveObject)
        {
           
        }

        public bool Exists(string key) => false;
        
        public void DeleteSave(string path)
        {
            
        }
    }
}
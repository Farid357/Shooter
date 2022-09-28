namespace Shooter.SaveSystem
{
    public interface IStorage
    {
        public T Load<T>(string key);
        
        public void Save<T>(string key, T saveObject);
        
        public bool Exists(string key);
    }

    public interface IDeletable
    {
        public void TryDelete(string path);
    }
}
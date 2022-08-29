namespace Shooter.Tools
{
    public interface IPool<T>
    {
        public void Release(T obj);

        public T Get();

    }
}
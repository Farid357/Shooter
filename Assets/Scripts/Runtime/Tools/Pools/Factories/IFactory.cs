namespace Shooter.Tools
{
    public interface IFactory<T>
    {
        public T Create();

    }
}
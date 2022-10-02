namespace Shooter.Tools
{
    public interface IFactory<out T>
    {
        public T Create();

    }
}
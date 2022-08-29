namespace Shooter.Model
{
    public interface IView<T>
    {
        public void Visualize(T self);
    }
}
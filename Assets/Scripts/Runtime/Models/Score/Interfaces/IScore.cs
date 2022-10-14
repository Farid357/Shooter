namespace Shooter.Model
{
    public interface IScore : IReadOnlyCounter
    {
        void Add(int amount);
    }
}
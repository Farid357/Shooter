namespace Shooter.Model
{
    public interface IEnemiesSimulation : IReadOnlyEnemiesSimulation
    {
        void Add(IEnemy enemy);

    }
}
namespace Shooter.Model
{
    public interface IReadOnlyEnemiesSimulation
    {
        bool HasEnemyDied { get; }

        bool NotContainsAliveEnemy { get; }

    }
}
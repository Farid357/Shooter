namespace Shooter.Model
{
    public interface IEnemyWaves
    {
        IReadOnlyEnemiesSimulation Simulation { get; }
        
        void CreateNext(EnemyWaveData wave);
    }
}
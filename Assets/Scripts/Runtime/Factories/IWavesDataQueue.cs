using Shooter.Model;

namespace Shooter.GameLogic
{
    public interface IWavesDataQueue
    {
        EnemyWaveData Dequeue();
    }
}
using Cysharp.Threading.Tasks;

namespace Shooter.GameLogic
{
    public interface IEnemyNavMeshAgent
    {
        bool CanIncreaseSpeed { get; }
        
        UniTaskVoid IncreaseSpeedForSeconds(float increaseSpeed, float seconds);
        
        UniTaskVoid SlowDownForSeconds(float newSpeed, float seconds);
    }
}
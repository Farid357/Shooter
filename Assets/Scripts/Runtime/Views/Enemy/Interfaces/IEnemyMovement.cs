using Shooter.GameLogic;

namespace Shooter.Model
{
    public interface IEnemyMovement
    {
        IEnemyNavMeshAgent Agent { get; }
        
        void Init(ICharacterTransform character);
        
        void MoveToCharacter();

    }
}
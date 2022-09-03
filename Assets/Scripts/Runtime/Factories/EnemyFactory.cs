using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class EnemyFactory : SerializedMonoBehaviour
    {
        public abstract IEnemyMovement Create();
    }
}
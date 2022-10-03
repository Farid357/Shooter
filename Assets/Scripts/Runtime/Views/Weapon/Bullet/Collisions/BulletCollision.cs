using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class BulletCollision : MonoBehaviour, IBulletCollision
    {
        [field: SerializeField, ProgressBar(1, 100)] public int Damage { get; protected set; }
        
        public abstract void IncreaseDamageForSeconds(int damage, float seconds);
        
        public abstract bool CanIncreaseDamage { get; }
        
        
    }
}
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class BulletCollision : MonoBehaviour, IBulletCollision
    {
        public abstract void IncreaseDamageForSeconds(int damage, float seconds);
        
        public abstract bool CanIncreaseDamage { get; }
        
        public abstract int Damage { get; }
        
    }
}
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartBulletCollision : BulletCollision
    {
        [SerializeField, Min(1)] private int _damage = 2;
        
        protected override void OnCollide(IHealth health, Vector3 point)
        {
            Attack(health);
        }

        private void Attack(IHealth health)
        {
            if (health.IsAlive)
                health.TakeDamage(_damage);
        }
    }
}
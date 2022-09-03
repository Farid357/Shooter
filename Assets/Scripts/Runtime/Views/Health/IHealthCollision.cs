using UnityEngine;

namespace Shooter.Model
{
    public interface IHealthCollision
    {
        public void TryTakeDamage(in int damage);
        
        public Vector3 Position { get; }
    }
}
using System;
using UnityEngine;

namespace Shooter.Model
{
    [RequireComponent(typeof(Collider))]
    public sealed class HealthCollision : MonoBehaviour, IHealthCollision
    {
        private IHealth _health;

        public void Init(IHealth health)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public void TryDamage(in int damage)
        {
            if (_health.IsAlive)
                _health.TakeDamage(damage);
        }
    }
}
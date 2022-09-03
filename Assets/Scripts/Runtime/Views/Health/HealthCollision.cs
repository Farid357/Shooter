using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public sealed class HealthCollision : MonoBehaviour, IHealthCollision
    {
        private IHealth _health;
        
        public Vector3 Position => transform.position;

        public void Init(IHealth health)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public void TryTakeDamage(in int damage)
        {
            if (_health.IsAlive)
                _health.TakeDamage(damage);
        }

    }
}
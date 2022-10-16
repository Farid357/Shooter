using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class FireBulletCollision : BulletCollision
    {
        [SerializeField, Min(0.01f)] private float _attackCooldown = 0.1f;
        
        public override bool CanIncreaseDamage => false;

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthTransformView healthTransformView) && _attackCooldown == 0)
            {
                Attack(healthTransformView.Health);
            }
            
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void Attack(IHealth health)
        {
            if (health.IsAlive)
                health.TakeDamage(Damage);
        }

        private void Update() => _attackCooldown = Mathf.Max(0, _attackCooldown - Time.deltaTime);

        public override void IncreaseDamageForSeconds(int damage, float seconds)
        {
            throw new InvalidOperationException("Bullet can't increase damage!");
        }
    }
}
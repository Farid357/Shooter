using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public sealed class StandartBulletCollision : BulletCollision, IBulletCollision
    {
        [SerializeField] private bool _needDisableOnEnteredCollision = true;
        private bool _canIncreaseDamage;

        public override bool CanIncreaseDamage => _canIncreaseDamage;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthTransformView healthTransformView))
            {
                Attack(healthTransformView.Health);
            }

            if (_needDisableOnEnteredCollision)
                gameObject.SetActive(false);
        }

        public override async void IncreaseDamageForSeconds(int damage, float seconds)
        {
            if (Damage >= damage || CanIncreaseDamage == false)
                throw new InvalidOperationException(nameof(IncreaseDamageForSeconds));

            var startDamage = Damage;
            SetDamage(damage, false);
            await UniTask.Delay(TimeSpan.FromSeconds(seconds));
            SetDamage(startDamage, true);
        }

        private void Attack(IHealth health)
        {
            if (health.IsAlive)
                health.TakeDamage(Damage);
        }

        private void SetDamage(int damage, bool canIncreaseDamage)
        {
            Damage = damage.TryThrowLessThanOrEqualsToZeroException();
            _canIncreaseDamage = canIncreaseDamage;
        }
    }
}
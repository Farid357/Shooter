using System;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Model
{
    public interface IWeapon
    {
        public void Shoot();

        public bool CanShoot { get; }
    }

    public interface IBullet
    {
        public void Throw();
    }

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

    public sealed class CharacterHealth : IHealth
    {
        private readonly IHealth _health;

        public CharacterHealth(IHealth health)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public bool IsAlive => _health.IsAlive;

        public void TakeDamage(in int damage)
        {
            _health.TakeDamage(damage);
        }
    }

    public sealed class Health : IHealth
    {
        private readonly IView<int> _view;
        private int _value;

        public Health(int amount, IView<int> view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _value = amount.TryThrowLessThanOrEqualsToZeroException();
        }

        public bool IsAlive => _value > 0;

        public void TakeDamage(in int damage)
        {
            if (IsAlive == false)
                throw new InvalidOperationException("Health is not alive!");

            damage.TryThrowLessThanOrEqualsToZeroException();
            _value -= damage;
            _view.Visualize(_value);
        }
    }
}
using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class HealthShield : IHealth
    {
        private readonly IHealth _health;
        private int _protection;

        public HealthShield(IHealth health, int protection)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _protection = protection.TryThrowLessThanOrEqualsToZeroException();
        }

        public bool IsAlive => _health.IsAlive;

        public int StartValue => _health.StartValue;

        public int Value => _health.Value;
        
        public void TakeDamage(int damage)
        {
            if (_protection > 0)
            {
                damage -= _protection;
                _protection -= damage;
            }

            _health.TakeDamage(damage);

        }

        public void Heal(int amount) => _health.Heal(amount);

        public bool CanHeal(int amount) => _health.CanHeal(amount);
        
    }
}
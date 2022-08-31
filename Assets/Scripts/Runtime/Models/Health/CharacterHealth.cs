using System;

namespace Shooter.Model
{
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
}
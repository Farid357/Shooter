using System;

namespace Shooter.Model
{
    public sealed class PoisonHealth : IHealth
    {
        private readonly IHealth _health;

        public PoisonHealth(IHealth health)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public bool IsAlive => _health.IsAlive;
        
        public int StartValue => _health.StartValue;
        
        public int Value => _health.Value;
        
        public void TakeDamage(int damage) => _health.TakeDamage(damage * 2);

        public void Heal(int amount)
        {
            throw new InvalidOperationException("This health can't heal!");
        }

        public bool CanHeal(int amount) => false;
        
    }
}
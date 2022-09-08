using System;

namespace Shooter.Model
{
    public sealed class EnemyHealth : IHealth
    {
        private readonly IHealth _health;
        private readonly IReward _reward;

        public EnemyHealth(IHealth health, IReward reward)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _reward = reward ?? throw new ArgumentNullException(nameof(reward));
        }

        public bool IsAlive => _health.IsAlive;

        public int StartValue => _health.StartValue;
        
        public int Value => _health.Value;
        
        public void TakeDamage(in int damage)
        {
           _health.TakeDamage(damage);
           
           if(_health.IsAlive == false)
               _reward.Apply();
        }

        public void Heal(in int amount) => _health.Heal(amount);

        public bool CanHeal(in int amount) => _health.CanHeal(amount);
        
    }
}
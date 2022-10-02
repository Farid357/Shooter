using System;

namespace Shooter.Model
{
    public sealed class HealthDeathReward : IUpdateble
    {
        private readonly IHealth _health;
        private readonly IReward _reward;
        private bool _notEarnedReward = true;
        
        public HealthDeathReward(IHealth health, IReward reward)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _reward = reward ?? throw new ArgumentNullException(nameof(reward));
        }
        
        public void Update(float deltaTime)
        {
            if (_health.IsDied && _notEarnedReward)
            {
                _reward.Apply();
                _notEarnedReward = false;
            }
        }
    }
}
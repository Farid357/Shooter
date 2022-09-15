using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Regeneration : IUpdateble
    {
        private readonly IHealth _health;
        private readonly ITimer _timer;
        private readonly int _value;

        public Regeneration(IHealth health, ITimer timer, int value)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _value = value.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Update(float deltaTime)
        {
            if (_health.Value < _health.StartValue)
            {
                if (_timer.IsEnded)
                    Heal(_health);
            }
        }

        private void Heal(IHealth health)
        {
            var difference = health.StartValue - health.Value;
            health.Heal(difference < _value ? difference : _value);
        }
    }
}
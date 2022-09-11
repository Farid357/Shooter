using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Regeneration : IUpdateble
    {
        private readonly IHealth _health;
        private readonly int _value;
        private ITimer _timer;

        public Regeneration(IHealth health, ITimer timer, int value)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _value = value.TryThrowLessThanOrEqualsToZeroException();
        }

        public async void Update(float deltaTime)
        {
            if (_health.Value < _health.StartValue)
            {
                await _timer.End();
                Heal(_health);
                _timer = _timer.Restart();
            }
        }

        private void Heal(IHealth health)
        {
            var difference = health.StartValue - health.Value;
            UnityEngine.Debug.Log(difference);
            health.Heal(difference < _value ? difference : _value);
        }
    }
}
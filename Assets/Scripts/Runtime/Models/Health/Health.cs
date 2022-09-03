using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Health : IHealth
    {
        private readonly IHealthView _view;
        private int _value;

        public Health(int amount, IHealthView view)
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
            _value = Math.Max(0, _value - damage);
            _view.Visualize(_value);
        }
    }
}
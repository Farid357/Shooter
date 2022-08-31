using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Health : IHealth
    {
        private readonly IView<int> _view;
        private readonly IDeathView _deathView;
        private int _value;

        public Health(int amount, IView<int> view, IDeathView deathView)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _deathView = deathView ?? throw new ArgumentNullException(nameof(deathView));
            _value = amount.TryThrowLessThanOrEqualsToZeroException();
        }

        public bool IsAlive => _value > 0;

        public void TakeDamage(in int damage)
        {
            if (IsAlive == false)
                throw new InvalidOperationException("Health is not alive!");

            damage.TryThrowLessThanOrEqualsToZeroException();
            _value -= Math.Max(0, _value);
            _view.Visualize(_value);
            
            if(_value == 0)
                _deathView.Visualize();
        }
    }
}
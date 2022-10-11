using System;

namespace Shooter.Model
{
    public sealed class CharacterHealthRegenerationAbility : IAbility
    {
        private readonly IAbilityView _view;
        private readonly IHealth _health;
        private const int Amount = 40;

        public CharacterHealthRegenerationAbility(IAbilityView view, IHealth health)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public void Apply()
        {
            var halfAmount = Amount / 2;

            if (_health.CanHeal(Amount))
            {
                _health.Heal(Amount);
            }

            else if (_health.CanHeal(halfAmount))
            {
                _health.Heal(halfAmount);
            }

            _view.VisualizeApply(0.2f);
        }
    }
}
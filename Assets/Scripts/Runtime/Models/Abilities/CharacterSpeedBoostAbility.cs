using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class CharacterSpeedBoostAbility : IAbility
    {
        private readonly IAbilityView _view;
        private readonly ICharacterMovement _movement;
        private readonly float _applySeconds;
        private const float Speed = 1.2f;

        public CharacterSpeedBoostAbility(IAbilityView view, ICharacterMovement movement, float applySeconds)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _applySeconds = applySeconds.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Apply()
        {
            if (_movement.CanIncreaseSpeed)
                _movement.IncreaseSpeedForSeconds(Speed, _applySeconds);
            
            _view.VisualizeApply(_applySeconds);
        }
    }
}
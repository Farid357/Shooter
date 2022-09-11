using System;
using Shooter.GameLogic;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class CharacterIncreaseBulletsDamageAbility : IAbility
    {
        private readonly IAbilityView _view;
        private readonly IBulletCollision[] _bullets;
        private readonly float _applySeconds;
        
        public CharacterIncreaseBulletsDamageAbility(IAbilityView view, IBulletCollision[] bullets, float applySeconds)
        {
            _applySeconds = applySeconds.TryThrowLessOrEqualsToZeroException();
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bullets = bullets ?? throw new ArgumentNullException(nameof(bullets));
        }

        public void Apply()
        {
            foreach (var bullet in _bullets)
            {
                if (bullet.CanIncreaseDamage)
                {
                    var increaseDamage = bullet.Damage * 2;
                    bullet.IncreaseDamageForSeconds(increaseDamage, _applySeconds);
                    _view.VisualizeApply(_applySeconds);
                }
            }
        }
    }
}
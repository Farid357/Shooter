using System;
using System.Threading.Tasks;
using Shooter.GameLogic;
using Shooter.Tools;
using Sirenix.Utilities;

namespace Shooter.Model
{
    public sealed class CharacterIncreaseBulletsDamageAbility : IAbility, IDisposable
    {
        private readonly IAbilityView _view;
        private readonly IBulletsFactory[] _bulletsFactories;
        private readonly float _applySeconds;
        private bool _hasApplied;

        public CharacterIncreaseBulletsDamageAbility(IAbilityView view, IBulletsFactory[] bulletsFactories, float applySeconds)
        {
            _applySeconds = applySeconds.TryThrowLessThanOrEqualsToZeroException();
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bulletsFactories = bulletsFactories ?? throw new ArgumentNullException(nameof(bulletsFactories));
            _bulletsFactories.ForEach(factory => factory.OnCreated += BulletsFactoryOnCreated);
        }

        private void BulletsFactoryOnCreated(BulletMovement bullet)
        {
            if (_hasApplied)
            {
                if (bullet.TryGetComponent(out IBulletCollision bulletCollision) && bulletCollision.CanIncreaseDamage)
                {
                    var increaseDamage = bulletCollision.Damage * 2;
                    bulletCollision.IncreaseDamageForSeconds(increaseDamage, _applySeconds);
                    _view.VisualizeApply(_applySeconds);
                }
            }
        }

        public async void Apply()
        {
            _hasApplied = true;
            await Task.Delay(TimeSpan.FromSeconds(_applySeconds));
            _hasApplied = false;
        }

        public void Dispose() => _bulletsFactories.ForEach(factory => factory.OnCreated -= BulletsFactoryOnCreated);

    }
}
using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class EnemiesInRadiusSlowdownAbility : IAbility
    {
        private readonly IAbilityView _abilityView;
        private readonly IEnemiesInRadiusFinder _enemiesFinder;
        
        private readonly float _slowdownSpeed;
        private readonly float _seconds;

        public EnemiesInRadiusSlowdownAbility(IAbilityView abilityView,  IEnemiesInRadiusFinder enemiesFinder, float seconds, float slowdownSpeed)
        {
            _abilityView = abilityView ?? throw new ArgumentNullException(nameof(abilityView));
            _enemiesFinder = enemiesFinder ?? throw new ArgumentNullException(nameof(enemiesFinder));
            _slowdownSpeed = slowdownSpeed.TryThrowLessThanOrEqualsToZeroException();
            _seconds = seconds.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Apply()
        {
            if (_enemiesFinder.TryFind(out var enemies))
            {
                _abilityView.VisualizeApply(_seconds);
                
                for (var i = 0; i < enemies.Count; i++)
                {
                    var enemy = enemies[i];
                    if (enemy.Health.IsAlive)
                    {
                        enemy.Movement.Agent.SlowDownForSeconds(_slowdownSpeed, _seconds);
                    }
                }
            }
        }
    }
}
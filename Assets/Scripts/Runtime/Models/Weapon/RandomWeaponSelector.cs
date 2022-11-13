using System;
using Shooter.Root;

namespace Shooter.Model
{
    public sealed class RandomWeaponSelector : IUpdateble
    {
        private readonly IReadOnlyEnemiesSimulation _enemiesSimulation;
        private readonly IRandomWeaponFactory _randomWeaponFactory;
        private readonly IPlayerRoot _playerRoot;
        
        public RandomWeaponSelector(IReadOnlyEnemiesSimulation enemiesSimulation, IRandomWeaponFactory randomWeaponFactory, IPlayerRoot playerRoot)
        {
            _enemiesSimulation = enemiesSimulation ?? throw new ArgumentNullException(nameof(enemiesSimulation));
            _randomWeaponFactory = randomWeaponFactory ?? throw new ArgumentNullException(nameof(randomWeaponFactory));
            _playerRoot = playerRoot ?? throw new ArgumentNullException(nameof(playerRoot));
        }

        public void Update(float deltaTime)
        {
            if (_enemiesSimulation.HasEnemyDied)
            {
                var weapon = _randomWeaponFactory.Get();
                _playerRoot.Compose(weapon.Item2, weapon.Item1);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class EnemySimulation : IEnemiesSimulation
    {
        private readonly List<IEnemy> _enemies = new();
        private readonly INavMeshBaker _navMeshBaker;

        public EnemySimulation(INavMeshBaker navMeshBaker)
        {
            _navMeshBaker = navMeshBaker ?? throw new ArgumentNullException(nameof(navMeshBaker));
        }

        public bool NotContainsAliveEnemy => _enemies.HasNotAny(enemy => enemy.Health.IsAlive);
        
        public void Add(IEnemy enemy)
        {
            if (enemy is null)
                throw new ArgumentNullException(nameof(enemy));
            
            enemy.Enable();
            enemy.Movement.MoveToCharacter();
            enemy.Movement.RotateToCharacter();
            _navMeshBaker.Bake();
            _enemies.Add(enemy);
        }

    }
}
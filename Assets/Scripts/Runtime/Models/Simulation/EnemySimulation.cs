﻿using System;
using System.Collections.Generic;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class EnemySimulation : IEnemiesSimulation, IUpdateble
    {
        private readonly List<IEnemy> _aliveEnemies = new();
        private readonly INavMeshBaker _navMeshBaker;
        private readonly IView<int> _aliveEnemiesCountView;

        public EnemySimulation(INavMeshBaker navMeshBaker, IView<int> aliveEnemiesCountView)
        {
            _navMeshBaker = navMeshBaker ?? throw new ArgumentNullException(nameof(navMeshBaker));
            _aliveEnemiesCountView = aliveEnemiesCountView ?? throw new ArgumentNullException(nameof(aliveEnemiesCountView));
        }

        public bool NotContainsAliveEnemy => _aliveEnemies.Count == 0;

        public void Add(IEnemy enemy)
        {
            if (enemy is null)
                throw new ArgumentNullException(nameof(enemy));

            enemy.Enable();
            enemy.Movement.MoveToCharacter();
            enemy.Movement.RotateToCharacter();
            _navMeshBaker.Bake();
            _aliveEnemies.Add(enemy);
            _aliveEnemiesCountView.Visualize(_aliveEnemies.Count);
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < _aliveEnemies.Count; i++)
            {
                var enemy = _aliveEnemies[i];

                if (enemy.Health.IsDied)
                {
                    _aliveEnemies.Remove(enemy);
                    _aliveEnemiesCountView.Visualize(_aliveEnemies.Count);
                }
            }
        }
    }
}
using System;
using Shooter.GameLogic;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Model
{
    [Serializable]
    public sealed class EnemyWaveData
    {
        [SerializeField] private StandartEnemyFactory[] _factories;
        
        [field: SerializeField, Range(1, 1000)] public int EnemiesCount { get; private set; }
        
        [field: SerializeField, Range(0.001f, 10f)] public float CreateDelaySeconds { get; private set; }

        [field: SerializeField, Min(0.1f)] public float SecondsAfterEnd { get; private set; }
        
        public IEnemyFactory[] EnemyFactories => _factories;
        
        public EnemyWaveData(int enemiesCount, StandartEnemyFactory[] enemyFactory, float createDelaySeconds, float secondsAfterEnd)
        {
            _factories = enemyFactory ?? throw new ArgumentNullException(nameof(enemyFactory));
            EnemiesCount = enemiesCount.TryThrowLessThanOrEqualsToZeroException();
            CreateDelaySeconds = createDelaySeconds.TryThrowLessThanOrEqualsToZeroException();
            SecondsAfterEnd = secondsAfterEnd.TryThrowLessThanOrEqualsToZeroException();
        }
        
        public EnemyWaveData CreateNext()
        {
            return new EnemyWaveData(EnemiesCount + 1, _factories, CreateDelaySeconds, SecondsAfterEnd);
        }
    }
}
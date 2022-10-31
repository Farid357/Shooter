using System;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Model
{
    [Serializable]
    public sealed class EnemyWaveData
    {
        [SerializeField] private EnemyWaveFactoryData[] _factoriesData;

        [field: SerializeField, Range(0.001f, 10f)] public float CreateDelaySeconds { get; private set; }

        [field: SerializeField, Min(0.1f)] public float SecondsAfterEnd { get; private set; }
        
        public EnemyWaveFactoryData[] EnemyFactoriesData => _factoriesData;
        
        public EnemyWaveData(EnemyWaveFactoryData[] enemyFactoriesData, float createDelaySeconds, float secondsAfterEnd)
        {
            _factoriesData = enemyFactoriesData ?? throw new ArgumentNullException(nameof(enemyFactoriesData));
            CreateDelaySeconds = createDelaySeconds.TryThrowLessThanOrEqualsToZeroException();
            SecondsAfterEnd = secondsAfterEnd.TryThrowLessThanOrEqualsToZeroException();
        }
        
        public EnemyWaveData CreateNext()
        {
            return new EnemyWaveData(_factoriesData, CreateDelaySeconds, SecondsAfterEnd);
        }
    }
}
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class WaveFactory
    {
        private readonly IEnemyWaves _waves;
        private readonly List<EnemyWaveData> _waveDatas;
        private EnemyWaveData _currentWaveData;
        private int _currentWaveIndex;

        public WaveFactory(IEnemyWaves waves, List<EnemyWaveData> waveDatas)
        {
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _waveDatas = waveDatas ?? throw new ArgumentNullException(nameof(waveDatas));
            _currentWaveData = waveDatas[0];
        }

        private bool NeedCreateNext => _waves.Simulation.NotContainsAliveEnemy;

        private bool IsNextWaveNull(int index) => _waveDatas.Count - 1 < index;

        public async UniTaskVoid SpawnNextLoop()
        {
            while (true)
            {
                if (NeedCreateNext)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_currentWaveData.SecondsAfterEnd));
                    _waves.CreateNext(_currentWaveData);
                    _currentWaveIndex++;

                    if (IsNextWaveNull(_currentWaveIndex))
                    {
                        var current = _waveDatas[_currentWaveIndex - 1];
                        var newWaveData = current.CreateNext();
                        _waveDatas.Add(newWaveData);
                    }

                    _currentWaveData = _waveDatas[_currentWaveIndex];
                }

                await UniTask.Yield();
            }
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class WaveFactory
    {
        private readonly IEnemyWaves _waves;
        private readonly ITimer _waitNextWaveTimer;
        private readonly IWavesDataQueue _wavesData;

        public WaveFactory(IEnemyWaves waves, ITimer waitNextWaveTimer, IWavesDataQueue wavesData)
        {
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _waitNextWaveTimer = waitNextWaveTimer ?? throw new ArgumentNullException(nameof(waitNextWaveTimer));
            _wavesData = wavesData ?? throw new ArgumentNullException(nameof(wavesData));
        }

        private bool NeedCreateNext => _waves.Simulation.NotContainsAliveEnemy;

        public async UniTaskVoid SpawnNextLoop()
        {
            while (true)
            {
                if (NeedCreateNext)
                {
                    var waveData = _wavesData.Dequeue();
                    _waitNextWaveTimer.Restart(waveData.SecondsAfterEnd);
                    await UniTask.Delay(TimeSpan.FromSeconds(waveData.SecondsAfterEnd));
                    _waves.CreateNext(waveData);
                }

                await UniTask.Yield();
            }
        }
    }
}
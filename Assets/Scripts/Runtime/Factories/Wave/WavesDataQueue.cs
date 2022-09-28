using System;
using System.Collections.Generic;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class WavesDataQueue : IWavesDataQueue
    {
        private readonly Queue<EnemyWaveData> _waveData;

        public WavesDataQueue(Queue<EnemyWaveData> waveData)
        {
            _waveData = waveData ?? throw new ArgumentNullException(nameof(waveData));
        }

        private bool LeftOneElement() => _waveData.Count == 1;

        public EnemyWaveData Dequeue()
        {
            if (LeftOneElement())
            {
                var current = _waveData.Peek();
                var newWaveData = current.CreateNext();
                _waveData.Enqueue(newWaveData);
                return current;
            }
                
            return _waveData.Dequeue();
        }
    }
}
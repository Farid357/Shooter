using System;
using System.Threading.Tasks;

namespace Shooter.Model
{
    public sealed class EnemyWaves : IEnemyWaves
    {
        private readonly IEnemiesSimulation _simulation;

        public EnemyWaves(IEnemiesSimulation simulation)
        {
            _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        }

        public IReadOnlyEnemiesSimulation Simulation => _simulation;

        public async void CreateNext(EnemyWaveData wave)
        {
            var random = new Random();
            
            for (var i = 0; i < wave.EnemiesCount; i++)
            {
                var randomIndex = random.Next(0, wave.EnemyFactories.Length);
                var enemy = wave.EnemyFactories[randomIndex].Create();
                _simulation.Add(enemy);
                await Task.Delay(TimeSpan.FromSeconds(wave.CreateDelaySeconds));
            }
        }
    }
}
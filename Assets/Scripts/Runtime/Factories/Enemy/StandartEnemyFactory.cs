using System;
using Shooter.Model;
using Shooter.Root;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartEnemyFactory : SerializedMonoBehaviour, IEnemyFactory
    {
        [SerializeField] private ICharacterMovement _character;
        [SerializeField] private IHealthTransformView _characterHealthTransformView;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Enemy _prefab;

        private IPool<Enemy> _pool;
        private IRewardFactory _rewardFactory;
        private ISystemUpdate _systemUpdate;
        private IScore _score;
        private IReward _diedHealthCounterReward;

        public void Init(ISystemUpdate systemUpdate, IRewardFactory rewardFactory, IScore score, IReward diedHealthCounterReward)
        {
            _pool = new IndependentPool<Enemy>(new GameObjectsFactory<Enemy>(_prefab, transform));
            _rewardFactory = rewardFactory ?? throw new ArgumentNullException(nameof(rewardFactory));
            _systemUpdate = systemUpdate ?? throw new ArgumentNullException(nameof(systemUpdate));
            _score = score ?? throw new ArgumentNullException(nameof(score));
            _diedHealthCounterReward = diedHealthCounterReward ?? throw new ArgumentNullException(nameof(diedHealthCounterReward));
        }

        public IEnemy Create()
        {
            var enemy = _pool.Get();
            enemy.Init(_character, _characterHealthTransformView);
            var reward = _rewardFactory.Create();

            var rewards = new[]
            {
                reward,
                _diedHealthCounterReward,
                new ScoreReward(_score, enemy.Score)
            };

            var enemyReward = new HealthDeathReward(enemy.Health, new Rewards(rewards));
            enemy.transform.position = _spawnPoints.GetRandomFromArray().position;
            _systemUpdate.Add(enemyReward);
            return enemy;
        }
    }
}
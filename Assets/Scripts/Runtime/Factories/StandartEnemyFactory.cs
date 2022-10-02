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

        private IndependentPool<Enemy> _pool;
        private IRewardFactory _rewardFactory;
        private ISystemUpdate _systemUpdate;
        private IScore _score;

        public void Init(ISystemUpdate systemUpdate, IRewardFactory rewardFactory, IScore score)
        {
            _rewardFactory = rewardFactory ?? throw new ArgumentNullException(nameof(rewardFactory));
            _pool = new IndependentPool<Enemy>(new GameObjectsFactory<Enemy>(_prefab, transform));
            _systemUpdate = systemUpdate ?? throw new ArgumentNullException(nameof(systemUpdate));
            _score = score ?? throw new ArgumentNullException(nameof(score));
        }

        public IEnemy Create()
        {
            var enemy = _pool.Get();
            enemy.Init(_character, _characterHealthTransformView);
            var reward = _rewardFactory.Create();
            var enemyReward = new HealthDeathReward(enemy.Health, reward);
            var scoreEnemyReward = new HealthDeathReward(enemy.Health, new ScoreReward(_score, enemy.Score));
            enemy.transform.position = _spawnPoints.GetRandomFromArray().position;
            _systemUpdate.Add(enemyReward, scoreEnemyReward);
            return enemy;
        }
    }
}
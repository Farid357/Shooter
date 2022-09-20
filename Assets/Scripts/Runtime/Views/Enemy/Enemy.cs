using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Enemy : SerializedMonoBehaviour, IEnemy
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField, Min(10)] private int _healthCount = 10;
        [SerializeField] private HealthTransformView _health;
        [SerializeField] private IHealthView _enemyHealthView;
        [SerializeField] private StandartEnemyMovement _movement;

        [SerializeField] private EnemyType _type;
        [SerializeField, Min(0), ShowIf("_type", EnemyType.WithShield)]
        private int _protection;
        
        public IEnemyMovement Movement => _movement;

        public IHealth Health { get; private set; }
        
        [field: SerializeField, Range(1, 100000)] public int Score { get; private set; }

        public void Init(ICharacterMovement character, IHealthTransformView characterHealthTransformView)
        {
            var health = new Health(_healthCount, _enemyHealthView);
            Health = _type == EnemyType.WithShield ? new HealthShield(health, _protection) : health;
            Health = _type == EnemyType.WithPoison ? new PoisonHealth(health) : health;
            _health.Init(Health);
            _movement.Init(character);
            _attack.Init(characterHealthTransformView);
        }

        public void Enable() => gameObject.SetActive(true);
        
        private enum EnemyType
        {
            WithShield,
            WithPoison,
            Standart
        }
    }
}
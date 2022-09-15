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

        [SerializeField, Min(0), Tooltip("Can be 0!")]
        private int _protection;
        
        public IEnemyMovement Movement => _movement;

        public IHealth Health { get; private set; }
        
        [field: SerializeField, Range(1, 100000)] public int Score { get; private set; }

        public void Init(ICharacterMovement character, IHealthTransformView characterHealthTransformView)
        {
            var health = new Health(_healthCount, _enemyHealthView);
            Health = _protection > 0 ? new HealthShield(health, _protection) : health ;
            _health.Init(Health);
            _movement.Init(character);
            _attack.Init(characterHealthTransformView);
        }

        public void Enable() => gameObject.SetActive(true);
    }
}
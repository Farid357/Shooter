using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Enemy : SerializedMonoBehaviour, IEnemy
    {
        [SerializeField, Min(10)] private int _healthCount = 10;
        [SerializeField] private HealthTransformView _health;
        [SerializeField] private IHealthView _enemyHealthView;
        [SerializeField] private EnemyToCharacterChaser _chaser;

        [SerializeField] private EnemyType _type;

        [SerializeField, Min(0), ShowIf("_type", EnemyType.WithShield)]
        private int _protection;

        [field: SerializeField] public IEnemyMovement Movement { get; private set; }
        

        [field: SerializeField, Range(1, 100000)]
        public int Score { get; private set; }
        
        public IHealth Health { get; private set; }

        public void Init(ICharacterMovement character, IHealthTransformView characterHealthTransformView)
        {
            _chaser.Init(characterHealthTransformView);
            var health = new Health(_healthCount, _enemyHealthView);
            Health = _type == EnemyType.WithShield ? new Armor(health, new DummyArmorView(), _protection) : health;
            Health = _type == EnemyType.WithPoison ? new PoisonHealth(health) : health;
            _health.Init(Health);
            Movement.Init(character);
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
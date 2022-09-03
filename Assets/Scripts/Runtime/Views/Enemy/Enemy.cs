using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField, Min(10)] private int _healthCount = 10;
        [SerializeField] private HealthCollision _health;
        [SerializeField] private EnemyHealthView _enemyHealthView;
        
        [field: SerializeField] public EnemyMovement Movement { get; private set; }

        public void Init(ICharacter character, IHealthCollision healthCollision)
        {
            var health = new Health(_healthCount, _enemyHealthView);
            _health.Init(health);
            Movement.Init(character);
            _attack.Init(healthCollision);
        }
    }
}
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField, Min(10)] private int _healthCount = 10;
        [SerializeField] private HealthTransformView _health;
        [SerializeField] private EnemyHealthView _enemyHealthView;
        [SerializeField, Min(5)] private int _moneyReward = 5;
        
        [field: SerializeField] public StandartEnemyMovement Movement { get; private set; }

        public void Init(ICharacterTransform character, IHealthTransformView healthTransformView, IWallet wallet)
        {
            var reward = new MoneyReward(wallet, _moneyReward);
            var health = new EnemyHealth(new Health(_healthCount, _enemyHealthView), reward);
            _health.Init(health);
            Movement.Init(character);
            _attack.Init(healthTransformView);
        }
    }
}
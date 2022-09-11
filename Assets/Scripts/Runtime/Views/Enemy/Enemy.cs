using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Enemy : SerializedMonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField, Min(10)] private int _healthCount = 10;
        [SerializeField] private HealthTransformView _health;
        [SerializeField] private IHealthView _enemyHealthView;
        [SerializeField, Min(5)] private int _moneyReward = 5;
        [SerializeField] private IAbilityView _speedBoostAbility;
        [SerializeField] private IAbilityView _bulletsDamageAbility;
        [SerializeField] private IAbilityView _regenerationAbility;

        [field: SerializeField] public StandartEnemyMovement Movement { get; private set; }

        public void Init(ICharacterMovement character, IHealth characterHealth, IHealthTransformView characterHealthTransformView, IWallet wallet)
        {
            var abilities = new IAbility[]
            {
                new CharacterSpeedBoostAbility(_speedBoostAbility, character, 6f),
                new CharacterIncreaseBulletsDamageAbility(_bulletsDamageAbility, FindObjectsOfType<BulletCollision>(), 6f),
                new CharacterHealthRegenerationAbility(_regenerationAbility, characterHealth)
            };

            IRewardFactory factory = new RandomRewardFactory(abilities, new IReward[] { new MoneyReward(wallet, _moneyReward) });
            var health = new EnemyHealth(new Health(_healthCount, _enemyHealthView), factory.Create());
            _health.Init(health);
            Movement.Init(character);
            _attack.Init(characterHealthTransformView);
        }
    }
}
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PotionRoot : CompositeRoot
    {
        [SerializeField] private EnemyRoot _enemyRoot;
        [SerializeField] private PotionPickupsFactory _potionPickupsFactory;
        [SerializeField] private IHealthTransformView _character;
        [SerializeField] private IPotionView _healthPotionView;
        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private IInventoryView _potionInventoryView;
        [SerializeField] private ItemGameObjectViewFactory _potionGameObjectViewFactory;
        
        public override void Compose()
        {
            var potions = new IPotion[]
            {
                new HealthPotion(_character.Health, _healthPotionView),
                new RewardPotion(_healthPotionView, new ScoreReward(_scoreRoot.Score, 1000)),
                new NegativeHealthPotion(_character.Health, _healthPotionView)
            };
            
            var potionFactory = new PotionFactory(potions, _potionGameObjectViewFactory);
            var inventory = new Inventory<IPotion>(_potionInventoryView, 5);
            _potionPickupsFactory.Init(potionFactory, inventory, _enemyRoot.WaveFactory);
        }
    }
}
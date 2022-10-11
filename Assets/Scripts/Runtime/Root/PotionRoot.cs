using System.Collections.Generic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PotionRoot : SerializedMonoBehaviour
    {
        [SerializeField] private EnemyRoot _enemyRoot;
        [SerializeField] private PotionPickupsFactory _potionPickupsFactory;
        [SerializeField] private IHealthTransformView _character;
        [SerializeField] private IPotionView _healthPotionView;
        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private ItemGameObjectViewFactory _potionGameObjectViewFactory;
        
        [field: SerializeField] public IReadOnlyDictionary<KeyCode,int> KeypadNumbers { get; private set; }

        [field: SerializeField] public IInventoryView InventoryView { get; private set; }
        
        public void Compose(IInventory<IPotion> inventory)
        {
            var potions = new IPotion[]
            {
                new HealthPotion(_character.Health, _healthPotionView),
                new RewardPotion(_healthPotionView, new ScoreReward(_scoreRoot.Score, 1000)),
                new NegativeHealthPotion(_character.Health, _healthPotionView)
            };
            
            var potionFactory = new PotionFactory(potions, _potionGameObjectViewFactory);
            _potionPickupsFactory.Init(potionFactory, inventory, _enemyRoot.WaveFactory);
        }
    }
}
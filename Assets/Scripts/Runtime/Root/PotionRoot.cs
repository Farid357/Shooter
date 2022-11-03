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
        [SerializeField] private IPlayerRoot _playerRoot;
        [SerializeField] private IBulletsView[] _bulletsViews;
        [SerializeField] private IInventoryView _inventoryView;

        public IInventoryItemSelector<IPotion> Selector { get; private set; }
        
        [field: SerializeField] public IReadOnlyDictionary<KeyCode,int> KeypadNumbers { get; private set; }

        
        public IInventory<IPotion> Compose()
        {
            var potionsInventory = new Inventory<IPotion>(_inventoryView, 3);
            var potions = new IPotion[]
            {
                new HealthPotion(_character.Health, _healthPotionView),
                new RewardPotion(_healthPotionView, new ScoreReward(_scoreRoot.Score(), 1000)),
                new NegativeHealthPotion(_character.Health, _healthPotionView)
            };
            
            var potionFactory = new PotionFactory(potions);
            Selector = new DroppingWeaponSelector(_playerRoot, potionFactory, _bulletsViews);
            _potionPickupsFactory.Init(potionFactory, _potionGameObjectViewFactory, potionsInventory, _enemyRoot.WaveFactory, Selector);
            return potionsInventory;
        }
    }
}
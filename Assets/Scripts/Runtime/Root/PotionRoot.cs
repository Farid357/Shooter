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
        [SerializeField] private IScoreRoot _scoreRoot;
        [SerializeField] private ItemGameObjectViewFactory _potionGameObjectViewFactory;
        [SerializeField] private IPlayerRoot _playerRoot;
        [SerializeField] private IBulletsView[] _bulletsViews;
        [SerializeField] private IInventoryView _inventoryView;
        [SerializeField] private ICharacterMovement _characterMovement;
        [SerializeField] private Dictionary<PotionType, IPotionView> _views;

        public IInventoryItemSelector<IPotion> Selector { get; private set; }
        
        [field: SerializeField] public IReadOnlyDictionary<KeyCode,int> KeypadNumbers { get; private set; }

        
        public IInventory<IPotion> Compose()
        {
            var potionsInventory = new Inventory<IPotion>(_inventoryView, 3);
            var view = _potionGameObjectViewFactory.Create();
            
            var potions = new IPotion[]
            {
                new HealthPotion(_character.Health, _views[PotionType.Health], view),
                new RewardPotion(_views[PotionType.Reward], new ScoreReward(_scoreRoot.Score(), 1000), view),
                new NegativeHealthPotion(_character.Health, _views[PotionType.NegativeHealth], view),
                new SpeedPotion(_characterMovement, _views[PotionType.Speed], view)
            };
            
            var potionFactory = new PotionFactory(potions);
            Selector = new ThrowingWeaponSelector(_playerRoot, _bulletsViews);
            _potionPickupsFactory.Init(potionFactory, _potionGameObjectViewFactory, potionsInventory, _enemyRoot.WaveFactory, Selector);
            return potionsInventory;
        }
        
        private enum PotionType
        {
            Health,
            Reward,
            Speed,
            NegativeHealth
        }
    }
}
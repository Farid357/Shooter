using System;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Root;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class PotionPickupsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private PotionPickup _prefab;
        [SerializeField] private IPlayerRoot _playerRoot;
        private IFactory<(IPotion, IInventoryItemGameObjectView)> _potionFactory;
        private IInventory<IPotion> _inventory;
        private IInventoryItemSelector<IPotion> _potionSelector;
        private IWaveFactory _waveFactory;

        public void Init(IFactory<(IPotion, IInventoryItemGameObjectView)> potionFactory, IInventory<IPotion> inventory, IWaveFactory waveFactory)
        {
            _waveFactory = waveFactory ?? throw new ArgumentNullException(nameof(waveFactory));
            _potionFactory = potionFactory ?? throw new ArgumentNullException(nameof(potionFactory));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _potionSelector = new PotionSelector(_playerRoot, new ComputerPotionInput());
        }

        private void Update()
        {
            if (_waveFactory.IsCreatingNext)
            {
                CreatePickup();
            }
        }

        private void CreatePickup()
        {
            var potionPickup = Instantiate(_prefab, transform.position, _prefab.transform.rotation);
            var tuple = _potionFactory.Create();
            var item = new Item<IPotion>(potionPickup.ItemData, tuple.Item1, tuple.Item2);
            potionPickup.Init(_inventory, new InventorySlot<IPotion>(_potionSelector, item));
        }
    }
}
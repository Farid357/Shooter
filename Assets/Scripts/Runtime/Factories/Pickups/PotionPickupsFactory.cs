using System;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class PotionPickupsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private PotionPickup _prefab;
        [SerializeField] private ISpline _spine;
        [SerializeField] private Transform _spawnPoint;
        
        private IFactory<IPotion> _potionFactory;
        private IInventory<IPotion> _inventory;
        private IInventoryItemSelector<IPotion> _potionSelector;
        private IWaveFactory _waveFactory;
        private IFactory<IInventoryItemGameObjectView> _potionGameObjectFactory;

        public void Init(IFactory<IPotion> potionFactory, IFactory<IInventoryItemGameObjectView> potionGameObjectFactory, IInventory<IPotion> inventory, IWaveFactory waveFactory, IInventoryItemSelector<IPotion> potionsSelector)
        {
            _waveFactory = waveFactory ?? throw new ArgumentNullException(nameof(waveFactory));
            _potionFactory = potionFactory ?? throw new ArgumentNullException(nameof(potionFactory));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _potionSelector = potionsSelector ?? throw new ArgumentNullException(nameof(potionsSelector));
            _potionGameObjectFactory = potionGameObjectFactory;
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
            var potionPickup = Instantiate(_prefab, _spawnPoint.position, _prefab.transform.rotation, transform);
            potionPickup.Movement.Init(_spine);
            var potion = _potionFactory.Create();
            var item = new Item<IPotion>(potionPickup.ItemData, potion, _potionGameObjectFactory.Create());
            potionPickup.Init(_inventory, new InventorySlot<IPotion>(_potionSelector, item, 2));
        }
    }
}
using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shooter.Root
{
    public sealed class InventoryRoot : CompositeRoot
    {
        [SerializeField] private ShotgunBulletsFactory _shotgunBulletsFactory;
        [SerializeField] private IInventoryView _inventoryView;
        [SerializeField] private Dictionary<KeyCode, int> _keypadNumbers;
        [SerializeField] private PlayerRoot _playerRoot;
        [SerializeField] private StandartBulletsFactory _explosiveBulletsFactory;
        [SerializeField] private PickupsRoot _pickupsRoot;
        
        [VerticalGroup("Start Weapon")] [SerializeField]
        private ItemData _weaponItemData;

        [VerticalGroup("Start Weapon")] [SerializeField]
        private InventoryItemView _weaponItemViewPrefab;

        [FormerlySerializedAs("_weaponData")] [VerticalGroup("Start Weapon")] [SerializeField]
        private WeaponData _startWeaponData;

        [VerticalGroup("Start Weapon")] [SerializeField]
        private ItemGameObjectView _itemGameObjectView;

        private IWeapon _weapon;
        private SystemUpdate _systemUpdate;
        private readonly IWeaponInput _standartWeaponInput = new StandartWeaponInput();

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            IWeaponFactory factory = new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _startWeaponData);
            var inventory = new Inventory<(IWeapon, IWeaponInput)>(_inventoryView);
            var weaponSelector = new WeaponSelector(_playerRoot);
            var itemView = Instantiate(_weaponItemViewPrefab);
            _weapon = factory.Create();
            var item = new Item<(IWeapon, IWeaponInput)>(_weaponItemData, (_weapon, _standartWeaponInput), itemView);
            var slot = new InventorySlot<(IWeapon, IWeaponInput)>(weaponSelector, item);
            inventory.Add(slot, 1);
            var inventoryItemsSelector = new InventoryItemsSelector<(IWeapon, IWeaponInput)>(inventory);
            var weaponInputSelector = new InventoryItemsSelectorInput(_keypadNumbers, inventoryItemsSelector);

            itemView.Init(_itemGameObjectView);
            itemView.Show();
            _systemUpdate.Add(weaponInputSelector);
            _pickupsRoot.Compose(inventory);
            inventoryItemsSelector.Select(0);
        }

        private void Update() => _systemUpdate.TryUpdateAll(Time.deltaTime);
    }
}
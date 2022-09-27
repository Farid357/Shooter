using System.Collections.Generic;
using System.Linq;
using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class InventoryRoot : CompositeRoot
    {
        [SerializeField] private IFactory<IBullet> _shotgunBulletsFactory;
        [SerializeField] private IInventoryView _inventoryView;
        [SerializeField] private Dictionary<KeyCode, int> _keypadNumbers;
        [SerializeField] private PlayerRoot _playerRoot;
        [SerializeField] private PickupsRoot _pickupsRoot;
        [SerializeField] private IInventoryView _grenadesInventoryView;
        [SerializeField] private GrenadeView _grenade;
        [SerializeField] private ItemData _grenadeItem;
        [SerializeField] private Dictionary<KeyCode,int> _grenadeInventoryKeypadNumbers;
        [SerializeField] private IFactory<IInventoryItemGameObjectView> _grenadeGameObjectViewFactory;
        [SerializeField] private IFactory<IGrenade> _grenadeFactory;
        
        [VerticalGroup("Start Weapon")] [SerializeField]
        private ItemData _weaponItemData;

        [VerticalGroup("Start Weapon")] [SerializeField]
        private WeaponData _startWeaponData;

        [VerticalGroup("Start Weapon")] [SerializeField]
        private ItemGameObjectView _weaponView;
        
        private SystemUpdate _systemUpdate;
        private IWeaponInput _standartWeaponInput = new StandartWeaponInput();

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            IWeaponFactory factory = new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _startWeaponData);
            var weapon = factory.Create();
            var inventory = new Inventory<(IWeapon, IWeaponInput)>(_inventoryView);
            var weaponSelector = new WeaponSelector(_playerRoot);
            var item = new Item<(IWeapon, IWeaponInput)>(_weaponItemData, (weapon, _standartWeaponInput), _weaponView);
            var slot = new InventorySlot<(IWeapon, IWeaponInput)>(weaponSelector, item, 1);
            var grenadeInventory = new Inventory<IGrenade>(_grenadesInventoryView, 3);
            var grenadeItem = new Item<IGrenade>(_grenadeItem, _grenade, _grenade.ItemView);
            var grenadeSlot = new InventorySlot<IGrenade>(new GrenadeSelector(_playerRoot), grenadeItem, 2);
            grenadeInventory.Add(grenadeSlot);
            inventory.Add(slot);
            
            var weaponInventoryItemsSelector = new InventoryItemsSelector<(IWeapon, IWeaponInput)>(inventory);
            var grenadeInventorySelector = new InventoryItemsSelector<IGrenade>(grenadeInventory);
            var selectors = new List<IInventoryItemsSelector>{ grenadeInventorySelector, weaponInventoryItemsSelector };
            var inventoryItemsInputSelector = new InventoryItemsSelectorInput(_keypadNumbers, selectors, weaponInventoryItemsSelector);
            var grenadeInputSelector = new InventoryItemsSelectorInput(_grenadeInventoryKeypadNumbers, selectors, grenadeInventorySelector);
            _weaponView.Show();
            
            _systemUpdate.Add(inventoryItemsInputSelector, grenadeInputSelector);
            _pickupsRoot.Compose(inventory, grenadeInventory);
            weaponInventoryItemsSelector.Select(0);
            _systemUpdate.Add(new BulletsAdderAfterCooldown(inventory.Slots.Select(model => model.Item.Model.Item1), 
                new IndependentTimer(new DummySecondsView(), 10)), new InventoryItemsDrop(grenadeInventory));
        }

        
        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}
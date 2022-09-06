using System;
using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class InventoryRoot : CompositeRoot
    {
        [SerializeField] private StandartBulletsFactory _bulletsFactory;
        [SerializeField] private ShotgunBulletsFactory _shotgunBulletsFactory;
        [SerializeField] private IInventoryView _inventoryView;
        [SerializeField] private Dictionary<KeyCode, int> _keypadNumbers;
        [SerializeField] private PlayerRoot _playerRoot;
        
        [VerticalGroup("Start Weapon")]
        [SerializeField] private ItemData _weaponItemData;
        
        [VerticalGroup("Start Weapon")]
        [SerializeField] private IItemView _weaponItemView;
        
        [VerticalGroup("Start Weapon")]
        [SerializeField] private WeaponData _weaponData;
        
        private IWeapon _weapon;
        private SystemUpdate _systemUpdate;

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            _bulletsFactory.Init(_systemUpdate);
            var pickups = FindObjectsOfType<WeaponPickup>();
            IWeaponFactory factory = new WeaponFactory(_shotgunBulletsFactory, _bulletsFactory);
            var inventory = new Inventory<IWeapon>(_inventoryView);
            _weapon = factory.CreateAk74(_weaponData).Weapon;
            pickups.ForEach(pickup => pickup.Init(inventory, factory));
            var item = new Item<IWeapon>(_weaponItemData, _weapon, _weaponItemView);
            inventory.Add(item, 1);
            _systemUpdate.Add(new InventoryItemsSelector<IWeapon>(item, inventory, _keypadNumbers, _playerRoot.Compose(_weapon)));
        }
        
        private void Update() => _systemUpdate.Update(Time.deltaTime);
        
    }
}
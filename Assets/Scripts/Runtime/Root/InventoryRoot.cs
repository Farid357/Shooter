using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
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
        [SerializeField] private StandartBulletsFactory _explosiveBulletsFactory;
        
        [VerticalGroup("Start Weapon")]
        [SerializeField] private ItemData _weaponItemData;
        
        [VerticalGroup("Start Weapon")]
        [SerializeField] private InventoryItemView _weaponItemViewPrefab;
        
        [VerticalGroup("Start Weapon")]
        [SerializeField] private WeaponData _weaponData;

        [VerticalGroup("Start Weapon")]
        [SerializeField] private ItemGameObjectView _itemGameObjectView;
        
        private IWeapon _weapon;
        private SystemUpdate _systemUpdate;

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            IWeaponFactory factory = new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _weaponData);
            var inventory = new Inventory<IWeapon>(_inventoryView);
            _weapon = factory.Create();
            
            InitPickups<Ak74Pickup>(_bulletsFactory, inventory);
            InitPickups<ShotgunPickup>(_shotgunBulletsFactory, inventory);
            InitPickups<PistolPickup>(_bulletsFactory, inventory);
            InitPickups<RpgPickup>(_explosiveBulletsFactory, inventory);
            InitPickups<BulletsPickup>(inventory);
            
            var itemView = Instantiate(_weaponItemViewPrefab);
            itemView.Init(_itemGameObjectView);
            itemView.Show();
            var item = new Item<IWeapon>(_weaponItemData, _weapon, itemView);
            inventory.Add(item, 1);
            _systemUpdate.Add(new InventoryItemsSelector<IWeapon>(item, inventory, _keypadNumbers, _playerRoot.Compose(_weapon)));
        }

        private void InitPickups<T>(IFactory<IBullet> bulletsFactory, IInventory<IWeapon> inventory) where  T : MonoBehaviour, IWeaponPickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(bulletsFactory, inventory));
        }
        
        private void InitPickups<T>(IInventory<IWeapon> inventory) where  T : MonoBehaviour, IBulletsPickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(inventory));
        }
        
        private void Update() => _systemUpdate.Update(Time.deltaTime);
        
    }
}
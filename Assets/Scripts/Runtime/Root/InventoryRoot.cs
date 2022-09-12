using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private WeaponData _rpgData;
        [SerializeField] private WeaponData _pistolData;
        [SerializeField] private WeaponData _shotgunData;
        [SerializeField] private WeaponData _ak74Data;

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
        private readonly IWeaponInput _standartWeaponInput = new WeaponKeyboardInput();

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            IWeaponFactory factory = new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _startWeaponData);
            var inventory = new Inventory<(IWeapon, IWeaponInput)>(_inventoryView);
            var weaponSelector = new WeaponSelector(_playerRoot);
            _weapon = factory.Create();

            InitPickups<Ak74Pickup>(new WeaponFactoryWithShootWaitingAndRollback(_bulletsFactory, _ak74Data), inventory, _standartWeaponInput);
            InitPickups<ShotgunPickup>(new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _shotgunData), inventory, _standartWeaponInput);
            InitPickups<RpgPickup>(new WeaponFactoryWithShootWaitingAndRollback(_explosiveBulletsFactory, _rpgData), inventory, _standartWeaponInput);
            InitPickups<PistolPickup>(new WeaponFactoryWithShootWaiting(_bulletsFactory, _pistolData), inventory, _standartWeaponInput);
            InitPickups<BulletsPickup>(inventory);

            var itemView = Instantiate(_weaponItemViewPrefab);
            itemView.Init(_itemGameObjectView);
            itemView.Show();
            var item = new Item<(IWeapon, IWeaponInput)>(_weaponItemData, (_weapon, _standartWeaponInput), itemView);
            var slot = (weaponSelector, item);
            inventory.Add(slot, 1);
            _systemUpdate.Add(new InventoryItemsSelector<(IWeapon, IWeaponInput)>(item, inventory, _keypadNumbers));
            weaponSelector.Select((_weapon, _standartWeaponInput));
        }

        private void InitPickups<T>(IWeaponFactory weaponFactory, IInventory<(IWeapon, IWeaponInput)> inventory, IWeaponInput weaponInput) where T : WeaponPickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(inventory, _playerRoot, (weaponFactory.Create(), weaponInput)));
        }

        private void InitPickups<T>(IInventory<(IWeapon, IWeaponInput)> inventory) where T : MonoBehaviour, IBulletsPickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(inventory));
        }

        private void Update() => _systemUpdate.Update(Time.deltaTime);
    }
}
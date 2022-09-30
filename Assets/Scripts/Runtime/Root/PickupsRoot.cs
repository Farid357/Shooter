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
    public sealed class PickupsRoot : SerializedMonoBehaviour
    {
        [SerializeField] private readonly IFactory<IBullet> _bulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _shotgunBulletsFactory;
        
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _rpgData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _shotgunData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _ak74Data;
        [SerializeField, VerticalGroup("Weapon Data")] private readonly IFactory<IBullet> _explosiveBulletsFactory;
        [SerializeField] private PlayerRoot _playerRoot;
        [SerializeField] private readonly IFactory<IGrenade> _grenadeFactory;
        
        private readonly IWeaponInput _standartWeaponInput = new StandartWeaponInput();
        
        public void Compose(IInventory<(IWeapon, IWeaponInput)> inventory, IInventory<IGrenade> grenadeInventory)
        {
            InitPickups<Ak74Pickup>(new WeaponFactoryWithShootWaitingAndRollback(_bulletsFactory, _ak74Data), inventory, _standartWeaponInput);
            InitPickups<ShotgunPickup>(new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _shotgunData), inventory, _standartWeaponInput);
            InitPickups<RpgPickup>(new WeaponFactoryWithShootWaitingAndRollback(_explosiveBulletsFactory, _rpgData), inventory, _standartWeaponInput);
            InitPickups<PistolPickup>(new WeaponFactoryWithShootWaiting(_bulletsFactory, _pistolData), inventory, new BurstWeaponInput());
            InitPickups<BulletsPickup>(inventory);
            InitPickups<GrenadePickup>(grenadeInventory, new GrenadeSelector(_playerRoot), _grenadeFactory);
        }

        private void InitPickups<T>(IInventory<IGrenade> inventory, GrenadeSelector selector, IFactory<IGrenade> factory) where T : GrenadePickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(inventory, selector, factory));
        }

        private void InitPickups<T>(IWeaponFactory weaponFactory, IInventory<(IWeapon, IWeaponInput)> inventory, IWeaponInput weaponInput) where T : WeaponPickup
        {
            var pickups = FindObjectsOfType<T>();
            var weapon = weaponFactory.Create();
            pickups.ForEach(pickup => pickup.Init(inventory,  new InventorySlot<(IWeapon, IWeaponInput)>(
                new WeaponSelector(_playerRoot), new Item<(IWeapon, IWeaponInput)>(pickup.ItemData, (weapon, weaponInput), pickup.ItemGameObjectView), 1)));
        }

        private void InitPickups<T>(IReadOnlyInventory<(IWeapon, IWeaponInput)> inventory) where T : MonoBehaviour, IBulletsPickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(inventory));
        }
    }
}
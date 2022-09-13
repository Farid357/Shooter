using Shooter.GameLogic;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PickupsRoot : MonoBehaviour
    {
        [SerializeField] private StandartBulletsFactory _bulletsFactory;
        [SerializeField] private ShotgunBulletsFactory _shotgunBulletsFactory;
        
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _rpgData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _shotgunData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _ak74Data;
        [SerializeField, VerticalGroup("Weapon Data")] private StandartBulletsFactory _explosiveBulletsFactory;
        [SerializeField] private PlayerRoot _playerRoot;
        
        private readonly IWeaponInput _standartWeaponInput = new StandartWeaponInput();
        
        public void Compose(IInventory<(IWeapon, IWeaponInput)> inventory)
        {
            InitPickups<Ak74Pickup>(new WeaponFactoryWithShootWaitingAndRollback(_bulletsFactory, _ak74Data), inventory, _standartWeaponInput);
            InitPickups<ShotgunPickup>(new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _shotgunData), inventory, _standartWeaponInput);
            InitPickups<RpgPickup>(new WeaponFactoryWithShootWaitingAndRollback(_explosiveBulletsFactory, _rpgData), inventory, _standartWeaponInput);
            InitPickups<PistolPickup>(new WeaponFactoryWithShootWaiting(_bulletsFactory, _pistolData), inventory, _standartWeaponInput);
            InitPickups<BulletsPickup>(inventory);
        }

        private void InitPickups<T>(IWeaponFactory weaponFactory, IInventory<(IWeapon, IWeaponInput)> inventory, IWeaponInput weaponInput) where T : WeaponPickup
        {
            var pickups = FindObjectsOfType<T>();
            var weapon = weaponFactory.Create();
            pickups.ForEach(pickup => pickup.Init(inventory, _playerRoot, (weapon, weaponInput)));
        }

        private void InitPickups<T>(IReadOnlyInventory<(IWeapon, IWeaponInput)> inventory) where T : MonoBehaviour, IBulletsPickup
        {
            var pickups = FindObjectsOfType<T>();
            pickups.ForEach(pickup => pickup.Init(inventory));
        }
    }
}
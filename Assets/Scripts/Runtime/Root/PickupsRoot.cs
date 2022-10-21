using System.Collections.Generic;
using System.Linq;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.SaveSystem;
using Shooter.Shop;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PickupsRoot : SerializedMonoBehaviour
    {
        [Title("Bullets Factories")]
        [SerializeField] private readonly IFactory<IBullet> _bulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _shotgunBulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _fireBulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _explosiveBulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _laserBulletsFactory;
        
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _rpgData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _shotgunData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _ak74Data;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _laserData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolWithFireBullets;
        
        [SerializeField] private GrenadePickupsFactory _grenadePickupsFactory;
        [SerializeField] private WeaponPickupsFactory _weaponPickupsFactory;
        [SerializeField] private HandWeaponFactory _handWeaponFactory;
        
        public void Compose(IInventory<(IWeapon, IWeaponInput)> weaponsInventory, IInventory<IGrenade> grenadesInventory)
        {
            _grenadePickupsFactory.Init(grenadesInventory);
            _grenadePickupsFactory.SpawnLoop().Forget();
            var weaponTypesStorage = new CollectionStorage<WeaponType>(new BinaryStorage());
            var weaponSpawnTypes = weaponTypesStorage.Exists(WeaponsKey.Value) ? weaponTypesStorage.Load(WeaponsKey.Value)
                : new List<WeaponType> { WeaponType.Sword, WeaponType.PistolWithFireBullets};
            
            var factoriesContainer = new Dictionary<WeaponType, IFactory<IWeapon>>
            {
                { WeaponType.Ak74, new WeaponFactoryWithShootWaiting(_bulletsFactory, _ak74Data) },
                { WeaponType.Pistol, new WeaponFactoryWithShootWaiting(_bulletsFactory, _pistolData) },
                { WeaponType.Rpg, new WeaponFactoryWithShootWaiting(_explosiveBulletsFactory, _rpgData) }, 
                { WeaponType.Shotgun, new WeaponFactoryWithShootWaiting(_shotgunBulletsFactory, _shotgunData)},
                { WeaponType.PistolWithFireBullets, new WeaponFactoryWithShootWaiting(_fireBulletsFactory, _pistolWithFireBullets)},
                { WeaponType.LaserGun, new WeaponFactoryWithShootWaiting(_laserBulletsFactory, _laserData)},
                { WeaponType.Sword, new DummyFactoryFromShootingWeapon(_handWeaponFactory)}
            };

            var inputs = new Dictionary<WeaponType, IWeaponInput>
            {
                { WeaponType.Ak74, new StandartWeaponInput() },
                { WeaponType.Pistol, new BurstWeaponInput() },
                { WeaponType.Rpg, new StandartWeaponInput() },
                { WeaponType.Shotgun, new BurstWeaponInput() },
                { WeaponType.PistolWithFireBullets, new BurstWeaponInput()},
                { WeaponType.Sword, new StandartWeaponInput()},
                { WeaponType.LaserGun, new StandartWeaponInput()},
            };
            
            _weaponPickupsFactory.Init(weaponSpawnTypes.ToList(), weaponsInventory, factoriesContainer, inputs);
            _weaponPickupsFactory.SpawnLoop().Forget();
        }
    }
}
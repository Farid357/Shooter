using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Shop;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class PickupsRoot : SerializedMonoBehaviour
    {
        [SerializeField] private readonly IFactory<IBullet> _bulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _shotgunBulletsFactory;
        [SerializeField] private readonly IFactory<IBullet> _fireBulletsFactory;
        
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _rpgData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _shotgunData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _ak74Data;
        [SerializeField, VerticalGroup("Weapon Data")] private readonly IFactory<IBullet> _explosiveBulletsFactory;
        [SerializeField] private readonly IFactory<IGrenade> _grenadeFactory;
        [SerializeField] private GrenadePickupsFactory _grenadePickupsFactory;
        [SerializeField] private WeaponPickupsFactory _weaponPickupsFactory;
        
        public void Compose(IInventory<(IWeapon, IWeaponInput)> weaponsInventory, IInventory<IGrenade> grenadesInventory)
        {
            _grenadePickupsFactory.Init(grenadesInventory);
            _grenadePickupsFactory.SpawnLoop().Forget();
            var weaponSpawnTypes = new List<WeaponType> { WeaponType.Ak74 , WeaponType.Pistol};
            
            var factoriesContainer = new Dictionary<WeaponType, IFactory<IWeapon>>
            {
                { WeaponType.Ak74, new WeaponFactoryWithShootWaitingAndRollback(_bulletsFactory, _ak74Data) },
                { WeaponType.Pistol, new WeaponFactoryWithShootWaiting(_bulletsFactory, _pistolData) },
                { WeaponType.Rpg, new WeaponFactoryWithShootWaitingAndRollback(_explosiveBulletsFactory, _rpgData) }, 
                { WeaponType.Shotgun, new WeaponFactoryWithShootWaitingAndRollback(_shotgunBulletsFactory, _shotgunData)},
                { WeaponType.PistolWithFireBullets, new WeaponFactoryWithShootWaitingAndRollback(_fireBulletsFactory, _shotgunData)}
            };

            var inputs = new Dictionary<WeaponType, IWeaponInput>
            {
                { WeaponType.Ak74, new StandartWeaponInput() },
                { WeaponType.Pistol, new BurstWeaponInput() },
                { WeaponType.Rpg, new StandartWeaponInput() },
                { WeaponType.Shotgun, new BurstWeaponInput() },
                { WeaponType.PistolWithFireBullets, new BurstWeaponInput()}

            };
            
            _weaponPickupsFactory.Init(weaponSpawnTypes, weaponsInventory, factoriesContainer, inputs);
            _weaponPickupsFactory.SpawnLoop().Forget();
        }
    }
}
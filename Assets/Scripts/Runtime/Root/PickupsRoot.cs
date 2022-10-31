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
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _rpgData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _shotgunData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _ak74Data;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _laserData;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _pistolWithFireBullets;
        [SerializeField, VerticalGroup("Weapon Data")] private WeaponData _healRpgData;
        [SerializeField, VerticalGroup("Weapon Data")] private DualWeaponData _dualPistolsData;

        [SerializeField] private GrenadePickupsFactory _grenadePickupsFactory;
        [SerializeField] private WeaponPickupsFactory _weaponPickupsFactory;
        [SerializeField] private HandWeaponFactory _handWeaponFactory;
        [SerializeField] private GrenadeSelectorRoot _grenadeSelectorRoot;

        private readonly List<WeaponType> _defaultWeapons = new()
        {
            WeaponType.Shotgun
        };
        
        public void Compose(IInventory<(IWeapon, IWeaponInput)> weaponsInventory, IInventory<IGrenade> grenadesInventory)
        {
            _grenadePickupsFactory.Init(grenadesInventory, _grenadeSelectorRoot.Compose());
            _grenadePickupsFactory.SpawnLoop().Forget();
            var weaponTypesStorage = new CollectionStorage<WeaponType>(new BinaryStorage());
          //  var weaponSpawnTypes = weaponTypesStorage.Exists(WeaponsKey.Value)? CreateWeaponsList(weaponTypesStorage.Load(WeaponsKey.Value)): _defaultWeapons;
          var weaponSpawnTypes = new List<WeaponType> { WeaponType.DualPistols, WeaponType.LaserGun };
          
            var factoriesContainer = new Dictionary<WeaponType, IFactory<IWeapon>>
            {
                { WeaponType.Ak74, new WeaponFactoryWithShootWaiting(_ak74Data.BulletsFactory, _ak74Data) },
                { WeaponType.Pistol, new WeaponFactoryWithShootWaiting(_pistolData.BulletsFactory, _pistolData) },
                { WeaponType.Rpg, new WeaponFactoryWithShootWaiting(_rpgData.BulletsFactory, _rpgData) }, 
                { WeaponType.Shotgun, new WeaponFactoryWithShootWaiting(_shotgunData.BulletsFactory, _shotgunData)},
                { WeaponType.PistolWithFireBullets, new WeaponFactoryWithShootWaiting(_pistolWithFireBullets.BulletsFactory, _pistolWithFireBullets)},
                { WeaponType.LaserGun, new WeaponFactoryWithShootWaiting(_laserData.BulletsFactory, _laserData)},
                { WeaponType.Sword, new DummyFactoryFromShootingWeapon(_handWeaponFactory)},
                { WeaponType.HealRpg, new WeaponFactoryWithShootWaiting(_healRpgData.BulletsFactory, _healRpgData)},
                { WeaponType.DualPistols, new DualWeaponFactory(new WeaponFactoryWithShootWaiting(_dualPistolsData.FirstData.BulletsFactory, _dualPistolsData.FirstData), new WeaponFactoryWithShootWaiting(_dualPistolsData.SecondData.BulletsFactory, _dualPistolsData.SecondData))}
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
                { WeaponType.HealRpg, new BurstWeaponInput()},
                { WeaponType.DualPistols, new BurstWeaponInput()}
            };
            
            _weaponPickupsFactory.Init(weaponSpawnTypes.ToList(), weaponsInventory, factoriesContainer, inputs);
            _weaponPickupsFactory.SpawnLoop().Forget();
        }

        private List<WeaponType> CreateWeaponsList(IEnumerable<WeaponType> loadedWeapons)
        {
            var list = loadedWeapons.ToList();
            list.AddRange(_defaultWeapons);
            return list;
        }
    }
}
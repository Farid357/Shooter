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
using UnityEngine.Serialization;

namespace Shooter.Root
{
    public sealed class PickupsRoot : SerializedMonoBehaviour
    {
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData RpgData { get; private set; }
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData PistolData { get; private set; }
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData ShotgunData{ get; private set; }
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData Ak74Data { get; private set; }
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData LaserData { get; private set; }
        
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData PistolWithFireBullets { get; private set; }
        [field: SerializeField, VerticalGroup("Weapon Data")] public WeaponData HealRpgData { get; private set; }
        [field: SerializeField, VerticalGroup("Weapon Data")] public DualWeaponData DualPistolsData { get; private set; }

        [FormerlySerializedAs("_throwingWeaponsAdder")] [SerializeField] private ThrowingWeaponsCountAdder _throwingWeaponsCountAdder;
        [SerializeField] private WeaponPickupsFactory _weaponPickupsFactory;
        [SerializeField] private HandWeaponFactory _handWeaponFactory;
        [SerializeField] private GrenadeSelectorRoot _grenadeSelectorRoot;
        [SerializeField] private ThrowingWeaponsTypeAdder _throwingWeaponsTypeAdder;

        private readonly List<WeaponType> _defaultWeapons = new() { WeaponType.Shotgun };

        public void Compose(IInventory<(IWeapon, IWeaponInput)> weaponsInventory, IInventory<IThrowingWeapon> grenadesInventory)
        {
            _throwingWeaponsCountAdder.Init(grenadesInventory);
            var throwingWeaponTypesStorage = new CollectionStorage<ThrowingWeaponType>(new BinaryStorage());
           // var throwingWeaponTypes = throwingWeaponTypesStorage.Exists(WeaponsKey.Value)
             //   ? CreateThrowingWeaponsList(throwingWeaponTypesStorage.Load(WeaponsKey.Value))
               // : new[] { ThrowingWeaponType.Standart };

            var throwingWeaponTypes = new [] { ThrowingWeaponType.Knife };
            _throwingWeaponsTypeAdder.Init(grenadesInventory, _grenadeSelectorRoot.Compose(), throwingWeaponTypes);
            _throwingWeaponsCountAdder.SpawnLoop().Forget();
            _throwingWeaponsTypeAdder.SpawnNewGrenadeTypeLoop().Forget();

            var weaponTypesStorage = new CollectionStorage<WeaponType>(new BinaryStorage());
          //  var weaponSpawnTypes = weaponTypesStorage.Exists(WeaponsKey.Value)? CreateWeaponsList(weaponTypesStorage.Load(WeaponsKey.Value)): _defaultWeapons;
          var weaponSpawnTypes = new List<WeaponType>
          {
             WeaponType.Sword, WeaponType.LaserGun
          };
          
            var factoriesContainer = new Dictionary<WeaponType, IFactory<IWeapon>>
            {
                { WeaponType.Ak74, new WeaponFactoryWithShootWaiting(Ak74Data.BulletsFactory, Ak74Data) },
                { WeaponType.Pistol, new WeaponFactoryWithShootWaiting(PistolData.BulletsFactory, PistolData) },
                { WeaponType.Rpg, new WeaponFactoryWithShootWaiting(RpgData.BulletsFactory, RpgData) }, 
                { WeaponType.Shotgun, new WeaponFactoryWithShootWaiting(ShotgunData.BulletsFactory, ShotgunData)},
                { WeaponType.PistolWithFireBullets, new WeaponFactoryWithShootWaiting(PistolWithFireBullets.BulletsFactory, PistolWithFireBullets)},
                { WeaponType.LaserGun, new WeaponFactoryWithShootWaiting(LaserData.BulletsFactory, LaserData)},
                { WeaponType.Sword, new DummyFactoryFromShootingWeapon(_handWeaponFactory)},
                { WeaponType.HealRpg, new WeaponFactoryWithShootWaiting(HealRpgData.BulletsFactory, HealRpgData)},
                { WeaponType.DualPistols, new DualWeaponFactory(new WeaponFactoryWithShootWaiting(DualPistolsData.FirstData.BulletsFactory, DualPistolsData.FirstData), new WeaponFactoryWithShootWaiting(DualPistolsData.SecondData.BulletsFactory, DualPistolsData.SecondData))}
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

        private IEnumerable<ThrowingWeaponType> CreateThrowingWeaponsList(IEnumerable<ThrowingWeaponType> loadedTypes)
        {
            var list = loadedTypes.ToList();
            list.Add(ThrowingWeaponType.Standart);
            return list;
        }

        private List<WeaponType> CreateWeaponsList(IEnumerable<WeaponType> loadedWeapons)
        {
            var list = loadedWeapons.ToList();
            list.AddRange(_defaultWeapons);
            return list;
        }
    }
}
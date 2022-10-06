using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Root;
using Shooter.Shop;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shooter.GameLogic
{
    public sealed class WeaponPickupsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponType, (WeaponPickup, ItemGameObjectViewFactory)> _weaponPickups;
        [SerializeField, Min(0.1f)] private float _minDelay = 1.2f;
        [SerializeField, Min(0.1f)] private float _maxDelay = 1.5f;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private IPlayerRoot _playerRoot;
        private List<WeaponType> _weaponSpawnTypes;
        private IInventory<(IWeapon, IWeaponInput)> _inventory;
        private IReadOnlyDictionary<WeaponType,IFactory<IWeapon>> _factoriesContainer;
        private IReadOnlyDictionary<WeaponType, IWeaponInput> _weaponInputs;

        public void Init(List<WeaponType> weaponSpawnTypes, IInventory<(IWeapon, IWeaponInput)> inventory, IReadOnlyDictionary<WeaponType, IFactory<IWeapon>> factoriesContainer, IReadOnlyDictionary<WeaponType, IWeaponInput> weaponInputs)
        {
            _weaponInputs = weaponInputs ?? throw new ArgumentNullException(nameof(weaponInputs));
            _factoriesContainer = factoriesContainer ?? throw new ArgumentNullException(nameof(factoriesContainer));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _weaponSpawnTypes = weaponSpawnTypes ?? throw new ArgumentNullException(nameof(weaponSpawnTypes));
        }
        
        public async UniTaskVoid SpawnLoop()
        {
            while (_weaponSpawnTypes.Count > 0)
            {
                var delay = Random.Range(_minDelay, _maxDelay);
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                var randomTypeIndex = Random.Range(0, _weaponSpawnTypes.Count);
                var weaponSpawnType = _weaponSpawnTypes[randomTypeIndex];
                var pickupPrefab = _weaponPickups[weaponSpawnType].Item1;
                var position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                var pickup = Instantiate(pickupPrefab, position, Quaternion.identity);
                var weapon = _factoriesContainer[weaponSpawnType].Create();
                var item = new Item<(IWeapon, IWeaponInput)>(pickup.ItemData, (weapon, _weaponInputs[weaponSpawnType]),_weaponPickups[weaponSpawnType].Item2.Create());
                var slot = new InventorySlot<(IWeapon, IWeaponInput)>(new WeaponSelector(_playerRoot), item);
                pickup.Init(_inventory, slot);
                _weaponSpawnTypes.Remove(weaponSpawnType);
            }
        }
    }
}
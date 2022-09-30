using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Shop;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shooter.GameLogic
{
    public sealed class PickupsFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponType, WeaponPickup> _weaponPickups;
        [SerializeField, Min(0.1f)] private float _minDelay = 1.2f;
        [SerializeField, Min(0.1f)] private float _maxDelay = 1.5f;
        [SerializeField] private Transform[] _spawnPoints;
        private List<WeaponType> _weaponSpawnTypes;
        private InventorySlot<(IWeapon, IWeaponInput)> _inventorySlot;
        private IInventory<(IWeapon, IWeaponInput)> _inventory;

        public void Init(List<WeaponType> weaponSpawnTypes, IInventory<(IWeapon, IWeaponInput)> inventory, InventorySlot<(IWeapon, IWeaponInput)> inventorySlot)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _inventorySlot = inventorySlot ?? throw new ArgumentNullException(nameof(inventorySlot));
            _weaponSpawnTypes = weaponSpawnTypes ?? throw new ArgumentNullException(nameof(weaponSpawnTypes));
        }
        
        public async UniTaskVoid SpawnLoop()
        {
            while (true)
            {
                var delay = Random.Range(_minDelay, _maxDelay);
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                var randomTypeIndex = Random.Range(0, _weaponSpawnTypes.Count);
                var pickup = _weaponPickups[_weaponSpawnTypes[randomTypeIndex]];
                var position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                Instantiate(pickup, position, Quaternion.identity).Init(_inventory, _inventorySlot);
            }
        }
    }
}
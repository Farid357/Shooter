using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ThrowingWeaponsTypeAdder : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<ThrowingWeaponType, ThrowingWeaponData> _data;
        [SerializeField, Min(0.2f)] private float _addNewTypeDelay = 15f;
        [SerializeField] private Transform _spawnPoint;

        private IInventoryItemSelector<IThrowingWeapon> _grenadeSelector;
        private IInventory<IThrowingWeapon> _inventory;
        private List<ThrowingWeaponType> _throwingWeaponTypes;

        public void Init(IInventory<IThrowingWeapon> inventory, IInventoryItemSelector<IThrowingWeapon> grenadeSelector, IEnumerable<ThrowingWeaponType> throwingWeaponTypes)
        {
            if ( throwingWeaponTypes is null || throwingWeaponTypes.Any(type => _data.ContainsKey(type) == false))
            {
                throw new ArgumentOutOfRangeException(nameof(throwingWeaponTypes));
            }
            
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _grenadeSelector = grenadeSelector ?? throw new ArgumentNullException(nameof(grenadeSelector));
            _throwingWeaponTypes = throwingWeaponTypes.ToList();

        }

        public async UniTaskVoid SpawnNewGrenadeTypeLoop()
        {
            if (_inventory.IsFull == false && _throwingWeaponTypes.Count > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_addNewTypeDelay));
                var throwingWeaponType = _throwingWeaponTypes[0];
                Instantiate(_data[throwingWeaponType], _spawnPoint.position, Quaternion.identity).Prefab.Init(_inventory, _grenadeSelector, _data[throwingWeaponType].Factory);
                _throwingWeaponTypes.RemoveAt(0);
            }
        }
    }
}
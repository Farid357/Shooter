using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ThrowingWeaponsTypeAdder : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<ThrowingWeaponType, GrenadePickup> _prefabs;
        [SerializeField, Min(0.2f)] private float _addNewTypeDelay = 15f;
        [SerializeField] private GameObjectFactory<GrenadeView, CharacterMovement> _grenadesFactory;
        [SerializeField] private Transform _spawnPoint;

        private IInventoryItemSelector<IGrenade> _grenadeSelector;
        private IInventory<IGrenade> _inventory;
        private List<ThrowingWeaponType> _throwingWeaponTypes;

        public void Init(IInventory<IGrenade> inventory, IInventoryItemSelector<IGrenade> grenadeSelector, IEnumerable<ThrowingWeaponType> throwingWeaponTypes)
        {
            if ( throwingWeaponTypes is null || throwingWeaponTypes.Any(type => _prefabs.ContainsKey(type) == false))
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
                Instantiate(_prefabs[throwingWeaponType], _spawnPoint.position, Quaternion.identity).Init(_inventory, _grenadeSelector, _grenadesFactory);
                _throwingWeaponTypes.RemoveAt(0);
            }
        }
    }
}
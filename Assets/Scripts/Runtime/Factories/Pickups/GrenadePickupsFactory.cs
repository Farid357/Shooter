using System;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Root;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class GrenadePickupsFactory : SerializedMonoBehaviour
    {
        [SerializeField, Min(0.2f)] private float _spawnDelay = 15f;
        [SerializeField, Min(1f)] private float _yOffset = 2f;
        [SerializeField] private GrenadePickup _grenade;
        [SerializeField] private ICharacterTransform _character;
        [SerializeField] private IFactory<IGrenade> _grenadesFactory;

        private IInventory<IGrenade> _inventory;
        private IInventoryItemSelector<IGrenade> _grenadeSelector;

        public void Init(IInventory<IGrenade> inventory, IInventoryItemSelector<IGrenade> grenadeSelector)
        {
            if (_inventory is not null)
                throw new InvalidOperationException($"{nameof(GrenadePickupsFactory)} already inited");
            
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _grenadeSelector = grenadeSelector ?? throw new ArgumentNullException(nameof(grenadeSelector));
        }
        
        public async UniTaskVoid SpawnLoop()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelay));
                var position = new Vector3(_character.Position.x, _character.Position.y * _yOffset, _character.Position.z);
                Instantiate(_grenade, position, Quaternion.identity).Init(_inventory, _grenadeSelector, _grenadesFactory);
            }
        }
    }
}
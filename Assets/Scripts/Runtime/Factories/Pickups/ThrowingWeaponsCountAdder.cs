using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ThrowingWeaponsCountAdder : SerializedMonoBehaviour
    {
        [SerializeField, Min(0.2f)] private float _addNewCountDelay = 15f;
        [SerializeField] private int _addAmount = 1;

        private IInventory<IThrowingWeapon> _inventory;
        
        public void Init(IInventory<IThrowingWeapon> inventory)
        {
            if (_inventory is not null)
                throw new InvalidOperationException($"{nameof(ThrowingWeaponsCountAdder)} already inited");

            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public async UniTaskVoid SpawnLoop()
        {
            while (true)
            {
                if (_inventory.Slots.Count() > 0 && _inventory.Slots.Any(slot => slot.ItemsCount < slot.MaxItemsCount))
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_addNewCountDelay));
                    var slot = _inventory.Slots.First(slot => slot.ItemsCount < slot.MaxItemsCount);
                    slot.AddItems(_addAmount);
                }

                await UniTask.Yield();
            }
        }
    }
}
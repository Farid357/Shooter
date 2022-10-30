using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Shooter.Model.Inventory
{
    public sealed class InventoryItemsDropInput<TItem> : IUpdateble
    {
        private readonly IReadOnlyDictionary<KeyCode, int> _keypadNumbers;
        private readonly IInventory<TItem> _inventory;

        public InventoryItemsDropInput(IReadOnlyDictionary<KeyCode, int> keypadNumbers, IInventory<TItem> inventory)
        {
            _keypadNumbers = keypadNumbers ?? throw new ArgumentNullException(nameof(keypadNumbers));
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public async void Update(float deltaTime)
        {
            if (Input.anyKeyDown == false)
                return;

            var wasSelectedItem = false;
            var keydown = KeyCode.None;
            
            foreach (var key in _keypadNumbers.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    keydown = key;
                    UniTask.Create(async () =>
                    {
                        wasSelectedItem = true;
                        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
                        wasSelectedItem = false;
                    });
                }

            }

            if (wasSelectedItem && Input.GetKeyDown(KeyCode.X))
            {
                var containsItem = _inventory.Slots.Count() > _keypadNumbers[keydown];

                if (containsItem)
                {
                    var model = _inventory.Slots.ElementAt(_keypadNumbers[keydown]).Item.Model;
                    _inventory.Drop(model);
                    await System.Threading.Tasks.Task.Yield();
                }
            }
        }
    }
}
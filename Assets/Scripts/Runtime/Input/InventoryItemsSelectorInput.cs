using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemsSelectorInput : IUpdateble
    {
        private readonly IReadOnlyDictionary<KeyCode, int> _keypadNumbers;
        private readonly IInventoryItemsSelector _itemsSelector;
        
        public InventoryItemsSelectorInput(IReadOnlyDictionary<KeyCode, int> keypadNumbers,  IInventoryItemsSelector itemsSelector)
        {
            _keypadNumbers = keypadNumbers ?? throw new ArgumentNullException(nameof(keypadNumbers));
            _itemsSelector = itemsSelector ?? throw new ArgumentNullException(nameof(itemsSelector));
        }

        public void Update(float deltaTime)
        {
            foreach (var (key, number) in _keypadNumbers)
            {
                if (Input.GetKeyDown(key))
                {
                    if (_itemsSelector.CanSelect(number))
                    {
                        _itemsSelector.Select(number);
                    }
                }
            }
        }
    }
}
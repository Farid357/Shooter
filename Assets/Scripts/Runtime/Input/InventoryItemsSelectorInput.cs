using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Model;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemsSelectorInput : IUpdateble
    {
        private readonly IReadOnlyDictionary<KeyCode, int> _keypadNumbers;
        private readonly IEnumerable<IInventoryItemsSelector> _allInventoryItemsSelectors;
        private readonly IInventoryItemsSelector _itemsSelector;
        
        public InventoryItemsSelectorInput(IReadOnlyDictionary<KeyCode, int> keypadNumbers, IEnumerable<IInventoryItemsSelector> allInventoryItemsSelectors, IInventoryItemsSelector itemsSelector)
        {
            _keypadNumbers = keypadNumbers ?? throw new ArgumentNullException(nameof(keypadNumbers));
            _allInventoryItemsSelectors = allInventoryItemsSelectors ?? throw new ArgumentNullException(nameof(allInventoryItemsSelectors));
            _itemsSelector = itemsSelector ?? throw new ArgumentNullException(nameof(itemsSelector));
        }

        public void Update(float deltaTime)
        {
            if(Input.anyKeyDown == false)
                return;

            foreach (var (key, number) in _keypadNumbers)
            {
                if (Input.GetKeyDown(key) && _itemsSelector.CanSelect(number))
                {
                    _allInventoryItemsSelectors.ToList().
                        FindAll(selector => selector.CanUnselect)
                        .ForEach(selector => selector.Unselect());

                    _itemsSelector.Select(number);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Model;
using Shooter.Model.Inventory;
using Shooter.Player;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryItemsSelector<T> : IUpdateble
    {
        private readonly IInventoryItemsContainer<T> _inventory;
        private readonly IReadOnlyDictionary<KeyCode, int> _keypadNumbers;
        private readonly IPlayer _player;
        private Item<T> _lastSelectedItem;

        public InventoryItemsSelector(Item<T> startSelectedItem, IInventoryItemsContainer<T> inventory,
            IReadOnlyDictionary<KeyCode, int> keypadNumbers, IPlayer player)
        {
            _lastSelectedItem = startSelectedItem;
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _keypadNumbers = keypadNumbers ?? throw new ArgumentNullException(nameof(keypadNumbers));
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void Update(float deltaTime)
        {
            foreach (var (key, number) in _keypadNumbers)
            {
                if (Input.GetKeyDown(key))
                {
                    if (_inventory.Contains(number))
                    {
                        _lastSelectedItem.View.Hide();
                        var item = _inventory.Items.ElementAt(number);
                        item.View.Show();
                        _lastSelectedItem = item;

                        if (item.ObjectIsWeapon(out var weapon))
                            _player.SwitchWeapon(weapon);
                    }
                }
            }
        }
    }
}
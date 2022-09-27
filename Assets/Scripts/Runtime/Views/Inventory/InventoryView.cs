using System.Collections.Generic;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventoryItemView _prefab;
        [SerializeField] private Transform _secondInventoryContent;
        [SerializeField, Min(1)] private int _itemsCountOnOneInventory = 5;
        
        private int _visualizedItemsCount;
        private readonly Dictionary<ItemData, InventoryItemView> _items = new();
        
        public void VisualizeNewItem(ItemData item, int count)
        {
            _visualizedItemsCount++;
            Visualize(item, count, _visualizedItemsCount < _itemsCountOnOneInventory ? _content : _secondInventoryContent);
        }

        public void VisualizeItemsCount(ItemData item, int count)
        {
            _items[item].Visualize(item.Sprite, count);
        }

        public void DropItem(ItemData item)
        {
            var createdItem = _items[item].gameObject;
            _items.Remove(item);
            Destroy(createdItem);
        }

        private void Visualize(ItemData item, int count, Transform parent)
        {
            var itemView = Instantiate(_prefab, parent);
            itemView.Visualize(item.Sprite, count);
            _items.Add(item, itemView);
        }
    }
}
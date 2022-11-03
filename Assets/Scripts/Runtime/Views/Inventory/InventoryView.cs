using System.Collections.Generic;
using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventoryItemView _prefab;

        private readonly Dictionary<ItemData, InventoryItemView> _items = new();
        
        public void VisualizeNewItem(ItemData item, int count)
        {
            var itemView = Instantiate(_prefab, _content);
            itemView.Visualize(item.Sprite, count);
            _items.Add(item, itemView);
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
    }
}
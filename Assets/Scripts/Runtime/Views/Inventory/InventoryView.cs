using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventoryItemView _prefab;
        [SerializeField] private Transform _secondInventoryContent;
        private int _visualizedItemsCount;
        private const int ItemsCountOnOneInventory = 5;
        
        public void VisualizeItem(ItemData item, int count)
        {
            _visualizedItemsCount++;
            Visualize(item, count, _visualizedItemsCount < ItemsCountOnOneInventory ? _content : _secondInventoryContent);
        }

        private void Visualize(ItemData item, int count, Transform parent)
        {
            var itemView = Instantiate(_prefab, parent);
            itemView.Visualize(item.Sprite, count);
        }
    }
}
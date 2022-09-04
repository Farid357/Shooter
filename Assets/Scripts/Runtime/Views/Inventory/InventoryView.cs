using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private InventoryItemView _prefab;

        public void VisualizeItem(ItemData item, int count)
        {
            var itemView = Instantiate(_prefab, _parent);
            itemView.Visualize(item.Sprite, count);
        }
    }
}
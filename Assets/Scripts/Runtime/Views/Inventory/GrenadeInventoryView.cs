using Shooter.Model.Inventory;
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public sealed class GrenadeInventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventoryItemView _prefab;
        
        public void VisualizeItem(ItemData item, int count)
        {
            var itemView = Instantiate(_prefab, _content);
            itemView.Visualize(item.Sprite, count);
        }
    }
}
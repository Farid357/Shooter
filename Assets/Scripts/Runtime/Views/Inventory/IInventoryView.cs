using Shooter.GameLogic.Inventory;

namespace Shooter.Model.Inventory
{
    public interface IInventoryView
    {
        public void VisualizeNewItem(ItemData item, int count);

        public void VisualizeItemsCount(ItemData item, int count);

        public void DropItem(ItemData item);
    }
}
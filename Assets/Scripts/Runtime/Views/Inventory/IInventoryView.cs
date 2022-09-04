using Shooter.GameLogic.Inventory;

namespace Shooter.Model.Inventory
{
    public interface IInventoryView
    {
        public void VisualizeItem(ItemData item, int count);
    }
}
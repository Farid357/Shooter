using System.Collections.Generic;

namespace Shooter.Model.Inventory
{
    public interface IReadOnlyInventory<TItem>
    {
        public IEnumerable<InventorySlot<TItem>> Slots { get; }
    }
}
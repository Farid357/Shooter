using Shooter.GameLogic.Inventory;
using Shooter.Model.Inventory;

public sealed class DummyInventoryView : IInventoryView
{
    public bool IsVisualized { get; private set; }
    
    public void VisualizeNewItem(ItemData item, int count) => IsVisualized = true;
    
    public void VisualizeItemsCount(ItemData item, int count)
    {
        
    }

    public void DropItem(ItemData item)
    {
    }
}
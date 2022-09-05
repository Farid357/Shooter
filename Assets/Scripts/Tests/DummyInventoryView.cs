using Shooter.GameLogic.Inventory;
using Shooter.Model.Inventory;

public sealed class DummyInventoryView : IInventoryView
{
    public bool IsVisualized { get; private set; }
    
    public void VisualizeItem(ItemData item, int count) => IsVisualized = true;
    
}
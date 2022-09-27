namespace Shooter.Model.Inventory
{
    public sealed class DummyItemsSelector : IInventoryItemsSelector
    {
        public bool CanUnselect => true;
        
        public void Select(int index)
        {
            
        }

        public void Unselect()
        {
        }

        public void ReplaceAnotherSelector(IInventoryItemsSelector selector)
        {
            
        }

        public bool CanSelect(int index) => true;
        
    }
}
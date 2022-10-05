namespace Shooter.Model.Inventory
{
    public interface IInventoryItemSelector<in T>
    {
        public void Select(T item);

        public void Unselect();
    }
}
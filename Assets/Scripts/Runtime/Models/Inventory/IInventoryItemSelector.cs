namespace Shooter.Model.Inventory
{
    public interface IInventoryItemSelector<in T>
    {
        public void Select(T grenade);

        public void Unselect();
    }
}
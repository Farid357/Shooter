namespace Shooter.Model.Inventory
{
    public interface IItemSelector<in T>
    {
        public void Select(T item);
    }
}
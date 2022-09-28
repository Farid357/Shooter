namespace Shooter.Shop
{
    public interface IShoppingCartView
    {
        void Visualize(IGood good);

        void Remove(IGood good);

        void Clear();
    }
}
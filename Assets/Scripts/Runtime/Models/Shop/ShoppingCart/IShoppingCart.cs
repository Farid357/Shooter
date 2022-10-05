namespace Shooter.Shop
{
    public interface IShoppingCart : IReadOnlyShoppingCart
    {
        void Add(IGood good);

        void Remove(IGood good);

        void Clear();

    }
}
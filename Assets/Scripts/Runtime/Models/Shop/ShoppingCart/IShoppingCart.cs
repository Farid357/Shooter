using System.Collections.Generic;

namespace Shooter.Shop
{
    public interface IShoppingCart
    {
        IEnumerable<IGood> Goods { get; }
        
        int TotalPrice { get; }

        void Add(IGood good);

        void Remove(IGood good);

        void Clear();

    }
}
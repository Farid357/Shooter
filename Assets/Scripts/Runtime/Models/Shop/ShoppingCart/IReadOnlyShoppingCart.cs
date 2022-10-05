using System.Collections.Generic;

namespace Shooter.Shop
{
    public interface IReadOnlyShoppingCart
    {
        IEnumerable<IGood> Goods { get; }
        
        int TotalPrice { get; }
    }
}
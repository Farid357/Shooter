using System;
using System.Collections.Generic;
using System.Linq;

namespace Shooter.Shop
{
    public sealed class ShoppingCart : IShoppingCart
    {
        private readonly List<IGood> _goods = new();
        private readonly IShoppingCartView _view;

        public ShoppingCart(IShoppingCartView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }
        
        public IEnumerable<IGood> Goods => _goods;
        
        public int TotalPrice => _goods.Select(good => good.Data.Price).Sum();

        public void Add(IGood good)
        {
            if (good is null)
                throw new ArgumentNullException(nameof(good));

            _goods.Add(good);
            _view.Visualize(good);
        }

        public void Remove(IGood good)
        {
            if (good is null)
                throw new ArgumentNullException(nameof(good));

            if (_goods.Contains(good) == false)
                throw new InvalidOperationException("Shopping cart doesn't contain this good!");

            _goods.Remove(good);
            _view.Remove(good);
        }
        
        public void Clear()
        {
            _goods.Clear();
            _view.Clear();
        }
    }
}
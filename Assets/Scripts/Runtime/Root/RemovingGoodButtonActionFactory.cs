using System;
using Shooter.Shop;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class RemovingGoodButtonActionFactory : IRemovingGoodButtonOnClickActionFactory
    {
        private readonly IShoppingCart _shoppingCart;

        public RemovingGoodButtonActionFactory(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public IButtonClickAction Create(IGood good)
        {
            return new RemovingGoodButtonAction(good, _shoppingCart);
        }
    }
}
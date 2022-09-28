using System;
using Shooter.GameLogic;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class SelectingGoodButtonAction : IButtonClickAction
    {
        private readonly IGood _good;
        private readonly IShoppingCart _shoppingCart;

        public SelectingGoodButtonAction(IGood good, IShoppingCart shoppingCart)
        {
            _good = good ?? throw new ArgumentNullException(nameof(good));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public void OnClick() => _shoppingCart.Add(_good);
        
    }
}
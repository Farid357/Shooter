using System;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class ClearingGoodsButtonAction : IButtonClickAction
    {
        private readonly IShoppingCart _shoppingCart;

        public ClearingGoodsButtonAction(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public void OnClick() => _shoppingCart.Clear();
        
    }
}
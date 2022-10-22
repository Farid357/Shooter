using System;
using System.Linq;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class ClearingGoodsButtonAction : IButtonClickAction
    {
        private readonly IShoppingCart[] _shoppingCarts;

        public ClearingGoodsButtonAction(IShoppingCart[] shoppingCarts)
        {
            _shoppingCarts = shoppingCarts ?? throw new ArgumentNullException(nameof(shoppingCarts));
        }

        public void OnClick()
        {
            foreach (var shoppingCart in _shoppingCarts)
            {
                if(shoppingCart.Goods.Count() == 0)
                    continue;
                
                shoppingCart.Clear();
            }
        }
    }
}
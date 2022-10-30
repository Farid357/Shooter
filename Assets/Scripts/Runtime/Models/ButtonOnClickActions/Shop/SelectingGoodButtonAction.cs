using System;
using System.Linq;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class SelectingGoodButtonAction : IButtonClickAction
    {
        private readonly IGood _good;
        private readonly IShoppingCart _shoppingCart;
        private readonly IButton _button;

        public SelectingGoodButtonAction(IGood good, IShoppingCart shoppingCart, IButton button)
        {
            _good = good ?? throw new ArgumentNullException(nameof(good));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _button = button ?? throw new ArgumentNullException(nameof(button));
            _button.Enable();

            if (_shoppingCart.Goods.Any(g => g.Data.Name == good.Data.Name))
                _button.Disable();
        }

        public void OnClick()
        {
            _shoppingCart.Add(_good);
            _button.Disable();
        }
    }
}
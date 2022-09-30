using System;
using Shooter.Model;
using Shooter.Shop;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class GoodSwitchingView : MonoBehaviour, IGoodSwitchingView
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private SelectingGoodButton _selectingGoodButton;
        [SerializeField] private GoodInContentView _goodView;
        private IShoppingCart _shoppingCart;

        public void Init(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _goodView.Init(_selectingGoodButton);
        }
        
        public void Switch(IGood good)
        {
            _priceText.text = good.Data.Price.ToString();
            _nameText.text = good.Data.Name;
            _goodView.Visualize(good.Data);
            _selectingGoodButton.Subscribe(new SelectingGoodButtonAction(good, _shoppingCart));
        }
    }
}
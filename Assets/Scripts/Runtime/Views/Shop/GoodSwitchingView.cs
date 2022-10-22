using System;
using System.Collections.Generic;
using System.ComponentModel;
using Shooter.Model;
using Shooter.Shop;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.GameLogic
{
    public sealed class GoodSwitchingView : SerializedMonoBehaviour, IGoodSwitchingView
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Dictionary<WalletType, Sprite> _paymentSystemsSprites;
        [SerializeField] private Image _paymentSystemImage;
        [SerializeField] private SelectingGoodButton _selectingGoodButton;
        private IReadOnlyDictionary<WalletType, IShoppingCart> _shoppingCarts;
        
        [field: SerializeField] public GoodInContentView GoodView { get; private set; }

        public void Init(IReadOnlyDictionary<WalletType, IShoppingCart> shoppingCarts)
        {
            _shoppingCarts = shoppingCarts ?? throw new ArgumentNullException(nameof(shoppingCarts));
            GoodView.Init(_selectingGoodButton);
        }
        
        public void Switch(IGood good, WalletType walletType)
        {
            if (!Enum.IsDefined(typeof(WalletType), walletType))
                throw new InvalidEnumArgumentException(nameof(walletType), (int)walletType, typeof(WalletType));

            if (_shoppingCarts.ContainsKey(walletType) == false)
                throw new ArgumentOutOfRangeException(nameof(walletType));

            _paymentSystemImage.sprite = _paymentSystemsSprites[walletType];
            _priceText.text = good.Data.Price.ToString();
            _nameText.text = good.Data.Name;
            GoodView.Visualize(good.Data);
            _selectingGoodButton.DeleteAllSubscribers();
            _selectingGoodButton.Subscribe(new SelectingGoodButtonAction(good, _shoppingCarts[walletType], _selectingGoodButton));
        }
    }
}
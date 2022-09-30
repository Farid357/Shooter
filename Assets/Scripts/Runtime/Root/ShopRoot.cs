using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Shop;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class ShopRoot : CompositeRoot
    {
        [SerializeField] private WeaponGoodData[] _weaponGoodData;
        [SerializeField] private ShoppingCartView _shoppingCartView;
        [SerializeField] private INotEnoughMoneyView _notEnoughMoneyView;
        [SerializeField] private IView<int> _moneyView;
        [SerializeField] private BuyGoodButton _buyGoodButton;
        [SerializeField] private ClearingGoodsButton _clearingGoodsButton;
        [SerializeField] private SwitchingRightGoodButton _switchingGoodRightButton;
        [SerializeField] private SwitchingLeftGoodButton _switchingGoodLeftButton;
        [SerializeField] private GoodSwitchingView _goodSwitchingView;
        
        public override void Compose()
        {
            IShoppingCart shoppingCart = new ShoppingCart(_shoppingCartView);
            _goodSwitchingView.Init(shoppingCart);
            _shoppingCartView.Init(new RemovingGoodButtonActionFactory(shoppingCart));
            IClient client = new Client(_notEnoughMoneyView, new Wallet(_moneyView, new BinaryStorage()), shoppingCart);
            _buyGoodButton.Subscribe(new BuyGoodButtonAction(client));
            var goods = CreateWeaponGoods();
            _switchingGoodLeftButton.Subscribe(new SwitchingLeftGoodAction(_goodSwitchingView, goods));
            var switchingRightGoodAction = new SwitchingRightGoodAction(_goodSwitchingView, goods);
            _switchingGoodRightButton.Subscribe(switchingRightGoodAction);
            switchingRightGoodAction.OnClick();
            _clearingGoodsButton.Subscribe(new ClearingGoodsButtonAction(shoppingCart));
        }

        private IEnumerable<IGood> CreateWeaponGoods()
        {
            foreach (var weaponGoodData in _weaponGoodData)
            {
                yield return new WeaponGood(new Good(weaponGoodData), weaponGoodData.Type, new CollectionStorage<WeaponType>(new BinaryStorage()));
            }
        }
    }
}
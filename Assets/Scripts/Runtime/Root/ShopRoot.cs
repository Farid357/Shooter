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
        [SerializeField] private SwitchingRightGoodButton _switchingRightGoodButton;
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
            var switchingGoodAction = new SwitchingGoodAction(_goodSwitchingView, goods);
            _switchingGoodLeftButton.Subscribe(switchingGoodAction);
            _switchingRightGoodButton.Subscribe(switchingGoodAction);
            _clearingGoodsButton.Subscribe(new ClearingGoodsButtonAction(shoppingCart));
            switchingGoodAction.SwitchRight();
            switchingGoodAction.SwitchLeft();
        }

        private IEnumerable<IGood> CreateWeaponGoods()
        {
            foreach (var weaponGoodData in _weaponGoodData)
            {
                yield return new WeaponGood(new Good(_goodSwitchingView.GoodView, weaponGoodData), weaponGoodData.Type, new CollectionStorage<WeaponType>(new BinaryStorage()));
            }
        }
    }
}
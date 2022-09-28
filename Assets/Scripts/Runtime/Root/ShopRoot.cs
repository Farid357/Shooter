using System.Collections.Generic;
using JetBrains.Annotations;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Shop;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class ShopRoot : CompositeRoot
    {
        [SerializeField] private WeaponGoodData[] _weaponGoodData;
        [SerializeField] private GoodViewsFactory _goodViewsFactory;
        [SerializeField] private ShoppingCartView _shoppingCartView;
        [SerializeField] private IEnoughMoneyView _enoughMoneyView;
        [SerializeField] private IView<int> _moneyView;
        [SerializeField] private BuyGoodButton _buyGoodButton;
        [SerializeField] private ClearingGoodsButton _clearingGoodsButton;
        
        public override void Compose()
        {
            IShoppingCart shoppingCart = new ShoppingCart(_shoppingCartView);
            _shoppingCartView.Init(new RemovingGoodButtonActionFactory(shoppingCart));
            IClient client = new Client(_enoughMoneyView, new Wallet(_moneyView, new BinaryStorage()), shoppingCart);
            _buyGoodButton.Subscribe(new BuyGoodButtonAction(client));
            _goodViewsFactory.Create(CreateWeaponGoods(), shoppingCart);
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
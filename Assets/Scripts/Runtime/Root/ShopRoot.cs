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
        [SerializeField] private AbilityGoodData[] _characterSpeedBoostAbilityGoodData;
        [SerializeField] private AbilityGoodData[] _increaseBulletsAbilityGoodData;

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
            IClient client = new Client(new Wallet(_moneyView, new BinaryStorage()), shoppingCart);
            _buyGoodButton.Subscribe(new BuyGoodButtonAction(client, _notEnoughMoneyView));
            var goods = CreateGoods();
            var switchingGoodAction = new SwitchingGoodAction(_goodSwitchingView, goods);
            _switchingGoodLeftButton.Subscribe(switchingGoodAction);
            _switchingRightGoodButton.Subscribe(switchingGoodAction);
            _clearingGoodsButton.Subscribe(new ClearingGoodsButtonAction(shoppingCart));
            switchingGoodAction.SwitchRight();
            switchingGoodAction.SwitchLeft();
        }

        private IEnumerable<IGood> CreateGoods()
        {
            foreach (var weaponGoodData in _weaponGoodData)
            {
                yield return new WeaponGood(new Good(_goodSwitchingView.GoodView, weaponGoodData), weaponGoodData.Type, new CollectionStorage<WeaponType>(new BinaryStorage()));
            }

            foreach (var goodData in _characterSpeedBoostAbilityGoodData)
            {
                yield return CreateGoodAbility<CharacterSpeedBoostAbility>(goodData);
            }

            foreach (var goodData in _increaseBulletsAbilityGoodData)
            { 
                yield return CreateGoodAbility<CharacterIncreaseBulletsReward>(goodData);
            }
        }

        private IGood CreateGoodAbility<TUserStorage>(AbilityGoodData goodData)
        {
            var good = new Good(_goodSwitchingView.GoodView, goodData);
            return new SaveGood<TUserStorage, float>(good, goodData.ForUsingSeconds, new BinaryStorage());
        }
    }
}
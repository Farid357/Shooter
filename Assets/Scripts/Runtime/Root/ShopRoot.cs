using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Shooter.Shop;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class ShopRoot : CompositeRoot
    {
        [Title("Data")]
        [SerializeField] private WeaponGoodData[] _weaponGoodData;
        [SerializeField] private AbilityGoodData[] _characterSpeedBoostAbilityGoodData;
        [SerializeField] private AbilityGoodData[] _increaseBulletsAbilityGoodData;
        [SerializeField] private ArmorGoodData[] _armorGoodData;
        [SerializeField] private IWalletRoot _walletRoot;
        
        [Title("Views")]
        [SerializeField] private INotEnoughMoneyView _notEnoughMoneyView;
        [SerializeField] private ShoppingCartView _shoppingCartView;
        [SerializeField] private GoodSwitchingView _goodSwitchingView;
        
        [Title("Buttons")]
        [SerializeField] private BuyGoodButton _buyGoodButton;
        [SerializeField] private ClearingGoodsButton _clearingGoodsButton;
        [SerializeField] private SwitchingRightGoodButton _switchingRightGoodButton;
        [SerializeField] private SwitchingLeftGoodButton _switchingGoodLeftButton;
        
        public override void Compose()
        {
            IShoppingCart shoppingCart = new ShoppingCart(_shoppingCartView);
            _goodSwitchingView.Init(shoppingCart);
            _shoppingCartView.Init(new RemovingGoodButtonActionFactory(shoppingCart));
            IClient client = new Client(_walletRoot.Wallet(), shoppingCart);
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
                yield return new WeaponGood(new Good(_goodSwitchingView.UsingGoodView, weaponGoodData), weaponGoodData.Type, new CollectionStorage<WeaponType>(new BinaryStorage()));
            }

            foreach (var goodData in _characterSpeedBoostAbilityGoodData)
            {
                yield return CreateGoodAbility<CharacterSpeedBoostAbility>(goodData);
            }

            foreach (var goodData in _increaseBulletsAbilityGoodData)
            { 
                yield return CreateGoodAbility<CharacterIncreaseBulletsReward>(goodData);
            }

            foreach (var goodData in _armorGoodData)
            {
                yield return new SaveGood<CharacterHealthView, int>(new Good(_goodSwitchingView.UsingGoodView, goodData), goodData.Protection, new BinaryStorage());
            }
        }

        private IGood CreateGoodAbility<TUserStorage>(AbilityGoodData goodData)
        {
            var good = new Good(_goodSwitchingView.UsingGoodView, goodData);
            return new SaveGood<TUserStorage, float>(good, goodData.ForUsingSeconds, new BinaryStorage());
        }
    }
}
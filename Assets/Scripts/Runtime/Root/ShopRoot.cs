﻿using System.Collections.Generic;
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
        [SerializeField] private BuyGoodsButton _buyGoodsButton;
        [SerializeField] private ClearingGoodsButton _clearingGoodsButton;
        [SerializeField] private SwitchingRightGoodButton _switchingRightGoodButton;
        [SerializeField] private SwitchingLeftGoodButton _switchingGoodLeftButton;

        public override void Compose()
        {
            IShoppingCart shoppingCart = new ShoppingCart(_shoppingCartView);
            IShoppingCart diamondsShoppingCart = new ShoppingCart(_shoppingCartView);
            _goodSwitchingView.Init(new Dictionary<WalletType, IShoppingCart>
                {
                    { WalletType.WithCoins, shoppingCart },
                    { WalletType.WithDiamonds, diamondsShoppingCart }
                }
            );
            
            _shoppingCartView.Init(new RemovingGoodButtonActionFactory(shoppingCart));
            IClient client = new Client(_walletRoot.CoinsWallet(), shoppingCart);
            IClient clientWithDiamondsWallet = new Client(_walletRoot.DiamondsWallet(), diamondsShoppingCart);
            _buyGoodsButton.Subscribe(new BuyGoodsButtonAction(new[] { client, clientWithDiamondsWallet }, _notEnoughMoneyView));
            var goods = CreateGoods();
            var switchingGoodAction = new SwitchingGoodAction(_goodSwitchingView, goods);
            _switchingGoodLeftButton.Subscribe(switchingGoodAction);
            _switchingRightGoodButton.Subscribe(switchingGoodAction);
            _clearingGoodsButton.Subscribe(new ClearingGoodsButtonAction(new[] { shoppingCart, diamondsShoppingCart }));
            switchingGoodAction.SwitchRight();
            switchingGoodAction.SwitchLeft();
        }

        private IEnumerable<(IGood, WalletType)> CreateGoods()
        {
            foreach (var weaponGoodData in _weaponGoodData)
            {
                yield return (new WeaponGood(new Good(_goodSwitchingView.GoodView, weaponGoodData), weaponGoodData.Type,
                        new CollectionStorage<WeaponType>(new BinaryStorage())), weaponGoodData.WalletForPay);
            }

            foreach (var goodData in _characterSpeedBoostAbilityGoodData)
            {
                yield return (CreateGoodAbility<CharacterSpeedBoostAbility>(goodData), goodData.WalletForPay);
            }

            foreach (var goodData in _increaseBulletsAbilityGoodData)
            {
                yield return (CreateGoodAbility<CharacterIncreaseBulletsReward>(goodData), goodData.WalletForPay);
            }

            foreach (var goodData in _armorGoodData)
            {
                yield return (new SaveGood<CharacterHealthView, int>(new Good(_goodSwitchingView.GoodView, goodData),
                        goodData.Protection, new BinaryStorage()), goodData.WalletForPay);
            }
        }

        private IGood CreateGoodAbility<TUserStorage>(AbilityGoodData goodData)
        {
            var good = new Good(_goodSwitchingView.GoodView, goodData);
            return new SaveGood<TUserStorage, float>(good, goodData.ForUsingSeconds, new BinaryStorage());
        }
    }
}
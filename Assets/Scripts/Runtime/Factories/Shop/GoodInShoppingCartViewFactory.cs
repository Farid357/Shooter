using System;
using Shooter.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Shooter.Shop
{
    public sealed class GoodInShoppingCartViewFactory : IGoodInShoppingCartViewFactory
    {
        private readonly GoodInShoppingCartView _prefab;
        private readonly Transform _content;
        private readonly IShoppingCart _shoppingCart;
        
        public GoodInShoppingCartViewFactory(GoodInShoppingCartView prefab, Transform content, IShoppingCart shoppingCart)
        {
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
            _content = content ?? throw new ArgumentNullException(nameof(content));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public IGoodView Create(IGood good)
        {
            var goodView = Object.Instantiate(_prefab, _content);
            goodView.RemovingButton.Subscribe(new EnableGoodSelectingButtonAction(new SelectingButtonFromDataFinder(), good.Data,
                new RemovingGoodButtonAction(good, _shoppingCart)));
            return goodView;
        }
    }
}
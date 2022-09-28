using System;
using System.Collections.Generic;
using Shooter.Model;
using Shooter.Tools;
using Sirenix.Utilities;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class ShoppingCartView : MonoBehaviour, IShoppingCartView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private GoodInShoppingCartView _prefab;
        private readonly Dictionary<GoodData, GoodView> _goodViews = new();
        private IRemovingGoodButtonOnClickActionFactory _removingButtonActionFactory;

        public void Init(IRemovingGoodButtonOnClickActionFactory removingButtonActionFactory)
        {
            _removingButtonActionFactory = removingButtonActionFactory ?? throw new ArgumentNullException(nameof(removingButtonActionFactory));
        }
        
        public void Visualize(IGood good)
        {
            var goodView = Instantiate(_prefab, _content);
            goodView.Visualize(good.Data);
            goodView.RemovingButton.Subscribe(new EnableGoodSelectingButtonAction(new SelectingButtonFromDataFinder(), good.Data,
                _removingButtonActionFactory.Create(good)));
            _goodViews.Add(good.Data, goodView);
        }

        public void Remove(IGood good) => Destroy(_goodViews[good.Data]);

        public void Clear() => _goodViews.ForEach(good => Destroy(good.Value));
        
    }
}
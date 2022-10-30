using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class ShoppingCartView : MonoBehaviour, IShoppingCartView
    {
        [SerializeField] private TMP_Text _totalPrice;
        
        private readonly Dictionary<IGoodData, IGoodView> _goodViews = new();
        private IGoodInShoppingCartViewFactory _goodViewFactory;

        public void Init(IGoodInShoppingCartViewFactory goodViewFactory)
        {
            _goodViewFactory = goodViewFactory ?? throw new ArgumentNullException(nameof(goodViewFactory));
        }
        
        public void Visualize(IGood good)
        {
            var goodView = _goodViewFactory.Create(good);
            goodView.Visualize(good.Data);
            _goodViews.Add(good.Data, goodView);
        }

        public void Remove(IGood good)
        {
            _goodViews[good.Data].Destroy();
            _goodViews.Remove(good.Data);
        }

        public void Clear()
        {
            _goodViews.ForEach(good => good.Value.Destroy());
            _goodViews.Clear();
        }

        public void VisualizeTotalPrice(int totalPrice) => _totalPrice.text = totalPrice.ToString();
    }
}
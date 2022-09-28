using System;
using System.Collections.Generic;
using Shooter.Model;
using UnityEngine;

namespace Shooter.Shop
{
    public sealed class GoodViewsFactory : MonoBehaviour
    {
        [SerializeField] private GoodInContentView _prefab;
        [SerializeField] private Transform _parent;
        
        public void Create(IEnumerable<IGood> goods, IShoppingCart shoppingCart)
        {
            if (goods == null)
                throw new ArgumentNullException(nameof(goods));
            
            if (shoppingCart == null)
                throw new ArgumentNullException(nameof(shoppingCart));
            
            foreach (var good in goods)
            {
                var data = Instantiate(_prefab, _parent);
                data.SelectingButton.Subscribe(new SelectingGoodButtonAction(good, shoppingCart));
            }
        }
    }
}
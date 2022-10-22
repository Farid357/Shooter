using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class SwitchingGoodAction : ISwitchingGoodAction
    {
        private readonly IGoodSwitchingView _goodSwitchingView;
        private readonly IEnumerable<(IGood Self, WalletType WalletType)> _goods;
        private int _index;

        public SwitchingGoodAction(IGoodSwitchingView goodSwitchingView, IEnumerable<(IGood, WalletType)> goods)
        {
            _goodSwitchingView = goodSwitchingView ?? throw new ArgumentNullException(nameof(goodSwitchingView));
            _goods = goods ?? throw new ArgumentNullException(nameof(goods));
        }

        public bool CanSwitchLeft() => _index - 1 >= 0;

        public bool CanSwitchRight() => _index + 1 < _goods.Count();

        public void SwitchLeft()
        {
            if (CanSwitchLeft() == false)
                throw new InvalidOperationException(nameof(CanSwitchLeft));
            
            _index--;
            _goodSwitchingView.Switch(_goods.ElementAt(_index).Self, _goods.ElementAt(_index).WalletType);
        }

        public void SwitchRight()
        {
            if (CanSwitchRight() == false)
                throw new InvalidOperationException(nameof(CanSwitchRight));
            
            _index++;
            _goodSwitchingView.Switch(_goods.ElementAt(_index).Self, _goods.ElementAt(_index).WalletType);
        }
        
    }
}
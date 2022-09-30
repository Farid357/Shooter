using System;
using System.Collections.Generic;
using System.Linq;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class SwitchingLeftGoodAction : ISwitchingGoodAction
    {
        private readonly IGoodSwitchingView _goodSwitchingView;
        private readonly IEnumerable<IGood> _goods;

        public SwitchingLeftGoodAction(IGoodSwitchingView goodSwitchingView, IEnumerable<IGood> goods)
        {
            _goodSwitchingView = goodSwitchingView ?? throw new ArgumentNullException(nameof(goodSwitchingView));
            _goods = goods ?? throw new ArgumentNullException(nameof(goods));
        }

        public int Index { get; private set; }
        
        public void OnClick()
        {
            Index--;
            if (CanSwitch(Index) == false)
                throw new InvalidOperationException($"{nameof(CanSwitch) } is false");
            
            _goodSwitchingView.Switch(_goods.ElementAt(Index));
        }

        public bool CanSwitch(int index) => index > 0;
    }
}
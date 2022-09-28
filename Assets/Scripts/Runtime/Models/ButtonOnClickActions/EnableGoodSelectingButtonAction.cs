using System;
using Shooter.Shop;

namespace Shooter.Model
{
    public sealed class EnableGoodSelectingButtonAction : IButtonClickAction
    {
        private readonly ISelectingButtonFromDataFinder _selectingButtonFinder;
        private readonly IButtonClickAction _buttonClickAction;
        private readonly GoodData _data;

        public EnableGoodSelectingButtonAction(ISelectingButtonFromDataFinder selectingButtonFinder, GoodData data, IButtonClickAction buttonClickAction)
        {
            _selectingButtonFinder = selectingButtonFinder ?? throw new ArgumentNullException(nameof(selectingButtonFinder));
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _buttonClickAction = buttonClickAction ?? throw new ArgumentNullException(nameof(buttonClickAction));
        }

        public void OnClick()
        {
            _buttonClickAction.OnClick();
            _selectingButtonFinder.Find(_data).Enable();
        }
    }
}
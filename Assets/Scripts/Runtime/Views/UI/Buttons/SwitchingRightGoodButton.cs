﻿using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class SwitchingRightGoodButton : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _button;
        private ISwitchingGoodAction _switchingGoodAction;

        public void Subscribe(ISwitchingGoodAction switchingGoodAction)
        {
            _switchingGoodAction = switchingGoodAction ?? throw new ArgumentNullException(nameof(switchingGoodAction));
            _button.onClick.AddListener(TryClick);
        }

        private void TryClick()
        {
            if (_switchingGoodAction.CanSwitchRight())
            {
                _switchingGoodAction.SwitchRight();
            }
        }

        private void OnDestroy() => _button.onClick.RemoveListener(TryClick);
        
    }
}
using System;
using System.Collections.Generic;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class Button : MonoBehaviour, IButton
    {
        [SerializeField] private UnityEngine.UI.Button _button;
        private readonly List<IButtonClickAction> _buttonClickActions = new();
        
        public void Subscribe(IButtonClickAction buttonClickAction)
        {
            if (buttonClickAction is null)
                throw new ArgumentNullException(nameof(buttonClickAction));

            _buttonClickActions.Add(buttonClickAction);
            _button.onClick.AddListener(buttonClickAction.OnClick);
        }

        public void DeleteAllSubscribers()
        {
            foreach (var buttonClickAction in _buttonClickActions)
            {
                _button.onClick.RemoveListener(buttonClickAction.OnClick);
            }
        }

        public void Enable() => _button.interactable = true;
        
        public void Disable() => _button.interactable = false;

        private void OnDestroy() => DeleteAllSubscribers();
    }
}
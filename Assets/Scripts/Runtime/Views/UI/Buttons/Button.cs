using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class Button : MonoBehaviour, IButton
    {
        [SerializeField] private UnityEngine.UI.Button _button;
        private IButtonClickAction _buttonClickAction;

        public void Subscribe(IButtonClickAction buttonClickAction)
        {
            _buttonClickAction = buttonClickAction;
            _button.onClick.AddListener(buttonClickAction.OnClick);
        }

        public void Enable()
        {
            _button.interactable = true;
        }
        
        private void OnDestroy() => _button.onClick.RemoveListener(_buttonClickAction.OnClick);
    }
}
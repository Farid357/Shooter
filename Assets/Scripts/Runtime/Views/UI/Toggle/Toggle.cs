using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Toggle : MonoBehaviour, IToggle
    {
        [SerializeField] private UnityEngine.UI.Toggle _toggle;

        public bool IsOn { get; private set; }

        private void OnEnable() => _toggle.onValueChanged.AddListener(SetIsOn);

        private void OnDestroy() => _toggle.onValueChanged.RemoveListener(SetIsOn);

        private void SetIsOn(bool isOn) => IsOn = isOn;
    }
}
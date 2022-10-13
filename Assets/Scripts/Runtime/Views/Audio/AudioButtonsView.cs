using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class AudioButtonsView : MonoBehaviour, ISwitchingStateButtonActionView
    {
        [SerializeField] private AudioButton _mute;
        [SerializeField] private AudioButton _on;
        [SerializeField] private AudioSlider _slider;

        public void Visualize(bool isOn)
        {
            _mute.gameObject.SetActive(!isOn);
            _on.gameObject.SetActive(isOn);


            if (isOn)
            {
                _slider.Disable();
            }
            else
            {
                _slider.Enable();
            }
        }
    }
}
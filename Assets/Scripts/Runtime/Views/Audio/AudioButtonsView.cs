using Shooter.GameLogic;
using UnityEngine;

namespace Shooter.Model
{
    public sealed class AudioButtonsView : MonoBehaviour, ISaveAndChangeBoolButtonActionView
    {
        [SerializeField] private AudioButton _mute;
        [SerializeField] private AudioButton _on;
        
        public void Visualize(bool isOn)
        {
            _mute.gameObject.SetActive(!isOn);
            _on.gameObject.SetActive(isOn);
        }
    }
}
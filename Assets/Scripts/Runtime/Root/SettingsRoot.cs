using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class SettingsRoot : CompositeRoot
    {
        [SerializeField] private AudioButtonsView _audioButtonsView;
        [SerializeField] private AudioButton _muteAudioButton;
        [SerializeField] private AudioButton _onAudioButton;
        [SerializeField] private AudioSlider _audioSlider;
        [SerializeField] private ChangeAudioVolumeSliderAction _changeAudioVolumeSliderAction;
        
        public override void Compose()
        {
            var switchingStateButtonAction = new SwitchingStateButtonAction<AudioButton>(new BinaryStorage(), _audioButtonsView);
            _muteAudioButton.Subscribe(switchingStateButtonAction);
            _onAudioButton.Subscribe(switchingStateButtonAction);
            _audioSlider.Subscribe(_changeAudioVolumeSliderAction);
        }
    }
}
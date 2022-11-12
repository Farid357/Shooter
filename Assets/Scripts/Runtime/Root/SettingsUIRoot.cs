using Shooter.GameLogic;
using Shooter.GameLogic.Settings;
using Shooter.Model;
using Shooter.Model.Settings;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class SettingsUIRoot : CompositeRoot
    {
        [SerializeField] private AudioButtonsView _audioButtonsView;
        [SerializeField] private AudioButton _muteAudioButton;
        [SerializeField] private AudioButton _onAudioButton;
        [SerializeField] private AudioSlider _audioSlider;
        [SerializeField] private ChangeAudioVolumeSliderAction _changeAudioVolumeSliderAction;
        [SerializeField] private SwitchingCursorStateButton _button;
        [SerializeField] private FrameRateDropdown _frameRateDropdown;
        [SerializeField] private SwitchingButtonView _switchingCursorStateButton;
        [SerializeField] private ShadowResolutionDropdown _shadowQualityDropdown;
        [SerializeField] private QualityLevelDropdown _qualityLevelDropdown;
        [SerializeField] private UnityEngine.UI.Button _soundTabButton;
        
        private readonly FrameRateSelector _frameRateSelector = new();
        private readonly ShadowResolutionSelector _shadowResolutionSelector = new();
        private readonly QualityLevelSelector _qualityLevelSelector = new();
        private SwitchingStateButtonAction<AudioButton> _audioStateButtonAction;

        public override void Compose()
        {
            _qualityLevelDropdown.Create();
            _qualityLevelDropdown.OnSelected += _qualityLevelSelector.Select;
            _shadowQualityDropdown.Create();
            _shadowQualityDropdown.OnSelected += _shadowResolutionSelector.Select;
            _frameRateDropdown.Create();
            _frameRateDropdown.OnSelected += _frameRateSelector.Select;
             _audioStateButtonAction = new SwitchingStateButtonAction<AudioButton>(new BinaryStorage(), _audioButtonsView);
            _soundTabButton.onClick.AddListener(_audioStateButtonAction.Visualize);
            _muteAudioButton.Subscribe(_audioStateButtonAction);
            _button.Subscribe(new SwitchingStateButtonAction<SettingsRoot>(new BinaryStorage(), _switchingCursorStateButton));
            _onAudioButton.Subscribe(_audioStateButtonAction);
            _audioSlider.Subscribe(_changeAudioVolumeSliderAction);
        }

        private void OnDestroy()
        {
            _frameRateDropdown.OnSelected -= _frameRateSelector.Select;
            _shadowQualityDropdown.OnSelected -= _shadowResolutionSelector.Select;
            _qualityLevelDropdown.OnSelected -= _qualityLevelSelector.Select;
            _soundTabButton.onClick.RemoveListener(_audioStateButtonAction.Visualize);
        }
    }
}
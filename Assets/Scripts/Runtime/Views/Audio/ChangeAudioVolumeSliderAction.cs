using Shooter.SaveSystem;
using UnityEngine;
using UnityEngine.Audio;

namespace Shooter.GameLogic
{
    public sealed class ChangeAudioVolumeSliderAction : MonoBehaviour, ISliderChangedValueAction
    {
        [SerializeField] private CountView _countView;
        [SerializeField] private AudioMixer _audioMixer;
        
        private const string GroupName = "Master";
        private readonly StorageWithNameSaveObject<AudioMixer, float> _storage = new(new BinaryStorage());
        
        public void Change(float value)
        {
            _countView.Visualize(Mathf.RoundToInt(value * 100f));
            _storage.Save(value);
            _audioMixer.SetFloat(GroupName, value);
        }
    }
}
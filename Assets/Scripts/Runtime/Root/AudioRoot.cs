using Shooter.GameLogic;
using Shooter.SaveSystem;
using UnityEngine;
using UnityEngine.Audio;

namespace Shooter.Root
{
    public sealed class AudioRoot : CompositeRoot
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSlider _audioSlider;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        
        public override void Compose()
        {
            var storage = new StorageWithNameSaveObject<AudioMixer, float>(new BinaryStorage());
            var volume = storage.HasSave() ? storage.Load() : 1;
            _audioMixer.SetFloat(_audioMixerGroup.name, volume);
            _audioSlider.SetFloat(volume);
        }
    }
}
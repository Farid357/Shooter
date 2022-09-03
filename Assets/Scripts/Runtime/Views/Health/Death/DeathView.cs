using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class DeathView : MonoBehaviour, IDeathView
    {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private DeathWindow _window;
        [SerializeField] private GameObject _character;
        [SerializeField] private CharacterCamera _camera;
        
        private IToggle _soundToggle;

        public void Init(IToggle soundToggle)
        {
            _soundToggle = soundToggle ?? throw new ArgumentNullException(nameof(soundToggle));
        }

        public void VisualizeDeath()
        {
            if (_soundToggle.IsOn)
                _sound.Play();
            _window.Show();
            _character.SetActive(false);
            _camera.ClearParent();
        }
    }
}
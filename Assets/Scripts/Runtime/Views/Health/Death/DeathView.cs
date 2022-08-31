using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class DeathView : MonoBehaviour, IDeathView
    {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private DeathWindow _window;
        private IToggle _soundToggle;

        public void Init(IToggle soundToggle)
        {
            _soundToggle = soundToggle ?? throw new ArgumentNullException(nameof(soundToggle));
        }

        public void Visualize()
        {
            if (_soundToggle.IsOn)
                _sound.Play();
            _window.Show();
        }
    }
}
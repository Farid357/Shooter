using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterDeathView : MonoBehaviour, IDeathView
    {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private DeathWindow _window;
        [SerializeField] private CharacterMovement _character;
        [SerializeField] private CharacterCamera _camera;

        public void VisualizeDeath()
        {
            _sound.Play();
            _window.Show();
            _character.gameObject.SetActive(false);
            _camera.ClearParent();
        }
    }
}
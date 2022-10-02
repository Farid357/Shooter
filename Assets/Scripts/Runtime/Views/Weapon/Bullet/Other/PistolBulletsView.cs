using Shooter.Model;
using TMPro;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class PistolBulletsView : MonoBehaviour, IBulletsView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private ParticleSystem _particle;
        private bool _isNotFirstVisualize;
        
        public void Visualize(int bullets)
        {
            if (_isNotFirstVisualize)
            {
                _audio.PlayOneShot(_audio.clip);
                _particle.Play();
            }
            
            _text.text = "\u221E";
            _isNotFirstVisualize = true;
        }
    }
}
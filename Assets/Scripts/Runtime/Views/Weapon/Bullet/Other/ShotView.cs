using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ShotView : MonoBehaviour, IShotView
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private ParticleSystem _particle;
        
        public void VisualizeShot()
        {
            _audio.PlayOneShot(_audio.clip);
            _particle.Play();
        }
    }
}
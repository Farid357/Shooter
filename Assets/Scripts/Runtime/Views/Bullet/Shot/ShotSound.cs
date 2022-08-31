using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ShotSound : MonoBehaviour, IShotSound
    {
        [SerializeField] private AudioSource _audio;
        
        public void Play() => _audio.PlayOneShot(_audio.clip);
        
    }
}
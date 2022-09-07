using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ShotView : MonoBehaviour, IShotView
    {
        [SerializeField] private AudioSource _audio;
        
        public void Visualize() => _audio.PlayOneShot(_audio.clip);
        
    }
}
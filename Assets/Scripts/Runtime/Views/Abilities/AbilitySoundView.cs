using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class AbilitySoundView : MonoBehaviour, IAbilityView
    {
        [SerializeField] private AudioSource _applySound;
        
        public void VisualizeApply(float applySeconds) => _applySound.Play();
        
    }
}
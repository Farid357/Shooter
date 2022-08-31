using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class DeathWindow : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        
    }
}
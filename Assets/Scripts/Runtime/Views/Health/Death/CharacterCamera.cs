using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Camera))]
    public sealed class CharacterCamera : MonoBehaviour
    {
        public void ClearParent() => transform.SetParent(null);
        
    }
}
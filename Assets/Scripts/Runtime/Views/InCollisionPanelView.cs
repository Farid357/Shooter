using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public sealed class InCollisionPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<CharacterMovement>() != null)
            {
                _panel.SetActive(true);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.GetComponent<CharacterMovement>() != null)
            {
                _panel.SetActive(false);
            }
        }
    }
}
using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterMovement>() != null)
            {
                OnPicked();
                gameObject.SetActive(false);
            }
        }

        protected abstract void OnPicked();
    }
}
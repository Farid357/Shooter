using UnityEngine;

namespace Shooter.GameLogic.Inventory
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<CharacterMovement>() != null)
            {
                OnPicked();
            }
        }

        protected abstract void OnPicked();
    }
}
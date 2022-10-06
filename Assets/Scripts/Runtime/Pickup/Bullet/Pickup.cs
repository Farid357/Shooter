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
                gameObject.SetActive(false);
            }
        }

        protected abstract void OnPicked();
    }
}
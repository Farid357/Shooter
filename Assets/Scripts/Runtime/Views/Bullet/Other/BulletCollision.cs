using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public abstract class BulletCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthTransformView healthTransformView))
            {
                OnCollide(healthTransformView.Health, collision.transform.position);
            }

            gameObject.SetActive(false);
        }

        protected abstract void OnCollide(IHealth health, Vector3 point);
        
    }
}
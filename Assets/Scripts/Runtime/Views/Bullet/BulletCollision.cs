using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public sealed class BulletCollision : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _damage = 2;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthTransformView healthTransformView))
            {
                Attack(healthTransformView.Health);
            }
            
            gameObject.SetActive(false);
        }

        private void Attack(IHealth health)
        {
            if (health.IsAlive)
                health.TakeDamage(_damage);
        }
    }
}
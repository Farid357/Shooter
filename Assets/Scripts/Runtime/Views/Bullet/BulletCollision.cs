using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public sealed class BulletCollision : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _damage = 2;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealthCollision healthCollision))
            {
                healthCollision.TryTakeDamage(_damage);
                Debug.Log("hit");
            }

            gameObject.SetActive(false);
        }
    }
}
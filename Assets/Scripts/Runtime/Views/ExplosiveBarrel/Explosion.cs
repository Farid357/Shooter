using System.Linq;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Explosion : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int _damage = 10;
        [SerializeField, Min(0.1f)] private float _radius = 1.5f;

        public void Thunder()
        {
            var colliders = new Collider[2000];
            var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders);

            if (count > 0)
            {
                foreach (var collider in colliders.Where(collider1 => collider1 is not null))
                {
                    Debug.Log(collider.gameObject.name);
                    if (collider.TryGetComponent(out IHealthTransformView healthTransformView))
                    {
                        Debug.Log("check");
                        TryDamage(healthTransformView.Health);
                    }
                }
            }
        }

        private void TryDamage(IHealth health)
        {
            if (health.IsAlive)
                health.TakeDamage(_damage);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
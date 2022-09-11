using System.Linq;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class Explosion : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _radius = 1.5f;

        public void Thunder(int damage)
        {
            var colliders = new Collider[2000];
            var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders);

            if (count > 0)
            {
                foreach (var collider in colliders.Where(collider1 => collider1 is not null))
                {
                    if (collider.TryGetComponent(out IHealthTransformView healthTransformView))
                    {
                        TryDamage(healthTransformView.Health, damage);
                    }
                }
            }
        }

        private void TryDamage(IHealth health, int damage)
        {
            damage.TryThrowLessThanOrEqualsToZeroException();
            
            if (health.IsAlive)
                health.TakeDamage(damage);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
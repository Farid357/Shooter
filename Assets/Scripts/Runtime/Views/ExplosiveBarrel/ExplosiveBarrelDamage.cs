using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ExplosiveBarrelDamage : MonoBehaviour, IHealthView
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private AudioSource _sound;
        [SerializeField, Min(0.1f)] private float _radius = 1.5f;
        [SerializeField, Range(1, 100)] private int _damage;
        
        public void Visualize(int health)
        {
            if (health == 0)
            {
                gameObject.SetActive(false);
                Instantiate(_particlePrefab, transform.position, Quaternion.identity).Play();
                _sound.PlayOneShot(_sound.clip);
                Explode();
            }
        }

        private void Explode()
        {
            var colliders = new Collider[50];
            var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders);

            if (count > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out IHealthTransformView healthTransformView))
                    {
                        TryDamage(healthTransformView.Health);
                    }
                }
            }
        }

        private void TryDamage(IHealth health)
        {
            Debug.Log("Explode");
            if (health.IsAlive)
                health.TakeDamage(_damage);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
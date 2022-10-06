using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class HealBarrelExplosionView : MonoBehaviour, IHealthView
    {
        [SerializeField, Min(1)] private int _healAmount = 5;
        [SerializeField] private LayerMask _healMask;
        [SerializeField] private float _healRadius = 5f;
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private AudioSource _audio;

        public void Visualize(int healthCount)
        {
            if (healthCount == 0)
            {
                var colliders = Physics.OverlapSphere(transform.position, _healRadius, _healMask.value);
                _audio.Play();
                Instantiate(_particlePrefab, transform.position, Quaternion.identity).Play();
                TryHeal(colliders);
            }
        }

        private void TryHeal(Collider[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out IHealthTransformView healthTransformView))
                {
                    var health = healthTransformView.Health;

                    if (health.CanHeal(_healAmount))
                        health.Heal(_healAmount);
                }
            }
            
            gameObject.SetActive(false);
        }
    }
}
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ExplosiveBarrelExplosionView : MonoBehaviour, IHealthView
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private Explosion _explosion;
        [SerializeField, Range(1, 100)] private int _damage = 2;
        
        public void Visualize(int health)
        {
            if (health == 0)
            {
                gameObject.SetActive(false);
                Instantiate(_particlePrefab, transform.position, Quaternion.identity).Play();
                _sound.PlayOneShot(_sound.clip);
                _explosion.Thunder(_damage);
            }
        }
    }
}
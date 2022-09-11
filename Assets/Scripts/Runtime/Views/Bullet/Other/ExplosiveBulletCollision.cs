using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ExplosiveBulletCollision : BulletCollision, IBulletCollision
    {
        [SerializeField] private Explosion _explosionPrefab;
        [SerializeField] private ParticleSystem _explosionParticlePrefab;

        [SerializeField, ProgressBar(1, 100)] private int _damage = 2;
        
        public override int Damage => _damage;
        
        public override bool CanIncreaseDamage => false;
        
        private void OnCollisionEnter(Collision collision)
        {
            Explode(collision.contacts[0].point);
            gameObject.SetActive(false);
        }

        private void Explode(Vector3 point)
        {
            Instantiate(_explosionPrefab, point, Quaternion.identity).Thunder(Damage);
            Instantiate(_explosionParticlePrefab,  point, Quaternion.identity).Play();
        }

        public override void IncreaseDamageForSeconds(int damage, float seconds)
        {
         
        }
    }
}
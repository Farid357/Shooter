using System;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class GrenadeView : MonoBehaviour, IGrenade
    {
        [SerializeField, MinValue(0.2)] private float _force = 0.2f;
        [SerializeField] private Vector3 _throwDirection;
        [SerializeField, MinValue(0.4f)] private float _explosionSeconds = 1.2f;
        [SerializeField, ProgressBar(1, 100, G = 0, R = 1, B = 0)] private int _damage = 10;
        [SerializeField] private Explosion _explosionPrefab;
        [SerializeField] private ParticleSystem _explosionParticlePrefab;
        [SerializeField] private ItemGameObjectView _itemView;
        private Rigidbody _rigidbody;
        
        public IGameObjectItemView ItemView => _itemView;

        public bool CanShoot => gameObject.activeInHierarchy;
        
        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        public void Shoot()
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_throwDirection * _force);

            UniTask.Create(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_explosionSeconds));
                var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity).Play();
                explosion.Thunder(_damage);
                Destroy(gameObject);
            });
        }

    }
}
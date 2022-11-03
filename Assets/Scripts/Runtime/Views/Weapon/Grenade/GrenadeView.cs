using System;
using Cysharp.Threading.Tasks;
using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
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
        [SerializeField] private StandartExplosion _explosionPrefab;
        [SerializeField] private InventoryItemGameObjectView _itemView;
        private Rigidbody _rigidbody;

        public IInventoryItemGameObjectView ItemView => _itemView;

        public bool CanShoot => gameObject.activeInHierarchy;

        public bool HasDropped { get; private set; }

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        public async void Shoot()
        {
            HasDropped = true;
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_throwDirection * _force);
            await UniTask.Delay(TimeSpan.FromSeconds(_explosionSeconds));
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explosion.Thunder(_damage);
            gameObject.SetActive(false);
        }
    }
}
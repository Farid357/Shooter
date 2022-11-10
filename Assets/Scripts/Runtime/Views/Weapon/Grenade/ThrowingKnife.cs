using Shooter.GameLogic.Inventory;
using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ThrowingKnife : SerializedMonoBehaviour, IGrenade
    {
        [SerializeField, MinValue(0.2)] private float _force = 0.2f;
        [SerializeField] private Vector3 _throwDirection;
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

        public void Shoot()
        {
            HasDropped = true;
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_throwDirection * _force);
            gameObject.SetActive(false);
        }
    }
}
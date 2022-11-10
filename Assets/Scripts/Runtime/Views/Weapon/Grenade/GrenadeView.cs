using System;
using Cysharp.Threading.Tasks;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class GrenadeView : ThrowingWeaponView
    {
        [SerializeField] private ThrowingKnife _throwingKnife;
        [SerializeField, MinValue(0.4f)] private float _explosionSeconds = 1.2f;
        [SerializeField, ProgressBar(1, 100, G = 0, R = 1, B = 0)] private int _damage = 10;
        [SerializeField] private StandartExplosion _explosionPrefab;

        public override IInventoryItemGameObjectView ItemView => _throwingKnife.ItemView;

        public override bool CanShoot => gameObject.activeInHierarchy;

        public bool HasDropped => _throwingKnife.HasDropped;

        public override async void Shoot()
        {
            _throwingKnife.Shoot();
            await UniTask.Delay(TimeSpan.FromSeconds(_explosionSeconds));
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explosion.Thunder(_damage);
        }
    }
}
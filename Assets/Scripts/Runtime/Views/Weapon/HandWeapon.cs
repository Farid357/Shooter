using System;
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class HandWeapon : MonoBehaviour, IShootingWeapon
    {
        [SerializeField, Min(0.01f)] private float _attackDistance = 1.5f;
        [SerializeField, Min(0.01f)] private float _attackCooldown = 1.5f;

        [SerializeField, ProgressBar(0, 100, 1, 0, 0)]
        private int _damage = 50;
        
        private float _time;
        
        public bool CanShoot => _time == 0;
        
        private void Awake() => _time = _attackCooldown;

        private void Update()
        {
            _time = Mathf.Max(0, _time - Time.deltaTime);
            Debug.DrawRay(transform.position,transform.forward, Color.red);
        }

        public void Shoot()
        {
            if (CanShoot == false)
                throw new InvalidOperationException(nameof(CanShoot));

            _time = _attackCooldown;
            var ray = new Ray(transform.position, -transform.forward);
            if (Physics.Raycast(ray, out var hit, _attackDistance))
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out IHealthTransformView healthTransformView))
                {
                    if (healthTransformView.Health.IsAlive)
                        healthTransformView.Health.TakeDamage(_damage);
                }
            }
        }
    }
}
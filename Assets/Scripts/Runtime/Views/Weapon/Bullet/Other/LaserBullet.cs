using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class LaserBullet : Bullet
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField, Min(0.1f)] private float _maxDistance = 50;
        [SerializeField, ProgressBar(1, 100, 1, 0, 0)] private int _damage = 25;

        private readonly RayCastCatcher _rayCastHit = new();
        private Camera _camera;
        private bool _isThrowing;

        private void OnEnable() => _camera ??= Camera.main;

        public override async void Throw()
        {
            _isThrowing = true;
            _line.positionCount = 2;
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            _isThrowing = false;
            _line.positionCount = 0;
        }

        private void Update()
        {
            if (_isThrowing == false)
                return;

            var centerOfScreen = new Vector3(0.5f, 0.5f, 0f);
            _line.SetPosition(0, transform.position);
            var ray = _camera.ViewportPointToRay(centerOfScreen);

            if (_rayCastHit.Hit(out IHealthTransformView healthTransformView, ray, out var hit, _maxDistance))
            {
                _line.SetPosition(1, hit.point);
                var health = healthTransformView.Health;

                if (health.IsAlive)
                    health.TakeDamage(_damage);
            }

            else
            {
                _line.SetPosition(1, ray.GetPoint(_maxDistance));
            }
        }
    }
}
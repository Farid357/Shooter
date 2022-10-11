using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class BulletMovement : MonoBehaviour, IBullet
    {
        [SerializeField, Min(1)] private int _throwForce = 100;
        private Rigidbody _rigidbody;
        private Camera _camera;
        
        private void OnEnable()
        {
            _camera ??= Camera.main;
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        public void Throw()
        {
            var centerOfScreen = new Vector3(0.5f, 0.5f, 0f);
            var ray = _camera.ViewportPointToRay(centerOfScreen);
            _rigidbody.AddForce(ray.direction * _throwForce, ForceMode.Impulse);
        }
        
        public void Throw(Vector3 direction)
        {
            if (direction == Vector3.zero)
                throw new ArgumentOutOfRangeException(nameof(direction));
            
            _rigidbody.AddForce(direction * _throwForce / 2f, ForceMode.Impulse);
        }
    }
}
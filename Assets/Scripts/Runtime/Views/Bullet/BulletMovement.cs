using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Rigidbody), typeof(BulletCollision))]
    public sealed class BulletMovement : MonoBehaviour, IBullet
    {
        [SerializeField, Min(1)] private int _throwForce = 100;

        private Rigidbody _rigidbody;
        
        private void OnEnable() => _rigidbody = GetComponent<Rigidbody>();

        public void Throw()
        {
            _rigidbody.AddForce(Vector3.forward * _throwForce, ForceMode.Impulse);
        }
    }
}
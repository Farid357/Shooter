using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class CharacterMovement : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float _speed = 5f;
        [SerializeField, Min(1f)] private float _jumpForce = 100f;

        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction * _speed);
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
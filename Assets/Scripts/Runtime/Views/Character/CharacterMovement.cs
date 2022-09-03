using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public sealed class CharacterMovement : MonoBehaviour, ICharacter
    {
        [SerializeField, Min(0.001f)] private float _speed = 5f;
        [SerializeField, Min(1f)] private float _jumpForce = 10;

        private float _velocity;
        private CharacterController _characterController;

        public Vector3 Position => transform.position;

        public Quaternion Rotation => transform.rotation;

        public bool OnGround => _characterController.isGrounded;

        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void Update()
        {
            _velocity += Physics.gravity.y * Time.deltaTime;
            _characterController.Move(transform.up * _velocity * Time.deltaTime);
        }

        public void Move(Vector3 direction)
        {
            _characterController.Move(direction.normalized * _speed);
        }

        public void Jump()
        {
            if (OnGround == false)
                throw new InvalidOperationException(nameof(Jump));

            _velocity = _jumpForce;
        }
    }
}
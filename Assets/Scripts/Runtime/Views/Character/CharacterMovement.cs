using System;
using Cysharp.Threading.Tasks;
using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public sealed class CharacterMovement : MonoBehaviour, ICharacterMovement
    {
        [SerializeField, Min(0.001f)] private float _speed = 5f;
        [SerializeField, Min(1f)] private float _jumpForce = 10;

        private float _velocity;
        private CharacterController _characterController;

        public Vector3 Position => transform.position;

        public Quaternion Rotation => transform.rotation;

        public bool OnGround => _characterController.isGrounded;

        public bool CanIncreaseSpeed { get; private set; }
        
        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void Update()
        {
            _velocity += Physics.gravity.y * Time.deltaTime;
            _characterController.Move(transform.up * (_velocity * Time.deltaTime));
        }

        public void Move(Vector3 direction) => _characterController.Move(direction.normalized * _speed);

        public void Jump()
        {
            if (OnGround == false)
                throw new InvalidOperationException(nameof(Jump));

            _velocity = _jumpForce;
        }

        public void Rotate(Vector3 euler) => transform.rotation = Quaternion.Euler(euler);
        
        public void IncreaseSpeedForSeconds(float increaseSpeed, float seconds) => StartIncreaseSpeedForSeconds(increaseSpeed, seconds);

        private async UniTaskVoid StartIncreaseSpeedForSeconds(float increaseSpeed, float seconds)
        {
            if (CanIncreaseSpeed == false || _speed >= increaseSpeed)
                throw new InvalidOperationException(nameof(StartIncreaseSpeedForSeconds));
            
            var startSpeed = _speed;
            SetSpeed(increaseSpeed, false);
            await UniTask.Delay(TimeSpan.FromSeconds(seconds));
            SetSpeed(startSpeed, true);
        }

        private void SetSpeed(float speed, bool canIncreaseSpeed)
        {
            _speed = speed.TryThrowLessOrEqualsToZeroException();
            CanIncreaseSpeed = canIncreaseSpeed == CanIncreaseSpeed ? throw new InvalidOperationException(nameof(SetSpeed)) : canIncreaseSpeed;
        }
    }
}
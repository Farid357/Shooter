using System;
using System.Linq;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterMovementInput : IUpdateble, IFixedUpdatable
    {
        private readonly (KeyCode Key, Vector3 Direction)[] _datas =
        {
            new(KeyCode.W, Vector3.forward),
            new(KeyCode.S, Vector3.back),
            new(KeyCode.A, Vector3.left),
            new(KeyCode.D, Vector3.right)
        };

        private readonly CharacterMovement _movement;
        private Vector3 _direction;
        
        private bool NeedJump => Input.GetKeyDown(KeyCode.Space) && _movement.OnGround;

        public CharacterMovementInput(CharacterMovement movement)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
        }

        public void Update(float deltaTime)
        {
            if (_datas.Any(data => Input.GetKey(data.Key)))
            {
                _direction = _datas.First(data => Input.GetKey(data.Key)).Direction;
            }
            
            else
            {
                _direction = Vector3.zero;
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            if (_direction != Vector3.zero)
                _movement.Move(_direction);

            if (NeedJump)
            {
                _movement.Jump();
            }
        }
    }
}
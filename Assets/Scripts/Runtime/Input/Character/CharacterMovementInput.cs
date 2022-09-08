using System;
using System.Linq;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterMovementInput : IUpdateble, IFixedUpdatable
    {
        private readonly (KeyCode Key, CharacterDirection CharacterDirection)[] _datas;
        private readonly CharacterMovement _movement;
        private Vector3 _direction;

        private bool NeedJump => Input.GetKeyDown(KeyCode.Space) && _movement.OnGround;

        public CharacterMovementInput(CharacterMovement movement)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _datas = new(KeyCode, CharacterDirection)[]
            {
                new(KeyCode.W, new CharacterDirectionForward(_movement)),
                new(KeyCode.S, new CharacterDirectionBack(_movement)),
                new(KeyCode.A, new CharacterDirectionLeft(_movement)),
                new(KeyCode.D, new CharacterDirectionRight(_movement))
            };
        }

        public void Update(float deltaTime)
        {
            if (_datas.Any(data => Input.GetKey(data.Key)))
            {
                _direction = _datas.First(data => Input.GetKey(data.Key)).CharacterDirection.GetDirection();
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
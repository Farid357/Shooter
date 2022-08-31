using System;
using Shooter.Model;
using UnityEngine;
// ReSharper disable All

namespace Shooter.GameLogic
{
    public sealed class CharacterInput : IUpdateble
    {
        private readonly InputData[] _datas = 
        {
            new(KeyCode.W, Vector3.forward),
            new(KeyCode.S, Vector3.back),
            new(KeyCode.A, Vector3.left),
            new(KeyCode.D, Vector3.right)
        };

        private readonly CharacterMovement _movement;
        private Vector3 _direction;

        public CharacterInput(CharacterMovement movement)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
        }

        public void Update(float deltaTime)
        {
            foreach (var data in _datas)
            {
                if (Input.GetKeyDown(data.KeyCode))
                {
                    _direction = data.Direction;
                    _movement.Move(_direction);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movement.Jump();
            }
        }
    }
}
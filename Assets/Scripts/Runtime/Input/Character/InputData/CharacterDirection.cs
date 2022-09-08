using System;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class CharacterDirection
    {
        protected readonly CharacterMovement Movement;

        protected CharacterDirection(CharacterMovement movement)
        {
            Movement = movement ?? throw new ArgumentNullException(nameof(movement));
        }

        public abstract Vector3 GetDirection();
    }
}
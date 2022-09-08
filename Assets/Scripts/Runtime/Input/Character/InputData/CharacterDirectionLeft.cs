using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterDirectionLeft : CharacterDirection
    {
        public CharacterDirectionLeft(CharacterMovement movement) : base(movement)
        {
        }

        public override Vector3 GetDirection() => -Movement.transform.right;
    }
}
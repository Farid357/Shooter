using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterDirectionBack : CharacterDirection
    {
        public CharacterDirectionBack(CharacterMovement movement) : base(movement)
        {
        }

        public override Vector3 GetDirection() => -Movement.transform.forward;
    }
}
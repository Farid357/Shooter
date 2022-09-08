using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterDirectionRight : CharacterDirection
    {
        public CharacterDirectionRight(CharacterMovement movement) : base(movement)
        {
        }

        public override Vector3 GetDirection() => Movement.transform.right;
    }
}
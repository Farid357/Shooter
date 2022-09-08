using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class CharacterDirectionForward : CharacterDirection
    {
        public CharacterDirectionForward(CharacterMovement movement) : base(movement)
        {
        }

        public override Vector3 GetDirection() => Movement.transform.forward;
    }
}
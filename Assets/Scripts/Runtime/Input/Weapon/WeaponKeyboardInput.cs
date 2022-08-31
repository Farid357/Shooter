using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class WeaponKeyboardInput : IWeaponInput
    {
        public bool IsPressingLeftMouseButton => Input.GetMouseButton(0);
    }
}
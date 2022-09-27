using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class BurstWeaponInput : IWeaponInput
    {
        public bool IsPressingLeftMouseButton => Input.GetMouseButtonDown(0);
    }
}
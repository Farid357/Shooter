using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class StandartWeaponInput : IWeaponInput
    {
        public bool IsPressingLeftMouseButton => Input.GetMouseButton(0);
    }
}
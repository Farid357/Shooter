using Shooter.GameLogic;

namespace Shooter.Model.Inventory
{
    public sealed class DummyWeaponInput : IWeaponInput
    {
        public bool IsPressingLeftMouseButton => false;
    }
}
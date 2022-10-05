using Shooter.GameLogic;

namespace Shooter.Model.Inventory
{
    public sealed class DummyPotionInput : IPotionInput
    {
        public bool HasInputed => false;
    }
}
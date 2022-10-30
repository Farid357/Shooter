using Shooter.GameLogic.Inventory;

namespace Shooter.Model
{
    public interface IGrenade : IDroppingWeapon
    {
        IInventoryItemGameObjectView ItemView { get; }

    }
}
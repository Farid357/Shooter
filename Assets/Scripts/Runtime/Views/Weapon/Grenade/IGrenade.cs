using Shooter.GameLogic.Inventory;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public interface IGrenade : IDroppingWeapon
    {
        IInventoryItemGameObjectView ItemView { get; }

    }
}
using Shooter.GameLogic.Inventory;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public interface IGrenade : IShootingWeapon
    {
        IInventoryItemGameObjectView ItemView { get; }
        
    }
}
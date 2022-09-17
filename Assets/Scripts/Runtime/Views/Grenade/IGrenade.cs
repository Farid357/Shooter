using Shooter.GameLogic.Inventory;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public interface IGrenade : IShootingWeapon
    {
        public IGameObjectItemView ItemView { get; }
    }
}
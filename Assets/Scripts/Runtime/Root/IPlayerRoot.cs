using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Player;

namespace Shooter.Root
{
    public interface IPlayerRoot
    {
        public IPlayer Compose(IWeapon weapon, IWeaponInput weaponInput);
    }
}
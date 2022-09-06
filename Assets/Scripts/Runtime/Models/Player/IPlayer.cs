using Shooter.Model;

namespace Shooter.Player
{
    public interface IPlayer
    {
        public void SwitchWeapon(IWeapon weapon);
    }
}
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.Player;

namespace Shooter.Root
{
    public interface IPlayerRoot
    {
        void Compose(IWeaponInput weaponInput, IShootingWeapon weapon);

        void Compose(IPotionInput potionInput, IPotion potion);

        void Compose(IWeaponInput weaponInput, IGrenade grenade);
    }
}
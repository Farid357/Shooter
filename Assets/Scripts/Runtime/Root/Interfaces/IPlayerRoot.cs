using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Root
{
    public interface IPlayerRoot
    {
        IDroppingWeapon ComposedDroppingWeapon { get; }
        
        void Compose(IWeaponInput weaponInput, IWeapon weapon);

        void Compose(IWeaponInput weaponInput, IGrenade grenade);
    }
}
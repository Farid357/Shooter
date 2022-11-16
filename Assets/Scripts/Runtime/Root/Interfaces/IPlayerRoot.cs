using Shooter.Model;

namespace Shooter.Root
{
    public interface IPlayerRoot
    {
        IThrowingWeapon ComposedThrowingWeapon { get; }
        
        void Compose(IWeaponInput weaponInput, IWeapon weapon);

        void Compose(IWeaponInput weaponInput, IThrowingWeapon throwingWeapon);
    }
}
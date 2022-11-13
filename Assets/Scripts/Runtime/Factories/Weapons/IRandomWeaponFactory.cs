namespace Shooter.Model
{
    public interface IRandomWeaponFactory
    {
        (IWeapon, IWeaponInput) Get();
    }
}
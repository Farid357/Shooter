namespace Shooter.Model
{
    public interface IDroppingWeapon : IShootingWeapon
    {
        public bool HasDropped => false;

    }
}
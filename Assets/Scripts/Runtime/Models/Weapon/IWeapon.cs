namespace Shooter.Model
{
    public interface IWeapon
    {
        public void Shoot();

        public bool CanShoot { get; }
    }
}
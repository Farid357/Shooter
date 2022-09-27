namespace Shooter.Model
{
    public interface IShootingWeapon
    {
        public void Shoot();
        
        public bool CanShoot { get; }

        public bool HasShot => false;
        
    }
}
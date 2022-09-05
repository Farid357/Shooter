using Shooter.Model;

public sealed class DummyWeapon : IWeapon
{
    public void Shoot()
    {
        
    }

    public bool CanShoot { get; }
}
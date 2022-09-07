using Shooter.Model;
using Shooter.Test;

public sealed class DummyWeapon : IWeapon
{
    public void Shoot()
    {
        
    }

    public bool CanShoot => true;
    
}
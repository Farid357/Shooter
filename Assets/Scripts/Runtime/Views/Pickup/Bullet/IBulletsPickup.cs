using Shooter.Model;

public interface IBulletsPickup
{
    public void Init(IInventory<IWeapon> inventory);
}
using Shooter.GameLogic;
using Shooter.Model;

public interface IBulletsPickup
{
    public void Init(IInventory<(IWeapon, IWeaponInput)> inventory);
}
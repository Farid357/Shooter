using System;
using Shooter.Model;

public sealed class PistolBulletsPickup : BulletsPickup
{
    protected override Type WeaponTypeForAddBullets => typeof(Pistol);
}
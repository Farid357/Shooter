using System;
using Shooter.Model;

public sealed class Ak74BulletsPickup : BulletsPickup
{
    protected override Type WeaponTypeForAddBullets => typeof(Ak74);
}
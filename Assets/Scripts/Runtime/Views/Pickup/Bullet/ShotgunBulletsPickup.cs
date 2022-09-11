using System;
using Shooter.Model;

public sealed class ShotgunBulletsPickup : BulletsPickup
{
    protected override Type WeaponTypeForAddBullets => typeof(Shotgun);
}
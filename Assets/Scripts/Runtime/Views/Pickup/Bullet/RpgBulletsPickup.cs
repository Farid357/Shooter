using System;
using Shooter.Model;

public sealed class RpgBulletsPickup : BulletsPickup
{
    protected override Type WeaponTypeForAddBullets => typeof(Rpg);
}
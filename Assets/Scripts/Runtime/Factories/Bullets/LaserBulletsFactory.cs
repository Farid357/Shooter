using System;
using Shooter.Model;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class LaserBulletsFactory : BulletsFactory
    {
        [SerializeField] private LaserBullet _laserBullet;
        
        public override event Action<Bullet> OnCreated;
        
        public override IBullet Create()
        {
            OnCreated?.Invoke(_laserBullet);
            return _laserBullet;
        }
    }
}
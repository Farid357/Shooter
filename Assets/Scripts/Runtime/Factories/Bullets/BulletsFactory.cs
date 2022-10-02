using System;
using Shooter.Model;
using Sirenix.OdinInspector;

namespace Shooter.GameLogic
{
    public abstract class BulletsFactory : SerializedMonoBehaviour, IBulletsFactory
    {
        public abstract IBullet Create();

        public abstract event Action<BulletMovement> OnCreated;
    }
}
using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class WeaponData : SerializedMonoBehaviour
    {
        [field: SerializeField] public IFactory<IBullet> BulletsFactory { get; private set; }
        
        [field: SerializeField] public IBulletsView BulletsView { get; private set; }
        
        [field: SerializeField, MinValue(0.01f)] public float WaitSeconds { get; private set; } = 0.01f;

        [field: SerializeField, MinValue(1)] public int Bullets { get; private set; } = 1;
    }
}
using Shooter.GameLogic.Inventory;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class ThrowingWeaponData : SerializedMonoBehaviour
    {
        [field: SerializeField] public GrenadePickup Prefab { get; private set; }
        
        [field: SerializeField] public IFactory<ThrowingWeaponView> Factory { get; private set; }
    }
}
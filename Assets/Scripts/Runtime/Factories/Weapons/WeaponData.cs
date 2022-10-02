using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class WeaponData : SerializedMonoBehaviour
    {
        [field: SerializeField, MinValue(0.01f)] public float WaitSeconds { get; private set; } = 0.01f;

        [field: SerializeField] public IBulletsView BulletsView { get; private set; }

        [field: SerializeField, MinValue(1)] public int Bullets { get; private set; } = 1;
    }
}
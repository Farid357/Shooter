using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class WeaponData : SerializedMonoBehaviour
    {
        [field: SerializeField] public float WaitSeconds { get; private set; }

        [field: SerializeField] public IBulletsView BulletsView { get; private set; }

        [field: SerializeField] public IShotView ShotView { get; private set; }

        [field: SerializeField] public int Bullets { get; private set; }
    }
}
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class DualWeaponData : MonoBehaviour
    {
        [field: SerializeField] public WeaponData FirstData { get; private set; }
        
        [field: SerializeField] public WeaponData SecondData { get; private set; }

    }
}
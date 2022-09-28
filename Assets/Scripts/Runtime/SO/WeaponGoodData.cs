using UnityEngine;

namespace Shooter.Shop
{
    [CreateAssetMenu(menuName = "Create/Weapon Good Data", fileName = "Good")]
    public sealed class WeaponGoodData : GoodData
    {
        [field: SerializeField] public WeaponType Type { get; private set; }
    }
}
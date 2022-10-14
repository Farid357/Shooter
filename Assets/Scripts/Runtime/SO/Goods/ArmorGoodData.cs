using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Shop
{
    [CreateAssetMenu(menuName = "Create/Armor Good Data", fileName = "Armor Good")]
    public sealed class ArmorGoodData : GoodData
    {
        [field: SerializeField, PropertyRange(1, 100)] public int Protection { get; private set; }
    }
}
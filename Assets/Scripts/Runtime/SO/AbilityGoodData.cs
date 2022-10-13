using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Shop
{
    [CreateAssetMenu(menuName = "Create/Ability Good Data", fileName = "Ability Good")]
    public sealed class AbilityGoodData : GoodData
    {
        [field: SerializeField, PropertyRange(0.01f, 300)] public float ForUsingSeconds { get; private set; }
    }
}
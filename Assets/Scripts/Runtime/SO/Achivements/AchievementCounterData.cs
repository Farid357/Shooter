using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Model
{
    public abstract class AchievementCounterData : ScriptableObject
    {
        [field: SerializeField, MinValue(1)] public int NeedAmount { get; private set; }
        
        [field: SerializeField] public AchievementViewData ViewData { get; private set; }
    }
}
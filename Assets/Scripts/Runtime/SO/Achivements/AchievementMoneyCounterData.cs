using UnityEngine;

namespace Shooter.Model
{
    [CreateAssetMenu(menuName = "Create/Achievement/Money Counter Data", fileName = "Achievement Money Counter Data", order = 0)]
    public sealed class AchievementMoneyCounterData : AchievementCounterData
    {
        [field: SerializeField] public int MoneyReward { get; private set; }
        
    }
}
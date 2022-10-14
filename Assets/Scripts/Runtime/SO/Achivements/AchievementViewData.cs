using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Model
{
    [CreateAssetMenu(menuName = "Create/Achievement View Data", fileName = "AchievementViewData")]
    public sealed class AchievementViewData : ScriptableObject
    {
        [field: SerializeField, OnValueChanged("Validate")] public string Name { get; private set; }
        
        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
                Debug.LogWarning("Name is null!");
            
        }
    }
}
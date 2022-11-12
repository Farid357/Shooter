using UnityEngine;

namespace Shooter.GameLogic.Settings
{
    [CreateAssetMenu(menuName = "Create/Dropdown Data", fileName = "DropdownData", order = 0)]
    public sealed class DropdownData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}